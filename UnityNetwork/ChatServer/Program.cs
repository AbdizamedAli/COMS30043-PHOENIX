using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.IO;
using UnityNetwork;
using System.Threading;
//using System.Runtime.Serialization.Formatters.Binary;

namespace ChatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ChatServer server = new ChatServer();
            server.RunServer(8000);
        }

        public class ChatServer : MyEventHandler
        {
            public enum MessageID
            {
                Chat = 100,  // 聊天消息标志符
            }

            private TCPPeer peer;   // 服务端
            private List<Socket> peerList;  // 保存所有的客户端连接
            private Thread thread;  // 逻辑线程
            private bool isRunning = false;  // 用于关闭线程的标志
            protected EventWaitHandle waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset); // 用于暂停线程

            public ChatServer()
            {
                peerList = new List<Socket>();
            }

            // 启动服务器
            public void RunServer(int port)
            {
                // 添加聊天事件
                AddHandler((short)TCPPeer.MessageID.OnNewConnection, OnAccepted);
                AddHandler((short)TCPPeer.MessageID.OnDisconnect, OnLost);
                AddHandler((short)MessageID.Chat, OnChat);

                peer = new TCPPeer(this);
                peer.Listen(port);

                isRunning = true;
                thread = new Thread(UpdateHandler); // 创建逻辑线程
                thread.Start();
                Console.WriteLine("启动聊天服务器");
            }

            // 处理服务器接受客户端的连接
            public void OnAccepted(Packet packet)
            {
                Console.WriteLine("接受新的连接 {0}", packet.sk.RemoteEndPoint);
                peerList.Add(packet.sk);
            }

            // 处理丢失连接
            public void OnLost(Packet packet)
            {
                Console.WriteLine("丢失连接 {0}", packet.sk.RemoteEndPoint);
                peerList.Remove(packet.sk);
            }

            // 处理聊天消息
            public void OnChat(Packet packet)
            {
                string message = string.Empty;
                byte[] bs = null;
                using (MemoryStream stream = packet.Stream)
                {
                    try
                    {
                        BinaryReader reader = new BinaryReader(stream);
                        int byteLen = reader.ReadInt32(); // 读的规则要与客户端写一致
                        bs = reader.ReadBytes(byteLen);

                        // 反序列化
                        Chat.ChatProto chat = Packet.Deserialize<Chat.ChatProto>(bs);
                        Console.WriteLine("{0}: {1}", chat.userName, chat.chatMsg);
                    }
                    catch { return; }
                }
                
                // 准备需要发送的数据包
                Packet response = new Packet((short)MessageID.Chat);
                using (MemoryStream stream = response.Stream)
                {
                    try
                    {
                        BinaryWriter writer = new BinaryWriter(stream);
                        writer.Write(bs.Length);
                        writer.Write(bs);
                        response.EncodeHeader(stream);
                    }
                    catch{
                        return;
                    }
                }

                // 广播给所有客户端
                foreach (Socket sk in peerList)
                {
                    TCPPeer.Send(sk, response);
                }
            }

            // 这个函数被重写了
            public override void AddPacket(Packet packet)
            {
                lock (Packets)
                {
                    Packets.Enqueue(packet);
                    waitHandle.Set();  // 通知逻辑线程继续运行
                }
            }

            private void UpdateHandler()
            {         
                while (isRunning)
                {
                    waitHandle.WaitOne(-1);  // 等待直到新的信号可以继续运行
                    //Thread.Sleep(30); // 也可以等待N毫秒更新一次
                    ProcessPackets();
                }

                thread.Join(); // 回到主线程
                Console.WriteLine("关闭事件结程");
            }
        }
    }
}

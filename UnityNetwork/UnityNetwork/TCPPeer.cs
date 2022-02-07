using System;
using System.Net;
using System.Net.Sockets;

namespace UnityNetwork
{
    public class TCPPeer
    {
        public enum MessageID
        {
            OnNewConnection = 1, //服务器接受新的连接
            OnConnected = 2,       // 连接到服务器
            OnConnectFail = 3,       // 无法连接服务器
            OnDisconnect = 4,    // 失去远程的连接
        }

        // 网络事件管理器
        protected MyEventHandler handler;

        public TCPPeer (MyEventHandler h) { handler = h; }
       
        // 作为服务器，开始监听
        public void Listen( int port, int backlog=128 )
        {
            // 监听端口
            IPEndPoint ipe = new IPEndPoint(IPAddress.Any, port);
            // 创建socket
            Socket socket = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                // 将socket绑定到端口上
                socket.Bind(ipe);
                // 开始监听
                socket.Listen(backlog);
                // 开始异步接受连接
                socket.BeginAccept(new System.AsyncCallback(ListenCallback), socket);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);  // 这里发生的错误多数和端口被占用有关
            }
        }

        // 作为服务器，异步接受一个新的连接, 如果连接成功，取得远程客户端的Socket，开始异步接收客户端发来的数据
        void ListenCallback(System.IAsyncResult ar)
        {
            // 取得服务器socket
            Socket listener = (Socket)ar.AsyncState;
            try
            {
                // 获得客户端的socket
                Socket client = listener.EndAccept(ar);

                // 发布消息到逻辑队列
                handler.AddPacket(new Packet((short)MessageID.OnNewConnection, client));

                // 创建接收数据的数据包
                Packet packet = new Packet(0, client);
                // 开始接收来自客户端的数据
                client.BeginReceive(packet.buffer, 0, Packet.headerLength, SocketFlags.None, new System.AsyncCallback(ReceiveHeader), packet);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // 继续接受其它连接
            listener.BeginAccept(new System.AsyncCallback(ListenCallback), listener);
        }

        // 作为客户端，开始异步连接服务器
        public Socket Connect( string ip, int port )
        {
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(ip), port);
            try
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // 开始连接
                socket.BeginConnect(ipe, new System.AsyncCallback(ConnectionCallback), socket);
                return socket;
            }
            catch (System.Exception e)
            {
                // 无法连接服务器
                handler.AddPacket(new Packet((short)MessageID.OnConnectFail));
                return null;
            }
        }

        // 作为客户端，异步连接回调
        void ConnectionCallback(System.IAsyncResult ar)
        {
            Socket client = (Socket)ar.AsyncState;
            try
            {
                // 与服务器取得连接
                client.EndConnect(ar);

                // 通知已经成功连接到服务器
                handler.AddPacket(new Packet((short)MessageID.OnConnected, client));

                // 开始异步接收服务器信息
                Packet packet = new Packet(0, client);
                client.BeginReceive(packet.buffer, 0, Packet.headerLength, SocketFlags.None, new System.AsyncCallback(ReceiveHeader), packet);

            }
            catch (System.Exception e)
            {
                handler.AddPacket(new Packet((short)MessageID.OnConnectFail, client));
            }
        }

        // 无论是创建用于监听的服务器Socket还是用于发起连接的客户端Socket，
        // 最后都会进入接收数据状态。
        // 接收数据主要是通过ReceiveHeader和ReceiveBody两个函数。
        void ReceiveHeader(System.IAsyncResult ar)
        {
            Packet packet = (Packet)ar.AsyncState;
            try
            {
                // 返回网络上接收的数据长度
                int read = packet.sk.EndReceive(ar);
                // 已断开连接
                if (read < 1)
                {
                    // 通知丢失连接
                    handler.AddPacket(new Packet((short)MessageID.OnDisconnect, packet.sk));
                    return;
                }

                packet.readLength += read;
                // 消息头必须读满4个字节
                if (packet.readLength < Packet.headerLength)
                {
                    packet.sk.BeginReceive(packet.buffer,
                        packet.readLength, Packet.headerLength - packet.readLength, // 这次只读入剩余的数据
                        SocketFlags.None,
                        new System.AsyncCallback(ReceiveHeader),
                        packet);
                }
                else
                {
                    // 获得实际数据长度
                    packet.DecodeHeader();
                    packet.readLength = 0;

                    // 开始读取消息体
                    packet.sk.BeginReceive(packet.buffer, Packet.headerLength, packet.bodyLength,
                        SocketFlags.None,
                        new System.AsyncCallback(ReceiveBody),
                        packet);
                }

            }
            catch (System.Exception e)
            {
                handler.AddPacket(new Packet((short)MessageID.OnDisconnect, packet.sk));
            }
        }

        // 接收体消息
        void ReceiveBody(System.IAsyncResult ar)
        {
            Packet packet = (Packet)ar.AsyncState;

            try
            {
                // 返回网络上接收的数据长度
                int read = packet.sk.EndReceive(ar);
                // 已断开连接
                if (read < 1)
                {
                    // 通知丢失连接
                    handler.AddPacket(new Packet((short)MessageID.OnDisconnect, packet.sk));
                    return;
                }
                packet.readLength += read;

                // 消息体必须读满指定的长度
                if ( packet.readLength < packet.bodyLength)
                {
                    packet.sk.BeginReceive(packet.buffer,
                        Packet.headerLength + packet.readLength,
                        packet.bodyLength - packet.readLength,
                        SocketFlags.None,
                        new System.AsyncCallback(ReceiveBody),
                        packet);
                }
                else
                {
                    // 复制读入的数据包，将其传入到逻辑处理队列
                    Packet newpacket = new Packet(packet);
                    handler.AddPacket(newpacket);

                    // 下一个读取
                    packet.ResetParams();
                    packet.sk.BeginReceive(packet.buffer,
                        0,
                        Packet.headerLength,
                        SocketFlags.None,
                        new System.AsyncCallback(ReceiveHeader),
                        packet);
                }
            }
            catch (System.Exception e)
            {
                handler.AddPacket(new Packet((short)MessageID.OnDisconnect, packet.sk));
            }
        }

        // 向远程发送消息
        public static void Send( Socket sk, Packet packet  )
        {
            if (!packet.encoded)
            {
                throw new Exception("无效数据包!");
            }
            NetworkStream ns;
            lock (sk)
            {
                ns = new NetworkStream(sk);
                if (ns.CanWrite)
                {
                    try
                    {
                        ns.BeginWrite( packet.buffer, 0, Packet.headerLength + packet.bodyLength, new System.AsyncCallback(SendCallback), ns);
                    }
                    catch (System.Exception e){}
                }
            }
        }

        //发送回调
        private static void SendCallback(System.IAsyncResult ar)
        {
            NetworkStream ns = (NetworkStream)ar.AsyncState;
            try
            {
                ns.EndWrite(ar);
                ns.Flush();
                ns.Close();
            }
            catch (System.Exception e){}
        }
    }
}

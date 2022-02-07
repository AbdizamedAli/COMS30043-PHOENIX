using System.Collections.Generic;

namespace UnityNetwork
{
    public class MyEventHandler
    {
        // 回调函数
        public delegate void OnReceive( Packet packet );

        // 每个事件对应一个OnReceive函数
        protected Dictionary<int, OnReceive> handlers;

        // 存储数据包的队列
        protected Queue<Packet> Packets = new Queue<Packet>();

        public MyEventHandler()
        {
            handlers = new Dictionary<int, OnReceive>();
        }

        // 添加网络事件
        public void AddHandler(int msgid, OnReceive handler)
        {
            handlers.Add(msgid, handler);
        }

        // 将数据包入队，然后在ProcessPackets函数中处理数据包。
        // 因为网络和逻辑处理有可能是在不同的线程中，所以在入队出队的时候使用了lock防止多线程带来的问题。
        public virtual void AddPacket( Packet packet )
        {
            lock (Packets)
            {
                Packets.Enqueue(packet);
            }
        }

        // 数据包出队
        public Packet GetPacket()
        {
            lock (Packets)
            {
                if (Packets.Count == 0)
                    return null;
                return Packets.Dequeue();
            }
        }

        public void ProcessPackets()
        {
            Packet packet = null;
            for (packet = GetPacket(); packet != null; )
            {
                OnReceive handler = null;
                if (handlers.TryGetValue(packet.msgid, out handler))
                {
                    // 根据消息标识符执行相应的OnReceive函数
                    if (handler != null)
                        handler(packet);
                }
                // 继续处理其它包
                packet = GetPacket();
            }
        }
    } // class
} // namespace

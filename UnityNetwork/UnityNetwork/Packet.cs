using System;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace UnityNetwork
{
    public class Packet
    {
        // 数据包头4个字节作为保留字节
        // 0-1个字节用来保存数据长度
        // 2-3个字节用来保存消息id
        public const int headerLength = 4;
        
        public short msgid = 0; // 消息id，占2个字节
        public Socket sk = null;   // 接收数据的socket
        public byte[] buffer = new byte[1024];  // 用于保存数据的数组
        public int readLength = 0;   // TCP数据流读取的字节长度
        public short bodyLength = 0;   // 有效数据的长度，占2个字节
        public bool encoded = false; // 标志是否已经处理过头4个字节

        public Packet(short id, Socket s=null) {
            msgid = id;
            sk = s;
            byte[] bs = BitConverter.GetBytes(id);
            bs.CopyTo(buffer, 2);
        }

        // 复制构造函数
        public Packet(Packet p) {
            msgid = p.msgid;
            sk = p.sk;
            p.buffer.CopyTo(buffer, 0);
            bodyLength = p.bodyLength;
            readLength = p.readLength;
            encoded = p.encoded;
        }

        // 重置
        public void ResetParams()
        {
            msgid = 0;
            readLength = 0;
            bodyLength = 0;
            encoded = false;
        }

        // 将short类型的数据长度转为2个字节保存到byte数组的最前面
        public void EncodeHeader(MemoryStream stream)
        {
            if(stream!=null) bodyLength = (short)stream.Position;
            byte[] bs = BitConverter.GetBytes(bodyLength);
            bs.CopyTo(buffer, 0);

            encoded = true;
        }

        // 从byte数组头4个字节中解析出数据的长度和消息id
        public void DecodeHeader()
        {
            bodyLength = System.BitConverter.ToInt16(buffer, 0);
            msgid = System.BitConverter.ToInt16(buffer, 2);
        }

        // 用于读写数据流
        public MemoryStream Stream
        {
            get
            {
                return new MemoryStream(buffer, headerLength, buffer.Length - headerLength );
            }
        }

        // 序列化对象，这里使用的是C#自带的序列化类，也可以替换为JSON等序列化方式
        public static byte[] Serialize<T>(T t)
        {

            using (MemoryStream stream = new MemoryStream())
            {
                try
                {
                    // 创建序列化类
                    BinaryFormatter bf = new BinaryFormatter();
                    //序列化到stream中
                    bf.Serialize(stream, t);
                    stream.Seek(0, SeekOrigin.Begin);
                    return stream.ToArray();
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        // 反序列化对象，这里使用的是C#自带的序列化类，也可以替换为JSON等序列化方式
        public static T Deserialize<T>(byte[] bs)
        {
            using (MemoryStream stream = new MemoryStream(bs))
            {
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    T t = (T)bf.Deserialize(stream);
                    return t;
                }
                catch (Exception e)
                {
                    return default(T);
                }
            }
        }
    }
}

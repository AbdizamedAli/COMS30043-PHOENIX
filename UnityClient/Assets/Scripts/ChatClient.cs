using UnityEngine;
using UnityEngine.UI;
using UnityNetwork;
using System.IO;

public class ChatClient : MonoBehaviour {
    public enum MessageID
    {
        Chat = 100,  // 本示例中唯一的消息标志符
    }

    ChatHandler eventHandler;

    public Text revTxt;  // UI控件 收到的聊天消息
    public InputField inputText; // UI控件 输入的聊天消息
    public Button sendMsgButton; // UI控件 发送聊天消息按钮

    // Use this for initialization
    void Start () {

        // 在这里创建ChatManager实例
        eventHandler = new ChatHandler();
        // 注册一个聊天消息
        eventHandler.AddHandler((short)TCPPeer.MessageID.OnConnected, OnConnected);
        eventHandler.AddHandler((short)TCPPeer.MessageID.OnConnectFail, OnConnectFailed);
        eventHandler.AddHandler((short)TCPPeer.MessageID.OnDisconnect, OnLost);
        eventHandler.AddHandler((short)MessageID.Chat, OnChat);

        eventHandler.ConnectToServer();

        sendMsgButton.onClick.AddListener(delegate ()
        {
            SendChat();  // 点击按钮发送消息
        });
    }
	
	// Update is called once per frame
	void Update () {
        // unity中处理逻辑使用的是单线程
        eventHandler.ProcessPackets();
	}
    
    // 处理客户端取得与服务器的连接
    public void OnConnected(Packet packet)
    {
        Debug.Log("成功连接到服务器");
    }

    // 处理客户端与服务器连接失败
    public void OnConnectFailed(Packet packet)
    {
        Debug.Log("连接服务器失败，请退出");
    }

    // 处理丢失连接
    public void OnLost(Packet packet)
    {
        Debug.Log("丢失与服务器的连接");
    }

    // 发送聊天消息
    void SendChat()
    {
        // 聊天协议
        Chat.ChatProto proto = new Chat.ChatProto();
        proto.userName = "客户端";
        proto.chatMsg = inputText.text;

        // 序列化
        byte[] bs = Packet.Serialize<Chat.ChatProto>(proto);

        // 创建数据包
        Packet p = new Packet((short)MessageID.Chat);
        using (MemoryStream stream = p.Stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(bs.Length);
            writer.Write(bs);
            p.EncodeHeader(stream);
        }
        // 发送到服务器
        eventHandler.SendMessage(p);

        //清空输入框
        inputText.text = "";
    }

    // 处理聊天消息
    public void OnChat(Packet packet)
    {
        byte[] buffer = null;
        using (MemoryStream stream = packet.Stream)
        {
            BinaryReader reader = new BinaryReader(stream);
            int len = reader.ReadInt32();
            buffer = reader.ReadBytes(len);
        }
        Chat.ChatProto proto = Packet.Deserialize<Chat.ChatProto>(buffer);
        revTxt.text = proto.userName + ":" + proto.chatMsg;  // 显示收到的消息
        Debug.Log("收到服务器的消息:" + proto.chatMsg);
    }
}

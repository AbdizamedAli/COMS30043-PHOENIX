using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviourPunCallbacks
{
    PhotonView photonView;
    public InputField contentInput;//聊天输入框
    public GameObject textPrefab;//文本预制体
    public Transform layoutContent;//父物体
    public int flag = 0;
    void Start()
    {
        photonView = GetComponent<PhotonView>();

    }

    void Update()
    {
        if (flag==1&&Input.GetKeyDown(KeyCode.Return))
        {
            SendMessInfoBtn();
            contentInput.enabled = false;
            flag = 0;
        }
        if (flag==0&&Input.GetKeyDown(KeyCode.Return))
        {
            contentInput.enabled = true;
            flag = 1;
        }
    }
    //发送消息
    public void SendMessInfoBtn()
    {
        string info = PhotonNetwork.LocalPlayer.NickName + " :" + contentInput.text;
        photonView.RPC("SendMess", RpcTarget.All, info);
    }

    [PunRPC]
    void SendMess(string mess)
    {
        GameObject textobj = Instantiate(textPrefab, layoutContent);
        textobj.GetComponentInChildren<Text>().text = mess;
    }
}

using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBtn : MonoBehaviourPunCallbacks
{

    public string roomName;
    void Start()
    {


    }

    /// <summary>
    /// �����ť������÷���
    /// </summary>
    public void JoinRoomBtn()
    {


        PhotonNetwork.JoinRoom(roomName);//���뷿��
        NetworkManager._instance.RoomPanel.SetActive(true);//��ʾ�������
        NetworkManager._instance.LobbyPanel.SetActive(false);//���ش������


    }

}
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviourPunCallbacks
{

    public GameObject roomNamePrefab;//������ʾ��Ԥ����
    public Transform gridLayout;//Ԥ����ĸ�����

    /// <summary>
    /// ������µĺ�����ÿ�η�����ʧ�������ӣ��ͻ���е��ã�����Ҫ����ʧ��ʱ��������⴦��
    /// </summary>
    /// <param name="roomList">���صķ������</param>
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {

        Debug.Log("roomlist:" + roomList.Count);

        for (int i = 0; i < gridLayout.childCount; i++)//�����������µ�������
        {

            if (gridLayout.GetChild(i).gameObject.GetComponentInChildren<Text>().text == roomList[i].Name)//����ͬ������
            {

                Destroy(gridLayout.GetChild(i).gameObject);//����
                if (roomList[i].PlayerCount == 0)//����������Ϊ0
                {

                    roomList.Remove(roomList[i]);//�Ƴ��÷���
                }
            }
        }
        foreach (var room in roomList)//�������ɷ������ʾ
        {

            GameObject newRoom = Instantiate(roomNamePrefab, gridLayout.position, Quaternion.identity, gridLayout);//���ɷ��䰴ť
            newRoom.GetComponent<RoomBtn>().roomName = room.Name;//���÷�������
            newRoom.GetComponentInChildren<Text>().text = room.Name + "(" + room.PlayerCount + "/4)";//��ʾ����

        }
    }
}

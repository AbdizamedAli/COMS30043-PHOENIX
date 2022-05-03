using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject player;
    public static GameManager _insatnce;

    // Start is called before the first frame update

    void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "row1"), new Vector3(106.5f, 27.45f, 21.8f), Quaternion.identity);
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "row2"), new Vector3(106.5f, 27.45f, 21.6f), Quaternion.identity);
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "row3"), new Vector3(106.5f, 27.45f, 21.8f), Quaternion.identity);
                float x_1 = 53.85f;

                for (int i = 1; i < 4; i++)
                {
                    float y_1 = 7f;
                    x_1 += 7;

                    for (int j = 1; j < 4; j++)
                    {
                        y_1 -= 2;
                        if (i == 1)
                        {
                            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "text_wall_objects", "row_" + i + "_" + j), new Vector3(x_1, y_1, 43.71f), Quaternion.identity);
                        }
                        else if (i == 2)
                        {
                            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "text_wall_objects", "row_" + i + "_" + j), new Vector3(x_1, y_1, 43.71f), Quaternion.identity);
                        }
                        else
                        {
                            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "text_wall_objects", "row_" + i + "_" + j), new Vector3(x_1, y_1, 43.71f), Quaternion.identity);
                        }

                    }

                }

            }
        }
        _insatnce = this;

        int index = 0;
        Player[] players = PhotonNetwork.PlayerList;
        foreach (var item in players)
        {
            if (item.NickName == PhotonNetwork.NickName)
            {
                index = item.ActorNumber - 1;
                //  PhotonNetwork.Instantiate("Player", m_spawns.mySpawns[index].spawnPos.position, Quaternion.identity);
                GameObject p =  PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "MaleFreeSimpleMovement1"), new Vector3(0f, 2f, (float)(index * 10)), Quaternion.identity);
                p.transform.GetChild(1).gameObject.layer = 9 + index;
                p.GetComponentInChildren<Camera>().cullingMask = ~(1 << 9 + index);
                //Debug.Log(index + " Is the index");
            }
        }
    }
}

using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
public class GameManager : MonoBehaviourPunCallbacks
{


    public static GameManager _insatnce;
    List<Vector3> positionArray = new List<Vector3> { new Vector3(0, 2, 0), new Vector3(0, 2, 16) };




    // Start is called before the first frame update
    void Awake()
    {

        _insatnce = this;

        int index = 0;
        int i = 0;

        Player[] players = PhotonNetwork.PlayerList;
        foreach (var item in players)
        {

            if (item.NickName == PhotonNetwork.NickName)
            {

                index = item.ActorNumber - 1;
                //  PhotonNetwork.Instantiate("Player", m_spawns.mySpawns[index].spawnPos.position, Quaternion.identity);
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), positionArray.ElementAt(i), Quaternion.identity);
                Debug.Log("userid:" + item.ActorNumber);
                i++;
            }
        }
    }
}

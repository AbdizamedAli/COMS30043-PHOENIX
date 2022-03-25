using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room2 : MonoBehaviourPunCallbacks
{
    public GameObject player;
    public static room2 _insatnce;

    // Start is called before the first frame update
    void Awake()
    {
        _insatnce = this;
         PhotonNetwork.Instantiate(this.player.name, new Vector3(-2f, 4f, 45f), Quaternion.identity);

        
    }
}

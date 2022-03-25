using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room3 : MonoBehaviourPunCallbacks
{
    public GameObject player;
    public static room3 _insatnce;

    // Start is called before the first frame update
    void Awake()
    {
        _insatnce = this;
        PhotonNetwork.Instantiate(this.player.name, new Vector3(-27f, 5f, 83f), Quaternion.identity);


    }
}

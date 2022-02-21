using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class goin : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "room1in")
        {
            Destroy(gameObject);
            PhotonNetwork.LoadLevel(2);
        }
        if (other.tag == "room2in")
        {
            Destroy(gameObject);
            PhotonNetwork.LoadLevel(3);
        }
    }
}
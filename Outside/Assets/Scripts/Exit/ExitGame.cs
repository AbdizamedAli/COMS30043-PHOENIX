using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ExitGame : MonoBehaviourPunCallbacks
{
    private bool called = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !called)
        {
            PhotonNetwork.LoadLevel(3);
            called = true;
        }
    }
}

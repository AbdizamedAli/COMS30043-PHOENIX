using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TopDoor : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject door;
    private bool collision;
    void Update()
    {
        if (collision)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                this.photonView.RPC("setDoor", RpcTarget.All);
            }
        }

    }
    void OnTriggerEnter(Collider other)
    {
        collision = true;
    }
    void OnTriggerExit(Collider other)
    {
        collision = false;
    }
    [PunRPC]
    private void setDoor()
    {
        door.SetActive(true);
    }


}

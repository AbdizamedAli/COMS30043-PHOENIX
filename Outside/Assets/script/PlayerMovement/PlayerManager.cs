using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;


public class PlayerManager : MonoBehaviour
{
    PhotonView PV;



    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (PV.IsMine)
        {
            CreateController();
        }
    }
    private void CreateController()
    {
        Debug.Log("Instantiated player controller");
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"),gameObject.transform.position, Quaternion.identity);
        

    }
}

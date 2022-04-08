using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ResetBoxPositionSwitch :  MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject box;
    [SerializeField]

    private Transform boxSpawnPosition;
    private bool collision;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if (collision)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(collision){
                    this.photonView.RPC("ResetBox",RpcTarget.All,box,boxSpawnPosition);             
                }
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
    void ResetBox(GameObject box,Transform boxSpawnPosition){
        box.transform.position=boxSpawnPosition.transform.position;
    }

}

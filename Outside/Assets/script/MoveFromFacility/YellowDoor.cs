using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class YellowDoor : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject spawn_1;
    [SerializeField] private GameObject spawn_2;
    bool spawn1 = true;
    bool spawn2 = true;

    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (spawn1)
        {
            other.transform.position = spawn_1.transform.position;
            this.photonView.RPC("setSpawn", RpcTarget.All, 0);
    


        }
        else if (spawn2 && !spawn1)
        {
            other.transform.position = spawn_2.transform.position;
            this.photonView.RPC("setSpawn", RpcTarget.All, 1);


        }

    }

    [PunRPC]
    void setSpawn(int i)
    {
        if (i == 0)
        {
            spawn1 = false;
        }
        else
        {
            spawn2 = false;
        }
    }

}

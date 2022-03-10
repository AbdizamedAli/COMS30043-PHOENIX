using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RedDoor : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject spawn_1;
    [SerializeField] private GameObject spawn_2;
    private bool spawn1 = true;
    private bool spawn2 = true;

    public bool isDone = false;
    
    // Start is called before the first frame update
    void Start()
    {
      

    }

    // Update is called once per frame
    void Update()
    {
        if (isDone)
        {
            Debug.Log("Done");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isDone)
        {


            if (spawn1)
            {
                this.photonView.RPC("setSpawn", RpcTarget.All, 0);
                other.transform.position = spawn_1.transform.position;
                spawn1 = false;

            }
            else if (spawn2)
            {
                other.transform.position = spawn_2.transform.position;
                spawn2 = false;
                this.photonView.RPC("setSpawn", RpcTarget.All, 1);

            }
            else
            {
                Debug.Log("Full");
            }
        }
    }

    [PunRPC]
    private void setSpawn(int s)
    {
        if (s == 0)
        {
            spawn1 = false;
        }
        else
        {
            spawn2 = false;
        }
    }
}

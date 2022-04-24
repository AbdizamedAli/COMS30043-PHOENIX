using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OrangeDoorFloor1 : MonoBehaviourPunCallbacks
{
    [SerializeField]

    private GameObject spawn_1;

    [SerializeField]
    private GameObject spawn_2;

    public bool isDone = false;
    public int playerCount;

    private bool leftOrRight;// left false right true

    // Start is called before the first frame update
    void Start()
    {
        leftOrRight = false;
        playerCount = 0;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        other.transform.position = spawn_1.transform.position;


        if (playerCount < 2)
        {
            if (leftOrRight == false)
            {
                this.photonView.RPC("FlipLeftOrRight", RpcTarget.All);
                this.photonView.RPC("RaisePlayerCount", RpcTarget.All);
                other.transform.position = spawn_1.transform.position;
            }
            else if (leftOrRight == true)
            {
                this.photonView.RPC("FlipLeftOrRight", RpcTarget.All);
                this.photonView.RPC("RaisePlayerCount", RpcTarget.All);
                other.transform.position = spawn_2.transform.position;
            }
        }
        

    }
    [PunRPC]
    void FlipLeftOrRight()
    {
        if (leftOrRight == false)
        {
            leftOrRight = true;
        }
        else if (leftOrRight == true)
        {
            leftOrRight = false;
        }

    }

    [PunRPC]
    void RaisePlayerCount()
    {
        playerCount = playerCount + 1;
    }

}
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
    public int playerCount=0;

    private string leftOrRight;// left false right true

    // Start is called before the first frame update
    void Start()
    {
        leftOrRight = "false";
        playerCount = 0;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)

    {
        

        


        if (playerCount < 2)
        {
            
            if (leftOrRight.Equals("false"))
            {
                
                this.photonView.RPC("FlipLeftOrRight", RpcTarget.All,"true");
                print(leftOrRight);
                
                this.photonView.RPC("RaisePlayerCount", RpcTarget.All);
               
                other.transform.position = spawn_1.transform.position;
            }
            else if (leftOrRight.Equals("true"))
            {
                this.photonView.RPC("FlipLeftOrRight", RpcTarget.All,"false");
                this.photonView.RPC("RaisePlayerCount", RpcTarget.All);
                other.transform.position = spawn_2.transform.position;
            }
        }
        

    }
    [PunRPC]
    void FlipLeftOrRight(string flip)
    {
        leftOrRight=flip;
        

    }

    [PunRPC]
    void RaisePlayerCount()
    {
        playerCount = playerCount + 1;
    }

}
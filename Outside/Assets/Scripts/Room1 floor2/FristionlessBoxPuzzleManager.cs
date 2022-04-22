using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FristionlessBoxPuzzleManager : MonoBehaviourPunCallbacks
{
    private bool roomComplete;
    [SerializeField]
    private GameObject doorOne;
    [SerializeField]
    private GameObject doorTwo;
    [SerializeField]
    private Transform doorOneSpawn;
    [SerializeField]

    private Transform doorTwoSpawn;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RoomComplete(){
        print("win correct");
        //CompleteRoom();//
        this.photonView.RPC("CompleteRoom",RpcTarget.All);
    }
    [PunRPC]
    void CompleteRoom(){
        print("complete");
        //SpawnExitDoors();//
        this.photonView.RPC("SpawnExitDoors",RpcTarget.All);

    }

    [PunRPC]
    void SpawnExitDoors(){
        doorOne.transform.position=doorOneSpawn.transform.position;
        doorTwo.transform.position=doorTwoSpawn.transform.position;

    }
    
}

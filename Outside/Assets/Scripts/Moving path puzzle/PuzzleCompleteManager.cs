using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PuzzleCompleteManager : MonoBehaviourPunCallbacks
{
    private FloorManagerThree FloorManagerThree;
    [SerializeField] 
    GameObject leftExitDoor;
    [SerializeField] 
    GameObject rightExitDoor;
    [SerializeField]
    GameObject leftExitDoorSpawn;
    [SerializeField]
    GameObject rightExitdoorSpawn;
    void Awake(){
        FloorManagerThree=GameObject.FindObjectOfType<FloorManagerThree>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CompleteRoom(){
        this.photonView.RPC("SpawnExitDoors",RpcTarget.All);

    }
    [PunRPC]
    void SpawnExitDoors(){
        FloorManagerThree.PuzzleComplete();
        leftExitDoor.transform.position=leftExitDoorSpawn.transform.position;
        rightExitDoor.transform.position=rightExitdoorSpawn.transform.position;


    }
}

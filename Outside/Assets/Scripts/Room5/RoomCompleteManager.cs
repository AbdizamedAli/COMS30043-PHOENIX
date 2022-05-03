using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomCompleteManager : MonoBehaviourPunCallbacks

{
    [SerializeField]
    public GameObject leftExitDoor;
    [SerializeField]
    public GameObject leftExitDoorPosition;
    [SerializeField]
    public GameObject rightExitDoor;
    [SerializeField]
    public GameObject rightExitDoorPosition;



    private bool leftCompleted;
    private bool rightCompleted;

    private FloorManagerOne FloorManagerOne;

    void Awake(){
        FloorManagerOne=GameObject.FindObjectOfType<FloorManagerOne>();
    }
    // Start is called before the first frame update
    void Start()
    {
        leftCompleted=false;
        rightCompleted=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(leftCompleted==true && rightCompleted==true){
            //add code to spawn exit doors here
            print("room completed");
            this.photonView.RPC("SpawnExitDoors",RpcTarget.All);
            FloorManagerOne.PuzzleComplete();
        }
    }
    public void LeftComplete(){
        this.photonView.RPC("CompleteLeft",RpcTarget.All);
    }
    public void RightComplete(){
        this.photonView.RPC("CompleteRight",RpcTarget.All);
    }

    [PunRPC]
    public void SpawnExitDoors(){
        leftExitDoor.transform.position=leftExitDoorPosition.transform.position;
        rightExitDoor.transform.position=rightExitDoorPosition.transform.position;

    }
    [PunRPC]
    private void CompleteLeft(){
        leftCompleted=true;
    }
    [PunRPC]
    private void CompleteRight(){
        rightCompleted=true;
    }
}

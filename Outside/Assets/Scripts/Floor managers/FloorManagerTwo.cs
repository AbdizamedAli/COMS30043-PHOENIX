using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FloorManagerTwo : MonoBehaviourPunCallbacks
{
    [SerializeField]
    int numOfPuzzles;
    bool complete;

    [SerializeField]
    GameObject stairWall;
    [SerializeField]
    Transform stairWallNewPos;
    [SerializeField]
    private AudioSource AiSound;
    [SerializeField]
    private AudioSource AiSoundSecond;
    bool onePuzzleCompleted;
    [SerializeField]
    GameObject redDoor;
    // Start is called before the first frame update
    void Start()
    {
        complete=false;
        onePuzzleCompleted=false;
    }

    // Update is called once per frame
    void Update()
    {
        if (numOfPuzzles==0&&complete==false){
            //CompleteRoom();//
            this.photonView.RPC("CompleteRoom",RpcTarget.All);
            

        }
    }



    [PunRPC]
    void CompleteRoom(){
        //AiSoundSecond.Play();
        complete=true;
        stairWall.transform.position=stairWallNewPos.transform.position;
        redDoor.transform.position=stairWallNewPos.transform.position;
        print("yessss");
    }

    public void PuzzleComplete(){
        if(PhotonNetwork.IsMasterClient){
            this.photonView.RPC("DecreasePuzzleCount",RpcTarget.All);
            

        }
    }

    [PunRPC]
    void DecreasePuzzleCount(){
        if (numOfPuzzles==0){
            numOfPuzzles=0;
        }
        else{
            numOfPuzzles=numOfPuzzles-1;
        }
        if(onePuzzleCompleted==false){
            //AiSound.Play();
            onePuzzleCompleted=true;
        }
        
    }

}


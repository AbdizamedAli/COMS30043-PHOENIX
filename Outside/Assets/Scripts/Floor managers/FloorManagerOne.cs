using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FloorManagerOne : MonoBehaviourPunCallbacks
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
    // Start is called before the first frame update
    void Start()
    {
        complete=false;
    }

    // Update is called once per frame
    void Update()
    {
        if (numOfPuzzles==0&&complete==false){
            //CompleteRoom();//
            this.photonView.RPC("CompleteRoom",RpcTarget.All);
            print("yessss");

        }
    }



    [PunRPC]
    void CompleteRoom(){
        AiSound.Play();
        complete =true;
        stairWall.transform.position=stairWallNewPos.transform.position;
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
        
    }

}

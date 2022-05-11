using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FloorManagerThree : MonoBehaviourPunCallbacks
{
    [SerializeField]
    int numOfPuzzles;
    bool complete;

    [SerializeField]
    GameObject exitWall;
    [SerializeField]
    Transform exitWallNewPos;
    // Start is called before the first frame update
    void Start()
    {
        complete=false;
    }

    // Update is called once per frame
    void Update()
    {
        if (numOfPuzzles==0&&complete==false){
            
            this.photonView.RPC("CompleteRoom",RpcTarget.All);
            

        }
    }



    [PunRPC]
    void CompleteRoom(){
        complete=true;
        
        print("iuiuiuiu");
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


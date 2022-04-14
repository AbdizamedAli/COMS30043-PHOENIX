using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Photon.Pun;
public class addforce : MonoBehaviourPunCallbacks
{

    public Rigidbody box;
    private string pushDirection;

    private bool collided;

    private string movingDirection;
    private bool moving;

    private FristionlessBoxPuzzleManager FristionlessBoxPuzzleManager;
    
    //public GameObject barrier;

    void Awake(){
        collided=true;
        moving=false;
        FristionlessBoxPuzzleManager =GameObject.FindObjectOfType<FristionlessBoxPuzzleManager>();
    }
    // Start is called before the first frame update

    void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        if(pushDirection=="Forward"){
            //PushForward(box);//
            this.photonView.RPC("PushForward",RpcTarget.All,box);
        }
        if(pushDirection=="Right"){
            //PushRight(box);//
            this.photonView.RPC("PushRight",RpcTarget.All,box);
        }
        if(pushDirection=="Down"){
            //PushDown(box);//
            this.photonView.RPC("PushDown",RpcTarget.All,box);
        }
        if(pushDirection=="Left"){
            //PushLeft(box);//
            this.photonView.RPC("PushLeft",RpcTarget.All,box);
        }
    }

    public void Push(string Direction)
    {
        
        //SetPushDirection(Direction);//
        this.photonView.RPC("SetPushDirection",RpcTarget.All,Direction);
        
        
    }


    [PunRPC]
    void PushForward(Rigidbody box){
        box.velocity= new Vector3(0,0,-10);
                 
    }
    [PunRPC]
    void PushRight(Rigidbody box){
        
        box.velocity= new Vector3(-10,0,0);
        
     
    }
    [PunRPC]
    void PushDown(Rigidbody box){
        
        box.velocity= new Vector3(0,0,10);
    }
    [PunRPC]
    void PushLeft(Rigidbody box){  
        box.velocity= new Vector3(10,0,0);
    }
    IEnumerator Wait(int seconds){
        yield return new WaitForSeconds(seconds);
    }
    [PunRPC]
    void SetPushDirection(string Direction)
    {
        if(moving==false&&Direction!="nil"){
            //FlipMoving(true);//
            this.photonView.RPC("FlipMoving",RpcTarget.All,true);
            pushDirection=Direction;
            print(pushDirection);
        }
        else if(moving==false&&Direction=="nil"){
            pushDirection="nil";
            print(pushDirection);
        }
    }


    void OnTriggerEnter(Collider other){
        if(other.name=="collisionbox"){
            //SetVelocityZero();//
            this.photonView.RPC("SetVelocityZero",RpcTarget.All);
            //FlipCollided(true);//
            this.photonView.RPC("FlipCollided",RpcTarget.All,true);
            //FlipMoving(false);//
            this.photonView.RPC("FlipMoving",RpcTarget.All,false);
            print(other.name);

        }
        if(other.name=="Win"){
            //RoomCompleted();//
            this.photonView.RPC("RoomCompleted",RpcTarget.All);
        }
    }
    void OnTriggerExit(Collider other){
        if(other.name=="collisionbox"){
            //FlipCollided(false);//
            this.photonView.RPC("FlipCollided",RpcTarget.All,false);
        }

    }
    [PunRPC]
    void FlipCollided(bool trueOrFaslse){
        if(trueOrFaslse==true){
            collided=true;
        }
        else if(trueOrFaslse==false){
            collided=false;
        }
    }
    [PunRPC]
    void FlipMoving(bool trueOrFalse){
        if(trueOrFalse==true){
            moving=true;
        }
        if(trueOrFalse==false){
            moving=false;
        }
    }
    [PunRPC]
    void SetVelocityZero(){
        box.velocity=Vector3.zero;
    }
    public void Reset(){
        //ResetBoxPos();//
        this.photonView.RPC("ResetBoxPos",RpcTarget.All);
    }
    [PunRPC]
    void ResetBoxPos(){
        //SetPushDirection("nil");//
        this.photonView.RPC("SetPushDirection",RpcTarget.All,"nil");
        //no direction
    }
    void RoomCompleted(){
        //SetVelocityZero();//
        this.photonView.RPC("SetVelocityZero",RpcTarget.All);
        //FlipMoving(true);//
        this.photonView.RPC("FlipMoving",RpcTarget.All,true);
        FristionlessBoxPuzzleManager.RoomComplete();
        
    }
}

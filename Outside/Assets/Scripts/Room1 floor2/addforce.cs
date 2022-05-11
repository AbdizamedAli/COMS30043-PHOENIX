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
    
   

    void Awake(){
        collided=true;
        moving=false;
        FristionlessBoxPuzzleManager =GameObject.FindObjectOfType<FristionlessBoxPuzzleManager>();
    }
    // Start is called before the first frame update

    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {

        if(pushDirection=="Forward"){
            
            this.photonView.RPC("PushForward",RpcTarget.All);
        }
        if(pushDirection=="Right"){
            
            this.photonView.RPC("PushRight",RpcTarget.All);
        }
        if(pushDirection=="Down"){
            
            this.photonView.RPC("PushDown",RpcTarget.All);
        }
        if(pushDirection=="Left"){
            
            this.photonView.RPC("PushLeft",RpcTarget.All);
        }
    }

    public void Push(string Direction)
    {
        
        
        this.photonView.RPC("SetPushDirection",RpcTarget.All,Direction);
        
        
    }


    [PunRPC]
    void PushForward(){
        box.velocity= new Vector3(0,0,-5);
                 
    }
    [PunRPC]
    void PushRight(){
        
        box.velocity= new Vector3(-5,0,0);
        
     
    }
    [PunRPC]
    void PushDown(){
        
        box.velocity= new Vector3(0,0,5);
    }
    [PunRPC]
    void PushLeft(){  
        box.velocity= new Vector3(5,0,0);
    }
    IEnumerator Wait(int seconds){
        yield return new WaitForSeconds(seconds);
    }
    [PunRPC]
    void SetPushDirection(string Direction)
    {
        if(moving==false&&Direction!="nil"){
            
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
            
            this.photonView.RPC("SetVelocityZero",RpcTarget.All);
            
            this.photonView.RPC("FlipCollided",RpcTarget.All,true);
            
            this.photonView.RPC("FlipMoving",RpcTarget.All,false);
            print(other.name);

        }
        if(other.name=="Win"){
            
            this.photonView.RPC("RoomCompleted",RpcTarget.All);
        }
    }
    void OnTriggerExit(Collider other){
        if(other.name=="collisionbox"){
            
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
        
        this.photonView.RPC("ResetBoxPos",RpcTarget.All);
    }
    [PunRPC]
    void ResetBoxPos(){
        
        this.photonView.RPC("SetPushDirection",RpcTarget.All,"nil");
        
    }
    [PunRPC]
    void RoomCompleted(){
        
        this.photonView.RPC("SetVelocityZero",RpcTarget.All);
        
        this.photonView.RPC("FlipMoving",RpcTarget.All,true);
        FristionlessBoxPuzzleManager.RoomComplete();
        
    }
}

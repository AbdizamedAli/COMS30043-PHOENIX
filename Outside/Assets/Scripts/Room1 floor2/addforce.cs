using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
public class addforce : MonoBehaviour
{

    public Rigidbody box;
    private string pushDirection;

    private bool collided;

    private string movingDirection;
    private bool moving;
    
    //public GameObject barrier;

    void Awake(){
        collided=true;
        moving=false;
    }
    // Start is called before the first frame update

    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {

        if(pushDirection=="Forward"){
            PushForward(box);//
        }
        if(pushDirection=="Right"){
            PushRight(box);//
        }
        if(pushDirection=="Down"){
            PushDown(box);//
        }
        if(pushDirection=="Left"){
            PushLeft(box);//
        }
    }

    public void Push(string Direction)
    {
        
        SetPushDirection(Direction);
        
        
    }



    void PushForward(Rigidbody box){
        box.velocity= new Vector3(0,0,-10);
                 
    }
    void PushRight(Rigidbody box){
        
        box.velocity= new Vector3(-10,0,0);
        
     
    }
    void PushDown(Rigidbody box){
        
        box.velocity= new Vector3(0,0,10);
    }
    void PushLeft(Rigidbody box){  
        box.velocity= new Vector3(10,0,0);
    }
    IEnumerator Wait(int seconds){
        yield return new WaitForSeconds(seconds);
    }
    void SetPushDirection(string Direction)
    {
        if(moving==false&&Direction!="nil"){
            FlipMoving(true);//
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
            SetVelocityZero();//
            FlipCollided(true);//
            FlipMoving(false);//
            print(other.name);

        }
    }
    void OnTriggerExit(Collider other){
        if(other.name=="collisionbox"){
            FlipCollided(false);//
        }

    }
    void FlipCollided(bool trueOrFaslse){
        if(trueOrFaslse==true){
            collided=true;
        }
        else if(trueOrFaslse==false){
            collided=false;
        }
    }
    void FlipMoving(bool trueOrFalse){
        if(trueOrFalse==true){
            moving=true;
        }
        if(trueOrFalse==false){
            moving=false;
        }
    }
    void SetVelocityZero(){
        box.velocity=Vector3.zero;
    }
    public void Reset(){
        ResetBoxPos();//
    }

    void ResetBoxPos(){
        SetPushDirection("nil");//
        //no direction
    }
}

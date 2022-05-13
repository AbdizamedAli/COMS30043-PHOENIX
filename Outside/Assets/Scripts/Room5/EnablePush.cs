using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class EnablePush : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private float sizeOfForce;

    

    // Start is called before the first frame update
    void Start()
    {
        
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hitObject){
        if(hitObject!=null){
            Rigidbody rigidbody;
            rigidbody=hitObject.collider.attachedRigidbody; 
            push(hitObject, rigidbody);
            
            

        }
            
            
    }
        
    
    
    private void push(ControllerColliderHit hitObject,Rigidbody rigidbody){
        
        if(rigidbody != null){
            
            Vector3 forceVector = hitObject.gameObject.transform.position-transform.position;
            forceVector.y=0;
            forceVector.Normalize();

            rigidbody.AddForceAtPosition(forceVector*sizeOfForce,transform.position,ForceMode.Impulse);
        }

    }    
}

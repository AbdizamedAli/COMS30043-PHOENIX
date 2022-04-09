using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class EnablePush : MonoBehaviourPunCallbacks
{
    
    private float sizeOfForce;

    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        sizeOfForce=1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hitObject){
        if(hitObject!=null){
            Rigidbody rigidbody=hitObject.collider.attachedRigidbody;
            this.photonView.RPC("push",RpcTarget.All,hitObject,rigidbody);
        }
        
    }
    [PunRPC]
    private void push(ControllerColliderHit hitObject,Rigidbody rigidbody){
        if(rigidbody != null){
            Vector3 forceVector = hitObject.gameObject.transform.position-transform.position;
            forceVector.y=0;
            forceVector.Normalize();

            rigidbody.AddForceAtPosition(forceVector*sizeOfForce,transform.position,ForceMode.Impulse);
        }

    }    
}

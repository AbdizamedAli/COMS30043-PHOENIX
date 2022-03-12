using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePush : MonoBehaviour
{
    
    private float sizeOfForce;

    // Start is called before the first frame update
    void Start()
    {
        sizeOfForce=1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hitObject){
        Rigidbody rigidbody=hitObject.collider.attachedRigidbody;

        if(rigidbody != null){
            Vector3 forceVector = hitObject.gameObject.transform.position-transform.position;
            forceVector.y=0;
            forceVector.Normalize();

            rigidbody.AddForceAtPosition(forceVector*sizeOfForce,transform.position,ForceMode.Impulse);
        }

    }
}

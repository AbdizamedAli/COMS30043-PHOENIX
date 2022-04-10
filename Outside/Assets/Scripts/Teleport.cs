using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform teleportLocation;
    
    void OnTriggerEnter(Collider other){
        other.transform.position=teleportLocation.transform.position;
        print("collided");
    }
}

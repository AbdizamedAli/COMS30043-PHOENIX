using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform teleportLocation;
    public GameObject Player;
    
    void OnTriggerEnter(Collider other){
        Player.transform.position=teleportLocation.transform.position;
    }
}

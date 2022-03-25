using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDoorExit : MonoBehaviour
{
    [SerializeField] private GameObject spawn_1;

    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = spawn_1.transform.position;
    }
}

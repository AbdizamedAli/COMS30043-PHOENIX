using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class YellowDoorExit : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject spawn_1;
    private int count = 0;

    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = spawn_1.transform.position;
    }

    private void Update()
    {
        if (count == 2)
        {
            gameObject.SetActive(false);
        }
    }

}

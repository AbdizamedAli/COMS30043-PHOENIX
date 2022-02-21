using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class YellowDoor : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject spawn_1;
    [SerializeField] private GameObject spawn_2;
    private bool spawn1 = true;
    private bool spawn2 = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (spawn1)
        {
            other.transform.position = spawn_1.transform.position;
            spawn1 = false;

        }
        else if (spawn2)
        {
            other.transform.position = spawn_2.transform.position;
            spawn2 = false;
        }
        else
        {
            Debug.Log("Full");
        }
    }

}

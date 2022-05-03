using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRotate : MonoBehaviour
{
    private bool collision = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (collision)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        collision = true;
    }

    void OnTriggerExit(Collider other)
    {
        collision = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace keySystem
{
    public class keyRayCast : MonoBehaviour
    {
    public GameObject key_object;
    public bool action = false;
    public bool open = false;
    private keyitemController key_to_get;
    public GameObject instruction;
    
    void Update() 
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (action && !open)
            {
                key_object.SetActive(false);
                key_to_get.objectInteraction();
                action = false;
                open = true;
                instruction.SetActive(false);

            }
        }
    }


    void OnTriggerEnter(Collider collision) 
    {
        if (collision.transform.tag == "Player" && !open)
        {
            action = true;
            key_to_get = key_object.GetComponent<keyitemController>();
            instruction.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other) 
    {
        instruction.SetActive(false);

        action = false;    
    }



    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torchTrigger : MonoBehaviour
{
    private bool collision=false;
    private bool pressed=false;
    public Material TorchOn;
    public Material TorchOff;
    public GameObject Torch;

    void Update()
    {
        if (collision)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                
                
                
                if(pressed)
                {
                    
                    
                    
                    Torch.GetComponent<Renderer>().material = TorchOff;
                    pressed = false;
                }
                
                else if(!pressed)
                {
                    
                    
                    Torch.GetComponent<Renderer>().material = TorchOn;
                    
                    pressed = true;
                }
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


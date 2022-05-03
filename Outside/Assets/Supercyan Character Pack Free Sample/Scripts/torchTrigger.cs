using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class torchTrigger : MonoBehaviourPunCallbacks
{
    private bool collision=false;
    private bool pressed=false;
    public Material TorchOn;
    public Material TorchOff;
    public GameObject Torch;
    public GameObject TorchAbove;

    void Update()
    {
        if (collision)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                
                
                
                if(pressed)
                {
                    
                    
                    
                    Torch.GetComponent<Renderer>().material = TorchOff;
                    this.photonView.RPC("FlipTopTorch",RpcTarget.All,"on");
                    pressed = false;
                }
                
                else if(!pressed)
                {
                    
                    
                    Torch.GetComponent<Renderer>().material = TorchOn;
                    this.photonView.RPC("FlipTopTorch",RpcTarget.All,"off");
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

    [PunRPC]
    void FlipTopTorch(string state){
        if (state=="on"){
            TorchAbove.GetComponent<Renderer>().material = TorchOn;
        
        }
        else if(state =="off"){
            TorchAbove.GetComponent<Renderer>().material = TorchOff;
        }
    }
}


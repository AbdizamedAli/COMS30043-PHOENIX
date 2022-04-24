using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;



public class wallDoorButtonPress : MonoBehaviourPunCallbacks
{
    
    public GameObject Player;
    public GameObject WallDoor;
    public GameObject Button;
    public Material ButtonOn;
    public Material ButtonOff;
    public Material WallOn;
    public Material WallOff;
    private bool collision=false;
    private bool pressed=false;
    [SerializeField] private GameObject enter = null;
    [SerializeField] private bool final = false;

    void Update()
    {
        if (collision)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                print("e pressed");
 

                //if (Button.GetComponent<Renderer>().sharedMaterial.Equals(ButtonOn))
                if (pressed)
                {
                    
                    print("button on yes");

                    //Button.GetComponent<Renderer>().material = ButtonOff;
                    //WallDoor.GetComponent<Renderer>().material = WallOn;
                    //WallDoor.GetComponent<BoxCollider>().enabled = true;
                    ;
                    this.photonView.RPC("setWallOn", RpcTarget.All);
                    pressed = false;
                }
                //if (Button.GetComponent<Renderer>().sharedMaterial.Equals(ButtonOff))
                else if(!pressed)
                {
                    print("button off yes");
                    this.photonView.RPC("setWallOff", RpcTarget.All);
                    //Button.GetComponent<Renderer>().material = ButtonOn;
                    //WallDoor.GetComponent<Renderer>().material = WallOff;
                    //WallDoor.GetComponent<BoxCollider>().enabled = false;
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
    private void setWallOn()
    {
        Button.GetComponent<Renderer>().material = ButtonOff;
        WallDoor.GetComponent<Renderer>().material = WallOn;
        WallDoor.GetComponent<BoxCollider>().enabled = true;

    }
    [PunRPC]
    private void setWallOff()
    {
        Button.GetComponent<Renderer>().material = ButtonOn;
        WallDoor.GetComponent<Renderer>().material = WallOff;
        WallDoor.GetComponent<BoxCollider>().enabled = false;
        if (final && enter != null)
        {
            enter.GetComponent<RedDoor>().isDone = true;
        }



    }
}




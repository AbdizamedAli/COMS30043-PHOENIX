using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;



public class SelectionButton : MonoBehaviourPunCallbacks
{
    
    public GameObject Player;
    
    public GameObject Button;

    public GameObject Door;
    
    public Material ButtonOn;
    public Material ButtonOff;



    
    
    private bool collision=false;
    private bool pressed=false;
    [SerializeField] private GameObject enter = null;
    [SerializeField] private bool final = false;



    private DoorSelectionManager DoorSelectionManager;
    void Awake()
    {
        DoorSelectionManager=GameObject.FindObjectOfType<DoorSelectionManager>();
    }
    

    void Update()
    {
        if (collision)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //print("e pressed");
 

                //if (Button.GetComponent<Renderer>().sharedMaterial.Equals(ButtonOn))
                if (pressed)
                {
                    
                    //print("button on yes");
                    //Button.GetComponent<Renderer>().material = ButtonOff;
                    ButtonTurnOff();
                    DoorSelectionManager.DecreasePlayerCount(Door.name);
                    //WallDoor.GetComponent<Renderer>().material = WallOn;
                    //WallDoor.GetComponent<BoxCollider>().enabled = true;
                    pressed = false;
                }
                //if (Button.GetComponent<Renderer>().sharedMaterial.Equals(ButtonOff))
                else if(!pressed)
                {
                    //print("button off yes");
                    ButtonTurnOn();
                    DoorSelectionManager.IncreasePlayerCount(Door.name);
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
    private void ButtonTurnOn(){
        Button.GetComponent<Renderer>().material = ButtonOn;

    }
    [PunRPC]
    private void ButtonTurnOff(){
        Button.GetComponent<Renderer>().material = ButtonOff;

    }




}




using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class YellowButtonPress : MonoBehaviourPunCallbacks
{
    public GameObject slammingDoor;
    public GameObject yellowDoor;
    public Transform yellowDoorLocation;
    public Transform slammingDoorLocation;

    public GameObject Button;
    public Material ButtonOn;
    public Material ButtonOff;

    public GameObject PlayerDetection;
    //public GameObject DoorSelectionManager;

    private bool collision = false;
    private bool pressed = false;

    private int playerCount;

    //private YellowPlayerDetection yellowPlayerDetection;
    // Start is called before the first frame update
    void Start()
    {
        GameObject detectionCube = GameObject.Find("YellowPlayerDetectionCube");
        YellowPlayerDetection yellowPlayerDetection = detectionCube.GetComponent<YellowPlayerDetection>();
        playerCount = yellowPlayerDetection.YellowDoorCount;
        Debug.Log(playerCount);
    }

    // Update is called once per frame
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
                    ButtonTurnOff();
                    pressed = false;

                }
                //if (Button.GetComponent<Renderer>().sharedMaterial.Equals(ButtonOff))
                else if (!pressed && GameObject.Find("YellowPlayerDetectionCube").GetComponent<YellowPlayerDetection>().YellowDoorCount >= 1)
                {
                    //print("button off yes");
                    Debug.Log(playerCount);
                    Debug.Log(GameObject.Find("YellowPlayerDetectionCube").GetComponent<YellowPlayerDetection>().YellowDoorCount);
                    ButtonTurnOn();
                    pressed = true;
                    yellowDoor.transform.position = yellowDoorLocation.transform.position;
                    slammingDoor.transform.position = slammingDoorLocation.transform.position;
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
    private void ButtonTurnOn()
    {
        Button.GetComponent<Renderer>().material = ButtonOn;
    }

    [PunRPC]
    private void ButtonTurnOff()
    {
        Button.GetComponent<Renderer>().material = ButtonOff;
    }
}


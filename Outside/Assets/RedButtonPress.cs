using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RedButtonPress : MonoBehaviourPunCallbacks
{
    public GameObject slammingDoor;
    public GameObject redDoor;
    public Transform redDoorLocation;
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
        GameObject detectionCube = GameObject.Find("RedPlayerDetectionCube");
        RedPlayerDetection redPlayerDetection = detectionCube.GetComponent<RedPlayerDetection>();
        playerCount = redPlayerDetection.RedDoorCount;
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
                    this.photonView.RPC(nameof(ButtonTurnOff), RpcTarget.All);
                    pressed = false;

                }
                //if (Button.GetComponent<Renderer>().sharedMaterial.Equals(ButtonOff))
                else if (!pressed && GameObject.Find("RedPlayerDetectionCube").GetComponent<RedPlayerDetection>().RedDoorCount >= 1)
                {
                    //print("button off yes");
                    Debug.Log(playerCount);
                    Debug.Log(GameObject.Find("RedPlayerDetectionCube").GetComponent<RedPlayerDetection>().RedDoorCount);
                    this.photonView.RPC(nameof(ButtonTurnOn), RpcTarget.All);
                    pressed = true;
                    this.photonView.RPC(nameof(redDoorSetOpenDoor), RpcTarget.All);
                    //redDoor.transform.position = redDoorLocation.transform.position;
                    //slammingDoor.transform.position = slammingDoorLocation.transform.position;
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
    void redDoorSetOpenDoor()
    {
        redDoor.transform.position = redDoorLocation.transform.position;
        slammingDoor.transform.position = slammingDoorLocation.transform.position;
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

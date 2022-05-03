using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TopDoor : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject door;
    [SerializeField] private Material buttonOn;
    
    [SerializeField] private GameObject button;

    private bool pressed;

    private bool collision;

    private FloorManagerOne FloorManagerOne;

    void Awake(){
        FloorManagerOne=GameObject.FindObjectOfType<FloorManagerOne>();
        pressed=false;
    }
    void Update()
    {
        if (collision)
        {
            if (Input.GetKeyDown(KeyCode.E)&&pressed==false)
            {
                pressed=true;
                this.photonView.RPC("setDoor", RpcTarget.All);
                this.photonView.RPC("ButtonOn",RpcTarget.All);
                FloorManagerOne.PuzzleComplete();
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
    private void setDoor()
    {
        door.SetActive(true);
    }
    [PunRPC]
    private void ButtonOn(){
        button.GetComponent<Renderer>().material=buttonOn;

    }


}

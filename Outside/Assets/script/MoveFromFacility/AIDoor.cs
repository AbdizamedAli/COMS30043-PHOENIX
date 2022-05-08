using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AIDoor : MonoBehaviour
{
    [SerializeField] private GameObject spawn_1;
    [SerializeField] private GameObject spawn_2;
    private bool spawn1 = true;
    private bool spawn2 = true;
    public bool isDone = false;
    [SerializeField] CursorControl cursorControl;
    [SerializeField] GameObject canvasUI;
    //[SerializeField] NetworkManager networkManager;
    private bool inRoom = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //canvas.worldCamera = networkManager.singleton.LocalClient.PlayerObject.Camera;
        if (inRoom)
        {
            
            
            //cursorControl.GetComponent<bool>() = true;
            //canvas.worldCamera = other.GetComponent<Camera>();
            cursorControl.showCursor = true;
            Debug.Log(cursorControl.showCursor);
            //canvas.worldCamera = PhotonNetwork.LocalPlayer.gameObject.Camera;
            //canvas.worldCamera = 
           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isDone)
        {
            //other.transform.position = spawn_1.transform.position;
            //canvas.worldCamera = other.GetComponent<Camera>();
            if (spawn1)
            {
                this.GetComponent<PhotonView>().RPC("setSpawn", RpcTarget.All, 0);
                other.transform.position = spawn_1.transform.position;
                spawn1 = false;

            }
            else if (spawn2)
            {
                other.transform.position = spawn_2.transform.position;
                spawn2 = false;
                this.GetComponent<PhotonView>().RPC("setSpawn", RpcTarget.All, 1);

            }
            else
            {
                Debug.Log("Full");
            }
            inRoom = true;
        }
        var canvas = canvasUI.GetComponent<Canvas>();
        var player = other.gameObject;
        var camera = player.GetComponentInChildren<Camera>();
        canvas.worldCamera = camera;
    }

    [PunRPC]
    private void setSpawn(int s)
    {
        if (s == 0)
        {
            spawn1 = false;
        }
        else
        {
            spawn2 = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ResetBox :  MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject box;
    [SerializeField]

    private Transform boxSpawnPosition;

    private bool collision;

    [SerializeField]
    private Rigidbody boxRigidBody;

    private addforce addforce;
    void Awake(){
        addforce=GameObject.FindObjectOfType<addforce>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
         if (collision)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(collision){
                    this.photonView.RPC("ResetBox",RpcTarget.All,box,boxSpawnPosition);       
                    //ResetBoxPos(box,boxSpawnPosition);      
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
    void ResetBoxPos(GameObject box,Transform boxSpawnPosition){
        boxRigidBody.velocity=Vector3.zero;
        addforce.Reset();
        box.transform.position=boxSpawnPosition.transform.position;
        
    }
}

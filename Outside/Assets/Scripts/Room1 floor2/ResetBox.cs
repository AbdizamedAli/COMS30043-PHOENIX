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
       
    }

    // Update is called once per frame
    void Update()
    {
         if (collision)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(collision){
                    this.photonView.RPC(nameof(WhyDoesThisBreak),RpcTarget.All);
                        
                }
            }
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            collision = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {

            collision = false;
        }
    }
    [PunRPC]
    private void WhyDoesThisBreak()
    {
        boxRigidBody.velocity = Vector3.zero;
        addforce.Reset();
        box.transform.position = boxSpawnPosition.transform.position;

    }
}

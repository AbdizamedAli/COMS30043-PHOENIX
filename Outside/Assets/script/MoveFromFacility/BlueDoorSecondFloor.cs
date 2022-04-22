using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BlueDoorSecondFloor : MonoBehaviourPunCallbacks
{
    [SerializeField] 

    private  GameObject spawn_1;

    [SerializeField]
    private GameObject spawn_2;

    public bool isDone = false;

    private bool leftOrRight;// left false right true
    
    // Start is called before the first frame update
    void Start()
    {
        leftOrRight=false;
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        
            other.transform.position = spawn_1.transform.position;

            

            if(leftOrRight==false){
                this.photonView.RPC("FlipLeftOrRight",RpcTarget.All);
                other.transform.position = spawn_1.transform.position;
            }
            else if(leftOrRight==true){
                this.photonView.RPC("FlipLeftOrRight",RpcTarget.All);
                other.transform.position = spawn_2.transform.position;
            }
    
    }
    [PunRPC]
    void FlipLeftOrRight(){
        if(leftOrRight==false){
            leftOrRight=true;
        }
        else if(leftOrRight==true){
            leftOrRight=false;
        }

    }

}
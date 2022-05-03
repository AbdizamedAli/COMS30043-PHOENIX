using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SecondSectionManager : MonoBehaviourPunCallbacks
{
        [SerializeField]
    GameObject leftOne;
    [SerializeField]
    GameObject leftTwo;
    [SerializeField]
    GameObject leftThree;
    [SerializeField]
    GameObject rightOne;
    [SerializeField]
    GameObject rightTwo;
    [SerializeField]
    GameObject rightThree;
    float time;
    float delay;
    int currentBridge;
    void Awake(){
        rightOne.GetComponent<Renderer>().enabled=false;
        
        rightTwo.GetComponent<Renderer>().enabled=false;
        
        rightThree.GetComponent<Renderer>().enabled=false;
        

        leftOne.GetComponent<Collider>().enabled=false;
        rightOne.GetComponent<Collider>().enabled=false;
        leftTwo.GetComponent<Collider>().enabled=false;
        rightTwo.GetComponent<Collider>().enabled=false;
        leftThree.GetComponent<Collider>().enabled=false;
        rightThree.GetComponent<Collider>().enabled=false;

    }
    

    // Start is called before the first frame update
    void Start()
    {
        rightOne.GetComponent<Renderer>().enabled=false;
        
        rightTwo.GetComponent<Renderer>().enabled=false;
        
        rightThree.GetComponent<Renderer>().enabled=false;
        

        leftOne.GetComponent<Collider>().enabled=false;
        rightOne.GetComponent<Collider>().enabled=false;
        leftTwo.GetComponent<Collider>().enabled=false;
        rightTwo.GetComponent<Collider>().enabled=false;
        leftThree.GetComponent<Collider>().enabled=false;
        rightThree.GetComponent<Collider>().enabled=false;
        
        
        currentBridge=2;
        time=0f;
        delay=2.2f;
        
        
    
    }

    // Update is called once per frame
    void Update()
    {
        time=time+1f*Time.deltaTime;
        
        if(time>=delay&&PhotonNetwork.IsMasterClient){
            
            this.photonView.RPC("MovePaths",RpcTarget.All);
        }
        

    }
    [PunRPC]
    void MovePaths(){
        if(currentBridge==1){
                
                rightOne.GetComponent<Renderer>().enabled=true;
                leftOne.GetComponent<Collider>().enabled=true;
                rightOne.GetComponent<Collider>().enabled=true;

                rightTwo.GetComponent<Renderer>().enabled=false;
                leftTwo.GetComponent<Collider>().enabled=false;
                rightTwo.GetComponent<Collider>().enabled=false;
                currentBridge=3;
                time=0f;
            }
            else if(currentBridge==2){
                rightTwo.GetComponent<Renderer>().enabled=true;
                leftTwo.GetComponent<Collider>().enabled=true;
                rightTwo.GetComponent<Collider>().enabled=true;

                rightThree.GetComponent<Renderer>().enabled=false;
                leftThree.GetComponent<Collider>().enabled=false;
                rightThree.GetComponent<Collider>().enabled=false;


                
                currentBridge=1;
                time=0f;
            }
            else if(currentBridge==3){

                rightOne.GetComponent<Renderer>().enabled=false;
                leftOne.GetComponent<Collider>().enabled=false;
                rightOne.GetComponent<Collider>().enabled=false;

                rightThree.GetComponent<Renderer>().enabled=true;
                leftThree.GetComponent<Collider>().enabled=true;
                rightThree.GetComponent<Collider>().enabled=true;
    
                
                currentBridge=2;
                time=0f;

            }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ThirdSectionManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject leftOne;
    [SerializeField]
    GameObject leftOneSecond;
    [SerializeField]
    GameObject leftTwo;
    [SerializeField]
    GameObject leftTwoSecond;
    [SerializeField]
    GameObject leftThree;
    [SerializeField]
    GameObject leftThreeSecond;
    [SerializeField]
    GameObject rightOne;
    [SerializeField]
    GameObject rightOneSecond;
    [SerializeField]
    GameObject rightTwo;
    [SerializeField]
    GameObject rightTwoSecond;
    [SerializeField]
    GameObject rightThree;
    [SerializeField]
    GameObject rightThreeSecond;
    float time;
    float delay;

    
    int currentBridgeFirst;
    int currentBridgeSecond;
    void Awake(){
        leftOne.GetComponent<Renderer>().enabled=false;
        leftTwo.GetComponent<Renderer>().enabled=false;
        leftThree.GetComponent<Renderer>().enabled=false;
        

        leftOne.GetComponent<Collider>().enabled=false;
        rightOne.GetComponent<Collider>().enabled=false;
        leftTwo.GetComponent<Collider>().enabled=false;
        rightTwo.GetComponent<Collider>().enabled=false;
        leftThree.GetComponent<Collider>().enabled=false;
        rightThree.GetComponent<Collider>().enabled=false;

        rightOneSecond.GetComponent<Renderer>().enabled=false;
        rightTwoSecond.GetComponent<Renderer>().enabled=false;
        rightThreeSecond.GetComponent<Renderer>().enabled=false;
        

        leftOneSecond.GetComponent<Collider>().enabled=false;
        rightOneSecond.GetComponent<Collider>().enabled=false;
        leftTwoSecond.GetComponent<Collider>().enabled=false;
        rightTwoSecond.GetComponent<Collider>().enabled=false;
        leftThreeSecond.GetComponent<Collider>().enabled=false;
        rightThreeSecond.GetComponent<Collider>().enabled=false;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        leftOne.GetComponent<Renderer>().enabled=false;
        leftTwo.GetComponent<Renderer>().enabled=false;
        leftThree.GetComponent<Renderer>().enabled=false;
        

        leftOne.GetComponent<Collider>().enabled=false;
        rightOne.GetComponent<Collider>().enabled=false;
        leftTwo.GetComponent<Collider>().enabled=false;
        rightTwo.GetComponent<Collider>().enabled=false;
        leftThree.GetComponent<Collider>().enabled=false;
        rightThree.GetComponent<Collider>().enabled=false;

        rightOneSecond.GetComponent<Renderer>().enabled=false;
        rightTwoSecond.GetComponent<Renderer>().enabled=false;
        rightThreeSecond.GetComponent<Renderer>().enabled=false;
        

        leftOneSecond.GetComponent<Collider>().enabled=false;
        rightOneSecond.GetComponent<Collider>().enabled=false;
        leftTwoSecond.GetComponent<Collider>().enabled=false;
        rightTwoSecond.GetComponent<Collider>().enabled=false;
        leftThreeSecond.GetComponent<Collider>().enabled=false;
        rightThreeSecond.GetComponent<Collider>().enabled=false;
        
        
        time=0f;
        delay=0.8f;
        
        currentBridgeFirst=1;
        currentBridgeSecond=3;
        
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
        if(currentBridgeFirst==1){
                leftThree.GetComponent<Renderer>().enabled=false;
                leftThree.GetComponent<Collider>().enabled=false;
                rightThree.GetComponent<Collider>().enabled=false;
                leftOne.GetComponent<Renderer>().enabled=true;
                leftOne.GetComponent<Collider>().enabled=true;
                rightOne.GetComponent<Collider>().enabled=true;
                currentBridgeFirst=2;
                time=0f;
            }
            else if(currentBridgeFirst==2){
                leftTwo.GetComponent<Renderer>().enabled=true;
                leftTwo.GetComponent<Collider>().enabled=true;
                rightTwo.GetComponent<Collider>().enabled=true;

                leftOne.GetComponent<Renderer>().enabled=false;
                leftOne.GetComponent<Collider>().enabled=false;
                rightOne.GetComponent<Collider>().enabled=false;
                currentBridgeFirst=3;
                time=0f;
            }
            else if(currentBridgeFirst==3){
                leftThree.GetComponent<Renderer>().enabled=true;
                leftThree.GetComponent<Collider>().enabled=true;
                rightThree.GetComponent<Collider>().enabled=true;
    
                leftTwo.GetComponent<Renderer>().enabled=false;
                leftTwo.GetComponent<Collider>().enabled=false;
                rightTwo.GetComponent<Collider>().enabled=false;
                currentBridgeFirst=1;
                time=0f;

            }





            if(currentBridgeSecond==1){
                
                rightOneSecond.GetComponent<Renderer>().enabled=true;
                leftOneSecond.GetComponent<Collider>().enabled=true;
                rightOne.GetComponent<Collider>().enabled=true;

                rightTwoSecond.GetComponent<Renderer>().enabled=false;
                leftTwoSecond.GetComponent<Collider>().enabled=false;
                rightTwoSecond.GetComponent<Collider>().enabled=false;
                currentBridgeSecond=3;
                
            }
            else if(currentBridgeSecond==2){
                rightTwoSecond.GetComponent<Renderer>().enabled=true;
                leftTwoSecond.GetComponent<Collider>().enabled=true;
                rightTwo.GetComponent<Collider>().enabled=true;

                rightThreeSecond.GetComponent<Renderer>().enabled=false;
                leftThreeSecond.GetComponent<Collider>().enabled=false;
                rightThreeSecond.GetComponent<Collider>().enabled=false;


                
                currentBridgeSecond=1;
                
            }
            else if(currentBridgeSecond==3){

                rightOneSecond.GetComponent<Renderer>().enabled=false;
                leftOneSecond.GetComponent<Collider>().enabled=false;
                rightOneSecond.GetComponent<Collider>().enabled=false;

                rightThreeSecond.GetComponent<Renderer>().enabled=true;
                leftThreeSecond.GetComponent<Collider>().enabled=true;
                rightThreeSecond.GetComponent<Collider>().enabled=true;
    
                
                currentBridgeSecond=2;
                

            }

    }
}

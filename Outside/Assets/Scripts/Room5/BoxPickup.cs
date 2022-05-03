using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPickup : MonoBehaviour
{
    public Transform boxPosition;
    private GameObject boxOne;
    private GameObject boxTwo;
    private GameObject boxThree;
    private GameObject boxFour;
    private bool pickup;
    private List<string> boxList;
    // Start is called before the first frame update
    
    void Start()
    {
        pickup=false;
        

    }
    void OnMouseDown(){
        
        GetComponent<Rigidbody>().useGravity=false;
        this.transform.position=boxPosition.position;
        this.transform.parent=GameObject.Find("PickupLocation").transform;
        
    }
    void OnMouseUp(){
        this.transform.parent=null;
        GetComponent<Rigidbody>().useGravity=true;      
    }
}

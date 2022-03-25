using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSelectionManager : MonoBehaviour
{
    public GameObject BlueDoor;
    public GameObject RedDoor;
    public GameObject YellowDoor;

    public Transform BlueDoorLocation;
    public Transform RedDoorLocation;
    public Transform YellowDoorLocation;
    // Start is called before the first frame update
    int BlueDoorCount;
    int RedDoorCount;
    int YellowDoorCount;

    
    

    void Awake(){
        BlueDoorCount=0;
        RedDoorCount=0;
        YellowDoorCount=0;
       
        //no for loop yet, easier to add extra doors when needed.
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(BlueDoorCount==2){
            BlueDoor.transform.position=BlueDoorLocation.transform.position;
        }
        else if(YellowDoorCount==2){
            YellowDoor.transform.position=YellowDoorLocation.transform.position;
        }
        else if(RedDoorCount>=2){
            RedDoor.transform.position=RedDoorLocation.transform.position;
        }
    }
    //Add doors as needed

    public void DecreasePlayerCount(string DoorName){
        if (DoorName=="BlueDoor"){
            BlueDoorCount=BlueDoorCount-1;
        }
        if (DoorName=="RedDoor"){
            BlueDoorCount=BlueDoorCount-1;
        }
        if (DoorName=="YellowDoor"){
            BlueDoorCount=BlueDoorCount-1;
        }
        

    }

    public void IncreasePlayerCount(string DoorName){
        if (DoorName=="BlueDoor"){
            BlueDoorCount++;
        }
        if (DoorName=="RedDoor"){
            BlueDoorCount++;
        }
        if (DoorName=="YellowDoor"){
            BlueDoorCount++;
        }
    }
    

    
}

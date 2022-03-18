using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCompleteManager : MonoBehaviour

{
    [SerializeField]
    public GameObject leftExitDoor;
    [SerializeField]
    public GameObject rightExitDoor;


    private bool leftCompleted;
    private bool rightCompleted;
    // Start is called before the first frame update
    void Start()
    {
        leftCompleted=false;
        rightCompleted=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(leftCompleted==true && rightCompleted==true){
            //add code to spawn exit doors here
            print("room completed");
        }
    }
    public void LeftComplete(){
        leftCompleted=true;
    }
    public void RightComplete(){
        rightCompleted=true;
    }
}

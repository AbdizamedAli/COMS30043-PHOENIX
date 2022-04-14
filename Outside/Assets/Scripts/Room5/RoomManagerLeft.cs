using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManagerLeft : MonoBehaviour

{
    [SerializeField]
    private GameObject boxOne;
    [SerializeField]
    private GameObject boxTwo;
    [SerializeField]
    private GameObject boxThree;
    [SerializeField]
    private GameObject boxFour;
    [SerializeField]
    private GameObject boxFive;
    [SerializeField]
    private GameObject boxSix;
    private List<string> boxList;


    private RoomCompleteManager RoomCompleteManager;

    private bool completed;
    // Start is called before the first frame update
    void Awake(){
        boxList = new List<string>();

        RoomCompleteManager=GameObject.FindObjectOfType<RoomCompleteManager>();

        boxList.Add(boxOne.name);
        boxList.Add(boxTwo.name);
        boxList.Add(boxThree.name);
        boxList.Add(boxFour.name);
        boxList.Add(boxFive.name);
        boxList.Add(boxSix.name);
        completed =false;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //print(boxList.Count);
        if(boxList.Count==0&&completed==false){//false so it only calls complete manager once, if list is empty then all boxes are in correct position
        

            RoomCompleteManager.LeftComplete();
            
            completed=true;
            print("left yes");
            
        }
    }


    public void RemoveCorrectPosition(string boxName){ //adds box back to list, the number of correct positions has decresed by 1
        boxList.Add(boxName);
    
    }


    public void AddCorrectPosition(string boxName){ //removes box from list, the number of correct positions has increased by 1
        print(boxName);
        print("called");
        int boxPosition=boxList.IndexOf(boxName);
        boxList.RemoveAt(boxPosition);
        
    }
}

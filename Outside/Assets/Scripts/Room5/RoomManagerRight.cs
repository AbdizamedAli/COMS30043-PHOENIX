using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManagerRight : MonoBehaviour
{
    
    [SerializeField]
    private GameObject boxThree;
    [SerializeField]
    private GameObject boxFour;
    
    private List<string> boxList;
    private RoomCompleteManager RoomCompleteManager;
    private bool completed;

    void Awake(){
        boxList = new List<string>();
        RoomCompleteManager=GameObject.FindObjectOfType<RoomCompleteManager>();

        
        boxList.Add(boxThree.name);
        boxList.Add(boxFour.name);
        
        completed =false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(boxList.Count==0&&completed==false){//false so it only calls complete manager once

            RoomCompleteManager.RightComplete();
            completed=true;
            
        }
    }

    public void RemoveCorrectPosition(string boxName){
        boxList.Add(boxName);
    }


    public void AddCorrectPosition(string boxName){
        boxList.Remove(boxName);
    }
}

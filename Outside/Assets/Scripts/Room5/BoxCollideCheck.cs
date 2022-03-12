using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollideCheck : MonoBehaviour
{
    [SerializeField]
    private GameObject box;
    [SerializeField]
    private GameObject Square;

    

    private RoomManagerLeft RoomManagerLeft;
    private RoomManagerRight RoomManagerRight;
    private bool collision;
    [SerializeField]
    private string leftOrRight;

    private string squareName;





    void Awake(){
        RoomManagerLeft=GameObject.FindObjectOfType<RoomManagerLeft>();
        RoomManagerRight=GameObject.FindObjectOfType<RoomManagerRight>();
        squareName=Square.name;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void OnTriggerEnter(Collider other)
    {
        string otherName=other.name;
        collision = true;
        
        if (otherName.Equals(squareName)){
            
            if(leftOrRight=="left"){
                RoomManagerLeft.AddCorrectPosition(squareName);
            }
            else if(leftOrRight=="right"){
                RoomManagerRight.AddCorrectPosition(squareName);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        string otherName=other.name;
        collision = false;
        if (otherName.Equals(squareName)){
            if(leftOrRight=="left"){
                RoomManagerLeft.RemoveCorrectPosition(squareName);
            }
            else if(leftOrRight=="right"){
                RoomManagerRight.RemoveCorrectPosition(squareName);
            }

        }
    }
}

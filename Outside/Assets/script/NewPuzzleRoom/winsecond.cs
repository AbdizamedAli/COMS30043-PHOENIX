using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winsecond : MonoBehaviour
{
    public GameObject Ret;
    private FloorManagerThree FloorManagerThree;
    // Start is called before the first frame update
    void Awake(){
        FloorManagerThree=GameObject.FindObjectOfType<FloorManagerThree>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            
            other.transform.position = Ret.transform.position;
            FloorManagerThree.PuzzleComplete();


        }


    }
}

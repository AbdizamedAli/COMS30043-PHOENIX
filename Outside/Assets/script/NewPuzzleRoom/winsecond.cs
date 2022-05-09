using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winsecond : MonoBehaviour
{
    public GameObject Ret;
    private FloorManagerThree FloorManagerThree;
    private bool done;
    // Start is called before the first frame update
    void Awake(){
        FloorManagerThree=GameObject.FindObjectOfType<FloorManagerThree>();
    }
    void Start()
    {
       done=false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"&&done==false)
        {
            done=true;
            other.transform.position = Ret.transform.position;
            FloorManagerThree.PuzzleComplete();


        }


    }
}

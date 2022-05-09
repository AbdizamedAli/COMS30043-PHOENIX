using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class win : MonoBehaviour
{
    public GameObject Ret;
    private FloorManagerTwo FloorManagerTwo;
    private bool done;
    // Start is called before the first frame update
    void Awake(){
        FloorManagerTwo=GameObject.FindObjectOfType<FloorManagerTwo>();
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
            
            other.transform.position = Ret.transform.position;
            FloorManagerTwo.PuzzleComplete();


        }


    }
}

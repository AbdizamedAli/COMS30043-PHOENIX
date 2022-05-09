using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBuitton : MonoBehaviour
{
    

    private bool collision;
    private PuzzleCompleteManager PuzzleCompleteManager;
    private bool done;

    
    void Awake(){
        PuzzleCompleteManager=GameObject.FindObjectOfType<PuzzleCompleteManager>();
        done=false;
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
         if (collision)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(collision&&done==false){
                    PuzzleCompleteManager.CompleteRoom();
                    done=true;
                }
            }
        }

    }
    void OnTriggerEnter(Collider other)
    {
        collision = true;
    }
    void OnTriggerExit(Collider other)
    {
        collision = false;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolButton7 : MonoBehaviour
{
    private bool collision=false;
    private bool pressed=false;
    private SymbolChecker SymbolChecker;
    // Start is called before the first frame update
    void Awake(){
        SymbolChecker=GameObject.FindObjectOfType<SymbolChecker>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (collision){
            if(Input.GetKeyDown(KeyCode.E)){
               if(pressed==false){
                   pressed=true;
               }
               else if(pressed==true){
                   pressed=false;
               }
               SymbolChecker.SymbolSevenPressed(pressed);
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            collision = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {

            collision = false;
        }
    }
}

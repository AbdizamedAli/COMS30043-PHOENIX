using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafButton : MonoBehaviour
{
    private bool collision = false;
    private bool pressed = false;
    public AI_Behaviour checker;
    //private SymbolChecker SymbolChecker;
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
                if (pressed == false)
                {
                    pressed = true;
                }
                else if (pressed == true)
                {
                    pressed = false;
                }
                //SymbolChecker.SymbolOnePressed(pressed);
                checker.CheckRiddle("Leaf");
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

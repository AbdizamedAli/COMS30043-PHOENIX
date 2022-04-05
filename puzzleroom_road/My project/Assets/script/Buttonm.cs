using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttonm : MonoBehaviour
{
    public GameObject cy;
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
        if (other.tag == "Player")
        {
            Debug.Log("touch");
            cy.GetComponent<move>().flag = true;
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("touch");
            cy.GetComponent<move>().flag = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttonb : MonoBehaviour
{
    public GameObject Bridge;
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
        if(other.tag == "Player")
        {
            Bridge.GetComponent<bridge>().flag = false;
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Bridge.GetComponent<bridge>().flag = true;
        }
    }
}

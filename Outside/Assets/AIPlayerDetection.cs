using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayerDetection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        Cursor.lockState = CursorLockMode.Confined;
        Debug.Log("Cursor Trigger Begin");
    }

    void OnTriggerExit(Collider other)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("Cursor Trigger End");
    }
}

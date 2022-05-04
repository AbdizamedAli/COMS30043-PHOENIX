using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorControl : MonoBehaviour
{
    public bool showCursor;

    // Start is called before the first frame update
    void Start()
    {
        showCursor = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!showCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}

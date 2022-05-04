using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDoor : MonoBehaviour
{
    [SerializeField] private GameObject spawn_1;
    public bool isDone = false;
    [SerializeField] CursorControl cursorControl;
    [SerializeField] Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isDone)
        {
            other.transform.position = spawn_1.transform.position;
            canvas.worldCamera = other.GetComponent<Camera>();
        }
        //cursorControl.GetComponent<bool>() = true;
        cursorControl.showCursor = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueDoor : MonoBehaviour
{
    [SerializeField] private GameObject spawn_1;
    public bool isDone = false;
    [SerializeField] GameObject cursorControl;
    private bool showCursor;

    // Start is called before the first frame update
    void Start()
    {
        showCursor = cursorControl.GetComponent<bool>();
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
        }
        //cursorControl.GetComponent<bool>() = true;
        showCursor = true;
    }
}

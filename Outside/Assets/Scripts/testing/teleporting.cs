using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleporting : MonoBehaviour
{
    [SerializeField] private GameObject spawn_1;
    public bool isDone = false;
    
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
        
            other.transform.position = spawn_1.transform.position;
    
    }
}
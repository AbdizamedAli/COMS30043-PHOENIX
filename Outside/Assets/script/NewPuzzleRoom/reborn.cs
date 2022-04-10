using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
public class reborn : MonoBehaviour
{
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
        if(other.tag == "Player")
        {
            other.transform.Translate(new Vector3(114, 4, -79));
        }
    }
}

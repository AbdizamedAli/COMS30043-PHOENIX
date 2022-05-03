using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
public class reborn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject re;
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
            other.transform.position = re.transform.position;
        }
    }
}

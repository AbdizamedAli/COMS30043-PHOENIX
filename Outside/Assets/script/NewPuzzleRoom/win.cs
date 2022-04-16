using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class win : MonoBehaviour
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
            if (Input.GetKeyDown(KeyCode.E))
            {
                Destroy(other);
                PhotonNetwork.LoadLevel("PuzzleRoom01");
            }
        }


    }
}

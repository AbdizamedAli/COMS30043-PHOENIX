using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using photon;
public class teleport : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
        }
    }
    [PunRPC]
    void LoadGameScene()
    {
        PhotonNetwork.LoadLevel("PuzzleRoom01");
    }
}

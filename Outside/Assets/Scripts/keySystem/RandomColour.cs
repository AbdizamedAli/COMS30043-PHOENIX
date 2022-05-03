using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RandomColour : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update

    private void Awake()
    {
        gameObject.GetComponent<Renderer>().material.color = Random.ColorHSV();
    }

}

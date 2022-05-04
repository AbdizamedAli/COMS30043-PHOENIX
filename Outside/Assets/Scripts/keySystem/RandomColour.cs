using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RandomColour : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update

    private void Awake()
    {
        gameObject.GetComponent<Renderer>().material.color = GetColor(Random.Range(0,5));
    }

    private Color GetColor(int c)
    {
        Debug.Log(c);
        switch (c)
        {
            case 0: return Color.green;
            case 1: return Color.gray;
            case 2: return Color.black;
            case 3: return Color.cyan;
            case 4: return Color.magenta;
        }
        return Color.blue;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RandomColour : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update

    private void Awake()
    {
        gameObject.GetComponent<Renderer>().material.color = GetColor(Random.Range(0,10));
    }

    private Color GetColor(int c)
    {
        switch (c)
        {
            case 0: return Color.green;
            case 1: return Color.gray;
            case 2: return Color.black;
            case 3: return Color.cyan;
            case 4: return Color.magenta;
            case 5: return new Color(1f,0.65f,0f);
            case 6: return new Color(0.9f, 0.9f, 0.98f);
            case 7: return new Color(0.66f, 0.4f, 0.16f);
            case 8: return new Color(0.5f, 0f, 0f);
            case 9: return new Color(0f, 0.5f, 0.5f);

        }
        return Color.blue;
    }
}

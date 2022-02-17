using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace keySystem
{
    public class startRoomClue : MonoBehaviour
    {
        [SerializeField]private GameObject key;
        // Start is called before the first frame update
        void FixedUpdate()
        {
            gameObject.GetComponent<Renderer>().material.color = key.GetComponent<keyitemController>().key_colour;
        }

    }
}

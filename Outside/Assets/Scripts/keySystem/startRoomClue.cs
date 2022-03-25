using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace keySystem
{
    public class startRoomClue : MonoBehaviour
    {
        public GameObject actual_key;
        // Start is called before the first frame update
        void Start()
        {
            gameObject.GetComponent<Renderer>().material.color = actual_key.GetComponent<keyitemController>().key_colour;
        }

    }
}

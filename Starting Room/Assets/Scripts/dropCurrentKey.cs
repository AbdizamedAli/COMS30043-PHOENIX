using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace keySystem
{
    public class dropCurrentKey : MonoBehaviour
    {
        public GameObject current_key;
        public bool has_key = false;
        public KeyInventory key_inv;


        public void swapPosition(Vector3 position)
        {
            if (has_key)
            {
                current_key.transform.position = position;
                current_key.GetComponentInChildren<keyRayCast>().picked_up = false;
                current_key.SetActive(true);

                if (current_key.GetComponent<keyitemController>().key)
                {
                    key_inv.has_key = false;
                    
                }
            }
        }

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace keySystem
{
    public class keyRayCast : MonoBehaviour
    {
        public GameObject key_object;
        public bool trigger = false;
        public bool picked_up = false;
        private keyitemController key_to_get;
        public GameObject instruction;

        void Update()
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (trigger && !picked_up)
                {
                    key_object.SetActive(false);
                    key_to_get.objectInteraction();
                    trigger = false;
                    picked_up = true;
                    instruction.SetActive(false);

                }
            }
        }


        void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.tag == "Player" && !picked_up)
            {
                trigger = true;
                key_to_get = key_object.GetComponent<keyitemController>();
                instruction.SetActive(true);
            }
        }

        void OnTriggerExit(Collider other)
        {
            instruction.SetActive(false);

            trigger = false;
        }



    }
}

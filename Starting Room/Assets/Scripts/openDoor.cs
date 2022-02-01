using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace keySystem
{
    public class openDoor : MonoBehaviour
    {
        public GameObject animate_object;
        public GameObject instruction_has_key;
        public GameObject instruction_needs_key;
        [SerializeField] private KeyInventory key_inventory = null;
        public bool action = false;
        public bool open = false;


        void OnTriggerEnter(Collider collision) 
        {
            if (collision.transform.tag == "Player")
            {
                if (key_inventory.has_key)
                {
                    instruction_has_key.SetActive(true);
                }
                else if (!key_inventory.has_key)
                {
                    instruction_needs_key.SetActive(true);
                }
                action = true;
            }

        }

        void OnTriggerExit(Collider other) 
        {
            if (key_inventory.has_key)
            {
                instruction_has_key.SetActive(false);
            }
            else if (!key_inventory.has_key)
            {
                instruction_needs_key.SetActive(false);
            }
            action = false;    
        }

        void Update() 
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (action && !open && key_inventory.has_key)
                {

                    animate_object.GetComponent<Animator>().Play("door open");
                    action = false;
                    open = true;
                }
                // else if (action && open)
                // {
                //     animate_object.GetComponent<Animator>().Play("door closed");
                //     action = false;
                //     open = false;
                // }
            }
        }
    }
}
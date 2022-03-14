using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
namespace keySystem
{
    public class openDoor : MonoBehaviourPunCallbacks
    {
        public GameObject animate_object;
        [SerializeField] private KeyInventory key_inventory = null;
        public bool action = false;
        public bool open = false;


        void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.tag == "Player" && !open)
            {
                if (key_inventory.has_key)
                {

                        collision.GetComponent<Instructions>().has_key.SetActive(true);
                    
                }
                else if (!key_inventory.has_key)
                {
              
                        collision.GetComponent<Instructions>().need_key.SetActive(true);

                    
                }
                action = true;
            }

        }

        void OnTriggerExit(Collider other)
        {

            
            if (key_inventory.has_key)
            {
                Instructions instruct = other.GetComponent<Instructions>();

                if (instruct)
                {
                    instruct.has_key.SetActive(false);
                }


                
            }
            else if (!key_inventory.has_key)
            {
                Instructions instruct = other.GetComponent<Instructions>();

                if (instruct)
                {
                    instruct.need_key.SetActive(false);
                }

                
                action = false;
            }
        }

        void Update()
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (action && !open && key_inventory.has_key)
                {

                    this.photonView.RPC("playAnimation", RpcTarget.All);
                    action = false;
                    open = true;
                }
            }
        }
        [PunRPC]
        private void playAnimation()
        {
            animate_object.GetComponent<Animator>().Play("start_room_door_open");

        }
    }
}
using UnityEngine;
using Photon.Pun;

namespace keySystem
{
    public class keyitemController : MonoBehaviourPunCallbacks
    {
        [SerializeField] public bool key = false;
        [SerializeField] public KeyInventory key_inventory = null;
        public GameObject clue;

        public Color key_colour;
        public Vector3 actual_key_location;

        [SerializeField] bool actual = false;

        void Awake()
        {

        }
        void Start()
        {
            key_colour = Random.ColorHSV();
            if (PhotonNetwork.IsMasterClient && actual)
            {

                this.photonView.RPC("setKeyColor", RpcTarget.All, new Vector3(key_colour.r, key_colour.g, key_colour.b));
            }

        }

        public void objectInteraction()
        {
            if (key)
            {
                key_inventory.has_key = true;
                this.photonView.RPC("pickUp", RpcTarget.All);


            }
        }
        [PunRPC]
        private void setKeyColor(Vector3 colour)
        {
            Color cols = new Color(colour.x, colour.y, colour.z);
            gameObject.GetComponent<Renderer>().material.color = cols;
            clue.GetComponent<Renderer>().material.color = cols;

        }

        [PunRPC]
        private void pickUp()
        {
            gameObject.SetActive(false);

        }
    }
}
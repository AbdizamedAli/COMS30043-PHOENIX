using System.Collections.Generic;
using System.IO;
using System.Linq;
using Photon.Pun;
using UnityEngine;

namespace keySystem
{
    public class generateFalseKeys : MonoBehaviourPunCallbacks
    {
        public GameObject actual_key;
        private int false_keys = 10;
        public GameObject center;
        [SerializeField] GameObject clue;
        [SerializeField] KeyInventory key_inv;

        void Start()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                List<Vector3> random_postions = generateRandomPositions();
                foreach (var item in random_postions)
                {
                    this.photonView.RPC("spawnKeys", RpcTarget.All, item);


                }

            }
        }

        [PunRPC]
        public void spawnKeys(Vector3 position)
        {
            GameObject new_key = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Key Variant"), position, Quaternion.identity);
            new_key.GetComponentInChildren<keyitemController>().key = false;
            new_key.GetComponentInChildren<keyitemController>().key_inventory = key_inv;
            new_key.GetComponentInChildren<keyitemController>().clue = clue;

        }

        private Vector3 generateRandomPostion()
        {
            System.Random range_calc = new System.Random();
            return new Vector3(Random.Range(center.transform.position.x-4f,
                center.transform.position.x + 4f),
                center.transform.position.y,
                Random.Range(center.transform.position.z - 4f,
                center.transform.position.z + 4f));

        }


        public List<Vector3> generateRandomPositions()
        {
            List<Vector3> random_positions = new List<Vector3>();

            for (int i = 0; i < false_keys; i++)
            {
                Vector3 random_postition = generateRandomPostion();

                while (random_positions.Contains(random_postition))
                {
                    random_postition = generateRandomPostion();

                }

                random_positions.Add(random_postition);


            }

            return random_positions;
        }

    }
}

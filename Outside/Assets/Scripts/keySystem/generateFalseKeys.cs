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
            GameObject new_key = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "actual_key_false"), position, Quaternion.identity);
            new_key.GetComponent<keyitemController>().key = false;
            new_key.GetComponent<keyitemController>().key_inventory = key_inv;
            new_key.GetComponent<keyitemController>().clue = clue;

        }

        private Color randomColour()
        {
            return new Color(Random.Range(0f, 1f),
                             Random.Range(0f, 1f),
                             Random.Range(0f, 1f)
                             );
        }

        private Color getRandomColour()
        {
            Color new_colour = randomColour(); 
            while (new_colour.Equals(actual_key.GetComponent<keyitemController>().key_colour))
            {
                new_colour = randomColour();
            }
            return new_colour;

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

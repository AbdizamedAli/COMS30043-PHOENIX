using System.Collections.Generic;
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

        // Start is called before the first frame update
        //void Start()
        //{
        //    if (PhotonNetwork.IsMasterClient)
        //    {
        //        List<Vector3> random_postions = generateRandomPositions();
        //        foreach (var item in random_postions)
        //        {
        //        this.photonView.RPC("spawnKeys", RpcTarget.All, item);
                    

        //        }

        //    }
        //}

        [PunRPC]
        public void spawnKeys(Vector3 postion)
        {
            GameObject new_key = Instantiate(actual_key);
            new_key.GetComponent<PhotonView>().ViewID = PhotonNetwork.AllocateViewID(false);
            new_key.GetComponent<keyitemController>().key = false;
            new_key.transform.position = postion;
            new_key.GetComponent<Renderer>().material.color = getRandomColour();

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
            return new Vector3(Random.Range(center.transform.position.x-1f,
                center.transform.position.x + 1f),
                center.transform.position.y,
                Random.Range(center.transform.position.z - 1f,
                center.transform.position.z + 1f));

        }


        public List<Vector3> generateRandomPositions()
        {
            List<Vector3> random_positions = new List<Vector3>();

            for (int i = 0; i < false_keys; i++)
            {
                Vector3 random_postition = generateRandomPostion();
                bool valid_position = false;


                while (!valid_position || random_postition.Equals(actual_key.GetComponent<keyitemController>().actual_key_location) ||random_positions.Contains(random_postition))
                {
                    random_postition = generateRandomPostion();

                    valid_position = true;

                    Collider[] colliders = Physics.OverlapBox(random_postition, transform.localScale , Quaternion.identity);

                    foreach(Collider col in colliders)
                    {
                        if (col.tag == "key")
                        {
                            valid_position = false;
                        }
                    }

                }
                if (valid_position)
                {
                    random_positions.Add(random_postition);

                }


            }

            return random_positions;
        }

    }
}

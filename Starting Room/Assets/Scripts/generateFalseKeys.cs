using System.Linq;
using UnityEngine;

namespace keySystem
{
    public class generateFalseKeys : MonoBehaviour
    {
        public GameObject actual_key;
        private int false_keys = 10;

        // Start is called before the first frame update
        void Start()
        {
            Vector3[] random_postions = generateRandomPositions();
            spawnKeys(random_postions);
        }

        private void spawnKeys(Vector3[] postions)
        {
            foreach (var item in postions)
            {
                GameObject new_key = Instantiate(actual_key);
                new_key.GetComponent<keyitemController>().key = false;
                new_key.transform.position = item;
                new_key.GetComponent<Renderer>().material.color = getRandomColour();

            }

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
            return new Vector3(Random.Range(actual_key.transform.position.x-4,
                actual_key.transform.position.x + 4),
                actual_key.transform.position.y,
                Random.Range(actual_key.transform.position.z - 4,
                actual_key.transform.position.z + 4));

        }


        private Vector3[] generateRandomPositions()
        {
            Vector3[] random_positions = new Vector3[false_keys];

            for (int i = 0; i < false_keys; i++)
            {
                Vector3 random_postition = generateRandomPostion();
                while (random_positions.Contains(random_postition) || random_postition.Equals(actual_key.GetComponent<keyitemController>().actual_key_location))
                {
                    random_postition = generateRandomPostion();

                }
                random_positions[i] = random_postition;

            }

            return random_positions;
        }

    }
}

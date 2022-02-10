using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace keySystem
{
    public class randomKeyLocation : MonoBehaviour
    {
        public GameObject key;

        public void Start()
        {
            System.Random range_calc = new System.Random();
            float x_pos = key.transform.position.x;
            float z_pos = key.transform.position.z;

            float x_min = x_pos - 4;
            float x_max = x_pos + 4;

            float z_min = z_pos - 4;
            float z_max = z_pos + 4;


            float new_x = generateRandomLocation(range_calc, x_min, x_max);
            float new_z = generateRandomLocation(range_calc, z_min, z_max);

            setKeyPosition(new_x, key.transform.position.y, new_z);
        }

        private float generateRandomLocation(System.Random random, float min, float max)
        {
            float val = random.Next((int)min, (int)max);
            float offset = (float)val;
            return offset;
        }

        private void setKeyPosition(float x, float y, float z)
        {
            key.transform.position = new Vector3(x, y, z);
            key.GetComponent<keyitemController>().actual_key_location = key.transform.position;
        }
    }
}

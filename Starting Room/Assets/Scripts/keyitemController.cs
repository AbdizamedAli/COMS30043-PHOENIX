using UnityEngine;

namespace keySystem
{
    public class keyitemController : MonoBehaviour
    {
        [SerializeField] public bool key = false;

        [SerializeField] private KeyInventory key_inventory = null;

        public Color key_colour;
        public Vector3 actual_key_location;

        void Awake()
        {
            key_colour = new Color(Random.Range(0f, 1f),
                                   Random.Range(0f, 1f),
                                   Random.Range(0f, 1f)
                                   );
        }
        void Start()
        {
            gameObject.GetComponent<Renderer>().material.color = key_colour;
        }

        public void objectInteraction()
        {
            if (key)
            {
                key_inventory.has_key = true;

                gameObject.SetActive(false);

            }
        }
    }
}
using UnityEngine;

namespace keySystem
{
    public class keyitemController : MonoBehaviour
    {
        [SerializeField] private bool key = false;

        [SerializeField] private KeyInventory key_inventory = null;

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
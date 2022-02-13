using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectInteraction
{
    public class SelectionManager : MonoBehaviour
    {
        //[SerializeField] Material highlightMaterial;
        //[SerializeField] Camera camera;
        [SerializeField] GameObject instruction;
        [SerializeField] Inventory inventory;
        private bool isObjectSelected;
        private GameObject objectSelected;

        // Start is called before the first frame update
        void Start()
        {
            instruction.SetActive(false);
            isObjectSelected = false;
        }
        // Update is called once per frame
        void Update()
        {
            var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var selection = hit.transform;
                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null && selectionRenderer.tag == "Selectable")
                {
                    //selectionRenderer.material = highlightMaterial;
                    instruction.SetActive(true);
                    isObjectSelected = true;
                    objectSelected = selectionRenderer.gameObject;
                }
                else
                {
                    instruction.SetActive(false);
                    isObjectSelected = false;
                }
            }
            //Debug.DrawRay(Camera.main.ViewportPointToRay(Input.mousePos))

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isObjectSelected)
                {
                    if (objectSelected.name == "Screwdriver_Single")
                    {
                        inventory.has_screwdriver = true;
                    }
                    if (inventory.has_screwdriver)
                    {
                        hit.collider.gameObject.SetActive(false);
                    }
                }
            }
        }

        void implementPickup()
        {

        }

        void implementRotate()
        {

        }

        void implementUnscrewPanel()
        {

        }
    }
}
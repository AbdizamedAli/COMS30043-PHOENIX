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
        [SerializeField] GameObject rotateInstruction;
        [SerializeField] GameObject pressInstruction;
        [SerializeField] GameObject solvedMessage;
        [SerializeField] Inventory inventory;
        private bool isObjectSelected;
        private GameObject objectSelected;
        private GameObject redButton;

        // Start is called before the first frame update
        void Start()
        {
            rotateInstruction.SetActive(false);
            instruction.SetActive(false);
            pressInstruction.SetActive(false);
            redButton = GameObject.Find("BigRedButton");
            redButton.SetActive(false);
            isObjectSelected = false;
            solvedMessage.SetActive(false);
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
                else if (selectionRenderer != null && selectionRenderer.tag == "Rotatable")
                {
                    rotateInstruction.SetActive(true);
                    isObjectSelected = true;
                    objectSelected = selectionRenderer.gameObject;
                }
                else if (selectionRenderer != null && selectionRenderer.gameObject.name == "BigRedButton")
                {
                    pressInstruction.SetActive(true);
                    isObjectSelected = true;
                    objectSelected = selectionRenderer.gameObject;
                }
                else
                {
                    instruction.SetActive(false);
                    rotateInstruction.SetActive(false);
                    pressInstruction.SetActive(false);
                    isObjectSelected = false;
                }
            }
            //Debug.DrawRay(Camera.main.ViewportPointToRay(Input.mousePos))

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isObjectSelected)
                {
                    if (objectSelected.tag == "Selectable")
                    {
                        if (objectSelected.name == "Screwdriver_Single")
                        {
                            inventory.has_screwdriver = true;
                        }
                        if (inventory.has_screwdriver)
                        {
                            hit.collider.gameObject.SetActive(false);
                            if (objectSelected.name == "PowerPanelFront")
                            {
                                redButton.SetActive(true);
                            }                            
                        }
                    }
                    else if (objectSelected.tag == "Rotatable")
                    {
                        objectSelected.transform.Rotate(0.0f, 90.0f, 0.0f, Space.Self);
                        Debug.Log(objectSelected.transform.rotation.eulerAngles);
                    }
                    else if (objectSelected.name == "BigRedButton")
                    {
                        var correctSolution = true;
                        foreach (Transform panel in GameObject.Find("WirePuzzle").transform)
                        {
                            if (panel.rotation.eulerAngles.y % 360 != 0)
                            {
                                correctSolution = false;
                                Debug.Log("Incorrect");
                            }
                        }
                        if (correctSolution == true)
                        {
                            solvedMessage.SetActive(true);
                            Debug.Log("Correct");
                        }
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
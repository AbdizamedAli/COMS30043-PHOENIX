using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

namespace ObjectInteraction
{
    public class SelectionManager : MonoBehaviourPunCallbacks
    {
        //[SerializeField] Material highlightMaterial;
        //[SerializeField] Camera camera;
        [SerializeField] Inventory inventory;
        [SerializeField] GameObject exit;
        private bool isObjectSelected;
        private GameObject objectSelected;
        private GameObject redButton;
        private bool solved = false;
        [SerializeField] GameObject enter;
        [SerializeField] GameObject enterText;

        // Start is called before the first frame update
        void Start()
        {
            redButton = GameObject.Find("BigRedButton");
            redButton.SetActive(false);
            isObjectSelected = false;
        }
        // Update is called once per frame
        void Update()
        {
            if (!solved)
            {
                Camera[] c = Camera.allCameras;

                foreach (var cam in c)
                {
                    RaycastHit hit;
                    var ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

                    if (Physics.Raycast(ray, out hit) && cam.GetComponentInParent<PhotonView>().IsMine)
                    {
                        var selection = hit.transform;
                        var selectionRenderer = selection.GetComponent<Renderer>();
                        if (selectionRenderer != null && selectionRenderer.tag == "Selectable")
                        {
                            //selectionRenderer.material = highlightMaterial;
                            cam.GetComponentInParent<Instructions>().screw.SetActive(true);
                            //instruction.SetActive(true);
                            isObjectSelected = true;
                            objectSelected = selectionRenderer.gameObject;
                        }
                        else if (selectionRenderer != null && selectionRenderer.tag == "Rotatable")
                        {
                            //rotateInstruction.SetActive(true);
                            cam.GetComponentInParent<Instructions>().rotate.SetActive(true);

                            isObjectSelected = true;
                            objectSelected = selectionRenderer.gameObject;
                        }
                        else if (selectionRenderer != null && selectionRenderer.gameObject.name == "BigRedButton")
                        {
                            //pressInstruction.SetActive(true);
                            cam.GetComponentInParent<Instructions>().press.SetActive(true);

                            isObjectSelected = true;
                            objectSelected = selectionRenderer.gameObject;
                        }
                        else
                        {
                            cam.GetComponentInParent<Instructions>().screw.SetActive(false);
                            cam.GetComponentInParent<Instructions>().rotate.SetActive(false);
                            cam.GetComponentInParent<Instructions>().press.SetActive(false);
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
                                //objectSelected.transform.Rotate(0.0f, 90.0f, 0.0f, Space.Self);
                                this.photonView.RPC("rotateObject", RpcTarget.All);
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
                                    solved = true;
                                    cam.GetComponentInParent<Instructions>().screw.SetActive(false);
                                    cam.GetComponentInParent<Instructions>().rotate.SetActive(false);
                                    cam.GetComponentInParent<Instructions>().press.SetActive(false);
                                    this.photonView.RPC("blueExit", RpcTarget.All);

                                    Debug.Log("Correct");
                                }
                            }
                        }
                    }

                }
            }
        }
        [PunRPC]
        private void rotateObject()
        {
            objectSelected.transform.Rotate(0.0f, 90.0f, 0.0f, Space.Self);

        }
        [PunRPC]
        private void blueExit()
        {
            exit.SetActive(true);
            enter.GetComponent<BlueDoor>().isDone = true;


        }
    }
}
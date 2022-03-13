using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SparkyControl
{
    public class SparkyManager : MonoBehaviour
    {
        private bool isObjectSelected;
        private GameObject objectSelected;

        // Start is called before the first frame update
        void Start()
        {
            isObjectSelected = false;
        }

        // Update is called once per frame
        void Update()
        {
            RaycastHit hit;
            var ray = Camera.main.ViewportPointToRay (new Vector3(0.5f, 0.5f, 0f));

            if (Physics.Raycast(ray, out hit))
            {
                var selection = hit.transform;
                var selectionRenderer = selection.GetComponent<Renderer>();
                isObjectSelected = true;
                objectSelected = selectionRenderer.gameObject;
                //if (objectSelected.tag == "AI")
                //{
                //    Debug.Log("Sparky Selected");
                //}   
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isObjectSelected)
                {
                    if (objectSelected.tag == "AI")
                    {
                        var sparkyConvo = new SparkyControl.RasaManager();
                        sparkyConvo.SendMessageToRasa();
                        Debug.Log("Sparky Message Sent");
                    }
                }
            }
        }
    }
}

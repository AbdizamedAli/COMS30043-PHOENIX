using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SparkyControl
{
    public class SparkyManager : MonoBehaviour
    {
        private bool isObjectSelected;
        private GameObject objectSelected;
        public GameObject bot;
        public GameObject user;

        public Canvas canvas;   //the Canvas containing the UI for interacting with Sparky

        // Start is called before the first frame update
        void Start()
        {
            isObjectSelected = false;
            canvas.enabled = false;
            bot.SetActive(false);
            user.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            //Check whether player is looking at 'Sparky'/whether they are hovering over 'Sparky' with their crosshair.
            RaycastHit hit;
            var ray = Camera.main.ViewportPointToRay (new Vector3(0.5f, 0.5f, 0f));

            if (Physics.Raycast(ray, out hit))
            {
                var selection = hit.transform;
                var selectionRenderer = selection.GetComponent<Renderer>();
                isObjectSelected = true;
                objectSelected = selectionRenderer.gameObject;  
            }

            //if the player presses 'E' while hovering over 'Sparky' with their crosshair, show the canvas containing UI, unlock the cursor, and start the puzzle.
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isObjectSelected)
                {
                    if (objectSelected.tag == "AI")
                    {
                        //var sparkyConvo = gameObject.AddComponent(typeof(SparkyControl.RasaManager)) as SparkyControl.RasaManager;
                        //sparkyConvo.SendMessageToRasa();
                        Debug.Log("Sparky Message Sent");
                        canvas.enabled = true;
                        Cursor.lockState = CursorLockMode.Confined;
                    }
                }
            }
        }
    }
}

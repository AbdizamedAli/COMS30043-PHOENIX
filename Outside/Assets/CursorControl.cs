using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorControl : MonoBehaviour
{
    public bool showCursor;

    // Start is called before the first frame update
    //showCursor is initially set to false, as the cursor is only needed in the AI puzzle on floor2 - it is not required for the first section of the game.
    void Start()
    {
        showCursor = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if showCursor is set to true, then the cursor becomes unlocked (confined), so that it can be seen, and used with a UI.
        //if showCursor is set to false, then the cursor becomes locked, and the player just has a crosshair in the center of their camera view.
        if (!showCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            //Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}

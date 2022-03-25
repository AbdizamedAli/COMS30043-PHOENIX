using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSelectorr : MonoBehaviour
{
    GameObject[] row1;
    GameObject[] row2;
    GameObject[] row3;
    // Start is called before the first frame update
    void Start()
    {
        row1 = GameObject.FindGameObjectsWithTag("row_1_button");
        row2 = GameObject.FindGameObjectsWithTag("row_2_button");
        row3 = GameObject.FindGameObjectsWithTag("row_3_button");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private int indexOn(GameObject button)
    {
        return button.GetComponentInParent<FindOnButton>().indexOn();
    }
}

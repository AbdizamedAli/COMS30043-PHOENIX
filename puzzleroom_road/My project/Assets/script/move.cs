using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public bool flag = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(flag == true)
        {
            float movev = 0;
            float moveh = 0;
            if (Input.GetKey(KeyCode.UpArrow))
            {
                movev += Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                movev -= Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                moveh -= Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                moveh += Time.deltaTime;
            }
            this.transform.Translate(new Vector3(5*moveh, 0, 5*movev));
        }
    }

}

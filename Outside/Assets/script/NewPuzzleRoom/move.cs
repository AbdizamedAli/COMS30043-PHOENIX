using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public bool flag = false;
    private CharacterController Cc;
    // Start is called before the first frame update
    void Awake()
    {
        Cc = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(flag == true)
        {
            float movev = 0;
            float moveh = 0;
            if (Input.GetKey(KeyCode.I))
            {
                movev -= Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.K))
            {
                movev += Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.L))
            {
                moveh -= Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.J))
            {
                moveh += Time.deltaTime;
            }
            Cc.Move(new Vector3(5*moveh, 0, 5*movev));
        }
    }

}

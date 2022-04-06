using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class bridge : MonoBehaviour
{
    public bool flag = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            UpdateMove();
        }
        
    }
    public void UpdateMove()
    {
        float rx = Mathf.Sin(Time.time) * Time.deltaTime;
        this.transform.Translate(new Vector3(0, 0, 10*rx));
    }

}

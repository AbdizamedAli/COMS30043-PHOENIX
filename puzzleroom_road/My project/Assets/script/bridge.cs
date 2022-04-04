using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class bridge : MonoBehaviour
{
    public GameObject m_bridge;
    public GameObject m_button;
    public bool flag = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (flag == true)
        {
            UpdateMove();
        }
        
    }
    public void UpdateMove()
    {
        float rx = Mathf.Sin(Time.time) * Time.deltaTime;
        m_bridge.transform.Translate(new Vector3(0, 0, 10*rx));
    }
    void OnTriggerEnter(Collider m_button)
    {
        flag = false;
    }
    void OnTriggerExit(Collider m_button)
    {
        flag = true;
    }
}

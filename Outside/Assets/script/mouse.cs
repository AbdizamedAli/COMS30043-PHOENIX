using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse : MonoBehaviour
{
    public void Mouse()
    {
        if (Input.GetMouseButtonDown(2))//鼠标中键点击
        {
            switch (Cursor.lockState)
            {
                case CursorLockMode.None://如果显示状态
                    Cursor.lockState = CursorLockMode.Locked;//鼠标隐藏并锁定
                    break;
                case CursorLockMode.Locked://如果隐藏状态
                    Cursor.lockState = CursorLockMode.None;//鼠标显示并解锁
                    break;
                default:
                    break;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Mouse();
    }
}

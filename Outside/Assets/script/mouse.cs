using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse : MonoBehaviour
{
    public void Mouse()
    {
        if (Input.GetMouseButtonDown(2))//����м����
        {
            switch (Cursor.lockState)
            {
                case CursorLockMode.None://�����ʾ״̬
                    Cursor.lockState = CursorLockMode.Locked;//������ز�����
                    break;
                case CursorLockMode.Locked://�������״̬
                    Cursor.lockState = CursorLockMode.None;//�����ʾ������
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class FindOnButton : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject button1;
    [SerializeField] GameObject button2;
    [SerializeField] GameObject button3;
    [SerializeField] public int row;
    public int row_correct;
    public bool is_solved = false;

    // Start is called before the first frame update
    void Start()
    {
        this.photonView.RPC("setStartButtonState", RpcTarget.All);

    }

    [PunRPC]
    void setStartButtonState()
    {
        button1.GetComponent<Renderer>().material.color = Color.black;
        button2.GetComponent<Renderer>().material.color = Color.black;
        button3.GetComponent<Renderer>().material.color = Color.black;


    }

    public bool isCorrect()
    {
        return row_correct == indexOn();
    }


    public GameObject mapText(int index)
    {
        return GameObject.Find("row_" + row + "_" + index + "(Clone)");
    }

    public int getRow()
    {
        return row;
    }

    public bool hasBeenSelected()
    {
        return isOn(button1) || isOn(button2) || isOn(button3);

    }

    public int whichButton(GameObject button)
    {
        if (button.Equals(button1))
        {
            return 1;
        }
        else if (button.Equals(button2))
        {
            return 2;
        }
        else if (button.Equals(button3))
        {
            return 3;
        }
        else
        {
            return -1;
        }
    }

    public int indexOn()
    {
        if (hasBeenSelected())
        {
            if (isOn(button1))
            {
                return 1;
            }
            else if (isOn(button2))
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }
        else
        {
            return -1;
        }
    }

    public GameObject onButton()
    {
        if (hasBeenSelected())
        {
            if (isOn(button1))
            {
                return button1;
            }
            else if (isOn(button2))
            {
                return button2;
            }
            else
            {
                return button3;
            }
        }
        else
        {
            return null;
        }
    }

    private bool isOn(GameObject button)
    {
        return button.GetComponent<Renderer>().material.color == Color.red;
    }

}

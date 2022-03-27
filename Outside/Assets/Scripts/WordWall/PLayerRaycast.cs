using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class PLayerRaycast : MonoBehaviourPunCallbacks
{
    public float rayRange = 4;
    [SerializeField] Camera cam;
    GameObject text;
    GameObject buttonOff;
    GameObject buttonOn;
    GameObject hitss;


    void Update()
    {

        if (!photonView.IsMine)
        {
            return;
        }
        this.photonView.RPC("CastRay", RpcTarget.All);
    }

    [PunRPC]
    void CastRay()
    {
        if (cam  == null)
        {
            return;
        }
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hitInfo = new RaycastHit();
        bool hit = Physics.Raycast(ray, out hitInfo, rayRange);
        if (hit && hitInfo.transform.gameObject.layer == 7)
        {
            GameObject hitObject = hitInfo.transform.gameObject;
            if (Input.GetMouseButtonDown(0) && !hitObject.GetComponentInParent<FindOnButton>().is_solved)
            {
                if (hitObject.GetComponentInParent<FindOnButton>().hasBeenSelected())
                {
                    int index = hitObject.GetComponentInParent<FindOnButton>().indexOn();
                    int row = hitObject.GetComponentInParent<FindOnButton>().getRow();
                    this.photonView.RPC("setTextWallTextOff", RpcTarget.All,row ,index);
                    this.photonView.RPC("setButtonOffTextWall", RpcTarget.All,row,index);
                }

                    this.photonView.RPC("setButtonOnTextWall", RpcTarget.AllBuffered, hitObject.GetComponentInParent<FindOnButton>().getRow(), hitObject.GetComponentInParent<FindOnButton>().whichButton(hitObject));
                    this.photonView.RPC("setTextWallTextOn", RpcTarget.All, hitObject.GetComponentInParent<FindOnButton>().getRow(), hitObject.GetComponentInParent<FindOnButton>().whichButton(hitObject));
                
            }

        }

        if (hit && hitInfo.transform.gameObject.layer == 8)
        {
            GameObject hitObject = hitInfo.transform.gameObject;
            FindOnButton row_1 = GameObject.Find("row1(Clone)").GetComponent<FindOnButton>();
            FindOnButton row_2 = GameObject.Find("row2(Clone)").GetComponent<FindOnButton>();
            FindOnButton row_3 = GameObject.Find("row3(Clone)").GetComponent<FindOnButton>();

            if (Input.GetMouseButtonDown(0) && row_1.hasBeenSelected())
            {

                if (row_1.isCorrect() && row_2.isCorrect() && row_3.isCorrect())
                {
                    int index_1 = row_1.indexOn();
                    int index_2 = row_2.indexOn();
                    int index_3 = row_3.indexOn();
                    this.photonView.RPC("setWallbuttonSetSolve", RpcTarget.All,index_1,index_2,index_3);
                    this.photonView.RPC("thisThing", RpcTarget.All);
                }
                else
                {
                    int index_1 = row_1.indexOn();
                    int index_2 = row_2.indexOn();
                    int index_3 = row_3.indexOn();

                    this.photonView.RPC("setTextWallTextOff", RpcTarget.All, 1, index_1);
                    this.photonView.RPC("setTextWallTextOff", RpcTarget.All, 2, index_2);
                    this.photonView.RPC("setTextWallTextOff", RpcTarget.All, 3, index_3);
                    this.photonView.RPC("setButtonOffTextWall", RpcTarget.All, 1, index_1);
                    this.photonView.RPC("setButtonOffTextWall", RpcTarget.All, 2, index_2);
                    this.photonView.RPC("setButtonOffTextWall", RpcTarget.All, 3, index_3);
                    this.photonView.RPC("updateTimerWrongCombination", RpcTarget.All);

                }

            }

        }
    }
    [PunRPC]
    void setTextWallTextOff(int row, int index)
    {
        GameObject.Find("row_" + row + "_" + index + "(Clone)").GetComponent<TextMeshPro>().color = Color.black;
    }

    [PunRPC]
    void setButtonOffTextWall(int row, int index)
    {
        GameObject.Find("button_" + row + "_" + index).GetComponent<Renderer>().material.color = Color.black;
    }

    [PunRPC]
    void setTextWallTextOn(int row, int index)
    {

        GameObject.Find("row_" + row + "_" + index + "(Clone)").GetComponent<TextMeshPro>().color = Color.red;
    }

    [PunRPC]
    void setButtonOnTextWall(int row, int index)
    {
        GameObject.Find("button_" + row + "_" + index).GetComponent<Renderer>().material.color = Color.red;
    }


    [PunRPC]
    void thisThing()
    {
        GameObject.Find("Cube (14)PlusONeONe").GetComponent<Animator>().Play("next_level_door_open");

    }

    [PunRPC]
    void updateTimerWrongCombination()
    {
        GameObject[] timers = GameObject.FindGameObjectsWithTag("timer");

        foreach (var item in timers)
        {
            item.GetComponent<Timer>().startTime -= (double)60;
        }
    }

    [PunRPC]
    void setWallbuttonSetSolve(int i, int j, int k)
    {
        GameObject.Find("ENTER").GetComponent<Renderer>().material.color = Color.green;
        GameObject.Find("row1(Clone)").GetComponent<FindOnButton>().is_solved = true;
        GameObject.Find("row2(Clone)").GetComponent<FindOnButton>().is_solved = true;
        GameObject.Find("row3(Clone)").GetComponent<FindOnButton>().is_solved = true;

        GameObject.Find("row1(Clone)").GetComponent<FindOnButton>().onButton().GetComponent<Renderer>().material.color = Color.green;
        GameObject.Find("row2(Clone)").GetComponent<FindOnButton>().onButton().GetComponent<Renderer>().material.color = Color.green; 
        GameObject.Find("row3(Clone)").GetComponent<FindOnButton>().onButton().GetComponent<Renderer>().material.color = Color.green;

        GameObject.Find("row1(Clone)").GetComponent<FindOnButton>().mapText(i).GetComponent<TextMeshPro>().color = Color.green;
        GameObject.Find("row2(Clone)").GetComponent<FindOnButton>().mapText(j).GetComponent<TextMeshPro>().color = Color.green;
        GameObject.Find("row3(Clone)").GetComponent<FindOnButton>().mapText(k).GetComponent<TextMeshPro>().color = Color.green;
    }


}

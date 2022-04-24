using UnityEngine;
using Photon.Pun;
using Peer;
using System;
using System.Collections.Generic;

public class AudioChat : MonoBehaviourPunCallbacks
{
    private bool connected = false;
    private float max_volume = 1f;

    void Start()
    {
        if (!photonView.IsMine) return;
        PeerjsWrapper.Instance.code_stack += checkConected;
        checkConected(PeerjsWrapper.Instance.Connection);

    }

    private void Update()
    {
        UpdateVolume();
    }
    private float getVolume(float distance)
    {
        return Math.Min(max_volume, max_volume / distance);
    }

    private void checkConected(Code code)
    {
        if (code == Code.ConnectionOn)
        {
            connected = true;
        }
    }

    private void UpdateVolume()
    {
        if (!photonView.IsMine) return;

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            if (!player.GetPhotonView().IsMine)
            {
                float distance = Vector3.Distance(transform.position, player.transform.position);
                float new_volume = getVolume(distance);
                if (0f <= new_volume && new_volume <= 1f && connected == true)
                {
                    string ID = player.GetComponent<ConnectMicrophone>().ID;
                    PeerjsWrapper.Instance.sendVolume(new_volume, ID);

                }
            }
        }  
    }

}

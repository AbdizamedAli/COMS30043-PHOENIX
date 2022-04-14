using UnityEngine;
using Photon.Pun;
using Peer;
using System;

public class AudioChat : MonoBehaviourPunCallbacks
{
    private GameObject target_player = null;

    private float min_distance = 1f;
    private float max_distance = 20f;
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

    private void UpdateVolume()
    {
        if (!photonView.IsMine) return;

        if (target_player != null)
        {
            float distance = Vector3.Distance(transform.position, target_player.transform.position);
            float new_volume = getVolume(distance);
            if (0f <= new_volume && new_volume <= 1f)
            {
                PeerjsWrapper.Instance.sendVolume(new_volume);
            }
        }
    }

    private void checkConected(Code code)
    {
        if (code == Code.ConnectionOn)
        {
            setPlayer();

        }
        
    }

    private void setPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length > 1)
        {

            foreach (GameObject player in players)
            {
                if (!player.GetPhotonView().IsMine)
                {
                    target_player = player;
                }
            }
        }
        else
        {
            Invoke(nameof(setPlayer), 1);
        }
    }
}

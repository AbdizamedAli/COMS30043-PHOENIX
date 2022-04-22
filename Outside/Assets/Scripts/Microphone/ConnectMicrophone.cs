using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using FrostweepGames.Plugins.Native;
using Peer;


public class ConnectMicrophone : MonoBehaviourPunCallbacks
{
    private string id = "";
    private string client_id = "";
    private string _microphoneDevice;

    public string ID
    {
        get
        {
            return id;
        }
    }

    void Start()
    {
        if (!photonView.IsMine) return;

        if (!CustomMicrophone.HasConnectedMicrophoneDevices())
        {
            CustomMicrophone.RefreshMicrophoneDevices();
        }
        Debug.Log(CustomMicrophone.devices.Length + " microphone devices found");

        if (!CustomMicrophone.HasConnectedMicrophoneDevices())
        {
            Debug.Log("no microphone device connected");
        }

        _microphoneDevice = CustomMicrophone.devices[0];
        CustomMicrophone.Start(_microphoneDevice, true, 1, 44100);


        PeerjsWrapper.Instance.id_stack += trackID;
        PeerjsWrapper.Instance.createPeer();
    }

    void getClientID(string ID)
    {
        client_id = ID;
        PeerjsWrapper.Instance.Call(client_id);
    }

    [PunRPC]
    void getClientIdRpc(string ID)
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player.GetPhotonView().IsMine)
            {
                ConnectMicrophone mic = player.GetComponent<ConnectMicrophone>();
                mic.getClientID(ID);
            }
        }
    }

    void trackID(string ID)
    {
        photonView.RPC(nameof(setPlayerIdPeerJS), RpcTarget.All, ID);
        if (!PhotonNetwork.IsMasterClient)
        {
            photonView.RPC(nameof(getClientIdRpc), RpcTarget.Others, ID);
        }
    }
    [PunRPC]
    void setPlayerIdPeerJS(string id_peer)
    {
        id = id_peer;
    }


}

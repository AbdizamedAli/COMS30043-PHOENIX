using System.Collections;
using System.Collections.Generic;
using FrostweepGames.VoicePro;
using UnityEngine;
using Photon.Pun;
using FrostweepGames.Plugins.Native;

public class SoundInput : MonoBehaviour
{
    public Recorder recorder;
    public Listener listener;
    //public Speaker speaker;
    public bool IsRecording;
    //NetworkActorInfo info;
    // Start is called before the first frame update
    void Start()
    {
        recorder.RefreshMicrophones();
        //speaker.SetObjectOwner(GameObject.Find("Resources/PhotonPrefabs/PlayerController"));
        //    //int id = Random.Range(0, 100);

        //    info.name = "1";
        //    info.id = "1";
        //    //info.id = PhotonNetwork.LocalPlayer.UserId;
        //    NetworkRouter.Instance.Register(info);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !IsRecording)
        {
            StartRecord();
        }

        else if (Input.GetKeyUp(KeyCode.R) && IsRecording)
        {
            StopRecord();
        }
    }

    public void StartRecord()
    {
        if (CustomMicrophone.HasConnectedMicrophoneDevices())
        {
            recorder.SetMicrophone(CustomMicrophone.devices[0]);
            IsRecording = recorder.StartRecord();

            Debug.Log("Record started: " + IsRecording);
        }
        else
        {
            recorder.RefreshMicrophones();
        }
    }


    public void StopRecord()
    {
        recorder.StopRecord();
        IsRecording = false;
    }
}

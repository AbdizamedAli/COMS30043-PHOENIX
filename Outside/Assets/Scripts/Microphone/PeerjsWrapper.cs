using System.Runtime.InteropServices;
using System;
using System.Linq;
using System.Globalization;
using UnityEngine;
using System.Collections.Generic;

public enum Code
{
    ConnectionOn,
    ConnectionOff
}

namespace Peer
{
    public class PeerjsWrapper : MonoBehaviour
    {
        #region __Internal
        [DllImport("__Internal")]
        private static extern void connectToPeer();

        [DllImport("__Internal")]
        private static extern void startConnection(string foreignID);

        [DllImport("__Internal")]
        private static extern void startCall(string foreignID);

        [DllImport("__Internal")]
        private static extern void updateVolume(float newVolume);
        #endregion

        private static PeerjsWrapper _Instance;
        public static PeerjsWrapper Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new UnityEngine.GameObject("Voice_Audio_PeerJS").AddComponent<PeerjsWrapper>();

                return _Instance;
            }
        }

        public Code Connection 
        {
            get
            {
                return new_code;
            }
        }

        private Code new_code;
        private string peer_ID;
        private string client_ID;
        private bool connected;
        private bool called;

        // Called when the peer receives an id
        public Action<string> id_stack;
        public Action<Code> code_stack;


        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);

            new_code = Code.ConnectionOff;
            peer_ID = "";
            client_ID = "";
            connected = false;
            called = false;
        }

        public void createPeer()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            if(new_code != Code.ConnectionOn)
            {
                connectToPeer();
            }
#endif
        }

        public void Connect(string ci)
        {
            client_ID = ci;
            connected = true;
#if UNITY_WEBGL && !UNITY_EDITOR
            if (peer_ID != "")
            {
                startConnection(client_ID);
            }
#endif
        }

        public void Call(string ci)
        {
            client_ID = ci;
            called = true;
#if UNITY_WEBGL && !UNITY_EDITOR
            if (peer_ID != "")
            {
                startCall(client_ID);
            }
#endif
        }

        public void sendVolume(float volume)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            updateVolume(volume);
#endif
        }

        private void updateCode(int nc)
        {
            new_code = (Code)nc;

            Debug.Log("Code is " + new_code);
            code_stack?.Invoke(new_code);
        }

        private void sendClientId(string ID)
        {
            peer_ID = ID;
            id_stack?.Invoke(peer_ID);

#if UNITY_WEBGL && !UNITY_EDITOR
            if(client_ID != "")
            {
                if(connected)
                    startConnection(client_ID);

                if(called)
                    startCall(client_ID);
            }
#endif
        }
    }
}

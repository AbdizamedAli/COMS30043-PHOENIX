using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InRoom : MonoBehaviourPunCallbacks
{

    public GameObject playerInfoPrefab;//??????????????
    public Transform conlayout;//??????
    List<GameObject> mplayers = new List<GameObject>();
    Player[] allplayers;

    bool ready = false;
    PhotonView photonView;
    public InputField contentInput;//??????????
    public GameObject textPrefab;//??????????
    public Transform layoutContent;//??????
    void Start()
    {
        photonView = GetComponent<PhotonView>();

    }
    /// <summary>
    /// ????????????
    /// </summary>
    public override void OnJoinedRoom()
    {


        allplayers = PhotonNetwork.PlayerList;
        foreach (var item in allplayers)
        {

            GameObject obj = Instantiate(playerInfoPrefab, conlayout);

            m_PlayerInfo _PlayerInfo = obj.GetComponent<m_PlayerInfo>();
            _PlayerInfo.playerName = item.NickName;
            obj.GetComponentInChildren<Text>().text = item.NickName + "(" + "Not Ready" + ")";
            mplayers.Add(obj);
        }
    }

    /// <summary>
    /// ????????????????????????????
    /// </summary>
    /// <param name="newPlayer"></param>
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {

        GameObject obj = Instantiate(playerInfoPrefab, conlayout);
        m_PlayerInfo _PlayerInfo = obj.GetComponent<m_PlayerInfo>();
        _PlayerInfo.playerName = newPlayer.NickName;
        obj.GetComponentInChildren<Text>().text = newPlayer.NickName + "(" + "Not Ready" + ")";
        mplayers.Add(obj);
    }
    /// <summary>
    /// ????????????????????????????
    /// </summary>
    /// <param name="otherPlayer"></param>
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {

        foreach (var item in mplayers)
        {

            if (item.GetComponentInChildren<Text>().text.Contains(otherPlayer.NickName))
            {


                Destroy(item);
                break;
            }
        }
    }
    /// <summary>
    /// ??????????????????????????????????????????????????
    /// </summary>
    public override void OnLeftRoom()
    {

        foreach (var item in mplayers)
        {

            if (item.GetComponentInChildren<Text>().text.Contains(PhotonNetwork.LocalPlayer.NickName))
            {

                if (PhotonNetwork.LocalPlayer.IsMasterClient)
                {

                    foreach (var p in allplayers)
                    {

                        if (p != PhotonNetwork.LocalPlayer)
                        {

                            PhotonNetwork.CurrentRoom.SetMasterClient(p);
                            break;
                        }
                    }

                }

                Destroy(item);
                break;
            }
        }
    }


    Dictionary<string, bool> IsPlayerReady = new Dictionary<string, bool>();
    /// <summary>
    /// </summary>
    public void SetReadyBtn()
    {

        foreach (var item in mplayers)
        {

            m_PlayerInfo playerinfo = item.GetComponent<m_PlayerInfo>();
            if (!IsPlayerReady.ContainsKey(playerinfo.playerName))
            {

                IsPlayerReady.Add(playerinfo.playerName, false);
            }
        }
        ready = !ready;
        foreach (var item in IsPlayerReady)
        {

            if (item.Key == PhotonNetwork.LocalPlayer.NickName)
            {

                IsPlayerReady[item.Key] = ready;
                break;
            }
        }
        photonView.RPC("ReadyBtn", RpcTarget.All, PhotonNetwork.LocalPlayer.NickName, IsPlayerReady);
    }

    [PunRPC]
    void ReadyBtn(string xname, Dictionary<string, bool> keyValuePairs)
    {


        foreach (var item in keyValuePairs)
        {

            if (item.Value == true)
            {

                foreach (var p in mplayers)
                {

                    if (p.GetComponentInChildren<Text>().text.Contains(item.Key))
                    {

                        p.GetComponentInChildren<Text>().text = item.Key + "(" + "Ready" + ")";
                        break;
                    }
                }
            }
            else
            {

                foreach (var p in mplayers)
                {

                    if (p.GetComponentInChildren<Text>().text.Contains(item.Key))
                    {

                        p.GetComponentInChildren<Text>().text = item.Key + "(" + "Not Ready" + ")";
                        break;
                    }
                }
            }
        }

        //foreach (var item in mplayers)
        //{

        //    Debug.Log(item.GetComponentInChildren<Text>().text);
        //    if (item.GetComponentInChildren<Text>().text.Contains(xname))
        //    {

        //        if(ready)
        //        {

        //          item.GetComponentInChildren<Text>().text= xname + "(" + "??????" + ")";
        //        }
        //        else
        //        {

        //            item.GetComponentInChildren<Text>().text = xname + "(" + "??????" + ")";
        //        }
        //        break;
        //    }
        //}
    }


    //????????
    public void SendMessInfoBtn()
    {
        string info = PhotonNetwork.LocalPlayer.NickName + " :" + contentInput.text;
        photonView.RPC("SendMess", RpcTarget.All, info);
    }

    [PunRPC]
    void SendMess(string mess)
    {
        GameObject textobj = Instantiate(textPrefab, layoutContent);
        textobj.GetComponentInChildren<Text>().text = mess;
    }

    //????????
    public void StartGameButton()
    {

        photonView.RPC("LoadGameScene", RpcTarget.All);
    }


    [PunRPC]
    void LoadGameScene()
    {
        PhotonNetwork.LoadLevel("PuzzleRoom01");
    }
}

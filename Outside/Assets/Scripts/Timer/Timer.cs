using UnityEngine;
using Photon.Pun;
using TMPro;

public class Timer : MonoBehaviourPunCallbacks
{
    bool startTimer = false;
    public double timerIncrementValue;
    public double startTime;
    [SerializeField] double timer = 600;
    ExitGames.Client.Photon.Hashtable CustomeValue;

    private void Awake()
    {
        
    }

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            CustomeValue = new ExitGames.Client.Photon.Hashtable();
            startTime = PhotonNetwork.Time;
            startTimer = true;
            CustomeValue.Add("StartTime", startTime);
            PhotonNetwork.CurrentRoom.SetCustomProperties(CustomeValue);
        }
        else
        {
            if (PhotonNetwork.CurrentRoom.CustomProperties["StartTime"] == null)
            {
                return;
            }
            startTime = double.Parse(PhotonNetwork.CurrentRoom.CustomProperties["StartTime"].ToString());
            startTimer = true;
        }

    }

    void Update()
    {
        if (!startTimer) return;
        timerIncrementValue = PhotonNetwork.Time - startTime;

        float minutes = Mathf.FloorToInt(10 - (float)timerIncrementValue / 60);
        float seconds = Mathf.FloorToInt(60 -(float)timerIncrementValue % 60); 


        if (timerIncrementValue == 0)
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = string.Format("{0:00}:{1:00}", 10, 0);
        }
        gameObject.GetComponent<TextMeshProUGUI>().text = string.Format("{0:00}:{1:00}", minutes,seconds);
        if (timerIncrementValue >= timer)
        {
            //Timer Completed
            //Do What Ever You What to Do Here
        }
    }
}

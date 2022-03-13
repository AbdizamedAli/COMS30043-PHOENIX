using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace SparkyControl
{
    public class PostMessageJson
    {
        public string message;
        public string sender;
    }

    public class RasaManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private const string rasa_url = "http://localhost:5005/webhooks/rest/webhook";

        public void SendMessageToRasa()
        {
            // Create a json object from user message
            PostMessageJson postMessage = new PostMessageJson
            {
                sender = "user",
                message = "Hi"
            };

            string jsonBody = JsonUtility.ToJson(postMessage);
            print("User json : " + jsonBody);

            // Create a post request with the data to send to Rasa server
            StartCoroutine(PostRequest(rasa_url, jsonBody));
        }

        private IEnumerator PostRequest(string url, string jsonBody)
        {
            UnityWebRequest request = new UnityWebRequest(url, "POST");
            byte[] rawBody = new System.Text.UTF8Encoding().GetBytes(jsonBody);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(rawBody);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            Debug.Log("Response: " + request.downloadHandler.text);
        }
    }
}
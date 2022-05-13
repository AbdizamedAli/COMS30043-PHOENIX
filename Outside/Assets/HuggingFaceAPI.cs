using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

using SimpleJSON;
using MiniJSON;
using System.Linq;

//Based on: https://thomassimonini.medium.com/building-a-smart-robot-ai-using-hugging-face-and-unity-a78724810545

/// <summary>
/// This script handles:
///     - HFRankOrders_(string source_sentence): the post request to the API: given an user input score each action in the emotion list.
///     - ProcessResult(string result): given a string result, convert it to an array of floats and find the max score and max score index.
///     - Start(): to build the JSON correctly, we need to make a string array of all robot sentences;
/// </summary>
public class HuggingFaceAPI : MonoBehaviour
{
    [Header("HuggingFace Model URL")]
    public string model_url;

    [Header("HuggingFace Key API")]
    public string hf_api_key; // DO NOT SHARE YOUR PROJECT IF YOU DEFINED YOUR API KEY

    [HideInInspector]
    public string source_sentence; // User input text

    [HideInInspector]
    public List<string> sentences; // Robot list of sentences (actions)

    [HideInInspector]
    public float maxScore; // Value of the emotion with the highest score

    [HideInInspector]
    public int maxScoreIndex; // Index of the emotion with the highest score

    [Header("Jammo Behavior")]
    public AI_Behaviour jammo; // A reference to AI_Behavior Script

    void Start()
    {
        // To prepare the JSON, we take all the emotions candidates
        foreach (AI_Behaviour.PlayerEmotions emotions in jammo.playerEmotionsList)
        {
            sentences.Add(emotions.emotion);
        }
    }

    /// <summary>
    /// Given a user input text and a set of emotion candidates, call HF model to score each of them.
    /// </summary>
    /// <param name="source_sentence">user input sentence</param>
    public void HFRankOrders_(string source_sentence)
    {
        StartCoroutine(HFScore(source_sentence));
    }

    public IEnumerator HFScore(string prompt)
    {
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(prompt);

        // Make the web request
        var request = new UnityWebRequest(model_url, "POST");
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bytes);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.Send();

        Debug.Log("Status Code: " + request.responseCode);
        Debug.Log(request.downloadHandler.text);

        // If the request return an error set the error on console.
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
            Debug.Log(request.downloadHandler.data);
            yield return request.error;
        }
        else
        {
            JSONNode data = request.downloadHandler.text;
            // Process the result
            yield return ProcessResult(data);
        }

    }

    /// <summary>
    /// We receive a string, we need to process this data to transform it to an array of floats
    /// </summary>
    /// <param name="result">json return from API call</param>
    private IEnumerator ProcessResult(string result)
    {
        string cleanedResult = result.Replace("[", "");
        cleanedResult = cleanedResult.Replace("]", "");
        cleanedResult = cleanedResult.Replace("{", "");
        cleanedResult = cleanedResult.Replace("}", "");

        // Then, we need to split each element of the array and convert to float
        string[] splitArray = cleanedResult.Split(char.Parse(",")).ToArray();
        float[] values = new float[6];
        int arrayCounter = 0;
        //float[] myFloat = splitArray.Select(float.Parse).ToArray();
        for (int i = 0; i < 12; i++)
        {
            if (i % 2 != 0)
            {
                values[arrayCounter] = float.Parse(splitArray[i].Remove(0, 8));
                arrayCounter++;
            }
        }

        // Now that we have a array of floats, we can find the max score and the index of it.
        // We don't need to return these 2 variables, we'll access them directly since they are public.
        maxScore = values.Max();
        maxScoreIndex = values.ToList().IndexOf(maxScore);

        Debug.Log(maxScore);
        Debug.Log(maxScoreIndex);

        yield return null;
    }
}
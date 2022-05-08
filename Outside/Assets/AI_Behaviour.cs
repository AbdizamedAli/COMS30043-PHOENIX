using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

/// <summary>
/// This class is used to control the behavior of our Robot by calling the HuggingFaceAPI instance.
/// </summary>
public class AI_Behaviour : MonoBehaviour
{
    /// <summary>
    /// The Robot Action List
    /// </summary>
    [System.Serializable]
    public struct PlayerEmotions
    {
        public string emotion;
    }

    /// <summary>
    /// Enum of the different possible states of our Robot
    /// </summary>
    private enum State
    {
        Idle,
        //TODO: Define the other states
        sadness,
        joy,
        love,
        anger,
        fear,
        surprise
    }

    public EmotBotUI botUI;

    [Header("Robot list of actions")]
    public List<PlayerEmotions> playerEmotionsList;

    [Header("HuggingFace API")]
    public HuggingFaceAPI hfAPI;

    [Header("Input UI")]
    public InputField inputField;     // Our Input Field UI

    private State state;

    public string AIState;

    private string target;
    private int riddleNumber = 0;

    private void Awake()
    {
        // Set the State to Idle
        state = State.Idle;
    }

    void MakeMe()
    {
        target = "happy";
        var message = "make me " + target;
        botUI.UpdateDisplay("bot", message);
    }

    void CorrectEmotion(string emotion)
    {
        if (emotion == target)
        {
            botUI.UpdateDisplay("bot", "Well done. You made me " + target);
            botUI.UpdateDisplay("bot", "Here is a riddle for you:");
        }
        else
        {
            botUI.UpdateDisplay("bot", "That wasn't quite right. You were supposed to make me " + target);
            botUI.UpdateDisplay("bot", "Try again.");
        }
    }

    void GiveRiddle()
    {
        switch (riddleNumber)
        {
            default:
            case 0:
                botUI.UpdateDisplay("bot", "I'm in you, but not in him.");
                botUI.UpdateDisplay("bot", "I go up, but not down");
                botUI.UpdateDisplay("bot", "I'm in the colosseum, but not in a tower.");
                botUI.UpdateDisplay("bot", "I'm in a puzzle, but not a riddle.");
                botUI.UpdateDisplay("bot", "What am I?");
                break;
            case 1:
                botUI.UpdateDisplay("bot", "Walk on the living, they don't even mumble.");
                botUI.UpdateDisplay("bot", "Walk on the dead, they mutter and grumble.");
                break;
            case 2:
                botUI.UpdateDisplay("bot", "What kind of room has no doors or windows?");
                break;
        }
    }

    public void CheckRiddle(string answer)
    {
        string[] answers = { "U", "leaf", "mushroom" };
        var correctAnswer = answers[riddleNumber];
        if (answer == correctAnswer)
        {
            botUI.UpdateDisplay("bot", "Your friend answered the riddle correctly.");
            riddleNumber++;
        }
        else
        {
            botUI.UpdateDisplay("bot", "Your friend clearly doesn't know what they're doing.");
            botUI.UpdateDisplay("bot", "Tell them to give it another go...");
        }
        if (riddleNumber == 3)
        {
            //puzzle finished
        }
    }

    /// <summary>
    /// Utility function: Given the results of HuggingFace API, select the State with the highest score
    /// </summary>
    /// <param name="maxValue">Value of the option with the highest score</param>
    /// <param name="maxIndex">Index of the option with the highest score</param>
    private void Utility(float maxScore, int maxScoreIndex)
    {
        // TODO: Define the Utilitary function
        //goalObject = GameObject.Find(actionsList[maxScoreIndex].noun);

        string emotion = playerEmotionsList[maxScoreIndex].emotion;

        state = (State)System.Enum.Parse(typeof(State), emotion, true);
    }

    /// <summary>
    /// When the user finished to type the order
    /// </summary>
    /// <param name="prompt"></param>
    public void UISend(string prompt)
    {
        // Get the input text
        prompt = inputField.text;
        inputField.text = "";
        botUI.UpdateDisplay("user", prompt);
        // Call the Corountine UISend_
        StartCoroutine(UISend_(prompt));
    }
    public IEnumerator UISend_(string prompt)
    {
        // Ask HuggingFace API to rank the orders
        yield return hfAPI.HFScore(prompt);

        Utility(hfAPI.maxScore, hfAPI.maxScoreIndex);
        //Debug.Log(hfAPI.maxScore);
        AIStateFromState();
        Behaviour();
        yield return null;
    }

    public void AIStateFromState()
    {
        switch (state)
        {
            default:
            case State.Idle:
                //Debug.Log("STATE IDLE");
                AIState = "neutral";
                break;

            case State.sadness:
                //Debug.Log("STATE SADNESS");
                AIState = "sad";
                break;

            case State.joy:
                //Debug.Log("STATE JOY");
                AIState = "happy";
                break;

            case State.love:
                //Debug.Log("STATE LOVE");
                AIState = "happy";
                break;

            case State.anger:
                //Debug.Log("STATE ANGER);
                AIState = "sad";
                break;

            case State.fear:
                //Debug.Log("STATE FEAR");
                AIState = "sad";
                break;

            case State.surprise:
                //Debug.Log("STATE SURPRISE");
                AIState = "happy";
                break;
        }
    }

    public void Behaviour()
    {
        var rnd = Random.Range(0, 3);
        string[] happyChat = { "That was a nice thing to say. that makes me happy.", "I'm glad to hear it.", "Hearing you say that makes me happy", "That's great." };
        string[] sadChat = { "Well, that's not a very nice thing to say", "That makes me sad", "How could you say such a thing?", "How rude. Mother didn't raise you to say such things." };
        string chosenPhrase;

        switch (AIState)
        {
            default:
            case "happy":
                chosenPhrase = happyChat[rnd];
                botUI.UpdateDisplay("bot", chosenPhrase);
                break;
            case "sad":
                chosenPhrase = sadChat[rnd];
                botUI.UpdateDisplay("bot", chosenPhrase);
                break;
        }
    }

    private void Update()
    {


    }
}
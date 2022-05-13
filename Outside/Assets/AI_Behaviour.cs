using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Photon.Pun;

//Based on: https://thomassimonini.medium.com/building-a-smart-robot-ai-using-hugging-face-and-unity-a78724810545

/// <summary>
/// This class is used to control the behavior of our 'AI' by calling the HuggingFaceAPI instance.
/// </summary>
public class AI_Behaviour : MonoBehaviourPunCallbacks
{
    /// <summary>
    /// The player emotions list
    /// </summary>
    [System.Serializable]
    public struct PlayerEmotions
    {
        public string emotion;
    }

    /// <summary>
    /// Enum of the different possible emotions presented by player.
    /// </summary>
    private enum State
    {
        Idle,
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
    
    [SerializeField] CursorControl cursorControl;   //object containing script that controls whether cursor is visible.

    private State state;

    public string AIState;

    private string target;
    private int riddleNumber = 0;
    private string mode;    //keeps track of which player should be currently interacting with the puzzle - the one solving riddles ("riddle"), or the one talking to the AI ("emotion")

    public FloorManagerTwo floorManagerTwo; //object containing script that keeps track of which rooms are completed.

    [SerializeField] GameObject exit1;
    [SerializeField] GameObject exit2;
    [SerializeField] GameObject enter;

    private void Awake()
    {
        floorManagerTwo = GameObject.FindObjectOfType<FloorManagerTwo>();
        
        //call RPC hideExit, to hide the exit doors.
        this.photonView.RPC("hideExit", RpcTarget.All);

        // Set the State to Idle
        state = State.Idle;

        //Set the initial game mode to 'emotion' - players must elicit AI emotion first, to receive first riddle.
        mode = "emotion";

        //Send initial introduction messages from 'AI' to player. Introduce the puzzle.
        botUI.UpdateDisplay("bot", "Hello. Welcome to the emotional intelligence test.");
        botUI.UpdateDisplay("bot", "I suggest you have a chat with your friend and figure out what to do.");

        //Call MakeMe() function to start the puzzle.
        MakeMe();
    }

    //Sends a message to the player telling them to make the 'AI' happy or sad. Sets the 'target' emotion to either 'happy' or 'sad'.
    void MakeMe()
    {
        switch (riddleNumber)
        {
            default:
            case 0:
                target = "happy";
                break;
            case 1:
                target = "sad";
                break;
            case 2:
                target = "happy";
                break;
        }


        //Call botUI.UpdateDisplay to diplay "make me + emotion" message on UI.
        var message = "make me " + target;
        botUI.UpdateDisplay("bot", message);
    }

    //Checks whether the emotion that the player elicited in the 'AI' is equal to the target emotion.
    //If the correct emotion was elicited, mode is changed to "riddle", and GiveRiddle is called to start the riddle portion of the puzzle.
    //If the wrong emotion is elicited, the 'AI' asks the players to try again.
    void CorrectEmotion(string emotion)
    {
        //Checks whether the player is supposed to be messaging the AI at this point in the puzzle.
        if (mode == "emotion")
        {
            if (emotion == target)
            {
                botUI.UpdateDisplay("bot", "Well done. You made me " + target);
                botUI.UpdateDisplay("bot", "Here is a riddle for you:");
                mode = "riddle";
                GiveRiddle();
            }
            else
            {
                botUI.UpdateDisplay("bot", "That wasn't quite right. You were supposed to make me " + target);
                botUI.UpdateDisplay("bot", "Try again.");
            }
        }       
    }

    //Calls botUI.UpdateDisplay to display the next riddle on the UI, depending on the current riddleNumber.
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

    //Calls the RPC checkRiddle to check whether the answer selected by the second player was the correct answer to the current riddle.
    public void CheckRiddle(string answer)
    {
        this.photonView.RPC("checkRiddle", RpcTarget.All, answer);
    }

    /// <summary>
    /// Utility function: Given the results of HuggingFace API, select the State with the highest score
    /// </summary>
    /// <param name="maxValue">Value of the option with the highest score</param>
    /// <param name="maxIndex">Index of the option with the highest score</param>
    private void Utility(float maxScore, int maxScoreIndex)
    {
        string emotion = playerEmotionsList[maxScoreIndex].emotion;

        state = (State)System.Enum.Parse(typeof(State), emotion, true);
    }

    /// <summary>
    /// When the user finished to type
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

    //Translates the detected player input emotion, to the emotion elicited in the 'AI'.
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

    //defines how the 'AI' will behave dependin on whether the player has made it happy or sad.
    //response is chosen randomly from a list of phrases to create more natural dialogue.
    public void Behaviour()
    {
        var rnd = Random.Range(0, 3);
        string[] happyChat = { "That was a nice thing to say. that makes me happy.", "I'm glad to hear it.", "Hearing you say that makes me happy", "That's great." };
        string[] sadChat = { "Well, that's not a very nice thing to say", "That makes me sad", "How could you say such a thing?", "How rude. Mother didn't raise you to say such things." };
        string chosenPhrase;

        //If the player made the AI happy, then it responds with a phrase from the list of happy responses. It then checks whether happy was the correct target emotion.
        //If the player made the AI sad, then it repsonds with a phrase from the list of sad responses. It then checks whether sad was the correct target emotion.
        switch (AIState)
        {
            default:
            case "happy":
                chosenPhrase = happyChat[rnd];
                botUI.UpdateDisplay("bot", chosenPhrase);
                CorrectEmotion("happy");
                break;
            case "sad":
                chosenPhrase = sadChat[rnd];
                botUI.UpdateDisplay("bot", chosenPhrase);
                CorrectEmotion("sad");
                break;
        }
    }

    private void Update()
    {


    }

    //hides the exit doors.
    [PunRPC]
    private void hideExit()
    {
        exit1.SetActive(false);
        exit2.SetActive(false);
    }

    //reveals the exit doors to players, so they can leave the puzzle room.
    //tells floorManagerTwo that the puzzle has been completed.
    [PunRPC]
    private void showExit()
    {
        exit1.SetActive(true);
        exit2.SetActive(true);
        enter.GetComponent<AIDoor>().isDone = true;
        floorManagerTwo.PuzzleComplete();
    }

    //checks whether the answer selected by the player is the correct answer to the current riddle.
    //If the correct answer was selected, then the AI lets the players know, and the game mode is switched back to 'emotion', before MakeMe() is called to request another message from the player.
    //If the answer is incorrect, then the AI tells the players to select another answer.
    [PunRPC]
    private void checkRiddle(string answer)
    {
        string[] answers = { "U", "Leaf", "Mushroom" };
        var correctAnswer = answers[riddleNumber];
        if (mode == "riddle")
        {
            if (answer == correctAnswer)
            {
                botUI.UpdateDisplay("bot", "Your friend answered the riddle correctly.");
                mode = "emotion";
                riddleNumber++;
                MakeMe();
            }
            else
            {
                botUI.UpdateDisplay("bot", "Your friend clearly doesn't know what they're doing.");
                botUI.UpdateDisplay("bot", "Tell them to give it another go...");
            }
            if (riddleNumber == 3)
            {
                //finish puzzle and call RPC showExit, to reveal the exit doors and let players out.
                this.photonView.RPC("showExit", RpcTarget.All);
                cursorControl.showCursor = false;
            }
        }
    }
}
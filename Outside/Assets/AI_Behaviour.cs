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

    [Header("Robot list of actions")]
    public List<PlayerEmotions> playerEmotionsList;

    [Header("HuggingFace API")]
    public HuggingFaceAPI hfAPI;

    [Header("NavMesh and Animation")]
    //public Animator anim;                       // Robot Animator
    //public NavMeshAgent agent;                  // Robot agent (takes care of robot movement in the navmesh)
    //public float reachedPositionDistance;       // Tolerance distance between the robot and object.
    //public float reachedObjectPositionDistance; // Tolerance distance between the robot and object.
    //public Transform playerPosition;            // Our position
    //public GameObject goalObject;
    //public GameObject grabPosition;             // Position where the object will be placed during the grab

    public Camera cam;                          // Main Camera

    [Header("Input UI")]
    public InputField inputField;     // Our Input Field UI

    private State state;

    public string AIState;

    private void Awake()
    {
        // Set the State to Idle
        state = State.Idle;
    }

    /// <summary>
    /// Rotate the agent towards the camera
    /// </summary>
    /*private void RotateTo()
    {
        var _lookRotation = Quaternion.LookRotation(cam.transform.position);
        agent.transform.rotation = Quaternion.RotateTowards(agent.transform.rotation, _lookRotation, 360);
    }*/

    /// <summary>
    /// Grab the object by putting it in the grabPosition
    /// </summary>
    /// <param name="gameObject">Cube of color</param>
    /*void Grab(GameObject gameObject)
    {
        // Set the gameObject as child of grabPosition
        gameObject.transform.parent = grabPosition.transform;

        // To avoid bugs, set object velocity and angular velocity to 0
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        // Set the gameObject transform position to grabPosition
        gameObject.transform.position = grabPosition.transform.position;
    }*/

    /// <summary>
    /// Drop the gameObject
    /// </summary>
    /// <param name="gameObject">Cube of color</param>
   /* void Drop(GameObject gameObject)
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.transform.SetParent(null);
    }*/


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
        // Call the Corountine UISend_
        StartCoroutine(UISend_(prompt));
    }
    public IEnumerator UISend_(string prompt)
    {
        // Ask HuggingFace API to rank the orders
        yield return hfAPI.HFScore(prompt);

        Utility(hfAPI.maxScore, hfAPI.maxScoreIndex);
        //Debug.Log(hfAPI.maxScore);
        yield return null;
    }

    private void Update()
    {
        // TODO: Define the State Machine that will define how the Robot needs to act given its state
        switch(state)
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
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButton : MonoBehaviour
{
    [SerializeField]
    private string Direction;
    private addforce Addforce;


    void Awake()
    {
        Addforce=GameObject.FindObjectOfType<addforce>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter()
    {
        print("collide");
        Addforce.Push(Direction);

    }

}

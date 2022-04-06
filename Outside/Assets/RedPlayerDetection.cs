using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPlayerDetection : MonoBehaviour
{
    public GameObject slammingDoor;
    public GameObject roomDoor;

    private bool collision;
    //public DoorSelectionManager DoorSelectionManager;

    public int RedDoorCount;

    void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        collision = true;
        //DoorSelectionManager.IncreasePlayerCount(roomDoor.name);
        RedDoorCount += 1;
    }

    void OnTriggerExit(Collider other)
    {
        collision = false;
        //DoorSelectionManager.DecreasePlayerCount(roomDoor.name);
        RedDoorCount -= 1;
    }
}

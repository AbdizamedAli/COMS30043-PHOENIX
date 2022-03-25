using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    
    private void Reset()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    public abstract void Interact();

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerControl>().showInstruction();
        }
        
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerControl>().closeInstruction();
        }   
    }

}

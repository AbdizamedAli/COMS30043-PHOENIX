using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class addforcetest : MonoBehaviour
{

    public Rigidbody box;
    //public GameObject barrier;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Push(box);
        
    }

    void Push(Rigidbody box){
        box.AddForce(0,0,-1,ForceMode.Impulse);
        StartCoroutine("Wait",1);
        
        
    }
    IEnumerator Wait(int seconds){
        yield return new WaitForSeconds(seconds);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField]
    private AudioSource AiSound;
    //private AudioSource oldMusicTrack;
    private bool collided;




    
    // Start is called before the first frame update
    void Start()
    {
        collided=false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if( collided == false && !AiSound.isPlaying&&other.tag == "Player"){
            collided=true;
            //oldMusicTrack.Stop();
            AiSound.Play();
        }
    }
}

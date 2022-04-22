using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTwoMusicTrigger : MonoBehaviour
{
    [SerializeField]
    private AudioSource musicTrack;
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
        if( collided == false && !musicTrack.isPlaying){
            //oldMusicTrack.Stop();
            musicTrack.Play();
        }
    }
}

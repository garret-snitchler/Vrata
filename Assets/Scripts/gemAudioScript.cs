using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class audio_Script : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip audioClip54;
    public AudioClip audioClip56;
    public AudioClip audioClip59;
    public AudioClip audioClipDing;
    public int Counter = 0;

    private void Start()
    {
    Counter = 0;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Gem")
        {

            if (Counter == 2)
            {
                AudioSource.PlayOneShot(audioClip59);
                Counter = 3;
            }

            if (Counter == 1)
            {
                AudioSource.PlayOneShot(audioClip56);
                Counter = 2 ;
            }

            if (Counter == 0)
            {
                AudioSource.PlayOneShot(audioClip54);
                Counter = 1;
            }
        }



    }
}
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
    public AudioSource AudioSource2;
    public AudioClip magicInstruction;
    public int Counter = 0;
    public int FireCounter = 0;
    public int WaterCounter = 0;
    public int EarthCounter = 0;
    public float delay = 5;

    private void Start()
    {
    Counter = 0;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FireGem")
        {
            if (FireCounter == 0)
            {
                AudioSource.PlayOneShot(audioClip59);
                FireCounter = 1;
            }
        }


        if (other.tag == "WaterGem")
        {
            if (WaterCounter == 0)
            {
                AudioSource.PlayOneShot(audioClip56);
                WaterCounter = 1;
            }
        }

        if (other.tag == "EarthGem")
        {
            if (EarthCounter == 0)
            {
                AudioSource.PlayOneShot(audioClip54);
                EarthCounter = 1;
            }
        }


        if (other.tag =="Gem")
        {

            if (Counter == 0)
            {
                AudioSource2.clip = magicInstruction;
                AudioSource2.PlayDelayed(delay);
                Counter = 1;
            }
        }



    
}
}
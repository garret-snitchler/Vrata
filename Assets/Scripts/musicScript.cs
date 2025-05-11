using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicScript : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioSource VoiceBoxAudioSource;
    public AudioClip LabMusic;
    public AudioClip EarthMusic;
    public AudioClip FireMusicTop;
    public AudioClip FireMusicBottom;
    public AudioClip WaterMusic;
    public AudioClip BossMusic;
    public AudioClip NatureMusic;
    public AudioClip AudioClip52;
    public AudioClip AudioClip17;
    public AudioClip AudioClip10;

    private int earthCounter = 0;
    private int waterCounter = 0;
    private int fireCounter = 0;

    private int otherMusicTrigger = 0;

    private void Start()
    {
        AudioSource.clip = LabMusic;
        AudioSource.Play();
    }
    void OnTriggerEnter(Collider other)
    {


        if (other.tag == "NatureArea")
        {
            if (otherMusicTrigger == 0)
            {
                
                VoiceBoxAudioSource.PlayOneShot(AudioClip17);
                AudioSource.clip = NatureMusic;
                AudioSource.Play();
                otherMusicTrigger = 1;
            }
        }

        if (other.tag == "EarthArea")
        {
            AudioSource.clip = EarthMusic;
            AudioSource.Play();
            otherMusicTrigger = 0;
            if (earthCounter == 0)
            {
                earthCounter = 1;
                VoiceBoxAudioSource.PlayOneShot(AudioClip52);
            }
        }

        if (other.tag == "FireAreaTop")
        {
            AudioSource.clip = FireMusicTop;
            AudioSource.Play();
            otherMusicTrigger = 0;
            if (fireCounter == 0)
            {
                fireCounter = 1;
                VoiceBoxAudioSource.PlayOneShot(AudioClip17);
            }
        }

        if (other.tag == "FireAreaBottom")
        {
            AudioSource.clip = FireMusicBottom;
            AudioSource.Play();
            otherMusicTrigger = 0;
        }

        if (other.tag == "WaterArea")
        {
            AudioSource.clip = WaterMusic;
            AudioSource.Play();
            if (waterCounter == 0)
            {
                waterCounter = 1;
                VoiceBoxAudioSource.PlayOneShot(AudioClip10);
                otherMusicTrigger = 0;
            }
        }

        if (other.tag == "LabArea")
        {
            AudioSource.clip = LabMusic;
            AudioSource.Play();
            otherMusicTrigger = 0;
        }

        if (other.tag == "Boss")
        {
            AudioSource.clip = BossMusic;
            AudioSource.Play();
        }

    }
}
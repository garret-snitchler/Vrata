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

    private void Start()
    {
        AudioSource.clip = LabMusic;
        AudioSource.Play();
    }
    void OnTriggerEnter(Collider other)
    {


        if (other.tag == "NatureArea")
        {
            AudioSource.clip = NatureMusic;
            AudioSource.Play();
        }

        if (other.tag == "EarthArea")
        {
            AudioSource.clip = EarthMusic;
            AudioSource.Play();
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
        }

        if (other.tag == "WaterArea")
        {
            AudioSource.clip = WaterMusic;
            AudioSource.Play();
            AudioSource.clip = FireMusicTop;
            AudioSource.Play();
            if (waterCounter == 0)
            {
                waterCounter = 1;
                VoiceBoxAudioSource.PlayOneShot(AudioClip10);
            }
        }

        if (other.tag == "LabArea")
        {
            AudioSource.clip = LabMusic;
            AudioSource.Play();
        }

        if (other.tag == "Boss")
        {
            AudioSource.clip = BossMusic;
            AudioSource.Play();
        }

    }
}
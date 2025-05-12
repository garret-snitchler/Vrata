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
    private int nonEarthMusicTrigger = 0;
    private int nonWaterMusicTrigger = 0;
    private int nonTopFireMusicTrigger = 0;
    private int nonBottomFireMusicTrigger = 0;
    private int nonLabMusicTrigger = 0;

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
                AudioSource.clip = NatureMusic;
                AudioSource.Play();
                otherMusicTrigger = 1;
                nonTopFireMusicTrigger = 0;
                nonBottomFireMusicTrigger = 0;
                nonEarthMusicTrigger = 0;
                nonWaterMusicTrigger = 0;
                nonLabMusicTrigger = 0;
            }       
        }

        if (other.tag == "EarthArea")
        {
            if (nonEarthMusicTrigger == 0)
            {
                AudioSource.clip = EarthMusic;
                AudioSource.Play();

                otherMusicTrigger = 0;
                nonBottomFireMusicTrigger = 0;
                nonTopFireMusicTrigger = 0;
                nonEarthMusicTrigger = 1;
                nonWaterMusicTrigger = 0;
                nonLabMusicTrigger = 0;

            }

            if (earthCounter == 0)
            {
                earthCounter = 1;
                VoiceBoxAudioSource.PlayOneShot(AudioClip52);
            }
        }

        if (other.tag == "FireAreaTop")
        {
            if (nonTopFireMusicTrigger == 0)
            {
                AudioSource.clip = FireMusicTop;
                AudioSource.Play();

                nonTopFireMusicTrigger = 1;
                nonBottomFireMusicTrigger = 0;
                otherMusicTrigger = 0;
                nonEarthMusicTrigger = 0;
                nonWaterMusicTrigger = 0;
                nonLabMusicTrigger = 0;
            }
            otherMusicTrigger = 0;
            if (fireCounter == 0)
            {
                fireCounter = 1;
                VoiceBoxAudioSource.PlayOneShot(AudioClip17);
            }
        }

        if (other.tag == "FireAreaBottom")
        {
            if (nonBottomFireMusicTrigger == 0)
            {
                AudioSource.clip = FireMusicBottom;
                AudioSource.Play();

                nonBottomFireMusicTrigger = 1;
                otherMusicTrigger = 0;
                nonTopFireMusicTrigger = 0;
                nonWaterMusicTrigger = 0;
                nonEarthMusicTrigger = 0;
                nonLabMusicTrigger = 0;
            }

        }

        if (other.tag == "WaterArea")
        {
            if (nonWaterMusicTrigger == 0)
            {
                AudioSource.clip = WaterMusic;
                AudioSource.Play();

                otherMusicTrigger = 0;
                nonTopFireMusicTrigger = 0;
                nonBottomFireMusicTrigger = 0;
                nonEarthMusicTrigger = 0;
                nonWaterMusicTrigger = 1;
                nonLabMusicTrigger = 0;
            }

            if (waterCounter == 0)
            {
                waterCounter = 1;
                VoiceBoxAudioSource.PlayOneShot(AudioClip10);
            }
        }

        if (other.tag == "LabArea")
        {

            if (nonLabMusicTrigger == 0)
            {
                AudioSource.clip = LabMusic;
                AudioSource.Play();

                otherMusicTrigger = 0;
                nonTopFireMusicTrigger = 0;
                nonBottomFireMusicTrigger = 0;
                nonEarthMusicTrigger = 0;
                nonWaterMusicTrigger = 0;
                nonLabMusicTrigger = 1;

            }
        }

        if (other.tag == "Boss")
        {
            AudioSource.clip = BossMusic;
            AudioSource.Play();
        }

    }
}
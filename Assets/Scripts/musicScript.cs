using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicScript : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip LabMusic;
    public AudioClip EarthMusic;
    public AudioClip FireMusicTop;
    public AudioClip FireMusicBottom;
    public AudioClip WaterMusic;
    public AudioClip BossMusic;
    void OnTriggerEnter(Collider other)
    {


        if (other.tag == "EarthArea")
        {
            AudioSource.clip = EarthMusic;
            AudioSource.Play();
        }

        if (other.tag == "FireAreaTop")
        {
            AudioSource.clip = FireMusicTop;
            AudioSource.Play();
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstLabVoiceLineAudio : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip AudioClip22;
    public AudioClip AudioClip25;
    public AudioClip AudioClip21;
    public AudioClip AudioClip2;
    public AudioClip AudioClip32;
    public AudioClip AudioClip37;
    public AudioClip AudioClip39;
    public AudioClip BossVoiceLine01;
    public AudioClip BossVoiceLine02;
    public AudioClip YouWin;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CloneSight")
        {
            AudioSource.PlayOneShot(AudioClip22);
        }

        if (other.tag == "WeaponSight")
        {
            AudioSource.PlayOneShot(AudioClip25);
        }

        if (other.tag == "PracticeAreaSight")
        {
            AudioSource.PlayOneShot(AudioClip21);
        }

        if (other.tag == "PortalInSight")
        {
            AudioSource.PlayOneShot(AudioClip2);
        }

        if (other.tag == "Sword")
        {
            AudioSource.PlayOneShot(AudioClip32);
        }

        if (other.tag == "Bow")
        {
            AudioSource.PlayOneShot(AudioClip37);
        }

        if (other.tag == "Bomb")
        {
            AudioSource.PlayOneShot(AudioClip39);
        }



    }
}

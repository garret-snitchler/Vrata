using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstLabVoiceLineAudio : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioSource BossLine2AudioSource;
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
    public AudioClip AudioClip27;

    private int cloneCounter = 0;
    private int weaponCounter = 0;
    private int practiceCounter = 0;
    private int portalCounter = 0;
    private int bossCounter = 0;

    public float delay = 3;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CloneSight")
        {
            if (cloneCounter == 0)
            {
                cloneCounter = 1;
                AudioSource.PlayOneShot(AudioClip22);
            }
        }

        if (other.tag == "WeaponSight")
        {
            if (weaponCounter == 0)
            {
                weaponCounter = 1;
                AudioSource.PlayOneShot(AudioClip25);
            }
        }

        if (other.tag == "PracticeAreaSight")
        {
            if (practiceCounter == 0)
            {
                practiceCounter = 1;
                AudioSource.PlayOneShot(AudioClip21);
            }
        }

        if (other.tag == "Boss")
        {
            if (practiceCounter == 0)
            {
                bossCounter = 1;
                AudioSource.PlayOneShot(BossVoiceLine01);
                BossLine2AudioSource.clip = AudioClip27;
                BossLine2AudioSource.PlayDelayed(delay);
                BossLine2AudioSource.clip = BossVoiceLine02;
                BossLine2AudioSource.PlayDelayed(delay);
            }
        }

        if (other.tag == "PortalInSight")
        {
            if (portalCounter == 0)
            {
                portalCounter = 1;
                AudioSource.PlayOneShot(AudioClip2);
            }
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

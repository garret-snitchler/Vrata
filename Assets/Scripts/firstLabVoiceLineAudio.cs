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
    public AudioClip AudioClip30;
    public AudioClip AudioClip38;
    public AudioClip AudioClip49;
    public AudioClip PortalInfo;


    private int cloneCounter = 0;
    private int weaponCounter = 0;
    private int practiceCounter = 0;
    private int portalCounter = 0;
    private int portalInfoCounter = 0;
    private int bossCounter = 0;
    private int minorFireEnemyCounter = 0;
    private int majorFireEnemyCounter = 0;
    private int minorWaterEnemyCounter = 0;
    private int majorWaterEnemyCounter = 0;
    private int minorEarthEnemyCounter = 0;
    private int majorEarthEnemyCounter = 0;

    private int swordCounter = 0;
    private int bombCounter = 0;
    private int bowCounter = 0;

    public float delay = 4;

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
            if (bossCounter == 0)
            {
                bossCounter = 1;
                AudioSource.PlayOneShot(BossVoiceLine01);
                BossLine2AudioSource.clip = AudioClip27;
                BossLine2AudioSource.PlayDelayed(delay);
                BossLine2AudioSource.clip = BossVoiceLine02;
                BossLine2AudioSource.PlayDelayed(delay);
            }
        }


        if (other.tag == "waterEnemyMajor")
        {
            if (majorWaterEnemyCounter == 0)
            {
                majorWaterEnemyCounter = 1;
                AudioSource.PlayOneShot(AudioClip30);
            }
        }

        if (other.tag == "earthEnemyMajor")
        {
            if (majorEarthEnemyCounter == 0)
            {
                majorEarthEnemyCounter = 1;
                AudioSource.PlayOneShot(AudioClip49);
            }
        }



        if (other.tag == "fireEnemyMajor")
        {
            if (majorFireEnemyCounter == 0)
            {
                majorFireEnemyCounter = 1;
                AudioSource.PlayOneShot(AudioClip38);
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
            if (swordCounter == 0)
            {
                AudioSource.PlayOneShot(AudioClip32);
            }
            if (swordCounter == 2)
            {
                AudioSource.PlayOneShot(AudioClip32);
            }
            swordCounter += 1;
        }

        if (other.tag == "Bow")
        {
            if (bowCounter == 0)
            {
                AudioSource.PlayOneShot(AudioClip37);
            }
            if (bowCounter == 2)
            {
                AudioSource.PlayOneShot(AudioClip37);
            }
            bowCounter += 1;
        }

        if (other.tag == "Bomb")
        {
            if (bombCounter == 0)
            {
                AudioSource.PlayOneShot(AudioClip39);
            }
            if (bombCounter == 2)
            {
                AudioSource.PlayOneShot(AudioClip39);
            }
            bombCounter += 1;
        }
        if (other.tag == "PortalInfo")
        {
            if (portalInfoCounter == 0)
            {
                AudioSource.PlayOneShot(PortalInfo);
            }
            if (portalInfoCounter == 2)
            {
                AudioSource.PlayOneShot(PortalInfo);
            }
            portalInfoCounter += 1;
        }

    }
}

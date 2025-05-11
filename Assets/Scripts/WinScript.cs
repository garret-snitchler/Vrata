using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WinScript : MonoBehaviour
{
    public AudioSource audiosource;
    public AudioClip winclip;
    public GameObject BBEG;
    public GameObject BossFightPortal; 

    private bool win = false;

    // this should only be called when all three gems are collected
    public void InitializeBossFight()
    {
        BossFightPortal.SetActive(true);
    }

    void Update()
    {
        if (!win && BBEG.GetComponent<Enemy>().health <= 0)
        {
            win = true;
            audiosource.clip = winclip;
            audiosource.Play();
        }
    }
}
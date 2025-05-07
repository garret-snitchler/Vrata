using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WinScript : MonoBehaviour
{
    public AudioSource audiosource;
    public AudioClip winclip;
    public bool win;

    public void InitializeBossFight()
    {
        print("Boss fight initialized");

        if (win == true)
        { 
            audiosource.clip = winclip;
            audiosource.Play();
        }
    }
}
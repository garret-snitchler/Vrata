using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WinScript : MonoBehaviour
{
    public AudioSource audiosource;
    public AudioClip winclip;
    public GameObject BBEG;

    private bool win = false;

    public void InitializeBossFight()
    {
        BBEG.SetActive(true);
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
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public int maxHealth;
    private int health;
    public int powerUpTime;
    public TMPro.TextMeshPro healthText;
    public GameObject blackSquare;
    public GameObject ValleySpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        SetHealthText();
    }

    //// Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            StartCoroutine(FadeBlackOutSquare());
            Invoke(nameof(KillPlayer), 3);
        }
    }

    public void HealPlayer(int heal)
    {
        if (health + heal > maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += heal;
        }
        SetHealthText();
    }

    public void DamagePlayer(int damage)
    {
        health -= damage;
        SetHealthText();
    }

    void SetHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + health.ToString();
        }
    }

    void KillPlayer()
    {
        StartCoroutine(FadeBlackOutSquare(true));
        this.gameObject.transform.position = ValleySpawnPoint.transform.position;
        HealPlayer(10);
        StartCoroutine(FadeBlackOutSquare(false));
    }

    public IEnumerator FadeBlackOutSquare(bool fadeToBlack = true, int fadeSpeed = 3)
    {
        Color objectColor = blackSquare.GetComponent<Image>().color;
        float fadeAmount;

        if (fadeToBlack)
        {
            while (blackSquare.GetComponent<Image>().color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        } else
        {
            while (blackSquare.GetComponent<Image>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
        yield return new WaitForEndOfFrame();
    }
}

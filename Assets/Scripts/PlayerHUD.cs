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

    void Start()
    {
        health = maxHealth;
        SetHealthText();
    }

    void Update()
    {
        if (health <= 0)
        {
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
        StartCoroutine(FadeBlackOutSquare(true, true));
    }

    public IEnumerator FadeBlackOutSquare(bool fadeToBlack = true, bool playerKilled = false)
    {
        Color objectColor = blackSquare.GetComponent<Image>().color;
        float fadeSpeed = 0.5f;
        float fadeAmount;

        if (fadeToBlack)
        {
            this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            blackSquare.SetActive(true);
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
            blackSquare.SetActive(false);
        }
        yield return new WaitForEndOfFrame();

        if (playerKilled && fadeToBlack)
        {
            this.gameObject.transform.position = ValleySpawnPoint.transform.position;
            HealPlayer(10);
        } 

        if (!fadeToBlack)
        {
            this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }

        yield return new WaitForSeconds(2);
        StartCoroutine(FadeBlackOutSquare(false, playerKilled));
    }
}

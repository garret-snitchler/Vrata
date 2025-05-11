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
    public GameObject gameOverSpawnPoint;
    public GameObject valleySpawnPoint;

    private bool killPlayerBool = false;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        SetHealthText();
    }

    void Update()
    {
        if (health <= 0 && !killPlayerBool)
        {
            killPlayerBool = true;
            Invoke(nameof(KillPlayer), 0.5f);
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
        if (health - damage < 0)
        {
            health = 0;
        }
        else
        {
            health -= damage;
        }
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

    public void RespawnPlayer()
    {
        StartCoroutine(FadeBlackOutSquare(true));
        StartCoroutine(DelayRespawn());
    }

    public IEnumerator DelayRespawn()
    {
        yield return new WaitForSeconds(2.5f);
        this.gameObject.transform.position = valleySpawnPoint.transform.position;
    }

    public IEnumerator FadeBlackOutSquare(bool fadeToBlack = true, bool playerKilled = false)
    {
        Color objectColor = blackSquare.GetComponent<Image>().color;
        float fadeSpeed = 0.5f;
        float fadeAmount;

        if (fadeToBlack)
        {
            print("Start clear to black"); 
            this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            this.gameObject.GetComponent<MovePlayer>().enabled = false;
            while (blackSquare.GetComponent<Image>().color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        } else
        {
            print("start black to clear");
            while (blackSquare.GetComponent<Image>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
        yield return new WaitForEndOfFrame();

        if (playerKilled && fadeToBlack)
        {
            this.gameObject.transform.position = gameOverSpawnPoint.transform.position;
            HealPlayer(10);
        } 

        if (!fadeToBlack)
        {
            this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            this.gameObject.GetComponent<MovePlayer>().enabled = true;
        }

        yield return new WaitForSeconds(2);

        if (fadeToBlack)
        {
            StartCoroutine(FadeBlackOutSquare(false, playerKilled));
        } else if (playerKilled)
        {
            killPlayerBool = false; 
        }
    }
}

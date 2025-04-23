using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHUD : MonoBehaviour
{
    public int maxHealth;
    private int health;
    public int powerUpTime;
    public TMPro.TextMeshPro healthText;
    public string RespawnScene;
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
            GameOver();
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

    IEnumerator GameOver()
    {
        if (RespawnScene != "" && RespawnScene != null)
        {
            print("Respawn Scene: " + RespawnScene);
            SceneManager.LoadScene(RespawnScene);
        }
        else
        {
            print("Respawn scene not set, reloading current scene");
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
            yield return new WaitForEndOfFrame();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }
    }
}

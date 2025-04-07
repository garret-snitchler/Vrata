using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    public int health;
    public int powerUpTime;
    public TextMeshProUGUI healthText;
    // Start is called before the first frame update
    void Start()
    {
        SetHealthText();
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public void DamagePlayer(int damage)
    {
        health -= damage;
        SetHealthText();
    }

    void SetHealthText()
    {
        if (healthText != null)
        {
            print(health);
            healthText.text = "Health: " + health.ToString();
        }
    }
}

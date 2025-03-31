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
        setHealthText();
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public void damagePlayer(int damage)
    {
        health -= damage;
        setHealthText();
    }

    void setHealthText()
    {
        if (healthText != null)
        {
            print(health);
            healthText.text = "Health: " + health.ToString();
        }
    }
}

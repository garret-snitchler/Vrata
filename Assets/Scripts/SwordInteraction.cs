using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SwordInteraction : MonoBehaviour
{
    public SteamVR_Action_Vibration hapticAction;
    public List<Material> swordTypes = new List<Material>();

    private int currentColor = 0; 
    private Hand holdingHand;

    private List<Color> swordColors = new List<Color>()
    {
        new Color(0.706f, 0.329f, 0.086f), //fire
        new Color(0.227f, 0.663f, 0.043f), //earth
        new Color(0.059f, 0.196f, 0.486f), //water
    };

    public void OnCollisionEnter(Collision coll)
    {
        TriggerVibration();
        StopPowerup();
    }

    public void OnCollisionStay(Collision coll)
    {
        TriggerVibration();
    }

    public void TriggerVibration()
    {
        float duration = 0.01f;  // 1 second
        float frequency = 50.0f; // Adjust as needed (low = soft, high = intense)
        float amplitude = 1.0f; // 0 to 1 (intensity)

        hapticAction.Execute(0, duration, frequency, amplitude, holdingHand.handType);
    }

    private void OnAttachedToHand(Hand hand)
    {
        holdingHand = hand;
        StopPowerup();
    }

    private void OnDetachedFromHand(Hand hand)
    {
        holdingHand = null;
    }

    public void ChangeColor(List<bool> gems, int numUnlocked)
    {
        ChangeCurrentColorIndex(gems, numUnlocked);

        this.gameObject.GetComponent<MeshRenderer>().material = swordTypes[currentColor];

        if (currentColor > 0)
        {
            ParticleSystem.MainModule ps = this.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>().main;
            ps.startColor = swordColors[currentColor - 1];


            ParticleSystem.MainModule ps2 = this.gameObject.transform.GetChild(1).GetComponent<ParticleSystem>().main;
            ps2.startColor = swordColors[currentColor - 1];


            ParticleSystem.MainModule ps3 = this.gameObject.transform.GetChild(2).GetComponent<ParticleSystem>().main;
            ps3.startColor = swordColors[currentColor - 1];
        } else
        {
            StopPowerup();
        }
    }

    private void ChangeCurrentColorIndex(List<bool> gems, int numUnlocked)
    {
        if (numUnlocked == 0)
        {
            currentColor = 0;
            return;
        }

        int count = 0;
        while (count < 4)
        {
            currentColor += 1;
            if (currentColor > 3)
            {
                currentColor = 0;
                return;
            }

            if (gems[currentColor - 1])
            {
                return;
            }
            count++;
        }
    }

    public void UsePowerup()
    {
        if (currentColor > 0)
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            this.gameObject.transform.GetChild(2).gameObject.SetActive(true);
        }
    }

    public void StopPowerup()
    {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(2).gameObject.SetActive(false);
    }

    public bool UsingSpecialSword()
    {
        return currentColor > 0; 
    }
}

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
        new Color(0.514f, 0.761f, 1), //og
        new Color(0.227f, 0.663f, 0.043f), //earth
        new Color(0.706f, 0.329f, 0.086f), //fire
        new Color(0.059f, 0.196f, 0.486f), //water
        new Color(0.929f, 0.353f, 1) //purple
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
    }

    private void OnDetachedFromHand(Hand hand)
    {
        holdingHand = null;
    }

    public void ChangeColor(int numUnlocked)
    {
        if (currentColor + 1 > numUnlocked)
        {
            currentColor = 0; 
        } else
        {
            currentColor += 1; 
        }

        this.gameObject.GetComponent<MeshRenderer>().material = swordTypes[currentColor];

        ParticleSystem.MainModule ps = this.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>().main;
        ps.startColor = swordColors[currentColor];


        ParticleSystem.MainModule ps2 = this.gameObject.transform.GetChild(1).GetComponent<ParticleSystem>().main;
        ps2.startColor = swordColors[currentColor];


        ParticleSystem.MainModule ps3 = this.gameObject.transform.GetChild(2).GetComponent<ParticleSystem>().main;
        ps3.startColor = swordColors[currentColor];
    }

    public void UsePowerup()
    {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(true); 
        this.gameObject.transform.GetChild(2).gameObject.SetActive(true);
    }

    public void StopPowerup()
    {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(2).gameObject.SetActive(false);
    }
}

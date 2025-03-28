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

    public void OnCollisionEnter(Collision coll)
    {
        TriggerVibration();
    }

    public void OnCollisionStay(Collision coll)
    {
        TriggerVibration();
    }

    public void TriggerVibration()
    {
        float duration = 0.01f;  // 1 second
        float frequency = 150.0f; // Adjust as needed (low = soft, high = intense)
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
        ParticleSystem.MainModule ps = this.gameObject.transform.GetChild(2).GetComponent<ParticleSystem>().main;
        
        //TODO: add a list of custom colors
        ps.startColor = Color.red;

        //TODO: change sparks and glow colors too

        //TODO: fix the preview sword so it lays on the table. 
        //TODO: add a way to switch the sword between hands? 
    }
}

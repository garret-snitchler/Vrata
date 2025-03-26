using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class WeaponPickup : MonoBehaviour
{
    public Weapon weaponType;

    private WeaponHandler handlerScript; 

    public void Awake()
    {
        handlerScript = GameObject.Find("WeaponHandler").GetComponent<WeaponHandler>();
    }

    private void OnAttachedToHand(Hand hand)
    {
        handlerScript.ChangeWeaponType(weaponType, hand, this.gameObject); 
    }

    private void OnDetachedFromHand(Hand hand)
    {
        handlerScript.ChangeWeaponType(null, null, null);
    }
}

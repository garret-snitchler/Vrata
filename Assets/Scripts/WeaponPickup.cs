using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using System; 

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
        if (weaponType == Weapon.Longbow)
        {
            StartCoroutine(Wait(hand)); 
        } else
        {
            handlerScript.ChangeWeaponType(weaponType, hand, this.gameObject);
        }
    }

    private void OnDetachedFromHand(Hand hand)
    {
        handlerScript.ChangeWeaponType(null, null, null);
    }

    IEnumerator Wait(Hand hand)
    {
        yield return new WaitForSeconds(0.5f);
        handlerScript.ChangeWeaponType(weaponType, hand, this.gameObject);
    }
}

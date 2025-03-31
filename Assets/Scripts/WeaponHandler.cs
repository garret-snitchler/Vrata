using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class WeaponHandler : MonoBehaviour
{
    private Weapon? currentWeapon = null;
    private Hand? currentHand = null;
    private GameObject? currentWeaponObj = null;

    public int numGemsUnlocked = 0;
    public AudioSource powerupNotAvailable; 

    private bool powerupAvailable = true;

    public void ChangeWeaponType(Weapon? newWeaponType, Hand? hand, GameObject? newWeaponObj)
    {
        currentWeapon = newWeaponType;
        currentHand = hand;

        if (currentWeapon == Weapon.Longbow)
        {
            //TODO: set to arrow, not bow. 
            //if (hand.handType == SteamVR_Input_Sources.LeftHand)
            //{
            //    var obj = SteamVR_Input_Sources.RightHand.hand.currentAttachedObject;
            //    print(obj.name);
            //}
            //TODO: arrow should have ArrowInteraction (custom) script
                //That has ChangeColor and UsePowerup functions. 
        } else
        {
            currentWeaponObj = newWeaponObj;
        }
    }

    public void SwitchWeaponColor()
    {
        switch (currentWeapon)
        {
            case Weapon.Sword:
                currentWeaponObj.GetComponent<SwordInteraction>().ChangeColor(numGemsUnlocked);
                break;
            case Weapon.Boom:
                //TODO: add boom color change
                break;
            case Weapon.Longbow:
                //TODO: change color of arrow feathers and particle system. 
                break;
            default:
                break;
        }
    }

    public void EnablePowerup()
    {
        print(powerupAvailable);
        if (powerupAvailable)
        {
            powerupAvailable = false;
            currentWeaponObj.GetComponent<DealsDamage>().IsPoweredUp();
            switch (currentWeapon)
            {
                case Weapon.Sword:
                    currentWeaponObj.GetComponent<SwordInteraction>().UsePowerup();
                    break;
                default:
                    break;
            }
            StartCoroutine(Recharge());
        }
        else
        {
            powerupNotAvailable.Play();
        }
    }

    IEnumerator Recharge()
    {
        yield return new WaitForSeconds(10);
        DisablePowerup(); 
    }

    public void DisablePowerup()
    {
        switch (currentWeapon)
        {
            case Weapon.Sword:
                currentWeaponObj.GetComponent<SwordInteraction>().StopPowerup();
                break;
            default:
                break;
        }
        powerupAvailable = true;
    }
}

public enum Weapon { Longbow, Sword, Boom }

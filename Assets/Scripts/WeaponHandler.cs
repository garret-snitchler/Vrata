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
        currentWeaponObj = newWeaponObj;
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
                GetArrow().GetComponent<ArrowInteraction>().ChangeColor(numGemsUnlocked); 
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
            switch (currentWeapon)
            {
                case Weapon.Sword:
                    currentWeaponObj.GetComponent<DealsDamage>().IsPoweredUp();
                    currentWeaponObj.GetComponent<SwordInteraction>().UsePowerup();
                    break;
                case Weapon.Longbow:
                    GetArrow().GetComponent<DealsDamage>().IsPoweredUp();
                    GetArrow().GetComponent<ArrowInteraction>().UsePowerup();
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

    private GameObject GetArrow()
    {
        return currentWeaponObj.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
    }
}

public enum Weapon { Longbow, Sword, Boom }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using System;
using System.Linq; 

public class WeaponHandler : MonoBehaviour
{
    private Weapon? currentWeapon = null;
    private Hand? currentHand = null;
    private GameObject? currentWeaponObj = null;
    public int GemsUnlocked = 0;

    public List<bool> gemsUnlocked = new List<bool> { false, false, false };

    public AudioSource powerupNotAvailable;

    private bool powerupAvailable = true;
    private int arrowColor = 0;

    public void ChangeWeaponType(Weapon? newWeaponType, Hand? hand, GameObject? newWeaponObj)
    {
        currentWeapon = newWeaponType;
        currentHand = hand;
        currentWeaponObj = newWeaponObj;
    }

    public void SetColorOfNewInstantiate()
    {
        if (currentWeapon == Weapon.Longbow)
        {
            GetArrow().GetComponent<ArrowInteraction>().SetColor(arrowColor);
        }
    }

    public void SwitchWeaponColor()
    {
        int n = NumGemsUnlocked(); 
        if (n > 0)
        {
            switch (currentWeapon)
            {
                case Weapon.Sword:
                    currentWeaponObj.GetComponent<SwordInteraction>().ChangeColor(gemsUnlocked, n);
                    break;
                case Weapon.Boom:
                    currentWeaponObj.GetComponent<BoomSpawner>().ChangeColor(gemsUnlocked, n);
                    break;
                case Weapon.Longbow:
                    arrowColor = GetArrow().GetComponent<ArrowInteraction>().ChangeColor(gemsUnlocked, n);
                    break;
                default:
                    break;
            }
        }
        else
        {
            print("cant change color"); 
            powerupNotAvailable.Play();
        }
    }

    public void EnablePowerup()
    {
        if (powerupAvailable && NumGemsUnlocked() > 0)
        {
            switch (currentWeapon)
            {
                case Weapon.Sword:
                    if (currentWeaponObj.GetComponent<SwordInteraction>().UsingSpecialSword())
                    {
                        powerupAvailable = false;
                        currentWeaponObj.GetComponent<DealsDamage>().IsPoweredUp();
                        currentWeaponObj.GetComponent<SwordInteraction>().UsePowerup();
                        StartCoroutine(Recharge());
                    }
                    else
                    {
                        print("sword powerup not available");
                        powerupNotAvailable.Play();
                    }
                    break;
                case Weapon.Longbow:
                    if (GetArrow().GetComponent<ArrowInteraction>().UsingSpecialArrow())
                    {
                        powerupAvailable = false;
                        GetArrow().GetComponent<DealsDamage>().IsPoweredUp();
                        GetArrow().GetComponent<ArrowInteraction>().UsePowerup();
                        StartCoroutine(Recharge());
                    }
                    else
                    {
                        print("arrow powerup not available"); 
                        powerupNotAvailable.Play();
                    }
                    break;
                case Weapon.Boom:
                    if (currentWeaponObj.GetComponent<BoomSpawner>().UsingSpecialBoom())
                    {
                        powerupAvailable = false;
                        currentWeaponObj.GetComponent<BoomSpawner>().UsePowerup();
                        StartCoroutine(Recharge());
                    }
                    else
                    {
                        print("boom powerup not available");
                        powerupNotAvailable.Play();
                    }
                    break;
                default:
                    break;
            }
        }
        else
        {
            print("powerup not available yet"); 
            powerupNotAvailable.Play();
        }
    }

    IEnumerator Recharge()
    {
        print("start recharge"); 
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
            case Weapon.Longbow:
                GetArrow().GetComponent<ArrowInteraction>().StopPowerup();
                break;
            case Weapon.Boom:
                currentWeaponObj.GetComponent<BoomSpawner>().StopPowerup();
                break;
            default:
                break;
        }
        print("powerupAvailable"); 
        powerupAvailable = true;
    }

    private GameObject GetArrow()
    {
        return currentWeaponObj.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
    }

    public int NumGemsUnlocked()
    {
        return gemsUnlocked.Count(b => b);
    }
}
public enum Weapon { Longbow, Sword, Boom }



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
            case Weapon.Dagger:
                break;
            case Weapon.Longbow:
            default:
                break; 
        }
    }
}

public enum Weapon { Longbow, Sword, Dagger }

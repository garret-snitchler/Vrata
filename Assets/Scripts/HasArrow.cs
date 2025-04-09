using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasArrow : MonoBehaviour
{
    private WeaponHandler weaponHandlerScript; 
    private bool hasNotified = false; 

    void Start()
    {
        weaponHandlerScript = GameObject.Find("WeaponHandler").GetComponent<WeaponHandler>(); 
    }

    void Update()
    {
        if (this.gameObject.transform.childCount > 0 && !hasNotified)
        {
            hasNotified = true;
            weaponHandlerScript.SetColorOfNewInstantiate(); 
        } else if (this.gameObject.transform.childCount == 0 && hasNotified)
        {
            hasNotified = false; 
        }
    }
}

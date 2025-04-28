using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using System;
using Valve.VR.InteractionSystem;

public class BoomSpawner : MonoBehaviour
{
    public Hand leftHand;
    public Hand rightHand;

    public SteamVR_Input_Sources handTypeL;
    public SteamVR_Input_Sources handTypeR;
    public SteamVR_Action_Boolean LeftGrip;
    public SteamVR_Action_Boolean RightGrip;
    public SteamVR_Action_Boolean LeftTrigger;
    public SteamVR_Action_Boolean RightTrigger;

    public WeaponHandler handlerScript; 
    public GameObject boomPrefab;
    public List<Material> boomColors; 

    public bool canMakeBoom = true;
    public int currentColor = 0;
    private List<Color> glowColors = new List<Color>()
    {
        new Color(0.706f, 0.329f, 0.086f), //fire
        new Color(0.227f, 0.663f, 0.043f), //earth
        new Color(0.059f, 0.196f, 0.486f), //water
    };

    public void Awake()
    {
        handlerScript.ChangeWeaponType(Weapon.Boom, null, this.gameObject);
    }

    void Update()
    {
        if (LeftGrip.GetState(handTypeL))
        {
            // Grip pressed on left hand
            if (canMakeBoom && (leftHand.currentAttachedObject == null || leftHand.currentAttachedObject.GetComponent<HasBombs>() != null)
                && (rightHand.currentAttachedObject == null || rightHand.currentAttachedObject.GetComponent<HasBombs>()))
            {
                canMakeBoom = false; 
                print("make boom");
                GameObject currentBoom = Instantiate(boomPrefab, leftHand.transform.position - new Vector3(0.25f, 0, 0), leftHand.transform.rotation);
                currentBoom.GetComponent<Rigidbody>().useGravity = false;
                currentBoom.transform.GetChild(0).GetComponent<MeshRenderer>().material = boomColors[currentColor];

                if (currentColor > 0)
                {
                    ParticleSystem.MainModule ps = currentBoom.transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().main;
                    ps.startColor = glowColors[currentColor - 1];
                }

                StartCoroutine(Reset()); 
            }
        }
        else if (RightGrip.GetState(handTypeR))
        {
            // Grip pressed on right hand
            if (canMakeBoom && (leftHand.currentAttachedObject == null || leftHand.currentAttachedObject.GetComponent<HasBombs>() != null)
                && (rightHand.currentAttachedObject == null || rightHand.currentAttachedObject.GetComponent<HasBombs>()))
            {
                canMakeBoom = false;
                print("make boom");
                GameObject currentBoom = Instantiate(boomPrefab, rightHand.transform.position + new Vector3(0.25f, 0, 0), rightHand.transform.rotation);
                currentBoom.GetComponent<Rigidbody>().useGravity = false;
                currentBoom.transform.GetChild(0).GetComponent<MeshRenderer>().material = boomColors[currentColor];

                if (currentColor > 0)
                {
                    ParticleSystem.MainModule ps = currentBoom.transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().main;
                    ps.startColor = glowColors[currentColor - 1];
                }

                StartCoroutine(Reset());
            }
        }
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(3f);
        canMakeBoom = true; 
    }

    public int ChangeColor(List<bool> gems, int numUnlocked)
    {
        print("Change boom color"); 
        ChangeCurrentColorIndex(gems, numUnlocked);

        if (leftHand.currentAttachedObject != null && leftHand.currentAttachedObject.name == "Base Bomb(Clone)")
            leftHand.currentAttachedObject.transform.GetChild(0).GetComponent<MeshRenderer>().material = boomColors[currentColor];

        if (rightHand.currentAttachedObject != null && rightHand.currentAttachedObject.name == "Base Bomb(Clone)")
            rightHand.currentAttachedObject.transform.GetChild(0).GetComponent<MeshRenderer>().material = boomColors[currentColor];

        if (currentColor > 0)
        {
            if (leftHand.currentAttachedObject != null && leftHand.currentAttachedObject.name == "Base Bomb(Clone)")
            {
                ParticleSystem.MainModule ps = leftHand.currentAttachedObject.transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().main;
                ps.startColor = glowColors[currentColor - 1];
            }

            if (rightHand.currentAttachedObject != null && rightHand.currentAttachedObject.name == "Base Bomb(Clone)")
            {
                ParticleSystem.MainModule ps2 = rightHand.currentAttachedObject.transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().main;
                ps2.startColor = glowColors[currentColor - 1];
            }
        }
        else
        {
            StopPowerup();
        }

        return currentColor;
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
            print("use boom powerup");
            if (leftHand.currentAttachedObject != null && leftHand.currentAttachedObject.name == "Base Bomb(Clone)")
            {
                leftHand.currentAttachedObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                leftHand.currentAttachedObject.GetComponent<DealsDamage>().IsPoweredUp();
            }
            if (rightHand.currentAttachedObject != null && rightHand.currentAttachedObject.name == "Base Bomb(Clone)")
            {
                rightHand.currentAttachedObject?.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                rightHand.currentAttachedObject.GetComponent<DealsDamage>().IsPoweredUp();
            }
        }
    }

    public void StopPowerup()
    {
        leftHand.currentAttachedObject?.transform.GetChild(0)?.GetChild(0).gameObject.SetActive(false);
        rightHand.currentAttachedObject?.transform.GetChild(0)?.GetChild(0).gameObject.SetActive(false);
    }

    public bool UsingSpecialBoom()
    {
        return currentColor > 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using System; 

public class UserControllerButtons : MonoBehaviour
{
    // action set
    [Header("Actions")]
    public SteamVR_ActionSet playerOperationActionSet;
    //Hand to get (right hand or left hand or any hand)
    public SteamVR_Input_Sources handTypeL;
    public SteamVR_Input_Sources handTypeR;
    public SteamVR_Action_Boolean HitA;
    public SteamVR_Action_Boolean DoubleClickA;
    public SteamVR_Action_Boolean HitB;
    public SteamVR_Action_Boolean HitX;
    public SteamVR_Action_Boolean HitY;
    public SteamVR_Action_Boolean LeftTrigger;
    public SteamVR_Action_Boolean RightTrigger;
    public SteamVR_Action_Vector2 LeftJoystick;
    public SteamVR_Action_Vector2 RightJoystick;
    public SteamVR_Action_Boolean LeftGrip;
    public SteamVR_Action_Boolean RightGrip;

    [Space]
    [Header("Dynamic Portal")]
    //public GameObject TeleportingAsset;
    public GameObject SnapTurnAsset;
    public GameObject worldParentPrefab; 

    [Space]
    [Header("Other")]
    public Transform VRCamera;
    public WeaponHandler weaponHandlerScript;

    public bool portalMode = false;
    private GameObject parentPortal;
    private bool isOneTimePortal = true; 

    void Start()
    {
        playerOperationActionSet.Activate(); 
    }

    void Update()
    {
        if (HitA.GetStateDown(handTypeR))
        {
            print("Hi");
            //Hit A once
            if (!portalMode)
            {

                SetPortalMode(true);
                GameObject worldParent = Instantiate(worldParentPrefab, VRCamera);
                worldParent.transform.rotation = Quaternion.Euler(0, worldParent.transform.eulerAngles.y, 0);
                worldParent.transform.SetParent(null);

                parentPortal = worldParent.transform.GetChild(0).gameObject;
                parentPortal.GetComponent<DynamicPortalParent>().GetObjects();
            } else
            {
                SetPortalMode(false);
                parentPortal.GetComponent<DynamicPortalParent>().Activate();
            }
        }
        else if (HitB.GetStateDown(handTypeR))
        {
            //Hit B
            if (portalMode)
            {
                ChangePortalType();
            }
        } else if (!portalMode && HitX.GetStateDown(handTypeL))
        {
            //Hit X, not in portal mode - switch color
            weaponHandlerScript.SwitchWeaponColor();
        } else if (!portalMode && HitY.GetStateDown(handTypeL))
        {
            //Hit Y, not in portal mode - enable powerup
            weaponHandlerScript.EnablePowerup(); 
        } else if (DoubleClickA.GetStateDown(handTypeR))
        {
            //Double click A
            parentPortal.GetComponent<DynamicPortalParent>().ClosePortal(); 
            SetPortalMode(false);
            Destroy(parentPortal.transform.parent.gameObject);
        } else if (portalMode && LeftTrigger.GetState(handTypeL)) {
            //set rotation left
            parentPortal.GetComponent<DynamicPortalParent>().Rotate(true, LeftJoystick.GetAxis(handTypeL));
        } else if (portalMode && RightTrigger.GetState(handTypeR)) {
            //set rotation right
            parentPortal.GetComponent<DynamicPortalParent>().Rotate(false, RightJoystick.GetAxis(handTypeR));
        } else if (portalMode)
        {
            //Move portals
            parentPortal.GetComponent<DynamicPortalParent>().MovePortal(LeftJoystick.GetAxis(handTypeL), LeftGrip.GetState(handTypeL), RightJoystick.GetAxis(handTypeR), RightGrip.GetState(handTypeR));
        }
    }

    void ChangePortalType()
    {
        isOneTimePortal = !isOneTimePortal;
        parentPortal.GetComponent<DynamicPortalParent>().SetPortalType(isOneTimePortal);
    }

    void SetPortalMode(bool mode)
    {
        portalMode = mode;
        if (portalMode)
        {
            //TeleportingAsset.SetActive(false);
            SnapTurnAsset.SetActive(false);
            Time.timeScale = 0.1f;
        }
        else
        {
            //TeleportingAsset.SetActive(true);
            SnapTurnAsset.SetActive(true);
            Time.timeScale = 1.0f;
        }
    }

    public bool IsInPortalMode()
    {
        return portalMode;
    }
}

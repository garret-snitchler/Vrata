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
    public GameObject TeleportingAsset;
    public GameObject SnapTurnAsset;
    public GameObject worldParentPrefab; 

    [Space]
    [Header("Other")]
    public Transform VRCamera; 


    private bool portalMode = false;
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
            print("A");
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
            print("B");
            if (portalMode)
            {
                ChangePortalType();
            }
        } else if (HitX.GetStateDown(handTypeL))
        {
            print("X");
        } else if (HitY.GetStateDown(handTypeL))
        {
            print("Y");
        } else if (DoubleClickA.GetStateDown(handTypeR))
        {
            SetPortalMode(false);
            Destroy(parentPortal.transform.parent.gameObject);
        } else if (LeftTrigger.GetState(handTypeL)) {
            //set rotation left
            parentPortal.GetComponent<DynamicPortalParent>().Rotate(true, LeftJoystick.GetAxis(handTypeL));
        } else if (RightTrigger.GetState(handTypeR)) {
            //set rotation right
            parentPortal.GetComponent<DynamicPortalParent>().Rotate(false, RightJoystick.GetAxis(handTypeR));
        } else if (portalMode)
        {
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
            TeleportingAsset.SetActive(false);
            SnapTurnAsset.SetActive(false);
        }
        else
        {
            TeleportingAsset.SetActive(true);
            SnapTurnAsset.SetActive(true);
        }
    }
}

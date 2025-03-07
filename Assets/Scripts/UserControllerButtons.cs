using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using System; 

public class UserControllerButtons : MonoBehaviour
{
    // action set
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
    public GameObject portalParentPrefab;
    public GameObject TeleportingAsset;
    public GameObject SnapTurnAsset;

    private bool portalMode = false;
    private GameObject parent;
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
                Vector3 playerPosition = this.gameObject.transform.position;
                Vector3 spawnPoint = playerPosition + new Vector3(0, 1, -2);

                parent = Instantiate(portalParentPrefab, spawnPoint, Quaternion.identity);
                parent.GetComponent<DynamicPortalParent>().GetObjects();
            } else
            {
                SetPortalMode(false);
                parent.GetComponent<DynamicPortalParent>().Activate(); 
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
            Destroy(parent); 
        } else if (portalMode)
        {
            parent.GetComponent<DynamicPortalParent>().MovePortal(LeftJoystick.GetAxis(handTypeL), LeftGrip.GetState(handTypeL), RightJoystick.GetAxis(handTypeR), RightGrip.GetState(handTypeR));
        }
    }

    void ChangePortalType()
    {
        isOneTimePortal = !isOneTimePortal;
        parent.GetComponent<DynamicPortalParent>().SetPortalType(isOneTimePortal);
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

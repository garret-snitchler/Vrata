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
    [Space]
    [Header("Dynamic Portal")]
    public GameObject portalParentPrefab;

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
                portalMode = true;
                Vector3 playerPosition = this.gameObject.transform.position;
                Vector3 spawnPoint = playerPosition + new Vector3(0, 1, -2);

                parent = Instantiate(portalParentPrefab, spawnPoint, Quaternion.identity);
                parent.GetComponent<DynamicPortalParent>().GetObjects();
            } else
            {
                portalMode = false;
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
            portalMode = false; 
            Destroy(parent); 
        }
    }

    void ChangePortalType()
    {
        isOneTimePortal = !isOneTimePortal;
        parent.GetComponent<DynamicPortalParent>().SetPortalType(isOneTimePortal);
    }
}

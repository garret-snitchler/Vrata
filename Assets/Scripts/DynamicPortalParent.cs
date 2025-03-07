using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using System.Threading.Tasks;

public class DynamicPortalParent : MonoBehaviour
{
    public Material bluePortal;
    public Material purplePortal;

    public GameObject leftPortal;
    public GameObject rightPortal;
    public DynamicPortal leftPortalScript;
    public DynamicPortal rightPortalScript;

    private bool isOneTimeUse = true; 

    public void GetObjects()
    {
        this.leftPortal = this.gameObject.transform.GetChild(0).gameObject;
        this.rightPortal = this.gameObject.transform.GetChild(1).gameObject;
        this.leftPortalScript = leftPortal.GetComponent<DynamicPortal>();
        this.rightPortalScript = rightPortal.GetComponent<DynamicPortal>();
        SetPortalType(true);
    }

    public void SetPortalType(bool type)
    {
        isOneTimeUse = type;
        Material color = type ? bluePortal : purplePortal;
        this.leftPortalScript.SetPortalType(type, color);
        this.rightPortalScript.SetPortalType(type, color);
    }

    public void Activate()
    {
        this.leftPortalScript.Activate();
        this.rightPortalScript.Activate();
        if (!isOneTimeUse)
        {
            StartCoroutine(Open15Seconds());
        }
    }

    public void MovePortal(Vector2 leftMovement, bool leftGrip, Vector2 rightMovement, bool rightGrip)
    {
        this.leftPortalScript.Move(leftMovement, leftGrip);
        this.rightPortalScript.Move(rightMovement, rightGrip);
    }

    public GameObject GetMate(GameObject child)
    {
        if (child == leftPortal) return rightPortal;
        if (child == rightPortal) return leftPortal;
        return null; 
    }

    public void OneTimeUsed()
    {
        Destroy(this.gameObject); 
    }

    IEnumerator Open15Seconds()
    {
        yield return new WaitForSeconds(15);
        Destroy(this.gameObject); 
    }
}

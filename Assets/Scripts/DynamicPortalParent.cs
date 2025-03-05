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

    public GameObject entryPortal;
    public GameObject exitPortal;
    public DynamicPortal entryPortalScript;
    public DynamicPortal exitPortalScript;

    private bool isOneTimeUse = true; 

    public void GetObjects()
    {
        this.entryPortal = this.gameObject.transform.GetChild(0).gameObject;
        this.exitPortal = this.gameObject.transform.GetChild(1).gameObject;
        this.entryPortalScript = entryPortal.GetComponent<DynamicPortal>();
        this.exitPortalScript = exitPortal.GetComponent<DynamicPortal>();
        SetPortalType(true);
    }

    public void SetPortalType(bool type)
    {
        isOneTimeUse = type;
        Material color = type ? bluePortal : purplePortal;
        this.entryPortalScript.SetPortalType(type, color);
        this.exitPortalScript.SetPortalType(type, color);
    }

    public void Activate()
    {
        this.entryPortalScript.Activate();
        this.exitPortalScript.Activate();
        if (!isOneTimeUse)
        {
            StartCoroutine(Open15Seconds());
        }
    }

    public GameObject GetMate(GameObject child)
    {
        if (child == entryPortal) return exitPortal;
        if (child == exitPortal) return entryPortal;
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

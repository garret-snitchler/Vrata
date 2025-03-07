using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicPortal : MonoBehaviour
{
    public DynamicPortalParent parentScript; 
    private bool isOneTime = true;
    private bool isActive = false;

    public void SetPortalType(bool isOneTime, Material color)
    {
        this.isOneTime = isOneTime;
        this.gameObject.GetComponent<MeshRenderer>().material = color;
    }
    
    public void Activate()
    {
        isActive = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isActive && other.gameObject.tag != "Portalled")
        {
            other.gameObject.tag = "Portalled"; 

            GameObject mate = parentScript.GetMate(this.gameObject);

            other.gameObject.transform.position = mate.transform.position;

            if (isOneTime)
            {
                parentScript.OneTimeUsed();
            }
        }
    }
}

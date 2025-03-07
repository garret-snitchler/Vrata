using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicPortal : MonoBehaviour
{
    public DynamicPortalParent parentScript; 
    private bool isOneTime = true;
    private bool isActive = false;
    private Rigidbody rigidbody; 

    public void Awake()
    {
        this.rigidbody = this.gameObject.GetComponent<Rigidbody>();
    }

    public void SetPortalType(bool isOneTime, Material color)
    {
        this.isOneTime = isOneTime;
        this.gameObject.GetComponent<MeshRenderer>().material = color;
    }
    
    public void Activate()
    {
        isActive = true;
    }

    public void Move(Vector2 movement, bool isVertical)
    {
        if (movement == Vector2.zero)
        {
            this.rigidbody.velocity = movement;
        }
        else if (isVertical)
        {
            print("is vertical"); 
            rigidbody.AddForce(new Vector3(0, -movement.y, 0)); 
        }
        else
        {
            rigidbody.AddForce(new Vector3(-movement.x, 0, -movement.y));
        }
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

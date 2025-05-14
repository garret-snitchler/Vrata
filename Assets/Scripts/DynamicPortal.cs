using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using System;

public class DynamicPortal : MonoBehaviour
{
    public DynamicPortalParent parentScript;

    public GameObject VortexFront;
    public GameObject VortexBack;
    public AudioClip PortalNoise;
    public AudioSource PortalSource;
    public AudioClip PortalMoveUp;
    public AudioClip PortalMoveSide;

    private bool isOneTime = true;
    private bool isActive = false;
    private Rigidbody rigidbody;
    private Hand hand;
    private bool isMakingSound = false; 


    public void Awake()
    {
        this.rigidbody = this.gameObject.GetComponent<Rigidbody>();
    }

    public void SetInput(bool isLeft)
    {
        this.hand = isLeft ? Player.instance.leftHand : Player.instance.rightHand;
    }

    public void SetPortalType(bool isOneTime, Material color)
    {
        this.isOneTime = isOneTime;
        this.gameObject.GetComponent<MeshRenderer>().material = color;
    }
    
    public void Activate()
    {
        isActive = true;
        PortalSource.PlayOneShot(PortalNoise);
        VortexFront.SetActive(true);
        VortexBack.SetActive(true);
    }

    public void Move(Vector2 movement, bool isVertical)
    {
        print(movement);
        movement *= 150; 

        if (movement == Vector2.zero)
        {
            this.rigidbody.velocity = movement;
        }
        else if (isVertical)
        {
            print("is vertical"); 
            rigidbody.AddRelativeForce(new Vector3(0, movement.y, 0));
            if (!isMakingSound)
            {
                isMakingSound = true;
                PortalSource.PlayOneShot(PortalMoveUp);
                StartCoroutine(WaitAndReset()); 
            }
        }
        else
        {
            rigidbody.AddRelativeForce(new Vector3(-movement.y, 0, movement.x));
            if (!isMakingSound)
            {
                isMakingSound = true;
                PortalSource.PlayOneShot(PortalMoveSide);
                StartCoroutine(WaitAndReset());
            }
        }
    }

    private IEnumerator WaitAndReset()
    {
        yield return new WaitForSeconds(2.5f);
        isMakingSound = false;
    }

    public void Rotate(Vector2 joystickMovement)
    {
        this.gameObject.transform.Rotate(new Vector3(0, joystickMovement.x, joystickMovement.y));
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

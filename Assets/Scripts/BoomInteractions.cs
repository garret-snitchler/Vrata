using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class BoomInteractions : MonoBehaviour
{
    public Rigidbody rb;

    private bool hasBeenPickedUp = false;

    void Update()
    {
        if (!hasBeenPickedUp && (gameObject.transform.parent?.name == "LeftHand" ||
            gameObject.transform.parent?.name == "RightHand"))
        {
            print("picked up");
            hasBeenPickedUp = true;
        }
        else if (hasBeenPickedUp && (gameObject.transform.parent == null &&
            gameObject.transform.parent == null))
        {
            print("thrown");
            rb.useGravity = true;
        }
    }
}
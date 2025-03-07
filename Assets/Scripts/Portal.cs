using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    // currently just using this for the purpose of having one portal keep track of the portal it's paired with, other things can be added as needed
    public GameObject otherPortal; // this is the portal that is paired with this one

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        print("THIS TRIGGERED");
        if (collider.gameObject.tag != "Portalled")
        {
            GameObject gameObject = collider.gameObject;
            gameObject.transform.parent.parent.transform.position = otherPortal.transform.position;

            gameObject.tag = "Portalled";
        }
    }
}

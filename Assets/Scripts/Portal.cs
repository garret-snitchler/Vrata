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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            GameObject player = collision.gameObject;
            player.transform.position = otherPortal.transform.position;
        }
    }
}

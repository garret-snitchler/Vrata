using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        print("PORTAL ENTERED");
        GameObject gameObject = collider.gameObject;
        //print(gameObject.tag);
        if (gameObject.tag != "Portalled")
        {
            //originalItem = gameObject;
            //originalItem.tag = gameObject.tag; // store object's original tag so we can return it later
            gameObject.transform.parent.parent.transform.position = otherPortal.transform.position;
            gameObject.tag = "Portalled";
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        print("PORTAL EXITED");
        //print(collider.gameObject.tag);
        if (collider.gameObject.tag == "Portalled")
        {
            StartCoroutine(waitThreeSeconds(collider.gameObject));
            //collider.gameObject.tag = originalItem.tag;
            // remove item data from equation
        }
        //originalItem = null;
        //print(collider.gameObject.tag);
    }

    private IEnumerator waitThreeSeconds(GameObject gameObject)
    {
        yield return new WaitForSeconds(3);
        gameObject.tag = "Player";
    }
}

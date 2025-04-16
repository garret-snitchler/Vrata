using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement; 

// This class represents a static portal, something only usable by players. It currently assumes that both portals are within the same scene, and contains no code for visualizing the other end of the portal. If these assumptions are broken, additional coding will be needed.
public class StaticPortal : MonoBehaviour
{
    // currently just using this for the purpose of having one portal keep track of the portal it's paired with, other things can be added as needed
    public GameObject otherPortal; // this is the portal that is paired with this one
    public string NextScene; 

    private void OnTriggerEnter(Collider collider)
    {
        GameObject gameObject = collider.gameObject;
        if (gameObject.tag == "Player")
        {
            if (otherPortal != null)
            {
                gameObject.transform.parent.parent.transform.position = otherPortal.transform.position;
                gameObject.tag = "Portalled";
            } else
            {
                print("Trigger scene change"); 
                SceneManager.LoadScene(NextScene);
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Portalled")
        {
            // give player time to step out of portal
            StartCoroutine(waitThreeThenResetTag(collider.gameObject));
        }
    }

    private IEnumerator waitThreeThenResetTag(GameObject gameObject)
    {
        yield return new WaitForSeconds(3);
        gameObject.tag = "Player";
    }
}

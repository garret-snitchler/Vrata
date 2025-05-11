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
    public WeaponHandler weaponHandlerScript; 
    public WinScript winScript;

    private void OnTriggerEnter(Collider collider)
    {
        GameObject gameObject = collider.gameObject;
        if (gameObject.tag == "Player")
        {
            GameObject player = GameObject.Find("Player");
            if (otherPortal != null && player != null)
            {
                StartCoroutine(player.GetComponent<PlayerHUD>().FadeBlackOutSquare(true));
                StartCoroutine(WaitThreeThenPortal(gameObject, player));
            }
            StartCoroutine(waitSixThenResetTag(gameObject));
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Portalled")
        {
            // give player time to step out of portal
            StartCoroutine(waitSixThenResetTag(collider.gameObject));
        }
    }

    private IEnumerator WaitThreeThenPortal(GameObject gameObject, GameObject player)
    {
        yield return new WaitForSeconds(2.5f); //wait for fade to black
        player.transform.position = otherPortal.transform.position;
        gameObject.tag = "Portalled";
    }

    private IEnumerator waitSixThenResetTag(GameObject gameObject)
    {
        yield return new WaitForSeconds(5); //wait for fade to black + exit time
        gameObject.tag = "Player";

        int count = weaponHandlerScript != null ? weaponHandlerScript.NumGemsUnlocked() : 0; 
        if (otherPortal.name == "Spawn Point - Valley" && count == 3)
        {
            //You've won all three gems: initiate win sequence. 
            winScript.InitializeBossFight(); 
        }
    }
}

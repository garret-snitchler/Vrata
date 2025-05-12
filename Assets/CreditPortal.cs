using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class CreditPortal : MonoBehaviour
{
    public Player player;

    private void OnTriggerEnter(Collider collider)
    {
        GameObject gameObject = collider.gameObject;
        if (gameObject.tag == "Player")
        {
            // start credits sequence
            StartCoroutine(player.GetComponent<PlayerHUD>().FadeBlackOutSquare(true, false, true));
        }
    }
}

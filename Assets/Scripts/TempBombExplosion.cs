using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempBombExplosion : MonoBehaviour
{
    public void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            this.gameObject.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            StartCoroutine(Kaboom());
        }
    }

    IEnumerator Kaboom()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject); 
    }
}

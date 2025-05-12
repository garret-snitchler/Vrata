using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempBombExplosion : MonoBehaviour
{

    public AudioSource boomSource;
    public AudioClip boomClip;
    public void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            this.gameObject.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            boomSource = GetComponent<AudioSource>();
            StartCoroutine(Kaboom());
        }
    }

    IEnumerator Kaboom()
    {
        yield return new WaitForSeconds(2);
        boomSource.PlayOneShot(boomClip);
        Destroy(this.gameObject);
    }
}

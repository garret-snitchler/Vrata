using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealsDamage : MonoBehaviour
{
    public int baseDamage = 10;
    public int powerupDamage = 30;

    public bool isPoweredUp = false; 

    public void IsPoweredUp()
    {
        isPoweredUp = true;
        print("is power up = true in DealsDamage");
    }

    public void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            var script = coll.gameObject.GetComponent<Enemy>();
            if (script != null)
            {
                script.TakeDamage(isPoweredUp ? powerupDamage : baseDamage);
            }
        }
    }
}

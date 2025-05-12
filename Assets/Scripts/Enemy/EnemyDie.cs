using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderDie : MonoBehaviour
{
    public Enemy enemyScript; 

    void Update()
    {
        if (enemyScript.health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempColorChange : MonoBehaviour
{
    public Enemy enemyScript; 

    void Update()
    {
        var mat = this.gameObject.GetComponent<MeshRenderer>().material;
        if (enemyScript.health > 0)
        {
            float c = (enemyScript.health / 100f);
            mat.color = new Color(1, c, c);
        } else
        {
            mat.color = Color.black; 
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        print(coll.gameObject.name);
    }
}

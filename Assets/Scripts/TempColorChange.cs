using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempColorChange : MonoBehaviour
{
    public Enemy enemyScript; 

    void Update()
    {
        var mat = this.gameObject.GetComponent<MeshRenderer>().material;
        mat.color = new Color(enemyScript.health / 255, 0, 0);
    }
}

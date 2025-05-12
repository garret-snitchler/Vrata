using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasBombs : MonoBehaviour
{

    void Awake()
    {
        GameObject.Find("Bomb Handler").GetComponent<BoomSpawner>().enabled = true; 
    }

    private void OnDestroy()
    {
        GameObject.Find("Bomb Handler").GetComponent<BoomSpawner>().enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBoss : MonoBehaviour
{
    public Enemy enemyScript;
    private bool hasGem = true;
    public GameObject gemPrefab;
    public Transform gemSpawnPoint;
    public int levelNum;
    public WeaponHandler weaponHandlerScript; 

    void Update()
    {
        if (enemyScript.health <= 0 && hasGem)
        {
            hasGem = false; 
            print("give gem");
            GameObject gem = Instantiate(gemPrefab, gemSpawnPoint.position, Quaternion.identity);
            gem.transform.GetChild(levelNum).gameObject.SetActive(true);
            gem.transform.SetParent(gemSpawnPoint);
            weaponHandlerScript.gemsUnlocked[levelNum] = true;
        }
    }
}

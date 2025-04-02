using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowInteraction : MonoBehaviour
{
    public GameObject arrowFeatherObj; 
    public List<Material> arrowFeatherMats = new List<Material>();
    public Valve.VR.InteractionSystem.FireSource fireOnScript; 

    private int currentColor = 0;

    private List<(Color, Color)> fireColors = new List<(Color, Color)>()
    {
        (new Color(1f,0f,0f), new Color(1f, 0.651f, 0f)), 
        (new Color(0f, 0f, 1f), new Color(0f, 0.392f, 1f)), 
        (new Color(0.031f, 0.324f, 0f), new Color(0.078f, 0.039f, 0f))
    };

    public void OnCollisionEnter(Collision coll)
    {
        print(coll.gameObject.name);
        StopPowerup();
    }

    public void ChangeColor(int numUnlocked)
    {
        if (currentColor + 1 > numUnlocked)
        {
            currentColor = 0;
        }
        else
        {
            currentColor += 1;
        }

        //arrowFeatherObj.GetComponent<MeshRenderer>().material = arrowFeatherMats[currentColor];
        
        if (fireOnScript.isBurning)
        {
            ParticleSystem.MainModule ps = fireOnScript.gameObject.transform.GetChild(1).GetChild(0).GetComponent<ParticleSystem>().main;
            ps.startColor = new ParticleSystem.MinMaxGradient(fireColors[currentColor].Item1, fireColors[currentColor].Item2);
        }
    }

    public void UsePowerup()
    {
        fireOnScript.FireExposure();
        StartCoroutine(WaitThenChangeColor()); 
    }

    IEnumerator WaitThenChangeColor()
    {
        yield return new WaitForSeconds(0.5f);
        ParticleSystem.MainModule ps = fireOnScript.gameObject.transform.GetChild(1).GetChild(0).GetComponent<ParticleSystem>().main;
        ps.startColor = new ParticleSystem.MinMaxGradient(fireColors[currentColor].Item1, fireColors[currentColor].Item2);
    }

    public void StopPowerup()
    {
        fireOnScript.isBurning = false; 
    }
}

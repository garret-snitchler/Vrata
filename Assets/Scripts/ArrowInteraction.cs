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
        (new Color(1f,0f,0f), new Color(1f, 0.651f, 0f)), //red
        (new Color(0.031f, 0.324f, 0f), new Color(0.078f, 0.039f, 0f)), //green
        (new Color(0f, 0f, 1f), new Color(0f, 0.392f, 1f)) //blue
    };

    public void OnCollisionEnter(Collision coll)
    {
        print(coll.gameObject.name);
        StopPowerup();
        StartCoroutine(YEET());
    }

    public void SetColor(int co)
    {
        print("arrow color: " + co);
        currentColor = co;
        arrowFeatherObj.GetComponent<MeshRenderer>().material = arrowFeatherMats[currentColor];
    }

    public int ChangeColor(List<bool> gems, int numUnlocked)
    {
        ChangeCurrentColorIndex(gems, numUnlocked); 

        arrowFeatherObj.GetComponent<MeshRenderer>().material = arrowFeatherMats[currentColor];
        
        if (fireOnScript.isBurning && currentColor > 0)
        {
            ParticleSystem.MainModule ps = fireOnScript.gameObject.transform.GetChild(1).GetChild(0).GetComponent<ParticleSystem>().main;
            ps.startColor = new ParticleSystem.MinMaxGradient(fireColors[currentColor-1].Item1, fireColors[currentColor-1].Item2);
        } else
        {
            StopPowerup();
        }

        return currentColor; 
    }

    private void ChangeCurrentColorIndex(List<bool> gems, int numUnlocked)
    {
        if (numUnlocked == 0)
        {
            currentColor = 0;
            return;
        }

        int count = 0; 
        while (count < 4)
        {
            currentColor += 1;
            if (currentColor > 3)
            {
                currentColor = 0;
                return;
            }

            if (gems[currentColor - 1])
            {
                return;
            }
            count++;
        }
    }

    public void UsePowerup()
    {
        if (currentColor > 0)
        {
            fireOnScript.FireExposure();
            StartCoroutine(WaitThenChangeColor());
        }
    }

    IEnumerator WaitThenChangeColor()
    {
        yield return new WaitForSeconds(0.01f);
        ParticleSystem.MainModule ps = fireOnScript.gameObject.transform.GetChild(1).GetChild(0).GetComponent<ParticleSystem>().main;
        ps.startColor = new ParticleSystem.MinMaxGradient(fireColors[currentColor-1].Item1, fireColors[currentColor-1].Item2);
    }

    public void StopPowerup()
    {
        fireOnScript.isBurning = false;
        if (this.gameObject.transform.GetChild(1).childCount > 1)
        {
            Destroy(this.gameObject.transform.GetChild(1).GetChild(1).gameObject);
        }
    }

    public bool UsingSpecialArrow()
    {
        return currentColor > 0; 
    }

    IEnumerator YEET()
    {
        yield return new WaitForSeconds(0.01f);
        Destroy(this.gameObject);
    }
}

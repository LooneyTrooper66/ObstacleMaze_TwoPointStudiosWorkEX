using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHit : MonoBehaviour, IHittable
{
    bool lerpColor = false;
    float lerpTime = 1.5f;
    float currentLerpTime;
    Material material;
    Color startingColour;


    private void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        startingColour = material.color;
        currentLerpTime = lerpTime;
    }

    private void Update()
    {
        if (lerpColor == false) return;

        if (lerpColor)
        {
            material.color = Color.Lerp(material.color, startingColour, Time.deltaTime);
            currentLerpTime -= Time.deltaTime;
        }

        // makes sure lerp is turned off 
        // lerp doesnt make the colour match perfectly so have to use a timer instead of a comparison
        if (currentLerpTime <= 0)
        {
            material.color = startingColour;
            lerpColor = false;
            currentLerpTime = lerpTime;
            //Debug.Log("stopped lerp");
        }
    }

    public void ChangeColour()
    {
        material.color = Color.red;
        lerpColor = true;
    }
}

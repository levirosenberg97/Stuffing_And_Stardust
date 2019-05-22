using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowControlScript : MonoBehaviour
{
    public List<Material> mats;
    public bool lightAdded;
    public float startingLight;
    public ParticleSystem smoke;
    public Vector4 lightCenter;
    public float darkness;
    float slow = 4;

    LightCrystalTrackingScript tracker;
    Vector3 startingPos;
    
    float lightWidth;
    float currentWidth;

    

    IEnumerator Start()
    {
        tracker = GameObject.FindObjectOfType<LightCrystalTrackingScript>();
        if (tracker != null)
        {
            startingLight = tracker.lightCrystals;
        }
        foreach (Material mat in mats)
        {
            mat.SetFloat("_LightDistance", startingLight);
            currentWidth = mat.GetFloat("_LightDistance");
            lightWidth = currentWidth;

            mat.SetFloat("_ShadowDarkness", darkness);

            var smokeShape = smoke.shape;
            smokeShape.radius = lightWidth;

            mat.SetVector("_LightOrigin", lightCenter);
            startingPos = mat.GetVector("_LightOrigin");
            smoke.transform.position = startingPos;
        }

        if (lightAdded == true)
        {
            lightWidth = tracker.currentCrystals;
            lightAdded = false;
        }

        yield return new WaitForSeconds(.2f);

        while (currentWidth != lightWidth)
        {
            foreach (Material mat in mats)
            {
                currentWidth = Mathf.Lerp(currentWidth, lightWidth, Time.deltaTime / slow);
                var smokeShape = smoke.shape;
                smokeShape.radius = currentWidth;
                mat.SetFloat("_LightDistance", currentWidth);
            }
            if(currentWidth >= lightWidth - .01f)
            {
                break;
            }
            yield return new WaitForSeconds(.01f);
        }
        yield return null;
    }
}

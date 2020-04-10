using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class underwater : MonoBehaviour
{
    private float waterLevel;
    private bool isUnderwater;
    private Color underwaterColor;

    // Start is called before the first frame update
    void Start()
    {
        waterLevel = 5.55f;
        underwaterColor = new Color(0.1014f, 0.2348f, 0.4056f, 0.5f);

    }

    // Update is called once per frame
    void Update()
    {

        if ((Camera.main.transform.position.y < waterLevel) != isUnderwater)
        {
            isUnderwater = Camera.main.transform.position.y < waterLevel;
            SetFogColor(isUnderwater);
        }

    }

    // Set fog color based on underwater or not
    void SetFogColor(bool isUnderwater)
    {
        if (isUnderwater)
        {
            RenderSettings.fog = true;
            RenderSettings.fogColor = underwaterColor;
            RenderSettings.fogDensity = 0.15f;
        }
        else
        {
            RenderSettings.fog = false;
        }
    }
}
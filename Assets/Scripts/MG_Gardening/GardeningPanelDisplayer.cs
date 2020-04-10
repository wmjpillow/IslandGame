using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GardeningPanelDisplayer : MonoBehaviour
{
    public GameObject everything;
    public Slider slider_moist;
    public Slider slider_progress;
    public Slider healthiness;
    public Image slidercolor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void displayPanel(Soil soil, float x, float y){
        if(soil.GetPlant() == null){
            return;
        }
        gameObject.SetActive(true);
        Vector3 newpos = gameObject.transform.position;
        newpos.x = x;
        newpos.y = y;
        everything.transform.position = newpos;

        Color warning = new Color();
        warning.r = 255.0f/255.0f;
        warning.g = 63/255.0f;
        warning.b = 63/255.0f;
        warning.a = 1.0f;

        Color safe = new Color();
        safe.r = 63/255.0f;
        safe.g = 195/255.0f;
        safe.b = 255/255.0f;
        safe.a = 1.0f;

        
        float moistlevel = (soil._moistLevel)/ 200.0f; // This could go off 250.f as well.
        if(moistlevel < 0.25f || moistlevel > 0.75){ //In danger zone
            slidercolor.color = warning;
        }
        else{
            slidercolor.color = safe;
        }
    

        slider_moist.value = Mathf.Clamp((soil._moistLevel)/ 200.0f, 0.0f, 1.0f);


        float ttg = GardeningManager.Instance.GetPlantData(soil.GetPlant()._plantName).nextPhaseTime;
        float tp = soil.GetPlant().GetTimeAlive();
        slider_progress.value = Mathf.Clamp(tp/ttg, 0.0f, 1.0f);
        healthiness.value = Mathf.Clamp((soil._healthiness) / 100.0f, 0.0f, 1.0f);
    }

    public void removePanel(){
        gameObject.SetActive(false);
    }
}

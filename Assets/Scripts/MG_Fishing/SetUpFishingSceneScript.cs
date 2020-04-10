using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpFishingSceneScript : MonoBehaviour
{
    public GameObject fishing_rod;
    public GameObject fishing_panel;
    //public Camera cam;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnMouseDown()
    {
        Invoke("ShowRod", 1f);
    }

    private void ShowRod()
    {
        fishing_rod.SetActive(true);
        fishing_panel.SetActive(true);
    }
}

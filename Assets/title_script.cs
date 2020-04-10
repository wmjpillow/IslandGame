using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class title_script : MonoBehaviour
{

    public GameObject gardenOverlay;

    // Start is called before the first frame update
    void Start()
    {
        gardenOverlay.SetActive(false);
        Time.timeScale = 0f;
        GameObject.Find("EndText").SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame()
    {
        Time.timeScale = 1f;
        gardenOverlay.SetActive(true);
        GameObject.Find("TitleCanvas").SetActive(false);
        GlobalValueTracker.Instance.transport1.SetActive(true);
        GlobalValueTracker.Instance.transport2.SetActive(true);
        GlobalValueTracker.Instance.transport3.SetActive(true);
        GlobalValueTracker.Instance.transport4.SetActive(true);
        GlobalValueTracker.Instance.transport5.SetActive(true);
        print("RIGHT HERE");
    }
}

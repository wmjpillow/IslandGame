using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level4 : MonoBehaviour
{

    float timeLeft;
    // Start is called before the first frame update
    void Start()
    {
        timeLeft = 15;
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalValueTracker.Instance.day == 4 && (CameraMove.Instance.GetCurrentSceneName().Equals("Swimming")))
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                GlobalValueTracker.Instance.g3 = true;
            }
        }
    }
}

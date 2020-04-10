using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class butterfly_net : MonoBehaviour
{
    private Vector3 mousePosition;
    float previousX = -1000;
    string current, previous;
    Vector3 to;
    float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.Rotate(0.0f, 90.0f, 90.0f, Space.Self);
        current = "right";
        previous = "NULL";
        to = new Vector3(0, 90, 90);
        timeLeft = 15;
    }

    // Update is called once per frame
    void Update()
    {

        if (GlobalValueTracker.Instance.day == 4)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                GlobalValueTracker.Instance.g1 = true;
            }
        }

        if (!CameraMove.Instance.GetCurrentSceneName().Equals("Butterfly"))
        {
            Destroy(this.gameObject);
        }

        Vector3 temp = Input.mousePosition;
        temp.z = 1.8f; // Set this to be the distance you want the object to be placed in front of the camera.
        this.transform.position = Camera.main.ScreenToWorldPoint(temp);



        previous = current;
        if (previousX != -1000)
        {
            if (this.transform.position.x < previousX && current != "left")
            {
                current = "left";
            }
            if (this.transform.position.x > previousX && current != "right")
            {
                current = "right";
            }
        }

        previousX = this.transform.position.x;

        if (current == "left" && previous == "right" || (current == "left" && previous == "left"))
        {
            Vector3 to = new Vector3(0, 270, 90);
            transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, to, Time.deltaTime * 10);
        }
        if (current == "right" && previous == "left" || (current == "right" && previous == "right"))
        {
            Vector3 to = new Vector3(0, 90, 90);
            transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, to, Time.deltaTime * 10);
        }
    }
}

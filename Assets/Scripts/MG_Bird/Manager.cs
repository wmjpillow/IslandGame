using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    static Manager instance=null;
    public Transform light;
    public GameObject bird1;
    public GameObject bird2;
    public GameObject bird3;
    public List<GameObject> birds;
    public float speed=10;
    public int BirdANum = 0;
    public int BirdBNum = 0;
    public int BirdCNum = 0;
    public Vector3 targetPosition;
    public static Manager Instance { get{
            if (instance==null)
            {
                GameObject go = GameObject.Find("Manager");
                if (go == null)
                {
                    go = new GameObject("Manager");
                }
                instance=go.AddComponent<Manager>();
            }
            return instance;
        } }

    // Start is called before the first frame update
    void Start()
    {
        if (!instance)
        instance = this;
        birds = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(light)
        {
            light.Rotate(new Vector3(speed, 0, 0) * Time.deltaTime);
            //light.localEulerAngles +=new Vector3(speed, 0, 0)  * Time.deltaTime;
        }
        if (bird1)
        {
            if (light.eulerAngles.x > 30&& light.eulerAngles.x < 31)
            {
                GameObject go = Instantiate(bird1);
                go.GetComponent<Bird>().targetPosition = targetPosition;
                birds.Add(go);
                BirdANum++;
            }
        }
        if(bird2)
        {
            if (light.eulerAngles.x > 40 && light.eulerAngles.x < 41)
            {
                GameObject go = Instantiate(bird2);
                go.GetComponent<Bird>().targetPosition = targetPosition;
                birds.Add(go);
                BirdBNum++;
            }
        }
        if (bird3)
        {
            if (light.eulerAngles.x > 60 && light.eulerAngles.x < 61)
            {
                GameObject go = Instantiate(bird3);
                go.GetComponent<Bird>().targetPosition = targetPosition;
                birds.Add(go);
                BirdCNum++;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdManager : MonoBehaviour
{
    static BirdManager instance=null;
    public Transform light;
    public GameObject FinchLeft;
    public GameObject FinchRight;
    public GameObject SparrowLeft;
    public List<GameObject> birds;
    public float speed=10;
    public int BirdANum = 0;
    public int BirdBNum = 0;
    public int BirdCNum = 0;
    public Vector3 targetPosition;
    public static BirdManager Instance { get{
            if (instance==null)
            {
                GameObject go = GameObject.Find("BirdManager");
                if (go == null)
                {
                    go = new GameObject("BirdManager");
                }
                instance=go.AddComponent<BirdManager>();
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
        if (FinchLeft)
        {
            if (light.eulerAngles.x > 30&& light.eulerAngles.x < 31)
            {
                GameObject go = Instantiate(FinchLeft);
                go.GetComponent<Bird>().targetPosition = targetPosition;
                birds.Add(go);
                BirdANum++;
            }
        }
        if(FinchRight)
        {
            if (light.eulerAngles.x > 40 && light.eulerAngles.x < 41)
            {
                GameObject go = Instantiate(FinchRight);
                go.GetComponent<Bird>().targetPosition = targetPosition;
                birds.Add(go);
                BirdBNum++;
            }
        }
        if (SparrowLeft)
        {
            if (light.eulerAngles.x > 60 && light.eulerAngles.x < 61)
            {
                GameObject go = Instantiate(SparrowLeft);
                go.GetComponent<Bird>().targetPosition = targetPosition;
                birds.Add(go);
                BirdCNum++;
            }
        }
    }
}

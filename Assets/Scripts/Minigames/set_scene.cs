using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class set_scene : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject butterfly;
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject net;
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject milkweed;

    // This script will simply instantiate the Prefab when the game starts.
    void Start()
    {

    }

    void OnMouseDown()
    {
        // Instantiate at position (0, 0, 0) and zero rotation.
        Invoke("spawn", 1f);
    }

    public void spawn()
    {
        Vector3 v3Pos;
        if (GlobalValueTracker.Instance.day < 4)
        {
            v3Pos = Camera.main.ViewportToWorldPoint(new Vector3(-0.05f, Random.Range(0.01f, 0.99f), 2f)); //butterflies
            Instantiate(butterfly, v3Pos, Quaternion.identity);
            print("Butterfly instantiated");
        }

        v3Pos = Camera.main.ViewportToWorldPoint(new Vector3(.5f, .5f, 2f)); //net
        Instantiate(net, v3Pos, Quaternion.identity);
        net.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);
        print("Net instantiated");

        if(GlobalValueTracker.Instance.day == 1) {
        Instantiate(milkweed, new Vector3(-5.41f, 7.16f, -3.66f), Quaternion.identity);
        Instantiate(milkweed, new Vector3(1.08f, 7.38f, -5.23f), Quaternion.identity);
        Instantiate(milkweed, new Vector3(-1.87f, 6.67f, -2.96f), Quaternion.identity);
        Instantiate(milkweed, new Vector3(5.3f, 6.75f, -0.01f), Quaternion.identity);
        }
        if (GlobalValueTracker.Instance.day == 2)
        {
            Instantiate(milkweed, new Vector3(-1.87f, 6.67f, -2.96f), Quaternion.identity);
            Instantiate(milkweed, new Vector3(5.3f, 6.75f, -0.01f), Quaternion.identity);
        }
        if (GlobalValueTracker.Instance.day == 3)
        {
            Instantiate(milkweed, new Vector3(-5.41f, 7.16f, -3.66f), Quaternion.identity);
        }

        print("Milkweeds instantiated");
    }
}

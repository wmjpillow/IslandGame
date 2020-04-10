﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class set_scene_bird : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject bird1;
    public GameObject bird2;
    float timeLeft;

    // This script will simply instantiate the Prefab when the game starts.
    void Start()
    {
        timeLeft = 15;
    }

    void OnMouseDown()
    {
        // Instantiate at position (0, 0, 0) and zero rotation.
        Invoke("spawn", 1f);
    }

    public void spawn()
    {
        if (GlobalValueTracker.Instance.day < 4)
        {
            Vector3 v3Pos = Camera.main.ViewportToWorldPoint(new Vector3(-0.05f, Random.Range(0.01f, 0.99f), 5f)); //butterflies
            Instantiate(bird1, v3Pos, Quaternion.identity);
            print("bird1 instantiated");
        }
    }
}

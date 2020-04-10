using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openClose : MonoBehaviour
{
    public GameObject ob;
    public KeyCode key;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            if (ob.active)
                ob.SetActive(false);
            else
                ob.SetActive(true);
        }
    }
}

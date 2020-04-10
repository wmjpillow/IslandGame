using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class milkweed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.transform.position.x != -0.01 && Camera.main.transform.position.y != 1 && //make sure net should still be spawned
            Camera.main.transform.position.z != -10)
        {
            Destroy(this.gameObject);
        }
    }
}

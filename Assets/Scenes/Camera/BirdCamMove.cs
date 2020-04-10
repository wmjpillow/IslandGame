using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdCamMove : MonoBehaviour
{
    // Keep track of last mouse position
    private Vector3 lastMousePos;
    
    // Start is called before the first frame update
    void Start()
    {
        lastMousePos = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // Check for Birdwatching minigame for camera movement
        if (CameraMove.Instance.GetCurrentSceneName().Equals("Birdwatching"))
        {

            // Check for mouse movement
            // if (Input.GetAxis("Mouse X") != 0 && Input.GetAxis("Mouse Y") != 0)
            // {
            //         transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * 20.0f,
            //             Input.GetAxisRaw("Mouse Y") * Time.deltaTime * 20.0f,
            //             0.0f);
            // }
        }
    }
}

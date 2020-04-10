using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysFaceCamera : MonoBehaviour
{
    // Start is called before the first frame update

        private Camera _mainCamera;
    void Start()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //Face towards the camera.
        Vector3 cameraPos = _mainCamera.transform.position;
        Vector3 back = transform.position - cameraPos;

        transform.forward = back.normalized;
    }
}

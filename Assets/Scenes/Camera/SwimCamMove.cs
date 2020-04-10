using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimCamMove : MonoBehaviour
{
    // Movement speed for swimming minigame
    private static float swimmingMoveSpeed = 10.0f;
    
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
        // Check for swimming minigame for camera movement
        if (CameraMove.Instance.GetCurrentSceneName().Equals("Swimming"))
        {

            // Check for mouse movement
            // if (Input.GetAxis("Mouse X") != 0 && Input.GetAxis("Mouse Y") != 0)
            // {
            //     // No reaching bounds
            //     if (transform.position.x > 20.0f && transform.position.x < 30.0f &&
            //         transform.position.y > 3.0f && transform.position.y < 5.0f)
            //     {
            //         transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * swimmingMoveSpeed,
            //             Input.GetAxisRaw("Mouse Y") * Time.deltaTime * swimmingMoveSpeed,
            //             0.0f);
            //     }

            //     // Otherwise reaching bounds
            //     else
            //     {                    
            //         // Compare current mouse position to last stored one
            //         Vector3 mouseDelta = Input.mousePosition - lastMousePos;
                    
            //         // Reach left x axis bound
            //         if (transform.position.x <= 20.0f) {
            //             // If not on y axis bounds either
            //             if (transform.position.y > 3.0f && transform.position.y < 5.0f)
            //             {
            //                 // If mouse moving right, resume motion
            //                 if (mouseDelta.x > 0)
            //                 {
            //                     transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * swimmingMoveSpeed,
            //                         Input.GetAxisRaw("Mouse Y") * Time.deltaTime * swimmingMoveSpeed,
            //                         0.0f);
            //                 }
            //                 else
            //                 {
            //                     transform.position += new Vector3(0.0f,
            //                         Input.GetAxisRaw("Mouse Y") * Time.deltaTime * swimmingMoveSpeed,
            //                         0.0f);
            //                 }
            //             }
            //             else
            //             {
            //                 // If mouse moving right, resume motion
            //                 if (mouseDelta.x > 0)
            //                 {
            //                     transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * swimmingMoveSpeed,
            //                         0.0f, 0.0f);
            //                 }
            //                 else
            //                 {
            //                     transform.position += new Vector3(0.0f, 0.0f, 0.0f);
            //                 }
            //             }
            //         }
                    
            //         // Reach right x axis bound
            //         else if (transform.position.x >= 30.0f)
            //         {                        
            //             // If not on y axis bounds either
            //             if (transform.position.y > 3.0f && transform.position.y < 5.0f)
            //             {
            //                 // If mouse moving right, resume motion
            //                 if (mouseDelta.x <0)
            //                 {
            //                     transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * swimmingMoveSpeed,
            //                         Input.GetAxisRaw("Mouse Y") * Time.deltaTime * swimmingMoveSpeed,
            //                         0.0f);
            //                 }
            //                 else
            //                 {
            //                     transform.position += new Vector3(0.0f,
            //                         Input.GetAxisRaw("Mouse Y") * Time.deltaTime * swimmingMoveSpeed,
            //                         0.0f);
            //                 }
            //             }
            //             else
            //             {
            //                 // If mouse moving right, resume motion
            //                 if (mouseDelta.x < 0)
            //                 {
            //                     transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * swimmingMoveSpeed,
            //                         0.0f, 0.0f);
            //                 }
            //                 else
            //                 {
            //                     transform.position += new Vector3(0.0f, 0.0f, 0.0f);
            //                 }
            //             }                        
            //         }
                
            //         // Reach bottom y axis bound
            //         else if (transform.position.y <= 3.0f)
            //         {          
            //             // If not on either x axis border
            //             if (transform.position.x > 20.0f && transform.position.x < 30.0f)
            //             {
            //                 // If mouse moving up, resume motion
            //                 if (mouseDelta.y > 0)
            //                 {
            //                     transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * swimmingMoveSpeed,
            //                         Input.GetAxisRaw("Mouse Y") * Time.deltaTime * swimmingMoveSpeed,
            //                         0.0f);
            //                 }
            //                 else
            //                 {
            //                     transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * swimmingMoveSpeed, 
            //                         0.0f, 0.0f);
            //                 }    
            //             }
            //             else
            //             {
            //                 // If mouse moving up, resume motion
            //                 if (mouseDelta.y > 0)
            //                 {
            //                     transform.position += new Vector3(0.0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * swimmingMoveSpeed,
            //                         0.0f);
            //                 }
            //                 else
            //                 {
            //                     transform.position += new Vector3(0.0f, 0.0f, 0.0f);
            //                 }    
            //             }
            //         }
                    
            //         // Reach top y axis bound
            //         else if(transform.position.y >= 5.0f)
            //         {
            //             // If not on either x axis border
            //             if (transform.position.x > 20.0f && transform.position.x < 30.0f)
            //             {
            //                 // If mouse moving up, resume motion
            //                 if (mouseDelta.y < 0)
            //                 {
            //                     transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * swimmingMoveSpeed,
            //                         Input.GetAxisRaw("Mouse Y") * Time.deltaTime * swimmingMoveSpeed,
            //                         0.0f);
            //                 }
            //                 else
            //                 {
            //                     transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * swimmingMoveSpeed, 
            //                         0.0f, 0.0f);
            //                 }    
            //             }
            //             else
            //             {
            //                 // If mouse moving up, resume motion
            //                 if (mouseDelta.y < 0)
            //                 {
            //                     transform.position += new Vector3(0.0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * swimmingMoveSpeed,
            //                         0.0f);
            //                 }
            //                 else
            //                 {
            //                     transform.position += new Vector3(0.0f, 0.0f, 0.0f);
            //                 }    
            //             }
            //         }                    
            //     }
            // }
        }
    }
}

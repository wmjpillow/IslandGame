using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwmCamFlash : MonoBehaviour
{
    
    // Keeping track of variables for camera flash
    public Image flashPanel;
    private bool isFlash;
	private float lastAlpha;
	
	// Flash cam noise
	public AudioClip camSFX;
	private SFXManager sfxMan;
	
	// Camera overlay associated with swimming minigame
	public GameObject swimCamOverlay;
    
    // Start is called before the first frame update
    void Start()
    {
        //flashPanel.transform.position = Camera.main.transform.position;
        flashPanel.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
        isFlash = false;
		lastAlpha = 0.0f;
		sfxMan = FindObjectOfType<SFXManager>();
		
		swimCamOverlay.transform.localScale = transform.localScale;
		Instantiate(swimCamOverlay, transform.position, Quaternion.identity);
		swimCamOverlay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Check for mouse clicks for camera functionality
        if ((CameraMove.Instance.GetCurrentSceneName().Equals("Swimming") && CameraMove.Instance.transform.position.z == 18.0f) ||
            (CameraMove.Instance.GetCurrentSceneName().Equals("Birdwatching") && CameraMove.Instance.transform.position.z == -16.62f))
        {
	        
	        // Render camera overlay
	        swimCamOverlay.SetActive(true);
	
            if (Input.GetMouseButtonDown(0))
            {
	            // print("You took a picture!");
                // isFlash = true;
                flashPanel.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
				// lastAlpha = 1.0f;
				// sfxMan.playSoundEffect(camSFX);
            }

            if (isFlash)
            {
				lastAlpha = lastAlpha - Time.deltaTime;
                if (lastAlpha <= 0)
                {
                    flashPanel.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
                    // isFlash = false;
                } 
				else {
					flashPanel.GetComponent<CanvasRenderer>().SetAlpha(lastAlpha);
				}
            }
        } 
		else
        {
	        swimCamOverlay.SetActive(false);
			flashPanel.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
		}	
    }
}

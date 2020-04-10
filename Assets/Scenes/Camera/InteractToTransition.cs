using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractToTransition : ClickableObject
{

    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private string _sceneString;
    
    // Music to play at given transform
    public AudioClip music1;
    public AudioClip music2;
    public AudioClip music3;
    public AudioClip music4;
    
    // Get instance of MusicManager
    private MusicManager musicMan;
    
    // Zooming in sound
    public AudioClip zoomIn;
    private SFXManager sfxMan;

    
    // Start is called before the first frame update
    void Start()
    {
        musicMan = FindObjectOfType<MusicManager>();
        sfxMan = FindObjectOfType<SFXManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1)
        {
            if (GlobalValueTracker.Instance.day == 4 && (CameraMove.Instance.GetCurrentSceneName().Equals("Gardening")))
            {
                GlobalValueTracker.Instance.g4 = true;
            }
        }
    }

    public override void TriggerInteract(){
        CameraMove.Instance.SetTransformTarget(_cameraTransform);
        CameraMove.Instance.SetCurrentSceneName(this._sceneString);
        sfxMan.playSoundEffect(zoomIn);
        
        // Switch music depending on day
        if (IngameDate.GetInstance().getCurrentDay() == 0)
        {
            musicMan.changeMusic(music1);
        } else if (IngameDate.GetInstance().getCurrentDay() == 1)
        {
            musicMan.changeMusic(music2);
        } else if (IngameDate.GetInstance().getCurrentDay() == 2)
        {
            musicMan.changeMusic(music3);
        } else if (IngameDate.GetInstance().getCurrentDay() == 3)
        {
            musicMan.changeMusic(music4);
        }
    }

}

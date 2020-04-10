using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour
{

    [Tooltip("Linear Interpolation Time.")]
    [SerializeField] private float _vLerpTime;

    //Target to Lerp to
    private Transform _currentTransformTarget;
    private float _startTime;
    //Starting position of the camera
    [SerializeField] private Transform _originalTransform;

    //Raycasting
    private Camera _camera;

    //Singleton
    public static CameraMove Instance;
    private float timeSpan = 0.0f;

    private string currentSceneName = "Default";
    
    // Camera overlay
    public GardeningPanelDisplayer gpd;
    public GameObject gardenOverlay;
    
    // Refenrence to Music Manager and music to play
    public AudioClip mainHubMusic1;
    public AudioClip mainHubMusic2;
    public AudioClip mainHubMusic3;
    public AudioClip mainHubMusic4;
    private AudioClip songToPlay;
    private MusicManager musicMan;
    
    // Zooming in and out noises
    public AudioClip zoomOut;
    private SFXManager sfxMan;
    
    // Gaia image in background
    public RawImage gaiaImage;
    public RawImage gaiaImage2;
    public RawImage gaiaImage3;
    public RawImage gaiaImage4;

    public GameObject moveButtons;

    // Objective text
    private ObjectiveTextUI objectiveTextUI;
    
    void Awake(){
        if(Instance == null){
            Instance = this;
        }
        else{
            if(Instance != this){
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        this._camera = GetComponent<Camera>();
        musicMan = FindObjectOfType<MusicManager>();
        sfxMan = FindObjectOfType<SFXManager>();
		objectiveTextUI = FindObjectOfType<ObjectiveTextUI>();

        gaiaImage2.enabled = false;
        gaiaImage3.enabled = false;
        gaiaImage4.enabled = false;

        //songToPlay = mainHubMusic1;
    }

    // Update is called once per frame
    void Update()
    {

        LerpToTarget();

        if(Input.GetMouseButtonDown(0)){
            if(!EventSystem.current.IsPointerOverGameObject()){
                //Fire Raycasts
                RaycastHit hit;
                int LayerMask = 0;
                LayerMask = ~LayerMask;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
                out hit, 100, LayerMask, QueryTriggerInteraction.Ignore)) {
                    if(hit.collider.gameObject.TryGetComponent(typeof(ClickableObject), out Component component)){
                        ((ClickableObject)component).TriggerInteract();
                    }
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape)){
            TransformToOriginal();
            musicMan.changeMusic(songToPlay);
            sfxMan.playSoundEffect(zoomOut);
            Debug.Log(_originalTransform);
        }

        if (this.currentSceneName == "Gardening")
        {
            // Render camera overlay
            gardenOverlay.SetActive(true);
            gpd.removePanel();
            Soil soil = null;
            if(!EventSystem.current.IsPointerOverGameObject()){
                //Fire Raycasts
                RaycastHit hit;
                int LayerMask = 0;
                LayerMask = ~LayerMask;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
                out hit, 100, LayerMask, QueryTriggerInteraction.Ignore)) {
                    if(hit.collider.gameObject.TryGetComponent(typeof(Soil), out Component component)){
                        //((Soil)component).TriggerInteract();
                        soil = ((Soil)component);
                    }
                }
            }

            if(soil != null){
                //Update to change the values..
                if(soil.GetPlant() != null){
                    gpd.displayPanel(soil, Input.mousePosition.x, Input.mousePosition.y);
                }
            }

        }
        else if (this.currentSceneName != "Gardening")
        {
            gardenOverlay.SetActive(false);
        }
        
        // If currently in main hub area, set Gaia Image to enabled
        if (this.currentSceneName == "Default" && this.transform.position.z == -36.73f)
        {
            moveButtons.SetActive(true);
            gaiaImage.enabled = true;
			objectiveTextUI.updateObjectiveText("Welcome to our garden! Click the circles to find games to play! Try to play all 5 games before each day ends!");
        }
        if(this.currentSceneName == "Gardening")
        {
            objectiveTextUI.updateObjectiveText("Gardening Game is coming soon!");
        }

        if(this.currentSceneName != "Default")
        {
            moveButtons.SetActive(false);
        }
        
        // Change music basic on stage
        if (GlobalValueTracker.Instance.day == 1)
        {
            songToPlay = mainHubMusic1;
        }
        else if (GlobalValueTracker.Instance.day == 2)
        {
            songToPlay = mainHubMusic2;
        }
        else if (GlobalValueTracker.Instance.day == 3)
        {
            songToPlay = mainHubMusic3;
        }
        else if (GlobalValueTracker.Instance.day == 4)
        {
            songToPlay = mainHubMusic4;
        }

    }

    public void Escape()
    {
        TransformToOriginal();
        musicMan.changeMusic(songToPlay);
        sfxMan.playSoundEffect(zoomOut);
        Debug.Log(_originalTransform);
    }

    public void SetTransformTarget(Transform transform){
        this._currentTransformTarget = transform;
        this._startTime = Time.time;
        this.timeSpan = 0;
    }

    public void TransformToOriginal(){
        this._currentTransformTarget = _originalTransform;
        this._startTime = Time.time;
        this.timeSpan = 0;
        this.currentSceneName = "Default";
    }

    private void LerpToTarget(){
        //Lerp to the transform
        
        if(_currentTransformTarget != null){
            this.timeSpan += Time.deltaTime;
 
            float progress = Mathf.Clamp(timeSpan/ _vLerpTime, 0.0f, 1.0f);
            if(progress != 1.0f){
                Transform from = transform;
                Transform to = _currentTransformTarget;
                

                transform.rotation = Quaternion.Slerp(from.rotation, to.rotation, progress);
                transform.position = Vector3.Slerp(from.position, to.position, progress);
            }

        }
        else{
            this.timeSpan = 0;
        }
        if (GlobalValueTracker.Instance.day == 2)
        {
            gaiaImage.texture = gaiaImage2.texture;
        }
        if (GlobalValueTracker.Instance.day == 3)
        {
            gaiaImage.texture = gaiaImage3.texture;
        }
        if (GlobalValueTracker.Instance.day == 4)
        {
            gaiaImage.texture = gaiaImage4.texture;
        }

        gaiaImage.enabled = false;
    }
    public void SetCurrentSceneName(string s){
        this.currentSceneName = s;
    }

    public string GetCurrentSceneName(){
        return this.currentSceneName;
    }

    
}

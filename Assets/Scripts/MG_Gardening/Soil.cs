using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soil : ClickableObject
{
    private int _plant;
    private Plant _currentPlant;
    private SoilObserver _sov;
    public float _moistLevel = 100.0f; //Max 1.0f. When the day ends, increase the nutrition by moiselevel
    private float _upTime = 0.0f;
    private float _actualTime = 0.0f;
    public float _healthiness = 100.0f; //When this value approaches 0, the plant dies.

    class SoilObserver : Observer{
        private Soil _soil;
        public SoilObserver(Soil soil){
            //Register interest for changes:
            
            this._soil = soil;
            IngameDate.GetInstance().registerObserver(this);
        }

        public override void HandleSubjectChanges(Subject s){
            if(s.GetType() == typeof(IngameDate)){
                IngameDate d = (IngameDate)s;
                //Debug.Log("We noticed a day change. The current date is" + d.getCurrentDay());
                //Notify the soil.
                this._soil.giveNutrition();
                
            }
        }
    }
    
    // Reference to SFXManager and audio clips for plant, fertilizer, and watering
    private SFXManager sfxMan;
    public AudioClip plantSound;
    public AudioClip fertilizerSound;
    public AudioClip wateringSound;

	public static int total = 0;
	public static int score = 0;
    public static int plantingScore = 0;

    // Reference to Text UI
	private ObjectiveTextUI objectiveTextUI;
	private static int lastDayUpdated;
    
    // Start is called before the first frame update
    void Start()
    {
        this._sov = new SoilObserver(this);
        sfxMan = FindObjectOfType<SFXManager>();

		// Initialize text
		objectiveTextUI = FindObjectOfType<ObjectiveTextUI>();
        
        // Set total to global tracker value goal
        total = GlobalValueTracker.Instance.gardening_goal;
    }

    // Update is called once per frame
    void Update()
    {
        _upTime += Time.deltaTime; // This gives us an accurate representation of the time.
        if(_currentPlant){
            if(this._moistLevel > 150.0f){
                _actualTime += 0.3f * Time.deltaTime;
                _healthiness -= 0.005f;
            }
            else if(this._moistLevel > 50.0f){
                _actualTime += Time.deltaTime;
                
                _healthiness += 0.001f;
                if(_healthiness > 100.0f){
                    _healthiness = 100.0f;
                }
            }
            else{
                _actualTime += 0.1f * Time.deltaTime;
                _healthiness -= 0.002f;
            }
            
        }

        if(GlobalValueTracker.Instance.day >= 3){
            //Make all plants die!
            this._healthiness = -0.5f;
        }


        if(this._moistLevel < 0){
            this._moistLevel = 0;
        }
        else{
            this._moistLevel -= 0.004f;
        }

        if(_healthiness <= 0 && _currentPlant){
            Destroy(_currentPlant.gameObject);
            _currentPlant = null;
            //Get the plant data for the dead plant
            SerializablePlantData pd = GardeningManager.Instance.GetPlantData("Dead_Plant");
            this.SetPlant(pd.prefab);
        }

		total = GlobalValueTracker.Instance.gardening_goal;	

		// Depending on what day, update UI text
		if(CameraMove.Instance.GetCurrentSceneName() == "Gardening") {
			// if(IngameDate.GetInstance().getCurrentDay() == 0) {
            //     if (plantingScore >= 6)
            //     {
            //         objectiveTextUI.updateObjectiveText("Congratulations, you planted a garden! You’re a Happy Sprout! You win!");
            //     }
            //     else
            //     {
            //         objectiveTextUI.updateObjectiveText("Click on the round plots to plant crops! Hover the cursor over the plots to check the health of your plants.");   
            //     }
            // }
			// else
			// {
			// 	// Always update score every frame
            //     if (IngameDate.GetInstance().getCurrentDay() < 3)
            //     {
            //         if (score >= total)
            //         {
            //             switch(IngameDate.GetInstance().getCurrentDay()){
            //                 case 1:
            //                     objectiveTextUI.updateObjectiveText("Congratulations, you planted and grew a garden! You’re a Vegetable Grower! You win!");
            //                     break;
            //                 case 2:
            //                     objectiveTextUI.updateObjectiveText("Congratulations, you planted a garden! You’re a Farmer! You win!");
            //                     break;
            //             }
            //         }
            //         else
            //         {
            //             objectiveTextUI.updateObjectiveText("Harvest some crops and plant new ones!");
            //         }
            //     }
            // }
		}

		// Update variables if day changes
        if (lastDayUpdated != IngameDate.GetInstance().getCurrentDay())
        {
            score = 0;
            GlobalValueTracker.Instance.plantsHarvested = 0;
			print("Gardening goal for today is: " + GlobalValueTracker.Instance.gardening_goal);
        }

        lastDayUpdated = IngameDate.GetInstance().getCurrentDay();
        
    }

    void giveNutrition(){
        //this._plant += 1;
        if (this._currentPlant)
        {
            this._currentPlant.IncrementTimeAlive( Mathf.Clamp(1.0f * (_actualTime / _upTime), 0.3f, 1.0f));
            this._upTime = 0;
        }
    }

    void forceNutrition(float namount){
        if (this._currentPlant)
        {
            this._currentPlant.IncrementTimeAlive(namount);
        }
    }

    public override void TriggerInteract(){
        switch(GardeningManager.Instance.GetInhandObjectType()){
            case GardeningManager.InhandObjectType.SEED:
                plantPlants();
                sfxMan.playSoundEffect(plantSound);
                if (IngameDate.GetInstance().getCurrentDay() == 0)
                {
                    plantingScore += 1;
                }
                break;
            case GardeningManager.InhandObjectType.FERTILIZER:
                this._healthiness -= 70.0f;
                forceNutrition(1.0f);
                GardeningManager.Instance.SetInhandObjectType(GardeningManager.InhandObjectType.SEED);
                sfxMan.playSoundEffect(fertilizerSound);
                break;
            case GardeningManager.InhandObjectType.WATERCAN:
                this._moistLevel += 50.0f;
                sfxMan.playSoundEffect(wateringSound);
                break;
            default:
                break;
        }

    }

    private void plantPlants(){
        
        //If there is no plant, plant seed.
        if(_currentPlant == null)
        {
            GardeningManager gm = GardeningManager.Instance;
            GameObject prefab = gm.GetCurrentPlantPrefab();
            //Vector3 farmlandpos = transform.position;
            this._healthiness = 100.0f;
            SetPlant(prefab);
        }
        //If there is plant, check if you can collect it, and do so.
        else{
            if(_currentPlant.GetIsRipe()){
                GameObject instance = _currentPlant.gameObject;
                Destroy(instance);
                Debug.Log("Collected Plant!");
                this._currentPlant = null;
				score += 1;
				GlobalValueTracker.Instance.plantsHarvested = score;
            }
            else if(_currentPlant.GetIsDead()){
                                GameObject instance = _currentPlant.gameObject;
                Destroy(instance);
                Debug.Log("Collected Dead Plant...");
                this._currentPlant = null;
            }
        }
    }

    public void SetPlant(GameObject prefab){
        //If there is a plant, we destroy it first.
        float timeAlive = 0;
        if(_currentPlant != null){
            GameObject instance = _currentPlant.gameObject;
            timeAlive = _currentPlant.GetTimeAlive();
            Destroy(instance);
        }
        Vector3 farmlandpos = transform.position;
        GameObject new_instance = Instantiate(prefab, new Vector3(farmlandpos.x, farmlandpos.y, farmlandpos.z), transform.rotation);
        this._currentPlant = new_instance.GetComponent<Plant>();
        this._currentPlant.SetSoil(this);
    }

    public Plant GetPlant(){
        return this._currentPlant;
    }
}

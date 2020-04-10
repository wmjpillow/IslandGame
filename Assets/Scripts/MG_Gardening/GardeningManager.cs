using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardeningManager : MonoBehaviour
{
    private string _currentPlantName = "Carrot_0";
    public enum InhandObjectType
    {
        SEED,
        FERTILIZER,
        WATERCAN
    }

    private InhandObjectType _inHandType = InhandObjectType.SEED;

    public List<SerializablePlantData> prefabList = new List<SerializablePlantData>();

    //Singleton
    public static GardeningManager Instance;

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

    void Start(){
    }

    void Update(){

    }

    public GameObject GetCurrentPlantPrefab(){
        return GetPlantPrefabFromString(_currentPlantName);
    }

    public string GetCurrentPlantName()
    {
        return _currentPlantName;
    }

    public void SetCurrentPlantName(string s){
        this._currentPlantName = s;
    }

    public GameObject GetPlantPrefabFromString(string name){
        foreach(SerializablePlantData spd in prefabList){
            if(name.Equals(spd.name)){
                return spd.prefab;
            }
        }
        return null;
    }

    public GameObject GetPlantNextPhaseReady(string name, float timeAlive){
        foreach(SerializablePlantData spd in prefabList){
            if(name.Equals(spd.name)){
                if(timeAlive >= spd.nextPhaseTime){
                    return GetPlantPrefabFromString(spd.nextPhaseName);
                }
            }
        }
        return null;
    }

    public InhandObjectType GetInhandObjectType(){
        return this._inHandType;
    }

    public void SetInhandObjectType(InhandObjectType newtype){
        this._inHandType = newtype;
    }

    public SerializablePlantData GetPlantData(string name){
        foreach(SerializablePlantData spd in prefabList){
            if(name.Equals(spd.name)){
                return spd;
            }
        }
        return null;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    private float _timeAlive = 0;
    public string _plantName;
    private Soil _soil;
    [SerializeField]private bool _isRipePhase;
    [SerializeField]private bool _isDead;
    void Update(){

    }
    

    public void IncrementTimeAlive(float time){
        this._timeAlive += time;
        //Lookup the prefab from the gardening manager
        if(_isRipePhase || _isDead){
            return; //A ripe plant will not be able to grow any further.
        }
        GameObject prefab = GardeningManager.Instance.GetPlantNextPhaseReady(_plantName, _timeAlive);
        if(prefab != null){
            //Call the soil to set new plant
            this._soil.SetPlant(prefab);
        }
    }

    public float GetTimeAlive(){
        return this._timeAlive;
    }

    public void SetPlantName(string name){
        this._plantName = name;
    }

    public void SetSoil(Soil s){
        this._soil = s;
    }

    public bool GetIsRipe(){
        return _isRipePhase;
    }

    public bool GetIsDead(){
        return _isDead;
    }
}


[System.Serializable]
public class SerializablePlantData
{
    public string name;
    public GameObject prefab;
    public float nextPhaseTime;
    public string nextPhaseName;
}

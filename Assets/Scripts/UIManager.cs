using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //Singleton
    public static UIManager Instance;

    public SerializableUIData[] UIData;

    private Dictionary<SerializableUIData, float> _fadingTo;

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
        _fadingTo = new Dictionary<SerializableUIData, float>();
    }

    void Update(){
        foreach(SerializableUIData key in _fadingTo.Keys){
            float fadeto;
            _fadingTo.TryGetValue(key, out fadeto);
            Image img = key.UI.GetComponent<Image>();
            Color col = img.color;
            col.a = col.a + (fadeto - col.a) * 0.05f;
            img.color = col;//Simple linear interpolation
            if(fadeto == 0.0f && col.a < 0.05f){
                //remove me!
                key.UI.SetActive(false);
            }
            if(fadeto == 1.0f){
                key.UI.SetActive(true);
            }
        }
    }

    public void ShowUI(string name){
        foreach(SerializableUIData data in UIData){
            if(data.name.Equals(name)){
                data.UI.SetActive(true);
            }
        }
        
    }

    public void HideUI(string name){
        foreach(SerializableUIData data in UIData){
            if(data.name.Equals(name)){
                data.UI.SetActive(false);
            }
        }
    }

    public void FadeIn(string name){
        //Set this thing in the dictionary
        foreach(SerializableUIData data in UIData){
            if(data.name.Equals(name)){
                if(_fadingTo.ContainsKey(data)){
                    _fadingTo[data] = 1.0f;
                }
                else{
                    _fadingTo.Add(data, 1.0f);
                }
                
                
            }
        }
    }

    public void FadeOut(string name){
        //Set this thing in the dictionary
        foreach(SerializableUIData data in UIData){
            if(data.name.Equals(name)){
                if(_fadingTo.ContainsKey(data)){
                    _fadingTo[data] = 0.0f;
                }
                else{
                    _fadingTo.Add(data, 0.0f);
                }
               
            }
        }
    }
    
}

[System.Serializable]
public class SerializableUIData
{
    public string name;
    public GameObject UI;
}


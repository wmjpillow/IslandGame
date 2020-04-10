using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveTextUI : MonoBehaviour
{
         
    // Text component
    public Text objectiveText;
         
    // Start is called before the first frame update
    void Start()
    {

    }
      
    // Update is called once per frame
    void Update()
    {
             
    }
    
    // Update text based on score
    public void updateObjectiveText(string update)
    {
         objectiveText.text = update;
    }
    
    // Disable text when needed
    public void disableObjectiveText()
    {
        objectiveText.enabled = false;
    }
    
    // Enable text when needed
    public void enableObjectiveText()
    {
        objectiveText.enabled = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BirdUIText : MonoBehaviour
{
    public Text FinchLeft;
    public Text FinchRight;
    public Text SparrowLeft;
    //public string FinchLeftText;
    //public string FinchRightText;
    //public string SparrowLeftText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(FinchLeft)
        {
            FinchLeft.text = "FinchLeft: " + BirdManager.Instance.BirdANum + " in the scene";
        }
        if (FinchRight)
        {
            FinchRight.text = "BirdType B: " + BirdManager.Instance.BirdBNum + " in the scene";
        }
        if (SparrowLeft)
        {
            SparrowLeft.text = "BirdType C: " + BirdManager.Instance.BirdCNum + " in the scene";
        }
    }
}

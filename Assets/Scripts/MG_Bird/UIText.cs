using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIText : MonoBehaviour
{
    public Text bird1;
    public Text bird2;
    public Text bird3;
    //public string bird1Text;
    //public string bird2Text;
    //public string bird3Text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(bird1)
        {
            bird1.text = "BirdType A: " + Manager.Instance.BirdANum + " in the scene";
        }
        if (bird2)
        {
            bird2.text = "BirdType B: " + Manager.Instance.BirdBNum + " in the scene";
        }
        if (bird3)
        {
            bird3.text = "BirdType C: " + Manager.Instance.BirdCNum + " in the scene";
        }
    }
}

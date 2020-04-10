using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Animation animation = this.GetComponent<Animation>();
        animation.Play("Catching");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

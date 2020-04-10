using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bird_script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Animation animation = this.GetComponent<Animation>();
        animation.Play("Fly");
    }

    // Update is called once per frame
    void Update()
    {

    }
}

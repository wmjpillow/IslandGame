using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ThrowCube : MonoBehaviour
{
    //public bool isThrown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeCube(bool isThrown)
    {
        if (isThrown)
        {
            ThrowCubeDown();
        }
        else
        {
            GetCubeUp();
        }
    }

    private void ThrowCubeDown()
    {
        //transform.Rotate(64f, 0, 0);
        transform.DORotate(new Vector3(64f, 0, 0), 0.1f);
    }
    private void GetCubeUp()
    {
        //transform.Rotate(-64f, 0, 0);
        transform.DORotate(new Vector3(0, 0, 0), 0.1f);
    }
}

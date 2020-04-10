using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ThrowBall : MonoBehaviour
{
    public GameObject endpos_1;
    public GameObject endpos_2;
    public GameObject startpos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeBall(bool isThrown)
    {
        if (isThrown)
        {
            ThrowBallDown();
        }
        else
        {
            GetBallUp();
        }
    }

    private void ThrowBallDown()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOJump(endpos_1.transform.position, 2f, 1, 0.2f));
        sequence.Append(transform.DOJump(endpos_2.transform.position, 3f, 1, 0.2f));
    }
    private void GetBallUp()
    {
        transform.DOComplete();
        transform.DOJump(startpos.transform.position, 3f, 1, 0.4f);
    }

    public void ShakeBall()
    {
        /*Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(time);
        sequence.Append(transform.DOShakePosition(2.0f, 0.2f));*/
        transform.DOShakePosition(2.0f, 0.2f);
    }
    public void StopShakingBall()
    {
        transform.DOComplete();
    }
}

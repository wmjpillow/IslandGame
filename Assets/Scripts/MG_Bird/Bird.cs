using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bird:MonoBehaviour
{

    public Rigidbody rigid;
    public Vector3 targetPosition;

    float maxSpeed = 3.0f;
    float maxTurnSpeed = 5.0f;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    Vector3 Seek(Vector3 targetPosition)
    {
        Vector3 diff = targetPosition - transform.position;
        Vector3 desiredVelocity = diff.normalized * maxSpeed;

        return desiredVelocity - rigid.velocity;
    }
    void Update()
    {

        Vector3 steeringForce = Seek(targetPosition);
        //// 限制掉头的力，鸟不能直接倒车 =.=

        Vector3 velocityNormalized = rigid.velocity.normalized;
        // 沿速度方向的大小
        Vector3 f1 = Vector3.Dot(steeringForce, velocityNormalized) * velocityNormalized;
        if (Vector3.Dot(steeringForce, velocityNormalized) < 0)
        {
            // 直接减掉向后的力
            steeringForce -= f1;
        }

        rigid.AddForce(steeringForce);

        // 设定角度，带有平滑
        Vector3 mid = Vector3.Lerp(transform.forward, rigid.velocity.normalized, 0.1f);
        transform.LookAt(transform.position + mid);

        // 速度限制
        if (rigid.velocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            rigid.velocity = rigid.velocity.normalized * maxSpeed;
        }
    }
}
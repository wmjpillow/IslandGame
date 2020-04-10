using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float m_speed=10;
    public float m_angleSpeed=10;
    public Rigidbody m_rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); //A D 左右
        float vertical = Input.GetAxis("Vertical"); //W S 上 下
        //if (horizontal != 0)
            m_rigidbody.velocity = Vector3.right * horizontal * m_speed+Vector3.forward * vertical * m_speed;
        //else
        //    m_rigidbody.velocity = 
        
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");
        this.transform.eulerAngles += new Vector3(-v, h, 0) * m_angleSpeed * Time.deltaTime;
    }
}

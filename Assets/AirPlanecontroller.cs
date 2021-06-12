using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPlanecontroller : MonoBehaviour
{
    [SerializeField] float m_speed = 1f;
    float m_time = 0f;
    private Vector3 position;
    private Vector3 m_origin;
    private Vector3 screenToWorldPointPosition;
    Quaternion rot;
    Quaternion quaternion;
    Quaternion m_rot;
    Quaternion m_quotanion;
    float m_countTime = 0;


    private void Start()
    {
        m_origin = this.transform.position;
    }
    void Update()
    {
        m_time += Time.deltaTime;
        position = Input.mousePosition;
        quaternion = Quaternion.identity;
        m_quotanion = Quaternion.identity;
        position.z = 10f;
        screenToWorldPointPosition = new Vector3(Camera.main.ScreenToWorldPoint(position).x, Camera.main.ScreenToWorldPoint(position).y, m_time * m_speed);
        gameObject.transform.position = screenToWorldPointPosition;
        if ((315f < this.transform.rotation.eulerAngles.z && 360f > this.transform.rotation.eulerAngles.z) || (this.transform.rotation.eulerAngles.z >= 0f && this.transform.rotation.eulerAngles.z < 45f))
        {
            rot = Quaternion.AngleAxis((screenToWorldPointPosition.x - m_origin.x)/ -5, Vector3.forward);
            quaternion = this.transform.rotation * rot;
            this.transform.rotation = quaternion;
        }
        else
        {
            //Debug.Log("モード切り替え");
            if (this.transform.rotation.eulerAngles.z > 300f && this.transform.rotation.eulerAngles.z < 315f)
            {
                //Debug.Log("a");
                m_rot = Quaternion.Euler(0, 0, 3f);
            }
            else
            {
                //Debug.Log("b");
                m_rot = Quaternion.Euler(0, 0, -3f);
            }
            m_quotanion = m_rot * this.transform.localRotation;
            transform.rotation = m_quotanion;
        }
        m_origin = this.transform.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TitlePlane : MonoBehaviour
{
    [SerializeField] float m_speed = 0.1f;
    [SerializeField]CinemachineVirtualCameraBase m_virtualCamera;
    CinemachineTransposer transposer;
    float m_originZOffset;
    float m_time = 0;
    bool IsStart;
    // Start is called before the first frame update
    void Start()
    {
        transposer = ((CinemachineVirtualCamera)m_virtualCamera).GetCinemachineComponent<CinemachineTransposer>();
        m_originZOffset = transposer.m_FollowOffset.z;
        IsStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        m_time += Time.deltaTime;
        if (IsStart)
        {
            transposer.m_FollowOffset.z -= m_time * m_speed;
        }
        else
        {
            transposer.m_FollowOffset.z = m_originZOffset;
        }
    }

    public void SetBool()
    {
        IsStart = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraEffecter : MonoBehaviour
{
    //[SerializeField] CinemachineVirtualCamera m_cinemachine = null;
    [SerializeField] CinemachineVirtualCameraBase m_cinemachine = null;
    [SerializeField] float m_zoomLength = -5;
    [SerializeField] float m_zoomSpeed = 0.2f;
    float m_StartOffset;
    CinemachineTransposer m_transposer;
    
    // Start is called before the first frame update
    void Start()
    { 
        m_transposer = ((CinemachineVirtualCamera)m_cinemachine).GetCinemachineComponent<CinemachineTransposer>();
        m_StartOffset  = m_transposer.m_FollowOffset.z;
        Debug.Log(m_transposer);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            if (m_transposer.m_FollowOffset.z < m_zoomLength)
            {
                ZoomUp(m_zoomSpeed);
            }
        }
        else if(Input.GetKeyUp("a"))
        {
            while (true)
            {
                if (m_transposer.m_FollowOffset.z > m_StartOffset)
                {
                    ZoomOut(m_zoomSpeed);
                }
            } 
        }

        if (Input.GetKeyDown("d"))
        {
            if (m_transposer.m_FollowOffset.z > m_zoomLength)
            {
                ZoomOut(m_zoomSpeed);
            }
        }
        else if (Input.GetKeyUp("d"))
        {
            if (m_transposer.m_FollowOffset.z < m_StartOffset)
            {
                ZoomUp(m_zoomSpeed);
            }
        }
    }

    public void ZoomUp(float m_zoomSpeed)
    {
        m_transposer.m_FollowOffset.z += m_zoomSpeed;
    }

    public void ZoomOut(float m_zoomSpeed)
    {
        m_transposer.m_FollowOffset.z -= m_zoomSpeed;
    }

    public void CameraShake()
    {

    }
}

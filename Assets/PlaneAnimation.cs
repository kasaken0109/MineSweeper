using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneAnimation : MonoBehaviour
{
    bool IsStopped = true;
    public int m_speed = 15;
    AudioSource audioSource;
    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        IsStopped = false;
    }

    private void Update()
    {
        if (!IsStopped)
        {
            this.transform.position -= new Vector3(0, 0, m_speed);
        }
        if (this.transform.position.z <= 0)
        {
            audioSource.volume = 0;
        }
    }
}

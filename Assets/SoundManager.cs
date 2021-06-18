using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip m_se = null;
    [SerializeField] AudioClip m_explode = null;
    [SerializeField] AudioClip m_barrier = null;
    [SerializeField] AudioClip m_createBarrier = null;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayUI()
    {
        audioSource.PlayOneShot(m_se);
    }

    public void PlayExplode(Transform transform)
    {
        AudioSource.PlayClipAtPoint(m_explode,transform.position);
    }

    public void PlayBarrier()
    {
        audioSource.PlayOneShot(m_barrier);
    }

    public void PlayCreateBarrier()
    {
        audioSource.PlayOneShot(m_createBarrier);
    }
}

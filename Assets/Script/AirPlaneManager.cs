using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPlaneManager : MonoBehaviour
{
    public static AirPlaneManager Instance { get; private set; }
    [SerializeField] int m_life = 3;
    [SerializeField] GameObject[] m_parts = null;
    [SerializeField] GameObject[] m_hpUI = null;
    [SerializeField] GameObject[] m_explosions = null;
    [SerializeField] GameObject m_barrier = null;
    [SerializeField] GameObject m_invincibleUI = null;
    [SerializeField] GameObject m_destroy = null;
    [SerializeField] SoundManager soundManager;
    bool godMode = false;
    public bool Islive = true;
    bool IsBarrierActive = false;
    bool IsFirstTime;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        m_explosions[0].SetActive(false);
        m_explosions[1].SetActive(false);
        IsFirstTime = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ActiveObject(int index)
    {
        m_explosions[index].SetActive(true);
        m_parts[index].SetActive(false);
        yield return new WaitForSeconds(1f);
        m_explosions[index].SetActive(false);
    }

    void  ActiveObject(bool value)
    {
        m_barrier.SetActive(value);
        m_invincibleUI.SetActive(value);
        IsBarrierActive = value;
    }
    public void BarriarCreate()
    {
        
        if (!m_barrier.activeInHierarchy)
        {
            soundManager.PlayCreateBarrier();
        }
        ActiveObject(true);
    }

    public void NonActiveBarrier()
    {
        if (!IsFirstTime)
        {
            return;
        }
        Debug.Log(IsFirstTime);
        ActiveObject(false);
        IsFirstTime = false;
    }

    public void GetDamage()
    {
        Debug.Log(m_life);
        if (IsBarrierActive)
        {
            soundManager.PlayBarrier();
            ActiveObject(false);
            return;
        }
        m_life--;
        if (m_life == 2)
        {
            StartCoroutine(ActiveObject(0));
        }
        else if (m_life == 1)
        {
            StartCoroutine(ActiveObject(1));
        }
        else if (m_life == 0)
        {
            CellSetController.Instance.GameOver();
            DestroyPlane();
            Islive = false;
        }
        for (int i = m_hpUI.Length; i > m_life; i--)
        {
            m_hpUI[i - 1].SetActive(false);
            soundManager.PlayExplode(this.transform);
        }
    }

    public void DestroyPlane()
    {
        Instantiate(m_destroy, this.transform.position, this.transform.rotation);
        gameObject.SetActive(false);
    }
}

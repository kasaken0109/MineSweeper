using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPlaneManager : MonoBehaviour
{
    [SerializeField] int m_life = 3;
    [SerializeField] GameObject[] m_parts;
    [SerializeField] GameObject[] m_hpUI;
    [SerializeField] GameObject[] m_explosions;
    [SerializeField] GameObject m_destroy;
     public bool Islive = true;
    // Start is called before the first frame update
    void Start()
    {
        m_explosions[0].SetActive(false);
        m_explosions[1].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_life == 2)
        {
            StartCoroutine(ActiveObject(0));
        }
        else if (m_life == 1)
        {
            StartCoroutine(ActiveObject(1));
        }
    }

    IEnumerator ActiveObject(int index)
    {
        m_explosions[index].SetActive(true);
        m_parts[index].SetActive(false);
        yield return new WaitForSeconds(1f);
        m_explosions[index].SetActive(false);
    }

    public void GetDamage()
    {
        m_life--;
        Debug.Log(m_life);
        if (m_life == 0)
        {
            CellSetController.Instance.GameOver();
            DestroyPlane();
            Islive = false;
        }
        for (int i = m_hpUI.Length; i > m_life; i--)
        {
            m_hpUI[i - 1].SetActive(false);
        }
    }

    public void DestroyPlane()
    {
        Instantiate(m_destroy, this.transform.position, this.transform.rotation);
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBase : MonoBehaviour
{
    private int bingoNum;
    private int score = 1;

    public int Score
    {
        get => score;
    }

    [SerializeField] GameObject m_cover = null;

    public int BingoNum
    {
        get => bingoNum;
        set
        {
            bingoNum = value;
        }
    }

    public bool IsOpen;

    // Start is called before the first frame update
    private void Start()
    {
        IsOpen = false;
    }
    // Update is called once per frame
    void Update()
    {
    }

    public enum CellDisplayState
    {
        Cover = 0,
        Uncover = 1,
        Checked = 2,
    }

    public void CheckOpen(int num)
    {
        if(num == bingoNum)
        {
            m_cover?.SetActive(true);
            IsOpen = true;
            score = 2;
        }
    } 

    public void SetPanel(bool IsActive)
    {
        if(IsActive) m_cover?.SetActive(true);
        else
        {
            m_cover?.SetActive(false);
        }
    }
}



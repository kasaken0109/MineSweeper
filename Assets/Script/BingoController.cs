using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BingoController : MonoBehaviour
{
    [SerializeField] int m_maxValue = 100;
    [SerializeField] int rawNum = 5;
    [SerializeField] int columNum = 5;
    [SerializeField] CellBase m_cellBase;
    [SerializeField] GameObject m_displayText;
    [SerializeField] GameObject m_bingoPanel;
    [SerializeField] GameObject[] m_reachPanel;
    [SerializeField] private GridLayoutGroup lgridLayout;
    [SerializeField] private GridLayoutGroup rgridLayout;
    List<int> bingoBalls;
    List<int> bingoTiles;
    List<int> openNums;
    CellBase[,] m_cells;
    int checkCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        bingoBalls = new List<int>();
        bingoTiles = new List<int>();
        openNums = new List<int>();
        for (int i = 1; i <= m_maxValue; i++)
        {
            bingoBalls.Add(i);
            bingoTiles.Add(i);
        }
        m_cells = new CellBase[rawNum, columNum];
        for (int i = 0; i < rawNum; i++)
        {
            for (int k = 0; k < columNum; k++)
            {
                m_cells[i, k] = Instantiate(m_cellBase);
                if (i == 2 && k == 2)
                {
                    m_cells[i, k].GetComponentInChildren<Text>().text = "Free";
                    m_cells[i, k].SetPanel(true);
                    m_cells[i, k].IsOpen = true;
                }
                else
                {
                    m_cells[i, k].BingoNum = bingoTiles[Random.Range(0, bingoTiles.Count)];
                    m_cells[i, k].IsOpen = false;
                    bingoTiles.Remove(m_cells[i, k].BingoNum);
                    m_cells[i, k].GetComponentInChildren<Text>().text = m_cells[i, k].BingoNum.ToString();
                }
                m_cells[i, k].transform.parent = lgridLayout.transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenNum()
    {
        int randomNum = bingoBalls[Random.Range(0, bingoBalls.Count)];
        bingoBalls.Remove(randomNum);
        openNums.Add(randomNum);
        DisplayNum(randomNum);
        CheckOpen(randomNum);
    }

    public void DisplayNum(int num)
    {
        var m = Instantiate(m_displayText);
        Text text = m.GetComponent<Text>();
        text.text = num.ToString();
        m.transform.parent = rgridLayout.transform;
    }

    void CheckOpen(int num)
    {
        for (int i = 0; i < rawNum; i++)
        {
            for (int k = 0; k < rawNum; k++)
            {
                m_cells[i, k].CheckOpen(num);
            }
        }
        CheckBingo();
    }

    //List<int> reach = new List<int>();
    //List<int> bingoScore = new List<int>() { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
    void CheckBingo()
    {
        List<int> reach = new List<int>();
        List<int> bingoScore = new List<int>() { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        for (int i = 0; i < rawNum; i++)
        {
            for (int k = 0; k < columNum; k++)
            {
                if (i == 2 && k == 2)
                {
                    bingoScore[2] *= 2;
                    bingoScore[7] *= 2;
                    bingoScore[10] *= 2;
                    bingoScore[11] *= 2;
                }
                else
                {
                    bingoScore[i] *= m_cells[i, k].Score;
                    bingoScore[columNum + k] *= m_cells[i, k].Score;
                    if (i == k)
                    {
                        bingoScore[10] *= m_cells[i, k].Score;
                    }
                    else if (i + k == 4)
                    {
                        bingoScore[11] *= m_cells[i, k].Score;
                    }
                }
            }
        }
        for (int j = 0; j < bingoScore.Count; j++)
        {
            if (bingoScore[j] == 16) reach.Add(j);
            else if (bingoScore[j] == 32)
            {
                m_bingoPanel?.SetActive(true);
            }
            Debug.Log($"Score[{j}]:{bingoScore[j]}");
        }
        foreach (var item in reach)
        {
            m_reachPanel[item].SetActive(true);
        }
    }
    void Bingo()
    {
        
    }
}

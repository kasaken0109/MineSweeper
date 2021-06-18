using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class CellSetController : MonoBehaviour
{
    public static CellSetController Instance { get; private set; }
    [SerializeField] private Cell _cell;
    [SerializeField] GameObject _Panel;
    [SerializeField] GameObject _UIButton = null;
    [SerializeField] private GridLayoutGroup layoutGroup = null;
    [SerializeField] int columNum = 5;
    [SerializeField] int rawNum = 5;
    [SerializeField] int _bomNum = 5;
    [SerializeField] Text _gameover = null;
    [SerializeField] AirPlaneManager airPlaneManager;
    [SerializeField] AirPlanecontroller air;
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] Text m_bombNum = null;
    [SerializeField] Text m_skilNumText = null;
    [SerializeField] Animator m_UIanim = null;
    [SerializeField] int m_skillCount = 10;
    [SerializeField] AudioClip m_clear;
    int skillCountUp;
    int m_bombRemainNum;
    bool IsStop = false;
    bool IsGameOver = false;
    bool godMode = false;
    int _pushCount = 0;
    Cell[,] _cells;

    CanvasGroup canvasGroup;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        BombSet();
        m_bombRemainNum = _bomNum;
        _UIButton.SetActive(false);
        skillCountUp = 0;
        virtualCamera.Priority = 8;
    }

    private void BombSet()
    {
        int bomCountNum = 0;
        _cells = new Cell[columNum, rawNum];
        for (int i = 0; i < rawNum; i++)
        {
            for (int k = 0; k < columNum; k++)
            {
                _cells[i, k] = Instantiate(_cell);
                _cells[i, k].CellState = CellState.None;
                _cells[i, k]._cellID = i * columNum + k;
                _cells[i, k].transform.parent = layoutGroup.transform;
            }
        }

        for (int i = 0; i < _bomNum; i++)
        {
            while (true)
            {
                int _randmNumY = Random.Range(0, rawNum);
                int _randmNumX = Random.Range(0, columNum);
                if (_cells[_randmNumY, _randmNumX].CellState != CellState.Mine)
                {
                    _cells[_randmNumY, _randmNumX].CellState = CellState.Mine;
                    bomCountNum++;
                    for (int j = -1; j <= 1; j++)
                    {
                        for (int k = -1; k <= 1; k++)
                        {
                            if ((j != 0 || k != 0) && IsExsistPlace(_randmNumY + j, _randmNumX + k) && _cells[_randmNumY + j, _randmNumX + k].CellState != CellState.Mine)
                            {
                                _cells[_randmNumY + j, _randmNumX + k].CellState += 1;
                            }
                        }
                    }
                    break;
                }
            }

        }
    }

    private void Update()
    {
        m_bombNum.text = "敵残機 :" + m_bombRemainNum;
        if (m_bombRemainNum == 0 && !IsGameOver)
        {
            GameClear();
        }
        if (skillCountUp >= m_skillCount)
        {
            Debug.Log("skillClear");
            airPlaneManager.BarriarCreate();
            skillCountUp = 0;
        }
        m_skilNumText.text = skillCountUp.ToString();

        if (Input.GetButtonDown("Jump"))
        {
            if (!godMode)
            {
                godMode = true;
            }
            else
            {
                godMode = false;
            }
        }
    }
    bool IsExsistPlace(int y, int x)
    {
        if (0 <= x && x < columNum && 0 <= y && y < rawNum)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UncoverMines(int raw, int colum)
    {
        if (_cells[raw,colum]._bombFlag)
        {
            return;
        }
        List<int> idX = new List<int>();
        List<int> idY = new List<int>();
        bool CanOpen = true;
        if (_cells[raw,colum].CellState == CellState.Mine)
        {
            skillCountUp = 0;
            if (airPlaneManager.Islive)
            {
                m_UIanim.SetTrigger("Hit");
                if (!godMode)
                {
                    airPlaneManager.GetDamage();
                }
                DecreaseBombNum();
            }
            _cells[raw, colum]._IsBomb = true;
            _cells[raw, colum].Activetext();
        }
        else
        {
            for (int j = -1; j <= 1; j++)
            {
                for (int k = -1; k <= 1; k++)
                {
                    if (IsExsistPlace(raw + j, colum + k))
                    {
                        if (_cells[raw + j, colum + k].CellState == CellState.Mine)
                        {
                            CanOpen = false;
                            break;

                        }
                        else
                        {
                            if (_cells[raw + j, colum + k]._cellDisplayState != CellDisplayState.Checked && !_cells[raw + j, colum + k]._bombFlag)
                            {
                                idX.Add(k);
                                idY.Add(j);
                            }
                        }
                    }
                }
            }
            if (CanOpen)
            {
                for (int z = 0; z < idX.Count; z++)
                {
                    _cells[raw + idY[z], colum + idX[z]].Activetext();
                    _cells[raw + idY[z], colum + idX[z]]._cellDisplayState = CellDisplayState.Checked;
                    skillCountUp++;
                    UncoverMines(raw + idY[z], colum + idX[z]);
                }
            }
            else
            {
                _cells[raw, colum].Activetext();
                skillCountUp++;
            }
        }
        
    }

    public void IDCheck(Cell cell)
    {
        int xid = 0;
        int yid = 0;
        for (int i = 0; i < rawNum; i++)
        {
            for (int k = 0; k < columNum; k++)
            {
                if (cell._cellID == i * columNum + k)
                {
                    yid = i;
                    xid = k;
                    break;
                }
            }
        }
        if (!_cell._bombFlag)
        {
            UncoverMines(yid, xid);
        }
        _pushCount++;
        
    }

    public void TimeStopper()
    {
        if (!IsStop)
        {
            Time.timeScale = 0.25f;
            IsStop = true;
        }
        else
        {
            Time.timeScale = 1;
            IsStop = false;
        }
        
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
        IsGameOver = true;
        _gameover.text = "GameOver";
        _UIButton.SetActive(true);
        for (int i = 0; i < rawNum; i++)
        {
            for (int k = 0; k < columNum; k++)
            {
                if (_cells[i,k].CellState == CellState.Mine)
                {
                    _cells[i,k].Activetext();
                }
            }
        }
    }

    public void GameClear()
    {
        int count = 0;
        for (int i = 0; i < rawNum; i++)
        {
            for (int k = 0; k < columNum; k++)
            {
                if (_cells[i, k]._box.activeSelf || _cells[i, k]._plane.activeSelf)
                {
                    count++;
                }
            }
        }
        if (count >= rawNum * columNum - _bomNum)
        {
            _gameover.text = "GameClear!";
            var m = GameObject.Find("PlayerAction").GetComponent<AudioSource>();
            m.clip = m_clear;
            m.volume = 1;
            virtualCamera.Priority = 12;
            _UIButton.SetActive(true);
        }
    }

    public void IncreaseBombNum()
    {
        if (m_bombRemainNum == _bomNum)
        {
            return;
        }
        m_bombRemainNum++;
    }
    public void DecreaseBombNum()
    {
        if (m_bombRemainNum == 0)
        {
            return;
        }
        m_bombRemainNum--;
    }

    //public int SearchMine(int raw,int colum)
    //{
    //    int _bombCount = 0;
    //    if (_cells[raw, colum].CellState == CellState.Mine)
    //    {
    //        return -1;
    //    }
    //    if (0 < colum && raw < rawNum - 1)
    //    {
    //        if (_cells[raw + 1, colum - 1].CellState == CellState.Mine)
    //        {
    //            _bombCount++;
    //            //Debug.Log($"{colum} , (1,-1) ,{raw} ,{_bombCount}");
    //        }
    //    }
    //    if ( raw < rawNum - 1)
    //    {
    //        if (_cells[raw + 1, colum].CellState == CellState.Mine)
    //        {
    //            _bombCount++;
    //            //Debug.Log($"{colum} , (1,0) ,{raw} ,{_bombCount}");
    //        }
    //    }
    //    if (colum < columNum - 1 && raw < rawNum - 1)
    //    {
    //        if (_cells[raw + 1, colum + 1].CellState == CellState.Mine)
    //        {
    //            _bombCount++;
    //            //Debug.Log($"{colum} , (1,1) ,{raw} ,{_bombCount}");
    //        }
    //    }
    //    if (0 < raw)
    //    {
    //        if (_cells[raw -1, colum].CellState == CellState.Mine)
    //        {
    //            _bombCount++;
    //            //Debug.Log($"{colum} , (-1,0) ,{raw} ,{_bombCount}");
    //        }
    //    }
    //    if (colum < columNum - 1)
    //    {
    //        if (_cells[raw, colum + 1].CellState == CellState.Mine)
    //        {
    //            _bombCount++;
    //            //Debug.Log($"{colum} , (0,1) ,{raw} ,{_bombCount}");
    //        }
    //    }
    //    if (0 < colum && 0 < raw)
    //    {
    //        if (_cells[raw - 1, colum - 1].CellState == CellState.Mine)
    //        {
    //            _bombCount++;
    //            //Debug.Log($"{colum} , (-1,-1) ,{raw} ,{_bombCount}");
    //        }
    //    }
    //    if (0 < colum)
    //    {
    //        if (_cells[raw, colum -1].CellState == CellState.Mine)
    //        {
    //            _bombCount++;
    //            //Debug.Log($"{colum} , (0,-1) ,{raw} ,{_bombCount}");
    //        }
    //    }
    //    if (colum < columNum - 1 && 0 < raw)
    //    {
    //        if (_cells[raw - 1, colum + 1].CellState == CellState.Mine)
    //        {
    //            _bombCount++;
    //            //Debug.Log($"{colum} , (-1,1) ,{raw} ,{_bombCount}");
    //        }
    //    }
    //    return _bombCount;
    //}
}

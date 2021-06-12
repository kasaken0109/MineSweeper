using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField]
    private Text _view = null;
    [SerializeField]
    private Image _Panelimage = null;
    [SerializeField]
    private CellState _cellState = CellState.None;
    [SerializeField]
    public GameObject _plane = null;
    [SerializeField]
    private GameObject _flagPanel = null;
    [SerializeField] GameObject m_explosion = null;
    public CellDisplayState _cellDisplayState = CellDisplayState.Cover;
    public int _cellID;
    public bool _bombFlag = false;
    bool _IsInputActive = false;
    //public bool _IsCovered = true;

    public CellState CellState
    {
        get => _cellState;
        set 
        { 
            _cellState = value;
            OnCellStateChanged();
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        //_IsCovered = true;
        _flagPanel.SetActive(false);
        m_explosion.SetActive(false);
        _plane.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        OnCellStateChanged();
        if (Input.GetMouseButtonDown(1) && _IsInputActive)
        {
            SetFlag();
            _IsInputActive = false;
        }
    }

    private void OnValidate()
    {
        OnCellStateChanged();
    }

    private void OnCellStateChanged()
    {
        if (_view == null) { return; }
        if (_cellState == CellState.None)
        {
            _view.text = "";
        }
        else if (_cellState == CellState.Mine)
        {
            _view.text = "X";
            _view.color = Color.red;
        }
        else
        {
            _view.text = ((int)_cellState).ToString();
            _view.color = Color.blue;
        }
    }

    public void Activetext()
    {
        _Panelimage.gameObject.SetActive(false);
        m_explosion.SetActive(true);
        _plane.SetActive(true);
        _cellDisplayState = CellDisplayState.Uncover;
    }

    public void IDCheck()
    {
        CellSetController.Instance.IDCheck(this);
    }

    public void SetFlag()
    {
        if (_cellDisplayState == CellDisplayState.Cover)
        {
            if (!_flagPanel.activeSelf)
            {
                _flagPanel.SetActive(true);
                CellSetController.Instance.DecreaseBombNum();
                _bombFlag = true;
            }
            else
            {
                _flagPanel.SetActive(false);
                CellSetController.Instance.IncreaseBombNum();
                _bombFlag = false;
            }
        } 
    }

    public void PointerEnter()
    {
        _IsInputActive = true;
    }

    public void PointerExit()
    {
        _IsInputActive = false;
    }

}
public enum CellState
{
    None = 0,
    One = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,

    Mine = -1,

}

public enum CellDisplayState
{
    Cover = 0,
    Uncover = 1,
    Checked = 2,
}

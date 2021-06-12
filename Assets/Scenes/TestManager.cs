using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager: MonoBehaviour
{
    private int _selectedIndexX = 0;
    private int _selectedIndexY = 0;
    private GameObject[,] _items = new GameObject[7, 5];

    void Start()
    {
        for (var i = 0; i < _items.GetLength(0); i++)
        {
            for (var k = 0; k < _items.GetLength(1); k++)
            {
                var item = GameObject.CreatePrimitive(PrimitiveType.Cube);
                item.transform.position = new Vector3(-4 + i * 2, -2 + k * 2, 0);
                _items[i, k] = item;
            }
        }
        UpdateItems();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _selectedIndexX--;
            UpdateItems();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _selectedIndexX++;
            UpdateItems();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _selectedIndexY++;
            UpdateItems();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _selectedIndexY--;
            UpdateItems();
        }
    }

    private void CapSelectedIndex()
    {
        var h = _items.GetLength(0);
        var v = _items.GetLength(1);

        _selectedIndexX =
                    _selectedIndexX < 0 ? 0 :
                    _selectedIndexX >= h ? h - 1 :
                    _selectedIndexX;

        _selectedIndexY =
                    _selectedIndexY < 0 ? 0 :
                    _selectedIndexY >= v ? v - 1 :
                    _selectedIndexY;
    }

    private void UpdateItems()
    {
        CapSelectedIndex();
        for (var i = 0; i < _items.GetLength(0); i++)
        {
            for (var k = 0; k < _items.GetLength(1); k++)
            {
                var renderer = _items[i, k].GetComponent<Renderer>();
                renderer.material.color =
                    (i == _selectedIndexX && k == _selectedIndexY ? Color.red : Color.white);
            }
        }
    }
}

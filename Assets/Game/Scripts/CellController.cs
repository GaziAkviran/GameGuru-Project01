using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CellController : MonoBehaviour
{
    private Vector2Int position;
    private bool isMarked;
    private IGridView gridView;

    [Inject]
    public void Construct(IGridView gridView)
    {
        this.gridView = gridView;
    }

    public void Init(int row, int col)
    {
        position = new Vector2Int(row, col);
        isMarked = false;
    }

    public void SetState(bool state)
    {
        isMarked = state;
    }
   
}

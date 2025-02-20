using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class CellController : MonoBehaviour
{
    [SerializeField, Foldout("References")] private CellAnimationController animationController;
    
    private Vector2Int position;
    private bool isMarked;
    private IGridView gridView;

    public bool IsMarked => isMarked;

    [Inject]
    public void Construct(IGridView gridView)
    {
        this.gridView = gridView;
    }

    public void Init(int row, int col)
    {
        position = new Vector2Int(row, col);
        isMarked = false;
        animationController.Init();
    }

    public void SetState(bool state)
    {
        isMarked = state;
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (isMarked)
        {
            animationController.PlayCellSelectAnimation();
        }
        else
        {
            animationController.PlayCellDeselectAnimation();
        }
    }

    private void OnMouseDown()
    {
        ToggleState();
    }

    private void ToggleState()
    {
        isMarked = !isMarked;
        UpdateVisual();
        gridView.UpdateCellVisual(position.x, position.y, isMarked);
    }
}

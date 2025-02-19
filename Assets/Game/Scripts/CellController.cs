using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

public class CellController : MonoBehaviour
{
    [SerializeField, Foldout("References")] CellAnimationController animationController;
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
        animationController.Init();
    }

    public void SetState(bool state)
    {
        isMarked = state;
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        Transform xMark = transform.Find("Visual/CellSprite");
        if (xMark != null)
        {
            xMark.gameObject.SetActive(isMarked);
        }
    }

    private void OnMouseDown()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ToggleState();
        }
    }

    private void ToggleState()
    {
        isMarked = !isMarked;
        UpdateVisual();
        gridView.UpdateCellVisual(position.x, position.y, isMarked);
    }
}

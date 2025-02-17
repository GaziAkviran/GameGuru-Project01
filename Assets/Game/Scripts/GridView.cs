using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

public class GridView : MonoBehaviour, IGridView
{
    [SerializeField, BoxGroup("Settings")] private float cellSize = 1f;
    [SerializeField, BoxGroup("Settings")] private float spacing = 0.1f;

    private CellFactory cellFactory;
    private GameObject[,] gridCells;

    private void Start()
    {
        CreateGrid(5);
    }

    [Inject]
    public void Construct(CellFactory cellFactory)
    {
        this.cellFactory = cellFactory;
    }

    public void CreateGrid(int size)
    {
        gridCells = new GameObject[size, size];
        float startX = -(size * (cellSize + spacing)) / 2;
        float startY = size * (cellSize + spacing) / 2;

        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                CreateCell(row, col, startX, startY);
            }
        }
    }

    private void CreateCell(int row, int col, float startX, float startY)
    {
        Vector3 position = new Vector3(startX + col * (cellSize + spacing), startY + row * (cellSize + spacing));
        
        var cellController = cellFactory.Create(new Vector2Int(row, col));
        cellController.transform.position = position;
        cellController.transform.parent = transform;
        
        gridCells[row, col] = cellController.gameObject;
    }
    
    public void UpdateCellVisual(int row, int col, bool state)
    {
        if (gridCells != null && gridCells[row, col] != null)
        {
            var cellController = gridCells[row, col].GetComponent<CellController>();
            if (cellController != null)
            {
                cellController.SetState(state);
            }
        }
    }

    public void RecalculateGrid(int size)
    {
        CleanupCurrentGrid();
        CreateGrid(size);
    }
    
    private void CleanupCurrentGrid()
    {
        if (gridCells == null) return;

        for (int row = 0; row < gridCells.GetLength(0); row++)
        {
            for (int col = 0; col < gridCells.GetLength(1); col++)
            {
                if (gridCells[row, col] != null)
                {
                    Destroy(gridCells[row, col]);
                }
            }
        }
    }
}

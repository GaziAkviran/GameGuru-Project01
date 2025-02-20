using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GridMatchChecker : IGridMatchChecker
{
    private readonly IGridView gridView;
    
    public GridMatchChecker(IGridView gridView)
    {
        this.gridView = gridView;
    }

    public void CheckAndClearMatches(int row, int col)
    {
        var matchedCells = FindAllMatches(row, col);
        ClearMatchedCells(matchedCells);
    }

    private HashSet<Vector2Int> FindAllMatches(int row, int col)
    {
        var matchedCells = new HashSet<Vector2Int>();
        AddHorizontalMatches(row, col, matchedCells);
        AddVerticalMatches(row, col, matchedCells);
        return matchedCells;
    }

    private void AddHorizontalMatches(int row, int col, HashSet<Vector2Int> matchedCells)
    {
        var horizontalMatches = CheckLine(row, col, Vector2Int.right);
        if (horizontalMatches.Count >= 3)
        {
            foreach(var match in horizontalMatches)
            {
                matchedCells.Add(match);
            }
        }
    }

    private void AddVerticalMatches(int row, int col, HashSet<Vector2Int> matchedCells)
    {
        var verticalMatches = CheckLine(row, col, Vector2Int.up);
        if (verticalMatches.Count >= 3)
        {
            foreach(var match in verticalMatches)
            {
                matchedCells.Add(match);
            }
        }
    }

    private void ClearMatchedCells(HashSet<Vector2Int> matchedCells)
    {
        foreach (var cell in matchedCells)
        {
            var cellObject = gridView.GetGridCells()[cell.x, cell.y];
            if (cellObject != null)
            {
                var cellController = cellObject.GetComponent<CellController>();
                if (cellController != null)
                {
                    cellController.PlayBlastEffect(() => 
                    {
                        gridView.UpdateCellVisual(cell.x, cell.y, false);
                    });
                }
            }
        }
    }

    private List<Vector2Int> CheckLine(int row, int col, Vector2Int direction)
    {
        var matches = new List<Vector2Int>();
        if (!IsMarked(row, col)) return matches;
        
        matches.Add(new Vector2Int(row, col));
        matches.AddRange(CheckLineDirection(row, col, direction));
        matches.AddRange(CheckLineDirection(row, col, -direction));
        
        return matches;
    }

    private List<Vector2Int> CheckLineDirection(int startRow, int startCol, Vector2Int direction)
    {
        var matches = new List<Vector2Int>();
        int currentRow = startRow + direction.x;
        int currentCol = startCol + direction.y;
        
        while (IsValidCell(currentRow, currentCol) && IsMarked(currentRow, currentCol))
        {
            matches.Add(new Vector2Int(currentRow, currentCol));
            currentRow += direction.x;
            currentCol += direction.y;
        }
        
        return matches;
    }

    private bool IsValidCell(int row, int col)
    {
        if (row < 0 || col < 0)
            return false;
            
        return row < gridView.GridSize && col < gridView.GridSize;
    }

    private bool IsMarked(int row, int col)
    {
        return gridView.IsCellMarked(row, col);
    }
}

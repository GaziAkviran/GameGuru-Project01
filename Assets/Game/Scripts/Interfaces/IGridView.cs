
using UnityEngine;

public interface IGridView
{
    void CreateGrid(int size);
    void UpdateCellVisual(int row, int col, bool state);
    void RecalculateGrid(int size);
    bool IsCellMarked(int row, int col);
    GameObject[,] GetGridCells();
    int GridSize { get; }
    float CellSize { get; }
    float Spacing { get; }

}


public interface IGridView
{
    void CreateGrid(int size);
    void UpdateCellVisual(int row, int col, bool state);
    void RecalculateGrid(int size);
}

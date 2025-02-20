using UnityEngine;
using Zenject;

public class CameraController : MonoBehaviour
{
    private Camera mainCamera;
    private IGridView gridView;

    [Inject]
    public void Construct(IGridView gridView)
    {
        this.gridView = gridView;
        mainCamera = Camera.main;
        AdjustCameraSize(gridView.GridSize);
    }

    public void AdjustCameraSize(int gridSize)
    {
        float cellSize = gridView.CellSize;
        float spacing = gridView.Spacing;

        float gridWidth = gridSize * (cellSize + spacing);
        float gridHeight = gridSize * (cellSize + spacing);

        float screenRatio = (float)Screen.width / Screen.height;
        
        float requiredOrthoSize = gridHeight / 2f;
        float requiredWidthSize = (gridWidth / screenRatio) / 2f;
        
        float padding = 1.5f;

        mainCamera.orthographicSize = Mathf.Max(requiredOrthoSize, requiredWidthSize) + padding;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GridUIController : MonoBehaviour
{
    [SerializeField, Foldout("References")] private TMP_InputField gridSizeInput;
    [SerializeField, Foldout("References")] private Button buildButton;
    
    private IGridView gridView;
    private CameraController cameraController; 
    [Inject]
    public void Construct(IGridView gridView, CameraController cameraController)
    {
        this.gridView = gridView;
        this.cameraController = cameraController;
    }

    private void Start()
    {
        buildButton.onClick.AddListener(OnBuildButtonClicked);
    }

    private void OnBuildButtonClicked()
    {
        if (int.TryParse(gridSizeInput.text, out int newSize) && newSize > 0)
        {
            gridView.RecalculateGrid(newSize);
            cameraController.AdjustCameraSize(newSize);
        }
    }
}

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
    [SerializeField, Foldout("References")] private TMP_Text matchCountText;
    [SerializeField, Foldout("References")] private Button buildButton;
    
    private IGridView gridView;
    private IGridMatchChecker matchChecker;
    
    private CameraController cameraController; 
    [Inject]
    public void Construct(IGridView gridView, CameraController cameraController, IGridMatchChecker matchChecker)
    {
        this.gridView = gridView;
        this.cameraController = cameraController;
        this.matchChecker = matchChecker;
    }

    private void Start()
    {
        buildButton.onClick.AddListener(OnBuildButtonClicked);
        UpdateMatchCount();
    }

    private void OnBuildButtonClicked()
    {
        if (int.TryParse(gridSizeInput.text, out int newSize) && newSize > 0)
        {
            gridView.RecalculateGrid(newSize);
            cameraController.AdjustCameraSize(newSize);
            UpdateMatchCount();
        }
    }
    
    public void UpdateMatchCount()
    {
        matchCountText.text = "Match Count: " + matchChecker.MatchCount;
    }
}

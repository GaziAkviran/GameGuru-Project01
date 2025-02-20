using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField, Foldout("References")] private GameObject cellPrefab;
    [SerializeField, Foldout("References")] private GridView gridViewPrefab;

    public override void InstallBindings()
    {
        Container.Bind<GameObject>()
            .WithId("CellPrefab")
            .FromInstance(cellPrefab)
            .AsSingle();

        Container.BindFactory<Vector2Int, CellController, CellFactory>()
            .FromFactory<CellFactory>();
        
        var gridView = Container.InstantiatePrefabForComponent<GridView>(gridViewPrefab);
        Container.Bind<IGridView>().To<GridView>().FromInstance(gridView).AsSingle();
        
        
        var gridUI = FindObjectOfType<GridUIController>();
        Container.Bind<GridUIController>().FromInstance(gridUI).AsSingle();
    
        var checker = new GridMatchChecker(gridView, gridUI);
        Container.Bind<IGridMatchChecker>()
            .FromInstance(checker)
            .AsSingle();
        
        gridView.SetMatchChecker(checker);
        
        var cameraController = FindObjectOfType<CameraController>();
        Container.Bind<CameraController>().FromInstance(cameraController).AsSingle();
        
       
    }
}

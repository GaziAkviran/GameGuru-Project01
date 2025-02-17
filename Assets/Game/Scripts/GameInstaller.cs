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
        
        Container.Bind<IGridView>()
            .To<GridView>()
            .FromComponentInNewPrefab(gridViewPrefab)
            .AsSingle();
    }
}

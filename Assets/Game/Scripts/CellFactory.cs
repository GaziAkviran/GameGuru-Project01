using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CellFactory : PlaceholderFactory<Vector2Int, CellController>
{
    private readonly DiContainer container;
    private readonly GameObject cellPrefab;

    [Inject]
    public CellFactory(DiContainer container, [Inject(Id = "CellPrefab")] GameObject cellPrefab)
    {
        this.container = container;
        this.cellPrefab = cellPrefab;
    }

    public override CellController Create(Vector2Int position)
    {
        var instance = container.InstantiatePrefabForComponent<CellController>(cellPrefab);
        instance.Init(position.x, position.y);
        return instance;
    }
}

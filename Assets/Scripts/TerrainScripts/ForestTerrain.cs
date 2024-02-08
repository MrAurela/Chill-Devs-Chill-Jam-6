using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestTerrain : TerrainTile
{
    public override void SpawnPrefab()
    {
        resourcePath = "TerrainTiles/ForestTerrain";
        tileType = Enums.TerrainType.FOREST;
        base.SpawnPrefab();
    }

    private void Awake()
    {
    }

    public override void AffectNearTiles()
    {

    }
    public override void UpdateTile()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesolateTerrain : TerrainTile
{
    public override void SpawnPrefab()
    {
        resourcePath = "TerrainTiles/DesolateTerrain";
        base.SpawnPrefab();
    }

    private void Awake()
    {
        tileType = Enums.TerrainType.DESOLATE;
    }

    public override void AffectNearTiles()
    {

    }
    public override void UpdateTile()
    {

    }
}

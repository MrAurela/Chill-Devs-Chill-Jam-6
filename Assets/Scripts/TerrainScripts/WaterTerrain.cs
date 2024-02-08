using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTerrain : TerrainTile
{
    public override void SpawnPrefab()
    {
        resourcePath = "TerrainTiles/WaterTerrain";
        tileType = Enums.TerrainType.WATER;
        base.SpawnPrefab();
    }

    public override void AffectNearTiles()
    {

    }
    public override void UpdateTile()
    {

    }
}

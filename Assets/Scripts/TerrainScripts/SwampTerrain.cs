using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwampTerrain : TerrainTile
{
    public override void SpawnPrefab()
    {
        resourcePath = "TerrainTiles/SwampTerrain";
        tileType = Enums.TerrainType.SWAMP;
        base.SpawnPrefab();
    }
    public override void AffectNearTiles()
    {

    }
    public override void UpdateTile()
    {

    }
}

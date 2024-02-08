using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockTerrain : TerrainTile
{
    public override void SpawnPrefab()
    {
        resourcePath = "TerrainTiles/RockTerrain";
        tileType = Enums.TerrainType.ROCK;
        base.SpawnPrefab();
    }

    public override void AffectNearTiles()
    {

    }
    public override void UpdateTile()
    {

    }
}

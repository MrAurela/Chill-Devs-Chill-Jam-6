using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockTerrain : TerrainTile
{
    public override void SpawnPrefab()
    {
        resourcePath = "TerrainTiles/RockTerrain";
        placingRule = Resources.Load<TerrainPlacingRules>("TerrainData/RockPlacingRule");
        tileType = Enums.TerrainType.ROCK;
        base.SpawnPrefab();
    }
}

//using Mono.Cecil; REMOVED - CAUSES BUILD TO FAIL
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTerrain : TerrainTile
{
    public override void SpawnPrefab()
    {
        resourcePath = "TerrainTiles/WaterTerrain";
        placingRule = Resources.Load<TerrainPlacingRules>("TerrainData/WaterPlacingRule");
        tileType = Enums.TerrainType.WATER;
        base.SpawnPrefab();
    }
}

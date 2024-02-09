using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestTerrain : TerrainTile
{
    public override void SpawnPrefab()
    {
        resourcePath = "TerrainTiles/ForestTerrain";
        placingRule = Resources.Load<TerrainPlacingRules>("TerrainData/ForestPlacingRule");
        tileType = Enums.TerrainType.FOREST;
        base.SpawnPrefab();
    }
}

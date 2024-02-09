using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwampTerrain : TerrainTile
{
    public override void SpawnPrefab()
    {
        resourcePath = "TerrainTiles/SwampTerrain";
        placingRule = Resources.Load<TerrainPlacingRules>("TerrainData/SwampPlacingRule");
        tileType = Enums.TerrainType.SWAMP;
        base.SpawnPrefab();
    }
}

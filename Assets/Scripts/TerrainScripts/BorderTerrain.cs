using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BorderTerrain : TerrainTile
{
    public override void SpawnPrefab()
    {
        placingRule = Resources.Load<TerrainPlacingRules>("TerrainData/DesolatePlacingRule");
        resourcePath = "TerrainTiles/BorderTerrain";
        tileType = Enums.TerrainType.BORDER;
        base.SpawnPrefab();
    }
}

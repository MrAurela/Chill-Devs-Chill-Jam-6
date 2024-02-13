using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DesolateTerrain : TerrainTile
{
    public override void SpawnPrefab()
    {
        placingRule = Resources.Load<TerrainPlacingRules>("TerrainData/DesolatePlacingRule");
        resourcePath = "TerrainTiles/DesolateTerrain";
        tileType = Enums.TerrainType.DESOLATE;
        base.SpawnPrefab();
    }

    public override int CheckPlacingRules(bool verbose = false)
    {
        if (token != null && creatureToken != null)
        {
            //hex.SetCreatureErrorMarker(true);
            creatureToken.ErrorToggle(true);
            return -1;
        }
        else
        {
            return 0;
        }
    }
}

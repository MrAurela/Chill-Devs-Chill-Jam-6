using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesolateTerrain : TerrainTile
{
    public override void SpawnPrefab()
    {
        resourcePath = "TerrainTiles/DesolateTerrain";
        tileType = Enums.TerrainType.DESOLATE;
        base.SpawnPrefab();
    }
}

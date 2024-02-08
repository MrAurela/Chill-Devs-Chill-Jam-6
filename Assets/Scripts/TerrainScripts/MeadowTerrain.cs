using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeadowTerrain : TerrainTile
{
    public override void SpawnPrefab()
    {
        resourcePath = "TerrainTiles/MeadowTerrain";
        tileType = Enums.TerrainType.MEADOW;
        base.SpawnPrefab();
    }

    private void Awake()
    {
    }

    public override void AffectNearTiles()
    {

    }
    public override void UpdateTile()
    {

    }
}

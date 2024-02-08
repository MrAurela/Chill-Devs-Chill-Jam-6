using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestTerrain : TerrainTile
{
    public override void SpawnPrefab()
    {
        resourcePath = "TerrainTiles/ForestTerrain";
        base.SpawnPrefab();
    }

    private void Awake()
    {
        tileType = Enums.TerrainType.FOREST;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void AffectNearTiles()
    {

    }
    public override void UpdateTile()
    {

    }
}

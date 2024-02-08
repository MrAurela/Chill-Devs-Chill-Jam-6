using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesolateTerrain : TerrainTile
{
    public override void SpawnPrefab()
    {
        resourcePath = "TerrainTiles/DesolateTerrain";
        base.SpawnPrefab();
    }

    private void Awake()
    {
        tileType = Enums.TerrainType.DESOLATE;
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

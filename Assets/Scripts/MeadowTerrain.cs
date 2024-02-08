using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeadowTerrain : TerrainTile
{
    public override void SpawnPrefab()
    {
        resourcePath = "TerrainTiles/MeadowTerrain";
        base.SpawnPrefab();
    }

    private void Awake()
    {
        tileType = Enums.TerrainType.MEADOW;
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

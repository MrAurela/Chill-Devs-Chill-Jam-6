using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyTile : TileClass
{
    private void Awake()
    {
        tileType = TileUtils.TileType.EMPTY;
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

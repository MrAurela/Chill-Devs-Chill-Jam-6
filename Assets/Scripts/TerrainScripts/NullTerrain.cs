using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullTerrain : TerrainTile
{
    public override int CheckPlacingRules(bool verbose = false)
    {
        return 0;
    }
}

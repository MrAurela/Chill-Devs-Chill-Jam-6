//using Mono.Cecil; REMOVED - CAUSES BUILD TO FAIL
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTerrain : TerrainTile
{
    public override void SpawnPrefab()
    {
        resourcePath = "TerrainTiles/WaterTerrain";
        placingRule = Resources.Load<TerrainPlacingRules>("TerrainData/WaterPlacingRule");
        tileType = Enums.TerrainType.WATER;
        base.SpawnPrefab();
    }

    public override bool CheckPlacingRules(CubeIndex _index)
    {
        int bad = 0, good = 0;
        Dictionary<string, Enums.TerrainType> nearTerrains = Grid.inst.Neighbours(_index);

        foreach(Enums.TerrainType near in nearTerrains.Values)
        {
            if(placingRule.forbiddenTerrains.Contains(near))
            {
                bad++;
            }
            else if(placingRule.allowedTerrains.Contains(near))
            {
                good++;
            }
        }

        switch(placingRule.priority)
        {
            case Enums.RulePriority.BAD:
                return bad > placingRule.maxNegativeRule;

            case Enums.RulePriority.GOOD:
                return good > placingRule.minPositiveRule;

            case Enums.RulePriority.BOTH:
                return (bad > placingRule.maxNegativeRule && good > placingRule.minPositiveRule);
        }

        return false;
    }
}

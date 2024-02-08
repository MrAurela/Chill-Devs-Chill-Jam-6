using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Terrain Placing Rule", menuName = "Terrain/Placing Rule")]
public class TerrainPlacingRules : ScriptableObject
{
    [Range(0, 6)]
    public int maxNegativeRule = 1;
    public List<Enums.TerrainType> forbiddenTerrains;
    [Space]
    [Space]
    [Range(0, 6)]
    public int minPositiveRule = 1;
    public List<Enums.TerrainType> allowedTerrains;
    [Space]
    [Space]
    [Range(0, 6)]
    public int mandatoryTiles = 0;
    public Enums.TerrainType mandatoryTerrain;
    [Space]
    [Space]
    public Enums.RulePriority priority = Enums.RulePriority.BAD;
}


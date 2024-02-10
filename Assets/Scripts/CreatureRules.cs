using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Creture Placement Rule", menuName = "Creature Placement Rules")]
public class CreatureRules : ScriptableObject
{
    public List<Enums.TerrainType> habitant; // Must live on one of these
    public List<Enums.CreatureType> preys; // Must be next to at least one of these
    public List<Enums.CreatureType> friends; // Must be next to all of these
    public List<Enums.CreatureType> enemies; // Must be next to none of these
    [Range(0f, 1f)] public float spreadingProbability;
    [Range(0, 6)] public int maxSpreadingPerTurn;
}

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

    public bool CheckRules(Enums.TerrainType _terrain, List<Enums.CreatureType> _creatures)
    {
        return CheckHabitantRule(_terrain) && CheckPreyRule(_creatures) && CheckFriendRule(_creatures) && CheckEnemyRule(_creatures);
    }

    // Is the animal on the right terrain?
    private bool CheckHabitantRule(Enums.TerrainType terrain)
    {
        foreach (Enums.TerrainType possibleTerrain in habitant)
        {
            if (possibleTerrain == terrain) return true;
        }
        //Debug.Log("Habitant rule failed. Current terrain: " + terrain + " Required terrains: " + habitant.ToString());
        return false;
    }

    // Is at least 1 animal of prey type found?
    private bool CheckPreyRule(List<Enums.CreatureType> _creatures)
    {
        foreach (Enums.CreatureType creature in preys)
        {
            foreach (Enums.CreatureType nearCreature in _creatures)
            {
                if (nearCreature == creature) return true;
            }
        }

        if (preys.Count != 0) Debug.Log("Prey rule failed.");
        // If there is no prey, the rule is still valid
        return preys.Count == 0;
    }

    // Is at 1 animal of all friend type is found?
    private bool CheckFriendRule(List<Enums.CreatureType> _creatures)
    {
        foreach (Enums.CreatureType creature in friends)
        {
            int friends = 0;

            foreach (Enums.CreatureType nearCreature in _creatures)
            {
                if (nearCreature == creature)
                {
                    friends += 1;
                    break;
                }
            }

            if (friends == 0)
            {
                Debug.Log("Friend rule failed.");
                return false;
            }
        }

        // If there is no friend, the rule is still valid
        return true;
    }

    // Is there no enemy creatures?
    private bool CheckEnemyRule(List<Enums.CreatureType> _creatures)
    {
        foreach (Enums.CreatureType creature in enemies)
        {
            foreach (Enums.CreatureType nearCreature in _creatures)
            {
                if (nearCreature == creature)
                {
                    Debug.Log("Enemy rule failed.");
                    return false;
                }
            }
        }

        // If there is no enemy, the rule is still valid
        return true;
    }
}

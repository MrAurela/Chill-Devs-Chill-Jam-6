using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Terrain Placing Rule", menuName = "Terrain/Placing Rule")]
public class TerrainPlacingRules : ScriptableObject
{
    [SerializeField]
    public List<Rule> rules;
    [SerializeField]
    public Enums.RuleLogic mixLogic;

    public bool CheckRules(List<Enums.TerrainType> _terrains)
    {
        int goodChecks = 0;
        bool result = false;

        foreach(Rule _rule in rules)
        {
            if(_rule.kind == Enums.RuleKind.ONLY_ALLOWED) //special case for Only Allowed rule
            {
                result = true;
                foreach (Enums.TerrainType nearTerrain in _terrains)
                {
                    foreach (Enums.TerrainType terrain in _rule.affectedTerrains)
                    {
                        if (terrain != nearTerrain)
                        {
                            Debug.Log("Prohibited Terrain Found");
                            result = false;
                        }
                    }
                }
            }
            else   // normal checks
            {
                int currentTarget = 0;
                foreach (Enums.TerrainType nearTerrain in _terrains)
                {
                    foreach (Enums.TerrainType terrain in _rule.affectedTerrains)
                    {
                        if (terrain == nearTerrain)
                            currentTarget++;
                    }
                }
                switch (_rule.kind)
                {
                    case Enums.RuleKind.MIN_ALLOWED:
                        if (currentTarget >= _rule.targetCount)
                        {
                            Debug.Log("Good Terrain Found");
                            goodChecks++;
                        }

                        break;
                    case Enums.RuleKind.MAX_ALLOWED:
                        if (currentTarget <= _rule.targetCount)
                        {
                            goodChecks++;
                            Debug.Log("Bad Terrain Found");
                        }
                        break;
                    case Enums.RuleKind.ONLY_ALLOWED:
                        if (currentTarget == _rule.targetCount)
                        {
                            goodChecks++;
                            Debug.Log("Bad Terrain Found");
                        }
                        break;
                    default:
                        break;
                }

                switch (mixLogic)
                {
                    case Enums.RuleLogic.OR:
                        if (goodChecks > 0)
                            result = true;
                        else
                            result = false;
                        break;

                    case Enums.RuleLogic.AND:
                        if (goodChecks == rules.Count)
                            result = true;
                        else
                            result = false;
                        break;
                }
            }
        }

        //final check of the result
        if(result)
        {
            Debug.Log("RULE TRUE");
            Debug.Log("-----------------------------------------------------------");
            return true;
        }
        else
        {
            Debug.Log("RULE FALSE");
            Debug.Log("-----------------------------------------------------------");
            return false;
        }
    }
}

[System.Serializable]
public class Rule
{
    public List<Enums.TerrainType> affectedTerrains;
    [Range(0, 6)]
    public int targetCount =1;
    public Enums.RuleKind kind;
}



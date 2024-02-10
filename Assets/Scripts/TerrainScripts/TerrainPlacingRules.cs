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


    public bool CheckRules(List<Enums.TerrainType> _terrains, bool verbose = false)
    {
        int goodChecks = 0;
        bool result = false, isolated = true;

        foreach (Rule _rule in rules)
        {
            if (_rule.kind == Enums.RuleKind.ONLY_ALLOWED) //special case for Only Allowed rule
            {
                result = true;
                foreach (Enums.TerrainType nearTerrain in _terrains)
                {
                    bool isAllowed = false;
                    if (nearTerrain == Enums.TerrainType.NULL)
                        continue;

                    foreach (Enums.TerrainType allowed in _rule.affectedTerrains)
                    {
                        if(allowed == nearTerrain)
                        {
                            isAllowed = true;
                        }
                    }
                    if(!isAllowed)
                    {
                        result = false;
                        break;
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
                            if (verbose)
                                Debug.Log("Good Terrain Found");
                            goodChecks++;
                        }

                        break;
                    case Enums.RuleKind.MAX_ALLOWED:
                        if (currentTarget <= _rule.targetCount)
                        {
                            goodChecks++;
                            if (verbose)
                                Debug.Log("Good Terrain Found");
                        }
                        break;
                    case Enums.RuleKind.EXACTLY_ALLOWED:
                        if (currentTarget == _rule.targetCount)
                        {
                            goodChecks++;
                            if (verbose)
                                Debug.Log("Good Terrain Found");
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
        if (result)
        {
            if (verbose)
                Debug.Log("RULE TRUE");
            return true;
        }
        else
        {
            if (verbose)
                Debug.Log("RULE FALSE");
            return false;
        }
    }
    /*
    public bool CheckRules(Enums.TerrainType _terrain, bool verbose = false)
    {
        int goodChecks = 0;
        bool result = false, isolated = true;

        //if isolated just skip other checks, it's always prohibited
        foreach (Enums.TerrainType nearTerrain in _terrain)
        {
            if (nearTerrain != Enums.TerrainType.DESOLATE)
            {
                isolated = false;
            }
        }
        if (isolated)
        {
            result = false;
        }
        else
        {
            foreach (Rule _rule in rules)
            {
                if (_rule.kind == Enums.RuleKind.ONLY_ALLOWED) //special case for Only Allowed rule
                {
                    result = true;
                    foreach (Enums.TerrainType nearTerrain in _terrain)
                    {
                        bool isAllowed = false;
                        if (nearTerrain == Enums.TerrainType.NULL)
                            continue;

                        foreach (Enums.TerrainType allowed in _rule.affectedTerrains)
                        {
                            if (allowed == nearTerrain)
                            {
                                isAllowed = true;
                            }
                        }
                        if (!isAllowed)
                        {
                            result = false;
                            break;
                        }
                    }
                }
                else   // normal checks
                {
                    int currentTarget = 0;
                    foreach (Enums.TerrainType nearTerrain in _terrain)
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
                                if (verbose)
                                    Debug.Log("Good Terrain Found");
                                goodChecks++;
                            }

                            break;
                        case Enums.RuleKind.MAX_ALLOWED:
                            if (currentTarget <= _rule.targetCount)
                            {
                                goodChecks++;
                                if (verbose)
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
        }


        //final check of the result
        if (result)
        {
            if (verbose)
                Debug.Log("RULE TRUE");
            return true;
        }
        else
        {
            if (verbose)
                Debug.Log("RULE FALSE");
            return false;
        }
    }*/
}




[System.Serializable]
public class Rule
{
    public List<Enums.TerrainType> affectedTerrains;
    [Range(0, 6)]
    public int targetCount = 1;
    public Enums.RuleKind kind;
}



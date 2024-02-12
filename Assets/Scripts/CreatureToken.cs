using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureToken
{
    public string name;
    public Sprite sprite;
    public Enums.CreatureType creatureType;
    public CreatureRules creatureRules;

    public void Set(string name, Sprite sprite, Enums.CreatureType creatureType, CreatureRules creatureRules)
    {
        this.name = name;
        this.sprite = sprite;
        this.creatureType = creatureType;
        this.creatureRules = creatureRules;
    }
}



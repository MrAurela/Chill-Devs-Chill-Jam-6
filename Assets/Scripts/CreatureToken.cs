using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureToken
{

    public string name;
    public Sprite sprite;
    public CreatureRules creatureRule;

    public void Set(string name, Sprite sprite, CreatureRules creatureRule)
    {
        this.name = name;
        this.sprite = sprite;
        this.creatureRule = creatureRule;
    }
}



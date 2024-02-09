using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards")]
public class CardData : ScriptableObject
{
    public string title;
    public string description;
    public Sprite image;
    public Color color;

    public Enums.CardType cardType;

    public Enums.TerrainType tileType;

    // If Card is a Token
    // If multiple tokens, randomly choose one
    public Enums.CreatureType[] tokenType;

    // Probability to add a token to the tile
    public float probability;

}

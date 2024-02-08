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

    public enum CardType { Tile, Token, Event }
    public CardType cardType;

    public Enums.TerrainType tileType;

    // If Card is a Token
    // If multiple tokens, randomly choose one
    public enum TokenType { Bird, Hawk, Rabbit, Bear, Wolf, SmallFish, BigFish, Deer, WildBoar, Heron, Raccoon, Trash, Human, Fire, None }
    public TokenType[] tokenType;

    // Probability to add a token to the tile
    public float probability;

}

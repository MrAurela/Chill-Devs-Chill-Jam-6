using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards")]
public class CardData : ScriptableObject
{
    public string title;
    public string description;
    public Sprite image;
    public string terrain;
    public string animal;

}

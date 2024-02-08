using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    public Card[] cards;

    public void DrawCard()
    {
        Debug.Log("Draw card");

        // Draw a card from the deck
        Card card = cards[Random.Range(0, cards.Length)];
        FindObjectOfType<Hand>().AddCard(Instantiate(card));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    [SerializeField] GameObject cardPrefab;

    //public Card[] cards;
    public CardData[] cards;

    public void DrawCard()
    {
        // Draw a card from the deck
        GameObject gameObject = Instantiate(cardPrefab, transform);
        Card card = gameObject.GetComponent<Card>();
        CardData cardData = cards[Random.Range(0, cards.Length)];
        card.Set(cardData);
        FindObjectOfType<Hand>().AddCard(card);
    }
}

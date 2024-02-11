using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    [SerializeField] GameObject cardPrefab;

    //public Card[] cards;
    public CardData[] cards;
    public int[] counts;
    public int cardCountMultiplier;

    public int drawnCards; //TODO: make private set

    private List<CardData> deck;

    void Awake()
    {
        drawnCards = 0;

        deck = new List<CardData>();
        for (int i = 0; i < cards.Length; i++)
        {
            for (int j = 0; j < counts[i] * cardCountMultiplier; j++)
            {
                deck.Add(cards[i]);
            }
        }
    }

    public void DrawCard()
    {
        if (deck.Count == 0) return;

        // Draw a card from the deck
        GameObject gameObject = Instantiate(cardPrefab, transform);
        Card card = gameObject.GetComponent<Card>();
        
        int index = Random.Range(0, deck.Count);
        CardData cardData = deck[index];
        deck.RemoveAt(index);
        card.Set(cardData);
        FindObjectOfType<Hand>().AddCard(card);

        drawnCards += 1;
    }

    public int GetCardsLeft()
    {
        return deck.Count;
    }
}

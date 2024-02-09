using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public GameObject[] positions;

    public List<Card> cards;
    public int max_cards = 3;

    void Start()
    {
        // Initialize empty hand of cards
        cards = new List<Card>();
        for (int i  = 0; i < max_cards; i++) cards.Add(null);

        // Add starting cards
        for (int i = 0; i < max_cards; i++) FindObjectOfType<CardDeck>().DrawCard();
    }
    
    // Replace missing card
    public void AddCard(Card card)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i] == null)
            {
                cards[i] = card;
                card.transform.SetParent(positions[i].transform);
                card.transform.localPosition = Vector3.zero;
                return;
            }
        }
    }

    public void RemoveCard(Card card)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i] == card)
            {
                cards[i] = null;
                return;
            }
        }
    }
}
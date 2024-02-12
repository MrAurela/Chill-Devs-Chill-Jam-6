using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeckIcon : MonoBehaviour
{

    [SerializeField] GameObject[] deckIconCards;
    [SerializeField] TextMeshProUGUI textField;

    private CardDeck deck;

    // Start is called before the first frame update
    void Start()
    {
        deck = FindObjectOfType<CardDeck>();
    }

    // Update is called once per frame
    void Update()
    {
        int cardsLeft = deck.GetCardsLeft();
        int cardsFull = deck.startCount;

        if (cardsLeft > 0) textField.text = cardsLeft.ToString();
        else textField.text = "";

        if (cardsLeft == 0) deckIconCards[0].SetActive(false);
        if ((float) cardsLeft / (float) cardsFull < 0.2f) deckIconCards[1].SetActive(false);
        if ((float) cardsLeft / (float) cardsFull < 0.4f) deckIconCards[2].SetActive(false);
        if ((float) cardsLeft / (float) cardsFull < 0.6f) deckIconCards[3].SetActive(false);
        if ((float) cardsLeft / (float) cardsFull < 0.8f) deckIconCards[4].SetActive(false);

        Debug.Log(((float) cardsLeft / (float) cardsFull).ToString());
    }
}

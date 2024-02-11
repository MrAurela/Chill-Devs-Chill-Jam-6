using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeckIcon : MonoBehaviour
{

    [SerializeField] Image iconField;
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
        textField.text = deck.GetCardsLeft().ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreField;

    private CardDeck cardDeck;
    private Hand hand;
    private bool gameEnded = false;

    void Start()
    {
        cardDeck = FindObjectOfType<CardDeck>();
        hand = FindObjectOfType<Hand>();
    }

    //When set active:
    void Update()
    {
        if (cardDeck.GetCardsLeft() + hand.GetCardCount() == 0  && !gameEnded)
        {
            gameEnded = true;

            //Show end screen: set all child objects active:
            foreach (Transform child in transform) child.gameObject.SetActive(true);

            int score = FindObjectOfType<Grid>().UpdateScore();
            scoreField.text = score.ToString();
        }
        
    }

    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}

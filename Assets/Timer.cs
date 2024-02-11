using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] float timeToDestroyCard = 10f;

    private CardDeck deck;

    // Start is called before the first frame update
    void Start()
    {
        //Set the max value of the slider to the timeToDestroyCard
        slider.maxValue = timeToDestroyCard;
        //Set the value of the slider to the timeToDestroyCard
        slider.value = timeToDestroyCard;

        deck = FindObjectOfType<CardDeck>();
    }

    // Update is called once per frame
    void Update()
    {
        if (deck.GetCardsLeft() > 0 )
        {
            //Decrease value by spent time:
            slider.value -= Time.deltaTime;

            //Update the value of the slider:
            slider.value = Mathf.Clamp(slider.value, 0, timeToDestroyCard);

            //If the value of the slider is less than or equal to 0, destroy a card from the deck and reset the value of the slider:
            if (slider.value <= 0)
            {
                FindObjectOfType<CardDeck>().DestroyCard();
                slider.value = timeToDestroyCard;
            }
        } else
        {
            gameObject.SetActive(false);
        }
        
    }
}

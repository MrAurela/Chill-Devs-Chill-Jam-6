using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Card : MonoBehaviour
{
    [SerializeField] string title, description;
    [SerializeField] Sprite image;
    [SerializeField] TextMeshProUGUI titleField, descriptionField;
    [SerializeField] Image imageField;
    [SerializeField] Color color;

    private Vector3 startLocation, startSize;

    // Start is called before the first frame update
    void Start()
    {
        titleField.text = title;
        descriptionField.text = description;
        if (image != null) imageField.sprite = image;
        else imageField.color = color;
    }

    void Excecute(GameObject target)
    {
        Debug.Log("Execute called on: " + target.name);
        target.GetComponent<Renderer>().material.color = color;

        // Remove the card from the hand and draw a new card. Destroy the card object at the end.
        FindObjectOfType<Hand>().RemoveCard(this);
        FindObjectOfType<CardDeck>().DrawCard();
        Destroy(gameObject);
    }

    
    public void DragStartHandler(BaseEventData data)
    {
        PointerEventData pointer = (PointerEventData)data;
        startLocation = transform.position;
        startSize = transform.localScale;

        // Make card smaller
        transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
    }
    
    public void DragHandler(BaseEventData data)
    {
        PointerEventData pointer = (PointerEventData)data;
        
        Canvas canvas = transform.parent.parent.parent.GetComponent<Canvas>();
        RectTransform rectTransform = GetComponent<RectTransform>();

        // Convert the pointer's screen position directly to the canvas' space
        Vector2 movePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent.GetComponent<RectTransform>(),
            pointer.position,
            canvas.worldCamera,
            out movePos);

        // Adjust the position based on the canvas scale and the calculated position
        rectTransform.anchoredPosition = movePos;
    }

    public void DragEndHandler(BaseEventData data)
    {
        PointerEventData pointer = (PointerEventData)data;

        //Get any Hex type GameObject that mouse hovers over:
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(pointer.position);
        if (Physics.Raycast(ray, out hit))
        {
            GameObject objectHit = hit.transform.gameObject;
            Excecute(objectHit);
        }
        else
        {
            // Reset the card's position and size
            transform.position = startLocation;
            transform.localScale = startSize;
        }
        
    }
    

    


}

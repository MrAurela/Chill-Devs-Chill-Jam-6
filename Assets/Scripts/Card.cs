using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Card : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI titleField, descriptionField;
    [SerializeField] Image imageField;

    private Vector3 startLocation, startSize;

    public CardData card;

    public void Set(CardData card)
    {
        this.card = card;

        titleField.text = card.title;
        descriptionField.text = card.description;
        if (card.image != null) imageField.sprite = card.image;
        else imageField.color = card.color;
    }

    void Excecute(TerrainTile terrain)
    {
        //Grid.inst.IsTilePlayable(idx)
        CubeIndex idx = terrain.index;
        Grid.inst.SwapTile(idx, card.tileType);

        // Remove the card from the hand and draw a new card. Destroy the card object at the end.
        FindObjectOfType<Hand>().RemoveCard(this);
        FindObjectOfType<CardDeck>().DrawCard();
        Destroy(gameObject);
    }

    /* Card can be played on a spot if it or neaby space is not DESOLATE */
    public bool IsValidPlay(TerrainTile terrain)
    {
        // First turn is always valid:
        if (FindObjectOfType<CardDeck>().drawnCards == FindObjectOfType<Hand>().max_cards) return true; 
        
        // Replacing with the same card is always invalid:
        if (terrain.tileType == card.tileType) return false;

        // Otherwise, check that the tile has neighbour which is not DESOLATE...
        List<GameObject> neighbours = Grid.inst.Neighbours(terrain.index);
        for (int i = 0; i < neighbours.Count; i++)
        {
            if (neighbours[i].GetComponent<TerrainTile>().tileType != Enums.TerrainType.DESOLATE) return true;
        }

        // ..or the tile itself is not DESOLATE.
        return terrain.tileType != Enums.TerrainType.DESOLATE;
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
            TerrainTile tile = objectHit.transform.parent.GetComponent<TerrainTile>();
            if (tile != null)
            {
                if (IsValidPlay(tile))
                {
                    Excecute(tile);
                    return;
                }
            }
        }
       
        // Otherwise, Reset the card's position and size
        transform.position = startLocation;
        transform.localScale = startSize;
    }
    

    


}

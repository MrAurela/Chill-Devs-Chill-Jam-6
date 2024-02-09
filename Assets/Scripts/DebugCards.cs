using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class DebugCards : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI titleField;
    [SerializeField] Image imageField;

    private Vector3 startLocation, startSize;

    public CardData card;

    private void Start()
    {
        titleField.text = card.title;
        if (card.image != null) imageField.sprite = card.image;
        else imageField.color = card.color;
    }

    void Excecute(GameObject target)
    {
        CubeIndex idx = target.GetComponentInParent<TerrainTile>().index;

        if (Grid.inst.TryPlaceTile(idx, card.tileType))
        {
            Grid.inst.SwapTile(idx, card.tileType);

            transform.position = startLocation;
            transform.localScale = startSize;
        }
        else
        {
            transform.position = startLocation;
            transform.localScale = startSize;
        }
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

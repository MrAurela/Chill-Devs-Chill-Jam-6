using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.SearchService;
using Unity.VisualScripting;


public class Card : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI titleField, descriptionField;
    [SerializeField] Image imageField, cardTypeIconField;

    private Vector3 startLocation, startSize;
    private AudioSource cameraAudioSource;

    public CardData card;
    public AudioClip pickingSound;
    public AudioClip placingSound;
    public AudioClip returnSound;

    [Range(0, 1f)]
    public float volume = 0.5f;

    public void Set(CardData card)
    {
        this.card = card;

        titleField.text = card.title;
        descriptionField.text = card.description;
        if (card.image != null) imageField.sprite = card.image;
        else imageField.color = card.color;
        if (card.cardTypeIcon != null) cardTypeIconField.sprite = card.cardTypeIcon;
    }

    void Excecute(TerrainTile target)
    {
        if (!IsValidPlay(card, target))
        {
            ReturnCard();
            return;
        }
        PlaySource(placingSound);
        CubeIndex idx = target.GetComponentInParent<TerrainTile>().index;

        if (card.cardType == Enums.CardType.TILE)
        {
            // Remove the card from the hand and draw a new card. Destroy the card object at the end.
            FindObjectOfType<Hand>().RemoveCard(this);
            FindObjectOfType<CardDeck>().DrawCard();

            Grid.inst.SwapTile(idx, card.tileType, card);

            Destroy(gameObject);

            FindObjectOfType<RaccoonExpressions>().UpdateExpression();

        } else if (card.cardType == Enums.CardType.TOKEN)
        {
            // Remove the card from the hand and draw a new card. Destroy the card object at the end.
            FindObjectOfType<Hand>().RemoveCard(this);
            FindObjectOfType<CardDeck>().DrawCard(); 
            
            Grid.inst.AddToken(idx, card);
            
            Destroy(gameObject);
        }
    }

    /* Card can be played on a spot if it or neaby space is not DESOLATE */
    public bool IsValidPlay(CardData card, TerrainTile terrain)
    {
        //If card is a terrain:
        if (card.cardType == Enums.CardType.TILE)
        {

            // Replacing with the same card is always invalid:
            if (terrain.tileType == card.tileType) return false;

            // Otherwise, check that the tile has neighbour which is not DESOLATE/NULL...
            List<Enums.TerrainType> neighbours = Grid.inst.Neighbours(terrain.index).Values.ToList();
            for (int i = 0; i < neighbours.Count; i++)
            {
                if (neighbours[i] != Enums.TerrainType.DESOLATE && neighbours[i] != Enums.TerrainType.NULL) return true;
            }

            // ..or the tile itself is not DESOLATE.
            return terrain.tileType != Enums.TerrainType.DESOLATE;
        } else if (card.cardType == Enums.CardType.TOKEN)
        {
            //Debug.Log(terrain.token);
            // There cannot be tokens on the same space:
            return terrain.token == null;
        } else
        {
            // If card is not a terrain or a creature, it is invalid.
            //TODO: Events
            return false;
        }
    }

    
    public void DragStartHandler(BaseEventData data)
    {
        PointerEventData pointer = (PointerEventData)data;
        startLocation = transform.position;
        startSize = transform.localScale;

        // Make card smaller
        transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        PlaySource(pickingSound);
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
        RaycastHit[] hits;
        Ray ray = Camera.main.ScreenPointToRay(pointer.position);

        hits = Physics.RaycastAll(ray);

        foreach (RaycastHit hit in hits)
        {
            GameObject objectHit = hit.transform.gameObject;
            TerrainTile tile = objectHit.transform.parent.GetComponent<TerrainTile>();
            if (tile != null)
            {
                Excecute(tile);
            }
        }

        // Otherwise, Reset the card's position and size
        ReturnCard();
    }

    private void ReturnCard()
    {
        transform.position = startLocation;
        transform.localScale = startSize;
        PlaySource(returnSound);
    }

    private void Start()
    {
        Camera.main.gameObject.TryGetComponent(out cameraAudioSource);

        if (cameraAudioSource == null)
        {
            cameraAudioSource = Camera.main.gameObject.AddComponent<AudioSource>();
        }
        cameraAudioSource.volume = volume;
    }

    private void PlaySource(AudioClip _clip)
    {
        if (cameraAudioSource == null) return;

        cameraAudioSource.PlayOneShot(_clip);
    }

}

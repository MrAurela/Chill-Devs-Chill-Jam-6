using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorCardDisplay : MonoBehaviour
{
    [SerializeField] GameObject terrainCard, creatureCard;
    [SerializeField] GameObject terrainCardError, creatureCardError, terrainCardSuccess, creatureCardSuccess;

    // Start is called before the first frame update
    void Start()
    {
        HideCards();
    }

    // Update is called once per frame
    void Update()
    {
        //Get any Hex type GameObject that mouse hovers over:
        RaycastHit[] hits;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        hits = Physics.RaycastAll(ray);

        foreach (RaycastHit hit in hits)
        {
            GameObject objectHit = hit.transform.gameObject;
            TerrainTile tile = objectHit.transform.parent.GetComponent<TerrainTile>();
            if (tile != null)
            {
                DisplayCards(tile);
                return;
            }
        }

        HideCards();
    }

    private void DisplayCards(TerrainTile tile)
    {
        if (tile.terrainCardData != null)
        {
            terrainCard.SetActive(true);
            terrainCard.GetComponentInChildren<Card>().Set(tile.terrainCardData);

            if (tile.CheckPlacingRulesTerrain() > 0)
            {
                terrainCardError.SetActive(false);
                terrainCardSuccess.SetActive(true);
            }
            else
            {
                terrainCardError.SetActive(true);
                terrainCardSuccess.SetActive(false);
            }
        }
        else
        {
            terrainCard.SetActive(false);
        }

        if (tile.creatureCardData != null)
        {
            creatureCard.gameObject.SetActive(true);
            creatureCard.GetComponentInChildren<Card>().Set(tile.creatureCardData);

            if (tile.CheckPlacingRulesCreature() > 0)
            {
                creatureCardError.SetActive(false);
                creatureCardSuccess.SetActive(true);
            }
            else
            {
                creatureCardError.SetActive(true);
                creatureCardSuccess.SetActive(false);
            }
        }
        else
        {
            creatureCard.gameObject.SetActive(false);
        }
    }

    private void HideCards()
    {
        terrainCard.gameObject.SetActive(false);
        creatureCard.gameObject.SetActive(false);
        terrainCardError.SetActive(false);
        creatureCardError.SetActive(false);
        terrainCardSuccess.SetActive(false);
        creatureCardSuccess.SetActive(false);
    }
}

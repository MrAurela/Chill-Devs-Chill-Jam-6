using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class CreatureTokenObject : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textField;
    [SerializeField] SpriteRenderer imageField;

    private CardData token; // Not necessarily needed
    void Awake()
    {
        HideToken();
    }

    void Update()
    {
        /*if (Input.GetMouseButtonDown(0)) // Checks if the left mouse button was clicked
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform) // Check if the ray hit this GameObject
                {
                    Click(null);
                }
            }
        }*/
    }

    public void DisplayToken(CardData token)
    {
        this.token = token;

        textField.text = token.title;
        if (token.tokenImage != null)
        {
            imageField.sprite = token.tokenImage;
            imageField.transform.localScale = Vector3.one * 0.1f;
        }
        else
        {
            imageField.color = token.color;
            imageField.transform.localScale = Vector3.one * 2.5f;
        }

        textField.enabled = true;
        imageField.enabled = true;
    }

    public void HideToken()
    {
        textField.enabled = false;
        imageField.enabled = false;
    }

    public void Click(BaseEventData data)
    {
        Debug.Log("Testing click on token: " + token.name);
    }

    public void ErrorToggle(bool toggle)
    {
        if (imageField == null) return;

        imageField.material.SetFloat("_EnableError", toggle ? 1 : 0);
    }
}

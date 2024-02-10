using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class CreatureTokenObject : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textField;
    [SerializeField] SpriteRenderer imageField;

    private CardData token; // Not necessarily needed

    void Start()
    {
        HideToken();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Checks if the left mouse button was clicked
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
        }
    }

    public void DisplayToken(CardData token)
    {
        this.token = token;

        textField.text = token.title;
        if (token.image != null) imageField.sprite = token.image;
        else imageField.color = token.color;

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



}

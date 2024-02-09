using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class CreatureTokenObject : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textField;
    [SerializeField] SpriteRenderer imageField;

    private CreatureToken token;

    private bool on; //TODO: remove, for testing only

    void Start()
    {
        token = new CreatureToken();
        token.name = "Test";
        token.sprite = imageField.sprite;

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

    public void DisplayToken(CreatureToken token)
    {
        this.token = token;
        textField.text = token.name;
        imageField.sprite = token.sprite;
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

        on = !on;
        if (on)
        {
            DisplayToken(token);
        } else
        {
            HideToken();
        }
        
    }



}

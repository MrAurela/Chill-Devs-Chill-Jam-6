using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorMarker : MonoBehaviour
{
    private SpriteRenderer renderer;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        if (renderer != null)
            renderer.enabled = false;
    }
    public void EnableMarker()
    {
        if (renderer != null)
            renderer.enabled = true;
    }
    public void DisableMarker()
    {
        if (renderer != null)
            renderer.enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorMarker : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        DisableMarker();
    }
    public void EnableMarker()
    {
        if (meshRenderer != null) meshRenderer.enabled = true;
        if (spriteRenderer != null) spriteRenderer.enabled = true;
    }
    public void DisableMarker()
    {
        if (meshRenderer != null) meshRenderer.enabled = false;
        if (spriteRenderer != null) spriteRenderer.enabled = false;
    }
}

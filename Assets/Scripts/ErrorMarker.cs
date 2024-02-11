using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorMarker : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
            meshRenderer.enabled = false;
    }
    public void EnableMarker()
    {
        if (meshRenderer != null)
            meshRenderer.enabled = true;
    }
    public void DisableMarker()
    {
        if (meshRenderer != null)
            meshRenderer.enabled = false;
    }
}

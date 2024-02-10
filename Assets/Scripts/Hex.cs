using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{
    [SerializeField] ErrorMarker terrainErrorMarker;
    [SerializeField] ErrorMarker creatureErrorMarker;

    public void SetTerrainErrorMarker(bool on)
    {
        if (on) terrainErrorMarker.EnableMarker();
        else terrainErrorMarker.DisableMarker();
    }

    public void SetCreatureErrorMarker(bool on)
    {
        if (on) creatureErrorMarker.EnableMarker();
        else creatureErrorMarker.DisableMarker();
    }
}

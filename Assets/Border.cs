using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static Enums;
using Unity.VisualScripting;

public class Border : MonoBehaviour
{
    public float randomHeight = 0.1f;
    public static Border inst;

    private void Awake()
    {
        if (!inst)
            inst = this;
    }

    public void FillBorder()
    {
        Dictionary<string, GameObject> gridNoBorders = new Dictionary<string, GameObject>();

        for (int i = 0; i < Grid.inst.Tiles.Count; i++)
        {
            GameObject ob = Grid.inst.Tiles.Values.ElementAt(i);
            CubeIndex _idx = ob.GetComponent<TerrainTile>().index;
            List<TerrainType> neighbourTerrains = Grid.inst.Neighbours(_idx).Values.ToList();

            foreach (TerrainType t in neighbourTerrains)
            {
                if (t == TerrainType.NULL)
                {
                    gridNoBorders.Add(_idx.ToString(), ob);
                    break;
                }
            }
        }

        TerrainTile borderTerrain = null;

        foreach (GameObject ob in gridNoBorders.Values.ToList())
        {
            float h = UnityEngine.Random.Range(-randomHeight, randomHeight);

            ob.transform.position = ob.transform.position + (Vector3.up * h);
            ob.transform.parent = transform;
                
            CubeIndex _idx = ob.GetComponent<TerrainTile>().index;
            ob.GetComponent<TerrainTile>().Delete();

            for (int i = 1; i < ob.transform.childCount; i++)
            {
                Destroy(ob.transform.GetChild(i).gameObject);
            }

            borderTerrain = ob.AddComponent<BorderTerrain>();
            borderTerrain.index = _idx;
            borderTerrain.SpawnPrefab();
            ob.GetComponent<TerrainTile>().tileType = TerrainType.NULL;
        }
    }
}

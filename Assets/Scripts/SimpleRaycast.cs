using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SimpleRaycast : MonoBehaviour
{
    public Grid grid;
    Camera cam;
    RaycastHit hit;
    Ray ray;
    Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        cam=gameObject.GetComponent<Camera>();
        ray = new Ray();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.GetComponentInParent<TerrainTile>() != null)
                {
                    CubeIndex idx = hit.transform.GetComponentInParent<TerrainTile>().index;
                    //grid.SwapTile(idx, Enums.TerrainType.MEADOW); //Does not work anymore, third parameter CardData is required
                }
            }
            return;
        }
        if (Input.GetMouseButtonDown(1))
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.GetComponentInParent<TerrainTile>() != null)
                {
                    CubeIndex idx = hit.transform.GetComponentInParent<TerrainTile>().index;
                    //grid.SwapTile(idx, Enums.TerrainType.FOREST);
                }
            }
            return;
        }
    }
}

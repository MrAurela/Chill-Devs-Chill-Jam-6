using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static TileUtils;

public class TileClass : MonoBehaviour
{
    //Cube coordinates, used for easier algorithms 
    private Vector3Int cubicCoord;

    public Vector3Int CubicCoord
    {
        get { return CubicCoord; }
        set
        {
            CubicCoord = value;
        }
    }

    public TileType tileType;

    public bool SetHexLocation(Vector3Int _v)
    {
        if(_v.x + _v.y + _v.z == 0)
        {
            cubicCoord = _v;
            return true;
        }
        else
        {
            return false;
        }
    }
    public Vector3Int GetHexLocation()
    {
        return cubicCoord;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void AffectNearTiles()
    {

    }
    public virtual void UpdateTile()
    {

    }
}

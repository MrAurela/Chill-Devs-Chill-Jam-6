using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEngine.UI.Image;

public static class TileUtils
{
    public enum TileType
    {
        NULL,
        EMPTY,
        GROUND,
        WATER,
        FOREST
    }
    public static bool CheckHexLocation(Vector3Int _v)
    {
        if (_v.x + _v.y + _v.z == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static Vector2Int CubicToAxial(Vector3Int _cubic)
    {
        return (Vector2Int)_cubic;
    }

    public static Vector3Int AxialToCubic(Vector2Int _axial)
    {
        return new Vector3Int(_axial.x, _axial.y, - _axial.x - _axial.y);
    }

    public static Vector3[] HexDirections = new Vector3[6]{
        new Vector3Int(1, 0,-1),
        new Vector3Int(1, -1,0),
        new Vector3Int(0, -1,1),
        new Vector3Int(-1, 0,1),
        new Vector3Int(-1, 1,0),
        new Vector3Int(0, 1,-1),
    };
}

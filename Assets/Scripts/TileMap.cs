using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;

[ExecuteInEditMode]
public class TileMap : MonoBehaviour
{
    [SerializeField]
    public short size = 7;
    [SerializeField]
    public bool update = false;
    [SerializeField]
    public bool clean = false;

    [SerializeField]
    GameObject groundTile;

    public TileClass[][] tileArray;

    public Vector3Int TileNeighbor(Vector3Int _location, int _dir)
    {
        switch (_dir)
        {
            case 1:
                return _location + new Vector3Int(1, 0, -1);
            case 2:
                return _location + new Vector3Int(1, -1, 0);
            case 3:
                return _location + new Vector3Int(0, -1, 1);
            case 4:
                return _location + new Vector3Int(-1, 0, 1);
            case 5:
                return _location + new Vector3Int(-1, 1, 0);
            case 6:
                return _location + new Vector3Int(0, 1, -1);
            default:
                return _location;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (clean)
        {
            for (int i = transform.childCount - 1; i > 0; i--)
            {
                GameObject.DestroyImmediate(transform.GetChild(i).gameObject);
            }
            clean = false;
        }
    }

    private void OnValidate()
    {
        if (update)
        {
            UpdateMap();
            update = false;
        }
        else if(false)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    tileArray[i][j] = null;
                    foreach (Transform child in gameObject.transform)
                    {
                        GameObject.DestroyImmediate(child.gameObject);
                        clean = false;
                    }
                }
            }
        }
    }

    public struct Orientation
    {
        public Vector4 forward;
        public Vector4 backward;
        public float start_angle; // in multiples of 60°

        public Orientation(Vector4 _forward, Vector4 _backward, float _start_angle)
        {
            this.forward = _forward;
            this.backward = _backward;
            this.start_angle = _start_angle;
        }
    };

    public Orientation orientation;

    Orientation layout_pointy = new Orientation(
        new Vector4(Mathf.Sqrt(3), Mathf.Sqrt(3) / 2f, 0, 3 / 2f),
        new Vector4(Mathf.Sqrt(3) / 3f, -1 / 3f, 0, 2 / 3f),
        0.5f);

    Orientation layout_flat = new Orientation(
        new Vector4(3 / 2f, 0, Mathf.Sqrt(3) / 2.0f, Mathf.Sqrt(3)),
        new Vector4(2 / 3f, 0, -1 / 3f, Mathf.Sqrt(3) / 3f),
        0);

    public Vector3 TileToPixel(Vector2Int _v2)
    {
        Orientation M = layout_flat;
        float x = (M.forward.x * _v2.x + M.forward.y * _v2.y) * size*3/4;
        float y = (M.forward.y * _v2.x + M.forward.z * _v2.y) * size;
        return new Vector3(x, 0, y);
    }

    private void UpdateMap()
    {
        Debug.Log("Updating map");
        tileArray = new TileClass[size][];

        for(int i = 0; i<size; i++)
        {
            tileArray[i] = new TileClass[size];

            for(int j = 0; j<size; j++)
            {
                Vector3Int _cubicLocation = TileUtils.AxialToCubic(new Vector2Int(i, j));

                if (TileUtils.CheckHexLocation(_cubicLocation))
                {
                    GameObject ob = Instantiate(groundTile, TileToPixel(new Vector2Int(i, j)), Quaternion.identity);
                    tileArray[i][j] = ob.GetComponent<TileClass>();
                    ob.transform.parent = gameObject.transform;
                }
                else
                {
                    TileClass nullTile = new TileClass();
                    nullTile.tileType = TileUtils.TileType.NULL;
                    tileArray[i][j] = nullTile;
                }
            }
        }
    }
}

using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class Grid : MonoBehaviour {
	public static Grid inst;

	//Map settings
	public MapShape mapShape = MapShape.Rectangle;
	public int mapWidth;
	public int mapHeight;

	//Hex Settings
	public HexOrientation hexOrientation = HexOrientation.Flat;
	public float hexRadius = 0.5f;

	//Internal variables
	private Dictionary<string, GameObject> grid = new Dictionary<string, GameObject>();
	public List<string> tileList = new List<string>();

	private CubeIndex[] directions = 
		new CubeIndex[] {
			new CubeIndex(1, -1, 0), 
			new CubeIndex(1, 0, -1), 
			new CubeIndex(0, 1, -1), 
			new CubeIndex(-1, 1, 0), 
			new CubeIndex(-1, 0, 1), 
			new CubeIndex(0, -1, 1)
		};

	[SerializeField] private GameObject hexPrefab;

	#region Getters and Setters
	public Dictionary<string, GameObject> Tiles {
		get {return grid;}
	}
	#endregion


	public void SwapTile(CubeIndex _idx, Enums.TerrainType _type)
	{
        foreach (KeyValuePair<string, GameObject> entry in grid)
        {
            Debug.Log("Key: " + entry.Key);
            Debug.Log("Terrain: " + entry.Value);

			if(entry.Key == _idx.ToString())
			{
				Debug.Log("Found the Key");
			}
        }

        if (TileObjectAt(_idx) == null)
		{
			Debug.Log("No Object in list at" + _idx);
            return;
		}

		GameObject ob = TileObjectAt(_idx);
		TerrainTile newTerrain = null;
        TileObjectAt(_idx).GetComponent<TerrainTile>().Delete();

		switch(_type)
		{
			case Enums.TerrainType.DESOLATE:
                newTerrain = ob.AddComponent<DesolateTerrain>();
				break;
            case Enums.TerrainType.MEADOW:
                newTerrain = ob.AddComponent<MeadowTerrain>();
                break;
            case Enums.TerrainType.FOREST:
                newTerrain = ob.AddComponent<ForestTerrain>();
                break;
            default:
				Debug.LogError("Error in enum terrain type");
				break;
        }
		if(newTerrain != null)
		{
            newTerrain.SpawnPrefab();
			grid[_idx.ToString()] = ob;
        }
    }

	#region Public Methods
	public void GenerateGrid() {
		//Generating a new grid, clear any remants and initialise values
		ClearGrid();

		//Generate the grid shape
		switch(mapShape) {
		case MapShape.Hexagon:
			GenHexShape();
			break;

		case MapShape.Rectangle:
			GenRectShape();
			break;

		case MapShape.Parrallelogram:
			GenParrallShape();
			break;

		default:
			break;
		}
	}

	public void ClearGrid() {
		Debug.Log ("Clearing grid...");

		foreach(var tile in grid)
		{
			if(tile.Value != null)
			{
                DestroyImmediate(tile.Value.gameObject, false);
            }
        }

		Transform[] tr = gameObject.GetComponentsInChildren<Transform>();
		for(int i = 1; i< tr.Length; i++)
		{
			DestroyImmediate(tr[i].gameObject);
        }

        grid.Clear();
		tileList.Clear();
	}

	public GameObject TileObjectAt(CubeIndex index){
        if (grid.ContainsKey(index.ToString()))
            return grid[index.ToString()];
        return null;
    }

	public GameObject TileAt(int x, int y, int z){
		return TileObjectAt(new CubeIndex(x,y,z));
	}

	public GameObject TileAt(int x, int z){
		return TileObjectAt(new CubeIndex(x,z));
	}

	public List<GameObject> Neighbours(CubeIndex index) {
		List<GameObject> ret = new List<GameObject>();
		CubeIndex o;

		for(int i = 0; i < 6; i++) {
			o = index + directions[i];
			if(grid.ContainsKey(o.ToString()))
				ret.Add(grid[o.ToString()]);
		}
		return ret;
	}
/*
	public List<TerrainTile> TilesInRange(TerrainTile center, int range){
		//Return tiles rnage steps from center, http://www.redblobgames.com/grids/hexagons/#range
		List<TerrainTile> ret = new List<TerrainTile>();
		CubeIndex o;

		for(int dx = -range; dx <= range; dx++){
			for(int dy = Mathf.Max(-range, -dx-range); dy <= Mathf.Min(range, -dx+range); dy++){
				o = new CubeIndex(dx, dy, -dx-dy) + center.index;
				if(grid.ContainsKey(o.ToString()))
					ret.Add(grid[o.ToString()]);
			}
		}
		return ret;
	}

	public List<TerrainTile> TilesInRange(CubeIndex index, int range){
		return TilesInRange(TileAt(index), range);
	}

	public List<TerrainTile> TilesInRange(int x, int y, int z, int range){
		return TilesInRange(TileAt(x,y,z), range);
	}

	public List<TerrainTile> TilesInRange(int x, int z, int range){
		return TilesInRange(TileAt(x,z), range);
	}
*/
	public int Distance(CubeIndex a, CubeIndex b){
		return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y) + Mathf.Abs(a.z - b.z);
	}

	public int Distance(TerrainTile a, TerrainTile b){
		return Distance(a.index, b.index);
	}
	#endregion

	#region Private Methods
	private void Awake() {
		if(!inst)
			inst = this;

		GenerateGrid();
	}

	private void GenHexShape() {
		Debug.Log ("Generating hexagonal shaped grid...");

        TerrainTile tile;
		Vector3 pos = Vector3.zero;

		int mapSize = Mathf.Max(mapWidth, mapHeight);
		
		for (int q = -mapSize; q <= mapSize; q++){
			int r1 = Mathf.Max(-mapSize, -q-mapSize);
			int r2 = Mathf.Min(mapSize, -q+mapSize);
			for(int r = r1; r <= r2; r++){
				switch(hexOrientation){
				case HexOrientation.Flat:
					pos.x = hexRadius * 3.0f/2.0f * q;
					pos.z = hexRadius * Mathf.Sqrt(3.0f) * (r + q/2.0f);
					break;
					
				case HexOrientation.Pointy:
					pos.x = hexRadius * Mathf.Sqrt(3.0f) * (q + r/2.0f);
					pos.z = hexRadius * 3.0f/2.0f * r;
					break;
				}
				
				tile = CreateHexGO( pos,("Hex[" + q + "," + r + "," + (-q-r).ToString() + "]"));
                tile.index = new CubeIndex(q, r, -q - r);
                grid.Add(tile.index.ToString(), tile.gameObject);
			}
		}
	}

	private void GenRectShape() {
		Debug.Log ("Generating rectangular shaped grid...");

        TerrainTile tile;
		Vector3 pos = Vector3.zero;

		switch(hexOrientation){
		case HexOrientation.Flat:
			for(int q = 0; q < mapWidth; q++){
				int qOff = q>>1;
                for (int r = -qOff; r < mapHeight - qOff; r++){
					pos.x = hexRadius * 3.0f/2.0f * q;
					pos.z = hexRadius * Mathf.Sqrt(3.0f) * (r + q/2.0f);
					
					tile = CreateHexGO( pos,("Hex[" + q + "," + r + "," + (-q-r).ToString() + "]"));
                    tile.index = new CubeIndex(q, r, -q - r);
                    grid.Add(tile.index.ToString(), tile.gameObject);
                    tileList.Add(tile.index.ToString());
				}
			}/*
            foreach (KeyValuePair<string, TerrainTile> entry in grid)
            {
                Debug.Log("Key: " + entry.Key);
                Debug.Log("Terrain: " + entry.Value);
			}*/
            break;
			
		case HexOrientation.Pointy:
			for(int r = 0; r < mapHeight; r++){
				int rOff = r>>1;
				for (int q = -rOff; q < mapWidth - rOff; q++){
					pos.x = hexRadius * Mathf.Sqrt(3.0f) * (q + r/2.0f);
					pos.z = hexRadius * 3.0f/2.0f * r;
					
					tile = CreateHexGO( pos,("Hex[" + q + "," + r + "," + (-q-r).ToString() + "]"));
					//tile.SpawnPrefab();
					grid.Add(tile.index.ToString(), tile.gameObject);
				}
			}
			break;
		}
	}

	private void GenParrallShape() {
		Debug.Log ("Generating parrellelogram shaped grid...");

        TerrainTile tile;
		Vector3 pos = Vector3.zero;

		for (int q = 0; q <= mapWidth; q++){
			
			for(int r = 0; r <= mapHeight; r++){
				switch(hexOrientation){
				case HexOrientation.Flat:
					pos.x = hexRadius * 3.0f/2.0f * q;
					pos.z = hexRadius * Mathf.Sqrt(3.0f) * (r + q/2.0f);
					break;
					
				case HexOrientation.Pointy:
					pos.x = hexRadius * Mathf.Sqrt(3.0f) * (q + r/2.0f);
					pos.z = hexRadius * 3.0f/2.0f * r;
					break;
				}

				tile = CreateHexGO( pos,("Hex[" + q + "," + r + "," + (-q-r).ToString() + "]"));
                tile.index = new CubeIndex(q, r, -q - r);
                grid.Add(tile.index.ToString(), tile.gameObject);
			}
		}
	}

	private TerrainTile CreateHexGO(Vector3 position, string name) {
        GameObject go = new GameObject(name, typeof(DesolateTerrain));

        go.transform.position = position;
        go.transform.parent = this.transform;

        TerrainTile newDesolateTerr = go.GetComponent<TerrainTile>();
		newDesolateTerr.SpawnPrefab();
        return newDesolateTerr;
	}
	#endregion
}

[System.Serializable]
public enum MapShape {
	Rectangle,
	Hexagon,
	Parrallelogram
}

[System.Serializable]
public enum HexOrientation {
	Pointy,
	Flat
}
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using System.Reflection;
using TMPro;
using static Enums;
using static UnityEngine.Rendering.VolumeComponent;
using UnityEngine.Rendering.Universal;
using Unity.Mathematics;

public class Grid : MonoBehaviour {
	public static Grid inst;

	//Map settings
	public MapShape mapShape = MapShape.Rectangle;
	public int mapWidth;
	public int mapHeight;
	[Space]
	public float randomHeight =0.05f;
	public float randomRotation = 2f;
    //Hex Settings
	[Space]
    public HexOrientation hexOrientation = HexOrientation.Flat;
	public float hexRadius = 0.5f;

	[Space]
	public TextMeshProUGUI scoreText;
	public int globalScore = 0;

	[Space]
	public CardData startTerrain;
	public CardData startToken;

	//Internal variables
	private Dictionary<string, GameObject> grid = new Dictionary<string, GameObject>();

    private CubeIndex[] directions = 
		new CubeIndex[] {
			new CubeIndex(1, -1, 0), 
			new CubeIndex(1, 0, -1), 
			new CubeIndex(0, 1, -1), 
			new CubeIndex(-1, 1, 0), 
			new CubeIndex(-1, 0, 1), 
			new CubeIndex(0, -1, 1)
		};
    [Space][SerializeField] private GameObject hexPrefab;

	#region Getters and Setters
	public Dictionary<string, GameObject> Tiles {
		get {return grid;}
	}
	#endregion

	
	public bool TryPlaceTile(CubeIndex _index, Enums.TerrainType _type)
	{
		if (TileObjectAt(_index).GetComponent<TerrainTile>().tileType == _type)
		{
			return false;
		}
		return true;
	}

	public void SwapTile(CubeIndex _idx, Enums.TerrainType _type, CardData _card)
	{
        if (TileObjectAt(_idx) == null)
		{
			Debug.LogError("No Object in list at" + _idx);
            return;
		}

		GameObject ob = TileObjectAt(_idx);
		TerrainTile newTerrain = null;
		CardData oldCreature = ob.GetComponent<TerrainTile>().creatureCardData;

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
            case Enums.TerrainType.ROCK:
                newTerrain = ob.AddComponent<RockTerrain>();
                break;
            case Enums.TerrainType.SWAMP:
                newTerrain = ob.AddComponent<SwampTerrain>();
                break;
            case Enums.TerrainType.WATER:
                newTerrain = ob.AddComponent<WaterTerrain>();
                break;
            default:
				Debug.LogError("Error in enum terrain type");
				break;
        }
		if(newTerrain != null)
		{
			newTerrain.index = _idx;
            newTerrain.SpawnPrefab();
            newTerrain.terrainCardData = _card;
			if (oldCreature != null) AddToken(_idx, oldCreature);
			else UpdateScore();
        }
    }

	public void AddToken(CubeIndex _idx, CardData _card)
	{
        GameObject ob = TileObjectAt(_idx);
		//ob.token = new CreatureToken(_card.title, _card.sprite, _card.creatureType);
		CreatureTokenObject creatureTokenObject = ob.GetComponentsInChildren<CreatureTokenObject>()[0]; //There should be only one token per tile
		creatureTokenObject.DisplayToken(_card);
		ob.GetComponent<TerrainTile>().token = new CreatureToken();
		ob.GetComponent<TerrainTile>().token.Set(_card.title, _card.image, _card.tokenType[0], _card.creatureRules);
		ob.GetComponent<TerrainTile>().creatureCardData = _card;

		UpdateScore();
    }

	public void UpdateScore()
	{
        globalScore = 0;
        foreach (GameObject ob in Tiles.Values)
		{
            globalScore += ob.GetComponent<TerrainTile>().CheckPlacingRules();
		}
        scoreText.text = globalScore.ToString();
    }
    
	#region Public Methods
    public void GenerateGrid()
    {
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
		/*
		foreach(KeyValuePair<string, GameObject> entry in Tiles)
        {
			CubeIndex id = new CubeIndex(entry.Key);
            List<TerrainType> neighbourTerrains = Grid.inst.Neighbours(id).Values.ToList();
			if(neighbourTerrains.Contains(Enums.TerrainType.NULL))
			{
				TerrainTile tile;
                entry.Value.TryGetComponent(out tile);
				tile.Delete();
				tile = entry.Value.AddComponent<BorderTerrain>();
				tile.index = id;
				tile.SpawnPrefab();
			}
        }*/
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

	public Dictionary<string, Enums.TerrainType> Neighbours(CubeIndex index) {
        Dictionary<string, Enums.TerrainType> dic = new Dictionary<string, Enums.TerrainType>();
		CubeIndex o;

		for (int i = 0; i < 6; i++)
		{
			o = index + directions[i];

			if (grid.ContainsKey(o.ToString()))
			{
				TerrainTile t = grid[o.ToString()].GetComponent<TerrainTile>();

                if (t != null)
				{
					dic.Add(t.index.ToString(), t.tileType);
				}
			}
			else
			{
                dic.Add(o.ToString(), Enums.TerrainType.NULL);
			}
		}
        return dic;
    }

    public Dictionary<string, Enums.CreatureType> CreatureNeighbours(CubeIndex index)
    {
        Dictionary<string, Enums.CreatureType> dic = new Dictionary<string, Enums.CreatureType>();
        CubeIndex o;

        for (int i = 0; i < 6; i++)
        {
            o = index + directions[i];

            if (grid.ContainsKey(o.ToString()))
            {
                TerrainTile t = grid[o.ToString()].GetComponent<TerrainTile>();
				

                if (t != null)
                {
                    if (t.token != null) dic.Add(t.index.ToString(), t.token.creatureType);
					else dic.Add(t.index.ToString(), Enums.CreatureType.NULL);
                }
            }
            else
            {
                dic.Add(o.ToString(), Enums.CreatureType.NULL);
            }
        }
        return dic;
    }

    public Dictionary<Enums.HexDirection, Enums.TerrainType> NeighboursDirections(CubeIndex index)
    {
        Dictionary<Enums.HexDirection, Enums.TerrainType> dic = new Dictionary<Enums.HexDirection, Enums.TerrainType>();
        CubeIndex o;

        for (int i = 0; i < 6; i++)
        {
            o = index + directions[i];
			Enums.HexDirection dir = Enums.HexDirection.NULL;
			
			switch(i)
			{
				case 0:
					dir= Enums.HexDirection.NORTH_EAST;
					break;
                case 1:
                    dir = Enums.HexDirection.SOUTH_EAST;
                    break;
                case 2:
                    dir = Enums.HexDirection.SOUTH;
                    break;
                case 3:
                    dir = Enums.HexDirection.SOUTH_WEST;
                    break;
                case 4:
                    dir = Enums.HexDirection.NORTH_WEST;
                    break;
                case 5:
                    dir = Enums.HexDirection.NORTH;
                    break;
            }
            if (grid.ContainsKey(o.ToString()))
            {
                TerrainTile t = grid[o.ToString()].GetComponent<TerrainTile>();

                if (t != null)
                {
                    dic.Add(dir, t.tileType);
                }
            }
            else
            {
                dic.Add(dir, Enums.TerrainType.NULL);
            }
        }
        return dic;
    }

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
		UpdateScore();
    }

	private void Start()
	{
        SwapTile(new CubeIndex(5, 1, -6), startTerrain.tileType, startTerrain);
        AddToken(new CubeIndex(5, 1, -6), startToken);
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

        switch (hexOrientation){
		case HexOrientation.Flat:
			for(int q = 0; q < mapWidth; q++){
				int qOff = q>>1;
                for (int r = -qOff; r < mapHeight - qOff; r++){
                    pos.x = hexRadius * 3.0f/2.0f * q;
					pos.z = hexRadius * Mathf.Sqrt(3.0f) * (r + q/2.0f);
					
					tile = CreateHexGO( pos,("Hex[" + q + "," + r + "," + (-q-r).ToString() + "]"));
                    tile.index = new CubeIndex(q, r, -q - r);
                    grid.Add(tile.index.ToString(), tile.gameObject);
				}
			}
            break;
			
		case HexOrientation.Pointy:
			for(int r = 0; r < mapHeight; r++){
				int rOff = r>>1;
				for (int q = -rOff; q < mapWidth - rOff; q++){
					pos.x = hexRadius * Mathf.Sqrt(3.0f) * (q + r/2.0f);
					pos.z = hexRadius * 3.0f/2.0f * r;
					
					tile = CreateHexGO( pos,("Hex[" + q + "," + r + "," + (-q-r).ToString() + "]"));
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

        GameObject go = Instantiate(hexPrefab);
		go.name = name;
		go.transform.position = position;
        go.transform.parent = this.transform;
		go.AddComponent<DesolateTerrain>();
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
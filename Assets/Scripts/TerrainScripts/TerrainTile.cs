﻿using UnityEngine;
using System.Collections.Generic;
using static Enums;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using System.Linq;
using static UnityEngine.Rendering.DebugUI;
using System;

public class TerrainTile : MonoBehaviour {

	protected Hex hex;
	protected TerrainPlacingRules placingRule;
    protected GameObject terrainGraphic;
    protected CreatureTokenObject creatureToken;

    public string resourcePath;

	[HideInInspector]
    public TerrainType tileType;
    public CubeIndex index;
	public CreatureToken token;

	public CardData terrainCardData, creatureCardData;
	
    public static Vector3 Corner(Vector3 origin, float radius, int corner, HexOrientation orientation){
		float angle = 60 * corner;
		if(orientation == HexOrientation.Pointy)
			angle += 30;
		angle *= Mathf.PI / 180;
		return new Vector3(origin.x + radius * Mathf.Cos(angle), 0.0f, origin.z + radius * Mathf.Sin(angle));
	}

	public virtual void SpawnPrefab()
	{
		GameObject prefab = Resources.Load<GameObject>(resourcePath);
        creatureToken = GetComponentInChildren<CreatureTokenObject>();
        hex = gameObject.GetComponentsInChildren<Hex>()[0];

		if (prefab != null)
		{
			GameObject go = Instantiate(prefab);
			go.transform.position = gameObject.transform.position;
			go.transform.parent = gameObject.transform;
			go.transform.rotation = Quaternion.AngleAxis(Mathf.Round(UnityEngine.Random.Range(0, 6))*60, Vector3.up);
			terrainGraphic = go;
		}
	}

    public virtual void Delete()
	{
		DestroyImmediate(terrainGraphic);
        DestroyImmediate(this);
	}

    public virtual int CheckPlacingRules(bool verbose = false)
    {
		int score = 0;

		if (CheckPlacingRulesTerrain(verbose) == 1)
		{
            score+= 2;
            hex.SetTerrainErrorMarker(false);
        } else
		{
			score -= 1;
            hex.SetTerrainErrorMarker(true);
        }

		if (token != null && creatureToken != null)
		{
            if (CheckPlacingRulesCreature() == 1)
            {
				score += 2;
				creatureToken.ErrorToggle(false);
            }
            else
            {
				score -= 1;
                creatureToken.ErrorToggle(true);
            }
        }

		return score;
    }

	public virtual int CheckPlacingRulesTerrain(bool verbose=false)
	{
        List<TerrainType> neighbourTerrains = Grid.inst.Neighbours(index).Values.ToList();
        if (placingRule.CheckRules(neighbourTerrains, verbose)) return 1;
        else return 0;
    }

	public virtual int CheckPlacingRulesCreature()
	{
        List<Enums.CreatureType> neighbourCreatures = Grid.inst.CreatureNeighbours(index).Values.ToList();
        if (token != null && token.creatureRules.CheckRules(tileType, neighbourCreatures)) return 1;
        else return 0;
    }

    #region Coordinate Conversion Functions
    public static OffsetIndex CubeToEvenFlat(CubeIndex c) {
		OffsetIndex o;
		o.row = c.x;
		o.col = c.z + (c.x + (c.x&1)) / 2;
		return o;
	}

	public static CubeIndex EvenFlatToCube(OffsetIndex o){
		CubeIndex c;
		c.x = o.col;
		c.z = o.row - (o.col + (o.col&1)) / 2;
		c.y = -c.x - c.z;
		return c;
	}

	public static OffsetIndex CubeToOddFlat(CubeIndex c) {
		OffsetIndex o;
		o.col = c.x;
		o.row = c.z + (c.x - (c.x&1)) / 2;
		return o;
	}
	
	public static CubeIndex OddFlatToCube(OffsetIndex o){
		CubeIndex c;
		c.x = o.col;
		c.z = o.row - (o.col - (o.col&1)) / 2;
		c.y = -c.x - c.z;
		return c;
	}

	public static OffsetIndex CubeToEvenPointy(CubeIndex c) {
		OffsetIndex o;
		o.row = c.z;
		o.col = c.x + (c.z + (c.z&1)) / 2;
		return o;
	}
	
	public static CubeIndex EvenPointyToCube(OffsetIndex o){
		CubeIndex c;
		c.x = o.col - (o.row + (o.row&1)) / 2;
		c.z = o.row;
		c.y = -c.x - c.z;
		return c;
	}

	public static OffsetIndex CubeToOddPointy(CubeIndex c) {
		OffsetIndex o;
		o.row = c.z;
		o.col = c.x + (c.z - (c.z&1)) / 2;
		return o;
	}
	
	public static CubeIndex OddPointyToCube(OffsetIndex o){
		CubeIndex c;
		c.x = o.col - (o.row - (o.row&1)) / 2;
		c.z = o.row;
		c.y = -c.x - c.z;
		return c;
	}
    #endregion

    #region A* Herustic Variables
    public int MoveCost { get; set; }
	public int GCost { get; set; }
	public int HCost { get; set; }
	public int FCost { get { return GCost + HCost; } }
	public Terrain Parent { get; set; }
	#endregion
}

[System.Serializable]
public struct OffsetIndex {
	public int row;
	public int col;

	public OffsetIndex(int row, int col){
		this.row = row; this.col = col;
	}
}

[System.Serializable]
public struct CubeIndex {
	public int x;
	public int y;
	public int z;

	public CubeIndex(int x, int y, int z){
		this.x = x; this.y = y; this.z = z;
	}

	public CubeIndex(int x, int z) {
		this.x = x; this.z = z; this.y = -x-z;
	}
    public CubeIndex(string s)
    {
		s = s.TrimStart('[');
		s = s.TrimEnd(']');
        string[] values = s.Split(',');
		this.x = int.Parse(values[0], System.Globalization.NumberStyles.Integer);
        this.y = int.Parse(values[1], System.Globalization.NumberStyles.Integer);
        this.z = int.Parse(values[2], System.Globalization.NumberStyles.Integer);
    }

    public static CubeIndex operator+ (CubeIndex one, CubeIndex two){
		return new CubeIndex(one.x + two.x, one.y + two.y, one.z + two.z);
	}

	public override bool Equals (object obj) {
		if(obj == null)
			return false;
		CubeIndex o = (CubeIndex)obj;
		if((System.Object)o == null)
			return false;
		return((x == o.x) && (y == o.y) && (z == o.z));
	}

	public override int GetHashCode () {
		return(x.GetHashCode() ^ (y.GetHashCode() + (int)(Mathf.Pow(2, 32) / (1 + Mathf.Sqrt(5))/2) + (x.GetHashCode() << 6) + (x.GetHashCode() >> 2)));
	}

	public override string ToString () {
		return string.Format("[" + x + "," + y + "," + z + "]");
	}
}
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Diagnostics;

public class MapObject: Locatable
{
	private string name;
	private string searchName;
	private int index;
	private Skill connectedSkill;
	private Location location;
	private Map objectMap;
	private int strength;
	private int height;
	private int remainingTime;
	private Boolean ground;
	private Boolean middle;
	private Boolean airborne;
	private Boolean benign;
	private Boolean reflect;
	private Boolean deflect;
	private Player owner;
	private Boolean visible;
	private Texture2D texture;
	private string conditions;
    private string loadedInfo;
    private Vector3 mapLocation;
    private RawImage screenObject;

	public MapObject (string nm, string sn, Skill sk, Location loc, Map mp, int str, int hgt, int rmt, string cond)
	{
		name = nm;
		searchName = sn;
		connectedSkill = sk;
		location = new Location (-1, -1);
		objectMap = mp;
		strength = str;
		height = hgt;
		remainingTime = rmt;
		setConditions (cond);
		index = -1;
		owner = null;
		visible = true;
		texture = Resources.Load <Texture2D> (@"Textures/" + searchName);
        loadedInfo = "";
		//specialConditions = cond;
	}

	public void setConditions (string cond)
	{
		conditions = cond;
		//floor = false;
		ground = false;
		middle = false;
		airborne = false;
		benign = false;
		reflect = false;
		deflect = false;
		if (conditions.Contains (@"GROUND")) {
			ground = true;
		} if (conditions.Contains (@"MIDDLE")) {
			middle = true;
		} if (conditions.Contains (@"AIRBORNE")) {
			airborne = true;
		} if (conditions.Contains (@"BENIGN")) {
			benign = true;
		} if (conditions.Contains (@"DEFLECT")) {
			deflect = true;
		} if (conditions.Contains (@"REFLECT")) {
			reflect = true;
		} if (conditions.Contains (@"INVISIBLE ")) {
			visible = false;
		}
	}

    public RawImage screenTexture()
    {
        return screenObject;
    }

    public Vector3 screenLocation()
    {
        return screenObject.transform.position;
    }


    public string LoadedInfo
    {
        get {
            return loadedInfo;
        }
        set {
            loadedInfo = value;
        }
    }

	public Texture2D mapTexture ()
	{
		return texture;
	}

	public int row ()
	{
		return currentLocation ().Row;
	}

	public int column ()
	{
		return currentLocation ().Column;
	}

	public Player Owner
	{
		get { return owner;}
		set { owner = value;}
	}

	public Boolean isVisible ()
	{
		return visible;
	}

	public string Name
	{
		get { return name;}
		set { name = value;}
	}

	public int Index
	{
		get { return index;}
		set { index = value;}
	}

	public Boolean interactsWith (Player target)
	{
		if (benign) {
			return false;
		} if (target.Grounded.IsActive && !ground) {
			return false;
		} if (((!target.Grounded.IsActive && !target.Airborne.IsActive) || target.Airborne.Potency <= 10) && !middle) {
			return false;
		} if (target.Airborne.Potency > 10 && !airborne) {
			return false;
		}
		return true;
	}

	public string SearchName
	{
		get { return searchName;}
		set { searchName = value;}
	}

	public int RemainingTime
	{
		get { return remainingTime;}
		set { remainingTime = value;}
	}

	public int Height
	{
		get { return height;}
		set { height = value;}
	}

	public int Strength
	{
		get { return strength;}
		set { strength = value;}
	}

	public int StrengthAbsolute
	{
		get { if (strength < 0) { return 10;} return Strength;}
		set { strength = value;}
	}

	public Skill ConnectedSkill
	{
		get { return connectedSkill;}
		set { connectedSkill = value;}
	}

	public ArrayList actualSpace ()
	{	return new ArrayList ();}

	public Location currentLocation ()
	{	return location;}

	public void changeLocation (Location newLocation)
	{	location = newLocation;}

	public void setCurrentMap (Map mp)
	{	objectMap = mp;}

	public Map currentMap ()
	{	return objectMap;}

	public int currentHeight ()
	{	return currentMap().heightOf(currentLocation()) + Height;}

	public bool sentient ()
	{	return false;}

	public string mapName ()
	{	return searchName;}

	public int span (Locatable otherLoc)
	{	return location.span (otherLoc.currentLocation());}

	public Boolean inLine (Location loc)
	{	return (loc.Row == currentLocation ().Row) || (loc.Column == currentLocation ().Column);}

	public Boolean bordersWith (string nm)
	{
		if (North != null && North.mapName ().Equals (nm)) {
			return true;
		}
		if (East != null && East.mapName ().Equals (nm)) {
			return true;
		}
		if (South != null && South.mapName ().Equals (nm)) {
			return true;
		}
		if (West != null && West.mapName ().Equals (nm)) {
			return true;
		}
		return false;
	}

	public Locatable North
	{
		get { return (Locatable)currentMap().objectAt(currentLocation().North);}
	}

	public Locatable NorthEast
	{
		get { return (Locatable)currentMap().objectAt(currentLocation().NorthEast);}
	}

	public Locatable East
	{
		get { return (Locatable)currentMap().objectAt(currentLocation().East);}
	}

	public Locatable SouthEast
	{
		get { return (Locatable)currentMap().objectAt(currentLocation().SouthEast);}
	}

	public Locatable South
	{
		get { return (Locatable)currentMap().objectAt(currentLocation().South);}
	}

	public Locatable SouthWest
	{
		get { return (Locatable)currentMap().objectAt(currentLocation().SouthWest);}
	}

	public Locatable West
	{
		get { return (Locatable)currentMap().objectAt(currentLocation().West);}
	}

	public Locatable NorthWest
	{
		get { return (Locatable)currentMap().objectAt(currentLocation().NorthWest);}
	}

	private int difference (int x, int y)
	{
		return Math.Abs (x - y);
	}

	public Boolean isGenNorth (Location loc)
	{
		return isNorth (loc)
			|| isNorthNorthEast(loc)
			|| isNorthNorthWest(loc)
			|| isEastNorthEast(loc)
			|| isWestNorthWest(loc);
	}

	public Boolean isGenEast (Location loc)
	{
		return isEast (loc)
			|| isEastNorthEast(loc)
			|| isEastSouthEast(loc)
			|| isNorthNorthEast(loc)
			|| isSouthSouthEast(loc);
	}

	public Boolean isGenSouth (Location loc)
	{
		return isSouth (loc)
			|| isSouthSouthEast(loc)
			|| isSouthSouthWest(loc)
			|| isEastSouthEast(loc)
			|| isWestSouthWest(loc);
	}

	public Boolean isGenWest (Location loc)
	{
		return isWest (loc)
			|| isWestNorthWest(loc)
			|| isWestSouthWest(loc)
			|| isNorthNorthWest(loc)
			|| isSouthSouthWest(loc);
	}

	public Boolean isMostlyNorth (Location loc)
	{
		return isNorth (loc)
			|| isNorthNorthEast(loc)
			|| isNorthNorthWest(loc);
	}

	public Boolean isMostlyEast (Location loc)
	{
		return isEast (loc)
			|| isEastNorthEast(loc)
			|| isEastSouthEast(loc);
	}

	public Boolean isMostlySouth (Location loc)
	{
		return isSouth (loc)
			|| isSouthSouthEast(loc)
			|| isSouthSouthWest(loc);
	}

	public Boolean isMostlyWest (Location loc)
	{
		return isWest (loc)
			|| isWestNorthWest(loc)
			|| isWestSouthWest(loc);
	}

	public Boolean isNorth (Location loc)
	{
		return (loc.Row < currentLocation ().Row)
			&& (loc.Column == currentLocation ().Column);
	}

	public Boolean isNorthNorthEast (Location loc)
	{
		return (loc.Row < currentLocation().Row)
			&& (loc.Column > currentLocation().Column)
			&& (difference(loc.Row, currentLocation().Row)
				> difference(loc.Column, currentLocation().Column));
	}

	public Boolean isNorthEast (Location loc)
	{
		return (loc.Row < currentLocation ().Row)
			&& (loc.Column == currentLocation ().Column);
	}

	public Boolean isEastNorthEast (Location loc)
	{
		return (loc.Row < currentLocation().Row)
			&& (loc.Column > currentLocation().Column)
			&& (difference(loc.Row, currentLocation().Row)
				< difference(loc.Column, currentLocation().Column));
	}

	public Boolean isEast (Location loc)
	{
		return (loc.Row == currentLocation ().Row)
			&& (loc.Column > currentLocation ().Column);
	}

	public Boolean isEastSouthEast (Location loc)
	{
		return (loc.Row > currentLocation().Row)
			&& (loc.Column > currentLocation().Column)
			&& (difference(loc.Row, currentLocation().Row)
				< difference(loc.Column, currentLocation().Column));
	}

	public Boolean isSouthEast (Location loc)
	{
		return (loc.Row < currentLocation ().Row)
			&& (loc.Column == currentLocation ().Column);
	}

	public Boolean isSouthSouthEast (Location loc)
	{
		return (loc.Row < currentLocation ().Row)
			&& (loc.Column == currentLocation ().Column);
	}

	public Boolean isSouth (Location loc)
	{
		return (loc.Row > currentLocation ().Row)
			&& (loc.Column == currentLocation ().Column);
	}

	public Boolean isSouthSouthWest (Location loc)
	{
		return (loc.Row < currentLocation ().Row)
			&& (loc.Column == currentLocation ().Column);
	}

	public Boolean isSouthWest (Location loc)
	{
		return (loc.Row < currentLocation ().Row)
			&& (loc.Column == currentLocation ().Column);
	}

	public Boolean isWestSouthWest (Location loc)
	{
		return (loc.Row > currentLocation().Row)
			&& (loc.Column < currentLocation().Column)
			&& (difference(loc.Row, currentLocation().Row)
				< difference(loc.Column, currentLocation().Column));
	}

	public Boolean isWest (Location loc)
	{
		return (loc.Row == currentLocation ().Row)
			&& (loc.Column < currentLocation ().Column);
	}

	public Boolean isWestNorthWest (Location loc)
	{
		return (loc.Row < currentLocation().Row)
			&& (loc.Column < currentLocation().Column)
			&& (difference(loc.Row, currentLocation().Row)
				< difference(loc.Column, currentLocation().Column));
	}

	public Boolean isNorthWest (Location loc)
	{
		return (loc.Row < currentLocation ().Row)
			&& (loc.Column == currentLocation ().Column);
	}

	public Boolean isNorthNorthWest (Location loc)
	{
		return (loc.Row < currentLocation().Row)
			&& (loc.Column < currentLocation().Column)
			&& (difference(loc.Row, currentLocation().Row)
				> difference(loc.Column, currentLocation().Column));
	}

	public override bool Equals (object obj)
	{
		if (obj == null || !obj.GetType().Name.Equals(@"MapObject")) {
			return false;
		}

		MapObject rhs = (MapObject)obj;
		if (searchName.Equals (rhs.SearchName)) {
			return index.Equals (rhs.Index);
		}
		return true;
	}

	public Location closestLocationFrom (Location loc, Boolean diagonals) {
		Location l = loc;
		Location[] locs = new Location[4];
		locs [0] = loc.North;
		locs [1] = loc.East;
		locs [2] = loc.South;
		locs [3] = loc.West;
		int spn = loc.span (currentLocation ());

		for (int i = 0; i < locs.Length; i++) {
			if (locs [i].span(currentLocation ()) < spn) {
				spn = locs [i].span (currentLocation ());
				l = locs [i];
			}
		}
		return loc;
	}

	public Boolean Reflect
	{
		get {
			return reflect;
		}
		set {
			reflect = value;
		}
	}

	public Boolean Deflect
	{
		get {
			return deflect;
		}
		set {
			deflect = value;
		}
	}

	public string DirectionOf(Location loc)
	{
		return currentLocation().DirectionOf(loc);
	}
	
	public string DirectionOf(Locatable loc)
	{
		return currentLocation().DirectionOf(loc);
	}

	public override string ToString ()
	{
		string output = @"";

		output = name + '\n' + searchName + '\n';
		if (connectedSkill == null) {
			output += @"null" + '\n';
		}
        output += Strength + @"" + '\n' + Height + @"" + '\n' +
            RemainingTime + @"" + '\n' + conditions + @"" + '\n';
        if (owner != null) {
            output += owner.SearchName;
        } else {
            output += @"null";
        }
		return output + '\n';
	}

}





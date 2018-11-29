using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Diagnostics;

public class Map
{
	private string name;
	private string searchName;
	private string info;
	private string mode;
	private ArrayList objects;
	private ArrayList roster;
	private ArrayList interactables;
	private ArrayList projectiles;
	private Locatable[][] mapGrid;
	private Skill[][] interactableGrid;
	private Skill[][] projectileGrid;
	private Skill[][] benignGrid;
	private int[][] heightMap;
    private ArrayList startingPoints1;
    private ArrayList startingPoints2;
	private Player mapOwner;
	private State[] traits;
	private Boolean ambience;
	private Boolean mapFall;
	private string music;
	private string stageMusic;
	public Boolean dark;
	public Boolean light;
	public Boolean foggy;
	public Boolean rainy;
	public string primaryTextColor;
	public ArrayList currentLocations;
	public ArrayList offLimitsLocations;
	private int rounds;
	private int playedRounds;
	private int index;
	private int time;
	private ArrayList trapsToAdd;
	private ArrayList trapsAdded;
	private ArrayList locsAdded;
	private ArrayList skills;
    private float activeTime;

	public Location startingPoint1;
	public Location startingPoint2;

    public Team team1;
    public Team team2;
    public int stageEffectLoops;
    public float stageEffectSpeed;


	public Map (string nm, string sn, string inf, string stMus, int rows, int columns, string[] trait)
	{
        stageEffectLoops = -1;
        stageEffectSpeed = -1;
		trapsToAdd = new ArrayList ();
		trapsAdded = new ArrayList ();
		locsAdded = new ArrayList ();
		offLimitsLocations = new ArrayList ();

        startingPoints1 = new ArrayList();
        startingPoints2 = new ArrayList();

		mode = @"";
		skills = new ArrayList ();
		//round = 0;
		ambience = false;
		name = nm;
		searchName = sn;
		info = inf;
		stageMusic = stMus;
		mapGrid = new Locatable[rows][];
		for (int i = 0; i < mapGrid.Length; i++) {
			mapGrid [i] = new Locatable[columns];
		}
		interactableGrid = new Skill[rows][];
		for (int i = 0; i < interactableGrid.Length; i++) {
			interactableGrid [i] = new Skill[columns];
		}
		projectileGrid = new Skill[rows][];
		for (int i = 0; i < projectileGrid.Length; i ++) {
			projectileGrid [i] = new Skill [columns];
		}
		benignGrid = new Skill[rows][];
		for (int i = 0; i < benignGrid.Length; i++) { 
			benignGrid [i] = new Skill [columns];
		}
		heightMap = new int[rows][];
		for (int i = 0; i < heightMap.Length; i++) {
			heightMap[i] = new int[columns];
		}
		objects = new ArrayList ();
		roster = new ArrayList ();
		interactables = new ArrayList ();
		projectiles = new ArrayList ();

		startingPoint1 = new Location ((1) + 3, columns / 2);
		startingPoint2 = new Location ((rows - 2) - 3, columns / 2);

        mapOwner = new DataReader ().loadPlayer (searchName, @"Map", true, false, false);
		mapOwner.MyTeam = new Team (@"Team 0", @"0", 1);
		mapOwner.changeLocation (new Location (-1, -1));
		mapOwner.CurrentMap = this;

		traits = new State[trait.Length];
		setTraits (trait);
		music = @"";

		rounds = 0;
		playedRounds = 0;
		index = 0;
		Time = 0;
        activeTime = 0.0f;

        team1 = null;
        team2 = null;

        startingPoints1.Add (StartingPoint1);
        startingPoints1.Add (StartingPoint1B);
        startingPoints1.Add (StartingPoint1C);
        startingPoints1.Add (StartingPoint1D);

        startingPoints2.Add (StartingPoint2);
        startingPoints2.Add (StartingPoint2B);
        startingPoints2.Add (StartingPoint2C);
        startingPoints2.Add (StartingPoint2D);
        //string ths = this.ToString();

	}

    public ArrayList StartingPoints1
    {
        get {
            return startingPoints1;
        }
        set {
            startingPoints1 = value;
        }
    }

    public ArrayList StartingPoints2
    {
        get
        {
            return startingPoints2;
        }
        set
        {
            startingPoints2 = value;
        }
    }

    public Team Team1
    {
        get {
            return team1;
        }
        set {
            team1 = value;
        }
    }

    public Team Team2
    {
        get
        {
            return team2;
        }
        set
        {
            team2 = value;
        }
    }

    public float ActiveTime
    {
        get {
            return activeTime;
        }
        set {
            activeTime = value;
        }
        
    }

	public ArrayList Skills
	{
		get
		{
			return skills;
		}
		set
		{
			skills = value;
		}
	}

	public int Rounds
	{
		get {
			return rounds;
		}
		set {
			rounds = value;
		}
	}

	public int PlayedRounds
	{
		get {
			return playedRounds;
		}
		set {
			playedRounds = value;
		}	
	}

	public int Time
	{
		get {
			return time;
		}
		set {
			time = value;
		}
	}

	public Player RandomUnit
	{
		get {
			Player p = null;
			while (p == null || p.KOd) {
				p = (Player)Roster [new System.Random ().Next (Roster.Count)];
			}

			return p;
		}
	}

	public int Index
	{
		get {
			return index;
		}
		set {
			index = value;
			if (value == 0) {
				Rounds++;
				Sort ();
				resetTurns ();
			}
		}
	}

	public Skill getSkill (string type)
	{
		return MapOwner.getSkill (type);
	}

	public void Sort ()
	{
		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 12; j++) {
				Roster.Sort ();
			}
		}
        /**
		for (int i = 0; i < Roster.Count; i++) {
			print (@"#" + (i + 1) + @": " + ((Player)Roster [i]).FirstName);
		}
		*/
	}


	private ArrayList OffLimitsLocations
	{
		get {
			return offLimitsLocations;
		}
		set {
			offLimitsLocations = value;
		}
	}

	private void setTraits (string[] traitSet)
	{
		dark = false;
		light = true;
		foggy = false;
		rainy = false;
		ambience = false;
		for (int i = 0; i < traitSet.Length; i ++)
		{
			setTrait (i, traitSet[i]);
			//name = traitSet[i].Substring(0, traitSet[i].IndexOf('.') - 1);

		}

	}

	public ArrayList TrapsToAdd
	{
		get {
			return trapsToAdd;
		}
		set {
			trapsToAdd = value;
		}
	}

	public ArrayList TrapsAdded
	{
		get {
			return trapsAdded;
		}
		set {
			trapsAdded = value;
		}
	}

	public ArrayList LocsAdded
	{
		get {
			return locsAdded;
		}
		set {
			locsAdded = value;
		}
	}

	public void setTraps ()
	{
		Location loc = null;
		string locTxt;
		Skill s;

		for (int i = 0; i < trapsToAdd.Count; i++) {

			while (loc == null || !isValid (loc) || !isEmpty (loc) || !isEmptyInteractable (loc)) {
				loc = new Location (new System.Random ().Next (Rows), new System.Random ().Next (Columns));
			}

			s = (Skill)trapsToAdd [i];

			if (s.Owner != MapOwner) {
				s.Owner = MapOwner;
			}

			addInteractable (s, loc);
		}

		for (int i = 0; i < trapsAdded.Count; i++) {
			s = (Skill)trapsAdded [i];
			loc = (Location)locsAdded [i];

			if (s.Owner != MapOwner) {
				s.Owner = MapOwner;
			}

			addInteractable (s, loc);
		}
	}

    public int StageEffectLoops
    {
        get { return stageEffectLoops; }
        set { stageEffectLoops = value; }
    }

    public float StageEffectSpeed
    {
        get { return stageEffectSpeed; }
        set { stageEffectSpeed = value; }
    }

    private void setTrait (int index, string trait)
	{
		string effect = @"";
		string specialQuals = @"";
		string type = @"";
		int potency = 1;
		double dPotency = 0;
		int numRounds = 0;
		double probability = 1;
		Skill s;
		Location loc = null;

        if (trait.StartsWith (@"STAGELOOP")) {
            stageEffectLoops = NumberConverter.ConvertToInt (trait.Substring(trait.IndexOf('P') + 1, 5));
        } if (trait.StartsWith (@"STAGESPEED")) {
            stageEffectSpeed = (float)NumberConverter.ConvertToInt(trait.Substring(trait.IndexOf('P') + 1, 5));
        }
        if (trait.Contains (@".")) {
			effect = trait.Substring (0, trait.IndexOf ('.'));
			s = new DataReader().loadSkill (MapOwner, effect, null);
			MapOwner.learn (s, false, -1);
			s.Owner = MapOwner;
		} if (trait.Contains(@"STEP:")) {
			type = "STEP";
			numRounds = NumberConverter.ConvertToInt (trait.Substring(trait.IndexOf (':') + 1, 5));
		} if (trait.Contains(@"INDEX:")) {
			type = "INDEX";
			numRounds = NumberConverter.ConvertToInt (trait.Substring(trait.IndexOf (':') + 1, 5));
		} if (trait.Contains(@"ROUND:")) {
			type = "ROUND";
			numRounds = NumberConverter.ConvertToInt (trait.Substring(trait.IndexOf (':') + 1, 5));
		} if (trait.Contains(@"ROUNDPLAY:")) {
			type = "ROUNDPLAY";
			numRounds = NumberConverter.ConvertToInt (trait.Substring(trait.IndexOf (':') + 1, 5));
		} if (trait.Contains(@"TIME:")) {
			type = "TIME";
			numRounds = NumberConverter.ConvertToInt (trait.Substring(trait.IndexOf (':') + 1, 5));
		} if (trait.Contains (@" END ")) {
			type = "END";
		} if (trait.Contains (@" BEGINNING ")) {
			type = "BEGINNING";
		} if (trait.Contains (@"CHANCE")) {
			probability = Convert.ToDouble (trait.Substring(trait.IndexOf('@') + 1, 5));
			if (probability == 0) {
				throw new NullReferenceException (trait.Substring(trait.IndexOf('@') + 1, 5));
			}
		} if (trait.Contains (@"RAND")) {
			potency = NumberConverter.ConvertToInt (trait.Substring(trait.IndexOf('|') + 1, 5));
		} if (trait.Contains (@"POW")) {
			potency = NumberConverter.ConvertToInt (trait.Substring(trait.IndexOf('|') + 1, 5));
		} if (trait.Contains (@"+")) {
			specialQuals += trait.Substring (trait.IndexOf(@"+") + 1);
		} if (trait.Contains (@"TRAP")) {

			s = new DataReader ().loadSkill (MapOwner, trait.Substring(trait.IndexOf(':') + 1), null);
			trapsToAdd.Add (s);

		} if (trait.Contains (@"AMBIENCE")) {
			ambience = true;
		} if (trait.Contains (@"MAPFALL")) {
			mapFall = true;
		} if (trait.Contains (@"DARK")) {
			dark = true;
		} if (trait.Contains (@"LIGHT")) {
			light = true;
		} if (trait.Contains (@"FOG")) {
			foggy = true;
		} if (trait.Contains (@"RAIN")) {
			rainy = true;
		} if (trait.Contains (@"TEXT")) {
			primaryTextColor = trait.Substring (trait.IndexOf (':') + 1);
		} if (trait.Contains (@"START1")) {
			startingPoint1 = new Location (NumberConverter.ConvertToInt (trait.Substring (trait.IndexOf (':') + 1, 3)),
				NumberConverter.ConvertToInt (trait.Substring (trait.IndexOf (':') + 5, 3)));
		} if (trait.Contains (@"START2")) {
			startingPoint2 = new Location (NumberConverter.ConvertToInt (trait.Substring (trait.IndexOf (':') + 1, 3)),
				NumberConverter.ConvertToInt (trait.Substring (trait.IndexOf (':') + 5, 3)));
		} if (trait.Contains (@"BOUND")) {
			if (trait.Contains (@"AREA")) {
				int leftBoundary = NumberConverter.ConvertToInt (trait.Substring (trait.IndexOf (':') + 1, 3)),
				rightBoundary = NumberConverter.ConvertToInt (trait.Substring (trait.IndexOf (':') + 5, 3)),
				upBoundary = NumberConverter.ConvertToInt (trait.Substring (trait.IndexOf (':') + 9, 3)),
				downBoundary = NumberConverter.ConvertToInt (trait.Substring (trait.IndexOf (':') + 13, 3));

				for (int i = 0; i < Rows; i++) {
					for (int j = 0; j < Columns; j++) {
						if (i >= upBoundary && i <= downBoundary) {
							if (j >= leftBoundary && j <= rightBoundary) {
								OffLimitsLocations.Add (new Location (i, j));
							}
						}
					}
				}
			} else {
				int inc = 1;
				inc = NumberConverter.ConvertToInt (trait.Substring (trait.IndexOf (':') + 1, 3));
				for (int i = 0; i < Rows; i++) { 
					for (int j = 0; j < Columns; j++) {
						if (trait.Contains (@"LEFT") && j <= inc) {
							OffLimitsLocations.Add (new Location (i, j));
						}
						if (trait.Contains (@"RIGHT") && j >= inc) {
							OffLimitsLocations.Add (new Location (i, j));
						}
						if (trait.Contains (@"UP") && i <= inc) {
							OffLimitsLocations.Add (new Location (i, j));
						}
						if (trait.Contains (@"DOWN") && i >= inc) {
							OffLimitsLocations.Add (new Location (i, j));
						}
					}
				}
			}
		}
		traits [index] = new State (effect, type, potency, dPotency, numRounds, probability, true, specialQuals);
	}

	public Location StartingPoint1
	{
		get {
			return startingPoint1;
		}
		set {
			startingPoint1 = value;
		}
	}

	public Location StartingPoint1B
	{
		get {
			return new Location (startingPoint1.Row - 1, startingPoint1.Column - 1);
		}
	}

	public Location StartingPoint1C
	{
		get {
			return new Location (startingPoint1.Row - 1, startingPoint1.Column + 1);
		}
	}

	public Location StartingPoint1D
	{
		get {
			return new Location (startingPoint1.Row - 1, startingPoint1.Column);
		}
	}

	public Location StartingPoint2
	{
		get {
			return startingPoint2;
		}
		set {
			startingPoint2 = value;
		}
	}

	public Location StartingPoint2B
	{
		get {
			return new Location (startingPoint2.Row + 1, startingPoint2.Column - 1);
		}
	}

	public Location StartingPoint2C
	{
		get {
			return new Location (startingPoint2.Row + 1, startingPoint2.Column + 1);
		}
	}

	public Location StartingPoint2D
	{
		get {
			return new Location (startingPoint2.Row + 1, startingPoint2.Column);
		}
	}


	public Boolean Dark
	{
		get {
			return dark;
		}
		set {
			dark = value;
		}
	}

	public Boolean Light
	{
		get {
			return light;
		}
		set {
			light = value;
		}
	}


	public string PrimaryTextColor
	{
		get {
			return primaryTextColor;
		}
		set {
			primaryTextColor = value;
		}
	}

	public string Mode
	{
		get
		{
			return mode;
		}
		set
		{
			mode = value;
		}	
	}

	public Boolean traitsContain (string key)
	{
		for (int i = 0; i < Traits.Length; i++) {
			if (Traits[i].Name.Equals (key)) {
				return true;
			}
		}
		return false;
	}

	public Boolean traitPropertiesContain (string key)
	{
		for (int i = 0; i < Traits.Length; i++)
		{
			if (Traits[i].Abbreviation.Contains (key))
			{
				return true;
			}
		}
	return false;	
	}

	public State getTrait (string key)
	{
		for (int i = 0; i < Traits.Length; i++) {
			if (Traits[i].Name.Equals (key)) {
				return (State)Traits [i];
			}
		}
		return null;
	}

	public string Music
	{
		get {
			return music;
		}
		set {
			music = value;
		}
	}

	public string StageMusic
	{
		get {
			return stageMusic;
		}
		set {
			stageMusic = value;
		}
	}

	public Boolean Ambience
	{
		get {
			return ambience;
		}
		set {
			ambience = value;
		}
	}

	public Boolean MapFall
	{
		get {
			return mapFall;
		}
		set {
			mapFall = value;
		}
	}

	public State[] Traits
	{
		get { return traits;}
		set { traits = value;}
	}

	public Player MapOwner
	{
		get {
			return mapOwner;
		}
		set {
			mapOwner = value;
			mapOwner.CurrentMap = this;
			//MapOwner.CurrentLocation = new Location (Rows/ 2, Columns / 2);
			mapOwner.changeLocation(new Location(Rows/ 2, Columns / 2));
		}
	}

	public int heightDifference (Location loc1, Location loc2)
	{
		return heightOf (loc1) - heightOf (loc2);
	}

	public int Rows
	{
		get {
			return mapGrid.Length;
		}
	}

	public int Columns
	{
		get {
			return mapGrid[0].Length;
		}
	}


	public Locatable[] this [int i] {
		get { return mapGrid [i]; }
		set { mapGrid [i] = value; }
	}

    public Locatable[][] MapGrid
    {
        get { return mapGrid; }
        set { mapGrid = value; }
    }

	public int[][] HeightMap
	{
		get { return heightMap;}
		set { heightMap = value;}
	}

	public string Name {
		get { return name; }
		set { name = value; }
	}

	public string SearchName {
		get { return searchName; }
		set { searchName = value; }
	}

	public string Info {
		get { return info;}
		set { info = value;}
	}

	public ArrayList Objects {
		get { return objects; }
		set { objects = value; }
	}

	public ArrayList Roster {
		get { return roster; }
		set { roster = value; }
	}

	public ArrayList Interactables {
		get { return interactables;}
		set { interactables = value;}
	}

	public ArrayList Projectiles {
		get { return projectiles;}
		set { projectiles = value;}
	}

	public Skill[][] InteractableGrid
	{
		get { return interactableGrid;}
		set { interactableGrid = value;}
	}

	public Skill[][] ProjectileGrid
	{
		get { return projectileGrid;}
		set { projectileGrid = value;}
	}

	public Skill[][] BenignGrid
	{
		get { return benignGrid;}
		set { benignGrid = value;}
	}

	public Boolean isValid (Location loc)
	{
		return !OffLimitsLocations.Contains (loc) && loc != null && (((loc.Row >= 0) && (loc.Row < MapGrid.Length))
			&& ((loc.Column >= 0) && (loc.Column < MapGrid [0].Length)));
	}

	public Boolean isEmpty (Location loc)
	{
		return MapGrid [loc.Row] [loc.Column] == null;
	}

	public Boolean canMakeMove (Player p, Location loc)
	{
		return isValid(loc) && (isEmpty (loc) || (playerAt (loc) != null && playerAt (loc).sameTeam (p) && playerAt (loc).IsActive))
			&& (p.Flight || heightMap[p.currentLocation().Row][p.currentLocation().Column] - heightMap[loc.Row][loc.Column] >= -1);
	}

	public Boolean locationInHeightRange (Location loc, Skill s)
	{
		if (!isValid (loc)) {
			return false;
		}
		if (s.MasterSkill != null && s.MasterSkill.Properties.Contains (@"AUTOLINK ") && s.Auto) {
			return true;
		}

		if (s.Properties.Contains (@"NEXTTO")) {
			return s.Owner.currentLocation ().bordersWith (loc) && Math.Abs (heightOf (s.Owner.currentLocation ()) - heightOf (loc)) <= 2;
		}

		if (s.Properties.Contains (@"ALLHEIGHT ")) {
			return true;
		}

		if (s.Properties.Contains (@"ALLBELOW")) {
			if (isEmpty (loc) && heightOf (loc) <= heightOf (s.Owner.currentLocation ()) + s.Owner.Airborne.Potency) {
				return true;
			} else if (playerAt (loc) != null && heightOf (loc) + playerAt (loc).Airborne.Potency <= heightOf (s.Owner.currentLocation ()) + s.Owner.Airborne.Potency) {
				return true;
			}
		}
			
		if (s.Properties.Contains (@"MAP ")) {
			if (!s.Properties.Contains (@"LEVELDIFF") && !s.Properties.Contains(@"LEAP") && s.Properties.Contains(@"JUMP")) {
				int difference = Math.Abs ((s.Owner.Airborne.Potency + heightOf (s.Owner.currentLocation ()))//s.CurrentHeight 
					- heightOf (loc));
				if (difference <= (s.Range + s.Threshold)) {
					return true;
				}
				return false;
			}
			return true;
		} 

		if (s.Properties.Contains (@"LEVELDIFF")) {
			return Math.Abs ((s.Owner.Airborne.Potency + heightOf (s.Owner.currentLocation ()) - heightOf (loc))) <= s.Range;
		}

		if (s.Properties.Contains (@"GRADUALHEIGHT")) {
			Location lc = s.Owner.currentLocation ();
			int hDiff = 0;
			while (!lc.Equals (loc)) {

				lc = lc.closest (this, loc);
				hDiff = lc.realSpan (loc);

				if (hDiff > 1 || hDiff < -1) {
					return false;
				}
			}
			return true;
		}



		if (s.Physical || s.Weapon|| s.Grapple) {
			return (s.Properties.Contains (@"FOLLOW ")
				|| Math.Abs ((heightOf (s.Owner.currentLocation ()) + s.Owner.Airborne.Potency) - heightOf (loc)) <= Math.Abs (s.Range + s.Threshold))
				|| (!s.Owner.Airborne.IsActive && (s.Properties.Contains (@"HIGH ") || s.Properties.Contains (@"AIR ")));
		}

			if (s.SForce || s.Projectile || s.SuperProjectile || s.Weapon) {
				return Math.Abs ((s.Owner.Airborne.Potency + heightOf (s.Owner.currentLocation ())) - 
					heightOf (loc)) <= s.Range;
			}

			return (s.SelfSkill
				|| (s.Properties.Contains (@"LAND") || s.Properties.Contains (@"DASH "))
				|| (s.Properties.Contains (@"INAIR") && playerAt (loc) != null && (!playerAt (loc).Grounded.IsActive || s.OTG))
				|| s.Properties.Contains(@"ALLHEIGHT")
				|| s.Properties.Contains(@"TELEPORT")
				|| (s.Properties.Contains (@"GRADUALHEIGHT ")))
				//|| (playerAt (loc) != null && Math.Abs((heightOf (loc) + playerAt (loc).Airborne.Potency) - (heightOf (s.Owner.currentLocation ()) + s.Owner.Airborne.Potency)) <= (loc.span (s.Owner.currentLocation ())))
				//|| ((heightOf (loc) - (heightOf (s.Owner.currentLocation ()) + s.Owner.Airborne.Potency)) <= (1));
				|| Math.Abs ((s.Owner.Airborne.Potency + heightOf (s.Owner.currentLocation ()) - heightOf (loc))) <= s.Range;
	}

	public int indexOfPlayer (Player p)
	{
		for (int i = 0; i < Roster.Count; i++) {
			if (((Player)Roster [i]).Equals (p)) {
				return i;
			}
		}
		return -1;

	}

	public string oppositeDirection (string dir) {
		if (dir.Equals (@"N")) {
			return @"S";
		}
		if (dir.Equals (@"NE")) {
			return @"SW";
		}
		if (dir.Equals (@"E")) {
			return @"W";
		}
		if (dir.Equals (@"SE")) {
			return @"NW";
		}
		if (dir.Equals (@"S")) {
			return @"N";
		}
		if (dir.Equals (@"SW")) {
			return @"NE";
		}
		if (dir.Equals (@"W")) {
			return @"E";
		}
		if (dir.Equals (@"NW")) {
			return @"SE";
		}
		return @"X";
	}

	public int heightOf (Location loc)
	{
		if (isValid (loc)) {
			return HeightMap [loc.Row] [loc.Column];
		} return 0;
	}

	public Boolean canMakeMove (Player p, Location loc, Skill s)
	{
		if (!s.LineSkill && !s.Properties.Contains (@"MAP ")) {
			return s.Properties.Contains (@"TELEPORT") || (heightOf(p.currentLocation()) - heightOf(loc) >= -1)
				|| s.Properties.Contains (@"LEVELDIFF") || (s.Properties.Contains (@"JUMP")
					&& heightOf(p.currentLocation()) - heightOf(loc) >= (-1 * p.JumpHeight))
				|| (s.Properties.Contains (@"LEAP")
					&& heightOf(p.currentLocation()) - heightOf(loc) >= (s.Range * -1));
		} return s.Properties.Contains (@"TELEPORT")
			|| s.Properties.Contains(@"EVERYLEVEL")
			|| s.Properties.Contains(@"ALLHEIGHT")
			|| (s.Properties.Contains (@"LEAP") && heightOf(p.currentLocation()) - heightOf(loc) >= (s.Range * -2))
			|| (heightOf(p.currentLocation()) - heightOf(loc) >= -1);
	}

	public Boolean canBePushed (Player p, Location loc, Skill s)
	{
		return (heightOf (p.currentLocation ()) + 1 >= heightOf (loc))
			|| heightOf(p.currentLocation()) + p.Airborne.Potency >= heightOf(loc);
	}

	public Boolean lineSkillCanReach (Skill s, Location loc)
	{
		if (s.Properties.Contains (@"MAP ") || s.Properties.Contains (@"ALLHEIGHT ") || s.Properties.Contains (@"EVERYLEVEL ") || s.Properties.Contains (@"LEVELDIFF ") || s.Auto || s.Individual) {
			return true;
		}


		if (s.LineSkill) {

			if (s.Grapple) {
				if (playerAt (loc) != null) {
					return Math.Abs ((heightOf (s.Owner.currentLocation ()) + s.Owner.Airborne.Potency) - (heightOf (loc) + playerAt (loc).Airborne.Potency)) <= 2;
				} else {
					if (s.SForce || s.Projectile || s.SuperProjectile) {
						return Math.Abs ((s.Owner.Airborne.Potency + heightOf (s.Owner.currentLocation ())) - 
							heightOf (loc)) <= s.Range;
					}

					//return Math.Abs ((heightOf (s.Owner.currentLocation ()) + s.Owner.Airborne.Potency) - heightOf (loc)) <= 1;
				}
			}

			if (s.Physical) {
				return (Math.Abs ((heightOf (s.Owner.currentLocation ()) + s.Owner.Airborne.Potency) - heightOf (loc)) <= 3);
			}

			if (s.Magic) {
				return Math.Abs ((heightOf (s.Owner.currentLocation ()) + s.Owner.Airborne.Potency) - heightOf (loc)) <= 4;
			}

			if (!s.Properties.Contains (@"EVERYLEVEL") && !s.Properties.Contains (@"ALLHEIGHT")) {
				if (!s.Properties.Contains (@"LEVELDIFF")) {
					return s.Owner.Airborne.Potency - heightOf (loc) >= -1;
				}
			}
		}
		return true;
	}

	public Boolean isEmptyInteractable (Location loc)
	{
		return InteractableGrid [loc.Row] [loc.Column] == null;
	}

	public Boolean isEmptyProjectile (Location loc)
	{
		return ProjectileGrid [loc.Row] [loc.Column] == null;
	}

	public Boolean isEmptyBenign (Location loc)
	{
		return BenignGrid [loc.Row] [loc.Column] == null;
	}

	public Locatable objectAt (Location loc)
	{
		if (isValid (loc)) {
			return MapGrid [loc.Row] [loc.Column];
		}
		return null;
	}

	public Player playerAt (Location loc)
	{
		if (isValid (loc) && !isEmpty (loc) && objectAt (loc).sentient ()) {
			return (Player)MapGrid [loc.Row] [loc.Column];
		}
		return null;
	}

	public Skill interactableAt (Location loc)
	{
		if (isValid (loc)) {
			return InteractableGrid[loc.Row][loc.Column];
		}
		return null;
	}

	public Skill projectileAt (Location loc) {
		if (isValid (loc)) {
			return ProjectileGrid[loc.Row][loc.Column];
		}
		return null;
	}

	public Skill benignAt (Location loc) {
		if (isValid (loc)) {
			return BenignGrid [loc.Row][loc.Column];
		}
		return null;
	}

	public string objectRep (Location loc)
	{
		if (MapGrid [loc.Row] [loc.Column] != null) {
			return MapGrid [loc.Row] [loc.Column].mapName ();
		}
		return @"---";
	}

	public void resetTurns ()
	{
		
		for (int i = 0; i < Roster.Count; i++) {
			((Player)Roster [i]).HasActedInRound = false;
		}
	}

	public string addPlayer (Player p)
	{
		string output = @"";
		output += string.Format (@"Adding player " + p.Name + '\n');
		if (isValid (p.currentLocation ()) && isEmpty (p.currentLocation ())) {
			Objects.Add (p);
			Roster.Add (p);
			p.setCurrentMap (this);
            if (this == null) {
                //throw new NullReferenceException (@"UMMMM WHAT THE FUCK, HOW THE FUCK CAN THIS BE NULL IF I'M"
                //                                  + @" REFERENCING IT? " + Name);
            }
            if (p.CurrentMap == null) {
                //throw new NullReferenceException (@"Null " + p.FirstName);
            }
			MapGrid [p.currentLocation ().Row] [p.currentLocation ().Column] = p;

			if (p.currentLocation () != null) {
				output += string.Format (@"{0} is set in {1}." + '\n', p.Name, p.currentLocation ());
			} else {
				output += string.Format (@"" + p.currentLocation () + @" is null");
			}
		} else {
			output += string.Format (@"{0} cannot be set in {1},", p.Name, p.currentLocation ());
			if (!isValid (p.currentLocation ())) {
				output += string.Format (@" invalid." + '\n');
			} else if (!isEmpty (p.currentLocation ())) {
				output += string.Format (@"{0} in location." + '\n', objectAt(p.currentLocation()));
			}
		}
		return output;
	}

	public string addObject (Locatable obj)
	{
		string output = @"";

		if (obj.sentient ()) {
			return addPlayer ((Player)obj);
		}

		output += string.Format (@"Adding" + obj.mapName() + '\n');
		if (isValid (obj.currentLocation ()) && isEmpty (obj.currentLocation ())) {
			objects.Add (obj);
			MapGrid [obj.currentLocation ().Row] [obj.currentLocation ().Column] = obj;

			if (obj.currentLocation () != null) {
				output += string.Format (@"{0} is set in {1}." + '\n', obj.mapName (), obj.currentLocation ());
			} else {
				output += string.Format (@"" + obj.currentLocation () + @" is null");
			}
			obj.setCurrentMap (this);
		} else {
			output += string.Format (@"{0} cannot be set in {1},", obj.mapName(), obj.currentLocation ());
			if (!isValid (obj.currentLocation ())) {
				output += string.Format (@" invalid." + '\n');
			} else if (!isEmpty (obj.currentLocation ())) {
				output += string.Format (@"{0} in location." + '\n', objectAt(obj.currentLocation()));
			}
		}
		return output;
	}

	public Boolean addInteractable (Skill s, Location loc)
	{
		if (s.Properties.Contains (@"DIRECTION")) {
			return addProjectile (s, loc);
		}
		if (!s.Benign) {
			if (isValid (loc) && isEmptyInteractable (loc) && (isEmptyProjectile (loc) || !projectileAt (loc).sameHitSections (s))) {

				if (s.Properties.Contains (@"NEUTRAL ")) {
					s.Owner = MapOwner;
					MapOwner.learn (s, false, -1);
				}
		
				if (s.Properties.Contains (@"DIRECTION")) {
					Projectiles.Add (s);
				}

				s.setLocations ();
				interactables.Add (s);
				s.CurrentLocation = loc;
				s.CurrentHeight = HeightMap [loc.Row] [loc.Column];
				InteractableGrid [loc.Row] [loc.Column] = s;

				return true;
			}
			return false;
		}

		return addBenign (s, loc);
	}

	public Boolean addProjectile (Skill s, Location loc)
	{
		if (isValid(loc) && isEmptyProjectile (loc) && (isEmptyInteractable (loc) || !interactableAt (loc).sameHitSections (s))) {

			if (s.Properties.Contains (@"NEUTRAL ")) {
				s.Owner = MapOwner;
				MapOwner.learn (s, false, -1);
			}

			s.setLocations ();
			projectiles.Add (s);
			interactables.Add (s);
			s.CurrentLocation = loc;
			s.CurrentHeight = HeightMap[loc.Row][loc.Column];
			ProjectileGrid[loc.Row][loc.Column] = s;

			return true;
		}
		return false;
	}


	public Boolean addBenign (Skill s, Location loc)
	{
		if (isValid (loc) && isEmptyBenign (loc)) {

			if (s.Properties.Contains (@"NEUTRAL ")) {
				s.Owner = MapOwner;
			}
			s.setLocations ();
			interactables.Add (s);
			s.CurrentLocation = loc;
			s.CurrentHeight = HeightMap[loc.Row][loc.Column];
			BenignGrid [loc.Row] [loc.Column] = s;

			return true;
		}
		return false;
	}

	//...I HAVE TO CLEAR
	public void clear ()
	{
		int objectsLength;
        for (int i = 0; i < roster.Count; i++) {
            objectsLength = roster.Count;
            removeObject ((Locatable)roster[i]);
            if (objectsLength > roster.Count)
            {
                objectsLength--;
                objectsLength = roster.Count;
            }
        } for (int i = 0; i < objects.Count; i ++) {
			objectsLength = objects.Count;
			removeObject ((Locatable)objects [i]);
			if (objectsLength > objects.Count) {
				objectsLength --;
				objectsLength = objects.Count;
			}
		} for (int i = 0; i < interactables.Count; i ++) {
			objectsLength = interactables.Count;
			removeInteractable ((Skill)interactables[i]);
			if (objectsLength > interactables.Count) {
				objectsLength --;
				objectsLength = interactables.Count;
			}
        } for (int i = 0; i < projectiles.Count; i++) {
            objectsLength = projectiles.Count;
            removeProjectile ((Skill)projectiles[i]);
            if (objectsLength > projectiles.Count)
            {
                objectsLength--;
                objectsLength = projectiles.Count;
            }
        } for (int i = 0; i < interactables.Count; i++) {
            objectsLength = interactables.Count;
            removeProjectile ((Skill)interactables [i]);
            if (objectsLength > interactables.Count)
            {
                objectsLength--;
                objectsLength = interactables.Count;
            }
        }
        for (int i = 0; i < MapGrid.Length; i ++) {
            for (int j = 0; j < MapGrid[0].Length; j ++) {
                MapGrid[i][j] = null;
                InteractableGrid[i][j] = null;
                ProjectileGrid[i][j] = null;
                BenignGrid[i][j] = null;
            }
        }
	}

	public Boolean removeObject (Locatable obj)
	{
		if (Objects.Contains (obj)) {
			objects.Remove (obj);
			if (isValid (obj.currentLocation ()) && !isEmpty (obj.currentLocation ())) {
				MapGrid [obj.currentLocation ().Row] [obj.currentLocation ().Column] = null;
			}
			if (obj.sentient ()) {
				Roster.Remove (obj);
				((Player)obj).resetLocations ();
			}
			return true;
		}
		return false;
	}

	public Boolean removeInteractable (Skill s)
	{
		if (s.Properties.Contains (@"DIRECTION")) {
			return removeProjectile (s);
		}
		if (!s.Benign) {
			if (Interactables.Contains (s)) {
				if (s.Owner.TrackSkills.Contains (s)) {
					s.Owner.TrackSkills.Remove (s);
				}

				Interactables.Remove (s);
				if (isValid (s.CurrentLocation) && !isEmptyInteractable (s.CurrentLocation)) {
					InteractableGrid [s.CurrentLocation.Row] [s.CurrentLocation.Column] = null;
				}
				s.CurrentLocation = null;
				return true;
			}
			return false;
		}
		return removeBenign (s);
	}

	public Boolean removeProjectile (Skill s)
	{
		if (Projectiles.Contains(s))
		{
			if (s.Owner.TrackSkills.Contains(s))
			{
				s.Owner.TrackSkills.Remove(s);
			}

			Projectiles.Remove(s);
			Interactables.Remove (s);
			if (isValid(s.CurrentLocation) && !isEmptyProjectile (s.CurrentLocation))
			{
				ProjectileGrid [s.CurrentLocation.Row][s.CurrentLocation.Column] = null;
			}
			s.CurrentLocation = null;
			return true;
		}
		return false;
	}


	public Boolean removeBenign (Skill s)
	{
		if (Interactables.Contains (s)) {
			if (s.Owner.TrackSkills.Contains (s)) {
				s.Owner.TrackSkills.Remove (s);
			}

			Interactables.Remove (s);
			if (isValid (s.CurrentLocation) && !isEmptyBenign (s.CurrentLocation)) {
				BenignGrid [s.CurrentLocation.Row] [s.CurrentLocation.Column] = null;
			}
			s.CurrentLocation = null;
			return true;
		}
		return false;
	}

    public int InteractableSize
    {
        get {
            int interactableSize = 0;
            Skill sk;
            for (int i = 0; i < Interactables.Count; i ++) {
                sk = (Skill)Interactables[i];
                if (!sk.Benign && !sk.Projectile) {
                    interactableSize++;
                }
            }
            return interactableSize;
        }

    }

    public int BenignSize
    {
        get
        {
            int benignSize = 0;
            Skill sk;
            for (int i = 0; i < Interactables.Count; i++)
            {
                sk = (Skill)Interactables[i];
                if (!sk.Benign)
                {
                    benignSize++;
                }
            }
            return benignSize;
        }

    }

	public Skill findInteractable (string name)
	{
		Skill s;
		for (int i = 0; i < Interactables.Count; i++) {
			s = (Skill)Interactables [i];
			if (s.SearchName.Equals (name)) {
				return s;
			}
		}
		return null;
	}

	public Skill findProjectile (string name)
	{
		Skill s;
		for (int i = 0; i<Projectiles.Count; i++) {
			s = (Skill)Projectiles[i];
			if (s.SearchName.Equals (name)) {
				return s;
			}
		}
		return null;
	}

	public override string ToString ()
	{
		string line = string.Format (@"{0}" + '\n', Name);
        line += string.Format(@"Size:{0}x{1}" + '\n', MapGrid.Length, MapGrid[0].Length);
		line += string.Format (@"SN:" + SearchName + '\n');
        line += string.Format (@"Music:" + Music + '\n'); 
		line += string.Format (@"Rounds:" + Rounds + '\n');
		line += string.Format (@"Played Rounds:" + PlayedRounds + '\n');
		line += string.Format (@"Index:" + Index + '\n');
        line += string.Format (@"Active Time:" + ActiveTime + '\n');
		line += string.Format (@"Mode:" + Mode + '\n');
        line += string.Format (@"Objects Total:" + (Objects.Count + Interactables.Count) + '\n');
		Location loc;
		for (int i = 0; i < MapGrid.Length; i++) {
            //line += " " + '\n';
            for (int j = 0; j < MapGrid[i].Length; j++)
            {
                loc = new Location(i, j);
                if (isValid(loc))
                {
                    if (!isEmpty(loc))
                    {
                        if (objectAt(loc).sentient())
                        {
                            line += string.Format(@"{0},{1}|{2}" + '\n', loc, @"player", ((Player)objectAt(loc)).SearchName);
                        }
                        else
                        {
                            line += string.Format(@"{0},{1}|{2}" + '\n', loc, @"mapobj", ((MapObject)objectAt(loc)).SearchName);
                        }
                    }
                    if (!isEmptyInteractable(loc))
                    {
                        line += string.Format(@"{0},{1}|{2}" + '\n', loc, @"interact", interactableAt(loc).SearchName);
                    }
                    if (!isEmptyProjectile (loc))
                    {
                        line += string.Format(@"{0},{1}|{2}" + '\n', loc, @"projectile", projectileAt(loc).SearchName);
                    }
                    if (!isEmptyBenign(loc))
                    {
                        line += string.Format(@"{0},{1}|{2}" + '\n', loc, @"benign", benignAt(loc).SearchName);
                    }

                }
            }
		}
		return line;
	}

	public string InfoToString ()
	{
		string line = string.Format (@"{0}, {1}x{2}" + '\n', Name, MapGrid.Length, MapGrid [0].Length);
		Location loc;
		for (int i = 0; i < MapGrid.Length; i++) {
			//line += " " + '\n';
			for (int j = 0; j < MapGrid [i].Length; j++) {
				loc = new Location (i, j);
				if (isValid (loc) && !isEmpty (loc) && objectAt(loc).isVisible() && objectAt(loc).sentient()) {
					line += string.Format (@"   ({0}): {1} ({2}), {3}" + '\n',
						loc, ((Player)objectAt (loc)).Name, ((Player)objectAt(loc)).MyTeam.Name,
						((Player)objectAt(loc)).Health);
				}
			}
		}
		return line;
	}

	public string StringRep (Player p) {

		string line = string.Format (@"{0}, {1}x{2}" + '\n', Name, MapGrid.Length, MapGrid [0].Length);
		Player pl;
		for (int i = 0; i < MapGrid.Length; i++) {
			line += " " + '\n';
			for (int j = 0; j < MapGrid [i].Length; j++) {
				if (!isEmpty (new Location (i, j)) && MapGrid[i][j].isVisible()) {

					if (MapGrid [i] [j].Equals (p)) {
						line += string.Format ('@' + p.FirstName.Substring (0, 3) + p.MyTeam.Abbreviation + '\t');
					} else {

						if (MapGrid [i] [j].sentient ()) {
							pl = (Player)MapGrid [i] [j];
							if (pl.KOd) {
								line += string.Format ('X' + pl.FirstName.Substring (0, 3) + pl.MyTeam.Abbreviation + '\t');
							} else if (pl.IsDefending) {
								if (pl.IsCrouching) {
									line += string.Format ('(' + pl.FirstName.Substring (0, 2) + pl.MyTeam.Abbreviation + ')' + '\t');
								} else {
									line += string.Format ('[' + pl.FirstName.Substring (0, 2) + pl.MyTeam.Abbreviation + ']' + '\t');
								}
							} else if (pl.Grounded.IsActive) {
								line += string.Format ('_' + pl.FirstName.Substring (0, 3) + pl.MyTeam.Abbreviation + '\t');
							} else if (pl.Airborne.IsActive) {
								line += string.Format ('^' + pl.FirstName.Substring (0, 3) + pl.MyTeam.Abbreviation + '\t');
							} else if (pl.IsCrouching) {
								line += string.Format ('|' + pl.FirstName.Substring (0, 2) + pl.MyTeam.Abbreviation + '|' + '\t');
							} else if (pl.IsResting) {
								line += string.Format ('#' + pl.FirstName.Substring (0, 2) + pl.MyTeam.Abbreviation + '#' + '\t');
							} else if (pl.Fatigue> 0 && pl.ResidualFatigue > 0) {
								line += string.Format ('"' + pl.FirstName.Substring (0, 2) + pl.MyTeam.Abbreviation + '"' + '\t');
							} else if (pl.Daze.IsActive || pl.Confuse.IsActive) {
								line += string.Format ('≈' + pl.FirstName.Substring (0, 2) + pl.MyTeam.Abbreviation + '≈' + '\t');
							} else if (pl.Sleep.IsActive) {
								line += string.Format (';' + pl.FirstName.Substring (0, 2) + pl.MyTeam.Abbreviation + ';' + '\t');
							} else {
								line += string.Format (pl.FirstName.Substring (0, 3) + pl.MyTeam.Abbreviation + '\t');
							}
						} else {
							line += string.Format (MapGrid[i][j].mapName().Substring (0, 3) + ((MapObject)MapGrid[i][j]).Strength + '\t');
						}
					}

				} else if (!isEmptyInteractable (new Location (i, j))) {
					line += string.Format (@"{0}" + '\t', InteractableGrid[i][j].Name.Substring(0, 3));
				} else {
					if (HeightMap [i] [j] != 0) {
						line += string.Format (@"--" + HeightMap [i] [j] + @"--" + '\t');
					} else {
						line += string.Format (@"-----" + '\t');
					}
				}
			}
			line += " " + '\n';
		}
		line += " " + '\n';
		return line;
		//}
	}

	public bool Contains (Player p) {
		Player pl;
		for (int i = 0; i < Roster.Count; i++) {
			pl = (Player)Roster [i];
			//print (string.Format(@"Comparing {0} and {1}", p.FirstName, pl.FirstName));
			if (p.Equals (pl)) {
				//print (p.FirstName + @" found!");
				return true;
			}
		}
		//print (p.FirstName + @" not found!");
		return false;
	}

	public string stringRep (Skill s)
	{
		Location loc;
		Player p;
		string line = string.Format (@"{0}, {1}x{2}" + '\n', Name, MapGrid.Length, MapGrid [0].Length);
		for (int i = 0; i < MapGrid.Length; i++) {
			line += " " + '\n';
			for (int j = 0; j < MapGrid [i].Length; j++) {
				loc = new Location (i, j);
				if (!isEmpty (loc) && objectAt(loc).isVisible()) {
					if (objectAt (loc).sentient ()) {
						p = (Player)objectAt (loc);
						if (p.Equals (s.Owner)) {
							line += string.Format ('@'+ p.MyTeam.Abbreviation + p.FirstName.Substring (0, 1) + loc.ToString () + '\t');
						} else if (s.Locations.Contains (loc) && s.willConnectToTarget (p)) {
							line += string.Format ('*' + p.MyTeam.Abbreviation + p.FirstName.Substring (0, 1) + loc.ToString () + '\t');
						} else {
							line += string.Format (p.MyTeam.Abbreviation + p.FirstName.Substring (0, 1) + loc.ToString () + '\t');
						}
					} else {
						if (s.Locations.Contains (loc)) {
							line += string.Format ('*' + MapGrid [i] [j].mapName ().Substring (0, 2) + loc.ToString () + '\t');
						} else {
							line += string.Format (MapGrid [i] [j].mapName ().Substring (0, 2) + loc.ToString () + '\t');
						}
					}
				} else if (!isEmptyInteractable (loc)) {
					if (s.Locations.Contains (loc)) {
						line += string.Format ('*' + InteractableGrid[i][j].SearchName.Substring(0, 2) + loc.ToString() + '\t');
					} else {
						line += string.Format (InteractableGrid[i][j].SearchName.Substring(0, 2) + '\t');
					}
				} else {
					if (s.Locations.Contains (loc)) {
						line += string.Format (loc.ToString() + '\t');
					} else {
						line += string.Format (@"--" + HeightMap[i][j] + @"--" + '\t');
					}
				}
			}
			line += " " + '\n';
		}
		line += " " + '\n';
		return line;

	}


	public Boolean contains (string nm)
	{
		for (int i = 0; i < Objects.Count; i++) {
			if (((Locatable)Objects [i]).mapName ().Equals (nm)) {
				return true;
			}
		}
		return false;

	}
}




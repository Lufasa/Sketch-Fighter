using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Diagnostics;

public class Team
{
	private Player activePlayer;
	string name;
	string abbreviation;
	int maxSize;
	public ArrayList players;
	public ArrayList listOfPlayers;
	public Boolean autoLink;
	Team extraTeam;
	ArrayList inventory;
	public string basicTeamOutput;
	private Player champion;
	private Player fallen;
	private Team master;
	private Boolean chessMode;
	private Boolean hasWon;
    private int roundsWon;
    private ArrayList methodsWon;

	public Team (string nm, string abb, int max)
	{
		name = nm;
		abbreviation = abb;
		maxSize = max;
		players = new ArrayList ();
		extraTeam = null;
		autoLink = true;
		champion = null;
		fallen = null;
		inventory = new ArrayList ();
		basicTeamOutput = "";
		master = null;
		activePlayer = null;
		chessMode = false;
		hasWon = false;
        roundsWon = 0;
        methodsWon = new ArrayList(); 
	}

    public int playerIndex (Player member)
    {
        Player p = null;
        int ind;
        for (int i = 0; i < TotalRoster.Count; i++)
        {
            p = (Player)TotalRoster[i];
            ind = i;
            if (p.Equals (member))
            {
                return ind;
            }

        }
        return -1;
    }

    public ArrayList MethodsWon
    {
        get {
            return methodsWon;
        }
        set {
            methodsWon = value;
        }
    }

    public int RoundsWon
    {
        get {
            return roundsWon;
        }
        set {
            roundsWon = value;
        }
    }

    public ArrayList TotalRoster
    {
        get {
            ArrayList list = Roster;
            Team thisTeam = extraTeam; 

            while (thisTeam != null) {
                for (int i = 0; i < thisTeam.Roster.Count; i ++) {
                    list.Add (Roster [i]);
                }
                thisTeam = thisTeam.ExtraTeam;
            }
            return list;
        }
    
    }

    public Team ExtraTeam
    {
        get {
            return extraTeam;
        }
        set {
            extraTeam = value;
        }
    }

	public Boolean HasWon
	{
		get {
			return hasWon && !IsDefeated;
		}
		set {
			hasWon = value;
		}
	}

	public Boolean ChessMode
	{
		get {
			return chessMode;
		}
		set {
			chessMode = value;
		}
	}
	public Player ActivePlayer
	{
		get {
			return activePlayer;
		}
		set {
			activePlayer = value;
		}
	}

	public ArrayList Inventory
	{
		get {
			return inventory;
		}
		set {
			inventory = value;
		}
	}

	public Team MasterTeam
	{
		get {
			return master;
		}
		set {
			master = value;
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

	public void setInventory (Player p)
	{
		Skill s;
		for (int i = 0; i < Inventory.Count; i ++) {
			s = (Skill)Inventory [i];
			s.Owner = p;
			s.setLocations ();
		}
	}

	public Boolean AutoLink
	{
		get {
			return autoLink;
		}
		set {
			autoLink = value;
			extraTeam.AutoLink = value;
		}
	}

	public Player this[int index]
	{
		get {
			return (Player)players [index];
		}
	}

	public ArrayList Active
	{
		get {
			ArrayList alive = new ArrayList ();
			for (int i = 0; i < Roster.Count; i++) {
				if (!((Player)Roster [i]).KOd) {
					alive.Add (Roster [i]);
				}
			}
			for (int i = 0; i < SubTeam.Count; i++) {
				if (!SubTeam [i].KOd) {
					alive.Add (SubTeam [i]);
				}
			}
			return alive;
		}
	}


	public Team SubTeam
	{
		get {
			return extraTeam;
		}
		set {
			extraTeam = value;
			if (extraTeam != null) {
				extraTeam.MasterTeam = this;
			}
		}
	}

	public ArrayList ListOfPlayers
	{
		get {
			return listOfPlayers;
		}
		set {
			listOfPlayers = value;
		}
	}

	public int Size
	{
		get {
			int size = 0;
			for (int i = 0; i < players.Count; i++) {
				size += ((Player)players [i]).Ratio;
			}
			if (extraTeam != null) {
				for (int i = 0; i < extraTeam.Count; i++) {
					size += ((Player)extraTeam [i]).Ratio;
				}
			}
			return size;
		}
	}

	public int ActiveCount
	{
		get {
			return Active.Count;
		}
	}

	public ArrayList NonRepresentative
	{
		get {
			ArrayList pl = new ArrayList ();
			for (int i = 0; i < Roster.Count; i++) {
				if (!((Player)Roster [i]).Equals (Representative)) {
					pl.Add (Roster [i]);
				}
			}
			return pl;
		}
	}

	public Player MVP
	{
		get {
			int output = -100000;
			Player p, activeP = null;
			for (int i = 0; i < Roster.Count; i++) {
				p = (Player)Roster [i];
				if (activeP == null || p.PerformanceRating > output) {
					activeP = p;
					output = p.PerformanceRating;
				}
			}
			if (activeP != null) {
				return activeP;
			}
			return Representative;
		}
	}

	public int HighestDamage
	{
		get {
			int highestDamage = 0;
			for (int i = 0; i < Roster.Count; i++) {
				if (this [i].DamageTotal > highestDamage) {
					highestDamage = this [i].DamageTotal;
				}
			}
			return highestDamage;
		}
	}

	public string HighestDamageChar
	{
		get {
			string nm = "";
			int damageTotal = 0;
			for (int i = 0; i < Roster.Count; i++) {
				if (this [i].DamageTotal > damageTotal) {
					damageTotal = this [i].DamageTotal;
					nm = this [i].FirstName;
				}
			}
			return nm;
		}
	}

	public int LongestCombo
	{
		get {
			int highestCombo = 0;
			for (int i = 0; i < Roster.Count; i++) {
				if (this [i].LongestCombo.Count > highestCombo) {
					highestCombo = this [i].LongestCombo.Count;
				}
			}
			return highestCombo;
		}
	}

	public string LongestComboChar
	{
		get {
			string nm = "";
			int highestCombo = 0;
			for (int i = 0; i < Roster.Count; i++) {
				if (this [i].HighestCombo > highestCombo) {
					highestCombo = this [i].HighestCombo;
					nm = this [i].FirstName;
				}
			}
			return nm;
		}
	}

	public Player Representative
	{
		get {
			if (IsDefeated) {
				return Fallen;
			}
			return Champion;
		}
		set {
			if (IsDefeated) {
				Fallen = value;
			} else {
				Champion = value;
			}
		}
	}

	public Player Fallen
	{
		get {
			if (IsDefeated && fallen == null) {
				return (Player)players [0];
			}
			return fallen;
		}
		set {
			fallen = value;
		}
	}

	public Player Champion
	{
		get {
			if (!IsDefeated && champion == null) {
				for (int i = 0; i < players.Count; i++) {
					if (((Player)players [i]).OnField && !((Player)players [i]).KOd) {
						return (Player)players [i];
					}
				}
			}
			return champion;
		}
		set {
			champion = value;
            champion.Champion = true;
		}
	}

	public int ActiveSize
	{
		get {
			int size = 0;
			for (int i = 0; i < players.Count; i++) {
				if (!((Player)players [i]).KOd && ((Player)players [i]).InPlay) {
					size+= ((Player)players [i]).Ratio;
				}
			}
			return size;
		}
	}

	public Boolean IsFull
	{
		get {
			return Size == maxSize;
		}
	}

	public Boolean Contains (string sName)
	{
		Player p;
		for (int i = 0; i < players.Count; i++) {
			p = (Player)players [i];
			if (p.SearchName.Equals (sName)) {
				return true;
			}
		}
		if (extraTeam != null) {
			for (int i = 0; i < extraTeam.Count; i++) {
				p = (Player)extraTeam [i];
				if (p.SearchName.Equals (sName)) {
					return true;
				}
			}
		}
		return false;
	}

	public Boolean Contains (Player p)
	{
		for (int i = 0; i < players.Count; i++) {
			if (p.Equals ((Player)players [i])) {
				return true;
			}
		}
		if (extraTeam != null) {
			for (int i = 0; i < extraTeam.Count; i++) {
				if (p.Equals ((Player)extraTeam [i])) {
					return true;
				}
			}
		}
		return false;
	}


	public Boolean ContainsClass (Player addition) {
		if (addition == null) {
			return false;
		}
		Player p;
		for (int i = 0; i < players.Count; i++) {
			p = (Player)players [i];
			if (p.PlayerClass.Equals (addition.SearchName) || p.PlayerClass.Equals (addition.PlayerClass)) {
				return true;
			}
		}
		return false;
	}

	public Boolean ContainsClass (string sName)
	{
		Player p;
		for (int i = 0; i < players.Count; i++) {
			p = (Player)players [i];
			if (p.PlayerClass.Equals (sName)) {
				return true;
			}
		}
		return false;
	}

	public Boolean DifferentClasses
	{
		get {
			//ArrayList list = players;
			Player p;

			for (int i = 0; i < players.Count; i++) {
				p = (Player)players [i];
				for (int j = 0; j < players.Count; j++) {
					if (!players[j].Equals(p) && ((Player)players[j]).PlayerClass.Equals(p.PlayerClass)) {
						return false;
					}
				}
			}

			DataReader reader = new DataReader ();
			for (int i = 0; i < players.Count; i++) {
				p = (Player)players [i];

				if (p.PlayStyle.Equals ("A")) {
					p.shiftVitality (300, false);
				} else if (p.PlayStyle.Equals ("B")) {
					p.shiftVitality (200, false);
					p.CriticalLimit += (int)(p.CriticalLimit * .3);
				} else {
					p.shiftVitality (100, false);
				}
			}
			return true;
		}
	}

	public Boolean Contains (int index)
	{
		if (index == -1) {
			return true;
		}
		Player p;
		for (int i = 0; i < players.Count; i++) {
			p = (Player)players [i];
			if (p.Index.Equals (index)) {
				return true;
			}
		}
		if (extraTeam != null) {
			for (int i = 0; i < extraTeam.Count; i++) {
				p = (Player)extraTeam [i];
				if (p.Index.Equals (index)) {
					return true;
				}
			}
		}
		return false;
	}

	public int MAXSIZE
	{
		get {
			return maxSize;
		} set {
			maxSize = value;
		}
	}


	public Boolean canAdd (Player p)
	{
		return p != null && (Size + p.Ratio <= MAXSIZE) && (!Contains(p.SearchName) || p.SearchName.Contains ("randomplayer"));
	}

	public Boolean canAddClass (Player p)
	{
		return p != null && !Contains(p.SearchName) && !ContainsClass(p.PlayerClass);
	}

	public string Name
	{
		get {
            if (MasterTeam != null) {
                return MasterTeam.Name + " ==> " + Name;
            }
            return name;
		}
		set {
			name = value;
		}
	}

	public string Abbreviation
	{
		get {
			return abbreviation;
		}
		set {
			abbreviation = value;
		}
	}

	public void addPlayer (Player p)
	{
		players.Add (p);
		p.MyTeam = this;
	}

	public ArrayList Roster {
		get {
			return players;
		}
		set {
			players = value;
			for (int i = 0; i < players.Count; i++) {
				((Player)players [i]).MyTeam = this;
			}
		}
	}

	public Boolean IsDefeated
	{
		get {
			if (ChessMode) {
				return this [0].KOd || !this [0].OnField;
			}
			Player p;
			for (int i = 0; i < Roster.Count; i++) {
				p = ((Player)Roster [i]);
				if (p.InPlay) {
					if (!p.KOd) {
						return false;
					}
				}
			}
			if (MasterTeam != null) {
				return MasterTeam.IsDefeated;
			}
			return true;
		}
	}

	public int Count
	{
		get {
			return Roster.Count;
		}
	}

	public string InfoToString (Player pl)
	{
		string line = string.Format ("{0} ({1})" + '\n', Name, Count);
		Player p;
		for (int i = 0; i < Roster.Count; i++) {
			p = (Player)Roster[i];
			if (!p.KOd) {
				line += string.Format ("   ({0}): {1} ({2}), {3}",
					p.currentLocation (), p.Name, p.PlayStyleDisplay, p.Health.MeterInfo);

				if (p.Equals (pl)) {
					line += " *";
				}
				line += "" + '\n';

			}
		}
		return line;
	}

	public override string ToString ()
	{
		string output = string.Format("{0}" + '\n', Name);
		Player p;
		for (int i = 0; i < Roster.Count; i++) {
			p = (Player)Roster [i];
			output += string.Format ("{0} [{1}, {2}]", p.FirstName, p.Ratio, p.PlayStyleDisplay);
			if (i + 1 < Roster.Count) {
				output += ", ";
			}
		}

		return output;
	}

	public string BasicTeamOutput
	{
		get {
			return basicTeamOutput;
		}
		set {
			basicTeamOutput = value;
		}
	}

	public override bool Equals (object obj)
	{
		if (obj != null) {
			Team rhs = (Team)obj;
			if (Name.Equals (rhs.Name) && Roster.Equals (rhs.Roster)) {
				return true;
			}
			if (SubTeam != null) {
				return SubTeam.Equals (rhs);
			}
			if (rhs.SubTeam != null) {
				return rhs.SubTeam.Equals (this);
			}

		}
		return false;
	}
}



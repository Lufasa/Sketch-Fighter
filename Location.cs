using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Diagnostics;

public class Location
{
	private int row;
	private int column;
	private int height;

	public Location (int r, int c)
	{
		row = r;
		column = c;
	}

	public Location (int r, int c, int h)
	{
		row = r;
		column = c;
		height = h;
	}

	public int Row
	{
		get { return row;}
	}

	public int Column
	{
		get { return column;}
	}

	public int Height
	{
		get { return height;}
	}

	public override string ToString ()
	{
		return string.Format ("{0}:{1}", Row, Column);
	}

	public string ToString (Map mp)
	{
		return string.Format ("{0}:{1}:{2}", Row, Column, mp.heightOf(this));
	}

	public int CompareTo (object obj)
	{
		Location rhs = (Location)obj;
		if (Row < rhs.Row || Column < rhs.Column || Height < rhs.Height) {
			return 1;
		} else if (Row > rhs.Row || Column > rhs.Column || Height > rhs.Height) {
			return -1;
		} return 0;
	}

	public int span (Location loc)
	{
		return Math.Abs (Row - loc.Row) + Math.Abs(Column - loc.Column);
	}

	public int realSpan (Location loc)
	{
		return (Row - loc.Row) + (Column - loc.Column);
	}

	public int fullSpan (Location loc) {
		return Math.Abs (Row - loc.Row) + Math.Abs(Column - loc.Column) + Math.Abs(Height - loc.Height);
	}

	public override bool Equals (object obj)
	{
		Location rhs = (Location)obj;
		return obj != null && rhs.Row == Row && rhs.Column == Column && rhs.Height == Height;
	}

	public Location North
	{ get { return new Location (Row - 1, Column);}}

	public Location NorthEast
	{ get { return new Location (Row - 1, Column + 1);}}

	public Location East
	{ get { return new Location (Row, Column + 1);}}

	public Location SouthEast
	{ get { return new Location (Row + 1, Column + 1);}}

	public Location South
	{ get { return new Location (Row + 1, Column);}}

	public Location SouthWest
	{ get { return new Location (Row + 1, Column - 1);}}

	public Location West
	{ get { return new Location (Row, Column - 1);}}

	public Location NorthWest
	{ get { return new Location (Row - 1, Column - 1);}}

	public string DirectionOf (Locatable loc)
	{
		return DirectionOf (loc.currentLocation ());
	}

	public string DirectionOf(Location loc)
	{
		if (loc != null) {
			if (Row > loc.Row && Column == loc.Column) {
				return "N";
			}
			if (Row > loc.Row && Column < loc.Column) {
				if (Math.Abs (Row - loc.Row) > Math.Abs (Column - loc.Column)) {
					return "NNE";
				}
				if (Math.Abs (Row - loc.Row) < Math.Abs (Column - loc.Column)) {
					return "ENE";
				}
				return "NE";
			}
			if (Row == loc.Row && Column < loc.Column) {
				return "E";
			}
			if (Row < loc.Row && Column < loc.Column) {
				if (Math.Abs (Row - loc.Row) > Math.Abs (Column - loc.Column)) {
					return "SSE";
				}
				if (Math.Abs (Row - loc.Row) < Math.Abs (Column - loc.Column)) {
					return "ESE";
				}
				return "SE";
			}
			if (Row < loc.Row && Column == loc.Column) {
				return "S";
			}
			if (Row < loc.Row && Column > loc.Column) {
				if (Math.Abs (Row - loc.Row) > Math.Abs (Column - loc.Column)) {
					return "SSW";
				}
				if (Math.Abs (Row - loc.Row) < Math.Abs (Column - loc.Column)) {
					return "WSW";
				}
				return "SW";
			}
			if (Row == loc.Row && Column > loc.Column) {
				return "W";
			}
			if (Row > loc.Row && Column > loc.Column) {
				if (Math.Abs (Row - loc.Row) > Math.Abs (Column - loc.Column)) {
					return "NNW";
				}
				if (Math.Abs (Row - loc.Row) < Math.Abs (Column - loc.Column)) {
					return "WNW";
				}
				return "NW";
			}
		}
		return "X";
	}

	public string OppositeDirectionOf (Locatable loc)
	{
		return new Location (Row, Column).OppositeDirectionOf (loc.currentLocation ());
	}

	public string OppositeDirectionOf (Location loc)
	{
		if (Row > loc.Row && Column == loc.Column) {
			return "S";
		} if (Row > loc.Row && Column < loc.Column) {
			if (Math.Abs (Row - loc.Row) > Math.Abs (Column - loc.Column)) {
				return "SSW";
			} if (Math.Abs (Row - loc.Row) < Math.Abs (Column - loc.Column)) {
				return "WSW";
			}
			return "SW";
		}
		if (Row == loc.Row && Column < loc.Column) {
			return "W";
		}
		if (Row < loc.Row && Column < loc.Column) {
			if (Math.Abs (Row - loc.Row) > Math.Abs (Column - loc.Column)) {
				return "NNW";
			} if (Math.Abs (Row - loc.Row) < Math.Abs (Column - loc.Column)) {
				return "WNW";
			}
			return "NW";
		}
		if (Row < loc.Row && Column == loc.Column) {
			return "N";
		}
		if (Row < loc.Row && Column > loc.Column) {
			if (Math.Abs (Row - loc.Row) > Math.Abs (Column - loc.Column)) {
				return "NNE";
			} if (Math.Abs (Row - loc.Row) < Math.Abs (Column - loc.Column)) {
				return "ENE";
			}
			return "NE";
		}
		if (Row == loc.Row && Column > loc.Column) {
			return "E";
		}
		if (Row > loc.Row && Column > loc.Column) {
			if (Math.Abs (Row - loc.Row) > Math.Abs (Column - loc.Column)) {
				return "SSE";
			} if (Math.Abs (Row - loc.Row) < Math.Abs (Column - loc.Column)) {
				return "ESE";
			}
			return "SE";
		}
		return "X";
	}
	public Location getNewLocationFromDirection (string direction)
	{
		if (direction.Equals("N") || direction.Equals("NNE") || direction.Equals("NNW"))
		{	return North;}

		if (direction.Equals("E") || direction.Equals("ENE") || direction.Equals("ESE"))
		{	return East;}

		if (direction.Equals("S") || direction.Equals("SSE") || direction.Equals("SSW"))
		{	return South;}

		if (direction.Equals("W") || direction.Equals("WNW") || direction.Equals("WSW"))
		{	return West;}

		if (direction.Equals ("NE"))
		{	return NorthEast;}

		if (direction.Equals ("SE"))
		{	return SouthEast;}

		if (direction.Equals ("SW"))
		{	return SouthWest;}

		if (direction.Equals ("NW"))
		{	return NorthWest;}

		return this;
	}

	public Location getNewLocationFromOppositeDirection (string direction)
	{
		if (direction.Equals("N") || direction.Equals("NNE") || direction.Equals("NNW"))
		{	return South;}

		if (direction.Equals("E") || direction.Equals("ENE") || direction.Equals("ESE"))
		{	return West;}

		if (direction.Equals("S") || direction.Equals("SSE") || direction.Equals("SSW"))
		{	return North;}

		if (direction.Equals("W") || direction.Equals("WNW") || direction.Equals("WSW"))
		{	return East;}

		if (direction.Equals ("NE"))
		{	return SouthWest;}

		if (direction.Equals ("SE"))
		{	return NorthWest;}

		if (direction.Equals ("SW"))
		{	return NorthEast;}

		if (direction.Equals ("NW"))
		{	return SouthEast;}

		return this;
	}

	public Boolean bordersWith (Location loc)
	{
		return span (loc) == 1;
	}

	public Location closest (Map mp, Location loc)
	{
		int dSpan = 60;
		Location lc = null;

		for (int i = 0; i < mp.Rows; i++) {
			for (int j = 0; j < mp.Columns; j++) {
				lc = new Location (i, j);
				if (lc.span (loc) < dSpan && bordersWith (lc)) {
					lc = new Location (i, j, loc.Height);
					dSpan = span (lc);
				}
			}
		}
		return lc;
	}

	public string DirectionLeft (Location loc)
	{
		string directOf = DirectionOf (loc), newDirect = "";
		if (directOf.Contains ("N")) {
			newDirect += "W";
		} if (directOf.Contains ("E")) {
			newDirect += "N";
		} if (directOf.Contains ("S")) {
			newDirect += "E";
		} if (directOf.Contains ("W")) {
			newDirect += "S";
		}

		return newDirect;
	}

	public string DirectionRight(Location loc)
	{
		string directOf = DirectionOf(loc), newDirect = "";
		if (directOf.Contains("N"))
		{
			newDirect += "E";
		}
		if (directOf.Contains("E"))
		{
			newDirect += "S";
		}
		if (directOf.Contains("S"))
		{
			newDirect += "W";
		}
		if (directOf.Contains("W"))
		{
			newDirect += "N";
		}
		return newDirect;
	}

	public string DirectionLeft (string dir)
	{
		string newDirect = "";
		if (dir.Contains("N"))
		{
			newDirect += "W";
		}
		if (dir.Contains("E"))
		{
			newDirect += "N";
		}
		if (dir.Contains("S"))
		{
			newDirect += "E";
		}
		if (dir.Contains("W"))
		{
			newDirect += "S";
		}
		return newDirect;
	}

	public string DirectionRight (string dir)
	{
		string newDirect = "";
		if (dir.Contains("N"))
		{
			newDirect += "E";
		}
		if (dir.Contains("E"))
		{
			newDirect += "S";
		}
		if (dir.Contains("S"))
		{
			newDirect += "W";
		}
		if (dir.Contains("W"))
		{
			newDirect += "N";
		}
		return newDirect;
	}

}



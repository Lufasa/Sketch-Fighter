using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Diagnostics;


public interface Locatable
{
	Location currentLocation ();

	ArrayList actualSpace ();

	void changeLocation (Location newLocation);

	int row ();

	int column ();

	void setCurrentMap (Map mp);

	Map currentMap ();

	int currentHeight ();

	bool sentient ();

	string mapName ();

	int span (Locatable otherLoc);

	Boolean inLine (Location loc);

	Boolean bordersWith (string objName);

	Boolean isNorth (Location obj);

	Boolean isNorthNorthEast (Location obj);

	Boolean isNorthEast (Location obj);

	Boolean isEastNorthEast (Location obj);

	Boolean isEast (Location obj);

	Boolean isEastSouthEast (Location obj);

	Boolean isSouthEast (Location obj);

	Boolean isSouthSouthEast (Location obj);

	Boolean isSouth (Location obj);

	Boolean isSouthSouthWest (Location obj);

	Boolean isSouthWest (Location obj);

	Boolean isWestSouthWest (Location obj);

	Boolean isWest (Location obj);

	Boolean isWestNorthWest (Location obj);

	Boolean isNorthWest (Location obj);

	Boolean isNorthNorthWest (Location obj1);

	Boolean isGenNorth (Location obj);

	Boolean isGenEast (Location obj);

	Boolean isGenSouth (Location obj);

	Boolean isGenWest (Location obj);

	Boolean isVisible ();

	Location closestLocationFrom (Location nextLoc, Boolean diagonals);

	Texture2D mapTexture ();

	string DirectionOf (Location loc);

	string DirectionOf (Locatable loc);

    Vector3 screenLocation ();

    RawImage screenTexture ();
}



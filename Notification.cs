using System;
using UnityEngine;
using UnityEngine.UI;

public class Notification
{
	private Text text;
	private float timer;
	private Vector3 currentLocation;

	public Notification (Text txt, float time, Vector3 loc)
	{
		text = txt;
		timer = time;
		currentLocation = loc;
	}

	public Text Info {
		get {
			return text;
		}
		set {
			text = value;
		}
	}

	public float Timer {
		get {
			return timer;
		}
		set {
			timer = value;
		}
	}

	public Vector3 CurrentLocation {
		get {
			return currentLocation;
		}
		set {
			currentLocation = value;
		}
	}
}


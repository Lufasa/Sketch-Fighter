using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Diagnostics;
using System.IO;
public class StageSelectScript : MonoBehaviour {

	public int stageHighlighted;
	public Camera mapCamera;

	public RawImage[] stages;

	public RawImage stageFloor;
	public RawImage stageLoadingFade;


	Color darkGrey;
	public Color purple;

	public DataReader reader;
	public AudioSource cursorSFX;
	public AudioSource selectSFX;
	public AudioSource stageBGM;
	public AudioSource stageAmbience;
	public AudioSource announcerSFX;
	public float cursorDelay;
	public Text stageInfo;
	private Map[] maps;
	public AudioClip[] music;
	public AudioClip[] ambience;
	private float countdown;
	private bool stageSelected;
	private float inputDelay;
	public static float MAX_VALUE = 0.2f;
	public RawImage[] team1;
	public RawImage[] team2;
	public Text loadingText;

	//public GameObject[] objects;

	// Use this for initialization
	void Start () {
		stageSelected = false;
		countdown = 1.0f;
		inputDelay = 0.0f;
		reader = new DataReader ();
		stageHighlighted = 1;
		cursorSFX = cursorSFX.GetComponent<AudioSource> ();
		selectSFX = selectSFX.GetComponent<AudioSource> ();
		mapCamera = mapCamera.GetComponent <Camera>();

		loadingText.color = new Color (1, 1, 1, 0);
		stageLoadingFade.color = new Color (1, 1, 1, 0);

		for (int i = 0; i < team1.Length; i++) {
			team1 [i].color = new Color (1, 1, 1, 0);
			team2 [i].color = new Color (1, 1, 1, 0);
		}

		stages [0].texture = Resources.Load <Texture2D> (@"Maps/subway/Graphics/BackgroundFloor");
		stages [1].texture = Resources.Load <Texture2D> (@"Maps/city/Graphics/BackgroundFloor");
		stages [2].texture = Resources.Load <Texture2D> (@"Maps/mansion/Graphics/BackgroundFloor");
		stages [3].texture = Resources.Load <Texture2D> (@"Maps/club/Graphics/BackgroundFloor");
		stages [4].texture = Resources.Load <Texture2D> (@"Maps/icemountain/Graphics/BackgroundFloor");
		stages [5].texture = Resources.Load <Texture2D> (@"Maps/minefield/Graphics/BackgroundFloor");
		stages [6].texture = Resources.Load <Texture2D> (@"Maps/chinatown/Graphics/BackgroundFloor");
		stages [7].texture = Resources.Load <Texture2D> (@"Maps/cathedral/Graphics/BackgroundFloor");
		stages [8].texture = Resources.Load <Texture2D> (@"Maps/dojo/Graphics/BackgroundFloor");
		stages [9].texture = Resources.Load <Texture2D> (@"Maps/training/Graphics/BackgroundFloor");
		stages [10].texture = Resources.Load <Texture2D> (@"Textures/Roster");

		//

		StreamReader t1 = new StreamReader (@"Assets/Resources/Data/Teams/Team1.txt");
		int length1 = NumberConverter.ConvertToInt (t1.ReadLine ());
		length1 = NumberConverter.ConvertToInt (t1.ReadLine ());
		length1 = NumberConverter.ConvertToInt (t1.ReadLine ());

		length1 = NumberConverter.ConvertToInt (t1.ReadLine ());
		string name1 = t1.ReadLine ();

		team1 [0].texture = Resources.Load <Texture2D> (@"Players/" + name1.Substring (0, name1.IndexOf (":")) + "/Artwork/Portrait");
		if (length1 > 1) {
			name1 = t1.ReadLine ();
			team1 [1].texture = Resources.Load <Texture2D> (@"Players/" + name1.Substring (0, name1.IndexOf (":")) + "/Artwork/Portrait");
		} else {

		}
		if (length1 > 2) {
			name1 = t1.ReadLine ();
			team1 [2].texture = Resources.Load <Texture2D> (@"Players/" + name1.Substring (0, name1.IndexOf (":")) + "/Artwork/Portrait");
		} else {
		
		}
		if (length1 > 3) {
			name1 = t1.ReadLine ();
			team1 [3].texture = Resources.Load <Texture2D> (@"Players/" + name1.Substring (0, name1.IndexOf (":")) + "/Artwork/Portrait");
		} else {
		
		}

		t1.Close ();

		StreamReader t2 = new StreamReader (@"Assets/Resources/Data/Teams/Team2.txt");
		length1 = NumberConverter.ConvertToInt (t2.ReadLine ());
		length1 = NumberConverter.ConvertToInt (t2.ReadLine ());
		length1 = NumberConverter.ConvertToInt (t2.ReadLine ());

		length1 = NumberConverter.ConvertToInt (t2.ReadLine ());
		name1 = t2.ReadLine ();

		team2 [0].texture = Resources.Load <Texture2D> (@"Players/" + name1.Substring (0, name1.IndexOf (":")) + "/Artwork/Portrait");
		if (length1 > 1) {
			name1 = t2.ReadLine ();
			team2 [1].texture = Resources.Load <Texture2D> (@"Players/" + name1.Substring (0, name1.IndexOf (":")) + "/Artwork/Portrait");
		} else {
		
		}
		if (length1 > 2) {
			name1 = t2.ReadLine ();
			team2 [2].texture = Resources.Load <Texture2D> (@"Players/" + name1.Substring (0, name1.IndexOf (":")) + "/Artwork/Portrait");
		} else {
			
		}
		if (length1 > 3) {
			name1 = t2.ReadLine ();
			team2 [3].texture = Resources.Load <Texture2D> (@"Players/" + name1.Substring (0, name1.IndexOf (":")) + "/Artwork/Portrait");
		} else {
			
		}

		t2.Close ();

		//

		darkGrey = new Color (50.0f/255.0f, 50.0f/255.0f, 50.0f/255.0f);

		for (int i = 0; i < stages.Length; i++) {
			stages [i].color = darkGrey;
		}

		stageInfo = stageInfo.GetComponent<Text> ();
		//stageInfo.color = Color.white;
		maps = reader.loadMaps ();

		print (maps [3].Name);

		music = new AudioClip[10];
		ambience = new AudioClip[10];
		for (int i = 0; i < music.Length; i++) {
			music[i] = Resources.Load <AudioClip> (@"Music/" + maps [i].StageMusic);
			ambience [i] = Resources.Load <AudioClip> (@"Maps/" + maps [i].SearchName + "/Sounds/ambience");
		}

		UpdateText (stageHighlighted);
	}

	public Color appropriateColor (Map battleMap)
	{
		if (battleMap.Dark) {
			return Color.white;
		} if (battleMap.Light) {
			return Color.black;
		} if (battleMap.PrimaryTextColor == "blue") {
			return Color.blue;
		} if (battleMap.PrimaryTextColor == "red") {
			return Color.red;
		} if (battleMap.PrimaryTextColor == "green") {
			return Color.green;
		}
		return Color.black;
	}

	void UpdateText (int i)
	{

		if (i <= maps.Length) {
			print (maps [i - 1].Name);
			stageInfo.text = string.Format (maps [i - 1].Name.ToUpper () + '\n' + '\n' + maps [i - 1].Info);
			stageFloor.texture = Resources.Load<Texture2D> (@"Maps/" + maps [i - 1].SearchName + "/Graphics/BackgroundHorizontal");
			stageLoadingFade.texture = Resources.Load<Texture2D> (@"Maps/" + maps [i - 1].SearchName + "/Graphics/BackgroundHorizontal");
		} else {
			if (i == 11) {
				stageInfo.text = "???";
				stageFloor.texture = Resources.Load<Texture2D> (@"Textures/TAI");
				stageBGM.Stop ();
				stageAmbience.Stop ();
			} else {
				stageInfo.text = "PLAYER";
				stageFloor.texture = Resources.Load<Texture2D> (@"Textures/TTAI");
				stageBGM.Stop ();
				stageAmbience.Stop ();
			}
		}

		if (i <= music.Length) {
			print (music [i - 1]);

			stageBGM.clip = music [i - 1];
			stageBGM.Play ();

			stageAmbience.clip = ambience [i - 1];
			stageAmbience.Play ();

		}


		for (int j = 0; j < stages.Length; j ++) {
			if (j == i - 1) {
				stages [j].color = Color.white;
			} else {
				stages [j].color = darkGrey;
			}
		}
	}

	// Update is called once per frame
	void Update () {

		if (stageSelected && stageHighlighted != 0) {

			if (stageBGM.volume > 0) {
				stageBGM.volume -= Time.deltaTime;
				stageAmbience.volume -= Time.deltaTime;
			}

			if (countdown < 0.0f) {
				loadingText.text = "LOADING";
				loadingText.color = purple;
				SceneManager.LoadScene (3);
				this.enabled = false;
			} else {
				countdown -= Time.deltaTime;
				if (stageLoadingFade.color.a < 1) {
					stageLoadingFade.color = new Color (1, 1, 1, stageLoadingFade.color.a + Time.deltaTime * 10);

					for (int i = 0; i < team1.Length; i++) {
						team1 [i].color = stageLoadingFade.color;
					} for (int i = 0; i < team2.Length; i++) {
						team2 [i].color = stageLoadingFade.color;
					}
				}
			}

		} else if (stageHighlighted != 0) {
			if (inputDelay <= 0.0f) {
				if (Input.GetKey (KeyCode.UpArrow) && stageHighlighted > 1) {
					inputDelay = MAX_VALUE;
					cursorSFX.Play ();
					stageHighlighted--;
					UpdateText (stageHighlighted);
				} else if (Input.GetKey (KeyCode.DownArrow) && stageHighlighted < 12) {
					inputDelay = MAX_VALUE;
					cursorSFX.Play ();
					if (stageHighlighted == 5) {
						stageHighlighted = 11;
					} else {
						stageHighlighted++;
					}
					UpdateText (stageHighlighted);
				} else if (Input.GetKey (KeyCode.LeftArrow) && stageHighlighted > 5 && stageHighlighted != 11) {
					inputDelay = MAX_VALUE;
					cursorSFX.Play ();
					stageHighlighted -= 5;
					UpdateText (stageHighlighted);
				} else if (Input.GetKey (KeyCode.RightArrow) && stageHighlighted < 6) {
					inputDelay = MAX_VALUE;
					cursorSFX.Play ();
					stageHighlighted += 5;
					UpdateText (stageHighlighted);
				}
			} else {
				inputDelay -= Time.deltaTime;
			}
		} else {
			if (countdown <= 0.0f) {
				SceneManager.LoadScene (1);
			} else {
				countdown -= Time.deltaTime;
			}
		}

		if (Input.GetKey (KeyCode.Escape)) {
			selectSFX.Play ();
			stageSelected = true;
			stageInfo.fontSize = 40;
			stageInfo.text = "NEVERMIND!";
			stageHighlighted = 0;
		}
			
		if (Input.GetKey (KeyCode.Return)) {
			selectSFX.Play ();
			announcerSFX.Play ();
			stageSelected = true;
			stageInfo.fontSize = 40;
			stageInfo.text = "READY!";

			if (stageHighlighted == 11) {
				reader.saveMapForBattle ("random", "random");
			} else if (stageHighlighted == 12) {
				reader.saveMapForBattle ("player", "player");
			} else {
				reader.saveMapForBattle (maps [stageHighlighted - 1].SearchName, "stage");
			}
			for (int i = 0; i < stages.Length; i++) {
				stages [i].color = darkGrey;
			}
			countdown = 5.0f;
		}
	
	}

	public void move (GameObject obj, float degree)
	{
		mapCamera.transform.Translate (new Vector3(0, degree, 0));
		print (mapCamera.name + ", " + mapCamera.transform.ToString());
	}
}

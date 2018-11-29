using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Diagnostics;
using System.IO;

public class PlayerSelectScript : MonoBehaviour {

	private static float MAX_VALUE = 0.2f;
	private float countdown;
	public float player1MoveDelay;
	public float player2MoveDelay;

	public RawImage player1Sprite;
	ArrayList player1Sprites;
	int player1Frame;
	public static double P1ANIMATIONSPEED = 0.2f;

	public RawImage player2Sprite;
	ArrayList player2Sprites;
	int player2Frame;
	public static double P2ANIMATIONSPEED = 0.2f;
	public AudioSource announcerSFX;

	public Texture2D questionMark;

	public Text player1Text;
	public Text player2Text;
	public DataReader reader;
	public bool cancelledOut;

	public AudioSource player1Sounds;
	public AudioSource player2Sounds;
	public AudioSource player1Select;
	public AudioSource player2Select;

	public RawImage player1SelectBoxBig;
	public RawImage player1SelectBoxSmall;
	public RawImage player2SelectBoxBig;
	public RawImage player2SelectBoxSmall;

	public RawImage team1Image1;
	public RawImage team1Image2;
	public RawImage team1Image3;
	public RawImage team1Image4;
	public Text team1P1Ratio;
	public Text team1P2Ratio;
	public Text team1P3Ratio;
	public Text team1P4Ratio;

	public RawImage team2Image1;
	public RawImage team2Image2;
	public RawImage team2Image3;
	public RawImage team2Image4;
	public Text team2P1Ratio;
	public Text team2P2Ratio;
	public Text team2P3Ratio;
	public Text team2P4Ratio;

	public RawImage randomPlayerABox;
	public RawImage dahliaBox;
	public RawImage alexBox;
	public RawImage miaBox;
	public RawImage terrenceBox;
	public RawImage aaronBox;
	public RawImage isabelleBox;
	public RawImage mertBox;
	public RawImage kirinBox;
	public RawImage winnieBox;
	public RawImage randomPlayerBBox;

	public Text p1Info;
	public Text p2Info;

	private bool[] player1Selecting;
	private bool[] player2Selecting;
	private bool[] player1PlayerEnabled;
	private bool[] player2PlayerEnabled;

	public Player player1;
	public Player player2;
	Team t1;
	Team t2;

	private int p1SelectedSlot;
	private int p2SelectedSlot;


	private Player[] players;
	private Player[] bosses;

	public int player1On;
	public int player2On;

	public Text aism1;
	public Text bism1;
	public Text cism1;
	public Text rism1;
	public Text aism2;
	public Text bism2;
	public Text cism2;
	public Text rism2;

	public AudioSource bgm;

	public RawImage pointer1;
	public RawImage pointer2;

	private bool player1InSelectMode;
	private bool player2InSelectMode;

	public KeyCode p1UpArrow;
	public KeyCode p2UpArrow;

	public KeyCode p1DownArrow;
	public KeyCode p2DownArrow;

	public KeyCode p1LeftArrow;
	public KeyCode p2LeftArrow;

	public KeyCode p1RightArrow;
	public KeyCode p2RightArrow;

	public KeyCode p1Enter;
	public KeyCode p2Enter;

	public KeyCode p1Escape;
	public KeyCode p2Escape;

	private bool p1HasSelected;
	private bool p2HasSelected;

	private bool p1SelectingRatio;
	private bool p2SelectingRatio;

	private string mode;

	void Awake () {

	}

	// Use this for initialization
	void Start () {

		t1 = new Team ("Team 1", "1", 4);
		t2 = new Team ("Team 2", "2", 4);

		countdown = 2.0f;
		reader = new DataReader ();
		cancelledOut = false;
		p1SelectingRatio = false;
		p2SelectingRatio = false;

		pointer1.enabled = false;
		pointer2.enabled = false;

		mode = "";

		player1InSelectMode = true;
		player2InSelectMode = true;
		//0-RAND-A   1-DAHLIA   2-ALEX   3-MIA   4-TERRENCE   5-AARON   6-ISABELLE   7-MERT   8-KIRIN   9-WINNIE   10-RAND-B 

		players = reader.loadPlayers ();

		p1HasSelected = false;
		p2HasSelected = false;

		Time.timeScale = 1;

		p1SelectedSlot = 1;
		p2SelectedSlot = 1;

		player1Text = player1Text.GetComponent<Text> ();
		player2Text = player2Text.GetComponent<Text> ();

		StreamReader modeStream = new StreamReader (@"Assets/Resources/Data/Current/Mode.txt");

		mode = modeStream.ReadLine ();

		modeStream.Close ();


		player1Sounds = player1Sounds.GetComponent<AudioSource> ();
		player2Sounds = player2Sounds.GetComponent<AudioSource> ();
		player1Sounds.loop = false;
		player2Sounds.loop = false;
		player1Select = player1Select.GetComponent<AudioSource> ();
		player2Select = player2Select.GetComponent<AudioSource> ();
		player1Select.loop = false;
		player2Select.loop = false;

		player1SelectBoxBig = player1SelectBoxBig.GetComponent<RawImage> ();
		player1SelectBoxSmall = player1SelectBoxSmall.GetComponent<RawImage> ();
		player2SelectBoxBig = player2SelectBoxBig.GetComponent<RawImage> ();
		player2SelectBoxSmall = player2SelectBoxSmall.GetComponent<RawImage> ();


		bgm = bgm.GetComponent<AudioSource> ();

		aism1 = aism1.GetComponent<Text> ();
		bism1 = bism1.GetComponent<Text> ();
		cism1 = cism1.GetComponent<Text> ();
		rism1 = rism1.GetComponent<Text> ();
		aism2 = aism2.GetComponent<Text> ();
		bism2 = bism2.GetComponent<Text> ();
		cism2 = cism2.GetComponent<Text> ();
		rism2 = rism2.GetComponent<Text> ();

		team1Image1 = team1Image1.GetComponent<RawImage> ();
		team1Image2 = team1Image2.GetComponent<RawImage> ();
		team1Image3 = team1Image3.GetComponent<RawImage> ();
		team1Image4 = team1Image4.GetComponent<RawImage> ();

		team1Image1.enabled = false;
		team1Image2.enabled = false;
		team1Image3.enabled = false;
		team1Image4.enabled = false;

		//team1P1Ratio.enabled = false;
		//team1P2Ratio.enabled = false;
		//team1P3Ratio.enabled = false;
		//team1P4Ratio.enabled = false;

		team2Image1 = team2Image1.GetComponent<RawImage> ();
		team2Image2 = team2Image2.GetComponent<RawImage> ();
		team2Image3 = team2Image3.GetComponent<RawImage> ();
		team2Image4 = team2Image4.GetComponent<RawImage> ();

		team2Image1.enabled = false;
		team2Image2.enabled = false;
		team2Image3.enabled = false;
		team2Image4.enabled = false;

		//team2P1Ratio.enabled = false;
		//team2P2Ratio.enabled = false;
		//team2P3Ratio.enabled = false;
		//team2P4Ratio.enabled = false;

		aaronBox = aaronBox.GetComponent<RawImage> ();
		terrenceBox = terrenceBox.GetComponent<RawImage> ();
		isabelleBox = isabelleBox.GetComponent<RawImage> ();
		alexBox = alexBox.GetComponent<RawImage> ();
		mertBox = mertBox.GetComponent<RawImage> ();
		winnieBox = winnieBox.GetComponent<RawImage> ();
		kirinBox = kirinBox.GetComponent<RawImage> ();
		dahliaBox = dahliaBox.GetComponent<RawImage> ();
		miaBox = miaBox.GetComponent<RawImage> ();
		randomPlayerABox = randomPlayerABox.GetComponent<RawImage> ();
		randomPlayerBBox = randomPlayerBBox.GetComponent<RawImage> ();

		player1Sprite = player1Sprite.GetComponent <RawImage> ();
		player1Sprites = new ArrayList ();
		player1Frame = 0;

		player2Sprite = player2Sprite.GetComponent <RawImage> ();
		player2Sprites = new ArrayList ();
		player2Frame = 0;

        player2Sprite.rectTransform.rotation = new Quaternion (0, 180, 0, 0);

		setUpInitialPositions ();
	}

	void setUpInitialPositions()
	{
		aism1.enabled = false;
		bism1.enabled = false;
		cism1.enabled = false;
		rism1.enabled = false;
		aism2.enabled = false;
		bism2.enabled = false;
		cism2.enabled = false;
		rism2.enabled = false;

		player1SelectBoxBig.transform.position = dahliaBox.transform.position;
		player2SelectBoxBig.transform.position = winnieBox.transform.position;

		player1SelectBoxBig.enabled = true;
		player1SelectBoxSmall.enabled = false;
		player2SelectBoxBig.enabled = true;
		player2SelectBoxSmall.enabled = false;

		//0-RAND-A   1-DAHLIA   2-ALEX   3-MIA   4-TERRENCE   5-AARON   6-ISABELLE   7-MERT   8-KIRIN   9-WINNIE   10-RAND-B 
		player1Selecting = new bool[11];
		for (int j = 0; j < player1Selecting.Length; j++) {
			player1Selecting[j] = false;
		}
		//Set Player1 highlighting MertBox
		player1Selecting[1] = true;
		player1On = 1;

		player2Selecting = new bool[11];
		for (int j = 0; j < player2Selecting.Length; j++) {
			player2Selecting[j] = false;
		}
		//Set Player1 highlighting MertBox
		player2Selecting[9] = true;
		player2On = 9;

		//0-RAND-A   1-DAHLIA   2-ALEX   3-MIA   4-TERRENCE   5-AARON   6-ISABELLE   7-MERT   8-KIRIN   9-WINNIE   10-RAND-B 
		checkLocation (1, player1On);
		checkLocation (2, player2On);
	}

	void enablePlayer1StyleSelect ()
	{
		aism1.enabled = true;
		bism1.enabled = true;
		cism1.enabled = true;
		rism1.enabled = true;
		pointer1.enabled = true;
		p1Info.enabled = false;
	}

	void disablePlayer1StyleSelect ()
	{
		aism1.enabled = false;
		bism1.enabled = false;
		cism1.enabled = false;
		rism1.enabled = false;
		pointer1.enabled = false;
		p1Info.enabled = true;
		//player1On = -1;
	}

	void enablePlayer2StyleSelect ()
	{
		aism2.enabled = true;
		bism2.enabled = true;
		cism2.enabled = true;
		rism2.enabled = true;
		pointer2.enabled = true;
		p2Info.enabled = false;
	}

	void disablePlayer2StyleSelect ()
	{
		aism2.enabled = false;
		bism2.enabled = false;
		cism2.enabled = false;
		rism2.enabled = false;
		pointer2.enabled = false;
		p2Info.enabled = true;
		//player2On = -1;
	}

	void disablePlayer1 ()
	{
		player1 = null;
		disablePlayer1StyleSelect ();
		player1SelectBoxBig.enabled = false;
		player1SelectBoxSmall.enabled = false;

		player1Text.text = "";

		for (int i = 0; i < player1Selecting.Length; i++) {
			player1Selecting [i] = false;
		}
		bism1.enabled = true;
		bism1.fontSize = 50;
		bism1.text = "READY!";
		p1Info.text = "";
		announcerSFX.Play ();
		player1On = -1;

	}

	void disablePlayer2 ()
	{
		player2 = null;
		disablePlayer2StyleSelect ();
		player2SelectBoxBig.enabled = false;
		player2SelectBoxSmall.enabled = false;

		player2Text.text = "";

		for (int i = 0; i < player2Selecting.Length; i++) {
			player2Selecting [i] = false;
		}
		bism2.enabled = true;
		bism2.fontSize = 50;
		bism2.text = "READY!";
		p2Info.text = "";

		announcerSFX.Play ();
		player2On = -1;
	}

	void checkPlayerInput ()
	{
		if (!t1.IsFull) {
			if (player1 != null) {
				if (player1MoveDelay <= 0.0f) {
					if (Input.GetKey (p1UpArrow)) {
						if (p1SelectedSlot > 1) {
							player1Sounds.Play ();
							player1MoveDelay = MAX_VALUE;
							p1SelectedSlot--;
							pointer1.transform.position = new Vector3 (pointer1.transform.position.x,
								pointer1.transform.position.y + 100f, pointer1.transform.position.z);

						}
					} else if (Input.GetKey (p1DownArrow)) {
						if (p1SelectedSlot < 4) {
							player1Sounds.Play ();
							player1MoveDelay = MAX_VALUE;
							p1SelectedSlot++;
							pointer1.transform.position = new Vector3 (pointer1.transform.position.x,
								pointer1.transform.position.y - 100f, pointer1.transform.position.z);
						}
					} else if (Input.GetKey (p1Enter)) {
						player1MoveDelay = MAX_VALUE;
						if (!p1SelectingRatio) {
							player1Select.Play ();
							if (p1SelectedSlot == 1) {
								player1.PlayStyle = "A";
							}
							if (p1SelectedSlot == 2) {
								player1.PlayStyle = "C";
							}
							if (p1SelectedSlot == 3) {
								player1.PlayStyle = "B";
							}
							if (p1SelectedSlot == 4) {
								player1.PlayStyle = "R";
							}
							//if (mode != "chess") {
								aism1.text = "R1";
								bism1.text = "R2";
								cism1.text = "R3";
								rism1.text = "R4";
								p1SelectingRatio = true;

							//} else {
							//	print (string.Format ("{0} ({1})", player1.FirstName, player1.PlayStyleDisplay));
							//	t1.addPlayer (player1);
							//	p1HasSelected = true;
							//	p1SelectingRatio = false;

							//}

						} else {
							if (p1SelectedSlot == 1) {
								player1.Ratio = 1;
							}
							if (p1SelectedSlot == 2) {
								player1.Ratio = 2;
							}
							if (p1SelectedSlot == 3) {
								player1.Ratio = 3;
							}
							if (p1SelectedSlot == 4) {
								player1.Ratio = 4;
							}

							if (t1.canAdd (player1)) {
								p1SelectingRatio = false;
								player1Select.Play ();

								aism1.text = "A-ISM";
								bism1.text = "B-ISM";
								cism1.text = "C-ISM";
								rism1.text = "???";

								print (string.Format ("{0} ({1})", player1.FirstName, player1.PlayStyleDisplay));
								t1.addPlayer (player1);
								p1HasSelected = true;
							}
						}
					}
				}
				if (p1HasSelected) {
					p1HasSelected = false;
					//SET TEAM IMAGE
					if (t1.Count == 1) {
						team1Image1.enabled = true;
						team1Image1.texture = Resources.Load<Texture2D> (@"Players/" + player1.SearchName + "/Artwork/Portrait");
						team1P1Ratio.text = t1 [0].PlayStyleActual + ", R" + t1 [0].Ratio;
					}
					if (t1.Count == 2) {
						team1Image2.enabled = true;
						team1Image2.texture = Resources.Load<Texture2D> (@"Players/" + player1.SearchName + "/Artwork/Portrait");
						team1P2Ratio.text = t1 [1].PlayStyleActual + ", R" + t1 [1].Ratio;
					}
					if (t1.Count == 3) {
						team1Image3.enabled = true;
						team1Image3.texture = Resources.Load<Texture2D> (@"Players/" + player1.SearchName + "/Artwork/Portrait");
						team1P3Ratio.text = t1 [2].PlayStyleActual + ", R" + t1 [2].Ratio;
					}
					if (t1.Count == 4) {
						team1Image4.enabled = true;
						team1Image4.texture = Resources.Load<Texture2D> (@"Players/" + player1.SearchName + "/Artwork/Portrait");
						team1P4Ratio.text = t1 [3].PlayStyleActual + ", R" + t1 [3].Ratio;
					}


					player1 = null;
					disablePlayer1StyleSelect ();
					if (t1.IsFull) {
						player1Sprite.enabled = false;
						disablePlayer1 ();
					} else {
						checkLocation (1, player1On);
					}
				}
			} else {
				//Player 1 controls
				if (player1MoveDelay <= 0.0f) {
					if (Input.GetKey (p1LeftArrow)) {

						player1Sounds.Play ();
						player1MoveDelay = MAX_VALUE;
						changeLocation (1, "left");

					} else if (Input.GetKey (p1RightArrow)) {

						player1Sounds.Play ();
						player1MoveDelay = MAX_VALUE;
						changeLocation (1, "right");

					} else if (Input.GetKey (p1UpArrow)) {

						//player1Sounds.Play ();
						//player1MoveDelay = MAX_VALUE;
						//changeLocation (1, "up");

					} else if (Input.GetKey (p1DownArrow)) {

						//player1Sounds.Play ();
						//player1MoveDelay = MAX_VALUE;
						//changeLocation (1, "down");

					} else if (Input.GetKey (p1Enter)) {

						if (player1On == 0) {

							player1MoveDelay = MAX_VALUE;

							player1 = players [new System.Random ().Next (players.Length)];
							while (t1.Contains (player1.SearchName) || !t1.canAdd(player1)) {
								player1 = players [new System.Random ().Next (players.Length)];
							}
							//player1Sprite.texture = Resources.Load<Texture2D> (@"Players/randomplayer/Animations/Neutral");
							if (!t1.IsFull) {
								enablePlayer1StyleSelect ();
							} else {
								disablePlayer1 ();
							}
							player1Select.Play ();

						} else if (player1On == 10) {

							player1MoveDelay = MAX_VALUE;

                            player1 = reader.loadPlayer ("randomplayer", "Player", false, false, true);
							//player1Sprite.texture = Resources.Load<Texture2D> (@"Players/randomplayer/Animations/Neutral");
							enablePlayer1StyleSelect ();
							player1Select.Play ();

						} else {

							player1MoveDelay = MAX_VALUE;

							if (!t1.Contains (players [player1On - 1].SearchName) && t1.canAdd(players[player1On - 1])) {
								player1 = reader.loadPlayer (players [player1On - 1].SearchName, "Player", false, false,
                                                             true);
								enablePlayer1StyleSelect ();
								player1Select.Play ();
							}
						}
					} 
				}
			}
		}

		if (!t2.IsFull) {
			if (player2 != null) {
				if (player2MoveDelay <= 0.0f) {
					if (Input.GetKey (p2UpArrow)) {
						player2MoveDelay = MAX_VALUE;
						if (p2SelectedSlot > 1) {
							p2SelectedSlot--;
							pointer2.transform.position = new Vector3 (pointer2.transform.position.x,
								pointer2.transform.position.y + 100f, pointer2.transform.position.z);

						}
					} else if (Input.GetKey (p2DownArrow)) {
						player2MoveDelay = MAX_VALUE;
						player2Sounds.Play ();
						if (p2SelectedSlot < 4) {
							p2SelectedSlot++;
							pointer2.transform.position = new Vector3 (pointer2.transform.position.x,
								pointer2.transform.position.y - 100f, pointer2.transform.position.z);
						}
					} else if (Input.GetKey (p2Enter)) {
						player2MoveDelay = MAX_VALUE;
						player2Sounds.Play ();
						if (!p2SelectingRatio) {
							player2Select.Play ();
							if (p2SelectedSlot == 1) {
								player2.PlayStyle = "A";
							}
							if (p2SelectedSlot == 2) {
								player2.PlayStyle = "C";
							}
							if (p2SelectedSlot == 3) {
								player2.PlayStyle = "B";
							}
							if (p2SelectedSlot == 4) {
								player2.PlayStyle = "R";
							}
							//if (mode != "chess") {
								aism2.text = "R1";
								bism2.text = "R2";
								cism2.text = "R3";
								rism2.text = "R4";
								p2SelectingRatio = true;
							//} else {
							//	print (string.Format ("{0} ({1})", player2.FirstName, player2.PlayStyleDisplay));
							//	t2.addPlayer (player2);
							//	p2HasSelected = true;
							//	p2SelectingRatio = false;
							//}

						} else {

							if (p2SelectedSlot == 1) {
								player2.Ratio = 1;
							}
							if (p2SelectedSlot == 2) {
								player2.Ratio = 2;
							}
							if (p2SelectedSlot == 3) {
								player2.Ratio = 3;
							}
							if (p2SelectedSlot == 4) {
								player2.Ratio = 4;
							}

							if (t2.canAdd (player2)) {
								p2SelectingRatio = false;
								player2Select.Play ();

								aism2.text = "A-ISM";
								bism2.text = "B-ISM";
								cism2.text = "C-ISM";
								rism2.text = "???";

								print (string.Format ("{0} ({1})", player2.FirstName, player2.PlayStyleDisplay));
								t2.addPlayer (player2);
								p2HasSelected = true;
							}
						}
					}
				}
				if (p2HasSelected) {
					p2HasSelected = false;
					//SET TEAM IMAGE
					if (t2.Count == 1) {
						team2Image1.enabled = true;
						team2Image1.texture = Resources.Load<Texture2D> (@"Players/" + player2.SearchName + "/Artwork/Portrait");
						team2P1Ratio.text = t2 [0].PlayStyleActual + ", R" + t2 [0].Ratio;
					}
					if (t2.Count == 2) {
						team2Image2.enabled = true;
						team2Image2.texture = Resources.Load<Texture2D> (@"Players/" + player2.SearchName + "/Artwork/Portrait");
						team2P2Ratio.text = t2 [1].PlayStyleActual + ", R" + t2 [1].Ratio;
					}
					if (t2.Count == 3) {
						team2Image3.enabled = true;
						team2Image3.texture = Resources.Load<Texture2D> (@"Players/" + player2.SearchName + "/Artwork/Portrait");
						team2P3Ratio.text = t2 [2].PlayStyleActual + ", R" + t2 [2].Ratio;
					}
					if (t2.Count == 4) {
						team2Image4.enabled = true;
						team2Image4.texture = Resources.Load<Texture2D> (@"Players/" + player2.SearchName + "/Artwork/Portrait");
						team2P4Ratio.text = t2 [3].PlayStyleActual + ", R" + t2 [1].Ratio;
					}


					player2 = null;
					disablePlayer2StyleSelect ();
					if (t2.IsFull) {
						player2Sprite.enabled = false;
						disablePlayer2 ();
					} else {
						checkLocation (2, player1On);
					}
				}

			} else {
				//Player 2 controls
				if (player2MoveDelay <= 0.0f) {
					if (Input.GetKey (p2LeftArrow)) {

						player2Sounds.Play ();
						player2MoveDelay = MAX_VALUE;
						changeLocation (2, "left");

					} else if (Input.GetKey (p2RightArrow)) {

						player2Sounds.Play ();
						player2MoveDelay = MAX_VALUE;
						changeLocation (2, "right");

					} else if (Input.GetKey (p2UpArrow)) {

						//player2Sounds.Play ();
						//player2MoveDelay = MAX_VALUE;
						//changeLocation (2, "up");

					} else if (Input.GetKey (p2DownArrow)) {

						//player2Sounds.Play ();
						//player2MoveDelay = MAX_VALUE;
						//changeLocation (2, "down");

					} else if (Input.GetKey (KeyCode.KeypadEnter)) {
						if (player2On == 0) {

							player2MoveDelay = MAX_VALUE;

							player2 = players [new System.Random ().Next (players.Length)];
							while (t2.Contains (player2.SearchName) || !t2.canAdd (player2)) {
								player2 = players [new System.Random ().Next (players.Length)];
							}
							//player2Sprite.texture = ImageMover.flipImageHorizontally ( Resources.Load<Texture2D> (@"Players/randomplayer/Animations/Neutral"));
							if (!t2.IsFull) {
								enablePlayer2StyleSelect ();
							} else {
								disablePlayer2 ();
							}

							player2Select.Play ();

						} else if (player2On == 10) {

							player2MoveDelay = MAX_VALUE;

                            player2 = reader.loadPlayer ("randomplayer", "Player", false, false, true);
							//player2Sprite.texture = ImageMover.flipImageHorizontally ( Resources.Load<Texture2D> (@"Players/randomplayer/Animations/Neutral"));
							enablePlayer2StyleSelect ();
							player2Select.Play ();

						} else {

							if (!t2.Contains (players [player2On - 1].SearchName) && t2.canAdd (players [player2On - 1])) {

								player2MoveDelay = MAX_VALUE;

                                player2 = reader.loadPlayer (players [player2On - 1].SearchName, "Player", false, false, true);
								enablePlayer2StyleSelect ();
								player2Select.Play ();
							}
						}
					}
				}
			}
		}

		if (Input.GetKey (p1Escape)) {
			player1Select.Play ();
			aism1.enabled = false;
			cism1.enabled = false;
			aism2.enabled = false;
			cism2.enabled = false;
			bism1.enabled = true;
			bism2.enabled = true;
			p1Info.enabled = false;
			p2Info.enabled = false;
			bism1.fontSize = 50;
			bism2.fontSize = bism1.fontSize;
			bism1.text = "AWW" + '\n' + "REALLY?";
			bism2.text = "COME ON" + '\n' + "MAN!";
			cancelledOut = true;
		}
	}

	void saveTeams()
	{
		DataReader reader = new DataReader ();
		reader.saveTeam ("Team1", t1);
		reader.saveTeam ("Team2", t2);
	}

	void SelectRandomTeam (Team t)
	{
		Player p;
		System.Random r = new System.Random();
		int st;
		for (int i = 0; i < t.MAXSIZE; i++) {
            p = reader.loadPlayer(players [r.Next (players.Length)].SearchName, "Player", false, false, true);
			while (t.Contains (p.SearchName)) {
				p = reader.loadPlayer(players [r.Next (players.Length)].SearchName, "Player", false, false, true);
			}
			st = r.Next (3);
			if (st == 1) {
				p.PlayStyle = "A";
			} else if (st == 2) {
				p.PlayStyle = "B";
			} else {
				p.PlayStyle = "C";
			}
			t.addPlayer (p);
		}
		if (t.Name.Equals ("Team 1")) {
			disablePlayer1 ();

		} else {
			disablePlayer2 ();
		}
	}

	// Update is called once per frame
	void Update () {
		if (!cancelledOut) {

			//if (t1 == null || t2 == null) {
			//	throw new UnityException("WOW DUDE");
			//}

			if (!t1.IsFull || !t2.IsFull) {
				checkPlayerInput ();
				checkTime ("Player 1 Move Delay", ref player1MoveDelay);
				checkTime ("Player 2 Move Delay", ref player2MoveDelay);
			} else {
				if (countdown < 0.0f) {
					saveTeams ();
					SceneManager.LoadScene (2);
				} else {
					countdown -= Time.deltaTime;
				}
			}
		} else {
			if (countdown < 0.0f) {
				this.enabled = false;
				SceneManager.LoadScene (0);
			} else {
				countdown -= Time.deltaTime;
			}
		}
	}

	void checkTime (string name, ref float num)
	{
		if (num >= 0.0f) {
			num -= (Time.deltaTime);
		}
	}

	public void adjustSpriteBasedOnScale (RawImage img)
	{
		img.transform.localScale = new Vector3 (
			img.texture.width//p.SpriteRatio [0] 
			* 0.002f,
			img.texture.height//p.SpriteRatio [1] 
			* 0.002f,
			1f
		);
		//print (string.Format ("{0}: ({1}, {2}, {3})", new object[4] {p.FirstName, img.transform.position.x, img.transform.position.y, p.SpriteScale}));
	}


	//MERT (4) -- ISABELLE (2) -- AARON (0) -- TERRENCE (1) -- ALEX (3)
	//RANDOM PLAYER (9) -- MIA (8) -- DAHLIA (7) -- WINNIE (5) -- KIRIN (6) -- RANDOM TEAM (10)
	public void checkLocation (int player, int playerPosition)
	{
		int position = playerPosition;
		if (playerPosition > 0 && playerPosition < 10) {
			position = playerPosition - 1;
		}

		if (player == 1) {
			if (playerPosition > 0 && playerPosition < 10) {
				if (!t1.Contains (players [position].SearchName)) {
					player1Text.text = string.Format ("{0}" + '\n' + "{1}",
						players [position].FirstName.ToUpper (), players [position].PlayerClass.ToUpper ());
					player1Sprite.texture = players [position].SpriteAppearance;
					adjustSpriteBasedOnScale (player1Sprite);
					p1Info.text = players [position].SelectInfo;
					player1Sprite.color = Color.white;
				} else {
					player1Text.text = "";
					p1Info.text = "";
					player1Sprite.texture = players [position].SpriteAppearance;
					adjustSpriteBasedOnScale (player1Sprite);
					player1Sprite.color = Color.grey;
				}
			} else {
				player1Text.text = "???";
				p1Info.text = "";
				player1Sprite.texture = Resources.Load <Texture2D> (@"Textures/Blank");
			}
		}

		if (player == 2) {
			if (playerPosition > 0 && playerPosition < 10) {
				print (position + " " + players.Length);
				if (!t2.Contains (players [position].SearchName)) {
					player2Text.text = string.Format("{0}" + '\n' + "{1}",
						players [position].FirstName.ToUpper (), players [position].PlayerClass.ToUpper());
					player2Sprite.texture = players [position].SpriteAppearance;
					adjustSpriteBasedOnScale (player2Sprite);
					p2Info.text = players [position].SelectInfo;
					player2Sprite.color = Color.white;
				} else {
					player2Text.text = "";
					p2Info.text = "";
					player2Sprite.texture = players [position].SpriteAppearance;
					adjustSpriteBasedOnScale (player2Sprite);
					player2Sprite.color = Color.grey;
				}
			} else {
				player2Text.text = "???";
				p2Info.text = "";
				player2Sprite.texture = Resources.Load <Texture2D> (@"Textures/Blank");
			}
		}
	}

	void changeLocation (int player, string direction)
	{
		if (player == 1) {
			if (direction.Equals ("left")) {

				if (player1On > 0) {
					player1Selecting [player1On - 1] = false;

					//if (player1On == player2On) {
					//	player2SelectBoxBig.enabled = true;
					//	player2SelectBoxSmall.enabled = true;
					//}

					player1On--;
					player1SelectBoxBig.transform.position = new Vector3 (player1SelectBoxBig.transform.position.x - 100,
						player1SelectBoxBig.transform.position.y, 0);
					player1SelectBoxSmall.transform.position = player1SelectBoxBig.transform.position;
					if (player2On == player1On) {
						player1SelectBoxBig.enabled = false;
						player1SelectBoxSmall.enabled = true;
					} else {
						player1SelectBoxBig.enabled = true;
						player1SelectBoxSmall.enabled = false;
					}
					checkLocation (1, player1On);
				}
			}

			else if (direction.Equals ("right")) {
				if (player1On < 10) {
					player1Selecting [player1On] = false;

					player1On++;
					player1SelectBoxBig.transform.position = new Vector3 (player1SelectBoxBig.transform.position.x + 100,
						player1SelectBoxBig.transform.position.y, 0);
					player1SelectBoxSmall.transform.position = player1SelectBoxBig.transform.position;
					if (player2On == player1On) {
						player1SelectBoxBig.enabled = false;
						player1SelectBoxSmall.enabled = true;
					} else {
						player1SelectBoxBig.enabled = true;
						player1SelectBoxSmall.enabled = false;
					}
					checkLocation (1, player1On);
				}
			}
		}
		else if (player == 2) {
			if (direction.Equals ("left")) {

				if (player2On > 0) {
					player2Selecting [player2On] = false;

					player2On--;
					player2SelectBoxBig.transform.position = new Vector3 (player2SelectBoxBig.transform.position.x - 100,
						player2SelectBoxBig.transform.position.y, 0);
					player2SelectBoxSmall.transform.position = player2SelectBoxBig.transform.position;
					if (player1On == player2On) {
						player2SelectBoxBig.enabled = false;
						player2SelectBoxSmall.enabled = true;
					} else {
						player2SelectBoxBig.enabled = true;
						player2SelectBoxSmall.enabled = false;
					}
					checkLocation (2, player2On);
				}
			}

			else if (direction.Equals ("right")) {
				if (player2On < 10) {
					player2Selecting [player2On] = false;

					player2On++;
					player2SelectBoxBig.transform.position = new Vector3 (player2SelectBoxBig.transform.position.x + 100,
						player2SelectBoxBig.transform.position.y, 0);
					player2SelectBoxSmall.transform.position = player2SelectBoxBig.transform.position;
					if (player1On == player2On) {
						player2SelectBoxBig.enabled = false;
						player2SelectBoxSmall.enabled = true;
					} else {
						player2SelectBoxBig.enabled = true;
						player2SelectBoxSmall.enabled = false;
					}
					checkLocation (2, player2On);
				}
			}
		}
	}
}

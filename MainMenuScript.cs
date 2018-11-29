using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;

public class MainMenuScript : MonoBehaviour
{

	public Text versusText;
	public Text chessText;
	public Text practiceText;
	public Text optionsText;
	public Text quitText;
	public RawImage backgroundImage;
	public AudioSource selectSFX;
	public AudioSource bgm;
	public AudioClip menuMove;
	public AudioClip menuSelect;

	public bool[] itemHighlighted;
	public bool yesHighlighted;

	public Color highlightAvailableColor;
	public Color highlightUnavailableColor;
	public Color nonHighlightAvailableColor;
	public Color nonHighlightUnavailableColor;

	private static float MAX_VALUE = 0.4f;
	public float moveDelay;
	public float resetDelay;

	// Use this for initialization
	void Start ()
	{

		//Editor.DeletePlayerPrefs ();

		Time.timeScale = 1.0f;
		moveDelay = 0.0f;
		resetDelay = 0.0f;

		versusText = versusText.GetComponent<Text> ();
		chessText = chessText.GetComponent<Text> ();
		practiceText = practiceText.GetComponent<Text> ();
		optionsText = optionsText.GetComponent<Text> ();
		quitText = quitText.GetComponent<Text> ();

		//highlightAvailableColor = new Color (0, 200, 0);
		//nonHighlightAvailableColor = new Color (0, 0, 0);

		yesHighlighted = false;

		//highlightUnavailableColor = highlightAvailableColor;/**new Color (
		//	highlightAvailableColor.r, highlightAvailableColor.g - 50, highlightAvailableColor.b);*/
		//nonHighlightUnavailableColor = nonHighlightAvailableColor;/**new Color (
		//	nonHighlightAvailableColor.r, nonHighlightAvailableColor.g - 50, nonHighlightAvailableColor.b);*/

		versusText.color = highlightAvailableColor;
		chessText.color = nonHighlightAvailableColor;
		practiceText.color = nonHighlightAvailableColor;
		optionsText.color = nonHighlightUnavailableColor;
		quitText.color = nonHighlightAvailableColor;

		//CAM (0) - VER (3)
		//ARC (1) - CHS (4)
		//PRA (2) - OPT (5)
		//    QUIT (6)
		itemHighlighted = new bool[5];
		for (int i = 0; i < itemHighlighted.Length; i++) {
			itemHighlighted [i] = false;
		}
		itemHighlighted [0] = true;

		selectSFX = selectSFX.GetComponent<AudioSource> ();
		bgm = bgm.GetComponent<AudioSource> ();
	}

	void Awake ()
	{
		if (FindObjectsOfType<AudioSource> ().Length > 3) {
			//Destroy (FindObjectsOfType<AudioSource>()[FindObjectsOfType<AudioSource> ().Length - 1]);
			Destroy (FindObjectsOfType<AudioSource> () [0]);
		}
	}

	void Update ()
	{


		if (moveDelay <= 0.0f && resetDelay <= 0.0f) {

			//MOVE DOWN
			if (Input.GetKey (KeyCode.DownArrow)) {
				
				selectSFX.clip = menuMove;
				selectSFX.Play ();
				moveDelay = MAX_VALUE;

				if (itemHighlighted [0]) {
					itemHighlighted [0] = false;
					itemHighlighted [1] = true;
					versusText.color = nonHighlightAvailableColor;
					chessText.color = highlightAvailableColor;
				} else if (itemHighlighted [1]) {
					itemHighlighted [1] = false;
					itemHighlighted [2] = true;
					chessText.color = nonHighlightAvailableColor;
					practiceText.color = highlightAvailableColor;
				} else if (itemHighlighted [2]) {
					itemHighlighted [2] = false;
					itemHighlighted [3] = true;
					practiceText.color = nonHighlightAvailableColor;
					optionsText.color = highlightUnavailableColor;
				} else if (itemHighlighted [3]) {
					itemHighlighted [3] = false;
					itemHighlighted [4] = true;
					optionsText.color = nonHighlightUnavailableColor;
					quitText.color = highlightAvailableColor;
				}
			}

			//MOVE UP
			else if (Input.GetKey (KeyCode.UpArrow)) {

				selectSFX.clip = menuMove;
				selectSFX.Play ();
				moveDelay = MAX_VALUE;

				if (itemHighlighted [1]) {
					itemHighlighted [1] = false;
					itemHighlighted [0] = true;
					versusText.color = highlightAvailableColor;
					chessText.color = nonHighlightAvailableColor;
				} else if (itemHighlighted [2]) {
					itemHighlighted [2] = false;
					itemHighlighted [1] = true;
					chessText.color = highlightAvailableColor;
					practiceText.color = nonHighlightAvailableColor;
				} else if (itemHighlighted [3]) {
					itemHighlighted [3] = false;
					itemHighlighted [2] = true;
					practiceText.color = highlightAvailableColor;
					optionsText.color = nonHighlightUnavailableColor;
				} else if (itemHighlighted [4]) {
					itemHighlighted [4] = false;
					itemHighlighted [3] = true;
					optionsText.color = highlightUnavailableColor;
					quitText.color = nonHighlightAvailableColor;
				}
			}
		}


		//CAM (0) - VER (3)
		//ARC (1) - CHS (4)
		//PRA (2) - OPT (5)
		//    QUIT (6)
		if (Input.GetKey (KeyCode.Return)) {
			if (itemHighlighted [0] || itemHighlighted [1] || itemHighlighted [2]) {
				StreamWriter wrt = new StreamWriter (@"Assets/Resources/Data/Current/Mode.txt");

				selectSFX.clip = menuSelect;
				selectSFX.Play ();

				//this.enabled = false;

				if (itemHighlighted [0]) {
					wrt.WriteLine ("versus");
				} else if (itemHighlighted [1]) {
					wrt.WriteLine ("chess");
				} else if (itemHighlighted [2]) {
					wrt.WriteLine ("training");
				}
				wrt.Close ();
				SceneManager.LoadScene (1);
			} else if (itemHighlighted [4]) {
				selectSFX.Play ();
				moveDelay = MAX_VALUE;
				QuitPress ();
			}
		} else if (Input.GetKey (KeyCode.Q)) {
			SceneManager.LoadScene (3);
			this.enabled = false;
		}


		if (resetDelay > 0.0f) {
			resetDelay -= Time.deltaTime;
			//print ("Reset delay: " + resetDelay);
		}
		if (moveDelay > 0.0f) {
			moveDelay -= Time.deltaTime;
			//print ("Move delay: " + moveDelay);
		}
	}

	public void QuitPress ()
	{
		Application.Quit ();

		/**
		introText.enabled = false;
		teamMatchText.enabled = false;
		onlineChessText.enabled = false;
		backgroundImage.enabled = false;
		*/
	}

	public void StartTeamMatch ()
	{
		this.enabled = false;
		//Screen.SetResolution (Screen.currentResolution.height, Screen.currentResolution.width, false);
		SceneManager.LoadScene (1);
	}

	public void StartPracticeMatch ()
	{
		Application.OpenURL ("http://www.youtube.com");
	}
}

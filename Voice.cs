using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;

public class Voice: MonoBehaviour
{
	private string[] intros;
	private string[][] specificIntros;

	private string[] taunts;

	private string[] victories;
	private string[][] specificVictories;

	private string[] criticals;

	private string[] defeats;

	public string[] finalVictories;
	public string[][] specificFinalVictories;

	private string[] finalDefeats;
	private string[][] specificFinalDefeats;

	public string[] appreciations;
	public string[][] specificAppreciations;

	public AudioSource contactSound;
	public AudioSource speech;
	public RawImage dialogueBubble;
	public Text dialogueText;
	public ArrayList dialogue;

	public GameObject voiceObj;

	private float speechCountdown;
	private float speechTimer;

	public Voice (string[] intr, string[][] specInt, string[] taunt, string[] vict, string[][] specVict,
		string[] crits, string[] def, string[] finalVic, string[][] specFinalVics, string[] finalDefs, string[][] specFinalDefs,
		string[] appr, string[][] specAppr)
	{
		speechTimer = 0.0f;
		intros = intr;
		specificIntros = specInt;

		taunts = taunt;

		victories = vict;
		specificVictories = specVict;

		criticals = crits;

		defeats = def;

		finalVictories = finalVic;
		specificFinalVictories = specFinalVics;

		finalDefeats = finalDefs;
		specificFinalDefeats = specFinalDefs;

		appreciations = appr;
		specificAppreciations = specAppr;

		dialogue = new ArrayList ();

		contactSound = new AudioSource ();
		Speech = new AudioSource ();
		voiceObj = new GameObject ();
	}

	public float SpeechCountdown
	{
		get {
			return speechCountdown;
		}
		set {
			speechCountdown = value;
		}
	}

	public float SpeechTimer
	{
		get {
			return speechTimer;
		}
		set {
			SpeechTimer = value;
		}
	}

	public void Clear ()
	{
		if (DialogueText != null) {
			DialogueText.text = "";
			DialogueText.enabled = false;
		}
		SpeechCountdown = -5.0f;

	}

	public RawImage DialogueBubble
	{
		get {
			return dialogueBubble;
		}
		set {
			dialogueBubble = value;
		}
	}

	public Text DialogueText
	{
		get {
			return dialogueText;
		}
		set {
			dialogueText = value;
		}
	}

	public ArrayList Dialogue
	{
		get {
			return dialogue;
		}
		set {
			dialogue = value;
		}
	}

	public AudioSource ContactSound
	{
		get {
			return contactSound;
		}
		set {
			contactSound = value;
		}
	}

	public AudioSource Speech
	{
		get {
			return speech;
		}
		set {
			speech = value;
		}
	}

	public string[] Intros
	{
		get { return intros;}
		set { intros = value;}
	}

	public string[][] SpecificIntros
	{
		get { return specificIntros;}
		set { specificIntros = value;}
	}

	public string[] Taunts
	{
		get { return taunts;}
		set { taunts = value;}
	}

	public string[] Victories
	{
		get { return victories;}
		set { victories = value;}
	}

	public string[][] SpecificVictories
	{
		get { return specificVictories;}
		set { specificVictories = value;}
	}

	public string[] Criticals
	{
		get { return criticals;}
		set { criticals = value;}
	}

	public string[] Defeats
	{
		get { return defeats;}
		set { defeats = value;}
	}

	public string[] FinalVictories
	{
		get { return finalVictories;}
		set { finalVictories = value;}
	}

	public string[][] SpecificFinalVictories
	{
		get { return specificFinalVictories;}
		set { specificFinalVictories = value;}
	}

	public string[] FinalDefeats
	{
		get { return finalDefeats;}
		set { finalDefeats = value;}
	}

	public string[][] SpecificFinalDefeats
	{
		get { return specificFinalDefeats;}
		set { specificFinalDefeats = value;}
	}

	public string[] Appreciations
	{
		get { return appreciations;}
		set { appreciations = value;}
	}

	public string[][] SpecificAppreciations
	{
		get { return specificAppreciations;}
		set { specificAppreciations = value;}
	}

	public string RandomIntro
	{
		get {
			System.Random r = new System.Random();
			int i = r.Next (intros.Length);
			return intros[i];
		}
	}

	public string SpecificIntro (Team t)
	{
		for (int i = 0; i < SpecificIntros.Length; i++) {

			if (t.Contains (SpecificIntros [i] [0])) {
				return SpecificIntros [i] [1];
			}
		}
		return RandomIntro;
	}

	public string SpecificIntro (Player p) {
		for (int i = 0; i < SpecificIntros.Length; i++) {
			if (SpecificIntros [i] [0].Equals (p.SearchName)) {
				return SpecificIntros [i] [1];
			}
		}
		return RandomIntro;
	}

	public string RandomTaunt
	{
		get {
			System.Random r = new System.Random();
			int i = r.Next (taunts.Length);
			return taunts[i];
		}
	}

	public string RandomVictory
	{
		get {
			System.Random r = new System.Random();
			int i = r.Next (victories.Length);
			return victories[i];
		}
	}

	public string SpecificVictory (string name)
	{
		string search;
		for (int i = 0; i < SpecificVictories.Length; i++) {
			if (name.Equals(SpecificVictories[i][0])) {
				return SpecificVictories [i] [1];
			}
		}
		return RandomVictory;
	}

	public string RandomCritical
	{
		get {
			System.Random r = new System.Random();
			int i = r.Next (criticals.Length);
			return criticals[i];
		}
	}

	public string RandomDefeat
	{
		get {
			System.Random r = new System.Random();
			int i = r.Next (defeats.Length);
			return defeats[i];
		}
	}

	public string RandomFinalVictory
	{
		get {
			System.Random r = new System.Random();
			int i = r.Next (finalVictories.Length);
			return finalVictories[i];
		}
	}

	public string SpecificFinalVictory (Team t)
	{
		for (int i = 0; i < SpecificVictories.Length; i++) {

			if (t.Contains (SpecificVictories [i] [0])) {
				return SpecificVictories [i] [1];
			}
		}
		return RandomFinalVictory;
	}

	public string SpecificFinalVictory (Player p) {
		for (int i = 0; i < SpecificFinalVictories.Length; i++) {
			if (SpecificFinalVictories [i] [0].Equals (p.SearchName)) {
				return SpecificFinalVictories [i] [1];
			}
		}
		return RandomFinalVictory;
	}

	public string RandomFinalDefeat
	{
		get {
			System.Random r = new System.Random();
			int i = r.Next (finalDefeats.Length);
			return finalDefeats[i];
		}
	}

	public string SpecificFinalDefeat (Team t)
	{
		for (int i = 0; i < SpecificFinalDefeats.Length; i++) {

			if (t.Contains (SpecificFinalDefeats [i] [0])) {
				return SpecificFinalDefeats [i] [1];
			}
		}
		return RandomFinalDefeat;
	}

	public string RandomAppreciation
	{
		get {
			System.Random r = new System.Random();
			int i = r.Next (appreciations.Length);
			return appreciations[i];
		}
	}

	public string SpecificAppreciation (string name)
	{
		for (int i = 0; i < SpecificAppreciations.Length; i++) {

			if (name.Equals(SpecificAppreciations [i] [0])) {
				return SpecificAppreciations [i] [1];
			}
		}
		return RandomAppreciation;
	}

	/**
		public int getIndexFromNumber (string[][] list, string name)
		{
			for (int i = 0; i < list.Length; i++) {
				if (list [i][0].Equals (name)) {
					return i;
				}
			}
			return -1;
		}
		*/
}





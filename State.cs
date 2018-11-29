using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Diagnostics;

public class State
{
	private string name;
	private string abbreviation;
	private double probability;
	private double doublePotency;
	private int numTurns;
	private int potency;
	//private ArrayList additionalStates;
	private State additionalStates;
	private Boolean malicious;
	private string phrase;
	private Color color;
	private int index;

	public State (string nm, string abb, int pot, double dPot, int turns, double prob, Boolean mal, string phr)
	{
		name = nm;
		abbreviation = abb;
		numTurns = turns;
		probability = prob;
		malicious = mal;
		phrase = phr;
		if (pot == 0) {
			potency = 0;
			doublePotency = dPot;
		} else {
			potency = pot;
			doublePotency = 0.0;
		}
		//additionalStates = new ArrayList ();
		additionalStates = null;
		color = Color.black;
		index = 0;
	}

	public Color StateColor
	{
		get { return color;}
		set { color = value;}
	}

	public int Index
	{
		get {
			return index;
		}
		set {
			index = value;
		}
	}

	public Boolean Malicious
	{
		get {
			return malicious;
		}
		set {
			malicious = value;
		}
	}

	public string Phrase
	{
		get {
			return  phrase;
		}
		set {
			phrase = value;
		}
	}

	public void Clear ()
	{
		AdditionalStates = null;
	}


	public State AdditionalStates
	{
		get { return additionalStates;}
		set { additionalStates = value;}
	}

	public void Add (State s)
	{
		if (AdditionalStates != null) {
			AdditionalStates.Potency += s.Potency;
			AdditionalStates.DoublePotency += s.DoublePotency;
			AdditionalStates.NumTurns += s.NumTurns;
			AdditionalStates.Probability = s.Probability;
		} else {
			AdditionalStates = new State (Name, Abbreviation, s.Potency, s.DoublePotency, s.NumTurns, s.Probability, s.Malicious, s.phrase);
		}
	}

	public State CloneState
	{
		get {
			return new State (
				Name, Abbreviation, Potency, DoublePotency, NumTurns, Probability, Malicious, Phrase);
		}
	}

	public string IncrementVariants
	{
		get {
			string output = "";
			if (AdditionalStates != null && AdditionalStates.NumTurns > 0) {
				AdditionalStates.NumTurns--;
				if (AdditionalStates.NumTurns == 0) {
					if (Name.Equals ("Counter")) {
						output += string.Format ("Regular stance resumes. ");
					} else if (Name.Equals ("Parry")) {
						output += string.Format ("Physical tension decreases. ");
					} else if (Name.Equals ("Poison")) {
						output += string.Format ("Poison is expelled from body. ");
					} else if (Name.Equals ("Regen")) {
						output += string.Format ("Regeneration ends. ");
					} else if (Name.Equals ("Daze")) {
						output += string.Format ("Head is cleared. ");
					} else if (Name.Equals ("Confuse")) {
						output += string.Format ("Confusion clears. ");
					} else if (Name.Equals ("Sadness")) {
						output += string.Format ("Sad feelings pass. ");
					} else if (Name.Equals ("Fury")) {
						output += string.Format ("Senses overcomes. ");
					} else if (Name.Equals ("Sleep")) {
						output += string.Format ("Wake-up time! ");
					} else if (Name.Equals ("Adle")) {
						output += string.Format ("Memories return. ");
					} else if (Name.Equals ("Freeze")) {
						output += string.Format ("Feeling returns to the legs. ");
					} else if (Name.Equals ("Immune")) {
						output += string.Format ("Immunity wears out. ");
					} else if (Name.Equals ("Invulnerable")) {
						output += string.Format ("Invulnerability wears out. ");
					} else if (Name.Equals ("Burn")) {
						output += string.Format ("Fires die out. ");
					} else if (Name.Equals ("Learn")) {
						output += string.Format ("Keen eye dies out. ");
					} else if (Name.Equals ("Invisible")) {
						output += string.Format ("Invisibility fades. ");
					} else {
						output += string.Format ("{0} alteration ends. ", AdditionalStates.Name);
					}
					additionalStates = null;
				}
			}
			return output;
		}
	}

	public string ClearStates
	{
		get {
			string output = "";
			if (IsActive) {
				//output += string.Format ("{0} ends. ", Name);
				if (Name.Equals ("Counter")) {
					output += string.Format ("Regular stance resumes. ");
				} else if (Name.Equals ("Parry")) {
					output += string.Format ("Physical tension decreases. ");
				} else if (Name.Equals ("Poison")) {
					output += string.Format ("Poison is expelled from body. ");
				} else if (Name.Equals ("Regen")) {
					output += string.Format ("Regeneration ends. ");
				} else if (Name.Equals ("Daze")) {
					output += string.Format ("Head is cleared. ");
				} else if (Name.Equals ("Confuse")) {
					output += string.Format ("Confusion clears. ");
				} else if (Name.Equals ("Sadness")) {
					output += string.Format ("Sad feelings pass. ");
				} else if (Name.Equals ("Fury")) {
					output += string.Format ("Senses overcomes. ");
				} else if (Name.Equals ("Sleep")) {
					output += string.Format ("Wake-up time! ");
				} else if (Name.Equals ("Adle")) {
					output += string.Format ("Memories return. ");
				} else if (Name.Equals ("Freeze")) {
					output += string.Format ("Feeling returns to the legs. ");
				} else if (Name.Equals ("Immune")) {
					output += string.Format ("Immunity wears out. ");
				} else if (Name.Equals ("Invulnerable")) {
					output += string.Format ("Invulnerability wears out. ");
				} else if (Name.Equals ("Burn")) {
					output += string.Format ("Fires die out. ");
				} else if (Name.Equals ("Learn")) {
					output += string.Format ("Keen eye dies out. ");
				} else if (Name.Equals ("Invisible")) {
					output += string.Format ("Invisibility fades. ");
				} else {
					output += string.Format ("{0} alteration ends. ", Name);
				}
			}
			if (AdditionalStates != null) {
				AdditionalStates = null;
			}
			Potency = 0;
			NumTurns = 0;
			Probability = 0.0;
			return output;
		}
	}

	public Boolean IsActive
	{
		get { return NumTurns != 0 || Potency != 0;}
	}

	public string Name
	{
		get { return name;}
		set { name = value;}
	}

	public string Abbreviation
	{
		get { return abbreviation;}
		set { abbreviation = value;}
	}

	public int Potency
	{
		get {

			int pot = potency;
			if (AdditionalStates != null) {
				pot += AdditionalStates.Potency;
			}
			return pot;
		}
		set { potency = value;}
	}

	public int NumTurns
	{
		get {
			int num = numTurns;
			if (AdditionalStates != null) {
				num += AdditionalStates.NumTurns;
			}
			return num;
		}
		set { numTurns = value;}
	}

	public double Probability
	{
		get {
			return probability;
		}
		set { probability = value;}
	}

	public double DoublePotency
	{
		get {
			double pot = doublePotency;
			if (AdditionalStates != null) {
				pot += AdditionalStates.DoublePotency;
			}
			return pot;
		}
		set { doublePotency = value;}
	}

	public Boolean Ailment
	{
		get {
			return name.Equals ("Poison") || name.Equals ("Daze") || name.Equals ("Confuse")
				|| name.Equals ("Sadness") || name.Equals ("Sleep") || name.Equals ("Adle") 
				|| name.Equals ("Freeze") || name.Equals ("Burn") || name.Equals ("Blind");
			
		}
	}

	public override string ToString ()
	{
		return string.Format ("{0} ({1}), P: {2} DP: {3} NT: {4} PR: {5}",
			Name, Abbreviation, Potency, DoublePotency, NumTurns, 100 * Probability);
	}

    public string StringRep ()
    {
        string output = @"";
        output += Potency + @"/" + DoublePotency + @"#" + NumTurns + @":" + Probability;
        if (AdditionalStates != null && AdditionalStates.Probability > 0) {
            output += @"" + '\n' + AdditionalStates.StringRep ();
        }
        return output;
    }
}





using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Diagnostics;

public class DynamicInt
{
	private string name;
	private string abbreviation;
	private int baseValue;
	private int holder;
	private int levelStat;
	private ArrayList variants;

	public DynamicInt (string nm, string ab, int val)
	{
		name = nm;
		abbreviation = ab;
		baseValue = val;
		variants = new ArrayList ();
		holder = 0;
		levelStat = 0;
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

	public int Holder
	{
		get { return holder;}
		set { holder = value;}
	}

	public int VariantCount
	{
		get {
			return variants.Count;
		}
	}

	public void addVariant (State variant)
	{
		variants.Add (new State(variant.Name, variant.Abbreviation, variant.Potency,
			variant.DoublePotency, variant.NumTurns, variant.Probability, variant.Malicious, variant.Phrase));
	}

	public string IncrementVariants
	{
		get {
			string output = "";
			State s;
			for (int i = 0; i < variants.Count; i++) {
				s = (State)variants [i];
				s.NumTurns--;
				if (s.NumTurns <= 0) {
					output += string.Format ("{0} alteration ends. ", s.Name);
					variants.Remove (variants [i]);
					i--;
				}
			}
			return output;
		}
	}

	public int BaseValue
	{
		get {
			return baseValue + levelStat;
		}
		set {
			baseValue = value;
		}
	}

	public int LevelStat
	{
		get {
			return levelStat;
		}
		set {
			levelStat = value;
		}
	}

	public int RealChange
	{
		get {
			int realValue = 0;
			for (int i = 0; i < variants.Count; i++) {
				realValue += ((State)variants [i]).Potency;
			}
			return realValue;
		}
	}

	public string RealChangeDisplay
	{
		get {
			if (RealChange > 0) {
				return "+" + RealChange;
			}
			return "" + RealChange;
		}
	}


	public int RealValue
	{
		get {
			int realValue = BaseValue;
			for (int i = 0; i < variants.Count; i++) {
				realValue += ((State)variants [i]).Potency;
			}
			return realValue;
		}
	}

	public string ClearVariants
	{
		get {
			string output = "";
			if (IsActive) {
				output += string.Format("{0} variation ends. ", Name);
			}
			variants.Clear ();
			return output;
		}
	}

	public Boolean IsActive
	{
		get {
			return variants.Count > 0;
		}
	}

	public override string ToString()
	{
		string output = "";
		output += string.Format("{0}: {1} ", Name, RealValue);
		if (RealValue > BaseValue)
		{
			output += string.Format("({0} +{1}) ", BaseValue, RealChange);
		}
		else if (RealValue < BaseValue)
		{
			output += string.Format("({0} {1}) ", BaseValue, RealChange);
		}
		for (int i = 0; i < variants.Count; i++)
		{
			output += "*" + ((State)variants[i]).ToString();
		}
		return output;
	}

    public string StringRep ()
    {
        string output = @"";
        State s;
        output += Name + @":" + BaseValue + '\n';
        output += @"Variants:" + variants.Count;
        for (int i = 0; i < variants.Count; i++)
        {
            s = (State)variants[i];
            output += @"" + '\n' + s.Potency + @"," + s.NumTurns;
        }
        return output;
    }
}



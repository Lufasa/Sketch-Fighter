using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Diagnostics;

public class DynamicDouble
{

	private string name;
	private string abbreviation;
	private double baseValue;
	private double holder;
	private double levelStat;
	private ArrayList variants; 

	public DynamicDouble (string nm, string ab, double val)
	{
		name = nm;
		abbreviation = ab;
		baseValue = val;
		variants = new ArrayList ();
		holder = 0;
		levelStat = 0.0;
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

	public double Holder
	{
		get { return holder;}
		set { holder = value;}
	}

	public int VariantCount
	{
		get { return variants.Count;}
	}

	public void addVariant (State variant)
	{
		variants.Add (variant);
	}

	public string IncrementVariants
	{
		get {
			string output = "";
			State s;
			for (int i = 0; i < variants.Count; i++) {
				s = (State)variants [i];
				s.NumTurns--;
				if (s.NumTurns <= 0.0) {
					output += string.Format ("{0} alteration ends. ", s.Name);
					variants.Remove (variants [i]);
					i--;
				}
			}
			return output;
		}
	}

	public double BaseValue
	{
		get {
			return baseValue + levelStat;
		}
		set {
			baseValue = value;
		}
	}

	public double RealChange
	{
		get {
			double realValue = 0;
			for (int i = 0; i < variants.Count; i++) {
				realValue += ((State)variants [i]).DoublePotency;
			}
			return realValue;
		}
	}

	public double LevelStat
	{
		get {
			return levelStat;
		}
		set {
			levelStat = value;
		}
	}

	public string RealChangeDisplay
	{
		get {
			if (RealChange > 0) {
				return "+" + (int)(RealChange * 100);
			}
			return "" + (int)(RealChange * 100);
		}
	}

	public double RealValue
	{
		get {
			double realValue = BaseValue;
			for (int i = 0; i < variants.Count; i++) {
				realValue += ((State)variants [i]).DoublePotency;
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

    public override string ToString ()
	{
		string output = "";
		output += string.Format("{0}: {1} ", Name, RealValue);
		if (RealValue > BaseValue) {
			output += string.Format("({0} +{1}) ", BaseValue, RealChange);
		} else if (RealValue < BaseValue) {
			output += string.Format("({0} {1}) ", BaseValue, RealChange);
		}
		for (int i = 0; i < variants.Count; i ++) {
			output += "*" + ((State)variants [i]).ToString ();
		}
		return output;
	}

    public string StringRep ()
    {
        string output = @"";
        State s;
        output += Name + @":" + BaseValue + '\n';
        output += @"Variants:" + variants.Count;
        for (int i = 0; i < variants.Count; i ++) {
            s = (State)variants [i];
            output += @"" + '\n' + s.Potency + @"," + s.NumTurns;
        }
        return output;
    }

	public string ToPercent ()
	{
		string output = "";
		output += string.Format("{0}: {1}% ", Name, (int)(100 * RealValue));
		if (RealValue > BaseValue) {
			output += string.Format("({0} +{1})", BaseValue, RealChange);
		} else if (RealValue < BaseValue) {
			output += string.Format("({0} {1})", BaseValue, RealChange);
		}
		return output;
	}

}






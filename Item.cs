using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Diagnostics;

public class Item
{
	private string name;
	private string specialConditions;
	private string searchName;
	private string description;
	private string areaOfEffect;
	private double[] statAugments;
	private int[] stateAugments;
	private int[] elementalAugments;
	private int[] resistances;
	private ArrayList availableSkills;
	private int numUses;
	private int maxUses;
	private Boolean canDuplicate;

	public Item (string nm, string conditions, string sName, string desc, string area, double[] stat, int[] state, int[] elem, int[] res, int uses, Boolean canDupe)
	{
		name = nm;
		searchName = sName;
		areaOfEffect = area;
		specialConditions = conditions;
		description = desc;
		statAugments = stat;
		stateAugments = state;
		elementalAugments = elem;
		resistances = res;
		numUses = uses;
		maxUses = uses;
		canDuplicate = canDupe;

		availableSkills = new ArrayList ();
	}

    public string BasicInfo
    {
        get {
            string output = "";
            output += searchName + "-" + numUses;
            return output;
        }

    }

	public string Name
	{
		get { return name;}
		set { name = value;}
	}

	public string SpecialConditions
	{
		get { return specialConditions;}
		set { specialConditions = value;}
	}

	public string SearchName
	{
		get { return searchName;}
		set { searchName = value;}

	}

	public string Description
	{
		get { return description;}
		set { description = value;}
	}

	public Boolean CanDuplicate
	{
		get { return canDuplicate;}
		set { canDuplicate = value;}
	}

	public int NumUses
	{
		get { return numUses;}
		set { numUses = value;}
	}

	public int MaxUses
	{
		get { return maxUses;}
		set { maxUses = value;}
	}

	public string AreaOfEffect
	{
		get { return areaOfEffect;}
		set { areaOfEffect = value;}
	}

	public double[] StatAugments
	{
		get { return statAugments;}
		set { statAugments = value;}
	}

	public int[] StateAugments
	{
		get { return stateAugments;}
		set { stateAugments = value;}
	}

	public int[] ElementalAugments
	{
		get { return elementalAugments;}
		set { elementalAugments = value;}
	}

	public int[] Resistances
	{
		get { return resistances;}
		set { resistances = value;}
	}

	public ArrayList AvailableSkills
	{
		get { return availableSkills;}
		set { availableSkills = value;}
	}

	public override string ToString ()
	{
		string output = "";
		output += string.Format ("{0}: {1} ", AreaOfEffect.Substring (0, 3).ToUpper (), Name.ToUpper ());
		if (MaxUses > 0) {
			output += string.Format ("{0}/{1}", NumUses, MaxUses);
		}
		/**
			if (AvailableSkills.Count > 0) {
				
				for (int j = 0; j < AvailableSkills.Count; j++) {
					output += string.Format (" " + '\t' + "-{0}", ((Skill)AvailableSkills[j]).Name);
				}
			}
			*/
		return output;
	}

	/**
		 * 
		 * 		private string areaOfEffect;
				private double[] statAugments;
				private int[] stateAugments;
				private int[] elementalAugments;
				private int[] resistances;
				private Skill[] availableSkills;
		 * 
		 * 
		 */ 
}



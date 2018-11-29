using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Diagnostics;

public class Meter
{

	private string name;
	private string currency;
	private int meterLevel;
    private int meterLevelAppearance;
	private int meterMax;
	private int meterShift;
	private DynamicInt meterMaxVariance;
	private DynamicInt meterShiftVariance;
	private Boolean canBeUsed;

	public Meter (string nm, string cur, int mx, int shft)
	{
		name = nm;
		currency = cur;
		meterLevel = mx;
        meterLevelAppearance = meterLevel;
		meterMax = mx;
		meterShift = shft;
		meterMaxVariance = new DynamicInt ("Meter Max Change", "MMC", 0);
		meterShiftVariance = new DynamicInt ("Meter Shift Variance", "MSV", 0);
		canBeUsed = true;
	}

	public Meter (string nm, string cur, int lvl, int mx, int shft)
	{
		name = nm;
		currency = cur;
		meterLevel = lvl;
        meterLevelAppearance = meterLevel;
		meterMax = mx;
		meterShift = shft;
		meterMaxVariance = new DynamicInt ("Meter Max Change", "MMC", 0);
		meterShiftVariance = new DynamicInt ("Meter Shift Variance", "MSV", 0);
		canBeUsed = true;
	}

	public Boolean CanBeUsed
	{
		get {
			return canBeUsed;
		}
		set {
			canBeUsed = value;
		}
	}

	public string Name
	{
		get { return name;}
		set { name = value;}
	}

	public string Currency
	{
		get { return currency;}
		set { currency = value;}
	}

	public int MeterLevel
	{
		get { return meterLevel;}
		set { meterLevel = value;}
	}

    public int MeterLevelAppearance
    {
        get { return meterLevelAppearance; }
        set { meterLevelAppearance = value; }
    }

	public int MeterMax
	{
		get { return meterMax + meterMaxVariance.RealValue;}
		set { meterMax = value;}
	}

	public int MeterShift
	{
		get { return meterShift + meterShiftVariance.RealValue;}
		set { meterShift = value;}
	}

	public DynamicInt MeterMaxVariance
	{
		get { return meterMaxVariance;}
		set { meterMaxVariance = value;}
	}

	public DynamicInt MeterShiftVariance
	{
		get { return meterShiftVariance;}
		set { meterShiftVariance = value;}
	}

    public void adjust (int inc)
    {
        if (meterLevelAppearance > meterLevel)
        {
            while (meterLevelAppearance - inc < meterLevel)
            {
                inc--;
            }
            inc *= -1;
        }
        else if (meterLevelAppearance < meterLevel)
        {
            while (meterLevelAppearance + inc > meterLevel)
            {
                inc--;
            }
        }

        if (meterLevelAppearance != meterLevel)
        { meterLevelAppearance += inc; }
    }

	public void addToMeter (int amt)
	{
		MeterLevel += amt;
		if (MeterLevel > MeterMax) {
			MeterLevel = MeterMax;
		}
		if (MeterLevel < 0) {
			MeterLevel = 0;
		}
	}

	public float MeterRatio
	{
		get {
			if (MeterMax > 0 && CanBeUsed)
				return ((float)MeterLevel) / ((float)MeterMax);
			return 0;
		}
	}

    public float MeterRatioAppearance
    {
        get
        {
            if (MeterMax > 0 && CanBeUsed)
            {
                return ((float)MeterLevelAppearance) / ((float)MeterMax);
            }
            return 0;
        }
    }

    public string StringRep()
    {
        return MeterLevel + @"/" + MeterMax + @":" + MeterShift;

    }

	public override string ToString()
	{
		return string.Format("{0}: {1}/{2} {3} ({4} {3})", Name, MeterLevel, MeterMax, Currency, MeterShift);
	}

	public string MeterInfo
	{
		get {
			return string.Format ("{0}: {1}/{2} {3}", Name, MeterLevel, MeterMax, Currency
			);
		}
	}

	public string BasicInfo
	{
		get {
			return string.Format ("{0}: {1}/{2} {3} {4}", new object[]{Name, MeterLevel, MeterMax, Currency, MeterShiftDisplay });
		}
	}

	public string MeterShiftDisplay
	{
		get {
			if (MeterShift >= 0) {
				return "+" + MeterShift + " " + Currency;
			}
			return MeterShift + " " + Currency;
		}

	}

	public string meterShiftDisplay (double ratio)
	{
		if (MeterShift >= 0) {
			return "+" + MeterShift * ratio + " " + Currency;
		}
		return MeterShift * ratio + " " + Currency;
	}

	public string ToString (double ratio)
	{
		return string.Format("{0}: {1}/{2} {3} {4}", new object[]{Name, MeterLevel, MeterMax, Currency, meterShiftDisplay (ratio)});
	}

	public string CurrentState
	{
		get {
			return string.Format ("{0}/{1} {2}", MeterLevel, MeterMax, Currency);
		}
	}
}



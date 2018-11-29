using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class Skill

{
	private int index;
	private string name;
	private string searchName;
	private string description;
	private string properties;
	private string[] conditions;
	private string expendableAmmo;
	private string reloadAmmo;
	private double ratio;
	private double hitRate;
	private int lengthCheck;
	private int range;
	private int motionRange;
	private State force;
	private int widthOrRadius;
	private int reps;
	private Boolean[] links;
	private int threshold;
	private int thresholdMeasure;
	private int numUses;
	private int maxUses;
	private int recovery;
	private int timer;
	private string[] inputs;
	private Boolean hasConnected;
	private Boolean defeatedTarget;
	private string defeatedName;
	private int hitStun;
	private ArrayList whiffSkills;
	private int statBuffs;
	private int statNerfs;
	private int statusBuffs;
	private int statusNerfs;
	private Boolean hasDropped;
	private Map currentMap;
	private int[] masteryCheckpoints;
	private int turnsRemaining;
    private int maxTurnsRemaining;
	private int itemsPerUse;
	private int powerIndex;
	private Boolean nonstop;
	private Location dedicatedLocation;
    private Reel animation;
    private ArrayList translatedInputs;

	private Player owner;
	private Player originalOwner;
	//private Map skillMap;

	private Boolean malicious;
	private Boolean beneficial;
	private Boolean noConditions;

	private Boolean selfSkill;
	private Boolean singleSkill;
	private Boolean lineSkill;
	private Boolean radiusSkill;
	private Boolean coneSkill;
	private Boolean objectSkill;

	private Boolean allies;
	private Boolean enemies;
	private Boolean self;

	private Boolean otg;
	private Boolean mid;
	private Boolean high;
	private Boolean air;
	private Boolean highAir;
	private Boolean equalLevel;
	private Boolean auto;

	private Boolean low;
	private Boolean overhead;
	private Boolean physical;
	private Boolean projectile;
	private Boolean superProjectile;
	private Boolean sForce;
	private Boolean grapple;
	private Boolean magic;
	private Boolean movement;
	private Boolean weapon;
	private Boolean targetless;
	private Boolean onWhiff;
	private Boolean multihit;
	private Boolean continuous;
	private Boolean endLink;
	private Boolean individual;
	private Boolean noCombo;
	private Boolean permeate;
	private Boolean actionChange;
	private Boolean claw;           
	private Boolean used;
	private Boolean once;
	private Boolean jumpIn;
	private Boolean learnedInField;

	private Boolean canAct;
	private Boolean canFreemove;
	private Boolean maintainMovements;
	private Boolean addLink;
	private Boolean combo;

	private ArrayList checks;

	private Texture2D playerTexture;
	//private Texture2D reversePlayerTexture;

	private Boolean jumpReset;
	private Boolean moveReset;
	private Boolean neutral;
	private Boolean myTurn;

	private Boolean benign;

	private Boolean usedNow;

	//private Boolean reload;
	//private Boolean expend;

	private int[] cost;
	private int[] change;

	private State[] statAlterations;
	private State[] stateAlterations;
	private ArrayList extraStateAlterations;
	private State[] elementals;
	private DynamicDouble[] stateResistances;
	private int animationFrames;

	private Boolean canBeLearned;
	private Boolean canBeLearnedBase;
	private Boolean canBeLearnedSpecial;

	private ArrayList locations;

	private Skill cancelSkill;
	private ArrayList linkSkills;
    private Skill masterSkill;
    private Skill counterSkill;
	private Skill timerSkill;
	private Skill trapSkill;
    private Skill dropItem;
	private ArrayList learnSkills;
	private ArrayList forgetSkills;

	private Location location;

	private string[] phrases;
	private string[][] specificPhrases;

	private int currentHeight;
	private int lastHeight;

	private ArrayList hitTargets;
	private ArrayList allTargets;


	private Boolean cloned;

	private string direction;
	private int currentRange;
	private int maxRange;

	private Location trackLocation;
	private int rowDifference;
	private int columnDifference;
	private int skillOutput;
	private string skillInfoList;
	private int speed;
	private int animations;
	private float activeTimeModulus;
	private float activeMatchTime;

	private int experience;
	private int mastery;

	private string directory;

	private Texture2D texture;

	public Texture2D mapTexture ()
	{	return texture;}

	public Skill ()
	{

	}

	public Skill (string nm, string sch, string desc, string prop, string type, string[] cond,
		string[] phr, string[][] spec, int rng, int spd, int hts, int mot, State frc, int wrd, double rat, double htr,
		int rep, Boolean[] link, int thr, int uses, int rec, int[] cst, int[] chng, State[] stat,
		State[] state, ArrayList xState, State[] elem, DynamicDouble[] alts, ArrayList lnks, Skill ctr,
		Skill timed, int tim, Skill trap, Skill drop, ArrayList lrn, ArrayList forg, ArrayList whf, string[] inp, Skill mst)
	{
        translatedInputs = new ArrayList ();
		checks = new ArrayList ();
		playerTexture = null;
		//reversePlayerTexture = null;

		locations = new ArrayList ();
		linkSkills = new ArrayList ();
		cancelSkill = null;
		dropItem = drop;
		whiffSkills = whf;

		experience = 0;
		mastery = 1;

		activeTimeModulus = 0.0f;
		activeMatchTime = 0.0f;

		animationFrames = 1;
		powerIndex = 0;

		combo = true;
		checks.Add (combo);

		index = 0;
		name = nm;
		searchName = sch;
		description = desc;
		properties = prop;

		originalOwner = null;

		conditions = cond;
		phrases = phr;
		specificPhrases = spec;
		range = rng;
		widthOrRadius = wrd;
		ratio = rat;
		hitRate = htr;
		reps = rep;
		links = link;
		linkSkills = lnks;
		counterSkill = ctr;
		timerSkill = timed;
		timer = tim;
		learnSkills = lrn;
		forgetSkills = forg;
		speed = spd;
		hitStun = hts;

		cost = cst;
		change = chng;
		elementals = elem;
		statAlterations = stat;
		stateAlterations = state;
		extraStateAlterations = xState;
		stateResistances = alts;

		cloned = false;

		owner = null;

		range = rng;
		widthOrRadius = wrd;
		force = frc;
		motionRange = mot;

		ratio = rat;
		hitRate = htr;

		reps = rep;
		threshold = thr;
		thresholdMeasure = thr;
		numUses = uses;
		maxUses = uses;

		masteryCheckpoints = new int[12];
		setMasteryCheckpoints ();

		//skillMap = null;

		recovery = rec;
		timer = tim;

		hitTargets = new ArrayList ();
		allTargets = new ArrayList ();

		setType (type);
		setSkillInfoList ();
		trapSkill = trap;
		location = null;
		currentHeight = 0;
		lastHeight = 0;
		direction = "X";
		currentRange = 0;
		MaxRange = 0;
		location = null;

		used = false;

		inputs = inp;
		masterSkill = mst;

		hasConnected = false;

		trackLocation = null;
		rowDifference = -1;
		columnDifference = -1;
		defeatedTarget = false;
		defeatedName = "";
		skillOutput = 0;
		turnsRemaining = -1;
        maxTurnsRemaining = -1;

		hasDropped = false;

		if (Properties.Contains (" MAP ") || type.Contains (" MAP ")) {
			texture = Resources.Load<Texture2D> (@"Textures/" + searchName);
		}

		dedicatedLocation = null;
	}

    public ArrayList TranslatedInputs
    {
        get {
            return translatedInputs;
        }
        set {
            translatedInputs = value;
        }
    }

    public Reel Animation
    {
        get {
            return animation;
        }
        set {
            animation = value;
        }
    }

	public Location DedicatedLocation
	{
		get
		{
			return dedicatedLocation;
		}
		set
		{
			dedicatedLocation = value;
		}
	}

	public int TurnsRemaining
	{
		get {
			return turnsRemaining;
		}
		set {
			turnsRemaining = value;
		}
	}

    public int MaxTurnsRemaining
    {
        get
        {
            return maxTurnsRemaining;
        }
        set
        {
            maxTurnsRemaining = value;
        }
    }

	public Boolean HardAttack
	{
		get {
			return Malicious && (((cost [1] <= -30 && Properties.Contains ("NORMAL ")) || (cost [1] <= -20 && Properties.Contains ("SPECIAL ")))
				|| Properties.Contains ("VITALITY") || Properties.Contains ("BURST"));
		}
	}

	public Boolean LightAttack
	{
		get {
			return Malicious && ((Properties.Contains ("NORMAL ") && (cost [1] <= -10 && cost [1] > -30)) || (Properties.Contains ("SPECIAL ") && cost [1] > -10));
		}
	}

	public Boolean HasDropped
	{
		get {
			return hasDropped;
		}
		set {
			hasDropped = value;
		}
	}

	public float StepTimeModulus
	{
		get {
			return activeTimeModulus;
		}
		set {
			activeTimeModulus = value;
		}
	}

	public float StepTime
	{
		get {
			return activeMatchTime;
		}
		set {
			activeMatchTime = value;
		}
	}


	public int AnimationFrames
	{
		get {
			return animationFrames;
		}
		set {
			animationFrames = value;
		}
	}

	public Boolean Used
	{
		get {
			return used;
		}
		set {
			used = value;
		}
	}

	public ArrayList WhiffSkills
	{
		get {
			return whiffSkills;
		}
		set {
			whiffSkills = value;
		}
	}

	public int HitStun
	{
		get {
			return hitStun;
		}
		set {
			hitStun = value;
		}
	}

	public Boolean UsedNow
	{
		get {
			return (usedNow && (!objectSkill || TrapSkill.UsedNow)) || Properties.Contains (" USEDNOW ");
		}
		set {
			usedNow = value;
			if (objectSkill) {
				TrapSkill.UsedNow = false;
			}
		}
	}

	public int Speed
	{
		get {
			return speed;
		}
		set {
			speed = value;
		}
	}

	public int SkillOutput
	{
		get {
			return skillOutput;
		}
		set {
			skillOutput = value;
		}
	}

	public Boolean DefeatedTarget
	{
		get {
			return defeatedTarget;
		}
		set {
			defeatedTarget = value;
		}
	}

	public string DefeatedName
	{
		get {
			return defeatedName;
		}
		set {
			defeatedName = value;
		}
	}

	public int RowDifference
	{
		get {
			return rowDifference;
		}
		set {
			rowDifference = value;
		}
	}

	public int ColumnDifference
	{
		get {
			return columnDifference;
		}
		set {
			columnDifference = value;
		}
	}


	public Boolean HasConnected
	{
		get {
			return hasConnected;
		}
		set {
			hasConnected = value;
		}
	}

	public Skill MasterSkill
	{
		get {
			return masterSkill;
		}
		set {
			masterSkill = value;
		}
	}

	public string[] Inputs
	{
		get {
			return inputs;
		}
		set {
			inputs = value;
		}
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

	public string Direction
	{
		get {
			return direction;
		}
		set {
			direction = value;
		}
	}

	public Location Location
	{
		get {
			return location;
		}
		set {
			location = value;
		}
	}

	public Location TrackLocation
	{
		get {
			return trackLocation;
		}
		set {
			trackLocation = value;
		}
	}

	public Boolean MotionSkill
	{
		get {
			return MotionRange != 0
				|| Properties.Contains(" MOVE")
				|| Properties.Contains("LOCKON")
				|| Properties.Contains ("LEAP")
				|| Properties.Contains ("TELEPORT")
				|| (SelfSkill && StateAlterations [2].Potency > 0);
		}
	}

	public string SkillInfoList
	{
		get {
			return skillInfoList;
		}
		set {
			skillInfoList = value;
		}
	}

	private void setSkillInfoList()
	{
		SkillInfoList = "";
		if (properties.Contains("VITALITY"))
		{
			skillInfoList += "VITALITY ";
		}
		if (properties.Contains("BURST"))
		{
			skillInfoList += "BURST ";
		}

		if (Malicious)
		{
			skillInfoList += "MALICIOUS ";
		}
		if (Beneficial)
		{
			skillInfoList += "BENEFICIAL ";
		}
		if (SelfSkill)
		{
			skillInfoList += "SELF ";
		}
		else if (SingleSkill)
		{
			skillInfoList += "SINGLE ";
		}
		else if (LineSkill)
		{
			skillInfoList += "LINE ";
		}
		else if (RadiusSkill)
		{
			skillInfoList += "RADIUS ";
		}
		else if (ConeSkill)
		{
			skillInfoList += "CONE ";
		}

		if (JumpIn)
		{
			skillInfoList += "JUMPIN";
		}

		if (Claw)
		{
			skillInfoList += "CLAW ";
		}
		if (Once)
		{
			skillInfoList += "ONCE ";
		}

		if (ObjectSkill)
		{
			skillInfoList += "OBJECT ";
		}
		if (Enemies)
		{
			skillInfoList += "ENEMY ";
		}
		if (Allies)
		{
			skillInfoList += "ALLY ";
		}
		if (Self)
		{
			skillInfoList += "OWN ";
		}
		if (Enemies && Allies && Self)
		{
			skillInfoList += "EVERYONE ";
		}

		if (OTG && Mid && High && Air && HighAir)
		{
			skillInfoList += "ALLSECTIONS ";
		}
		if (EqualLevel)
		{
			skillInfoList += "SAMESECTION ";
		}
		if (Auto)
		{
			skillInfoList += "AUTO ";
		}
		if (OTG)
		{
			skillInfoList += "OTG ";
		}
		if (Mid)
		{
			skillInfoList += "MID ";
		}
		if (High)
		{
			skillInfoList += "HIGH ";
		}
		if (Air)
		{
			skillInfoList += "AIR ";
		}
		if (HighAir)
		{
			skillInfoList += "HIGHAIR ";
		}
		if (Low)
		{
			skillInfoList += "LOW ";
		}
		if (Overhead)
		{
			skillInfoList += "OVERHEAD ";
		}

		if (Grapple)
		{
			skillInfoList += "GRAPPLE ";
		}
		if (Weapon)
		{
			skillInfoList += "WEAPON ";
		}
		if (Projectile)
		{
			skillInfoList += "PROJECTILE ";
		}
		if (SForce)
		{
			skillInfoList += "FORCE ";
		}
		if (SuperProjectile)
		{
			skillInfoList += "PROJECTILE-S ";
		}
		if (Physical)
		{
			skillInfoList += "PHYSICAL ";
		}
		if (Magic)
		{
			skillInfoList += "MAGIC ";
		}
		if (Movement)
		{
			skillInfoList += "MOVEMENT ";
		}
		if (Nonstop)
		{
			skillInfoList += "NONSTOP ";
		}

		if (CanBeLearned)
		{
			canBeLearned = true;
			//skillInfoList += "LEARN";
			if (CanBeLearnedSpecial)
			{
				skillInfoList += "LEARNS ";
			}
			if (CanBeLearnedBase)
			{
				skillInfoList += "LEARNB ";
			}
			if (!CanBeLearnedBase && !CanBeLearnedSpecial)
			{
				skillInfoList += "LEARN ";
			}
		}
		if (AddLink)
		{
			skillInfoList += "ADDLINK ";
		}
		if (Targetless)
		{
			skillInfoList += "TGLESS ";
		}
		if (OnWhiff)
		{
			skillInfoList += "ONWHIFF ";
		}
		if (Multihit)
		{
			skillInfoList += "MULTIHIT ";
		}
		if (CanAct)
		{
			skillInfoList += "CANACT ";
		}
		if (CanFreeMove)
		{
			skillInfoList += "FREEMOVE ";
		}
		if (MaintainMovements)
		{
			skillInfoList += "MAINTAIN ";
		}
		if (Continuous)
		{
			skillInfoList += "CONTINUOUS ";
		}
		if (EndLink)
		{
			skillInfoList += "ENDLINK ";
		}
		if (ExpendableAmmo != "N/A")
		{
			skillInfoList += "AMMO ";
		}

		if (Individual)
		{
			skillInfoList += "INDIVIDUAL ";
		}

		if (NoCombo)
		{
			skillInfoList += "NOCOMBO ";
		}

		if (ReloadAmmo != "N/A")
		{
			skillInfoList += "RELOAD ";
		}

		if (Permeate)
		{
			skillInfoList += "PERMEATE ";
		}

		if (!ActionChange)
		{
			skillInfoList += "NOCHANGE ";
		}

		if (Benign)
		{
			skillInfoList += "BENIGN ";
		}

		if (JumpReset)
		{
			skillInfoList += "JMPRST ";
		}

		if (MoveReset)
		{
			skillInfoList += "MOVRST ";
		}

		if (Neutral)
		{
			skillInfoList += "NEUTRAL ";
		}
		if (MyTurn)
		{
			skillInfoList += "MYTURN ";
		}
	}

	private void setType (string type)
	{

		statBuffs = 0;
		statNerfs = 0;
		statusBuffs = 0;
		statusNerfs = 0;
		powerIndex = 0;
		State st;

		for (int i = 0; i < StateAlterations.Length; i++) {
			if (StateAlterations[i].Probability != 0) {
				if (i <= 3 && i >= 6) {
					if (StateAlterations [i].Ailment) {
						statusNerfs++;
					} else {
						statusBuffs++;
					}
				}
			}
		}

		if (properties.Contains ("SACTION ")) {
			powerIndex = 6;
		}
		if (properties.Contains ("NORMAL ")) {
			powerIndex = 5;
		}
		if (properties.Contains ("SPECIAL ")) {
			powerIndex = 4;
		}
		if (properties.Contains ("VITALITY ")) {
			powerIndex = 3;
		}
		if (properties.Contains ("BURST ")) {
			powerIndex = 2;
		}

        if (properties.Contains ("TURNS"))
        {
            turnsRemaining = NumberConverter.ConvertToInt (properties.Substring (properties.IndexOf ('&') + 1, 3));
            maxTurnsRemaining = turnsRemaining;
        }

		for (int i = 0; i < StatAlterations.Length; i++) {

			if (StatAlterations [i] == null) {
			//	throw new NullReferenceException ("NAH " + Name + @" " + @" " + i);
			}

			if (StatAlterations[i].Probability != 0.0) {
				if (StatAlterations [i].Potency < 0 || StatAlterations [i].DoublePotency < 0) {	
					statNerfs++;
				} else {
					statBuffs++;
				}
			}
		}

		//checks = new ArrayList() { malicious, beneficial, learnedInField, selfSkill, singleSkill, lineSkill, radiusSkill, coneSkill,
		//	objectSkill, noConditions, allies, enemies, self, auto, otg, mid, high, air, highAir, low, overhead, targetless, onWhiff,
		//	multihit, canAct, canFreemove, maintainMovements, physical, grapple, magic, movement, weapon, projectile, superProjectile};

		malicious = false;
		beneficial = false;
		learnedInField = false;

		selfSkill = false;
		singleSkill = false;
		lineSkill = false;
		radiusSkill = false;
		coneSkill = false;
		objectSkill = false;
		noConditions = false;

		allies = false;
		enemies = false;
		self = false;

		auto = false;
		otg = false;
		mid = false;
		high = false;
		air = false;
		highAir = false;
		equalLevel = false;

		low = false;
		overhead = false;

		targetless = false;
		onWhiff = false;
		multihit = false;
		canAct = false;
		canFreemove = false;
		maintainMovements = false;

		physical = false;
		grapple = false;
		magic = false;
		movement = false;
		weapon = false;
		projectile = false;
		superProjectile = false;
		sForce = false;
		permeate = false;
		claw = false;
		once = false;

		continuous = false;
		individual = false;
		nonstop = false;

		canBeLearned = false;
		canBeLearnedBase = false;
		canBeLearnedSpecial = false;

		jumpReset = false;
		moveReset = false;

		addLink = false;
		noCombo = false;

		actionChange = true;
		benign = false;

		neutral = false;
		myTurn = false;
		jumpIn = false;

		expendableAmmo = "N/A";
		reloadAmmo = "N/A";

		if (type.Contains ("MALICIOUS")) {
			malicious = true;
		} if (type.Contains ("BENEFICIAL")) {
			beneficial = true;
		} if (type.Contains ("SELF")) {
			selfSkill = true;
		} else if (type.Contains ("SINGLE")) {
			singleSkill = true;
		} else if (type.Contains ("LINE")) {
			lineSkill = true;
		} else if (type.Contains ("RADIUS")) {
			radiusSkill = true;
		} else if (type.Contains ("CONE")) {
			coneSkill = true;
		}

		if ((properties.Contains ("NORM ") || properties.Contains ("NORM2 ") || properties.Contains ("NORM3 ")) && type.Contains ("ADDLINK ")) {
			jumpIn = true;
		}

		if (type.Contains ("OBJECT")) {
			objectSkill = true;
		}
		if (type.Contains ("ENEMY")) {
			enemies = true;
		} if (type.Contains ("ALLY")) {
			allies = true;
		} if (type.Contains ("OWN")) {
			self = true;
		} if (type.Contains ("EVERYONE")) {
			enemies = true;
			allies = true;
			self = true;
		}

		if (type.Contains (" EQUALLEVEL "))
		{
			equalLevel = true;
		}
		if (type.Contains ("ALLSECTIONS")) {
			otg = true;
			mid = true;
			high = true;
			air = true;
			highAir = true;
		} if (type.Contains ("AUTO")) {
			auto = true;
			otg = true;
			mid = true;
			high = true;
			air = true;
			highAir = true;
		} if (type.Contains (" OTG ")) {
			otg = true;
		} if (type.Contains (" MID ")) {
			mid = true;
		} if (type.Contains (" HIGH ")) {
			high = true;
		} if (type.Contains (" AIR ")) {
			air = true;
		} if (type.Contains (" HIGHAIR ")) {
			highAir = true;
		}  if (type.Contains (" LOW ")) {
			low = true;
		}  if (type.Contains (" OVERHEAD ")) {
			overhead = true;
		}

		if (type.Contains ("GRAPPLE ")) {
			grapple = true;
		} if (type.Contains ("WEAPON ")) {
			weapon = true;
		} if (type.Contains ("PROJECTILE ")) {
			projectile = true;
		} if (type.Contains ("FORCE ")) {
			sForce = true;
		} if (type.Contains ("PROJECTILE-S")) {
			superProjectile = true;
		} if (type.Contains ("PHYSICAL")) {
			physical = true;
		} if (type.Contains ("MAGIC")) {
			magic = true;
		} if (type.Contains ("MOVEMENT")) {
			movement = true;
		} if (type.Contains ("CLAW")) {
			claw = true;
		}

		if (type.Contains ("LEARN")) {
			canBeLearned = true;
			if (type.Contains ("LEARNB")) {
				canBeLearnedBase = true;
			} if (type.Contains ("LEARNS")) {
				canBeLearnedSpecial = true;
			}
		}
		/**
		if (type.Contains ("∆")) {
			HitStunRequired = NumberConverter.ConvertToInt (type.Substring (type.IndexOf ('∆') + 1, 1));
		}
		if (type.Contains ("∂")) {
			HitStun = NumberConverter.ConvertToInt (type.Substring (type.IndexOf ('∂') + 1, 1));
		}
		*/
		if (type.Contains ("ADDLINK")) {
			addLink = true;
		} if (type.Contains ("TGLESS")) {
			targetless = true;
		} if (type.Contains ("ONWHIFF")) {
			onWhiff = true;
		} if (type.Contains ("MULTIHIT")) {
			multihit = true;
		} if (type.Contains ("CANACT")) {
			canAct = true;
		} if (type.Contains ("FREEMOVE")) {
			canFreemove = true;
		} if (type.Contains ("MAINTAIN")) {
			maintainMovements = true;
		} if (type.Contains ("CONTINUOUS")) {
			continuous = true;
			skillInfoList += "CONTINUOUS ";
		} if (type.Contains ("NONSTOP")) {
			nonstop = true;
		} if (type.Contains ("ENDLINK")) {
			endLink = true;
		} if (type.Contains ("AMMO:")) {
			expendableAmmo = type.Substring (type.IndexOf (':') + 1, type.IndexOf ('.') - 5);
			itemsPerUse = 1;

		} if (type.Contains ("MULT:")) {

			if (type.IndexOf ('-') < 0) {
				throw new NullReferenceException (type.IndexOf ('-') + @" is below zero, smart one! (" + type + @")");
			}

			string ammos = type.Substring (0, type.IndexOf ('-'));

			expendableAmmo = ammos.Substring (ammos.IndexOf ('#') + 1);
			itemsPerUse = NumberConverter.ConvertToInt (ammos.Substring (ammos.IndexOf (':') + 1, 2));
		}

		if (type.Contains ("INDIVIDUAL")) {
			individual = true;
		} if (type.Contains ("NOCOMBO")) {
			noCombo = true;
		} if (type.Contains (" RELOAD") || properties.Contains (" CHARGE")) {
			reloadAmmo = type.Substring (type.IndexOf ('$') + 1);
		} if (type.Contains ("PERMEATE")) {
			permeate = true;
		}

		if (type.Contains ("NOCHANGE ")) {
			actionChange = false;
		} if (type.Contains ("BENIGN")) {
			benign = true;
		}

		if (type.Contains ("JMPRST") || properties.Contains("JMPRST")) {
			jumpReset = true;
		}

		if (type.Contains ("MOVRST") || properties.Contains("MOVRST")) {
			moveReset = true;
		}

		if (type.Contains ("NEUTRAL")) {
			neutral = true;
		} if (type.Contains ("MYTURN")) {
			myTurn = true;
		}
		if (type.Contains ("ONCE")) {
			once = true;
		}
		if (type.Contains ("NOCOND ")) {
			noConditions = true;
		}
	}

	public Boolean Cloned
	{
		get {
			return cloned;
		}
		set {
			cloned = value;
		}
	}

	public Skill CloneSkill
	{
		get {
			Skill cloneSkill = new Skill (Name, SearchName, Description, Properties, "", Conditions,
				Phrases, SpecificPhrases, Range, Speed, HitStun, MotionRange, Force, WidthOrRadius, Ratio, HitRate,
				Reps, Links, Threshold, MaxUses, Recovery, Cost, Change, StatAlterations, StateAlterations,
				ExtraStateAlterations, Elementals, StateResistances, LinkSkills, CounterSkill, TimerSkill, Timer,
				TrapSkill, DropItem, LearnSkills, ForgetSkills, WhiffSkills, Inputs, MasterSkill);

			cloneSkill.LinkSkills = new ArrayList ();

			cloneSkill.cloned = true;

			for (int i = 0; i < LinkSkills.Count; i++) {
				cloneSkill.LinkSkills.Add (((Skill)LinkSkills [i]).CloneSkill);
			}

			if (CounterSkill != null) {
				cloneSkill.CounterSkill = CounterSkill.CloneSkill;
			}

			if (TimerSkill != null) {
				cloneSkill.TimerSkill = TimerSkill.CloneSkill;
			}

			if (TrapSkill != null) {
				cloneSkill.TrapSkill = TrapSkill.CloneSkill;
			}

			cloneSkill.LearnSkills = new ArrayList ();
			for (int i = 0; i < LearnSkills.Count; i++) {
				cloneSkill.LearnSkills.Add (((Skill)LearnSkills [i]).CloneSkill);
			}

			//if (MasterSkill != null) {
			//	cloneSkill.MasterSkill = MasterSkill.CloneSkill;
			//}
			cloneSkill.Owner = Owner;
			cloneSkill.OriginalOwner = OriginalOwner;
			cloneSkill.Malicious = Malicious;
			cloneSkill.Beneficial = Beneficial;
			cloneSkill.SelfSkill = SelfSkill;
			cloneSkill.SingleSkill = SingleSkill;
			cloneSkill.LineSkill = LineSkill;
			cloneSkill.RadiusSkill = RadiusSkill;
			cloneSkill.ConeSkill = ConeSkill;
			cloneSkill.ObjectSkill = ObjectSkill;
			cloneSkill.Physical = Physical;
			cloneSkill.Allies = Allies;
			cloneSkill.Enemies = Enemies;
			cloneSkill.Self = Self;
			cloneSkill.Auto = Auto;
			cloneSkill.OTG = OTG;
			cloneSkill.EqualLevel = EqualLevel;
			cloneSkill.Mid = Mid;
			cloneSkill.High = High;
			cloneSkill.Air = Air;
			cloneSkill.HighAir = HighAir;
			cloneSkill.Low = Low;
			cloneSkill.Overhead = Overhead;
			cloneSkill.Targetless = Targetless;
			cloneSkill.Permeate = Permeate;
			cloneSkill.OnWhiff = OnWhiff;
			cloneSkill.Multihit = Multihit;
			cloneSkill.EndLink = EndLink;
			cloneSkill.CanAct = CanAct;
			cloneSkill.CanFreeMove = CanFreeMove;
			cloneSkill.MaintainMovements = MaintainMovements;
			cloneSkill.Grapple = Grapple;
			cloneSkill.Magic = Magic;
			cloneSkill.Claw = Claw;
			cloneSkill.Movement = Movement;
			cloneSkill.Weapon = Weapon;
			cloneSkill.Projectile = Projectile;
			cloneSkill.SuperProjectile = SuperProjectile;
			cloneSkill.SForce = SForce;
			cloneSkill.Permeate = Permeate;
			cloneSkill.Continuous = Continuous;
			cloneSkill.Individual = Individual;
			cloneSkill.CanBeLearned = CanBeLearned;
			cloneSkill.CanBeLearnedBase = CanBeLearnedBase;
			cloneSkill.CanBeLearnedSpecial = CanBeLearnedSpecial;
			cloneSkill.JumpReset = JumpReset;
			cloneSkill.MoveReset = MoveReset;
			cloneSkill.AddLink = AddLink;
			cloneSkill.NoCombo = NoCombo;
			cloneSkill.ActionChange = ActionChange;
			cloneSkill.Benign = Benign;
			cloneSkill.Neutral = Neutral;
			cloneSkill.JumpIn = JumpIn;
			cloneSkill.ReloadAmmo = ReloadAmmo;
			cloneSkill.ExpendableAmmo = ExpendableAmmo;
			cloneSkill.ItemsPerUse = ItemsPerUse;
			cloneSkill.CurrentHeight = CurrentHeight;
			cloneSkill.LastHeight = LastHeight;
			cloneSkill.MyTurn = MyTurn;
			cloneSkill.NoConditions = NoConditions;
			cloneSkill.setSkillInfoList ();
			cloneSkill.Nonstop = Nonstop;
			cloneSkill.PowerIndex = PowerIndex;
			cloneSkill.PlayerTexture = PlayerTexture;
			//cloneSkill.ReversePlayerTexture = ReversePlayerTexture;


			return cloneSkill;
		}
	}

	public Boolean Nonstop
	{
		get { return nonstop;}
		set { nonstop = value;}
	}

	public Boolean LearnedInField
	{
		get { return learnedInField;}
		set { learnedInField = value;}
	}

	public Boolean NoConditions
	{
		get { return noConditions;}
		set { noConditions = value;}
	}


	public double sweatdropAnimeEmoji ()
	{
		return 0.0;
	}

	public Boolean JumpIn
	{
		get { return jumpIn;}
		set { jumpIn = value;}
	}

	public Boolean Once
	{
		get { return once;}
		set { once = value;}
	}

	public Boolean Standalone
	{
		get {return !Properties.Contains ("CHILD") && !Properties.Contains ("MASTER") && !Properties.Contains ("FINISH");}
	}

	public Boolean sameSkillSet (Skill s)
	{
		if (s.MasterSkill == null) {
			return SearchName.Equals (s.SearchName);
		}
		return sameSkillSet (s.MasterSkill);
	}

	/**
	public Boolean sameSkillSet (string s)
	{
		if (MasterSkill == null) {
			return isSame (s);
		}
		//return MasterSkill//sameSkillSet (MasterSkill);
	}
	*/


	public Boolean IsMasterSkill
	{
		get { return Properties.Contains ("MASTER ");}
	}

	public Boolean MyTurn
	{
		get { return myTurn;}
		set { myTurn = value;}
	}


	public Boolean Benign
	{
		get { return benign;}
		set { benign = value;}
	}

	public Boolean Neutral
	{
		get { return neutral;}
		set { neutral = value;}
	}

	public Boolean Magic
	{
		get { return magic;}
		set { magic = value;}
	}

	public Boolean Claw
	{
		get { return claw;}
		set { claw = value;}
	}

	public Boolean Movement
	{
		get { return movement;}
		set { movement = value;}
	}

	public Boolean ActionChange
	{
		get { return actionChange;}
		set { actionChange = value;}
	}

	public Boolean AddLink
	{
		get { return addLink;}
		set { addLink = value;}
	}

	public Skill DropItem
	{
		get { return dropItem;}
		set { dropItem = value;}
	}

	public Location CurrentLocation
	{
		get { return location;}
		set { location = value;}
	}

	public Skill TrapSkill
	{
		get { return trapSkill;}
		set { trapSkill = value;}
	}

	public ArrayList LearnSkills
	{
		get { return learnSkills;}
		set { learnSkills = value;}
	}

	public ArrayList ForgetSkills
	{
		get { return forgetSkills;}
		set { forgetSkills = value;}
	}

	public Boolean ObjectSkill
	{
		get { return objectSkill;}
		set { objectSkill = value;}
	}

	public int CurrentHeight
	{
		get { return currentHeight;}
		set { currentHeight = value;}
	}

	public string ExpendableAmmo
	{
		get {
			return expendableAmmo;
		}
		set {
			expendableAmmo = value;
		}
	}


	public int ItemsPerUse
	{
		get
		{
			return itemsPerUse;
		}
		set
		{
			itemsPerUse = value;
		}
	}


	public string ReloadAmmo
	{
		get {
			return reloadAmmo;
		}
		set {
			reloadAmmo = value;
		}
	}

	public Boolean HasLinks
	{
		get {
			return EndLink || LinkSkills.Count > 0;
		}
	}

	public Boolean NoCombo
	{
		get { return noCombo;}
		set { noCombo = value;}
	}

	public Boolean EndLink
	{
		get { return endLink;}
		set { endLink = value;}
	}

	public Boolean Continuous
	{
		get { return continuous;}
		set { continuous = value;}
	}

	public Boolean MaintainMovements
	{
		get { return maintainMovements;}
		set { maintainMovements = value;}
	}

	public Boolean Multihit
	{
		get { return multihit;}
		set { multihit = value;}
	}

	public Boolean CanAct
	{
		get { return canAct;}
		set { canAct = value;}
	}

	public Boolean CanFreeMove
	{
		get { return canFreemove;}
		set { canFreemove = value;}
	}

	public Boolean Targetless
	{
		get { return targetless;}
		set { targetless = value;}
	}

	public Boolean OnWhiff
	{
		get { return onWhiff;}
		set { onWhiff = value;}
	}

	public Boolean Permeate
	{
		get { return permeate;}
		set { permeate = value;}
	}

	public Boolean JumpReset
	{
		get { return jumpReset;}
		set { jumpReset = value;}
	}

	public Boolean MoveReset
	{
		get { return moveReset;}
		set { moveReset = value;}
	}

	public string Name
	{
		get { return name;}
		set { name = value;}
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

	public string GeneralDescription (int i)
	{
		string tabbed = "";
		for (int j = 0; j < i; j ++) {
			tabbed += "" + '\t';
		}
		if (MasterSkill != null) {
			tabbed += "(AFTER " + MasterSkill.Name + @") ";
		}
		return tabbed + name.ToUpper () + @": " + description + @" (" + Type + @")" + CostEffectOverview (i);
	}

	public string CostEffectOverview (int j)
	{
		string output = "", tabbed = "";
		for (int i = 0; i < j; i ++) {
			tabbed += "" + '\t';
		}
		Skill s = this;
		if (inputs.Length > 0 && inputs[0].Length > 0) {
			output += "(Input " + inputs [0] + @") ";
		} if (cost [0] != 0 || cost [1] != 0 || cost [2] != 0 || cost [3] != 0) {
			output += string.Format ("[Cost ");
			if (Cost [0] != 0) {
				output += Cost [0] + @"HP ";
			}
			if (Cost [1] != 0) {
				output += Cost [1] + @"RM ";
			}
			if (Cost [2] != 0) {
				output += Cost [2] + @"GM ";
			}
			if (Cost [3] != 0) {
				output += Cost [3] + @"VM ";
			}
			output += string.Format ("] ");
		}

		if (s.TrapSkill != null) {
			s = s.TrapSkill;
		} else if (s.TimerSkill != null) {
			s = s.TimerSkill;
		}

		if (s.Change [0] != 0 || s.Change [1] != 0 || s.Change [2] != 0 || s.Change [3] != 0) {
			output += string.Format ("[Effect ");
			if (s.Change [0] != 0) {
				output += " " + s.realOutput (owner.PlayerStandard, s.Change [0]) + @"HP";
			}
			if (change [1] != 0) {
				output += " " + s.realOutput (owner.PlayerStandard, s.Change [1]) + @"RM";
			}
			if (change [2] != 0) {
				output += " " + s.realOutput (owner.PlayerStandard, s.Change [2]) + @"GM";
			}
			if (change [3] != 0) {
				output += " " + s.realOutput (owner.PlayerStandard, s.Change [3]) + @"VM";
			}
			output += string.Format ("] ");
		}

		output += string.Format (" " + s.Speed + @" SPD, " + s.HitStun + @" HTS");

		output += string.Format (" " + '\n');

		if (s.Conditions.Length > 0) {
			output += ConditionDisplays (tabbed);
		}

		if (s.LinkSkills.Count > 0) {
			while (s.Properties.Contains ("AUTOLINK") && s.LinkSkills.Count > 0) {
				s = (Skill)s.LinkSkills [0];
			}
		}

		if (s.LinkSkills.Count > 0 && !s.ObjectSkill) {

			if (s.Properties.Contains ("PCANCEL")) {
				if (j == 0) {
					output += tabbed + @"" + '\t' + @"" + '\t' + @"*CANCEL*" + '\n';
				}
			} else {
				for (int i = 0; i < s.LinkSkills.Count; i++) {
					if (((Skill)s.LinkSkills [i]).Properties.Contains ("CHILD")) {
						output += tabbed + '\t'
							+ ((Skill)s.LinkSkills [i]).GeneralDescription (j + 1);
							//+ @"" + '\t' + @"" + '\t' + @"-" + ((Skill)s.LinkSkills [i]).GeneralDescription (j + 1);//((Skill)s.LinkSkills [i]).Name + '\n';
					}
				} if (s.Properties.Contains (" NORM ")
					|| s.Properties.Contains (" NORM2 ")
					||  s.Properties.Contains (" NORM3 ")) {
					output += tabbed + @"" +'\t' + @"" + '\t' + @"(AFTER " + s.Name + @") NORMALS" + '\n';
				} if (s.Properties.Contains (" SPEC ")
					|| s.Properties.Contains (" SPEC2 ")
					||  s.Properties.Contains (" SPEC3 ")) {
					output += tabbed + @"" +'\t' + @"" + '\t' + @"(AFTER " + s.Name + @") SPECIALS" + '\n';
				} if (s.Properties.Contains (" VITA ")
						|| s.Properties.Contains (" VITA2 ")
						||  s.Properties.Contains (" VITA3 ")) {
					output += tabbed + @"" +'\t' + @"" + '\t' + @"(AFTER " + s.Name + @") VITA-SPECIALS" + '\n';
				} if (s.Properties.Contains (" BRST ")
						|| s.Properties.Contains (" BRST2 ")
						||  s.Properties.Contains (" BRST3 ")) {
					output += tabbed + @"" +'\t' + @"" + '\t' + @"(AFTER " + s.Name + @") BURSTS" + '\n';
				}
			}
		}
		return output;
	}

	public string Properties
	{
		get { return properties;}
		set { properties = value;}
	}

	public string[] Conditions
	{
		get { return conditions;}
		set { conditions = value;}
	}

	public State[] Elementals
	{
		get { return elementals;}
		set { elementals = value;}
	}

	public DynamicDouble[] StateResistances
	{
		get { return stateResistances;}
		set { stateResistances = value;}
	}

	public double Ratio
	{
		get { return ratio;}
		set { ratio = value;}
	}

	public double HitRate
	{
		get { return hitRate;}
		set { hitRate = value;}
	}

	public int PowerIndex
	{
		get { return powerIndex;}
		set { powerIndex = value;}
	}

	public int Range
	{
		get { return range;}
		set { range = value;}
	}

	public int CurrentRange
	{
		get { return currentRange;}
		set { currentRange = value;}
	}

	public int MaxRange
	{
		get { return maxRange;}
		set { maxRange = value;}
	}

	public State Force
	{
		get { return force;}
		set { force = value;}
	}



	public int MotionRange
	{
		get { return motionRange;}
		set { motionRange = value;}
	}

	public int WidthOrRadius
	{
		get { return widthOrRadius;}
		set { widthOrRadius = value;}
	}

	public int Reps
	{
		get { return reps;}
		set { reps = value;}
	}

	public Boolean[] Links
	{
		get { return links;}
		set { links = value;}
	}

	public int Threshold
	{
		get { return threshold;}
		set { threshold = value;}
	}

	public int ThresholdMeasure
	{
		get {
			if (TrapSkill != null) {
				return TrapSkill.ThresholdMeasure; 
			}
			return thresholdMeasure;
		}
		set { thresholdMeasure = value;}
	}

	public void resetDefeat ()
	{
		defeatedTarget = false;
		defeatedName = "";
		for (int i = 0; i < LinkSkills.Count; i++) {
			((Skill)LinkSkills[i]).resetDefeat ();
		}
		if (counterSkill != null) {
			counterSkill.resetDefeat ();
		} if (timerSkill != null) {
			timerSkill.resetDefeat ();
		}
	}

	public void resetThresholdMeasure ()
	{
		if (!Properties.Contains ("MAP ")) {
			ThresholdMeasure = Threshold;
			//DefeatedTarget = false;
			for (int i = 0; i < LinkSkills.Count; i++) {
				((Skill)LinkSkills [i]).resetThresholdMeasure ();
			}
			if (counterSkill != null) {
				counterSkill.resetThresholdMeasure ();
			}
			if (timerSkill != null) {
				timerSkill.resetThresholdMeasure ();
			}
		}
	}

	public int NumUses
	{
		get { return numUses;}
		set { numUses = value;}
	}

	public int MaxUses
	{
		get { return maxUses;}
	}

	public Player OriginalOwner
	{
		get { return originalOwner;}
		set {

			if (value == null) {
				//throw new NullReferenceException ("NO NUTRITIONAL VALUE");
			}

			originalOwner = value;

			for (int i = 0; i < LinkSkills.Count; i++) {
				((Skill)LinkSkills [i]).OriginalOwner = value;
			}
			if (TrapSkill != null) {
				TrapSkill.OriginalOwner = value;
			}
			if (TimerSkill != null) {
				TimerSkill.OriginalOwner = value;
			}
			if (DropItem != null) {
				DropItem.OriginalOwner = value;
			}
			if (originalOwner != null && originalOwner.currentMap () != null) {
				setLocations ();
			}
		}
	}

	public Texture2D PlayerTexture
	{
		get {
			return playerTexture;
		}
		set {
			playerTexture = value;
		}
	}

    /**
	public Texture2D ReversePlayerTexture
	{
		get {
			return reversePlayerTexture;
		}
		set {
			reversePlayerTexture = value;
		}
	}
	*/

	public Boolean NoAnimation
	{
		get {
			return properties.Contains (" MAP ") || properties.Contains (" NOANIM ");
		}
	}

	public Player Owner
	{
		get { return owner;}
		set {

			/**
			if (value == null) {
				throw new NullReferenceException ("NO NUTRITIONAL VALUE FOR " + Name);
			}
			*/

			owner = value;


			if (owner != null) {

				if (NoAnimation) {

					playerTexture = Owner.NeutralStance;
					//reversePlayerTexture = Owner.ReverseNeutralStance;

				} else if (playerTexture == null && value != null && !owner.Name.Contains ("owner") && !owner.Name.Contains ("random") && !properties.Contains ("MAP ") && !properties.Contains ("INVENTORY")) {

					if (owner.Completed && !cloned) {// || Owner == OriginalOwner || OriginalOwner == null) {
						playerTexture = Resources.Load<Texture2D> (@"Players/" + owner.SearchName + @"/Animations/" + searchName);
						//reversePlayerTexture = Resources.Load<Texture2D> (@"Players/" + owner.SearchName + @"/Animations/" + searchName + @"Reverse");
						if (playerTexture == null) {
							throw new NullReferenceException (Owner.SearchName + @" " + searchName);
						}
		
						//reversePlayerTexture = ImageMover.flipImageHorizontally (playerTexture);

					} else if (!owner.Completed && !cloned) {
						playerTexture = Owner.NeutralStance;
						//reversePlayerTexture = Owner.ReverseNeutralStance;
					}

				}


				for (int i = 0; i < LinkSkills.Count; i++) {
					((Skill)LinkSkills [i]).Owner = value;
				}
				if (TrapSkill != null) {
					TrapSkill.Owner = value;
				}
				if (TimerSkill != null) {
					TimerSkill.Owner = value;
				}
				if (DropItem != null) {
					DropItem.Owner = value;
				}

				if (owner != null && owner.currentMap () != null) {
					setLocations ();
				}
			}

		}
	}

	public int[] Cost
	{
		get {
			if (Owner != null) {
				if (Owner.NoCost) {
					return new int[] { 0, 0, 0, cost [3] };
				}

				if (!Owner.BloodPrice && !Owner.HalfCost && !Owner.StateActive (Owner.Fury)) {
					return cost;
				}

				int[] tempCost = new int[] { cost [0], cost [1], cost [2], cost [3] };

				if (Owner.HalfCost) {
					tempCost [0] /= 2;
					tempCost [1] /= 2;
					tempCost [2] /= 2;
					//return new int[] { cost [0] / 2, cost [1] / 2, cost [2] / 2, cost [3] };
				}
				if (Owner.BloodPrice) {
					tempCost [0] = ((cost [0] + cost [1] + cost [2]) * 5) * Owner.Ratio;
					//tempCost [0] = 5;
					tempCost [1] = 0;
					tempCost [2] = 0;
					//return new int[] { (cost [0] + cost [1] + cost [2]) * 5, 0, 0, cost [3]};
				}
				if (Owner.StateActive (Owner.Fury)) {
					tempCost [1] = 0;
				}
				return tempCost;
			}
			return cost;

		}
		set { cost = value;}
	}

	public int[] Change
	{
		get { return change;}
		set { change = value;}
	}

	public Boolean Malicious
	{
		get { return malicious;}
		set { malicious = value;}
	}

	public Boolean Beneficial
	{
		get { return beneficial;}
		set { beneficial = value;}
	}

	public Boolean EqualLevel
	{
		get { return equalLevel;} set { equalLevel = value;}
	}

	public Boolean OTGOnly
	{
		get { return OTG && !Mid && !High && !Air && !HighAir;}
	}

	public Boolean MidOnly
	{
		get { return !OTG && Mid && !High && !Air && !HighAir;}
	}

	public Boolean HighOnly
	{
		get { return !OTG && !Mid && High && !Air && !HighAir;}
	}

	public Boolean AirOnly
	{
		get { return !OTG && !Mid && !High && Air && !HighAir;}
	}

	public Boolean HighAirOnly
	{
		get { return !OTG && !Mid && !High && !Air && HighAir;}
	}

	public Boolean OTG
	{
		get { return otg;}
		set { otg = value;}
	}

	public Boolean Mid
	{
		get { return mid;}
		set { mid = value;}
	}

	public Boolean High
	{
		get { return high;}
		set { high = value;}
	}

	public Boolean Air
	{
		get { return air;}
		set { air = value;}
	}

	public Boolean HighAir
	{
		get { return highAir;}
		set { highAir = value;}
	}

	public Boolean Auto
	{
		get { return auto;}
		set { auto = value;}
	}

	public Boolean Low
	{
		get { return low;}
		set { low = value;}
	}

	public Boolean Overhead
	{
		get { return overhead;}
		set { overhead = value;}
	}

	public Boolean Grapple
	{
		get { return grapple;}
		set { grapple = value;}
	}

	public Boolean Weapon
	{
		get { return weapon;}
		set { weapon = value;}
	}

	public Boolean Projectile
	{
		get { return projectile;}
		set { projectile = value;}
	}

	public Boolean SuperProjectile
	{
		get { return superProjectile;}
		set { superProjectile = value;}
	}

	public Boolean SForce
	{
		get { return sForce; }
		set { sForce = value; }
	}

	public Boolean SelfSkill
	{
		get { return selfSkill;}
		set { selfSkill = value;}
	}

	public Boolean SingleSkill
	{
		get { return singleSkill;}
		set { singleSkill = value;}
	}

	public Boolean LineSkill
	{
		get { return lineSkill;}
		set { lineSkill = value;}
	}

	public Boolean RadiusSkill
	{
		get { return radiusSkill;}
		set { radiusSkill = value;}
	}

	public Boolean ConeSkill
	{
		get { return coneSkill;}
		set { coneSkill = value;}
	}

	public Boolean Allies
	{
		get { return allies;}
		set { allies = value;}
	}

	public Boolean Enemies
	{
		get { return enemies;}
		set { enemies = value;}
	}

	public Boolean Self
	{
		get { return self;}
		set { self = value;}
	}

	public Boolean CanBeLearned
	{
		get { return canBeLearned;}
		set { canBeLearned = value;}
	}

	public Boolean canBeLearnedBy (Player p)
	{
		{ return CanBeLearned && (!p.PlayStyleActual.Equals ("B") || (!Properties.Contains (" VITALITY ") && !Properties.Contains (" SACTION ")));}
	}

	public Boolean CanBeLearnedBase
	{
		get { return canBeLearnedBase;}
		set { canBeLearnedBase = value;}
	}

	public Boolean CanBeLearnedSpecial
	{
		get { return canBeLearnedSpecial;}
		set { canBeLearnedSpecial = value;}
	}

	public Boolean Individual
	{
		get { return individual;}
		set { individual = value;}
	}

	public ArrayList Locations
	{
		get { return locations;}
		set { locations = value;}
	}

	public State[] StatAlterations
	{
		get { return statAlterations;}
		set { statAlterations = value;}
	}

	public State[] StateAlterations
	{
		get { return stateAlterations;}
		set { stateAlterations = value;}
	}

	public ArrayList ExtraStateAlterations
	{
		get { return extraStateAlterations;}
		set { extraStateAlterations = value;}
	}

	public string[] Phrases
	{
		get { return phrases;}
		set { phrases = value;}
	}

	public string[][] SpecificPhrases
	{
		get { return specificPhrases;}
		set { specificPhrases = value;}
	}

	public string AppropriatePhrase (Player p)
	{
		if (p != null) {
			string nm = p.SearchName;
			if (nm != null) {
				string search;
				for (int i = 0; i < SpecificPhrases.Length; i++) {

					search = SpecificPhrases [i] [0];
					if (nm.Equals (search)) {
						return SpecificPhrases [i] [1];
					}
				}
			}
		}
		return RandomPhrase;
	}

	public string RandomPhrase
	{
		get {
			System.Random r = new System.Random();
			int i = r.Next (Phrases.Length);
			return Phrases[i];
		}
	}

	public void LinkSkill (Skill s) {
		linkSkills.Add (s);
	}

	public ArrayList LinkSkills
	{
		get { return linkSkills;}
		set { linkSkills = value;}
	}

	public Skill CancelSkill
	{
		get { return cancelSkill;}
		set { cancelSkill = value;}
	}

	public Skill CounterSkill
	{
		get { return counterSkill;}
		set { counterSkill = value;}
	}

	public Skill TimerSkill
	{
		get { return timerSkill;}
		set { timerSkill = value;}
	}

	public int Timer
	{
		get { return timer;}
		set { timer = value;}
	}

	public int Recovery
	{
		get { return recovery;}
		set { recovery = value;}
	}

	public Boolean ClassConditionsMet
	{
		get {
			/**
			if (properties.Contains ("SACTION")) {
				if (Owner.Cooldown != 0) {
					return false;
				}
			}
			*/

			//if ((owner.PlayStyle.Equals ("A")) && properties.Contains ("SACTION")) {
			if ((owner.PlayStyle.Equals ("A")) || Name.Equals ("Pace Cancel")) {
				return true;
				//return owner.Vitality.MeterLevel >= 100;
			} if (owner.PlayStyle.Equals ("B")) {
				return (Properties.Contains ("SPECIAL") || (Cost [3] >= 0 && !Properties.Contains("VITALITY") && !Properties.Contains("BURST")) || owner.CriticalActive);
					//&& (!properties.Contains ("SACTION") || owner.Vitality.MeterLevel >= 100);
			} else if (owner.PlayStyle.Equals ("C")) {
				return //(!Properties.Contains ("SACTION") || owner.SActionEnabled && owner.Vitality.MeterLevel >= 100) &&
					!Properties.Contains ("VITALITY") && (Properties.Contains ("BURST") || Math.Abs(Cost [3]) % 300 == 0) && !Properties.Contains ("SACTION");
			}
			return true;
		}
	}

	public Boolean AdleConditionsMet
	{
		get {
			return (owner.Adle.Potency < PowerIndex) || (Properties.Contains ("MAP ") 
				             || Properties.Contains ("BASIC")
				             || Properties.Contains ("ITEM"))
				;
		}
	}

	public Boolean MovementConditionsMet
	{
		get {
			return Properties.Contains ("MAP ") 
				             || !owner.StateActive (owner.Freeze) || Properties.Contains ("MVEXTRA ") || (MotionRange == 0
				&& !Properties.Contains(" MOVE ")
				&& !Properties.Contains("LOCKON"));
		}
	}

	public Boolean StatusConditionsMet
	{
		get {
			return Properties.Contains ("MAP ")
				             || !owner.StateActive (owner.Sleep)
				|| (Properties.Contains("CRITICAL")
				|| Properties.Contains("KNOCKOUT"));
		}
	}

	public Boolean presentPositionConditionsMet (Player p)
	{
		if (p.Equals ( Owner)) {
			return Properties.Contains ("MAP ")
				|| ((!Properties.Contains (" INAIR "))
				    || Owner.StateActive (Owner.Airborne));
		}
		return true;
	}

	public Boolean PositionConditionsMet
	{
		get {
			return (!owner.StateActive (owner.Grounded) || Properties.Contains ("REMAIN ")) &&
				(!owner.StateActive (owner.Airborne)
				|| Properties.Contains ("CRITICAL ")
				|| Properties.Contains ("MAP ")
				|| (//Properties.Contains ("LEAP ") || 
						Properties.Contains ("REMAIN ")
						|| Properties.Contains ("FOLLOW ")
					|| Properties.Contains ("STAY ")
					|| Properties.Contains ("JUMP ")
					|| Properties.Contains ("AUTOLINK ")
					|| Properties.Contains ("CHILD ")
					|| Properties.Contains ("TELEPORT ")
					|| Properties.Contains ("LAND ")
						|| Properties.Contains ("FLIGHT")));
		}
	}

	public Boolean LevelConditionsMet
	{
		get {
			return Owner.LastStand || SearchName.StartsWith ("Cancel") || Properties.Contains (" NOLVL ") || 

				(((Math.Abs (Cost [3]) / 100) <= Owner.Level) && Owner.Level >= 6) ||

				((Math.Abs (Cost [3]) / 100) + 1 <= Owner.Level);
		}
	}

	public Boolean ActionConditionsMet
	{
		get {
			if (Properties.Contains ("SACTION ")) {
				return !Owner.PlayStyleDisplay.Equals ("B-ISM") && Owner.SActionEnabled && (Owner.Cooldown == 0 || Owner.NoConditions);
			}
			return true;
		}
	}

	public Boolean LifeConditionsMet
	{
		get {
			return  !Owner.KOd || Properties.Contains ("MAP") || this.Equals (Owner.KOSkill) || Properties.Contains ("KOD ") || Properties.Contains ("ALLST ");
		}
	}

	public Boolean MeterConditionsMet
	{
		get {
			return (owner.Health.CanBeUsed || Cost [0] == 0)
				&& (owner.Rush.CanBeUsed || Cost [1] == 0)
				&& (owner.Guard.CanBeUsed || Cost [2] == 0)
				&& (owner.Vitality.CanBeUsed || Cost [3] == 0);


		}
	}

    public Boolean VersusConditionsMet
    {
        get {
            return MovementConditionsMet
                && AdleConditionsMet;
        }
    }

	public Boolean ConditionsMet
	{
		get
		{
			Boolean levelConditionsMet = LevelConditionsMet;
			Boolean styleConditionsMet = ClassConditionsMet;
			Boolean adleContitionsMet = AdleConditionsMet;
			Boolean statusConditionsMet = StatusConditionsMet;
			Boolean movementConditionsMet = MovementConditionsMet;
			Boolean positionConditionsMet = PositionConditionsMet;
			Boolean actionConditionsMet = ActionConditionsMet;
			Boolean lifeConditionsMet = LifeConditionsMet;
			Boolean jumpConditionsMet = !Properties.Contains ("JUMP ") || !owner.HasJumped;
			Boolean meterConditionsMet = MeterConditionsMet;
			int conds = 0;

			if (Properties.Contains ("MAP ") || Properties.Contains ("INDEPENDENT ") || Properties.Contains ("NEUTRAL ")) {
				levelConditionsMet = true;
				adleContitionsMet = true;
				statusConditionsMet = true;
				movementConditionsMet = true;
				positionConditionsMet = true;
				actionConditionsMet = true;
				lifeConditionsMet = true;
				jumpConditionsMet = true;
				meterConditionsMet = true;
			}

			if (owner.NoConditions || NoConditions) {
				return true;
			} else {
				for (int i = 0; i < conditions.Length; i++) {
					if (!conditionMet (conditions[i]) && !Properties.Contains (" OR ")) {
						return false;
					}
					else if (conditionMet (conditions [i])) {
						conds++;
					}
				}
			}
			return styleConditionsMet && adleContitionsMet && movementConditionsMet && statusConditionsMet
				&& positionConditionsMet && levelConditionsMet && actionConditionsMet && lifeConditionsMet
				&& jumpConditionsMet && meterConditionsMet && (!Once || !Used)
				&& (!Properties.Contains (" OR ") || conds > 0);
		}
	}

	public int TotalCost
	{
		get {
			return Math.Abs (Cost[0])
				+ Math.Abs (Cost[1])
				+ Math.Abs (Cost[2])
				+ Math.Abs (Cost[3]);
		}
	}

	public float FloatCost
	{
		get {
			if (Properties.Contains ("BURST")) {
				return 2.0f;
			} else if (Properties.Contains ("VITALITY")) {
				return 1.0f;
			} else if (Properties.Contains ("SPECIAL") || Properties.Contains ("ITEM") || Properties.Contains ("JUMP")) {
				return 1.0f;
			} else if (Properties.Contains ("NORMAL")){
				return 0.0f;
			}
			return 0.0f;
		}
	}

	public Boolean CostsMet
	{
		get
		{
			return owner != null && owner.Vitality != null && owner.Vitality.MeterLevel != null &&

				(((Cost [0] == 0 && Cost [1] == 0 && Cost [2] == 0 && Cost [3] == 0)

				||

				  ((owner.Rush.MeterLevel + Cost [1] >= 0 || owner.StateActive (owner.Fury)))
			
				&& (owner.Guard.MeterLevel + Cost [2] >= 0))
				
				&& (owner.Vitality.MeterLevel + Cost [3] >= 0)

					&& (NumUses > 0 || NumUses == -1 || Owner.NoConditions));

		}
	}

	public Boolean costsMet (int[] costSet) {

		return ((Cost [0] == 0 && Cost [1] == 0 && Cost [2] == 0 && Cost [3] == 0)
			|| ((owner.Health.MeterLevel + Cost [0] + costSet [0] >= 0)
		        && ((owner.Rush.MeterLevel + Cost [1] + costSet [1] >= 0 || owner.StateActive (owner.Fury)))
				&& (owner.Guard.MeterLevel + Cost [2] + costSet [2] >= 0)
				&& (owner.Vitality.MeterLevel + Cost [3] + costSet [3] >= 0)))
				&& (NumUses > 0 || NumUses == -1);
	}

	public string ConditionDisplays (string txt)
	{
		string output = "";
		for (int i = 0; i < conditions.Length; i++)
		{
			output += "" + txt + @"     CONDITION: ";

			if (conditions[i].Equals("BORDER"))
			{
				output += string.Format("BORDER   " + '\n');
			}
			if (conditions[i].Contains("PAIR:"))
			{
				output += string.Format("PAIRED WITH {0}   ", conditions[i].Substring (conditions[i].IndexOf (':') + 1).ToUpper() + '\n');
			}
			if (conditions[i].Equals("CRITICAL"))
			{
				output += string.Format("HP IS CRITICAL   " + '\n');
			}
			if (conditions[i].Equals("CRITICALACTIVE"))
			{
				output += string.Format("HP WAS CRITICAL   " + '\n');
			}
			if (conditions[i].Equals("FULLHEALTH"))
			{
				output += string.Format("FULL HP   " + '\n');
			}
			if (conditions[i].Equals("CONFUSED"))
			{
				output += string.Format("CONFUSED   " + '\n');
			}
			if (conditions[i].Equals("POISONED"))
			{
				output += string.Format("POISONED   " + '\n');
			}
			if (conditions[i].Contains("AMMO:"))
			{
				output += string.Format("AMMUNITION: {0}   " + '\n',
					conditions[i].Substring (conditions[i].IndexOf (':') + 1).ToUpper());
			}
			if (conditions[i].Contains("NOAMMO"))
			{
				output += string.Format("NO ACTIVE ITEMS" + '\n');
			}
			if (conditions[i].Contains("AMOUNT:"))
			{
				output += string.Format("AT LEAST "
					+ NumberConverter.ConvertToInt(conditions[i].Substring (conditions[i].IndexOf (':') + 1, 2))
					+ @" " + conditions[i].Substring (conditions[i].IndexOf ('-') + 1));
			}
			if (conditions[i].Contains("AMOUNT="))
			{
				output += string.Format("EXACTLY "
					+ NumberConverter.ConvertToInt(conditions[i].Substring (conditions[i].IndexOf (':') + 1, 2))
					+ @" " + conditions[i].Substring (conditions[i].IndexOf ('-') + 1));
			}
			if (conditions[i].Contains("NOITEM:"))
			{
				output += string.Format("NO " + conditions[i].Substring (conditions[i].IndexOf (':') + 1));
			}
			if (conditions[i].Contains("JUMP"))
			{
				output += string.Format("JUMP   " + '\n');
			}
			if (conditions[i].Contains("FLIGHT"))
			{
				output += string.Format("FLYING   " + '\n');
			}
			if (conditions[i].Contains("HP>"))
			{
				output += string.Format("HP GREATER THAN {0}   " + '\n', conditions[i].Substring (conditions[i].IndexOf (">") + 1));
			}
			if (conditions[i].Contains("HP<"))
			{
				output += string.Format("HP LESS THAN {0}   " + '\n', conditions[i].Substring (conditions[i].IndexOf ("<") + 1));
			}
			if (conditions[i].Contains("RM>"))
			{
				output += string.Format("RM GREATER THAN {0}   " + '\n', conditions[i].Substring (conditions[i].IndexOf (">") + 1));
			}
			if (conditions[i].Contains("RM<"))
			{
				output += string.Format("RM LESS THAN {0}   " + '\n', conditions[i].Substring (conditions[i].IndexOf ("<") + 1));
			}
			if (conditions[i].Contains("GM>"))
			{
				output += string.Format("GM GREATER THAN {0}   " + '\n', conditions[i].Substring (conditions[i].IndexOf (">") + 1));
			}
			if (conditions[i].Contains("GM<"))
			{
				output += string.Format("GM LESS THAN {0}   " + '\n', conditions[i].Substring (conditions[i].IndexOf ("<") + 1));
			}
			if (conditions[i].Contains("VM>"))
			{
				output += string.Format("VM GREATER THAN {0}   " + '\n', conditions[i].Substring (conditions[i].IndexOf (">") + 1));
			}
			if (conditions[i].Contains("VM<"))
			{
				output += string.Format("VM LESS THAN {0}   " + '\n', conditions[i].Substring (conditions[i].IndexOf ("<") + 1));
			}
			if (conditions[i].Contains("ST>"))
			{
				output += string.Format("STUN GREATER THAN {0}   " + '\n', conditions[i].Substring (conditions[i].IndexOf (">") + 1));
			}
			if (conditions[i].Contains("ST<"))
			{
				output += string.Format("STUN LESS THAN {0}   " + '\n', conditions[i].Substring (conditions[i].IndexOf ("<") + 1));
			}
			if (conditions[i].Contains("FAT>"))
			{
				output += string.Format("FATIGUE GREATER THAN {0}   " + '\n', conditions[i].Substring (conditions[i].IndexOf (">") + 1));
			}
			if (conditions[i].Contains("FAT<"))
			{
				output += string.Format("FATIGUE LESS THAN {0}   " + '\n', conditions[i].Substring (conditions[i].IndexOf ("<") + 1));
			}
			if (conditions[i].Contains("DMGIN>"))
			{
				output += string.Format("DAMAGE-IN > {0}   " + '\n', conditions[i].Substring (conditions[i].IndexOf (">") + 1));
			}
			if (conditions[i].Contains("DMGIN<"))
			{
				output += string.Format("DAMAGE-IN > {0}   " + '\n', conditions[i].Substring (conditions[i].IndexOf ("<") + 1));
			}
			if (conditions[i].Contains("DMGOUT>"))
			{
				output += string.Format("DAMAGE-OUT > {0}   " + '\n', conditions[i].Substring (conditions[i].IndexOf (">") + 1));
			}
			if (conditions[i].Contains("DMGOUT<"))
			{
				output += string.Format("DAMAGE-OUT < {0}   " + '\n', conditions[i].Substring (conditions[i].IndexOf ("<") + 1));
			}
			if (conditions[i].StartsWith ("LVL>"))
			{
				output += string.Format("LEVEL > {0}   " + '\n', conditions[i].Substring (conditions[i].IndexOf ('>') + 1));
			}
			if (conditions[i].StartsWith ("ALLYSTAND"))
			{
				output += string.Format("STANDING ALLY");
			}

		}
		return output;
	}


	public string ConditionDisplay
	{
		get {
			string output = "";
			for (int i = 0; i < conditions.Length; i++) {
				if (conditions [i].Equals ("BORDER")) {
					output += string.Format ("BORDER   " + '\n');
				}
				if (conditions [i].Contains ("PAIR:")) {
					output += string.Format ("PAIRED WITH {0}   ", conditions [i].Substring (conditions [i].IndexOf (':') + 1).ToUpper () + '\n');
				}
				if (conditions [i].Equals ("CRITICAL")) {
					output += string.Format ("HP IS CRITICAL   " + '\n');
				}
				if (conditions [i].Equals ("CRITICALACTIVE")) {
					output += string.Format ("HP WAS CRITICAL   " + '\n');
				}
				if (conditions [i].Equals ("FULLHEALTH")) {
					output += string.Format ("FULL HP   " + '\n');
				}
				if (conditions [i].Equals ("CONFUSED")) {
					output += string.Format ("CONFUSED   " + '\n');
				}
				if (conditions [i].Equals ("POISONED")) {
					output += string.Format ("POISONED   " + '\n');
				}
				if (conditions [i].Contains ("AMMO:")) {
					output += string.Format ("AMMUNITION: {0}   " + '\n',
						conditions [i].Substring (conditions [i].IndexOf (':') + 1).ToUpper ());
				}
				if (conditions[i].Contains("NOAMMO")) {
					output += string.Format("NO ACTIVE ITEMS" + '\n');
				}
 				if (conditions [i].Contains ("AMOUNT:")) {
					output += string.Format ("AT LEAST " 
						+ NumberConverter.ConvertToInt (conditions [i].Substring (conditions [i].IndexOf (':') + 1, 2))
						+ @" " + conditions [i].Substring (conditions [i].IndexOf ('-') + 1));
				}
				if (conditions [i].Contains ("AMOUNT=")) {
					output += string.Format ("EXACTLY " 
						+ NumberConverter.ConvertToInt (conditions [i].Substring (conditions [i].IndexOf (':') + 1, 2))
						+ @" " + conditions [i].Substring (conditions [i].IndexOf ('-') + 1));
				}
				if (conditions [i].Contains ("NOITEM:")) {
					output += string.Format ("NO " + conditions [i].Substring (conditions [i].IndexOf (':') + 1));
				}
				if (conditions [i].Contains ("JUMP")) {
					output += string.Format ("JUMP   " + '\n');
				}
				if (conditions [i].Contains ("FLIGHT")) {
					output += string.Format ("FLYING   " + '\n');
				}
				if (conditions [i].Contains ("HP>")) {
					output += string.Format ("HP GREATER THAN {0}   " + '\n', conditions [i].Substring (conditions [i].IndexOf (">") + 1));
				}
				if (conditions [i].Contains ("HP<")) {
					output += string.Format ("HP LESS THAN {0}   " + '\n', conditions [i].Substring (conditions [i].IndexOf ("<") + 1));
				}
				if (conditions [i].Contains ("RM>")) {
					output += string.Format ("RM GREATER THAN {0}   " + '\n', conditions [i].Substring (conditions [i].IndexOf (">") + 1));
				}
				if (conditions [i].Contains ("RM<")) {
					output += string.Format ("RM LESS THAN {0}   " + '\n', conditions [i].Substring (conditions [i].IndexOf ("<") + 1));
				}
				if (conditions [i].Contains ("GM>")) {
					output += string.Format ("GM GREATER THAN {0}   " + '\n', conditions [i].Substring (conditions [i].IndexOf (">") + 1));
				}
				if (conditions [i].Contains ("GM<")) {
					output += string.Format ("GM LESS THAN {0}   " + '\n', conditions [i].Substring (conditions [i].IndexOf ("<") + 1));
				}
				if (conditions [i].Contains ("VM>")) {
					output += string.Format ("VM GREATER THAN {0}   " + '\n', conditions [i].Substring (conditions [i].IndexOf (">") + 1));
				}
				if (conditions [i].Contains ("VM<")) {
					output += string.Format ("VM LESS THAN {0}   " + '\n', conditions [i].Substring (conditions [i].IndexOf ("<") + 1));
				}
				if (conditions [i].Contains ("DMGIN>")) {
					output += string.Format ("DAMAGE-IN > {0}   " + '\n', conditions [i].Substring (conditions [i].IndexOf (">") + 1));
				}
				if (conditions [i].Contains ("DMGIN<")) {
					output += string.Format ("DAMAGE-IN > {0}   " + '\n', conditions [i].Substring (conditions [i].IndexOf ("<") + 1));
				}
				if (conditions [i].Contains ("DMGOUT>")) {
					output += string.Format ("DAMAGE-OUT > {0}   " + '\n', conditions [i].Substring (conditions [i].IndexOf (">") + 1));
				}
				if (conditions [i].Contains ("DMGOUT<")) {
					output += string.Format ("DAMAGE-OUT < {0}   " + '\n', conditions [i].Substring (conditions [i].IndexOf ("<") + 1));
				}
				if (conditions [i].StartsWith ("LVL>")) {
					output += string.Format ("LEVEL > {0}   " + '\n', conditions [i].Substring (conditions [i].IndexOf ('>') + 1));
				}
				if (conditions [i].StartsWith ("ALLYSTAND")) {
					output += string.Format ("STANDING ALLY");
				}

			}
			return output + '\n';
		}
	}

	private Boolean conditionMet (string condition)
	{
		if (condition.StartsWith ("FAT>")) {
			return owner.ResidualFatigue > NumberConverter.ConvertToInt (condition.Substring (condition.IndexOf ('>') + 1));
		}
		if (condition.StartsWith ("FAT<")) {
			return owner.ResidualFatigue > NumberConverter.ConvertToInt (condition.Substring (condition.IndexOf ('<') + 1));
		}
		if (condition.StartsWith ("LVL>")) {
			return owner.Level > NumberConverter.ConvertToInt (condition.Substring (condition.IndexOf ('>') + 1));
		}
		if (condition.Equals ("ALLYSTAND")) {
			return owner.BorderStandingAlly != null;
		}
		if (condition.Equals ("CROUCH")) {
			return owner.IsCrouching;
		}
		if (condition.Equals ("BORDER")) {
			return owner.OnBorder;
		}
		if (condition.Contains ("PAIR:")) {
			return owner.bordersWith (condition.Substring (condition.IndexOf (':') + 1));
		}
		if (condition.Equals ("CRITICAL")) {
			return owner.Critical;
		}
		if (condition.Equals ("CRITICALACTIVE")) {
			return owner.CriticalActive;
		}
		if (condition.Equals ("FULLHEALTH")) {
			return owner.Health.MeterLevel == owner.Health.MeterMax;
		}
		if (condition.Equals ("CONFUSED")) {
			return owner.StateActive (owner.Confuse);
		}
		if (condition.Equals ("POISONED")) {
			return owner.StateActive (owner.Poison);
		}
		if (condition.Equals ("NOAMMO")) {
			return !owner.InventoryActive;
		}
		if (condition.Contains ("AMMO:")) {
			return (owner.inventoryGet (condition.Substring (condition.IndexOf (':') + 1)) != null
				&& owner.inventoryGet (condition.Substring (condition.IndexOf (':') + 1)).NumUses != 0);
		}
		if (condition.Contains ("AMOUNT:")) {
			return (owner.inventoryGet (condition.Substring (condition.IndexOf ('-') + 1)) != null &&
				owner.inventoryGet (condition.Substring (condition.IndexOf ('-') + 1)).NumUses
				>= NumberConverter.ConvertToInt (condition.Substring (condition.IndexOf (':') + 1, 2)));
		}
		if (condition.Contains ("AMOUNT=")) {
			return (owner.inventoryGet (condition.Substring (condition.IndexOf ('-') + 1)) != null &&
				owner.inventoryGet (condition.Substring (condition.IndexOf ('-') + 1)).NumUses
				== NumberConverter.ConvertToInt (condition.Substring (condition.IndexOf ('=') + 1, 2)));
		}
		if (condition.Contains ("NOITEM:")) {
			return (owner.inventoryGet ( condition.Substring (condition.IndexOf (':') + 1)) == null
				|| owner.inventoryGet ( condition.Substring (condition.IndexOf (':') + 1)).NumUses == 0);
		}
		if (condition.Equals ("ACTION")) {
			return owner.CanAct;
		}
		if (condition.Equals ("NOACTION")) {
			return !owner.CanAct;
		}
		if (condition.Contains ("JUMP")) {
			return !owner.HasJumped;
		}
		if (condition.Contains ("FLIGHT")) {
			return owner.Flight;
		}
		if (condition.Contains ("HP>")) {
			if (condition.Contains (">=")) {
				return owner.Health.MeterLevel >= NumberConverter.ConvertToInt (condition.Substring (condition.IndexOf (">") + 1));
			}
			return owner.Health.MeterLevel > NumberConverter.ConvertToInt (condition.Substring (condition.IndexOf (">") + 1));
		}
		if (condition.Contains ("HP<")) {
			if (condition.Contains ("<=")) {
				return owner.Health.MeterLevel <= NumberConverter.ConvertToInt (condition.Substring (condition.IndexOf ("<") + 1));
			}
			return owner.Health.MeterLevel < NumberConverter.ConvertToInt (condition.Substring (condition.IndexOf ("<") + 1));
		}
		if (condition.Contains ("RM>")) {
			if (condition.Contains (">=")) {
				return owner.Rush.MeterLevel >= NumberConverter.ConvertToInt (condition.Substring (condition.IndexOf (">") + 1));
			}
			return owner.Rush.MeterLevel > NumberConverter.ConvertToInt (condition.Substring (condition.IndexOf (">") + 1));
		}
		if (condition.Contains ("RM<")) {
			if (condition.Contains ("<=")) {
				return owner.Rush.MeterLevel <= NumberConverter.ConvertToInt (condition.Substring (condition.IndexOf ("<") + 1));
			}
			return owner.Rush.MeterLevel < NumberConverter.ConvertToInt (condition.Substring (condition.IndexOf ("<") + 1));
		}
		if (condition.Contains ("GM>")) {
			if (condition.Contains (">=")) {
				return owner.Guard.MeterLevel >= NumberConverter.ConvertToInt (condition.Substring (condition.IndexOf (">") + 1));
			}
			return owner.Guard.MeterLevel > NumberConverter.ConvertToInt (condition.Substring (condition.IndexOf (">") + 1));
		}
		if (condition.Contains ("GM<")) {
			if (condition.Contains ("<=")) {

			}
			return owner.Guard.MeterLevel < NumberConverter.ConvertToInt (condition.Substring (condition.IndexOf ("<") + 1));
		}
		if (condition.Contains ("VM>")) {
			if (condition.Contains (">=")) {
				return owner.Vitality.MeterLevel >= NumberConverter.ConvertToInt (condition.Substring (condition.IndexOf (">") + 1));
			}
			return owner.Vitality.MeterLevel > NumberConverter.ConvertToInt (condition.Substring (condition.IndexOf (">") + 1));
		}
		if (condition.Contains ("VM<")) {
			if (condition.Contains ("<=")) {
				return owner.Vitality.MeterLevel <= NumberConverter.ConvertToInt (condition.Substring (condition.IndexOf ("<") + 1));
			}
			return owner.Vitality.MeterLevel < NumberConverter.ConvertToInt (condition.Substring (condition.IndexOf ("<") + 1));
		}
		if (condition.Contains ("ST>")) {
			if (condition.Contains (">=")) {
				return owner.Stun.MeterLevel >= NumberConverter.ConvertToInt (condition.Substring (condition.IndexOf (">") + 1));
			}
			return owner.Stun.MeterLevel > NumberConverter.ConvertToInt (condition.Substring (condition.IndexOf (">") + 1));
		}
		if (condition.Contains ("ST<")) {
			if (condition.Contains ("<=")) {
				return owner.Stun.MeterLevel <= NumberConverter.ConvertToInt (condition.Substring (condition.IndexOf ("<") + 1));
			}
			return owner.Stun.MeterLevel < NumberConverter.ConvertToInt (condition.Substring (condition.IndexOf ("<") + 1));
		}
		if (condition.Contains ("DMGIN>")) {
			if (condition.Contains (">=")) {
				return owner.DamageTakenTotal >= NumberConverter.ConvertToInt (condition.Substring (condition.IndexOf (">") + 1));

			}
			return owner.DamageTakenTotal > NumberConverter.ConvertToInt (condition.Substring (condition.IndexOf (">") + 1));
		}
		if (condition.Contains ("DMGIN<")) {
			return owner.DamageTotal > NumberConverter.ConvertToInt (condition.Substring (condition.IndexOf (">") + 1));
		}
		if (condition.Contains ("DMGOUT<")) {
			return owner.DamageTotal < NumberConverter.ConvertToInt (condition.Substring (condition.IndexOf ("<") + 1));
		}
		if (condition.EndsWith ("STATE")) {
			if (condition.Contains (">=")) {

			}
			return owner.SpecialState || owner.LastStand;
		}
		return true;
	}

	public Boolean properPlayer (Player target, string sName)
	{
		return target.Equals (Owner);
	}

	public Boolean inLine (Location loc)
	{
		return owner.inLine (loc);
	}

	public Boolean CanCancel
	{
		get {
			Skill s = this;
			while (s.Properties.Contains ("AUTOLINK") && s.LinkSkills.Count > 0) {
				s = (Skill)s.LinkSkills [0];
			}
            if (s.Properties.Contains ("PCANCEL"))
            {
				return true;
			}
			return false;
		}
	}

	public Boolean CanLink
	{
		get {
			Skill s = this;
			while (s.Properties.Contains ("AUTOLINK") && s.LinkSkills.Count > 0) {
				s = (Skill)s.LinkSkills [0];
			}
			if (s.setCostsAndConditionsMet (s.LinkSkills)) {
				return true;
			}
			return false;
		}
	}

	public string FollowUps
	{
		get {
			string output = "";
			Skill s = this, sk = null;
			int numLinks = 0;
			while (s.Properties.Contains ("AUTOLINK") && s.LinkSkills.Count > 0) {
				s = (Skill)s.LinkSkills [0];
			}

			if (s.LinkSkills.Count == 0 || !owner.setCostsAndConditionsMet (s.Cost, s.LinkSkills)) {
				return "NO LINKS";
			}

			output += "LINKS:" + '\n';

			for (int j = 0; j < s.LinkSkills.Count; j++) {

				sk = ((Skill)s.LinkSkills [j]);

				if (sk.costsAndConditionsMet (s.Cost)) {
					output += string.Format (">>" + sk.Name.ToUpper () + @" ");
				} else {
					output += string.Format (sk.Name.ToUpper () + @" ");
				}

				if (numLinks >= 1) {
					output += "" + '\n' + @"";
					numLinks = 0;
				} else {
					output += "   ";
					numLinks ++;
				}
			}
			return output;
		}
	}

	public void setLocations ()
	{
		Locations.Clear ();
		Location loc;


        if (Owner != null && Owner.currentMap() != null)
        {
            currentHeight = Owner.currentMap().heightOf(Owner.currentLocation());

            if (SelfSkill)
            {
                Locations.Add(owner.currentLocation());
            }
            else
            {
                for (int i = 0; i < owner.currentMap().MapGrid.Length; i++)
                {
                    for (int j = 0; j < owner.currentMap().MapGrid[0].Length; j++)
                    {
                        loc = new Location(i, j);

                        if (owner.currentMap().isValid(loc)
                                && loc.span(owner.currentLocation()) <= Range)
                        {
                            if (!LineSkill || (owner.inLine(loc) && !loc.Equals(owner.currentLocation())))
                            {
                                Locations.Add(loc);
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < LinkSkills.Count; i++)
            {
                ((Skill)LinkSkills[i]).setLocations();
            }
            if (TrapSkill != null)
            {
                TrapSkill.setLocations();
            }
            if (TimerSkill != null)
            {
                TimerSkill.setLocations();
            }
            if (DropItem != null)
            {
                DropItem.setLocations();
            }
            if (CancelSkill != null)
            {
                CancelSkill.setLocations ();
            }
        }
	}


	public int LastHeight
	{
		get {
			return lastHeight;
		}
		set {
			lastHeight = value;
		}
	}

	public void resetLocations ()
	{
		Locations.Clear ();
		for (int i = 0; i < LinkSkills.Count; i++) {
			((Skill)LinkSkills [i]).resetLocations ();
		}
		if (TrapSkill != null) {
			TrapSkill.resetLocations ();
		}
		if (TimerSkill != null) {
			TimerSkill.resetLocations ();
		}
        if (DropItem != null) {
            DropItem.resetLocations ();
        }
        if (CancelSkill != null) {
            CancelSkill.resetLocations();
        }

    }

    public override string ToString ()
	{
		return string.Format ("{0} ({1}): {2} {3}", Name, SkillType, Description, Eligibility);
	}

	public string SkillType
	{
		get {
			if (SelfSkill) {
				return "Self";
			} if (SingleSkill) {
				return "Single";
			} if (LineSkill) {
				return "Line";
			} if (RadiusSkill) {
				return "Radius";
			} if (ConeSkill) {
				return "Cone";
			}
			return "N/A";
		}
	}

	public string Eligibility
	{
		get {
			if (!ConditionsMet) {
				return "(N/A)";
			} return "";
		}
	}

	public Boolean skillWillHit (Player target)
	{
		return (!target.Undead || !Properties.Contains ("WHFUND ")) && 
			((target.IsGuarding &&
			(!target.AutoGuard && ((Overhead && target.IsCrouching)
				|| (Low && target.IsStanding)))
			|| (Grapple)
			  || Properties.Contains ("UNBLOCKABLE")));
	}

	public int realOutput (Player target, int pot) {
        /**
        if (Properties.Contains ("RANDOMDMG"))
        {
            return pot + ;
        }
        */

        if (!Properties.Contains ("FLAT ")) {
			return (int)(Owner.randomOutput (elementalOutput (target, ratioOutput (target, (int)(pot)))) / target.guardRatio (false));
		}
		return pot;
	}

	public int[] realOutput (Player target, double decay)
	{
		if (target == null && !(owner == null)) {
			target = owner.PlayerStandard;
		}

		int[] outputs = new int[4];
		if (!Properties.Contains ("PIERCE") && !Properties.Contains ("FLAT")) {
			outputs [0] = (int)(Owner.randomOutput (elementalOutput (target, ratioOutput (target, (int)(Change [0] * decay)))) / target.guardRatio (skillWillHit (target)));
			outputs [1] = (int)(Owner.randomOutput (elementalOutput (target, ratioOutput (target, (int)(Change [1] * decay)))) / target.guardRatio (skillWillHit (target)));
			outputs [2] = (int)(Owner.randomOutput (elementalOutput (target, ratioOutput (target, (int)(Change [2] * decay)))) / target.guardRatio (skillWillHit (target)));
			outputs [3] = (int)(Owner.randomOutput (elementalOutput (target, ratioOutput (target, (int)(Change [3] * decay)))) / target.guardRatio (skillWillHit (target)));

		} else {
			outputs = change;
		}
		return outputs;
	}

	private void addArrayValues (Skill s, int[] outputs, Player target, double decay)
	{
		int spn = 0;
		if (!target.FirstName.Equals ("")) {
			spn = owner.span (target) - 1;
			if (spn > s.Threshold) {
				spn = s.Threshold - 1;
			}
		}

		if (s.Reps > 1 || s.ObjectSkill || !s.LineSkill || (!s.Properties.Contains ("NOPUSH") && !s.Properties.Contains ("ROW "))) {
			for (int i = 0; i < s.Reps; i++) {
				if (!s.Properties.Contains ("PIERCE") && !s.Properties.Contains ("FLAT")) {
					outputs [0] += (int)(s.elementalOutput (target, s.ratioOutput (target, (int)(s.Change [0] * decay))) / target.guardRatio (skillWillHit (target)));
					outputs [1] += (int)(s.elementalOutput (target, s.ratioOutput (target, (int)(s.Change [1] * decay))) / target.guardRatio (skillWillHit (target)));
					outputs [2] += (int)(s.elementalOutput (target, s.ratioOutput (target, (int)(s.Change [2] * decay))) / target.guardRatio (skillWillHit (target)));
					outputs [3] += (int)(s.elementalOutput (target, s.ratioOutput (target, (int)(s.Change [3] * decay))) / target.guardRatio (skillWillHit (target)));
				} else {
					outputs [0] += (int)(s.Change [0] / target.guardRatio (skillWillHit (target)));
					outputs [1] += (int)(s.Change [1] / target.guardRatio (skillWillHit (target)));
					outputs [2] += (int)(s.Change [2] / target.guardRatio (skillWillHit (target)));
					outputs [3] += (int)(s.Change [3] / target.guardRatio (skillWillHit (target)));
				}

				if (s.Links.Length > 0 && s.Links [i]) {
					break;
				}
			}
		} else {
			if (s.Properties.Contains ("NOPUSH") || s.Properties.Contains ("ROW ")) {
				if (!s.Properties.Contains ("PIERCE") && !s.Properties.Contains ("FLAT")) {
					outputs [0] += (int)(s.elementalOutput (target, s.ratioOutput (target, (int)(s.Change [0] * decay))) * (s.Threshold - spn) / target.guardRatio (skillWillHit (target)));
					outputs [1] += (int)(s.elementalOutput (target, s.ratioOutput (target, (int)(s.Change [1] * decay))) * (s.Threshold - spn) / target.guardRatio (skillWillHit (target)));
					outputs [2] += (int)(s.elementalOutput (target, s.ratioOutput (target, (int)(s.Change [2] * decay))) * (s.Threshold - spn) / target.guardRatio (skillWillHit (target)));
					outputs [3] += (int)(s.elementalOutput (target, s.ratioOutput (target, (int)(s.Change [3] * decay))) * (s.Threshold - spn) / target.guardRatio (skillWillHit (target)));
				} else {
					outputs [0] += (int)(s.Change [0] * (s.Threshold - spn) / target.guardRatio (skillWillHit (target)));
					outputs [1] += (int)(s.Change [1] * (s.Threshold - spn) / target.guardRatio (skillWillHit (target)));
					outputs [2] += (int)(s.Change [2] * (s.Threshold - spn) / target.guardRatio (skillWillHit (target)));
					outputs [3] += (int)(s.Change [3] * (s.Threshold - spn) / target.guardRatio (skillWillHit (target)));
				}
			} else {
				if (!s.Properties.Contains ("PIERCE") && !s.Properties.Contains ("FLAT")) {
					outputs [0] += (int)(s.elementalOutput (target, s.ratioOutput (target, (int)(s.Change [0] * decay))) / target.guardRatio (skillWillHit (target)));
					outputs [1] += (int)(s.elementalOutput (target, s.ratioOutput (target, (int)(s.Change [1] * decay))) / target.guardRatio (skillWillHit (target)));
					outputs [2] += (int)(s.elementalOutput (target, s.ratioOutput (target, (int)(s.Change [2] * decay))) / target.guardRatio (skillWillHit (target)));
					outputs [3] += (int)(s.elementalOutput (target, s.ratioOutput (target, (int)(s.Change [3] * decay))) / target.guardRatio (skillWillHit (target)));
				} else {
					outputs [0] += (int)(s.Change [0] / target.guardRatio (skillWillHit (target)));
					outputs [1] += (int)(s.Change [1] / target.guardRatio (skillWillHit (target)));
					outputs [2] += (int)(s.Change [2] / target.guardRatio (skillWillHit (target)));
					outputs [3] += (int)(s.Change [3] / target.guardRatio (skillWillHit (target)));
				}
			}
		}
	}

	public int[] fullOutput (Player target, double decay)
	{
		if (target == null) {
			target = new Player (-1, "", "", "", "", "", "", "", "", 0, 0.0, 0.0, "A", "A", 1, -1, false, true);
			target.setElementalProficiency (0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
			target.setBaseStats (100, 100, 100, 100, 0.9, 0.5, .9, 4, 20, 20, 5);
			target.MyTeam = new Team ("False Team", "FT", 4);
		}

		int[] outputs = new int [4];
		outputs [0] = 0;
		outputs [1] = 0;
		outputs [2] = 0;
		outputs [3] = 0;

		addArrayValues (this, outputs, target, decay);

		Skill nextSkill = null;
		if (Properties.Contains ("AUTOLINK") && LinkSkills.Count > 0 && ((Skill)LinkSkills [0]).ConditionsMet) {
			nextSkill = (Skill)LinkSkills [0];
		} else if (ObjectSkill) {
			nextSkill = TimerSkill;
		}

		while (nextSkill != null) {

			addArrayValues (nextSkill, outputs, target, decay);

			if (nextSkill.LinkSkills.Count > 0 && nextSkill.Properties.Contains ("AUTOLINK")) {
				nextSkill = (Skill)nextSkill.LinkSkills [0];
			} else {
				nextSkill = null;
			}
		}
		return outputs;
	}

	public int elementalOutput (Player target, int input)
	{
		if (input != 0) {
			int output = Math.Abs (input);

			for (int i = 0; i < 10; i++) {
				if (Elementals [i].Potency != 0) {
					output += (Elementals [i].Potency * owner.ElementalOffense [i]);
					output -= (Elementals [i].Potency * target.ElementalDefense [i]);
				}
			}

			output += bonusEffects (input, target);

			if (input < 0) {
				return -1 * output;
			}
			return output;
		}
		return 0;
	}

	public int bonusEffects (int input, Player target) {

		int output = 0;
		if ((target.Species.Contains ("Undead") || target.Species.Contains ("Homunculus")) && Elementals [8].Potency > 0) {
			output = -1 * (((Math.Abs(input) * 2) / 2) * Elementals [8].Potency);
		}
		if (input < 0) {
			return -1 * output;
		}
		return output;
	}


	public int ratioOutput (Player target, int input)
	{
		if (input != 0) {
			int physicalOutput = 0, magickalOutput = 0;

			//PHYSICAL RATIO
			if (Ratio > 0)
			{
				physicalOutput = Math.Abs (input);
				physicalOutput += (int)((Math.Abs(input) + ((7 * owner.Strength) / 1)) / OffenseRatio);

				if (Malicious) {
					physicalOutput -= ((Math.Abs(input) + ((7 * target.Grit) / 1)) / DefenseRatio);
				}
				physicalOutput = (int)(physicalOutput * ratio);
			}

			//MAGICKAL RATIO
			if ((1 - Ratio) > 0)
			{
				magickalOutput = Math.Abs (input);
				magickalOutput += (int)((Math.Abs(input) + ((7 * owner.Magick) / 1)) / OffenseRatio);
				if (Malicious) {
					magickalOutput -= ((Math.Abs(input) + ((7 * target.Resistance) / 1)) / DefenseRatio); 
				}
				magickalOutput = (int)(magickalOutput * (1 - ratio));
			}

			int output = physicalOutput + magickalOutput;

			if (Properties.Contains ("DMG%") && !Properties.Contains ("SPIRALDMG")) {
				output += NumberConverter.ConvertToInt (Properties.Substring (Properties.IndexOf ('%') + 1, 5)) * (Owner.span (target) - 1);
			}

			//TEAMWORK BONUS
			//int output = physicalOutput + magickalOutput;
			if (Malicious && target.sameTeam (owner) && !Neutral) {
				output -= ((owner.Teamwork * 5) / 3);
			} else if (!Malicious && target.sameTeam (owner) && !Neutral) {
				output += ((owner.Teamwork * 5) / 3);
			}

			if (input < 0 && output < 0) {
				return -1;
			}
			if (input < 0 && output > 0) {
				return -1 * output;
			} 
			return output;
		}
		return 0;
	}

	public Boolean sameHitSections (Skill s)
	{
		if (Properties.Contains ("MAP ")) {
			//SINGLE CONDITIONS

			if (properties.Contains ("GROUND ") && !properties.Contains ("MIDDLE ") && !properties.Contains ("AIRBORNE ")) {
				if (!s.OTG || s.Properties.Contains (" FORWARD ")) {
					return false;
				}
			}

			if (properties.Contains ("MIDDLE ") && !properties.Contains ("GROUND ") && !properties.Contains ("AIRBORNE ")) {
				if (!s.Mid && !s.High) {
					return false;
				}
			}

			if (properties.Contains ("AIRBORNE ") && !properties.Contains ("MIDDLE ") && !properties.Contains ("GROUND ")) {
				if (!s.Air && !s.HighAir) {
					return false;
				}
			}

			//DOUBLE CONDITIONS
			if (properties.Contains ("GROUND ") && properties.Contains ("MIDDLE ") && !properties.Contains ("AIRBORNE ")) {
				if (!s.OTG && !s.Mid && !s.High) {
					return false;
				}
			}

			if (properties.Contains ("MIDDLE ") && properties.Contains ("AIRBORNE ") && !properties.Contains ("GROUND ")) {
				if (!s.Mid && !s.High && !s.Air && !s.HighAir) {
					return false;
				}
			}

			if (properties.Contains ("GROUND ") && properties.Contains ("AIRBORNE ") && !properties.Contains ("MIDDLE ")) {
				if (!s.OTG && !s.Air && !s.HighAir) {
					return false;
				}
			}

			//TRIPLE CONDITIONS
			if (properties.Contains ("GROUND ") && properties.Contains ("AIRBORNE ") && properties.Contains ("MIDDLE ")) {
				if (!s.OTG && !s.Mid && !s.High && !s.Air && !s.HighAir) {
					return false;
				}
			}


		} else {
			if (OTG && !s.OTG) {
				return false;
			} if (Mid && !s.Mid) {
				return false;
			} if (High && !s.High) {
				return false;
			} if (Air && !s.Air) {
				return false;
			}  if (HighAir && !s.HighAir) {
				return false;
			}
		}
		return true;
	}

	public int OffenseRatio
	{
		get {
			return 10;
		}
	}

	public int DefenseRatio
	{
		get {
			return 10;
		}
	}

	public double HitChance
	{
		get {
			return owner.Dexterity + (((double)owner.Luck) / 1000) + (HitRate / 10);
		}
	}

	public double hitChance (Player target)
	{
		if (!Auto) {
			return (owner.Dexterity + (((double)owner.Luck) / 1000) + (HitRate / 10)) - ((target.Dexterity / 8.5) + (((double)target.Luck) / 1000));
		} else {
			return 1;
		}
	}

	public Boolean setCostsAndConditionsMet (ArrayList skillSet) {
		Skill s;
		for (int i = 0; i < skillSet.Count; i++) {
			s = (Skill)skillSet [i];
			if (s.CostsAndConditionsMet) {
				return true;
			}
		}
		return false;
	}

	public Boolean setCostsAndConditionsMet (int[] costSet, ArrayList skillSet) {
		Skill s;
		for (int i = 0; i < skillSet.Count; i++) {
			s = (Skill)skillSet [i];
			if (s.costsAndConditionsMet (costSet)) {
				return true;
			}
		}
		return false;
	}

	public string AllLocations
	{
		get {
			string output = "";
			for (int i = 0; i < Locations.Count; i++) {
				output += ((Location)Locations[i]).ToString () + @" ";
			}
			return output;
		}
	}

	public Boolean locationsContains (Location loc)
	{
		if (!LineSkill) {
			return Locations.Contains (loc);
		}

		Location referenceLoc;
		for (int x = 0; x < Locations.Count; x++) {
			referenceLoc = (Location)Locations [x];

			if (referenceLoc.Row == Owner.currentLocation ().Row && loc.Column == referenceLoc.Column && referenceLoc.span (loc) < WidthOrRadius) {
				return true;
			}
			if (referenceLoc.Column == Owner.currentLocation ().Column && loc.Row == referenceLoc.Row && referenceLoc.span (loc) < WidthOrRadius) {
				return true;
			}
		}
		return false;
	}

	public Boolean locationInRange (Location loc, Location targetLoc)
	{
		//Location referenceLoc = null;
		Location ownerLoc = owner.currentLocation ();
		//for (int i = 0; i < Locations.Count; i++) {

		if (RadiusSkill && Locations.Contains (loc)) {
			return true;
		}

		if (SelfSkill && loc.Equals (owner.currentLocation ())) {
			return true;
		}

		if (SingleSkill) {
			if (targetLoc != null && targetLoc.span (loc) < WidthOrRadius) {
				return true;
			}
		}

		if (LineSkill) {
			if (loc.span (ownerLoc) <= Range &&
			    (loc.Row == Owner.currentLocation ().Row || loc.Column == owner.currentLocation ().Column)
				&& targetLoc != null) {
			
				if (targetLoc.Row > Owner.currentLocation ().Row) {
					return loc.Row > owner.currentLocation ().Row;
				}

				if (targetLoc.Row < Owner.currentLocation ().Row) {
					return loc.Row < owner.currentLocation ().Row;
				}

				if (targetLoc.Column > Owner.currentLocation ().Column) {
					return loc.Column > owner.currentLocation ().Column;
				}

				if (targetLoc.Column < Owner.currentLocation ().Column) {
					return loc.Column < owner.currentLocation ().Column;
				}
			}
		}
		return false;
	}

	public void printSkill (int tab)
	{
		StreamWriter writer = new StreamWriter (@"Assets/Resources/Players/" + owner.SearchName + @"/Info/Skills/" + SearchName + @"-" + owner.PlayStyleDisplay + @".txt");
		writer.WriteLine (output (tab));
		writer.Close ();
	}

	public Boolean Combo
	{
		get {
			return combo;
		}
		set {
			combo = value;
		}
	}

	public string InfoText
	{
		get {
			string output = "";
			lengthCheck = 0;

			if (TurnsRemaining > 0 || TurnsRemaining < -1) {
				output += addInfoLine ("TRN " + TurnsRemaining);
			}

			if (!SelfSkill) {
				if (Threshold > 1)
					output += addInfoLine ("THR " + Threshold);

				if (Force.Potency != 0)
					output += addInfoLine ("FRC " + Force.Potency);

				if (Speed != 0)
					output += addInfoLine ("SPD " + Speed);

			}

			if (!Auto) {
				output += addInfoLine ("HTR " + (int)(hitChance (Owner) * 100) + @"%");
			}

			if (Recovery != 0)
				output += addInfoLine ("FAT " + Recovery);
		
	
			if (NumUses > 0)
				output += addInfoLine ("USE " + NumUses);
			else if (!NoConditions && !ExpendableAmmo.Equals ("N/A") && Owner.inventoryGet (ExpendableAmmo) != null)
				output += addInfoLine ("AMM " + Owner.inventoryGet (ExpendableAmmo).NumUses);

			if (Malicious || Beneficial) {
				if (Ratio > .5)
					output += addInfoLine (((int)(Ratio * 100)) + @"%PHY");
				else if (Ratio < .5)
					output += addInfoLine (((int)(1 - Ratio * 100)) + @"%MAG");
				else
					output += addInfoLine ("%BAL");
			}

			//output += addInfoLine ("IND ");

			if (Properties.Contains ("SPIRALDMG") || Properties.Contains ("SPIRALHP")) {
				output += addInfoLine ("+SPR");
			}
			if (CanBeLearned) {
				output += addInfoLine ("LRN");
			}

			output += ElementalInfoText;
			output += StatAugmentInfoText;
			output += StateAugmentInfoText;

			return output;
		}
	}

	public string addInfoLine (string line)
	{
		string output = "";
		lengthCheck++;

		output += line;

		if (lengthCheck > 3) {
			output += "" + '\n';
			lengthCheck = 0;
		} else {
			output += "   ";
		}
		return output;
	}

	public string ElementalInfoText
	{
		get {
			string output = "";
			State s;
			for (int i = 0; i < Elementals.Length; i++) {
				s = (State)Elementals [i];
				if (s.Probability > 0) {
					output += addInfoLine ( string.Format("{0}",
						s.Abbreviation, s.Potency, s.NumTurns, 100 * s.Probability));
				}
			}
			return output;
		}
	}

	public string StatAugmentInfoText
	{
		get {
			string output = "";
			for (int i = 0; i < StatAlterations.Length; i++) {
				if (StatAlterations[i].Probability != 0) {
					if (i > 3 && i < 6) {
						output += addInfoLine ( string.Format("{0}",
							StatAlterations [i].Abbreviation, StatAlterations [i].DoublePotency,
							(int)(StatAlterations [i].Probability * 100)));
					} else {
						output += addInfoLine (string.Format ("{0}",
							StatAlterations [i].Abbreviation, StatAlterations [i].Potency,
							(int)(StatAlterations [i].Probability * 100)));
					}
				}
			}
			return output;
		}
	}

	public string StateAugmentInfoText
	{
		get {
			string output = "";
			for (int i = 1; i < StateAlterations.Length; i++)
			{
				if (StateAlterations [i].Probability != 0 && (i == 0 || i > 2))
				{
					output += addInfoLine ( string.Format ("{0}",
						StateAlterations [i].Abbreviation, StateAlterations [i].Potency,
						StateAlterations [i].Probability * 100));

				}
			}

			State nextState;
			for (int i = 0; i < ExtraStateAlterations.Count; i++)
			{
				nextState = (State)ExtraStateAlterations [i];
				if (nextState.Probability != 0)
				{
					output += addInfoLine (string.Format ("{0}",
						nextState.Abbreviation, nextState.Potency,
						nextState.Probability * 100));
				}
			}
			return output;
		}
	}

    public string MasteryUp
    {
        get
        {
            Mastery++;
            string output = string.Format("{0} Mastery Up! {1}" + '/', Name, Mastery);
            hitRate += .01;

            if (Mastery % 5 == 0)
            {

                for (int i = 0; i < 3; i++)
                {
                    if (cost[i] < 0)
                    {
                        cost[i] -= 1;
                    }
                }

                if (Recovery > 1)
                {
                    Recovery--;
                }
                for (int i = 0; i < Elementals.Length; i++)
                {
                    if (Elementals[i].Potency > 0)
                    {
                        Elementals[i].Potency += 1;
                    }
                    if (Elementals[i].Potency < 0)
                    {
                        Elementals[i].Potency -= 1;
                    }
                }
            }

            for (int i = 0; i < change.Length; i++)
            {
                if (change[i] < 0)
                {
                    change[i] -= 10;
                }
                if (change[i] > 0)
                {
                    change[i] += 10;
                }
            }


            for (int i = 0; i < statAlterations.Length; i++)
            {
                if (statAlterations[i].Probability != 0 && statAlterations[i].Probability < 1)
                {
                    statAlterations[i].Probability += .01;
                }
                if (statAlterations[i].Potency > 0)
                {
                    statAlterations[i].Potency += 1;
                }
                if (statAlterations[i].Potency < 0)
                {
                    statAlterations[i].Potency -= 1;
                }
                if (statAlterations[i].DoublePotency > 0)
                {
                    statAlterations[i].DoublePotency += .01;
                }
                if (statAlterations[i].DoublePotency < 0)
                {
                    statAlterations[i].DoublePotency -= .01;
                }
            }

            for (int i = 0; i < stateAlterations.Length; i++)
            {
                if (stateAlterations[i].Probability != 0 && stateAlterations[i].Probability < 1)
                {
                    stateAlterations[i].Probability += .01;
                }
                if (!StateAlterations[i].Name.Equals("Leech"))
                {
                    if (stateAlterations[i].Potency > 0)
                    {
                        stateAlterations[i].Potency += 1;
                    }
                    if (stateAlterations[i].Potency < 0)
                    {
                        stateAlterations[i].Potency -= 1;
                    }
                }
                if (stateAlterations[i].DoublePotency > 0)
                {
                    stateAlterations[i].DoublePotency += .01;
                }
                if (stateAlterations[i].DoublePotency < 0)
                {
                    stateAlterations[i].DoublePotency -= .01;
                }
            }

            for (int i = 0; i < Elementals.Length; i++)
            {
                if (Elementals[i].Probability != 0 && Elementals[i].Probability < 1)
                {
                    Elementals[i].Probability += .01;
                }
                if (Elementals[i].DoublePotency > 0)
                {
                    Elementals[i].DoublePotency += .01;
                }
                if (Elementals[i].DoublePotency < 0)
                {
                    Elementals[i].DoublePotency -= .01;
                }
            }

            if (trapSkill != null) {
                output += trapSkill.MasteryUp;
            }

            if (dropItem != null)
            {
                output += dropItem.MasteryUp;
            }

            if (trapSkill != null)
            {
                output += trapSkill.MasteryUp;
            }


            return output;
        }
    }

    public string InformationOutput {
        get
        {
            string output = "";
            if (OriginalOwner != null && OriginalOwner != Owner)
            {
                output += originalOwner.SearchName + @":OriginalOwner" + '\n';
            }
            if (Owner != null)
            {
                output += Owner.SearchName + @" " + Owner.Row + @"," + Owner.Column + @":OriginalOwner" + '\n';
            }
            output += Mastery + @":Mastery" + '\n';
            output += Experience + @":Experience" + '\n';

            output += cost[0] + @":HPCost" + '\n';
            output += cost[1] + @":RushCost" + '\n';
            output += cost[2] + @":GuardCost" + '\n';
            output += cost[3] + @":VMCost" + '\n';

            output += change[0] + @":HPAffect" + '\n';
            output += change[1] + @":RushAffect" + '\n';
            output += change[2] + @":GuardAffect" + '\n';
            output += change[3] + @":VMAffect" + '\n';

            output += numUses + @":Uses" + '\n';
            output += turnsRemaining + @":TurnsRemaining" + '\n';

            output += usedNow + @":UsedNow" + '\n';
            output += timer + @":Timer" + '\n';

            output += currentHeight + @":CurrentHeight" + '\n';
            output += lastHeight + @":LastHeight" + '\n';

            output += direction + @":Direction" + '\n';
            output += currentRange + @":CurrentRange" + '\n';
            output += maxRange + @":MaxRange" + '\n';
            output += HitRate + @":HitRate" + '\n';

            for (int i = 0; i < statAlterations.Length; i ++) {
                output += statAlterations[i].Potency + @":" + statAlterations[i].DoublePotency + @"=" 
                                            + statAlterations [i].Probability + @"#" + statAlterations[i].Name + '\n';
            }

            for (int i = 0; i < stateAlterations.Length; i++) {
                output += stateAlterations[i].Potency + @":" + stateAlterations[i].DoublePotency + @"="
                                             + stateAlterations[i].Probability + @"#" + stateAlterations[i].Name + '\n';
            }

            for (int i = 0; i < elementals.Length; i++) {
                output += elementals[i].Potency + @":" + elementals[i].DoublePotency + @"="
                                       + elementals[i].Probability + @"#" + elementals[i].Name + '\n';
            }

            output += malicious + @":MALICIOUS" + '\n';
            output += beneficial + @":BENEFICIAL" + '\n';
            output += learnedInField + @":LEARNEDINFIELD" + '\n';
            output += selfSkill + @":SELF" + '\n';
            output += singleSkill + @":SINGLE" + '\n';
            output += lineSkill + @":LINE" + '\n';
            output += radiusSkill + @":RADIUS" + '\n';
            output += coneSkill + @":CONE" + '\n';
            output += objectSkill + @":OBJECT" + '\n';
            output += noConditions + @":NOCOND" + '\n';
            output += allies + @":ALLIES" + '\n';
            output += enemies + @":ENEMIES" + '\n';
            output += self + @":OWN" + '\n';
            output += auto + @":AUTO" + '\n';
            output += otg + @":OTG" + '\n';
            output += mid + @":MID" + '\n';
            output += high + @":HIGH" + '\n';
            output += air + @":AIR" + '\n';
            output += highAir + @":HIGHAIR" + '\n';
            output += equalLevel + @":EQUALLVL" + '\n';
            output += low + @":LOW" + '\n';
            output += overhead + @":OVERHEAD" + '\n';
            output += targetless + @":TGLESS" + '\n';
            output += onWhiff + @":WHIFF" + '\n';
            output += multihit + @":MULTI" + '\n';
            output += canAct + @":ACT" + '\n';
            output += canFreemove + @":FREE" + '\n';
            output += maintainMovements + @":MAINTAIN" + '\n';
            output += physical + @":PHYSICAL" + '\n';
            output += grapple + @":GRAPPLE" + '\n';
            output += magic + @":MAGIC" + '\n';
            output += movement + @":MOVEMENT" + '\n';
            output += weapon + @":WEAPON" + '\n';
            output += projectile + @":PROJECTILE" + '\n';
            output += superProjectile + @":SUPERPROJECTILE" + '\n';
            output += sForce + @":FORCE" + '\n';
            output += permeate + @":PERM" + '\n';
            output += claw + @":CLAW" + '\n';
            output += once + @":ONCE" + '\n';
            output += continuous + @":CONT" + '\n';
            output += individual + @":INDIV" + '\n';
            output += nonstop + @":NONSTOP" + '\n';
            output += canBeLearned + @":LRN" + '\n';
            output += canBeLearnedBase + @":LRNB" + '\n';
            output += canBeLearnedSpecial + @":LRNS" + '\n';
            output += jumpReset + @":JMPRST" + '\n';
            output += moveReset + @":MOVRST" + '\n';
            output += addLink + @":ADDLNK" + '\n';
            output += noCombo + @":NOCOMBO" + '\n';
            output += actionChange + @":ACTION" + '\n';
            output += benign + @":BENIGN" + '\n';
            output += neutral + @":NEUTRAL" + '\n';
            output += myTurn + @":MYTURN" + '\n';
            output += jumpIn + @":JUMPIN" + '\n';

            output += LinkSkills.Count + @":LINKSKILLS" + '\n';
            for (int i = 0; i < LinkSkills.Count; i ++) { output += ((Skill)LinkSkills [i]).SearchName + '\n'; }

            output += LearnSkills.Count + @":LEARNSKILLS" + '\n';
            for (int i = 0; i < LearnSkills.Count; i++) { output += ((Skill)LearnSkills[i]).SearchName + '\n'; }

            output += ForgetSkills.Count + @":FORGETSKILLS" + '\n';
            for (int i = 0; i < ForgetSkills.Count; i++) { output += ((Skill)ForgetSkills[i]).SearchName + '\n'; }

            if (counterSkill != null) { output += counterSkill.SearchName + @":COUNTERSKILL" + '\n'; }
            else { output += "null:COUNTERSKILL" + '\n'; } 

            if (timerSkill != null) { output += timerSkill.SearchName + @":TIMERSKILL" + '\n'; }
            else { output += "null:TIMERSKILL" + '\n'; }

            if (trapSkill != null) { output += trapSkill.SearchName + @":TRAPSKILL" + '\n'; }
            else { output += "null:TRAPSKILL" + '\n'; }

            if (dropItem != null) { output += dropItem.SearchName + @":DROPITEM" + '\n'; }
            else { output += "null:DROPITEM" + '\n'; }

            if (CurrentLocation != null)
            {
                output += CurrentLocation.Row + @"," + CurrentLocation.Column + @":LOCATION" + '\n';
            } else {
                output += "null:LOCATION" + '\n';
            }


            return output;
        }
    }

	public string output (int tab)
	{
		string output = "";
		string tabbed = "";
		string newLine = " " + '\n'; 
		for (int i = 0; i < tab; i++) {
			tabbed += "     ";
		}
		output += string.Format (tabbed + Name.ToUpper () + newLine);
		output += string.Format (tabbed + @"   " + Description + newLine);
		if (Inputs.Length > 0) {
			output += string.Format (tabbed + @"INPUT: " + Inputs [0] + newLine);
		}
		output += string.Format (tabbed + @"TYPE: " + SkillType);
		if (Physical) {
			output += string.Format (tabbed + @" Physical " + newLine);
		} else if (Grapple) {
			output += string.Format (tabbed + @" Grapple " + newLine);
		} else if (Magic) {
			output += string.Format (tabbed + @" Magic " + newLine);
		} else if (Projectile || SuperProjectile) {
			output += string.Format (tabbed + @" Projectile " + newLine);
		} else if (SForce) {
			output += string.Format (tabbed + @" Force " + newLine);
		} else if (Movement) {
			output += string.Format (tabbed + @" Movement " + newLine);
		}
		output += string.Format (tabbed + newLine);

		int[] realOutputs = fullOutput (null, 1);

		//output += string.Format (tabbed);
		output += string.Format (tabbed + @"COSTS" + newLine);
		output += string.Format (tabbed + @"    HP: " + Cost [0] + @", " + @" RM: " + Cost [1] + @", "
			+ @" GM: " + Cost [2] + @", " + @" VM: " + Cost [3] + newLine);
		if (NumUses >= 0) {
			output += string.Format (tabbed + @"USES: " + NumUses + newLine);
		} else if (!NoConditions && !ExpendableAmmo.Equals ("N/A")) {
			output += string.Format (tabbed + @"USES: " + Owner.inventoryGet (ExpendableAmmo).NumUses + newLine);
		}

		//output += string.Format (tabbed);
		output += string.Format (tabbed + @"EFFECTS" + newLine);
		output += string.Format (tabbed + @"    HP: " + realOutputs [0] + @", " + @" RM: " + realOutputs [1] + @", "
			+ @" GM: " + realOutputs [2] + @", " + @" VM: " + realOutputs [3]);
		if (StateAlterations [0].Potency != 0) {
			output += string.Format (String.Format ("    ST: {0} ", -1 * StateAlterations [0].Potency));
			if (StateAlterations [0].Probability < 1) {
				output += string.Format (tabbed + string.Format ("({0}%)" + newLine, (int)(stateAlterations [0].Probability * 100)));
			} else {
				output += string.Format (tabbed + @" " + newLine);
			}
		} else {
			output += string.Format (tabbed + @" " + newLine);
		}

		//output += string.Format (tabbed);
		if (!Movement) {
			output += string.Format (tabbed + @"AFFECT RANGE:");
			if (OTG && Mid && High && Air && HighAir)
			{
				output += string.Format(tabbed + @" All Hit Areas");
			}
			else
			{
				if (OTG)
				{
					output += string.Format(tabbed + @" OTG");
				}
				if (Mid)
				{
					output += string.Format(tabbed + @" Mid");
				}
				if (High)
				{
					output += string.Format(tabbed + @" High");
				}
				if (Air)
				{
					output += string.Format(tabbed + @" Air");
				}
				if (HighAir)
				{
					output += string.Format(tabbed + @" High Air");
				}
				if (EqualLevel)
				{
					output += string.Format(tabbed + @" Equal Level");
				}
			} if (Low) {
				output += string.Format (tabbed + @" (Low)");
			}
			if (Overhead) {
				output += string.Format (tabbed + @" (Overhead)");
			}
			output += string.Format (tabbed + newLine);
		}

		if (Reps > 1) {
			output += string.Format (tabbed + @"REPS: {0}", Reps + newLine);
		}

		output += string.Format (tabbed + @"RANGE: " + Range + newLine);

		output += string.Format (tabbed + @"WIDTH/RADIUS: " + WidthOrRadius + newLine);

		output += string.Format (tabbed + @"THRESHOLD: " + Threshold + newLine);

		if (Force.Potency != 0) {
			output += string.Format (tabbed + @"FORCE: {0}", Force.Potency + newLine);
		}
		if (NumUses > 0) {
			output += string.Format (tabbed + @"USES: {0}", NumUses + newLine);
		}
		if (Speed != 0) {
			output += string.Format (tabbed + @"SPEED: {0}", Speed + newLine);
		}
		if (HitStun != 0) {
			output += string.Format (tabbed + @"HIT STUN: {0}", HitStun + newLine);
		}
		if (MotionRange != 0) {
			output += string.Format (tabbed + @"MOTION RANGE: {0}", MotionRange + newLine);
		}
		if (Recovery > 0) {
			output += string.Format (tabbed + @"RECOVERY: {0}", Recovery + newLine);
		}
		if (Malicious) {
			output += string.Format (tabbed + @"HIT RATE: {0}%" + newLine, ((int)(hitChance (Owner) * 100)));
		}
		if (Timer != 0) {
			output += string.Format (tabbed + @"TIME: " + Timer + newLine);
		}
		if (Ratio < .5) {
			output += string.Format (tabbed + @"RATIO: {0}% Magic" + newLine, ((int)((1 - Ratio) * 100)));
		} else if (Ratio > .5) {
			output += string.Format (tabbed + @"RATIO: {0}% Physical" + newLine, ((int)((Ratio) * 100)));
		} else {
			output += string.Format (tabbed + @"RATIO: Balanced" + newLine);
		}

		//PROPERTIES
		output += string.Format (tabbed + @"PROPERTIES: " + newLine);
		int numProperties = 0;

		if (Properties.Contains ("SPIRALDMG") || Properties.Contains ("SPIRALHP")) {
			output += string.Format (tabbed + @"   Spiral Damage");
		}
		if (Properties.Contains ("LEAP")) {
			output += string.Format (tabbed + @"   Leap");
		}
		if (Properties.Contains ("CTRKD")) {
			output += string.Format (tabbed + @"   Counter Knockdown");
		}
		if (Properties.Contains ("CTRAIR")) {
			output += string.Format (tabbed + @"   Counter Juggle");
		}
		if (Properties.Contains ("CTRFREE")) {
			output += string.Format (tabbed + @"   Counter Reset");
		}
		if (Properties.Contains ("AIRFREE")) {
			output += string.Format (tabbed + @"   Juggle Reset");
		}
		if (Properties.Contains ("GNDFREE")) {
			output += string.Format (tabbed + @"   Ground Reset");
		}
		if (Properties.Contains ("STANDONLY")) {
			output += string.Format (tabbed + @"   Standing Link");
		}
		if (Properties.Contains ("AIRONLY")) {
			output += string.Format (tabbed + @"  Juggle Link");
		}
		if (Properties.Contains ("GROUNDONLY")) {
			output += string.Format (tabbed + @"   Ground Link");
		}
		if (Properties.Contains ("CTRONLY")) {
			output += string.Format (tabbed + @"   Counter Link");
		}
		if (CanAct) {
			output += string.Format (tabbed + @"   Action");
		}
		if (Properties.Contains ("FLIP")) {
			output += string.Format (tabbed + @"   Unit Flip");
		}
		if (Properties.Contains ("TOSS")) {
			output += string.Format (tabbed + @"   Target Toss");
		}
		if (JumpReset) {
			output += string.Format (tabbed + @"   Reset Jump");
		}
		if (MoveReset) {
			output += string.Format (tabbed + @"   Reset Movement");
		}
		if (CanBeLearned) {
			output += string.Format (tabbed + @"   Learn");
			if (CanBeLearnedBase) {
				output += string.Format ("^");
			}
			if (CanBeLearnedSpecial) {
				output += string.Format ("º");
			}
		}
		if (Continuous) {
			output += string.Format ("   Continuous");
		}
		output += string.Format (" " + newLine);

		output += "ELEMENTS:   " + newLine + ElementsBasic;
		output += "STAT EFFECTS:   " + newLine + StatAugmentsBasic;
		output += "STATUS EFFECTS:   " + newLine + StateAugmentsBasic + newLine;
		if (Conditions.Length > 0) {
			output += "CONDITIONS:   " + ConditionDisplay + newLine;
		}

		output += string.Format (" " + newLine);

		Skill basedSkill = nextSkill (this);
		/**
		while (basedSkill.LinkSkills.Count > 0) {
			if (basedSkill.Properties.Contains ("AUTOLINK ")) {
				basedSkill = (Skill)basedSkill.LinkSkills [0];
			} else {
				break;
			}
		}
		*/

		if (basedSkill.LinkSkills.Count > 0) {

			if (basedSkill.Equals(this)
				&& Properties.Contains ("PCANCEL "))
			{
				output += string.Format(newLine + tabbed + @"   *CANCEL*" + newLine);
			}
			else
			{
				output += string.Format(tabbed + @"FOLLOW-UPS:" + newLine);
				for (int i = 0; i < basedSkill.LinkSkills.Count; i++)
				{

					Skill nxt = nextSkill((Skill)basedSkill.LinkSkills[i]);

					//if (!nxt.Properties.Contains(" CHILD ")) {
						output += string.Format (tabbed + @"  " + nxt.Name);
						if (nxt.LinkSkills.Count > 0 && !nxt.Properties.Contains("AUTOLINK ")) {
							if (nxt.Properties.Contains ("PCANCEL")) {
								output += string.Format(" ***");
							} else {
								output += string.Format(" -->");
							}
						}
						if (nxt.CanAct)
						{
							output += string.Format(" <->");
						}
					output += string.Format (tabbed + newLine);
				}
			}
		}

		return output;
	}

	public Skill nextSkill (Skill s)
	{
		if (s.LinkSkills.Count > 0 && (s.Properties.Contains ("AUTOLINK ") || s.Properties.Contains ("RANDOMLINK "))) {
			return nextSkill ((Skill)s.LinkSkills [0]);//((Skill)nextSkill)
		}
		return s;
	}

	public string Type
	{
		get {
			/**
			if (Properties.Contains ("SACTION")) {
				return "S-ACTION";
			}
			*/
			if (Properties.Contains ("THROW ")) {
				return "THROW";
			}
			if (Properties.Contains ("EAT ") || Properties.Contains ("DRINK ")) {
				return "ITEM";
			}
			if (SearchName.Equals ("Cancel")) {
				return "CANCEL";
			}
			if (SearchName.Equals ("AutoGuard")) {
				return "GUARD";
			}
			if (Beneficial) {
				if (Change [0] != 0) {
					return "CURE";
				}
				if (Properties.Contains (" END")) {
					return "HEAL";
				}
				if (Properties.Contains ("STATE")) {
					return  "STATE";
				}
				if (statBuffs > 0) {
					return "BUFF";
				}
				if (statusBuffs > 0) {
					return "BLESS";
				}
				if (Properties.Contains ("ITEM")) {
					return "ITEM";
				}
				return "LIGHTMAGIC";
			}
			if (Malicious) {
				if (Magic && Properties.Contains ("MAPINT")) {
					return "HEAVYMAGIC";
				}
				if (Magic && (Change [0] != 0 || ObjectSkill)) {
					return "DARKMAGIC";
				}
				if (Weapon) {
					return "WEAPON";
				}
				if (Projectile) {
					return "PROJECTILE";
				}
				if (SuperProjectile) {
					return "SPROJECTILE";
				}
				if (SForce) {
					return "FORCE";
				}
				if (Grapple) {
					return "GRAPPLE";
				}
				if (statNerfs > 0) {
					return "NERF";
				}
				if (statusNerfs > 0) {
					return "CURSE";
				}
				if (Magic) {
					return "SPELL";
				}
				if (Physical) {
					if (Properties.Contains (" KICK ")) {
						return "KICK";
					}
					return "PHYSICAL";
				}
				if (Benign) {
					return "THROW";
				}
				if (Movement) {
					return "MOVEMENT";
				}
				return "MALICIOUS";
			}
			if (Movement) {
				return "MOVEMENT";
			}
			if (SelfSkill) {
				if (Properties.Contains ("TAUNT")) {
					return "TAUNT";
				}
				if (CounterSkill != null) {
					return "COUNTER";
				}
				if (ReloadAmmo != "N/A") {
					return "RELOAD";
				}
				if (statBuffs > 0) {
					return "BUFF";
				}
				if (statNerfs > 0) {
					return "NERF";
				}
				if (statusBuffs > 0) {
					return "BOOST";
				}
				if (statusNerfs > 0) {
					return "PENALTY";
				}
				return "SELF";
			}
			return "SKILL";
		}
	}

	public string infoOutput (Player target)
	{
		if (target == null) {
            target = new Player (-1, "", "", "", "", "", "", "", "", 0, 0.0, 0.0, "A", "A", 1, -1, false, true);
			target.setElementalProficiency (0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
			target.setBaseStats (100, 100, 100, 100, 0.9, 0.5, .9, 4, 20, 20, 5);
			target.MyTeam = new Team ("False Team", "FT", 4);
		}
			
		string output = "";//string.Format ("{0}", Name);
		if (target.IsGuarding) {
			output += string.Format (" (Blocking)");
		}

		//output += "" + '\n';

		output += "COST ---   ";
		if (Cost [0] != 0) {
			output += String.Format ("{0} HP   ", Cost [0]);
		}
		if (Cost [1] != 0) {
			output += String.Format ("{0} RM   ", Cost [1]);
		}
		if (Cost [2] != 0) {
			output += String.Format ("{0} GM   ", Cost [2]);
		}
		if (Cost [3] != 0) {
			output += String.Format ("{0} VM   ", Cost [3]);
		}

		if (NumUses >= 0) {
			output += string.Format ("{0} USES   ", NumUses);
		}
		if (!NoConditions && !ExpendableAmmo.Equals ("N/A") && Owner.inventoryGet (ExpendableAmmo) != null) {
			output += string.Format ("{0} USES   ",
				Owner.inventoryGet (ExpendableAmmo).NumUses);
		}
		output += '\n';

		output += "EFFECT ---   ";
		if (Properties.Contains ("RANDOMLINK") || Owner.Confuse.IsActive) {
			output += "???" + '\n';
		} else {

			if (Change [0] != 0 || (target != null && fullOutput (target, Owner.CurrentProration) [0] != 0)) {
				if (target == null) {
					output += String.Format ("{0} HP   ", Change [0]);
				} else {
					output += String.Format ("{0} HP   ", fullOutput (target, Owner.CurrentProration) [0]);
				}
			}
			if (Change [1] != 0 || (target != null && fullOutput (target, Owner.CurrentProration) [1] != 0)) {
				if (target == null) {
					output += String.Format ("{0} RM   ", Change [1]);
				} else {
					output += String.Format ("{0} RM   ", fullOutput (target, Owner.CurrentProration) [1]);
				}
			}
			if (Change [2] != 0 || (target != null && fullOutput (target, Owner.CurrentProration) [2] != 0)) {
				if (target == null) {
					output += String.Format ("{0} GM   ", Change [2]);
				} else {
					output += String.Format ("{0} GM   ", fullOutput (target, Owner.CurrentProration) [2]);
				}
			}
			if (Change [3] != 0 || (target != null && fullOutput (target, Owner.CurrentProration) [3] != 0)) {
				if (target == null) {
					output += String.Format ("{0} VM   ", Change [3]);
				} else {
					output += String.Format ("{0} VM   ", fullOutput (target, Owner.CurrentProration) [3]);
				}
			}
		}
		if (StateAlterations [0].Potency != 0) {
			output += String.Format ("{0} ST ", -1 * StateAlterations [0].Potency);
			if (StateAlterations [0].Probability < 1) {
				output += string.Format ("({0}%)", (int)(stateAlterations [0].Probability * 100));
			}
		}

		output += '\n' + @"BASIC ---    ";
		int numPerLine = 0;
		int numPerLineMax = 2;

		output += string.Format (SkillType.ToUpper () + @"   ");

		if (CanBeLearned) {
			if (numPerLine > numPerLineMax) {
				output += string.Format (" " + '\n');
				numPerLine = 0;
			}

			output += string.Format ("   LRN");
			if (CanBeLearnedBase) {
				output += string.Format ("^");
			}
			if (CanBeLearnedSpecial) {
				output += string.Format ("º");
			}
			output += string.Format ("   ");
			numPerLine++;
		}

		output += "" + '\n';

		//output += "ELEMENT ---   " + Elements;
		//output += "STATS ---   " + StatAugments;
		//output += "STATUS ---   " + StateAugments;
		//output += "CONDITIONS --- " + ConditionDisplay;

		output += FollowUps;

		return output;
	}

	public string Elements
	{
		get {
			string output = "";
			State s;
			for (int i = 0; i < Elementals.Length; i++) {
				s = (State)Elementals [i];
				if (s.Potency > 0) {
					if (i != 0) {
						output += "" + '\n' + @"";
					}
					output += string.Format ("{0}: Lvl. {1} -- {2} Elem. ({3}%)   ",
						s.Name, s.Potency, s.NumTurns, 100 * s.Probability);
				}
			}
			return output + '\n';
		}
	}

	public string ElementsBasic
	{
		get {
			string output = "";
			State s;
			for (int i = 0; i < Elementals.Length; i++) {
				s = (State)Elementals [i];
				if (s.Potency > 0) {
					output += string.Format ('\t' + @"{0}: Lvl. {1} -- {2} Elem. ({3}%)   " + '\n',
						s.Name, s.Potency, s.NumTurns, 100 * s.Probability);
				}
			}
			return output + '\n';
		}
	}


	public string StatAugments
	{
		get {
			string output = "";
			for (int i = 0; i < StatAlterations.Length; i++) {
				if (StatAlterations[i].Probability != 0) {
					if (i > 3 && i < 6) {
						output += string.Format ("{0}: {1} ({2}%)   " + '\n',
							StatAlterations [i].Name, StatAlterations [i].DoublePotency,
							(int)(StatAlterations [i].Probability * 100));
					} else {
						output += string.Format ("{0}: {1} ({2}%)   " + '\n',
							StatAlterations [i].Abbreviation, StatAlterations [i].Potency,
							(int)(StatAlterations [i].Probability * 100));
					}
				}
			}
			return output + '\n';
		}
	}

	public string StatAugmentsBasic
	{
		get {
			string output = "";
			for (int i = 0; i < StatAlterations.Length; i++) {
				if (StatAlterations[i].Probability != 0) {
					if (i > 3 && i < 6) {
						output += string.Format ("{0}: {1} ({2}%)   ",
							StatAlterations [i].Name, StatAlterations [i].DoublePotency,
							(int)(StatAlterations [i].Probability * 100));
					} else {
						output += string.Format ("{0}: {1} ({2}%)   ",
							StatAlterations [i].Abbreviation, StatAlterations [i].Potency,
							(int)(StatAlterations [i].Probability * 100));
					}
				}
			}
			return output + '\n';
		}
	}

	public string StateAugments
	{
		get {
			string output = "";
			int lines = 0;
			for (int i = 1; i < StateAlterations.Length; i++)
			{
				if (StateAlterations [i].Probability != 0)
				{
					if (i != 0) {
						if (lines > 1 && i > StateAlterations.Length - 1) {
							output += string.Format ("" + '\n' + '\n');
						}
					}
					lines++;

					output += string.Format ("{0}: {1} ({2}%)   ",
					StateAlterations [i].Abbreviation, StateAlterations [i].Potency,
						StateAlterations [i].Probability * 100);

				}
			}

			State nextState;
			for (int i = 0; i < ExtraStateAlterations.Count; i++)
			{
				nextState = (State)ExtraStateAlterations [i];
				if (nextState.Probability != 0)
				{
					output += string.Format ("{0}: {1} ({2}%)   ",
						nextState.Abbreviation, nextState.Potency,
						nextState.Probability * 100);
				}
			}
			return output + '\n';
		}
	}

	public string StateAugmentsBasic
	{
		get {
			string output = "";
			for (int i = 1; i < StateAlterations.Length; i++)
			{
				if (StateAlterations [i].Probability != 0)
				{
					output += string.Format ('\t' + @"{0}: {1} ({2}%)",
						StateAlterations [i].Abbreviation, StateAlterations [i].Potency,
						StateAlterations [i].Probability * 100);

				}
			}

			State nextState;
			for (int i = 0; i < ExtraStateAlterations.Count; i++)
			{
				nextState = (State)ExtraStateAlterations [i];
				if (nextState.Probability != 0)
				{
					output += string.Format ('\t' + @"{0}: {1} ({2}%)",
						nextState.Abbreviation, nextState.Potency,
						nextState.Probability * 100);
				}
			}
			return output;
		}
	}

	private string CounterOptions
	{
		get {
			string pr = CounterSkill.Properties, output = "";
			if (pr.Contains (" OTG ")) {
				output += "OTG ";
			} if (pr.Contains (" MID ")) {
				output += "MID ";
			} if (pr.Contains (" HIGH ")) {
				output += "HIGH ";
			} if (pr.Contains (" AIR ")) {
				output += "AIR ";
			} if (pr.Contains (" HIGHAIR ")) {
				output += "HIGHAIR ";
			} if (pr.Contains (" LOW ")) {
				output += "LOW ";
			} if (pr.Contains (" OVERHEAD ")) {
				output += "OVERHEAD ";
			} if (pr.Contains ("PROJECTILE ")) {
				output += "PRJ ";
			} if (pr.Contains ("PROJECTILE-S ")) {
				output += "SPRJ ";
			} if (pr.Contains ("FORCE ")) {
				output += "FRC ";
			} if (pr.Contains ("WEAPON ")) {
				output += "WPN ";
			} if (pr.Contains ("EQUALLEVEL ")) {
				output += "EQL ";
			}
			return output;
		}
	}


	public string HitSection
	{
		get
		{
			string output = "";
			if (Auto) {
				output = "AUTO";
			} else {
				if (Malicious) {
					output = "Hits: ";
				} else {
					output = "";
				}

				if (Projectile) {
					output += "PROJECTILE ";
				} if (SuperProjectile) { 
					output += "SUPER PROJECTILE";
				} if (SForce) {
					output += "FORCE ";
				} if (OTG) {
					output += "OTG ";
				} if (Mid) {
					output += "MID ";
				} if (High) {
					output += "HIGH ";
				} if (Air) {
					output += "AIR ";
				} if (Grapple) {
					output += "GRAPPLE ";
				} if (EqualLevel) {
					output += "EQUALLEVEL ";
				}
			}
			if (Low) {
				output += "(LOW) ";
			} if (Overhead) {
				output += "(OVERHEAD) ";
			}
			return output;
		}
	}

	public string engage (Player target)
	{
		string process = "";
		if (!owner.KOd && !target.KOd) {
			process = string.Format(" mugen {0} {1} ", owner.SearchName, target.SearchName);
			process += string.Format ("-p1.health {0} -p2.health {1} -p1.power {2} -p2.power {3} ",
				owner.Health.MeterLevel, target.Health.MeterLevel,
				owner.Vitality.MeterLevel, target.Vitality.MeterLevel);
			if (owner.BordersWithAlly && !owner.BorderAlly.KOd) {
				Player ally = owner.BorderAlly;
				process += string.Format("-p3 {0} -p3.health {1} -p3.power {2} ",
					ally.SearchName, ally.Health.MeterLevel, ally.Vitality.MeterLevel);
			} if (target.BordersWithAlly && !target.BorderAlly.KOd) {
				Player enemy = target.BorderAlly;
				process += string.Format("-p4 {0} -p4.health {1} -p4.power {2} ",
					enemy.SearchName, enemy.Health.MeterLevel, enemy.Vitality.MeterLevel);
			}
			process += "-log Saves/mugenmatch.txt ";
			return process;
		}
		return "No";
	}

	public Boolean properAllegiance (Player target)
	{
		if (Properties.Contains ("TARGETCRIT")) {
			if (!target.Critical) {
				return false;
			}
		}

		//SINGLE ALLEGIANCE
		if (Enemies && !Allies && !Self) {
			return !owner.sameTeam (target);
		} else if (Allies && !Enemies && !Self) {
			return owner.sameTeam (target) && !owner.Equals (target);
		} else if (!Allies && !Enemies && Self) {
			return owner.Equals (target);
		}

		//DOUBLE
		else if (Allies && Enemies && !Self) {
			return !owner.Equals (target);
		} else if (!Allies && Enemies && Self) {
			return!owner.sameTeam (target) || owner.Equals (target);
		} else if (Allies && !Enemies && Self) {
			return owner.sameTeam (target);
		}

		//TRIPLE
		else if (Allies && Enemies && Self) {
			return true;
		}
		return false;
	}

	public Boolean properHitSection (Player target)
	{
		if (ObjectSkill) {
			TrapSkill.Owner = Owner;
			TrapSkill.setLocations ();
			return TrapSkill.properHitSection (target);
		}

		if (owner.LockedOnTarget != null && !owner.LockedOnTarget.Equals(target)) {
			return false;
		}

		//SINGLE CONDITIONS
		if (Auto || Self) {
			return true;
		}

		if (Properties.Contains ("TARGCRIT ") && !target.Critical) {
			return false;
		}

		if (!owner.currentMap ().locationInHeightRange (target.currentLocation (), this)) {
			return false;
		}

		if (!owner.currentMap ().lineSkillCanReach (this, target.currentLocation ())) {
			return false;
		}

		if (Grapple && ((target.HurtStun > 0 || target.GuardStun > 0) && !target.Airborne.IsActive && !Properties.Contains ("INAIR "))) {
			return false;
		}

		//SAMELEVEL
		if (EqualLevel) {

			if (!owner.Airborne.IsActive && !owner.Grounded.IsActive) {
				return !target.Grounded.IsActive && !owner.Grounded.IsActive;
			}

			if (owner.Grounded.IsActive) {
				return target.Grounded.IsActive;
			}

			if (owner.Airborne.IsActive) {
				return (!target.Grounded.IsActive || Properties.Contains (" FOLLOW "))
					&& Math.Abs (owner.Airborne.Potency - target.Airborne.Potency) <= 4;
			}


			//return owner.Airborne.IsActive && target.Airborne.IsActive Math.Abs(target.Height - owner.Height) < 3;
		}

        if (Properties.Contains ("ALLABOVE"))
        {
            return Owner.currentHeight() + Owner.Potency(Owner.Airborne) <= target.currentHeight() - target.Potency(target.Airborne);
        }

        if (Properties.Contains ("ALLBELOW"))
        {
            return Owner.currentHeight() + Owner.Potency(Owner.Airborne) >= target.currentHeight() - target.Potency(target.Airborne);
        }


        //SINGLE CONDITIONS
        if (OTGOnly || (owner.Grounded.IsActive && EqualLevel)) {
			return target.IsKnockedDown;
		} else if (MidOnly) {
			if (Grapple) {
				return !target.Airborne.IsActive && !target.Grounded.IsActive;
			}
			return target.IsStanding || target.IsCrouching || (target.Airborne.IsActive && target.Airborne.Potency <= 1);
		} else if (HighOnly) {
			return !target.Grounded.IsActive
				&& ((target.IsStanding && target.Height > owner.Height - 2)	|| (target.IsCrouching && target.Height > owner.Height + 2)
			|| (target.Airborne.IsActive && target.Airborne.Potency <= 10));
		} else if (AirOnly) {
			return (target.Airborne.Potency > 0 && target.Airborne.Potency <= 20) || (target.UnitHeight - Owner.Height > 5);
		} else if (HighAirOnly) {
			return target.Airborne.Potency > 20;
		}

		//DOUBLE CONDITIONS
		else if ((OTG && Mid && !High && !Air && !HighAir) || (EqualLevel && !owner.Airborne.IsActive)) {
			return !target.Airborne.IsActive;
		} else if (!OTG && Mid && High && !Air && !HighAir) {
			return (target.IsStanding) || (!target.IsKnockedDown && target.Airborne.Potency <= 10);
		} else if (!OTG && !Mid && High && Air && !HighAir) {
			return (!target.IsKnockedDown || (target.IsStanding && target.Height > owner.Height - 2))
				&& (!target.IsCrouching || target.Height > owner.Height + 2) && target.Airborne.Potency <= 20;
		} else if (!OTG && !Mid && !High && Air && HighAir) {
			return target.Airborne.IsActive;
		}

		//TRIPLE CONDITIONS
		else if (OTG && Mid && High && !Air && !HighAir) {
			return !target.Airborne.IsActive || target.Airborne.Potency <= 10;
		} else if (!OTG && Mid && High && Air && !HighAir) {
			return !target.Grounded.IsActive && target.Airborne.Potency <= 20;
		} else if (!OTG && !Mid && High && Air && HighAir) {
			return !target.Grounded.IsActive;
		}

		//QUADRUPLE CONDITIONS
		else if (OTG && Mid && High && Air && !HighAir) {
			return target.Airborne.Potency <= 20;
		} else if (!OTG && Mid && High && Air && HighAir) {
			return !target.Grounded.IsActive;
		}

		//ALL CONDITIONS
		else if (OTG && Mid && High && Air && HighAir) {
			return true;
		}

		return false;
	}

	public Boolean willConnectToTarget (Player target)
	{
		if (target == null) {
			return false;
		}
		if (ObjectSkill) {
			return TrapSkill.willConnectToTarget (target);
		}
		return target != null && properAllegiance (target) && properHitSection (target);
	}
	//{ return properHitSection(target);}


	public Boolean CostsAndConditionsMet
	{
		get {
			return CostsMet && (ConditionsMet || Properties.Contains ("SKIP "));
		}
	}

	public Boolean costsAndConditionsMet (int[] i) {
		return costsMet (i) && ConditionsMet;
	}

	public Boolean Physical
	{
		get { return physical;}
		set { physical = value;}
	}

	public Boolean canCounter (Skill s)
	{
		string counterSection = Properties.Substring (Properties.IndexOf ("|") + 1);

		if (s.Physical && !counterSection.Contains ("PHYSICAL")) {
			return false;
		}

		if (s.Weapon && !counterSection.Contains ("WEAPON")) {
			return false;
		}

		if (s.Magic && !counterSection.Contains ("MAGIC")) {
			return false;
		}

		if (s.Overhead && !counterSection.Contains(" OVERHEAD ")) {
			return false;
		}

		if (s.Low && !counterSection.Contains (" LOW ")) {
			return false;
		}

		if (s.OTG && !counterSection.Contains (" OTG ")) {
			return false;
		}

		if (s.Mid && !counterSection.Contains (" MID ")) {
			return false;
		}

		if (s.High && !counterSection.Contains (" HIGH ")) {
			return false;
		}

		if (s.Air && !counterSection.Contains (" AIR ")) {
			return false;
		}

		if (s.Grapple && !counterSection.Contains("GRAPPLE ")) {
			return false;
		}

		if (s.Projectile && !counterSection.Contains("PROJECTILE ")) {
			return false;
		}

		if (s.SuperProjectile && !counterSection.Contains("PROJECTILE-S ")) {
			return false;
		}

		if (s.SForce && !counterSection.Contains("FORCE ")) {
			return false;
		}
		return true;
	}

	public Boolean sameLevel(Skill s)
	{
		return (Projectile && s.Projectile) || (SuperProjectile && s.SuperProjectile) || (SForce && s.SForce);
	}

	public Boolean higherLevel (Skill s)
	{
		return (SForce && (s.SuperProjectile || s.Projectile)) || (SuperProjectile && s.Projectile);
	}

	public Boolean lowerLevel (Skill s)
	{
		return (s.SForce && (SuperProjectile || Projectile)) || (s.SuperProjectile && Projectile);
	}

	public ArrayList HitTargets
	{
		get { return hitTargets;}
		set { hitTargets = value;}
	}

	public ArrayList AllTargets
	{
		get { return allTargets;}
		set { allTargets = value; }
	}

	public int Experience
	{
		get {
			return experience;
		}
		set {
			experience = value;
		}
	}

	public int Mastery
	{
		get {
			return mastery;
		}
		set {
			mastery = value;
		}
	}

	public int MasteryDisplay
	{
		get {
			if (Mastery > 1 && Mastery != 101) {
				return Experience - MasteryCheckpoints [Mastery];
			}
			if (Mastery == 10) {
				return 0;
			}
			return Experience;
		}
	}

	public string MasteryCheckpointDisplay
	{
		get {
			if (Mastery <= 20) {
				if (Mastery > 1) {
					return "" + (MasteryCheckpoints [Mastery + 1] - MasteryCheckpoints [Mastery]);
				}
				return "" + (MasteryCheckpoints [Mastery + 1]);
			}
			return "∞";
		}
	}

	public int MasteryCheckpointDisplayInt
	{
		get {
			if (Mastery < 10) {
				if (Mastery > 1) {
					return (MasteryCheckpoints [Mastery + 1] - MasteryCheckpoints [Mastery]);
				}
				return (MasteryCheckpoints [Mastery + 1]);
			}
			return 1;
		}
	}

	public int[] MasteryCheckpoints
	{
		get {
			return masteryCheckpoints;
		}
		set {
			masteryCheckpoints = value;
		}
	}

	public void setMasteryCheckpoints ()
	{
		int checkpoint = 200;
		masteryCheckpoints [0] = 0;
		masteryCheckpoints [1] = 50;

		for (int i = 2; i < masteryCheckpoints.Length; i++) {
			masteryCheckpoints [i] = checkpoint;
			checkpoint = (int)(((double)checkpoint * 3) / (double)1.8);
		}
	}

	public Boolean isSame (string s)
	{
		if (searchName.Equals (s)) {
			return true;
		}
		//if (Child != null) {
		//	return MasterSkill.isSame (s);
		//}
		return false;
	}

	public Boolean Matches (Skill s)
	{
		return s != null && (name.Equals (s.Name)) && !this.Equals (s);
	}
}


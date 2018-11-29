using System;
using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.IO;
using UnityEngine.UI;

public class Player : Locatable, IComparable
{
    private Boolean manualMode;
    private Rigidbody2D body;
    private Skill activeSkill;
    private float groundBound;
    private float horizontalForce;
    private float verticalForce;
    private string horizontalPattern;
    private string verticalPattern;
    private string jumpDirection;
    private RawImage hurtBox;
    private RawImage hitBox;
    private Text spriteInfo;
    private Text hitboxInfo;
    private Text hurtboxInfo;
    private float ssr;
    private Transform parentCanvas;
    private Boolean tacticalMode;
    private int lengthCheck;
    private float movementSpeed;
    private ArrayList checkedInput;
    private Skill cancelSkill;
    private Boolean canCancelSkill;

    private RawImage infoFrame;
    private Text nameDisplay;
    private RawImage infoPortrait;
    private Text infoFrameBlurb;
    private Text classRatioInfo;
    private RawImage infoHealthBar;
    private RawImage infoHealthBarBack;
    private RawImage infoHealthBarEffect;
    private RawImage infoRushBar;
    private RawImage infoRushBarBack;
    private RawImage infoRushBarEffect;
    private RawImage infoGuardBar;
    private RawImage infoGuardBarBack;
    private RawImage infoGuardBarEffect;
    private RawImage infoVitalityBar;
    private RawImage infoVitalityBarBack;
    private RawImage infoVitalityBarEffect;
    private RawImage infoStunBar;
    private RawImage infoStunBarBack;
    private RawImage infoStunBarEffect;
    private int transformationTime;


    private Player lastTarget;
    private int saves;
    private int defeats;

    private Texture2D queuedTexture;

    private Boolean noGainForMaster;
    private ArrayList usedColors;
    private float colorTimer;
    private int colorIndex;
    private Boolean hasActedInRound;
    private float activeMatchTime;
    private float activeTimeModulus;
    private float spriteScale;
    private int index;
    private string firstName;
    private string surname;
    private string searchName;
    private string sex;
    private string sexualPreference;
    private string species;
    private string nationality;
    private int age;
    private double weight;
    private double height;
    private string playerClass;
    private string assimilatedClass;
    private int assimilatedTime;
    private string style;
    private string playStyle;
    private string stageName;
    private string handDexterity;
    private int ratio;
    private Location location;
    private System.Random r;
    private Boolean engaging;
    private int jumpHeight;
    private int unitHeight;
    private Player owner;
    private int objectIndex;
    private Boolean turnEnded;
    private int cooldown;
    private string direction;
    private int experience;
    private int level;
    private int[] experienceCheckpoints;
    private Boolean specialState;
    private Boolean lastStand;
    private Boolean airOff;
    private ArrayList noAmmo;
    private int hitStunAdjustment;
    private int numRests;
    private Boolean skillExecuted;
    private int highestCombo;
    private ArrayList playerOutputIndexes;
    private Color color;
    private ArrayList emotions;
    private int[] spriteRatio;
    private RawImage[] sprites;
    private Boolean target;
    private float currentAnimationTime;
    private ArrayList currentAnimationTimes;
    private Boolean staggered;
    private string staggerDirection;
    private int staggerTime;
    private ArrayList importantColors;
    private ArrayList colors;
    private Boolean champion;

    private ArrayList opponents;

    private int combo;
    private int comboDamagePerRound;

    private RawImage mapSprite;

    public ArrayList normalAddedSkills;
    public ArrayList specialAddedSkills;
    public ArrayList vitalityAddedSkills;
    public ArrayList burstAddedSkills;
    public ArrayList sactionAddedSkills;
    public ArrayList jumpAddedSkills;
    public ArrayList itemAddedSkills;
    public ArrayList singleAddedSkills;
    public ArrayList mapOutputs;

    public DynamicInt strength;
    public DynamicInt grit;
    public DynamicInt magick;
    public DynamicInt resistance;
    public DynamicDouble dexterity;
    public DynamicDouble speed;
    public int speedBonuses;
    public DynamicDouble proration;
    public DynamicInt movement;
    public DynamicInt teamwork;
    public DynamicInt luck;
    public DynamicInt actionPoints;
    private int movesRemaining;
    private int fatigue;
    private int residualFatigue;
    private double currentProration;
    private double currentHitRate;
    private string engageText;
    private Player counterPlayer;
    private Boolean sActionEnabled;
    private Boolean reverseName;
    private ArrayList trackSkills;
    private ArrayList longestCombo;
    private string longestComboText;
    private int mapSize;
    private ArrayList pointSpace;

    private StreamWriter turnOutput;

    private Boolean firstTurn;
    private Boolean noMotion;

    private ArrayList timedConditions;

    private State[] states;
    private DynamicDouble[] stateResistances;

    private int[] elementalOffense;
    private int[] elementalDefense;
    private int timeRemaining;

    private Meter health;
    private Meter rush;
    private Meter guard;
    private Meter vitality;
    private Meter stun;
    private int criticalLimit;
    private int hurtStun;
    private Boolean inMovement;
    private int healthPresent;
    private int vitalityPresent;

    private Boolean hasChangedLocations;
    private Boolean hasChangedHeights;
    private float paceCancelCooldownTimer;

    private Boolean isCrouching;
    private Boolean isGuarding;
    private Boolean autoGuard;
    private Boolean isDizzied;
    private State guarding;
    private Boolean canAct;
    private Boolean hasActed;
    private Boolean hasJumped;
    private Boolean wasDizzied;
    private Boolean wasCritical;
    private Boolean isResting;
    private Boolean isTaunting;
    private int tauntDecay;
    private Boolean wasHit;
    private Boolean inRecovery;
    private Boolean hasFallen;
    private Boolean canRest;
    private Boolean canBeRevived;
    private Boolean criticalActive;
    private Boolean bonusRoundActivated;
    private Boolean hitMaliciously;
    private Boolean undead;
    private Boolean halfCost;
    private Boolean noCost;

    private Boolean leech;
    private Boolean counterState;

    private int previousTileHeight;

    private Player playerStandard;

    private ArrayList allSkills;
    private ArrayList normalSkills;
    private ArrayList specialSkills;
    private ArrayList vitalitySkills;
    private ArrayList burstSkills;
    private ArrayList jumpSkills;
    private ArrayList itemSkills;
    private ArrayList inventory;

    public RawImage healthBar;
    public RawImage healthBarBackground;

    public RawImage statusIcon;

    public RawImage shadowSprite;

    private Skill sActionSkill;
    private Skill counterSkill;
    private Skill criticalSkill;
    private Skill contactSkill;
    private Skill koSkill;
    private Skill tauntSkill;

    private int damagePerRound;
    private int damageTotal;

    private int connectsPerRound;
    private int attacksPerRound;
    private int connectsTotal;
    private int attacksTotal;

    private int healingPerRound;
    private int healingTotal;

    private int damageTakenPerRound;
    private int damageTakenTotal;

    private int statusAfflictionsPerRound;
    private int statusAfflictionsTotal;

    private int statusAidsPerRound;
    private int statusAidsTotal;

    private int stunPerRound;
    private int stunTotal;

    private int victories;

    private int fallingHeight;
    private int landingHeight;
    private Boolean inPlay;
    private Boolean onField;

    private float bonusTime;

    private int guardStun;

    private ArrayList timeActivatedSkills;

    private Team myTeam;

    private string[] conditions;

    private Voice voice;

    private Map playerMap;

    //All Special Conditions
    private Boolean vmGainBlocked;
    private Boolean weakFrame;
    private Boolean bloodPrice;
    private Boolean flight;
    private Boolean berserk;
    private Boolean timeStopped;
    private Boolean noGuard;
    private Boolean noConditions;
    private Boolean canFly;

    private ArrayList chainSkills;
    private ArrayList burstFollowUpSkills;
    private Player lockedOnTarget;

    private Player storedPlayer;
    private Player absorbedPlayer;

    private string element;

    Reel myReel;
    Reel[] allSprites;
    Reel[] winSprites;
    Reel[] loseSprites;

    private float dizzyTime;
    private float stumbleTime;

    private Boolean completed;

    private Vector3 destination;
    private ArrayList destinations;
    private Vector3 originalPosition;
    private MapVector movementIncrement;
    private Boolean movementDirection;

    private ArrayList movementTextures;
    private ArrayList movementIncrements;
    private ArrayList movementDirections;
    private ArrayList originalPositions;

    private Texture2D portrait;
    private Texture2D portraitCritical;

    private Texture2D neutral;
    private Texture2D crouchNeutral;
    private Texture2D groundNeutral;
    private Texture2D airNeutral;

    private Texture2D damage;
    private Texture2D crouchDamage;
    private Texture2D groundDamage;
    private Texture2D airDamage;

    private Boolean activePlayer;

    private string loadedInfo;

    private Team opposingTeam;

    //DUEL MODE INFO
    private float hitStunRemaining;
    private float hurtStunRemaining;
    private float blockStunRemaining;
    private Boolean canInput;
    private Boolean canChangeDirection;
    private Boolean counterHit;
    private Boolean inMotion;

    private KeyCode inputUp,
    inputDown,
    inputLeft,
    inputRight,
    inputForward,
    inputBack,
    inputNorth,
    inputEast,
    inputSouth,
    inputWest,
    inputA,
    inputB,
    inputC,
    inputD,
    inputY,
    inputZ,
    inputPause,
    inputConfirm,
    inputCancel,
        inputPaceCancel;

    //MULTIPLE INPUTS
    public ArrayList inputUpForward,
    inputDownForward,
    inputDownBack,
    inputUpBack,
    inputAB,
    inputAC,
    inputAD,
    inputBC,
    inputBD,
    inputCD,
    inputYZ,
    inputABC,
    inputABD,
    inputABY,
    inputACD,
    inputBCD,
    inputCDZ,
    inputABCD;

    /**
            p.TeamInfoDisplay = team1Names [teamIndex];
            p.TeamHealthBar = team1HealthBars [teamIndex];
            p.TeamHealthBarBackground = team1HealthBarBackgrounds [teamIndex];
            p.TeamRushBar = team1HealthBars[teamIndex];
            p.TeamRushBarBackground = team1HealthBarBackgrounds[teamIndex];
            p.TeamGuardBar = team1HealthBars[teamIndex];
            p.TeamGuardBarBackground = team1HealthBarBackgrounds[teamIndex];
            p.TeamVitalityBar = team1HealthBars[teamIndex];
            p.TeamVitalityBarBackground = team1HealthBarBackgrounds[teamIndex];
            p.TeamStunBar = team1HealthBars[teamIndex];
            p.TeamSprite = team1TargetSprites[teamIndex];
            p.TeamTimer = team1Timers[teamIndex];

 */
    private Text teamInfoDisplay;
    private RawImage teamHealthBar;
    private RawImage teamHealthBarEffect;
    private RawImage teamHealthBarBackground;
    private RawImage teamRushBar;
    private RawImage teamRushBarBackground;
    private RawImage teamGuardBar;
    private RawImage teamGuardBarBackground;
    private RawImage teamVitalityBar;
    private RawImage teamVitalityBarBackground;
    private RawImage teamStunBar;
    private RawImage teamSprite;
    private float teamTimer;

    private float vitalityTimer;
    private float stunTimer;

    private float statusIconTimer;
    private float healthAmt;

    public float teamHealthBarEffectAmount
    {
        get { return healthAmt; }
        set { healthAmt = value; }
    }

    public float StatusIconTimer
    {
        get { return statusIconTimer; }
        set { statusIconTimer = value; }
    }

    public float VitalityTimer
    {
        get { return vitalityTimer; }
        set { vitalityTimer = value; }
    }

    public float StunTimer
    {
        get { return stunTimer; }
        set { stunTimer = value; }
    }

    public RawImage TeamHealthBar
    {
        get { return teamHealthBar; }
        set { teamHealthBar = value; }
    }

    public RawImage TeamHealthBarEffect
    {
        get { return teamHealthBarEffect; }
        set { teamHealthBarEffect = value; }
    }

    public RawImage TeamHealthBarBackground
    {
        get { return teamHealthBarBackground; }
        set { teamHealthBarBackground = value; }
    }

    public RawImage TeamRushBar
    {
        get { return teamRushBar; }
        set { teamRushBar = value; }
    }

    public RawImage TeamRushBarBackground
    {
        get { return teamRushBarBackground; }
        set { teamRushBarBackground = value; }
    }

    public RawImage TeamGuardBar
    {
        get { return teamGuardBar; }
        set { teamGuardBar = value; }
    }

    public RawImage TeamGuardBarBackground
    {
        get { return teamGuardBarBackground; }
        set { teamGuardBarBackground = value; }
    }

    public RawImage TeamVitalityBar
    {
        get { return teamVitalityBar; }
        set { teamVitalityBar = value; }
    }

    public RawImage TeamVitalityBarBackground
    {
        get { return teamVitalityBarBackground; }
        set { teamVitalityBarBackground = value; }
    }

    public RawImage TeamStunBar
    {
        get { return teamStunBar; }
        set { teamStunBar = value; }
    }

    public RawImage TeamSprite
    {
        get { return teamSprite; }
        set { teamSprite = value; }
    }

    public Text TeamInfoDisplay
    {
        get { return teamInfoDisplay; }
        set { teamInfoDisplay = value; }
    }

    public float TeamTimer
    {
        get { return teamTimer; }
        set { teamTimer = value; }
    }

    public float PaceCancelCooldownTimer
    {
        get { return paceCancelCooldownTimer; }
        set { paceCancelCooldownTimer = value; }
    }



    public RawImage InfoPortrait
    {
        get {
            return infoPortrait;
        }
        set {
            infoPortrait = value;
        }
    }

    public Text ClassRatioInfo
    {
        get
        {
            return classRatioInfo;
        }
        set
        {
            classRatioInfo = value;
        }
    }

    public Text InfoFrameBlurb
    {
        get {
            return infoFrameBlurb;
        }
        set {
            infoFrameBlurb = value;
        }
    }

    public RawImage InfoHealthBar
    {
        get
        {
            return infoHealthBar;
        }
        set
        {
            infoHealthBar = value;
        }
    }

    public RawImage InfoHealthBarBack
    {
        get
        {
            return infoHealthBarBack;
        }
        set
        {
            infoHealthBarBack = value;
        }
    }

    public RawImage InfoHealthBarEffect
    {
        get
        {
            return infoHealthBarEffect;
        }
        set
        {
            infoHealthBarEffect = value;
        }
    }

    public RawImage InfoRushBar
    {
        get
        {
            return infoRushBar;
        }
        set
        {
            infoRushBar = value;
        }
    }

    public RawImage InfoRushBarBack
    {
        get
        {
            return infoRushBarBack;
        }
        set
        {
            infoRushBarBack = value;
        }
    }

    public RawImage InfoRushBarEffect
    {
        get
        {
            return infoRushBarEffect;
        }
        set
        {
            infoRushBarEffect = value;
        }
    }

    public RawImage InfoGuardBar
    {
        get
        {
            return infoGuardBar;
        }
        set
        {
            infoGuardBar = value;
        }
    }

    public RawImage InfoGuardBarBack
    {
        get
        {
            return infoGuardBarBack;
        }
        set
        {
            infoGuardBarBack = value;
        }
    }

    public RawImage InfoGuardBarEffect
    {
        get
        {
            return infoGuardBarEffect;
        }
        set
        {
            infoGuardBarEffect = value;
        }
    }

    public RawImage InfoVitalityBar
    {
        get
        {
            return infoVitalityBar;
        }
        set
        {
            infoVitalityBar = value;
        }
    }

    public RawImage InfoVitalityBarBack
    {
        get
        {
            return infoVitalityBarBack;
        }
        set
        {
            infoVitalityBarBack = value;
        }
    }

    public RawImage InfoVitalityBarEffect
    {
        get
        {
            return infoVitalityBarEffect;
        }
        set
        {
            infoVitalityBarEffect = value;
        }
    }

    public RawImage InfoStunBar
    {
        get
        {
            return infoStunBar;
        }
        set
        {
            infoStunBar = value;
        }
    }

    public RawImage InfoStunBarBack
    {
        get
        {
            return infoStunBarBack;
        }
        set
        {
            infoStunBarBack = value;
        }
    }

    public RawImage InfoStunBarEffect
    {
        get
        {
            return infoStunBarEffect;
        }
        set
        {
            infoStunBarEffect = value;
        }
    }


    public RawImage InfoFrame
    {
        get {
            return infoFrame;
        }
        set {
            infoFrame = value;
        }
    }

    public Text NameDisplay
    {
        get {
            return nameDisplay;
        }
        set {
            nameDisplay = value;
        }
    }

    ArrayList inputs,
    currentInput;

    ArrayList allInputs;

    public Boolean CorrespondingInput (KeyCode inpt)
    {
        KeyCode current;
        for (int i = 0; i < allInputs.Count; i ++) {
            current = (KeyCode)allInputs[i];
            if (current == inpt) {
                return true;
            }
        }
        return false;
    }

    public Skill CancelSkill
    {
        get
        {
            return cancelSkill;
        }
        set
        {
            cancelSkill = value;
        }
    }

    public Boolean CanCancelSkill
    {
        get
        {
            return canCancelSkill;
        }
        set
        {
            canCancelSkill = value;
        }
    }

    public Text SpriteInfo
    {
        get {
            return spriteInfo;
        }
        set {
            spriteInfo = value;
        }
    }

    public Text HitboxInfo
    {
        get {
            return hitboxInfo;
        }
        set {
            hitboxInfo = value;
        }
    }

    public Text HurtboxInfo
    {
        get
        {
            return hurtboxInfo;
        }
        set
        {
            hurtboxInfo = value;
        }
    }

    public RawImage HitBox
    {
        get {
            return hitBox;
        }
        set {
            hitBox = value;
        }
    }

    public RawImage HurtBox
    {
        get
        {
            return hurtBox;
        }
        set {
            hurtBox = value;
        }
    }


    public KeyCode InputUp
    {
        get
        {
            return inputUp;
        }
        set
        {
            inputUp = value;
        }
    }

    public KeyCode InputDown
    {
        get
        {
            return inputDown;
        }
        set
        {
            inputDown = value;
        }
    }

    public KeyCode InputLeft
    {
        get
        {
            return inputLeft;
        }
        set
        {
            inputLeft = value;
        }
    }

    public KeyCode InputRight
    {
        get
        {
            return inputRight;
        }
        set
        {
            inputRight = value;
        }
    }

    public KeyCode InputA
    {
        get
        {
            return inputA;
        }
        set
        {
            inputA = value;
        }
    }

    public KeyCode InputB
    {
        get
        {
            return inputB;
        }
        set
        {
            inputB = value;
        }
    }

    public KeyCode InputC
    {
        get
        {
            return inputC;
        }
        set
        {
            inputC = value;
        }
    }

    public KeyCode InputD
    {
        get
        {
            return inputD;
        }
        set
        {
            inputD = value;
        }
    }

    public KeyCode InputY
    {
        get
        {
            return inputY;
        }
        set
        {
            inputY = value;
        }
    }

    public KeyCode InputZ
    {
        get
        {
            return inputZ;
        }
        set
        {
            inputZ = value;
        }
    }

    public KeyCode InputPause
    {
        get
        {
            return inputPause;
        }
        set
        {
            inputPause = value;
        }
    }

    public KeyCode InputCancel
    {
        get
        {
            return inputCancel;
        }
        set
        {
            inputCancel = value;
        }
    }


    public KeyCode InputPaceCancel
    {
        get
        {
            return inputPaceCancel;
        }
        set
        {
            inputPaceCancel = value;
        }
    }

    public KeyCode InputConfirm
    {
        get
        {
            return inputConfirm;
        }
        set
        {
            inputConfirm = value;
        }
    }

    public ArrayList InputUpForward
    {
        get
        {
            return inputUpForward;
        }
        set
        {
            inputUpForward = value;
        }
    }

    public ArrayList InputDownForward
    {
        get
        {
            return inputDownForward;
        }
        set
        {
            inputDownForward = value;
        }
    }

    public ArrayList InputDownBack
    {
        get
        {
            return inputDownBack;
        }
        set
        {
            inputDownBack = value;
        }
    }

    public ArrayList InputUpBack
    {
        get
        {
            return inputUpBack;
        }
        set
        {
            inputUpBack = value;
        }
    }

    public ArrayList InputAB
    {
        get
        {
            return inputAB;
        }
        set
        {
            inputAB = value;
        }
    }

    public ArrayList InputAC
    {
        get
        {
            return inputAC;
        }
        set
        {
            inputAC = value;
        }
    }

    public ArrayList InputAD
    {
        get
        {
            return inputAD;
        }
        set
        {
            inputAD = value;
        }
    }

    public ArrayList InputBC
    {
        get
        {
            return inputBC;
        }
        set
        {
            inputBC = value;
        }
    }

    public ArrayList InputBD
    {
        get
        {
            return inputBD;
        }
        set
        {
            inputBD = value;
        }
    }

    public ArrayList InputCD
    {
        get
        {
            return inputCD;
        }
        set
        {
            inputCD = value;
        }
    }

    public ArrayList InputYZ
    {
        get
        {
            return inputYZ;
        }
        set
        {
            inputYZ = value;
        }
    }

    public ArrayList InputABC
    {
        get
        {
            return inputABC;
        }
        set
        {
            inputABC = value;
        }
    }

    public ArrayList InputABD
    {
        get
        {
            return inputABD;
        }
        set
        {
            inputABD = value;
        }
    }

    public ArrayList InputABY
    {
        get
        {
            return inputABY;
        }
        set
        {
            inputABY = value;
        }
    }

    public ArrayList InputACD
    {
        get
        {
            return inputACD;
        }
        set
        {
            inputACD = value;
        }
    }

    public ArrayList InputBCD
    {
        get
        {
            return inputBCD;
        }
        set
        {
            inputBCD = value;
        }
    }

    public ArrayList InputCDZ
    {
        get
        {
            return inputCDZ;
        }
        set
        {
            inputCDZ = value;
        }
    }


    public ArrayList InputABCD
    {
        get
        {
            return inputABCD;
        }
        set
        {
            inputABCD = value;
        }
    }

    public KeyCode InputForward
    {
        get
        {
            if (tacticalMode) {
                return inputForward;
            }
            if (direction.Equals("W"))
            {
                return InputLeft;
            }
            return InputRight;
        }
        set {
            inputForward = value;
        }
    }

    public KeyCode InputBack
    {
        get
        {
            if (tacticalMode) {
                return inputBack;
            }
            if (direction.Equals("E"))
            {
                return InputLeft;
            }
            return InputRight;
        }
        set {
            inputBack = value;
        }
    }

    public Team OpposingTeam
    {
        get
        {
            return opposingTeam;
        }
        set
        {
            opposingTeam = value;
        }
    }

    public ArrayList AllInputs
    {
        get {
            return allInputs;
        }
        set {
            allInputs = value;
        }
    }

    public Boolean irrelevantInput (string input)
    {
        if (input == null)
        {
            return true;
        }
        for (int i = 0; i < allInputs.Count; i++)
        {
            ///UnityEngine.Debug.Log (allInputs[i].GetType());
            ///if (!allInputs [i].GetType().Name.Equals ("ArrayList") && ((ArrayList)allInputs[i]).Contains(input)) {
            ///return true;
            ///} 
            if (input.Equals((allInputs[i])))
            {
                return false;
            }
        }
        return true;
    }


    public Boolean relevantInput (string input)
    {
        if (input == null) {
            return true;
        }
        for (int i = 0; i < allInputs.Count; i ++) {
            ///UnityEngine.Debug.Log (allInputs[i].GetType());
            ///if (!allInputs [i].GetType().Name.Equals ("ArrayList") && ((ArrayList)allInputs[i]).Contains(input)) {
            ///return true;
            ///} 
            if (input.Equals ((allInputs[i]))) {
                return true;
            }
        }
        return false;
    }

    public Skill ActiveSkill
    {
        get {
            return activeSkill;
        }
        set {
            activeSkill = value;
        }
    }

    public Boolean Input (string comd)
    {
        if (activeSkill == null)
        {
            for (int i = 0; i < CheckedInput.Count; i++)
            {
                if (CheckedInput[i] == comd)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public Boolean EmptyInput
    {
        get {
            return activeSkill == null && (Input("None") || CheckedInput.Count == 0);
        }
    }

    public string CheckedInputString
    {
        get {

            string output = @"";
            for (int i = 0; i < checkedInput.Count; i++) {
                output += (i + 1) + ")" + checkedInput [i].ToString () + " ";
            }
            if (checkedInput.Count == 0) {
                return "NOPE!";
            }
            return output;
        }
    }

    public ArrayList CheckedInput
    {
        get {
            return checkedInput;
        }
        set {
            checkedInput = value;
        }
    }

    public Boolean Input (ArrayList set)
    {
        Boolean match = false;
        for (int j = 0; j < set.Count; j++)
        {
            if (!CurrentInput.Contains (set[j])) {
                match = false;
            }

        }
        return false;
    }

    public Boolean ManualMode
    {
        get {
            return manualMode;
        }
        set {
            manualMode = value;
        }
    }

    public void setInputs (ArrayList list)
    {
        InputUp = (KeyCode)list[0];
        InputDown = (KeyCode)list[1];
        InputLeft = (KeyCode)list[2];
        InputRight = (KeyCode)list[3];

        InputA = (KeyCode)list[4];
        InputB = (KeyCode)list[5];
        InputC = (KeyCode)list[6];
        InputD = (KeyCode)list[7];
        InputY = (KeyCode)list[8];
        InputZ = (KeyCode)list[9];

        InputPause = (KeyCode)list[10];
        InputConfirm = (KeyCode)list[11];
        InputCancel = (KeyCode)list[12];
        InputPaceCancel = (KeyCode)list[13];

        InputUpForward = new ArrayList () { InputUp, InputForward };
        InputUpBack = new ArrayList () { InputUp, InputBack };
        InputDownForward = new ArrayList () { InputDown, InputForward };
        InputDownBack = new ArrayList () { InputDown, InputBack };
        InputAB = new ArrayList () { InputA, InputB };
        InputAC = new ArrayList () { InputA, InputC };
        InputAD = new ArrayList () { InputA, InputD };
        InputBC = new ArrayList () { InputB, InputC };
        InputBD = new ArrayList () { InputB, InputD };
        InputCD = new ArrayList () { InputC, InputD };
        InputYZ = new ArrayList () { InputY, InputZ };
        InputABC = new ArrayList () { InputA, InputB, InputC };
        InputABD = new ArrayList () { InputA, InputB, InputD };
        InputABY = new ArrayList () { InputA, InputB, InputY };
        InputBCD = new ArrayList () { InputB, InputC, InputD };
        InputCDZ = new ArrayList () { InputC, InputD, InputZ };
        InputABCD = new ArrayList () { InputA, InputB, InputC, InputD };

        inputs = new ArrayList ();
        currentInput = new ArrayList ();
        allInputs = new ArrayList() {InputA, InputB, InputC, InputD, InputY, InputZ, InputUp, InputDown, InputLeft, InputRight
            ///,
            ///InputUpForward, InputUpBack, InputDownForward, InputDownBack, InputAB, InputAC, InputAD,
            ///InputBC, InputBD, InputCD, InputYZ, InputABC, InputABD, InputABY, InputBCD, InputCDZ, InputABCD
        };
    }

    public float GroundBound
    {
        get {
            return groundBound;
        }
        set {
            groundBound = value;
        }
    }

    public Boolean IsAirborne
    {
        get {
            return MapSprite.transform.position.y < GroundBound;
        }
    }

    public void setSkillInputs ()
    {
    }

    public void move (Vector3 inc)
    {
        MapSprite.transform.position = inc;
        /**
        if (MyReel.CurrentHurtBox != null) {
            
        }
        /**
        if (MyReel.CurrentHitBox != null) {
            MyReel.CurrentHitBox.setBox(this, HitBox);
        }
        */

    }

    public string CheckMovement ()
    {
        string output = @"";
        float xIncrement = 0, yIncrement = 0, 
        xRate = 0, yRate = 0;

        //THERE! MOVEMENT!
        if (MyReel.MotionType.StartsWith (@"FreeFall")) {
            if (Body.velocity.y > 0) {
                Body.velocity = new Vector2 (Body.velocity.x, Body.velocity.y - Time.deltaTime);//Time.deltaTime;
            } else if (Body.velocity.y < 0) {
                Body.velocity = new Vector2 (Body.velocity.x, Body.velocity.y + Time.deltaTime);//Time.deltaTime;
            }
        } else if (MyReel.MotionType.StartsWith (@"Deccel:")) {
            if (Body.velocity.x > 0) {
                Body.velocity = new Vector2 (Body.velocity.x - Time.deltaTime, Body.velocity.y);//Time.deltaTime;
            } else if (Body.velocity.x < 0) {
                Body.velocity = new Vector2 (Body.velocity.x + Time.deltaTime, Body.velocity.y);//Time.deltaTime;
            }
        } 

        //UnityEngine.Debug.Log(MyReel.MotionType + @" SCREEN LEFT BOUND " + leftBound + @" - SCREEN RIGHT BOUND " + rightBound + @" - SCREEN BOTTOM BOUND " + rightBound);

       //UnityEngine.Debug.Log("X-INCREMENT " + xIncrement + " Y-INCREMENT " + yIncrement);

        if (xIncrement != 0 || yIncrement != 0)
        {
            MyReel.VerticalDistanceTraveled += Math.Abs (Body.velocity.x);
            MyReel.HorizontalDistanceTraveled += Math.Abs (Body.velocity.y);
            //move (leftBound, rightBound, bottomBound, xIncrement, yIncrement);
        }
        return output;
    }

    public string checkCollisions (BoxCollider2D screenLeft, BoxCollider2D screenRight, BoxCollider2D screenBottom, BoxCollider2D screenTop,
                                   BoxCollider2D stageLeft, BoxCollider2D stageRight, BoxCollider2D stageBottom, BoxCollider2D stageTop,
                                   BoxCollider2D stageGround, ArrayList bounds)
    {
        string output = @"";

        //0 - ground bound
        BoxCollider2D collider;


        //if (MyReel.MotionType.StartsWith ("FreeFall")) {

            UnityEngine.Debug.Log (Index + @" FREEEEEEEEEEE FAAAAAAAAALL" + MyReel.Value3 + " " + MapSprite.transform.position.y);

            if (MyReel.collidesWith (screenLeft) && Body.velocity.x < 0) {
                Body.velocity = new Vector2 (0, Body.velocity.y);
            }

            if (MyReel.collidesWith (screenRight) && Body.velocity.x > 0) {
                Body.velocity = new Vector2 (Body.velocity.x, 0);
            }

            if (MyReel.collidesWith (stageGround) && Body.velocity.y > 0) {
                //MapSprite.transform.position =
                if (MyReel.isType ("Landing")) {
                    SetReel(MyReel.Next);
                }

            }

            /**
            if (MyReel.isType("Landing"))
            {
                SetReel(MyReel.Next);
            }
            */

        //}

        for (int i = 0; i < bounds.Count; i ++) {
            
        }

        return output;
    }

    /**
    public string move (float leftBound, float rightBound, float bottomBound, float x, float y) {
        string output = @"";
        UnityEngine.Debug.Log (@" XVALUE (" + x + @") YVALUE (" + y + ") LEFT BOUND (" + leftBound + ") RIGHT BOUND (" + rightBound + ") BOTTOM BOUND (" + bottomBound + ") COMPARED TO SPRITE (" + MapSprite.transform.position + @")");

        if (x != 0 && MapSprite.transform.position.x + x > leftBound && MapSprite.transform.position.x + x < rightBound) {
            UnityEngine.Debug.Log (@"X");
            MapSprite.transform.position = new Vector3 (MapSprite.transform.position.x + x, MapSprite.transform.position.y, MapSprite.transform.position.z); 
        }
        if (y != 0) {
            UnityEngine.Debug.Log(@"Y");
            MapSprite.transform.position = new Vector3(MapSprite.transform.position.x, MapSprite.transform.position.y + y, MapSprite.transform.position.z);
        }
        if (MyReel.CurrentHurtBoxPosition != null && MyReel.CurrentHurtBoxPosition [0] != null && MyReel.CurrentHurtBox[0].transform != null) {
            for (int i = 0; i < MyReel.CurrentHurtBox.Length; i ++) {
                MyReel.CurrentHurtBox[i].transform.position = MyReel.CurrentHurtBoxPosition [i];
            }
        }
        return output;
    }
    */

    public ArrayList Inputs
    {
        get
        {
            return inputs;
        }
        set
        {
            inputs = value;
        }
    }

    public ArrayList CurrentInput
    {
        get {
            return inputs;
        }
        set {
            inputs = value;
        }
    }

    public Texture2D Neutral
    {
        get {
            return neutral;
        }
        set {
            neutral = value;
        }
    }

    public Texture2D Damage
    {
        get
        {
            return damage;
        }
        set
        {
            damage = value;
        }
    }

    public Texture2D CrouchDamage
    {
        get {
            return crouchDamage;
        }
        set {
            crouchDamage = value;
        }
    }

	public Boolean Completed
	{
		get {
			return completed;
		}
		set {
			completed = value;
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

	public string DirectionOf (Location loc)
	{
		return currentLocation ().DirectionOf (loc);
	}

	public string DirectionOf (Locatable loc)
	{
		return currentLocation ().DirectionOf (loc);
	}

	public string DirectionLeft
	{
		get {
			if (Direction == "N") {
				return "W";
			}
			if (Direction == "W") {
				return "S";
			}
			if (Direction == "S") {
				return "E";
			}
			if (Direction == "E") {
				return "N";
			}
			return "X";

		}
	}


	public string DirectionRight
	{
		get
		{
			if (Direction == "N")
			{
				return "E";
			}
			if (Direction == "W")
			{
				return "N";
			}
			if (Direction == "S")
			{
				return "W";
			}
			if (Direction == "E")
			{
				return "S";
			}
			return "X";
		}
	}


	public Boolean HasActedInRound 
	{
		get {
			return hasActedInRound;
		}
		set {
			hasActedInRound = value;
		}
	}

	public Texture2D mapTexture ()
	{
		return SpriteAppearance;
	}

	public Boolean withinReachOf (Skill s, Location targetLocation)
	{
		if (s.Properties.Contains ("MAP ")) {
			int difference = Math.Abs (s.CurrentHeight - currentMap ().heightOf (targetLocation));
			if (difference <= 2) {
				return true;
			}
			return false;
		}

		if (s.Properties.Contains ("LAND ")) {
			return true;
		}
		if ((s.Grapple || s.Physical) && currentMap ().playerAt (targetLocation) != null) {
			return Math.Abs ((currentMap ().heightOf (targetLocation) + currentMap ().playerAt (targetLocation).Potency (currentMap ().playerAt (targetLocation).Airborne))
			                 - (currentMap ().heightOf (currentLocation ()) + Potency (Airborne))) <= 5;
		}
		return Math.Abs ((currentMap ().heightOf (targetLocation) + currentMap ().playerAt (targetLocation).Potency (currentMap ().playerAt (targetLocation).Airborne))
		                 - (currentMap ().heightOf (currentLocation ()) + Potency (Airborne))) <= 4;
		return false;
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

	public float StepTimeModulus
	{
		get {
			return activeTimeModulus;
		}
		set {
			activeTimeModulus = value;
		}
	}

	public ArrayList actualSpace ()
	{
		return ActualSpace;
	}

	public ArrayList ActualSpace
	{
		get {
			return pointSpace;
		}
		set {
			pointSpace = value;
		}
	}

	public int [] SpriteRatio
	{
		get {
			return spriteRatio;
		}
		set {
			spriteRatio = value;
		}
	}

    public Boolean Champion
    {
        get {
            return champion;
        }
        set {
            champion = value;
        }
    }

    public Reel getReel (string token)
    {
        if  (token == "none") {
            return null;
        }
        Reel rl = null;
        for (int i = 0; i < AllSprites.Length; i ++) {
            if (AllSprites[i] == null)
            { throw new NullReferenceException("POOP!" + i); }
            if (AllSprites[i].Name.Equals (token)) {
                return AllSprites[i];
            }
        }
        return rl;
    }

    public void ReadInput (ArrayList lst)
    {
        checkedInput = new ArrayList ();
        int count = 0;
        for (int i = 0; i < lst.Count; i++)
        {
            if (((KeyCode)lst[i]).Equals(InputUp))
            {
                count++;
                checkedInput.Add("Up");
            }
            if (((KeyCode)lst[i]).Equals(InputDown))
            {
                count++;
                checkedInput.Add("Down");
            }
            if (((KeyCode)lst[i]).Equals(InputForward))
            {
                count++;
                checkedInput.Add("Forward");
            }
            if (((KeyCode)lst[i]).Equals(InputBack))
            {
                count++;
                checkedInput.Add("Back");
            }
            if (((KeyCode)lst[i]).Equals(InputA))
            {
                count++;
                checkedInput.Add("A");
            }
            if (((KeyCode)lst[i]).Equals(InputB))
            {
                count++;
                checkedInput.Add("B");
            }
            if (((KeyCode)lst[i]).Equals(InputC))
            {
                count++;
                checkedInput.Add("C");
            }
            if (((KeyCode)lst[i]).Equals(InputD))
            {
                count++;
                checkedInput.Add("D");
            }
            if (((KeyCode)lst[i]).Equals(InputY))
            {
                count++;
                checkedInput.Add("Y");
            }
            if (((KeyCode)lst[i]).Equals(InputZ))
            {
                count++;
                checkedInput.Add("Z");
            }
            if (((KeyCode)lst[i]).Equals(InputConfirm))
            {
                count++;
                checkedInput.Add("Confirm");
            }
            if (((KeyCode)lst[i]).Equals(InputCancel))
            {
                count++;
                checkedInput.Add("Cancel");
            }
            if (((KeyCode)lst[i]).Equals(InputPause))
            {
                count++;
                checkedInput.Add("Pause");
            }
            if (count == 0)
            {
                checkedInput.Add("None");
            }
        }
    }

    public Boolean MetersSet
    {
        get {
            return (Health.MeterLevel == Health.MeterLevelAppearance)
            && (Rush.MeterLevel == Rush.MeterLevelAppearance)
            && (Guard.MeterLevel == Guard.MeterLevelAppearance)
            && (Stun.MeterLevel == Stun.MeterLevelAppearance);
        }
    }

	public void LoadSprites ()
	{
		neutral = Resources.Load <Texture2D> (@"Players/" + SearchName + @"/Animations/Neutral");

		crouchNeutral = Resources.Load <Texture2D> (@"Players/" + SearchName + @"/Animations/CrouchNeutral");
		groundNeutral = Resources.Load <Texture2D> (@"Players/" + SearchName + @"/Animations/GroundNeutral");
		airNeutral = Resources.Load <Texture2D> (@"Players/" + SearchName + @"/Animations/AirNeutral");

		damage = Resources.Load <Texture2D> (@"Players/" + SearchName + @"/Animations/Damage");
		crouchDamage = Resources.Load <Texture2D> (@"Players/" + SearchName + @"/Animations/CrouchDamage");
		groundDamage = Resources.Load <Texture2D> (@"Players/" + SearchName + @"/Animations/GroundDamage");
		airDamage = Resources.Load <Texture2D> (@"Players/" + SearchName + @"/Animations/AirDamage");

		portrait = Resources.Load<Texture2D> (@"Players/" + SearchName + @"/Artwork/Portrait");

        if (!tacticalMode)
        {
            StreamReader reader = new StreamReader(@"Assets/Resources/Players/" + SearchName + @"/FrameData.txt");
            string nm, inf;
            string motionType, hitboxType, hurtboxType;
            string[] endConditions;
            int frames, numWins, numLosses, endC;
            Boolean looping, active, direct, inpt;
            float horizontalForce, verticalForce,
            val1, val2, val3, val4;
            string followUpRl;

            float rate;

            inf = reader.ReadLine();
            reader.ReadLine();
            int numSprites = NumberConverter.ConvertToInt (inf.Substring (0, 5));
            numWins = 1;
            numLosses = 1;
            allSprites = new Reel [numSprites];

            for (int i = 0; i < allSprites.Length; i++)
            {
                inf = reader.ReadLine();
                UnityEngine.Debug.Log (inf);
                nm = inf.Substring(0, inf.IndexOf(':'));
                frames = NumberConverter.ConvertToInt (reader.ReadLine().Substring (0, 5));
                UnityEngine.Debug.Log(frames + " FRAMES " + nm);
                rate = NumberConverter.ConvertToFloat (reader.ReadLine().Substring(0, 5));
                val1 = NumberConverter.ConvertToFloat(reader.ReadLine().Substring(0, 5));
                val2 = NumberConverter.ConvertToFloat(reader.ReadLine().Substring(0, 5));
                val3 = NumberConverter.ConvertToFloat(reader.ReadLine().Substring(0, 5));
                val4 = NumberConverter.ConvertToFloat(reader.ReadLine().Substring(0, 5));

                //motionType = reader.ReadLine();
                //hitboxType = reader.ReadLine();
                //CAN CHANGE DIRECTION


                looping = reader.ReadLine ().Substring (0, 4).Equals (@"true");
                active = reader.ReadLine ().Substring (0, 4).Equals (@"true");
                direct = reader.ReadLine ().Substring(0, 4).Equals (@"true");
                inpt = reader.ReadLine ().Substring (0, 4).Equals (@"true");

                inf = reader.ReadLine ();
                motionType = inf.Substring (0, inf.IndexOf (':'));
                inf = reader.ReadLine ();
                hurtboxType = inf.Substring(0, inf.IndexOf(':'));
                inf = reader.ReadLine ();
                hitboxType = inf.Substring(0, inf.IndexOf(':'));

                inf = reader.ReadLine();
                followUpRl = inf.Substring (0, inf.IndexOf (':'));

                inf = reader.ReadLine();
                endC = NumberConverter.ConvertToInt(inf.Substring(0, 5));

                UnityEngine.Debug.Log (endC + " END CONDITIONS " + nm);


                endConditions = new string[endC];

                for (int j = 0; j < endConditions.Length; j ++) {
                    endConditions[j] += reader.ReadLine ();
                }



                if (motionType.Contains (@"")) {
                    
                }

                allSprites [i] = new Reel (nm, @"Players/" + searchName + @"/Animations/" + nm, frames, rate, false,
                                              looping, active, 0f, frames > 1, false, true, new Vector3 (0, 0, 0), this);
                allSprites [i].CanChangeDirection = direct;
                allSprites [i].CanInput = inpt;
                allSprites [i].MotionType = motionType;
                allSprites [i].HurtboxInfo = hurtboxType;
                allSprites [i].HitboxInfo = hitboxType;
                allSprites [i].FollowUpName = followUpRl;
                //allSprites [i].Body = Body;
                allSprites [i].Value1 = val1;
                allSprites [i].Value2 = val2;
                allSprites [i].Value3 = val3;
                allSprites [i].Value4 = val4;
                allSprites [i].Conditions = endConditions;
                    UnityEngine.Debug.Log (nm + @" animation loaded " + Name);

                /**
                 *     float maxMeasure;
    float timeZeroToMax;
    float accelRatePerSecond;
    float velocity;

                 */ 


                reader.ReadLine ();

            }


            //WINS
            inf = reader.ReadLine();
            numWins = NumberConverter.ConvertToInt (inf.Substring(0, 5));

            winSprites = new Reel[numWins];
            for (int j = 0; j < winSprites.Length; j ++) {

                inf = reader.ReadLine();
                nm = inf.Substring(0, inf.IndexOf(':'));
                frames = NumberConverter.ConvertToInt(reader.ReadLine().Substring(0, 5));
                rate = NumberConverter.ConvertToFloat(reader.ReadLine().Substring(0, 5));

                val1 = NumberConverter.ConvertToFloat(reader.ReadLine().Substring(0, 5));
                val2 = NumberConverter.ConvertToFloat(reader.ReadLine().Substring(0, 5));
                val3 = NumberConverter.ConvertToFloat(reader.ReadLine().Substring(0, 5));
                val4 = NumberConverter.ConvertToFloat(reader.ReadLine().Substring(0, 5));

                //motionType = reader.ReadLine();
                //hitboxType = reader.ReadLine();
                //CAN CHANGE DIRECTION


                looping = reader.ReadLine().Substring(0, 4).Equals(@"true");
                active = reader.ReadLine().Substring(0, 4).Equals(@"true");
                direct = reader.ReadLine().Substring(0, 4).Equals(@"true");
                inpt = reader.ReadLine().Substring(0, 4).Equals(@"true");

                inf = reader.ReadLine();
                motionType = inf.Substring(0, inf.IndexOf(':'));
                inf = reader.ReadLine();
                hurtboxType = inf.Substring(0, inf.IndexOf(':'));
                inf = reader.ReadLine();
                hitboxType = inf.Substring(0, inf.IndexOf(':'));

                inf = reader.ReadLine();
                followUpRl = inf.Substring(0, inf.IndexOf(':'));

                inf = reader.ReadLine();
                endC = NumberConverter.ConvertToInt(inf.Substring(0, 5));

                UnityEngine.Debug.Log(endC + " END CONDITIONS " + nm);


                endConditions = new string[endC];

                for (int q = 0; q < endConditions.Length; q++)
                {
                    endConditions[q] += reader.ReadLine();
                }


                winSprites[j] = new Reel(nm, @"Players/" + searchName + @"/Animations/" + nm, frames, rate, false,
                          looping, active, 0f, frames > 1, false, true, new Vector3(0, 0, 0), this);
                winSprites[j].CanChangeDirection = direct;
                winSprites[j].CanInput = inpt;
                winSprites[j].MotionType = motionType;
                winSprites[j].HurtboxInfo = hurtboxType;
                winSprites[j].HitboxInfo = hitboxType;
                winSprites[j].FollowUpName = followUpRl;
                winSprites[j].Conditions = endConditions;
                UnityEngine.Debug.Log(nm + @"'s win animation loaded " + Name);
                reader.ReadLine ();

            }

            //LOSSES
            inf = reader.ReadLine();
            numLosses = NumberConverter.ConvertToInt(inf.Substring(0, 5));

            loseSprites = new Reel[numLosses];
            for (int j = 0; j < loseSprites.Length; j++)
            {
                inf = reader.ReadLine();
                UnityEngine.Debug.Log(inf);
                nm = inf.Substring(0, inf.IndexOf(':'));
                frames = NumberConverter.ConvertToInt(reader.ReadLine().Substring(0, 5));
                rate = NumberConverter.ConvertToFloat(reader.ReadLine().Substring(0, 5));

                val1 = NumberConverter.ConvertToFloat(reader.ReadLine().Substring(0, 5));
                val2 = NumberConverter.ConvertToFloat(reader.ReadLine().Substring(0, 5));
                val3 = NumberConverter.ConvertToFloat(reader.ReadLine().Substring(0, 5));
                val4 = NumberConverter.ConvertToFloat(reader.ReadLine().Substring(0, 5));

                //motionType = reader.ReadLine();
                //hitboxType = reader.ReadLine();
                //CAN CHANGE DIRECTION


                looping = reader.ReadLine().Substring(0, 4).Equals(@"true");
                active = reader.ReadLine().Substring(0, 4).Equals(@"true");
                direct = reader.ReadLine().Substring(0, 4).Equals(@"true");
                inpt = reader.ReadLine().Substring(0, 4).Equals(@"true");

                inf = reader.ReadLine();
                motionType = inf.Substring(0, inf.IndexOf(':'));
                inf = reader.ReadLine();
                hurtboxType = inf.Substring(0, inf.IndexOf(':'));
                inf = reader.ReadLine();
                hitboxType = inf.Substring(0, inf.IndexOf(':'));

                inf = reader.ReadLine();
                followUpRl = inf.Substring(0, inf.IndexOf(':'));

                inf = reader.ReadLine();
                endC = NumberConverter.ConvertToInt(inf.Substring(0, 5));

                UnityEngine.Debug.Log(endC + " END CONDITIONS " + nm);


                endConditions = new string[endC];

                for (int q = 0; q < endConditions.Length; q++)
                {
                    endConditions[q] += reader.ReadLine ();
                }

                loseSprites[j] = new Reel(nm, @"Players/" + searchName + @"/Animations/" + nm, frames, rate, false,
                          looping, active, 0f, frames > 1, false, true, new Vector3(0, 0, 0), this);
                loseSprites[j].CanChangeDirection = direct;
                loseSprites[j].CanInput = inpt;
                loseSprites[j].MotionType = motionType;
                loseSprites[j].HurtboxInfo = hurtboxType;
                loseSprites[j].HitboxInfo = hitboxType;
                loseSprites[j].FollowUpName = followUpRl;
                loseSprites[j].Conditions = endConditions;
                UnityEngine.Debug.Log(nm + @"'s lose animation loaded " + Name);

            }


            reader.Close ();
            MyReel = NeutralSprite;

            for (int i = 0; i < allSprites.Length; i++)
            {
                if (getReel(allSprites[i].FollowUpName) != null)
                {
                    allSprites[i].Next = getReel(allSprites[i].FollowUpName);
                }

            }

            stumbleTime = -1.0f;
            dizzyTime = -1.0f;
        }
	}

    public RawImage screenTexture ()
    {
        return MapSprite;
    }

    public Vector3 screenLocation ()
    {
        return MapSprite.transform.position;
    }

    public void setAllHurtAndHitBoxes ()
    {
        string nm, path;
        Texture2D textr;
        MyReel.adjustPlayerSpriteBasedOnScale (this, MapSprite);

        //MapSprite.transform.SetParent (.transform);

        for (int i = 0; i < allSprites.Length; i++)
        {
            //HURT BOX LOADING
            if (allSprites[i].HurtboxInfo == @"Neutral")
            {
                UnityEngine.Debug.Log(@"LOADING HURT BOX FOR " + allSprites[i].Name);

                allSprites[i].CurrentHurtBoxes = new BoxCollider2D [allSprites[i].Roll.Count][];
                for (int j = 0; j < allSprites[i].CurrentHurtBoxes.Length; j++)
                {
                    //HIT BOX
                    //textr = Resources.Load <Texture2D>(path);
                    allSprites[i].CurrentHurtBoxes [j] = new BoxCollider2D [3];
                    for (int q = 0; q < allSprites [i].CurrentHurtBoxes[j].Length; q ++) {
                        allSprites[i].CurrentHurtBoxes [j] [q] = new GameObject ().AddComponent <BoxCollider2D> ();
                        nm = allSprites[i].Name + (j + 1) + "#" + q;
                        allSprites[i].CurrentHurtBoxes[j][q].name = nm + @"";
                        path = string.Format (@"Players/{0}/HurtBox/{1}", SearchName, nm);
                        UnityEngine.Debug.Log (path);
                        if (allSprites [i].CurrentHurtBoxes [j] [q].transform == null) {
                            UnityEngine.Debug.Log ("WE'VE GOT PROBLEMS!");
                        }
                        if (MapSprite.transform == null) {
                            UnityEngine.Debug.Log("WE'VE GOT PROBLEMS!");
                        }
                        allSprites [i].CurrentHurtBoxes [j] [q].transform.SetParent (MapSprite.transform);
                        allSprites [i].CurrentHurtBoxes [j] [q].size = new Vector3 (MapSprite.GetComponent<RectTransform> ().sizeDelta.x, MapSprite.GetComponent<RectTransform> ().sizeDelta.y);

                    }
                }
            } else {
                allSprites[i].CurrentHurtBoxes = null;
            }

            //HIT BOX LOADING
            if (allSprites[i].HitboxInfo == @"Neutral")
            {
                UnityEngine.Debug.Log(@"LOADING HIT BOX FOR " + allSprites[i].Name);

                allSprites[i].CurrentHitBoxes = new BoxCollider2D[allSprites[i].Roll.Count][];
                for (int j = 0; j < allSprites[i].CurrentHitBoxes.Length; j++)
                {
                    //textr = Resources.Load <Texture2D>(path);
                    allSprites[i].CurrentHitBoxes[j] = new BoxCollider2D[3];
                    for (int q = 0; q < allSprites[i].CurrentHitBoxes[j].Length; q++)
                    {
                        nm = allSprites [i].Name + (j + 1) + "-" + q;
                        path = string.Format(@"Players/{0}/HitBox/{1}", SearchName, nm);
                        UnityEngine.Debug.Log (path);
                        allSprites [i].CurrentHitBoxes [j] [q].transform.SetParent (MapSprite.transform);
                    }
                }
            } else {
                allSprites[i].CurrentHitBoxes = null;
            }

        }


    }

    public RawImage cloneRawImage (string name, RawImage original, Transform parent)
    {
        RawImage clone = new GameObject ().AddComponent<RawImage> ();
        clone.gameObject.name = name;
        clone.transform.SetParent (parent);
        clone.GetComponent<RectTransform> ().sizeDelta = new Vector2 (original.rectTransform.sizeDelta.x, original.rectTransform.sizeDelta.y);
        clone.rectTransform.anchorMin = original.rectTransform.anchorMin;
        clone.rectTransform.anchorMax = original.rectTransform.anchorMax;
        clone.rectTransform.pivot = original.rectTransform.pivot;
        clone.transform.position = original.transform.position;
        //clone.transform.SetSiblingIndex(original.transform.GetSiblingIndex());
        clone.texture = original.texture;
        //= targetHealthBars [i].transform.position;
        return clone;
    }


    public float SSR
    {
        get {
            return ssr;
        }
        set {
            ssr = value;
        }
    }

    public Boolean BoxesEnabled
    {
        get {
            return true;
        }
    }

    public void updateReel ()
    {

        if (MyReel.FrameTime > 0)
        {
            if (MyReel.CanAdjustSide)
            {
                string dir = direction;
                MyReel.fixDirection ();
                if (dir != direction)
                {
                    if (!MyReel.Equals(TurningSprite) && (MyReel.Equals (NeutralSprite) || MyReel.Equals(CrouchingSprite))) {
                        if (MyReel.Standing)
                        {
                            //SetReel (TurningSprite);
                        } else if (MyReel.Crouching) {
                            //SetReel (CrouchingTurningSprite);
                        }
                    }
                }
            }

            MyReel.FrameTime -= Time.deltaTime;

            ///print (player.MyReel.FrameTime);
            if (MyReel.FrameTime <= 0)
            {
                MyReel.StepIndex ();

                if (spriteInfo != null)
                {
                    SpriteInfo.text = "SPRITE:" + MyReel.Name + " " + Index + '\n' + " (" + MapSprite.transform.position + ")" + '\n';

                    if (MyReel.CurrentHurtBox != null)
                    {
                        for (int i = 0; i < MyReel.CurrentHurtBox.Length; i++) {
                            SpriteInfo.text += "HURTBOX " + (i + 1) + ") " + MyReel.CurrentHurtBox[i].transform.name;
                        }
                    }
                    else
                    {
                        SpriteInfo.text += "HURTBOX: NONE";
                    }
                    /**
                    if (MyReel.CurrentHitBox != null)
                    {
                        SpriteInfo.text += "HITBOX: " + MyReel.CurrentHitBox.Name + " (" + MyReel.CurrentHitBox.AnchorType + ")" + '\n'
                            + MyReel.CurrentHitBox.CollisionType;
                    }
                    else
                    {
                        SpriteInfo.text += "HITBOX: NONE";
                    }
                    */
                }

                if ((MyReel.Index < 0) || MyReel.Index >= MyReel.Roll.Count) {
                    if (MyReel.isType (@"EndLoop") && MyReel.Next != null)
                    {
                        MyReel.HasStarted = false;
                        SetReel (MyReel.Next);
                    }
                }

                else if ((MyReel.Index >= 0) && MyReel.Index < MyReel.Roll.Count)
                {
                    ///print (player.MyReel.Index + " " + player.MyReel.Roll.Count + " " + player.MyReel.Path);
                    MyReel.FrameTime = MyReel.Framerate;
                    MapSprite.texture = (Texture2D)MyReel.Roll [MyReel.Index];

                    //HURTBOX
                    if (MyReel.CurrentHurtBoxes != null && MyReel.CurrentHurtBoxes [MyReel.Index] != null) {
                        UnityEngine.Debug.Log (@"LOADING HURT BOX " + MyReel.CurrentHurtBoxes[MyReel.Index].Length + @" " 
                                               + Index);
                        MyReel.CurrentHurtBox = MyReel.CurrentHurtBoxes[MyReel.Index];
                        if (BoxesEnabled) {
                            for (int i = 0; i < MyReel.CurrentHurtBox.Length; i++)
                            {
                                MyReel.CurrentHurtBox[i].enabled = true;
                                if (IsAirborne)
                                {
                                    //MyReel.CurrentHurtBox[i].color = Color.red;
                                }
                            }
                        }
                    } else {
                        UnityEngine.Debug.Log(@"NO HURT BOX FOR " + MyReel.Name + @" " + Index);
                    }
                    //HURTBOX

                }
            }
        }
        MyReel.adjustPlayerSpriteBasedOnScale (this, MapSprite);
    }


    public void SetReel (Reel r)
    {
        //player.MyReel.SetReel (r);
        UnityEngine.Debug.Log (@"SETTING REEL " + r.Name);
        MyReel = r;
        MyReel.CheckSpriteStatements ();
        MyReel.VerticalDistanceTraveled = 0;
        MyReel.HorizontalDistanceTraveled = 0;
        MyReel.Index = -1;
        MyReel.FrameTime = 0.01f;

        if (MyReel.MotionType.Contains ("FreeFall")) {
            Body.velocity = new Vector2 (MyReel.Value1, MyReel.Value2);
        } else if (MyReel.MotionType.StartsWith ("Forward#")) {
            Body.velocity = new Vector2(MyReel.Value1 * (float)Speed, 0);

        }
        else if (MyReel.MotionType.StartsWith("Back#"))
        {
            Body.velocity = new Vector2(MyReel.Value1 * (float)Speed, 0);
        } else {
            Body.velocity = new Vector2 (0, 0);
        }

        /**
        else if (MyReel.MotionType.StartsWith (@"Forward#"))
        {
            //Body.velocity
            xRate = (20f * ((float)NumberConverter.ConvertToDouble(MyReel.MotionType.Substring(MyReel.MotionType.IndexOf('#') + 1, 5)))) * (float)Speed;
            UnityEngine.Debug.Log(xRate);
            xIncrement += xRate;
            if (Direction.Equals(@"W"))
            {
                xIncrement *= -1;
            }
        }

        /**
        MyReel.Value3 = MyReel.Value1;
        MyReel.Value4 = MyReel.Value2;
        */
        //}
    }

    public void SetReel (Reel r, int index)
    {
        UnityEngine.Debug.Log(@"SETTING REEL " + r.Name + @" AT " + index);
        MyReel = r;
        MyReel.CheckSpriteStatements();
        MyReel.VerticalDistanceTraveled = 0;
        MyReel.HorizontalDistanceTraveled = 0;
        MyReel.Index = index;
        MyReel.FrameTime = 0.01f;

    }

    public Reel[] WinSprites
    {
        get {
            return winSprites;
        }
        set {
            winSprites = value;
        }
    }

    public Reel[] LoseSprites
    {
        get {
            return loseSprites;
        }
        set {
            loseSprites = value;
        }
    }

    public Reel[] AllSprites
    {
        get {
            return allSprites;
        }
        set {
            allSprites = value;
        }
    }

    public Reel NeutralSprite
    {
        get {
            return allSprites [0];
        }
        set {
            allSprites[0] = value;
        }
    }

    public Reel WalkForwardSprite
    {
        get
        {
            return allSprites [1];
        }
        set
        {
            allSprites [1] = value;
        }
    }

    public Reel WalkBackSprite
    {
        get
        {
            return allSprites [2];
        }
        set
        {
            allSprites [2] = value;
        }
    }

    public Reel CrouchingSprite
    {
        get {
            return allSprites [3];
        }
        set {
            allSprites [3] = value;
        }
    }

    public Reel StandingSprite
    {
        get {
            return allSprites [4];
        }
        set {
            allSprites [4] = value;
        }
    }

    public Reel CrouchedSprite
    {
        get {
            return allSprites [5];
        }
        set {
            allSprites [5] = value;
        }
    }

    public Reel TurningSprite
    {
        get {
            return allSprites [6];
        }
        set {
            allSprites [6] = value;
        }
    }

    public Reel CrouchingTurningSprite
    {
        get
        {
            return allSprites[7];
        }
        set
        {
            allSprites[7] = value;
        }
    }


    public Reel JumpStartSprite
    {
        get {
            return allSprites [8];
        }
        set {
            allSprites [8] = value;
        }
    }

    public Reel JumpNeutralSprite
    {
        get {
            return allSprites [9];
        }
        set {
            allSprites [9] = value;
        }
    }

    public Reel JumpForwardSprite
    {
        get {
            return allSprites [10];
        }
        set {
            allSprites [10] = value;
        }
    }

    public Reel JumpBackSprite
    {
        get {
            return allSprites [11];
        }
        set {
            allSprites [11] = value;
        }
    }

    public Reel LandingSprite
    {
        get {
            return allSprites [12];
        }
        set {
            allSprites [12] = value;
        }
    }

    public Reel DashForwardSprite
    {
        get {
            return allSprites [56];
        }
        set {
            allSprites [56] = value;
        }
    }

    public Reel DashBackSprite
    {
        get {
            return allSprites [13];
        }
        set {
            allSprites [13] = value;
        }
    }

    public Reel RunSprite
    {
        get {
            return allSprites [14];
        }
        set {
            allSprites [14] = value;
        }
    }

    public Reel GuardingLightSprite
    {
        get {
            return allSprites [15];
        }
        set {
            allSprites [15] = value;
        }
    }

    public Reel GuardingMediumSprite
    {
        get {
            return allSprites [16];
        }
        set {
            allSprites [16] = value;
        }
    }

    public Reel GuardingHeavySprite
    {
        get {
            return allSprites [17];
        }
        set {
            allSprites [17] = value;
        }
    }

    public Reel GuardingLightLowSprite
    {
        get {
            return allSprites [18];
        }
        set {
            allSprites [18] = value;
        }
    }

    public Reel GuardingMediumLowSprite
    {
        get {
            return allSprites [19];
        }
        set {
            allSprites [19] = value;
        }
    }

    public Reel GuardingHeavyLowSprite
    {
        get {
            return allSprites [20];
        }
        set {
            allSprites [20] = value;
        }
    }

    public Reel GuardingCrouchingLightSprite
    {
        get {
            return allSprites [21];
        }
        set {
            allSprites [21] = value;
        }
    }

    public Reel GuardingCrouchingMediumSprite
    {
        get {
            return allSprites [22];
        }
        set {
            allSprites [22] = value;
        }
    }

    public Reel GuardingCrouchingHeavySprite
    {
        get {
            return allSprites [23];
        }
        set {
            allSprites [23] = value;
        }
    }

    public Reel GuardingAirLightSprite
    {
        get {
            return allSprites [24];
        }
        set {
            allSprites [24] = value;
        }
    }

    public Reel GuardingAirMediumSprite
    {
        get {
            return allSprites [25];
        }
        set {
            allSprites [25] = value;
        }
    }

    public Reel GuardingAirHeavySprite
    {
        get {
            return allSprites [26];
        }
        set {
            allSprites [26] = value;
        }
    }

    public Reel CrashSprite
    {
        get {
            return allSprites [27];
        }
        set {
            allSprites [27] = value;
        }
    }

    public Reel LastStandSprite
    {
        get {
            return allSprites [28];
        }
        set {
            allSprites [28] = value;
        }
    }

    public Reel LightDamageSprite
    {
        get {
            return allSprites [29];
        }
        set {
            allSprites [29] = value;
        }
    }

    public Reel MediumDamageSprite
    {
        get {
            return allSprites [30];
        }
        set {
            allSprites [30] = value;
        }
    }

    public Reel HeavyDamageSprite
    {
        get {
            return allSprites [31];
        }
        set {
            allSprites [31] = value;
        }
    }

    public Reel LightLowDamageSprite
    {
        get {
            return allSprites [32];
        }
        set {
            allSprites [32] = value;
        }
    }

    public Reel MediumLowDamageSprite
    {
        get {
            return allSprites [33];
        }
        set {
            allSprites [33] = value;
        }
    }

    public Reel HeavyLowDamageSprite
    {
        get {
            return allSprites [34];
        }
        set {
            allSprites [34] = value;
        }
    }

    public Reel CrouchingLightDamageSprite
    {
        get {
            return allSprites [35];
        }
        set {
            allSprites [35] = value;
        }
    }

    public Reel CrouchingMediumDamageSprite
    {
        get {
            return allSprites [36];
        }
        set {
            allSprites [36] = value;
        }
    }

    public Reel CrouchingHeavyDamageSprite
    {
        get {
            return allSprites [37];
        }
        set {
            allSprites [37] = value;
        }
    }

    public Reel AirLightDamageSprite
    {
        get {
            return allSprites [38];
        }
        set {
            allSprites [38] = value;
        }
    }

    public Reel AirMediumDamageSprite
    {
        get {
            return allSprites [39];
        }
        set {
            allSprites [39] = value;
        }
    }

    public Reel AirHeavyDamageSprite
    {
        get {
            return allSprites [40];
        }
        set {
            allSprites [40] = value;
        }
    }

    public Reel StumbleForwardSprite
    {
        get {
            return allSprites [41];
        }
        set {
            allSprites [41] = value;
        }
    }

    public Reel StumbleBackSprite
    {
        get {
            return allSprites [42];
        }
        set {
            allSprites [42] = value;
        }
    }

    public Reel TripForwardSprite
    {
        get {
            return allSprites [43];
        }
        set {
            allSprites [43] = value;
        }
    }

    public Reel TripBackSprite
    {
        get {
            return allSprites [44];
        }
        set {
            allSprites [44] = value;
        }
    }

    public Reel CrumpleSprite
    {
        get {
            return allSprites [45];
        }
        set {
            allSprites [45] = value;
        }
    }

    public Reel FallingSprite
    {
        get {
            return allSprites [46];
        }
        set {
            allSprites [46] = value;
        }
    }

    public Reel GroundedFaceUpSprite
    {
        get {
            return allSprites [47];
        }
        set {
            allSprites [47] = value;
        }
    }

    public Reel GroundedFaceDownSprite
    {
        get {
            return allSprites [48];
        }
        set {
            allSprites [48] = value;
        }
    }

    public Reel StandingFaceUpSprite
    {
        get {
            return allSprites [49];
        }
        set {
            allSprites [49] = value;
        }
    }

    public Reel StandingFaceDownSprite
    {
        get {
            return allSprites [50];
        }
        set {
            allSprites [50] = value;
        }
    }

    public Reel GroundRecoverFaceUpSprite
    {
        get {
            return allSprites [51];
        }
        set {
            allSprites [51] = value;
        }
    }

    public Reel GroundRecoverFaceDownSprite
    {
        get {
            return allSprites [52];
        }
        set {
            allSprites [52] = value;
        }
    }

    public Reel SpiralVerticalSprite
    {
        get {
            return allSprites [53];
        }
        set {
            allSprites [53] = value;
        }
    }

    public Reel SpiralHorizontalSprite
    {
        get {
            return allSprites [54];
        }
        set {
            allSprites [54] = value;
        }
    }

    public Reel TauntSprite
    {
        get {
            return allSprites [55];
        }
        set {
            allSprites [55] = value;
        }
    }

	public Texture2D Portrait
	{
		get {
			return portrait;
		}
		set {
			portrait = value;
		}
	}

	public Texture2D NeutralStance
	{
		get {
			return neutral;
		}
	}

	public Texture2D SpriteRepresentation (string txt, string direct)
	{
		if (Completed && getSkill (txt) != null && getSkill (txt).PlayerTexture != null) {
			if (getSkill (txt).PlayerTexture == null && !getSkill (txt).Properties.Contains ("NOANIM ")) {
				throw new NullReferenceException (txt + " NOT FOUND");
			}
			//if (direct.Contains ("W") || direct.Equals ("N")) {
				return getSkill (txt).PlayerTexture;
			//} 
			//return getSkill (txt).ReversePlayerTexture;
		}
		return SpriteOf (txt, direct);
	}

	public Texture2D SpriteOf (string text, string direct)
	{
		if (Completed) {
			if (text.EndsWith (@"Neutral")) {
				if (text.Contains (@"Air")) {
					//if (direct.Contains ("W") || direct.Equals ("N")) {
					//	return reverseAirNeutral;
					//}
					return airNeutral;
				}
				if (text.Contains (@"Crouch")) {
					//if (direct.Contains ("W") || direct.Equals ("N")) {
					//	return reverseCrouchNeutral;
					//}
					return crouchNeutral;
				}
				//if (direct.Contains ("W") || direct.Equals ("N")) {
				//	return reverseNeutral;
				//}
				return neutral;
			}
			if (text.Contains (@"Air")) {
				//if (direct.Contains ("W") || direct.Equals ("N")) {
				//	return reverseAirDamage;
				//}
				return airDamage;
			}
			if (text.Contains (@"Crouch")) {
				//if (direct.Contains ("W") || direct.Equals ("N")) {
				//	return reverseCrouchDamage;
				//}
				return crouchDamage;
			}
			//if (direct.Contains ("W") || direct.Equals ("N")) {
			//	return reverseDamage;
			//}
			return damage;
		}
		return SpriteAppearance;
	}

    /**
	public Texture2D ReverseSpriteAppearance
	{
		get {
			if (Completed) {
				if (MyTeam != null && (MyTeam.IsDefeated || (MyTeam.HasWon && KOd))) {
					return reverseCrouchDamage;
				}
				if (KOd) {
					return reverseGroundNeutral;
				}
				if (StateActive (Airborne)) {
					if (HurtStun > 0) {
						return reverseAirDamage;
					} else {
						return reverseAirNeutral;
					}
				} if (StateActive (Grounded)) {
					if (HurtStun > 0) {
						return reverseGroundDamage;
					} else {
						return reverseGroundNeutral;
					}
				} if (IsCrouching) {
					if (HurtStun > 0) {
						return reverseCrouchDamage;
					} else {
						return reverseCrouchNeutral;
					}
				}
				if (HurtStun > 0) {
					return reverseDamage;
				}
				return reverseNeutral;
			}
			if (StateActive (Grounded) || KOd) {
				return reverseGroundDamage;
			}
			return reverseNeutral;
		}
	}
	*/

    public Texture2D VersusAppearance
    {
        get {
            return neutral;
        }
    }

	public Texture2D SpriteAppearance
	{
		get {
			if (Completed) {
				if (KOd) {
					return groundNeutral;
				}
				if (MyTeam != null && MyTeam.IsDefeated) {
					return crouchDamage;
				}
				if (StateActive (Airborne)) {
					if (HurtStun > 0) {
						return airDamage;
					} else {
						return airNeutral;
					}
				} if (StateActive (Grounded)) {
					if (HurtStun > 0) {
						return groundDamage;
					} else {
						return groundNeutral;
					}
				} if (IsCrouching) {
					if (HurtStun > 0) {
						return crouchDamage;
					} else {
						return crouchNeutral;
					}
				}
				if (HurtStun > 0) {
					return damage;
				}
				return neutral;
			}
			if (StateActive (Grounded) || KOd) {
				return groundDamage;
			}
			return neutral;
		}
	}
			
	public Reel MyReel
	{
		get {
			return myReel;
		}
		set {
			myReel = value;
		}
	}

	public Boolean HitMaliciously
	{
		get {
			return hitMaliciously;
		}
		set {
			hitMaliciously = value;
		}
	}

	public int randomOutput (int input)
	{
		int value = Math.Abs (input);
		int rand = 0;

		if (value > 1) {
			rand = R.Next (1);
		}
		if (value > 20) {
			rand = R.Next (2);
		}
		if (value > 50) {
			rand = R.Next (4);
		}
		if (value > 100) {
			rand = R.Next (6);
		}
		if (value > 500) {
			rand = R.Next (10);
		}
		if (value > 1000) {
			rand = R.Next (20);
		}

		if (input < 0) {
			return -1 * (value + rand);
		} return value + rand;
	}


	public Boolean PlayStyleMeterPrimed
	{
		get {
			return ((PlayStyle.Equals ("A") && Potency (Adle) < 2 && Vitality.MeterLevel >= 100) 
			        || (PlayStyle.Equals ("B") && Potency (Adle) < 2 && Vitality.MeterLevel >= 100 && CriticalActive)
			        || (PlayStyle.Equals ("C") && Potency (Adle) < 1 && Vitality.MeterLevel >= 300));
		}
	}

	public int TimeRemaining
	{
		get {
			return timeRemaining;
		}
		set {
			timeRemaining = value;
		}
	}

	public Boolean WasHit
	{
		get {
			return wasHit;
		}
		set {
			wasHit = value;
		}
	}

	public Boolean InRecovery
	{
		get {
			return inRecovery;
		}
		set {
			inRecovery = value;
		}
	}

	public int Row
	{
		get {
			return currentLocation ().Row;
		}
	}

	public int Column
	{
		get {
			return currentLocation ().Column;
		}
	}

	public int row ()
	{
		return currentLocation ().Row;
	}

	public int column ()
	{
		return currentLocation ().Column;
	}


	public Boolean HasChangedLocations
	{
		get {
			return hasChangedLocations;
		}
		set {
			hasChangedLocations = value;
		}
	}

	public Boolean HasChangedHeights
	{
		get {
			return hasChangedHeights;
		}
		set {
			hasChangedHeights = value;
		}
	}

	public RawImage MapSprite
	{
		get {
			return mapSprite;
		}
		set {
			mapSprite = value;
		}
	}

	public Boolean PlayerCanIncrementCycle
	{
		get {
			return Fatigue > 0
				|| (((StateActive (Grounded) && !StateActive (Confuse))
				     || StateActive (Airborne)) && (WasHit || InRecovery))

				|| StateActive (Burn) || HurtStun > 0 || GuardStun > 0;
		}
	}

	public Boolean OnBorder
	{
		get {
			return locationOnBorder (currentLocation ().North)
				|| locationOnBorder (currentLocation ().East)
				|| locationOnBorder (currentLocation ().South)
				|| locationOnBorder (currentLocation ().West);
		}
	}

	public Boolean locationOnBorder (Location loc)
	{
		return (!currentMap ().isValid (loc)
			|| (currentMap ().heightOf (loc) > (currentMap ().heightOf (currentLocation ()) + 2))
			|| (!currentMap ().isEmpty (loc)
				&& !currentMap ().objectAt (loc).sentient ()
				&& currentMap ().objectAt (loc).currentHeight () > (currentMap ().heightOf (currentLocation ()) + 2)));

	}

	public int GuardStun
	{
		get
		{	return guardStun;}
		set
		{	guardStun = value;}
	}

	public ArrayList PlayerOutputIndexes
	{
		get {
			return playerOutputIndexes;
		}
		set {
			playerOutputIndexes = value;
		}
	}

	public RawImage ShadowSprite
	{
		get {
			return shadowSprite;
		}
		set {
			shadowSprite = value;
		}
	}

	public Boolean InMovement
	{
		get {
			return  MovementIncrement != null || Destinations.Count > 0 || CurrentAnimationTime == -5.0f;
		}
	}

	public Vector3 Destination
	{
		get {
			return destination;
		}
		set {
			destination = value;
		}
	}

	public Vector3 OriginalPosition
	{
		get {
			return originalPosition;
		}
		set {
			originalPosition = value;
		}
	}

    public ArrayList MovementDirections
    {
        get {
            return movementDirections;
        }
        set {
            movementDirections = value;
        }
    }

    public Boolean MovementDirection
    {
        get {
            return movementDirection;
        }
        set {
            movementDirection = value;
        }
    
    }

	public MapVector MovementIncrement
	{
		get {
			return movementIncrement;
		}
		set {
			movementIncrement = value;
		}
	}

	public ArrayList MovementTextures
	{
		get {
			return movementTextures;
		}
		set {
			movementTextures = value;
		}
	}

	public ArrayList MovementIncrements
	{
		get {
			return movementIncrements;
		}
		set {
			movementIncrements = value;
		}
	}

	public ArrayList Colors
	{
		get
		{
			return colors;
		}
		set
		{
			colors = value;
		}
	}

	public Color BloodPriceColor
	{
		get { return (Color)Colors [0]; }
	}

	public Color LastStandColor
	{
		get { return (Color)Colors [1]; }
	}

	public int ColorsMet
	{
		get
		{
			int activeS = 0;
			if (StateActive(Sleep)) { activeS++;} //1
			if (StateActive(Fury)) { activeS++;} //2
			if (BloodPrice) { activeS++;} //3
			if (LastStand) { activeS++;} //4
			if (StateActive (Sadness)) { activeS++;} //5
			if (StateActive (Adle)) { activeS++;} //6
			if (StateActive (Regen)) { activeS++;} //7
			if (StateActive (Poison)) { activeS++;}  //8
			//if (StateActive (Daze)) { activeS++;} //9
			if (StateActive (Immune)) { activeS++;} //10
			if (StateActive (Invulnerable)) { activeS++;} //11
			if (StateActive (Freeze)) { activeS++;} //12
			if (StateActive (Burn)) { activeS++;} //13
			if (StateActive (Confuse)) { activeS++;} //14
			if (StateActive (Blind)) { activeS++;} //15
			if (StateActive (Learn)) { activeS++;} //16
			return activeS;
		}	
	}

	public void RefreshColors ()
	{
		AvailableColors = new ArrayList ();
		UsedColors = new ArrayList ();
		if (StateActive (Sleep)) { AvailableColors.Add (Sleep.StateColor);} //1
		if (StateActive (Fury)) { AvailableColors.Add (Fury.StateColor);} //2
		if (BloodPrice) { AvailableColors.Add (BloodPriceColor);} //3
		if (LastStand) { AvailableColors.Add (LastStandColor);} //4
		if (StateActive (Sadness)) { AvailableColors.Add (Sadness.StateColor);} //5
		if (StateActive (Adle)) { AvailableColors.Add (Adle.StateColor);} //6
		if (StateActive (Regen)) { AvailableColors.Add (Regen.StateColor);} //7
		if (StateActive (Poison)) { AvailableColors.Add (Poison.StateColor);} //8
		//if (StateActive (Daze)) { AvailableColors.Add (Daze.StateColor);} //9
		if (StateActive (Immune)) { AvailableColors.Add (Immune.StateColor);} //10
		if (StateActive (Invulnerable)) { AvailableColors.Add (Invulnerable.StateColor);} //11
		if (StateActive (Freeze)) { AvailableColors.Add (Freeze.StateColor);} //12
		if (StateActive (Burn)) { AvailableColors.Add (Burn.StateColor);} //13
		if (StateActive (Confuse)) { AvailableColors.Add (Confuse.StateColor);} //14
		if (StateActive (Blind)) { AvailableColors.Add (Blind.StateColor);} //15
		if (StateActive (Learn)) { AvailableColors.Add (Learn.StateColor);} //16
	}

	public ArrayList AvailableColors
	{
		get {
			return importantColors;
		}
		set {
			importantColors = value;
		}
	}

	public ArrayList UsedColors
	{
		get {
			return usedColors;
		}
		set {
			usedColors = value;
		}
	}

	public Boolean willDieIfLanding
	{
		get {
			return !Flight && Math.Abs (previousTileHeight - currentMap ().heightOf (currentLocation ())) < 50;
		}
	}

	public float ColorTimer
	{
		get {
			return colorTimer;
		}
		set {
			colorTimer = value;
		}
	}

	public Boolean ColorWillChange
	{
		get {
			return ColorsMet > 0;
		}
	}


	public int ColorIndex
	{
		get {
			return colorIndex;
		}
		set {
			colorIndex = value;
		}
	}

	public Player (int ind, string fplayer, string sname, string search, string mname, string sx, string prefs, string spe, string nat, int ag, double wgt, double hgt, string clss, string styl, int rat, int timeR, Boolean loadSprites,
                  Boolean tactical)
	{
        manualMode = true;
		CurrentAnimationTime = -1.0f;
		usedColors = new ArrayList ();
        movementSpeed = 0;
		//usedStatuses 
		colorIndex = 0;
		color = Color.blue;
		playerOutputIndexes = new ArrayList ();
		playerStandard = null;
		spriteScale = 1.0f;
		index = ind;
		firstName = fplayer;
		surname = sname;
		searchName = search;
		sex = sx;
		sexualPreference = prefs;
		species = spe;
		nationality = nat;
		age = ag;
		weight = wgt;
		height = hgt;
		playerClass = clss;
		assimilatedClass = "";
		assimilatedTime = 0;
		style = styl;
		ratio = rat;
		engageText = "";
		stageName = mname;
		element = "Fire";
		hasFallen = false;
		handDexterity = "Right";
		timedConditions = new ArrayList ();
		specialState = false;
		lastStand = false;
		noAmmo = new ArrayList ();
		airOff = false;
		skillExecuted = true;
		currentHitRate = 0;
		mapOutputs = new ArrayList ();
		queuedTexture = null;
        cancelSkill = new DataReader ().loadSkill (this, @"Cancel", null);
        cancelSkill.OriginalOwner = this;
        cancelSkill.Owner = this;
        //cancelSkill = new DataReader ().loadSkill ();

        firstTurn = true;
		noMotion = false;
		noGainForMaster = false;
        movementDirection = true;

		staggered = false;
		staggerDirection = "";
		staggerTime = 0;

		myReel = new Reel ();

        opposingTeam = new Team(@"", @"", 1);

		damagePerRound = 0;
		damageTotal = 0;
		damageTakenPerRound = 0;
		damageTakenTotal = 0;
		healingPerRound = 0;
		healingTotal = 0;
		stunPerRound = 0;
		stunTotal = 0;
		victories = 0;
		speedBonuses = 0;
		hurtStun = 0;
        hurtStunRemaining = 0;
        hitStunRemaining = 0;
        blockStunRemaining = 0;
        canInput = true;
        counterHit = true;
		highestCombo = 0;
		colorTimer = 0.0f;

		statusAfflictionsPerRound = 0;
		statusAfflictionsTotal = 0;

		statusAidsPerRound = 0;
		statusAidsTotal = 0;
		direction = "X";
		experience = 0;
		level = 1;
		experienceCheckpoints = new int [102];
		setExperienceCheckpoints ();

		connectsPerRound = 0;
		attacksPerRound = 0;
		connectsTotal = 0;
		attacksTotal = 0;

        healthPresent = 0;
        vitalityPresent = -1;

		leech = false;
		counterState = false;
        parentCanvas = null;

		allSkills = new ArrayList ();
		normalSkills = new ArrayList ();
		specialSkills = new ArrayList ();
		vitalitySkills = new ArrayList ();
		burstSkills = new ArrayList ();
		jumpSkills = new ArrayList ();
		itemSkills = new ArrayList ();
		timeActivatedSkills = new ArrayList ();
		inventory = new ArrayList ();
		chainSkills = new ArrayList ();
		trackSkills = new ArrayList ();
		burstFollowUpSkills = new ArrayList ();

		normalAddedSkills = new ArrayList ();
		specialAddedSkills = new ArrayList ();
		vitalityAddedSkills = new ArrayList ();
		burstAddedSkills = new ArrayList ();
		sactionAddedSkills = new ArrayList ();
		jumpAddedSkills = new ArrayList ();
		itemAddedSkills = new ArrayList ();
		singleAddedSkills = new ArrayList ();

		currentAnimationTimes = new ArrayList ();
        allSkills.Add (cancelSkill);
		currentAnimationTime = -5.0f;

		saves = 0;
		defeats = 0;
		mapSize = 1;
		combo = 0;
		comboDamagePerRound = 0;

		tauntSkill = null;
		importantColors = new ArrayList ();
		colors = new ArrayList ();

		engaging = false;
		isCrouching = false;
		isGuarding = false;
		isDizzied = false;
		guarding = new State ("Guard", "GRD", 0, 0.0, 0, 0, false, "");

		playerMap = null;
		fatigue = 0;
		residualFatigue = 0;
		playStyle = "A";
		r = new System.Random ();
		location = new Location (-1, -1);
		myTeam = null;

		vmGainBlocked = false;
		weakFrame = false;
		bloodPrice = false;
		berserk = false;
		flight = false;
		canFly = false;
		noConditions = false;
		timeStopped = false;
		reverseName = false;
		noGuard = false;
		halfCost = false;
		noCost = false;

		//canChain = false;

		canAct = true;
		hasActed = false;
		hasJumped = false;
		wasCritical = false;
		isResting = false;
		isTaunting = false;
		tauntDecay = 1;
		canRest = true;
		canBeRevived = true;
		criticalActive = false;
		bonusRoundActivated = false;
		sActionEnabled = true;
		hitMaliciously = false;
		wasHit = false;
		inRecovery = false;
		autoGuard = false;
		turnEnded = false;
		undead = false;
        transformationTime = -1;

		spriteRatio = null;

		states = null;
		stateResistances = null;
        jumpDirection = @"";

		storedPlayer = null;

		previousTileHeight = 0;

		timeRemaining = timeR;
		counterPlayer = null;
		chainSkills = new ArrayList ();
		burstFollowUpSkills = new ArrayList ();
		inPlay = true;
		onField = false;
		jumpHeight = 0;
		unitHeight = 0;
		fallingHeight = 0;
		landingHeight = 2;
        activeSkill = null;

		owner = null;
		objectIndex = 0;
		cooldown = 0;
		hitStunAdjustment = 0;
		bonusTime = 0;

		lockedOnTarget = null;
		absorbedPlayer = null;

		longestCombo = new ArrayList ();
		numRests = 0;

		hasChangedHeights = false;
		hasChangedLocations = false;

		activeTimeModulus = 0.0f;
		activeMatchTime = 0.0f;

		inMovement = false;
		destination = new Vector3 (-30, -30, -30);
		originalPosition = new Vector3 (-30, -30, -30);
		destinations = new ArrayList ();
		movementTextures = new ArrayList ();
		movementIncrements = new ArrayList ();
		movementIncrement = null;
		originalPositions = new ArrayList ();
        movementDirections = new ArrayList ();

		hasActedInRound = false;

		emotions = new ArrayList ();

		activePlayer = false;

		lastTarget = null;

		target = false;

        tacticalMode = tactical;

		if (Index > 0 && loadSprites) {
			LoadSprites ();
		}

        champion = false;

        loadedInfo = @"";

        hitStunRemaining = 0;
        hurtStunRemaining = 0;
        blockStunRemaining = 0;
        canInput = true;
        counterHit = false;
        inMotion = false;
	}

	public void setMeters (int hlth, int hlthreg, int rsh, int rshreg, int grd, int grdreg, int vit, int vitreg)
	{
		health = new Meter ("Health", "HP", hlth, hlthreg);
		criticalLimit = health.MeterMax / 6;
		rush = new Meter ("Rush", "RM", rsh, rshreg);
		guard = new Meter ("Gas", "GM", grd, grdreg);
		vitality = new Meter ("Vitality", "VM", 0, vit, vitreg);
		stun = new Meter ("Stun", "ST", 0, (Grit * 2) / 3, 1);
	}

	public void setBaseStats (int str, int grt, int mgk, int res, double dex, double spd, double pro, int mov, int tmw, int lck, int ap)
	{
		strength = new DynamicInt ("Strength", "STR", str);
		grit = new DynamicInt ("Grit", "GRT", grt);
		magick = new DynamicInt ("Magick", "MAG", mgk);
		resistance = new DynamicInt ("Resistance", "RES", res);
		dexterity = new DynamicDouble ("Dexterity", "DEX", dex);
		speed = new DynamicDouble ("Speed", "SPD", spd);
		proration = new DynamicDouble ("Proration", "PRO", pro);
		movement = new DynamicInt ("Movement", "MOV", mov);
		teamwork = new DynamicInt ("Teamwork", "TWK", tmw);
		luck = new DynamicInt ("Luck", "LCK", lck);
		actionPoints = new DynamicInt ("Action Points", "AP", ap);
		currentProration = 1;
		movesRemaining = Movement;
	}

    /**
         *     private float hitStunRemaining;
    private float hurtStunRemaining;
    private float blockStunRemaining;
    private Boolean canInput;
    private Boolean counterHit;

         */




    public Rigidbody2D Body
    {
        get
        {
            return body;
        }
        set
        {
            body = value;
        }
    }

    public int TransformationTime
    {
        get { return transformationTime; }
        set { transformationTime = value; }
    }

    public float HorizontalForce
    {
        get {
            return horizontalForce;
        }
        set {
            horizontalForce = value;
        }
    }

    public float VerticalForce
    {
        get {
            return verticalForce;
        }
        set {
            verticalForce = value;
        }
    }

    public string VerticalPattern
    {
        get
        {
            return verticalPattern;
        }
        set
        {
            verticalPattern = value;
        }
    }

    public string HorizontalPattern
    {
        get
        {
            return horizontalPattern;
        }
        set
        {
            horizontalPattern = value;
        }
    }

    public string MovementType
    {
        get {
            return jumpDirection;
        }
        set {
            jumpDirection = value;
        }
    }

    public Transform ParentCanvas
    {
        get {
            return parentCanvas;
        }
        set {
            parentCanvas = value;
        }
    }

    public Boolean InMotion
    {
        get {
            return inMotion;
        }
        set {
            inMotion = false;
        }
    }

    public float HitStunRemaining
    {
        get {
            return hitStunRemaining;   
        }
        set {
            hitStunRemaining = value;
        }
    }

    public float HurtStunRemaining
    {
        get
        {
            return hurtStunRemaining;
        }
        set
        {
            hurtStunRemaining = value;
        }
    }

    public float BlockStunRemaining
    {
        get
        {
            return blockStunRemaining;
        }
        set
        {
            blockStunRemaining = value;
        }
    }

    public Boolean CanInput
    {
        get
        {
            return canInput;
        }
        set
        {
            canInput = value;
        }
    }

    public Boolean CanMoveSprite
    {
        get {
            return canChangeDirection;
        }
        set {
            canChangeDirection = value;
        }
    }

    public Boolean CounterHit
    {
        get
        {
            return counterHit;
        }
        set
        {
            counterHit = value;
        }
    }

    public void setHealthMeasure ()
    {
        healthPresent = health.MeterLevel;
    }

    public Boolean tickHealthMeasure ()
    {
        if (healthPresent != health.MeterLevel) {
            if (healthPresent > health.MeterLevel) {
                healthPresent--;
            }
            else if (healthPresent < health.MeterLevel) {
                healthPresent++;
            }
            return true;
        }
        return false;
    }

    public void setVitalityMeasure ()
    {
        vitalityPresent = vitality.MeterLevel;
    }

    public Boolean tickVitalityMeasure ()
    {
        //return true;
        if (vitalityPresent != vitality.MeterLevel) {
            if (vitalityPresent > vitality.MeterLevel) {
                vitalityPresent--;
            }
            else if (vitalityPresent < vitality.MeterLevel) {
                vitalityPresent++;
            }
            return true;
        }
        return false;
    }

    public float HealthPresentRatio
    {
        get {
            return ((float)healthPresent) / ((float)health.MeterMax);
        }
    }

    public float VitalityPresentRatio
    {
        get
        {
            return ((float)vitalityPresent) / ((float)vitality.MeterMax);
        }
    }

	public void AdjustForRatio (Boolean inBattle)
	{
		health.MeterMax = health.MeterMax * Ratio;
		if (!inBattle) {
			health.MeterLevel = health.MeterMax;
            health.MeterLevelAppearance = health.MeterLevel;
			health.MeterShift *= Ratio;
		} else {
			health.MeterLevel *= Ratio;
		}

		rush.MeterMax = rush.MeterMax * Ratio;
		if (!inBattle) {
			rush.MeterLevel = rush.MeterMax;
            rush.MeterLevelAppearance = rush.MeterLevel;
			rush.MeterShift *= Ratio;
		} else {
			rush.MeterLevel *= Ratio;
		}

		guard.MeterMax = guard.MeterMax * Ratio;
		if (!inBattle) {
			guard.MeterLevel = guard.MeterMax;
            guard.MeterLevelAppearance = guard.MeterLevel;
			guard.MeterShift *= Ratio;
		} else {
			guard.MeterLevel *= Ratio;
		}

		stun.MeterMax = stun.MeterMax * Ratio;

		if (!inBattle) {
			vitality.MeterLevel += 100 * (Ratio - 1);
		} else {
			vitality.MeterLevel *= Ratio;
		}

		for (int i = 0; i < stateResistances.Length; i ++) {
			if (States [i].Malicious) {
				stateResistances [i].BaseValue += .05 * (Ratio - 1);
			}
		}

		if (!inBattle) {
			teamwork.BaseValue += (Teamwork * (ratio - 1)) / 10;
			speed.BaseValue += 0.01 * (Ratio - 1);

			CriticalLimit *= Ratio;
		}

	}

	public void setStates (double stn, double gnd, double air, double cntr, double pry, double psn, double rgn, double dze, double cnf, double sad, double fur, double slp, double adl, double frz, double imm, double inv, double brn, double lrn, double ini)
	{
		states = new State [21];
		stateResistances = new DynamicDouble [21];

		DataReader rd = new DataReader ();

		states [0] = rd.loadState ("StunHit", stn);
		states [0].Index = 0;
		stateResistances [0] = new DynamicDouble ("Stun Hit", "STN", 0);

		states [1] = rd.loadState ("Grounded", gnd);
		states [1].Index = 1;
		stateResistances [1] = new DynamicDouble ("Grounded", "GND", 0);

		states [2] = rd.loadState ("Airborne", air);
		states [2].Index = 2;
		stateResistances [2] = new DynamicDouble ("Airborne", "AIR", 0);

		states [3] = rd.loadState ("Counter", cntr);
		states [3].Index = 3;
		states[3].StateColor = new Color (121f / 255f, 198f / 255f, 64f / 255f, 1);
		stateResistances [3] = new DynamicDouble ("Counter", "CTR", 0);

		states [4] = rd.loadState ("Parry", pry);
		states [4].Index = 4;
		states[4].StateColor = new Color (102f / 255f, 204f / 255f, 198f / 255f, 1);
		stateResistances [4] = new DynamicDouble ("Parry", "PAR", 0);

		states [5] = rd.loadState ("Poison", psn);
		states [5].Index = 5;
		states [5].StateColor = new Color (102f / 255f, 0f / 255f, 102f / 255f, 1);
		stateResistances [5] = new DynamicDouble ("Poison", "PSN", 0);

		states [6] = rd.loadState ("Regen", rgn);
		states [6].Index = 6;
		states [6].StateColor = new Color (102f / 255f, 198f / 255f, 64f / 255f, 1);
		stateResistances [6] = new DynamicDouble ("Regen", "REG", 0);

		states [7] = rd.loadState ("Daze", dze);
		states [7].Index = 7;
		states [7].StateColor = new Color (255f / 255f, 153f / 255f, 153f / 255f, 1);
		stateResistances [7] = new DynamicDouble ("Daze", "DZE", 0);

		states [8] = rd.loadState ("Confuse", cnf);
		states [8].Index = 8;
		states [8].StateColor = new Color (204f / 255f, 102f / 255f, 153f / 255f, 1);
		stateResistances [8] = new DynamicDouble ("Confuse", "CNF", 0);

		states [9] = rd.loadState ("Sadness", sad);
		states [9].Index = 9;
		states [9].StateColor = new Color (0f / 255f, 0f / 255f, 102f / 255f, 1);
		stateResistances [9] = new DynamicDouble ("Sadness", "SAD", 0);

		states [10] = rd.loadState ("Fury", fur);
		states [10].Index = 10;
		states [10].StateColor = new Color (204f / 255f, 0f / 255f, 0f / 255f, 1);
		stateResistances [10] = new DynamicDouble ("Fury", "FUR", 0);

		states [11] = rd.loadState ("Sleep", slp);
		states [11].Index = 11;
		states [11].StateColor = new Color (204f / 255f, 255f / 255f, 255f / 255f, 1);
		stateResistances [11] = new DynamicDouble ("Sleep", "SLP", 0);

		states [12] = rd.loadState ("Adle", adl);
		states [12].Index = 12;
		states [12].StateColor = new Color (238f / 255f, 130f / 255f, 238f / 255f, 1);
		stateResistances [12] = new DynamicDouble ("Adle", "ADL", 0);

		states [13] = rd.loadState ("Freeze", frz);
		states [13].Index = 13;
		states [13].StateColor = new Color (51f / 255f, 102f / 255f, 255f / 255f, 1);
		stateResistances [13] = new DynamicDouble ("Freeze", "FRZ", 0);

		states [14] = rd.loadState ("Immune", imm);
		states [14].Index = 14;
		states [14].StateColor = new Color (51f / 255f, 102f / 255f, 0f / 255f, 1);
		stateResistances [14] = new DynamicDouble ("Immune", "IMM", 0);

		states [15] = rd.loadState ("Invulnerable", inv);
		states [15].Index = 15;
		states [15].StateColor = new Color(153f / 255f, 153f / 255f, 153f / 255f, 1);
		stateResistances [15] = new DynamicDouble ("Invulnerable", "INV", 0);

		states [16] = rd.loadState ("Burn", brn);
		states [16].Index = 16;
		states [16].StateColor = new Color(255f / 255f, 153f / 255f, 0f / 255f, 1);
		stateResistances [16] = new DynamicDouble ("Burn", "BRN", 0);

		states [17] = rd.loadState ("Learn", lrn);
		states [17].Index = 17;
		states [17].StateColor = new Color(64f / 255f, 224f / 255f, 208f / 255f, 1);
		stateResistances [17] = new DynamicDouble ("Learn", "LRN", 0);

		states [18] = rd.loadState ("Invisible", inv);
		states [18].Index = 18;
		states [18].StateColor = new Color(255f / 255f, 255f / 255f, 0f / 255f, 0);
		stateResistances [18] = new DynamicDouble ("Invisible", "INI", 0);

		states [19] = rd.loadState ("Leech", 0);
		states [19].Index = 19;
		states [19].StateColor = new Color(255f / 255f, 255f / 255f, 0f / 255f, 1);
		stateResistances [19] = new DynamicDouble ("Leech", "LCH", 0);

		states [20] = rd.loadState ("Blind", 0);
		states [20].Index = 20;
		states [20].StateColor = new Color(0f / 255f, 0f / 255f, 0f / 255f, 1);
		stateResistances [20] = new DynamicDouble ("Blind", "BLI", 0);

	}

	public void setElementalProficiency (int fireO, int fireD, int iceO, int iceD, int eleO,
		int eleD, int winO, int winD, int watO, int watD,
		int earO, int earD, int metO, int metD, int darO,
		int darD, int ligO, int ligD, int breO, int breD)
	{
		elementalOffense = new int [10];
		elementalDefense = new int [10];

		elementalOffense [0] = fireO;
		elementalDefense [0] = fireD;

		elementalOffense [1] = iceO;
		elementalDefense [1] = iceD;

		elementalOffense [2] = eleO;
		elementalDefense [2] = eleD;

		elementalOffense [3] = winO;
		elementalDefense [3] = winD;

		elementalOffense [4] = watO;
		elementalDefense [4] = watD;

		elementalOffense [5] = earO;
		elementalDefense [5] = earD;

		elementalOffense [6] = metO;
		elementalDefense [6] = metD;

		elementalOffense [7] = darO;
		elementalDefense [7] = darD;

		elementalOffense [8] = ligO;
		elementalDefense [8] = ligD;

		elementalOffense [9] = breO;
		elementalDefense [9] = breD;
	}


	public void useSprites (Player p)
	{
		neutral = p.neutral;

		crouchNeutral = p.crouchNeutral;
		groundNeutral = p.groundNeutral;
		airNeutral = p.airNeutral;

		damage = p.damage;
		crouchDamage = p.crouchDamage;
		groundDamage = p.groundDamage;
		airDamage = p.airDamage;

		portrait = p.Portrait;

	}

    public void clearSkillsAndItems ()
    {
        /**
         * 
         *          storedPlayer.AllSkills = AllSkills;
            storedPlayer.normalSkills = NormalSkills;
            storedPlayer.specialSkills = SpecialSkills;
            storedPlayer.vitalitySkills = VitalitySkills;
            storedPlayer.burstSkills = BurstSkills;
            storedPlayer.jumpSkills = JumpSkills;
            storedPlayer.itemSkills = ItemSkills;
            storedPlayer.timeActivatedSkills = TimeActivatedSkills;
            storedPlayer.inventory = Inventory;
            storedPlayer.chainSkills = ChainSkills;
            storedPlayer.burstFollowUpSkills = BurstFollowUpSkills;
            if (!storedPlayer.criticalActive) {
                storedPlayer.criticalSkill = CriticalSkill;
            }
            storedPlayer.tauntSkill = TauntSkill;
            storedPlayer.sActionSkill = SActionSkill;
            storedPlayer.counterSkill = CounterSkill;
            storedPlayer.ratio = Ratio;

            storedPlayer.Inventory = Inventory;

         * 
         */ 

        AllSkills = new ArrayList ();
        NormalSkills = new ArrayList ();
        SpecialSkills = new ArrayList();
        VitalitySkills = new ArrayList();
        BurstSkills = new ArrayList ();
        JumpSkills = new ArrayList();
        ItemSkills = new ArrayList ();
        TimeActivatedSkills = new ArrayList();
        //Inventory = new ArrayList ();
        ChainSkills = new ArrayList ();
        BurstFollowUpSkills = new ArrayList ();
        CriticalSkill = null;
        TauntSkill = null;
        SActionSkill = null;
        CounterSkill = null;
        ContactSkill = null;

        for (int i = 0; i < Inventory.Count; i ++) {
            ((Item)Inventory[i]).NumUses = 0;
        }
    }


	public string transform (Player transformed, Boolean transforming, Boolean critActive, Boolean healthMeterChanged, int transTime)
	{
		Boolean cActive = critActive;
		string output = string.Format ("{0} changes!" + '\n', FirstName);
		DataReader reader = new DataReader ();
		if (transforming && storedPlayer == null) {
			storedPlayer = new Player (index, firstName, surname, searchName, stageName, sex,
				sexualPreference, species, nationality, age, weight, height, playerClass, style, ratio, timeRemaining, false,
                                       tacticalMode);
			storedPlayer.setBaseStats (strength.BaseValue, grit.BaseValue, magick.BaseValue, resistance.BaseValue,
				dexterity.BaseValue, speed.BaseValue, proration.BaseValue, movement.BaseValue,
				teamwork.BaseValue, luck.BaseValue, actionPoints.BaseValue);

			storedPlayer.setMeters (Health.MeterMax, Health.MeterShift, Rush.MeterMax, Rush.MeterShift,
				Guard.MeterMax, Guard.MeterShift, Vitality.MeterMax, Vitality.MeterShift);
			storedPlayer.setStates (States [0].Probability, States [1].Probability, States [2].Probability, States [3].Probability,
				States [4].Probability, States [5].Probability, States [6].Probability, States [7].Probability, States [8].Probability,
				States [9].Probability, States [10].Probability, States [11].Probability, States [12].Probability, States [13].Probability,
				States [14].Probability, States [15].Probability, States [16].Probability, States [17].Probability, States [18].Probability);
			storedPlayer.setElementalProficiency (elementalOffense [0], elementalDefense [0], elementalOffense [1], elementalDefense [1],
				elementalOffense [2], elementalDefense [2], elementalOffense [3], elementalDefense [3], elementalOffense [4], elementalDefense [4],
				elementalOffense [5], elementalDefense [5], elementalOffense [6], elementalDefense [6], elementalOffense [7], elementalDefense [7],
				elementalOffense [8], elementalDefense [8], elementalOffense [9], elementalDefense [9]);
			storedPlayer.Conditions = Conditions;

			storedPlayer.Vocals = Vocals;
			storedPlayer.Completed = Completed;
			storedPlayer.useSprites (this);


			storedPlayer.AllSkills = AllSkills;
			storedPlayer.normalSkills = NormalSkills;
			storedPlayer.specialSkills = SpecialSkills;
			storedPlayer.vitalitySkills = VitalitySkills;
			storedPlayer.burstSkills = BurstSkills;
			storedPlayer.jumpSkills = JumpSkills;
			storedPlayer.itemSkills = ItemSkills;
			storedPlayer.timeActivatedSkills = TimeActivatedSkills;
			storedPlayer.inventory = Inventory;
			storedPlayer.chainSkills = ChainSkills;
			storedPlayer.burstFollowUpSkills = BurstFollowUpSkills;
			if (!storedPlayer.criticalActive) {
				storedPlayer.criticalSkill = CriticalSkill;
			}
			storedPlayer.tauntSkill = TauntSkill;
			storedPlayer.sActionSkill = SActionSkill;
			storedPlayer.counterSkill = CounterSkill;
            storedPlayer.contactSkill = ContactSkill;
			storedPlayer.ratio = Ratio;

			storedPlayer.Inventory = Inventory;

			storedPlayer.playStyle = PlayStyle;

			storedPlayer.SpecialState = SpecialState;
			//storedPlayer.CriticalActive = critActive;

			reader.loadSkills (storedPlayer, "Player");
			reader.loadItems (storedPlayer);
		}

		FirstName = transformed.FirstName;
		Surname = transformed.Surname;
		SearchName = transformed.SearchName;
		StageName = transformed.StageName;
		Sex = transformed.Sex;
		SexualPreference = transformed.SexualPreference;
		Species = transformed.Species;
		Nationality = transformed.Nationality;
		Age = transformed.Age;
		Weight = transformed.Weight;
		Height = transformed.Height;
		PlayStyle = transformed.PlayStyle;
		Style = transformed.Style;
		//Ratio = transformed.Ratio;

		Strength = transformed.Strength;
		Grit = transformed.Grit;
		Magick = transformed.Magick;
		Resistance = transformed.Resistance;
		Dexterity = transformed.Dexterity;
		Speed = transformed.Speed;
		Proration = transformed.Proration;
		Movement = transformed.Movement;
		Teamwork = transformed.Teamwork;
		Luck = transformed.Luck;
		ActionPoints = transformed.ActionPoints;

		SpecialState = transformed.SpecialState;

		if (healthMeterChanged) {
			Health.MeterMax = transformed.Health.MeterMax;
			Health.MeterShift = transformed.Health.MeterShift;
			Rush.MeterMax = transformed.Rush.MeterMax;
			Rush.MeterShift = transformed.Rush.MeterShift;
			Guard.MeterMax = transformed.Guard.MeterMax;
			Guard.MeterShift = transformed.Guard.MeterShift;
			Vitality.MeterMax = transformed.Vitality.MeterMax;
			Vitality.MeterShift = transformed.Vitality.MeterShift;
		}

		for (int i = 0; i < States.Length; i ++) {
			States [i].Potency = transformed.States [i].Potency;
			States [i].DoublePotency = transformed.States [i].DoublePotency;
			States [i].NumTurns = transformed.States [i].NumTurns;
			States [i].Probability = transformed.States [i].Probability;
		}

		for (int i = 0; i < ElementalOffense.Length; i++) {
			ElementalOffense [i] = transformed.ElementalOffense [i];
			ElementalDefense [i] = transformed.ElementalDefense [i];
		}

		Vocals.Intros = transformed.Vocals.Intros;
		Vocals.SpecificIntros = transformed.Vocals.SpecificIntros;
		Vocals.Taunts = transformed.Vocals.Taunts;
		Vocals.Victories = transformed.Vocals.Victories;
		Vocals.SpecificVictories = transformed.Vocals.SpecificVictories;
		Vocals.Criticals = transformed.Vocals.Criticals;
		Vocals.Defeats = transformed.Vocals.Defeats;
		Vocals.FinalVictories = transformed.Vocals.FinalVictories;
		Vocals.SpecificFinalVictories = transformed.Vocals.SpecificFinalVictories;
		Vocals.FinalDefeats = transformed.Vocals.FinalDefeats;
		Vocals.SpecificFinalDefeats = transformed.Vocals.SpecificFinalDefeats;
		Vocals.Appreciations = transformed.Vocals.Appreciations;
		Vocals.SpecificAppreciations = transformed.Vocals.SpecificAppreciations;

		AllSkills = transformed.AllSkills;
		normalSkills = transformed.NormalSkills;
		specialSkills = transformed.SpecialSkills;
		vitalitySkills = transformed.VitalitySkills;
		burstSkills = transformed.BurstSkills;
		jumpSkills = transformed.JumpSkills;
		itemSkills = transformed.ItemSkills;
		timeActivatedSkills = transformed.TimeActivatedSkills;
		inventory = transformed.Inventory;
		chainSkills = transformed.ChainSkills;
		burstFollowUpSkills = transformed.BurstFollowUpSkills;
		if (!CriticalActive) {
			criticalSkill = transformed.CriticalSkill;
		}
		tauntSkill = transformed.TauntSkill;
		koSkill = transformed.KOSkill;
		sActionSkill = transformed.SActionSkill;
		counterSkill = transformed.CounterSkill;
        contactSkill = transformed.ContactSkill;

		for (int i = 0; i < AllSkills.Count; i++) {
			((Skill)AllSkills [i]).Owner = this;
			((Skill)AllSkills [i]).setLocations ();
		}

		Completed = transformed.Completed;

		Conditions = transformed.Conditions;
		setConditions (Conditions);

		PlayStyle = transformed.PlayStyle;
		//CriticalActive = critActive;

		if (transforming) {
            LoadSprites ();
			AdjustForRatio (true);
		} else {
			useSprites (transformed);
			storedPlayer = null;
		}

        TransformationTime = transTime;
		return output;
	}

    public int HealthPresent
    {
        get {
            return healthPresent;
        }
        set {
            healthPresent = value; 
        }
    }

    public int VitalityPresent
    {
        get
        {
            return vitalityPresent;
        }
        set
        {
            vitalityPresent = value;
        }
    }

    public string LoadedInfo
    {
        get {
            return loadedInfo;
        }
        set {
            loadedInfo = value;
        }
    }

	public Boolean ActivePlayer
	{
		get {
			return activePlayer;
		}
		set {
			activePlayer = value;
		}
	}

	public Boolean NoGainForMaster
	{
		get {
			return noGainForMaster;
		}
		set {
			noGainForMaster = value;
		}
	}

	public Texture2D QueuedTexture
	{
		get {
			return queuedTexture;
		}
		set {
			queuedTexture = value;
		}
	}

	public int Saves
	{
		get {
			return saves;
		}
		set {
			saves = value;
		}
	}

	public int Defeats
	{
		get {
			return defeats;
		}
		set {
			defeats = value;
		}
	}

	public Player LastTarget
	{
		get {
			return lastTarget;
		}
		set {
			lastTarget = value;
		}
	}

	public Boolean CanActNext
	{
		get {
			return OnField && !KOd && Potency (Sleep) == 1 && !HasActedInRound;
		}
	}

	public RawImage StatusIcon
	{
		get {
			return statusIcon;
		}
		set {
			statusIcon = value;
		}
	}

	public ArrayList CurrentAnimationTimes
	{
		get {
			return currentAnimationTimes;
		}
		set {
			currentAnimationTimes = value;
		}
	}

	public float CurrentAnimationTime
	{
		get {
			return currentAnimationTime;
		}
		set {
			currentAnimationTime = value;
		}
	}

	public Vector3 CentralPosition
	{
		get {
			if (MapSprite != null && MapSprite.transform != null && MapSprite.transform.position != null) {
				return new Vector3 (MapSprite.transform.position.x, MapSprite.transform.position.y + (MapSprite.rectTransform.sizeDelta.y / spriteScale), 0);
			}
			throw new NullReferenceException (Name + " " + currentLocation () + " isn't valid");
		}
	}

	public RawImage HealthBar
	{
		get {
			return healthBar;
		}
		set {
			healthBar = value;
		}
	}

	public RawImage HealthBarBackground
	{
		get {
			return healthBarBackground;
		}
		set {
			healthBarBackground = value;
		}
	}


	public int HighestCombo
	{
		get { return highestCombo;}
		set { highestCombo = value;}
	}

	public Boolean AirOff
	{
		get {
			return airOff;
		}
		set {
			airOff = value;
		}
	}

	public Boolean SpecialState
	{
		get {
			return specialState;
		}
		set {
			specialState = value;
		}
	}

	public Boolean LastStand
	{
		get {
			return lastStand;
		}
		set {
			lastStand = value;
		}
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

	public int ExperienceDisplay
	{
		get {
			if (Level > 1 && Level != 101) {
				return Experience - ExperienceCheckpoints [Level];
			}
			if (Level == 101) {
				return 0;
			}
			return Experience;
		}
	}

	public string ExperienceCheckpointDisplay
	{
		get {
			if (Level <= 99) {
				if (Level > 1) {
					return "" + (ExperienceCheckpoints [Level + 1] - ExperienceCheckpoints [Level]);
				}
				return "" + (ExperienceCheckpoints [Level + 1]);
			}
			return "∞";
		}
	}

	public int ExperienceCheckpointDisplayInt
	{
		get {
			if (Level < 101) {
				if (Level > 1) {
					return (ExperienceCheckpoints [Level + 1] - ExperienceCheckpoints [Level]);
				}
				return (ExperienceCheckpoints [Level + 1]);
			}
			return 1;
		}
	}

	public ArrayList MapOutputs
	{
		get { return mapOutputs;}
		set { mapOutputs = value;}
	}

	public int PerformanceRating
	{
		/**
		p = (Player)Roster [i];
		if (p.HealingTotal + Math.Abs (p.DamageTotal) > output) {
			activeP = p;
			output = p.HealingTotal + Math.Abs (p.DamageTotal);
		}
		*/
		get {
			int output = Math.Abs (DamageTotal) + HealingTotal + (Victories * 1000) + (Defeats * -1000) + (Saves * 500);
			if (output > 0) {
				return output;
			}
			return 0;
		}

	}

	public int NumRests
	{
		get {
			return numRests;
		}
		set {
			numRests = value;
		}
	}

	public int Level
	{
		get {
			return level;
		}
		set {
			level = value;
		}
	}

	public int [] ExperienceCheckpoints
	{
		get {
			return experienceCheckpoints;
		}
		set {
			experienceCheckpoints = value;
		}
	}

	public void setExperienceCheckpoints ()
	{
		int checkpoint = 200;
		experienceCheckpoints [0] = 0;
		experienceCheckpoints [1] = 100;

		for (int i = 2; i < experienceCheckpoints.Length; i++) {
			experienceCheckpoints [i] = checkpoint;
			checkpoint = (int)(((double)checkpoint * 3) / (double)1.8);
		}
	}

	public ArrayList LongestCombo
	{
		get {
			return longestCombo;
		}
		set {
			longestCombo = value;
		}
	}

	public int HitStunAdjustment
	{
		get { return hitStunAdjustment;}
		set { hitStunAdjustment = value;}
	}

	public Boolean TurnEnded
	{
		get {
			return turnEnded;
		}
		set {
			turnEnded = value;
		}
	}

	public Player AbsorbedPlayer
	{
		get {
			return absorbedPlayer;
		}
		set {
			absorbedPlayer = value;
		}
	}

	public int SpeedBonuses
	{
		get {
			return speedBonuses;
		}
		set {
			speedBonuses = value;
		}
	}

	public string Element
	{
		get {
			return element;
		}
		set {
			element = value;
		}
	}

	public Player pairedCharacter (string sName) {

		Location [] locs = new Location [4];
		locs [0] = currentLocation ().North;
		locs [1] = currentLocation ().East;
		locs [2] = currentLocation ().South;
		locs [3] = currentLocation ().West;

		for (int i = 0; i < locs.Length; i++) {
			if (currentMap ().isValid (locs [i]) && !currentMap ().isEmpty (locs [i]) && currentMap ().objectAt (locs [i]).mapName ().Equals (sName)) {
				return (Player)currentMap ().objectAt (locs [i]);
			}
		}
		return null;
	}

	public Boolean FirstTurn
	{
		get {
			return firstTurn;
		}
		set {
			firstTurn = value;
		}
	}

	public ArrayList TimedConditions
	{
		get {
			return timedConditions;
		}
		set {
			timedConditions = value;
		}
	}

	public Player Owner
	{
		get {
			return owner;
		}
		set {
			owner = value;
		}
	}

	public Boolean BonusRoundActivated
	{
		get {
			return bonusRoundActivated;
		}
		set {
			bonusRoundActivated = value;
		}
	}

	public Boolean GetsBonusTime
	{
		get {
			if (!KOd && !IsResting && !StateActive (Sleep) && !IsTaunting && !TurnEnded && !StateActive (Daze)) {
				if (BonusTime > .75) {
					double rnd = r.NextDouble () + (BonusTime - .75);
					if (rnd > 1) {
						return true;
					}
				}
			}
			return false;
		}

	}

	public DynamicDouble [] StateResistances
	{
		get { return stateResistances;}
		set { stateResistances = value;}
	}

	public string StageName
	{
		get {
			return stageName;
		}
		set {
			stageName = value;
		}
	}

	public Player StoredPlayer
	{
		get { return storedPlayer;}
		set { storedPlayer = value;}
	}

	public Boolean HasJumped
	{
		get {
			return hasJumped
				&& (!StateActive (Invulnerable) || !StateActive (Immune));

		
		}
		set { hasJumped = value;}
	}

	public Boolean CanRest
	{
		get {
			return canRest && !StateActive (Confuse) && !StateActive (Fury);
		}
		set {
			canRest = value;
		}
	}

	public Boolean CanBeRevived
	{
		get {
			return canBeRevived;
		}
		set {
			canBeRevived = value;
		}
	}

	public Boolean CanAim
	{
		get {
			return !StateActive (Confuse) && !StateActive (Daze);
		}
	}

	public ArrayList ChainSkills
	{
		get { return chainSkills;}
		set { chainSkills = value;}
	}

	public ArrayList BurstFollowUpSkills
	{
		get { return burstFollowUpSkills;}
		set { burstFollowUpSkills = value;}
	}

	public ArrayList TimeActivatedSkills
	{
		get { return timeActivatedSkills;}
		set { timeActivatedSkills = value;}
	}

	public Boolean CanAct
	{
		get { return !KOd && canAct;}
		set { canAct = value;}
	}

	public Boolean CanUseVitalitySkills
	{
		get { return Vitality.MeterLevel >= 100;}
	}

	public Boolean CanUseBurstSkills
	{
		get { return Vitality.MeterLevel >= 300;}
	}

	public Boolean Target
	{
		get {
			return target;
		}
		set {
			target = value;
		}
	}

	public ArrayList Inventory
	{
		get { return inventory;}
		set { inventory = value;}
	}

	public Item inventoryGet (string sName)
	{
		for (int i = 0; i < Inventory.Count; i++) {
			if (((Item)Inventory [i]).SearchName.Equals (sName)) {
				return (Item)Inventory [i];
			}
		}
		return null;
	}

	public Skill skillGet (string sName)
	{
		for (int i = 0; i < AllSkills.Count; i++) {
			if (((Skill)AllSkills [i]).SearchName.Equals (sName)) {
				return (Skill)AllSkills [i];
			}
		}
		return null;
	}



	public Boolean addItem (Item i)
	{
		if (!inventory.Contains (i)) {
			Inventory.Add (i);
			return true;
		} else if (i.CanDuplicate) {
			getItem (i.Name).NumUses += i.NumUses;
			return true;
		}
		return false;
	}

	public Item getItem (string nm)
	{
		Item it;
		for (int i = 0; i < Inventory.Count; i++) {
			it = (Item)Inventory [i];
			if (it.Name.Equals (nm)) {
				return it;
			}
		}
		return null;
	}

	public Boolean Engaging
	{
		get { return engaging;}
		set { engaging = value;}
	}

	public Boolean HalfCost
	{
		get { return halfCost;}
		set { halfCost = value;}
	}

	public Boolean NoCost
	{
		get { return noCost;}
		set { noCost = value;}
	}

	public Boolean InventoryActive
	{
		get
		{
			for (int i = 0; i < Inventory.Count; i++)
			{
				if (((Item)Inventory [i]).NumUses == 0 || !((Item)Inventory [i]).SpecialConditions.Contains ("PASSIVE "))
				{
					return true;
				}
			}
			return false;
		}
	}

	public Boolean VMGainBlocked
	{
		get { return vmGainBlocked;}
		set { vmGainBlocked = value;}
	}

	public Boolean itemsContain (string token)
	{
		Item it;
		for (int i = 0; i < Inventory.Count; i++)
		{
			it = (Item)Inventory [i];
			if (it.SpecialConditions.Contains (token) && it.NumUses != 0)
			{
				return true;
			}
		}
		return false;
	}

	public Boolean WeakFrame
	{
		get { return weakFrame || itemsContain ("WEAKFRAME ");}
		set { weakFrame = value;}
	}

	public Boolean BloodPrice
	{
		get { return bloodPrice || itemsContain ("BLOODPRICE ");}
		set { bloodPrice = value;}
	}

	public Boolean Berserk
	{
		get { return berserk || itemsContain ("BERSERK ");}
		set { berserk = value;}
	}

	public Boolean Flight
	{
		get { return flight || itemsContain ("FLIGHT ");}
		set { flight = value;}
	}

	public Boolean CanFly
	{
		get { return canFly || itemsContain ("CANFLY ");}
		set { canFly = value;}
	}

	public Boolean TimeStopped
	{
		get { return timeStopped || itemsContain ("TIMESTOP ");}
		set { timeStopped = value;}
	}

	public Boolean NoGuard
	{
		get { return noGuard || itemsContain ("NOGUARD ");}
		set { noGuard = value;}
	}

	public Boolean NoConditions
	{
		get { return noConditions || itemsContain ("NOCONDITIONS ");}
		set { noConditions = value;}
	}

	public int Cooldown
	{
		get { return cooldown;}
		set { cooldown = value;}
	}

	public string setAction (Boolean cond)
	{
		canAct = cond;
		return string.Format ("{0} can act: {1}" + '\n', FirstName, canAct);
	}

	public float BonusTime
	{
		get {
			return bonusTime;
		}
		set {
			bonusTime = value;
		}
	}

	public System.Random R
	{
		get { return r;}
	}

	public void setConditions (string [] cond)
	{
		conditions = cond;
		for (int i = 0; i < cond.Length; i++) {
			setCondition (conditions [i]);
		}
	}

	public int JumpHeight
	{
		get {
			return jumpHeight;
		}
		set {
			jumpHeight = value;
		}
	}

	public Boolean Undead
	{
		get {
			return undead;
		}
		set {
			undead = value;
		}
	}

	public Color AppropriateColor
	{
		get {
			if (Completed) {
				return color;
			}
			if (MyTeam.Abbreviation.Equals ("1")) {
				return Color.red;
			}
			return Color.blue;
		}
	}

	private void setCondition (string cond)
	{
		DataReader reader = new DataReader ();
		if (cond.Contains ("JUMP:")) {
			jumpHeight = NumberConverter.ConvertToInt (cond.Substring (cond.IndexOf (':') + 1, 1));
			learn (reader.loadSkill (this, "Landing" + NumberConverter.ConvertToInt (cond.Substring (cond.IndexOf (':') + 1, 1)), null), false, -1);
		} else if (cond.Contains ("JUMPS")) {
			learn (reader.loadSkill (this, searchName, "Jump", "Player", null), false, -1);
			//learn (reader.loadSkill (this, "Landing" + NumberConverter.ConvertToInt (cond.Substring (cond.IndexOf (':') + 1, 1)), null), false);
		} else if (cond.Contains ("TAUNT")) {
			learn (reader.loadSkill (this, searchName, "Taunt", "Player", null), false, -1);
			//learn (reader.loadSkill (this, "Landing" + NumberConverter.ConvertToInt (cond.Substring (cond.IndexOf (':') + 1, 1)), null), false);
		} else if (cond.Contains ("COLOR:")) {
			string clr = cond.Substring (cond.IndexOf (':') + 1);
			if (clr.Equals ("RED")) {
				color = Color.red;
			}
			if (clr.Equals ("GREEN")) {
				color = Color.green;
			}
			if (clr.Equals ("YELLOW")) {
				color = Color.yellow;
			}
			if (clr.Equals ("GREY")) {
				color = Color.grey;
			}
			if (clr.Equals ("CYAN")) {
				color = Color.cyan;
			}
			if (clr.Equals ("MAGENTA")) {
				color = Color.magenta;
			}
			if (clr.Equals ("ORANGE")) {
				color = new Color (255.0f / 255.0f, 165.0f / 255.0f, 0.0f / 255.0f);
			}
			if (clr.Equals ("WHITE")) {
				color = Color.white;
			}
			if (clr.Equals ("LIGHTPURPLE")) {
				color = new Color (236.0f / 255.0f, 185.0f / 255.0f, 252.0f / 255.0f);
			}
			if (clr.Equals ("PURPLE")) {
				color = new Color (128.0f / 255.0f, 0.0f / 255.0f, 128.0f / 255.0f);
			}
			if (clr.Equals ("MINT")) {
				color = new Color (80.0f / 255.0f, 100.0f / 255.0f, 80.0f / 255.0f);
			}  
		} else if (cond.StartsWith ("SIZE")) {
			mapSize = NumberConverter.ConvertToInt (cond.Substring (cond.IndexOf (':') + 1, 1));
		}
		/**
		else if (cond.Contains ("DASH")) {
			learn (reader.loadSkill (this, "Dash", null), false);
			//learn (reader.loadSkill (this, "Landing" + NumberConverter.ConvertToInt (cond.Substring (cond.IndexOf (':') + 1, 1)), null), false);
		}
		else if (cond.Contains ("RUN")) {
			learn (reader.loadSkill (this, "Run", null), false);
			//learn (reader.loadSkill (this, "Landing" + NumberConverter.ConvertToInt (cond.Substring (cond.IndexOf (':') + 1, 1)), null), false);
		}*/
		else if (cond.StartsWith ("GRAB")) {
			learn (reader.loadGrab (this, searchName, false, 0), false, -1);
		} else if (cond.StartsWith ("AIRGRAB")) {
			learn (reader.loadGrab (this, searchName, true, NumberConverter.ConvertToInt ("" + cond [7])), false, -1);
		} else if (cond.Equals ("UNDEAD")) {
			undead = true;
		} else if (cond.Equals ("NOMOVEMENT")) {
			noMotion = true;
		} else if (cond.Equals ("NOVM")) {
			vmGainBlocked = true;
		} else if (cond.Equals ("WEAKFRAME")) {
			weakFrame = true;
		} else if (cond.Equals ("BLOODPRICE")) {
			bloodPrice = true;
		} else if (cond.Equals ("BERSERK")) {
			berserk = true;
		} else if (cond.Equals ("FLIGHT")) {
			flight = true;
		} else if (cond.Equals ("CANFLY")) {
			canFly = true;
		} else if (cond.Equals ("NOCONDITIONS")) {
			noConditions = true;
		} else if (cond.Equals ("LEFTHANDED")) {
			handDexterity = "Left";
		} else if (cond.Equals ("AMBIDEX")) {
			handDexterity = "Both";
		} else if (cond.Equals ("LAND")) {
			landingHeight = NumberConverter.ConvertToInt (cond.Substring (cond.IndexOf (':') + 1, 1));
		} else if (cond.Equals ("REVERSENAME")) {
			reverseName = true;
		} else if (cond.Equals ("NOGUARD")) {
			noGuard = true;
		} else if (cond.Equals ("MAXVM")) {
			Vitality.MeterLevel = 600;
		} else if (cond.Contains ("BASE")) {
			if (storedPlayer == null) {
				storedPlayer = new DataReader ().loadPlayer (cond.Substring (cond.IndexOf (':') + 1), "Player", true, true,
                                                             tacticalMode);
			}
		} else if (cond.Contains ("CRITICAL")) {
			criticalLimit = NumberConverter.ConvertToInt (cond.Substring (cond.IndexOf (':') + 1, 1));
		} else if (cond.Contains ("NOAMMO")) {
			noAmmo.Add (cond.Substring (cond.IndexOf (':') + 1));
		} else if (cond.Contains ("ELEMENT:")) {
			element = cond.Substring (cond.IndexOf (':') + 1);
		} else if (cond.Contains ("COUNTER")) {
			Counter.Add (new State ("Counter", "CTR", NumberConverter.ConvertToInt (cond.Substring (cond.IndexOf (':') + 1)), 0.0, -1, 1.0, false, ""));
		} else if (cond.Contains ("PARRY")) {
			Parry.Add (new State ("Parry", "PAR", NumberConverter.ConvertToInt (cond.Substring (cond.IndexOf (':') + 1)), 0.0, -1, 1.0, false, ""));
		} else if (cond.Contains ("POISON")) {
			Poison.Add (new State ("Poison", "PSN", NumberConverter.ConvertToInt (cond.Substring (cond.IndexOf (':') + 1)), 0.0, -1, 1.0, false, ""));
		} else if (cond.Contains ("REGEN")) {
			Regen.Add (new State ("Regen", "REG", NumberConverter.ConvertToInt (cond.Substring (cond.IndexOf (':') + 1)), 0.0, -1, 1.0, false, ""));
		} else if (cond.Contains ("DAZE")) {
			Daze.Add (new State ("Daze", "DZE", NumberConverter.ConvertToInt (cond.Substring (cond.IndexOf (':') + 1)), 0.0, -1, 1.0, false, ""));
		} else if (cond.Contains ("CONFUSE")) {
			Confuse.Add (new State ("Confuse", "CON", NumberConverter.ConvertToInt (cond.Substring (cond.IndexOf (':') + 1)), 0.0, -1, 1.0, false, ""));
		} else if (cond.Contains ("SADNESS")) {
			Sadness.Add (new State ("Sadness", "SAD", NumberConverter.ConvertToInt (cond.Substring (cond.IndexOf (':') + 1)), 0.0, -1, 1.0, false, ""));
		} else if (cond.Contains ("FURY")) {
			Fury.Add (new State ("Fury", "FUR", NumberConverter.ConvertToInt (cond.Substring (cond.IndexOf (':') + 1)), 0.0, -1, 1.0, false, ""));
		} else if (cond.Contains ("SLEEP")) {
			Sleep.Add (new State ("Sleep", "SLE", NumberConverter.ConvertToInt (cond.Substring (cond.IndexOf (':') + 1)), 0.0, -1, 1.0, false, ""));
		} else if (cond.Contains ("ADLE")) {
			Adle.Add (new State ("Adle", "ADL", NumberConverter.ConvertToInt (cond.Substring (cond.IndexOf (':') + 1)), 0.0, -1, 1.0, false, ""));
		} else if (cond.Contains ("FREEZE")) {
			Freeze.Add (new State ("Freeze", "FRZ", NumberConverter.ConvertToInt (cond.Substring (cond.IndexOf (':') + 1)), 0.0, -1, 1.0, false, ""));
		} else if (cond.Contains ("IMMUNE")) {
			Immune.Add (new State ("Immune", "IMM", NumberConverter.ConvertToInt (cond.Substring (cond.IndexOf (':') + 1)), 0.0, -1, 1.0, false, ""));
		} else if (cond.Contains ("INVULNERABLE")) {
			Invulnerable.Add (new State ("Invulnerable", "INV", NumberConverter.ConvertToInt (cond.Substring (cond.IndexOf (':') + 1)), 0.0, -1, 1.0, false, ""));
		} else if (cond.Contains ("BURN")) {
			Burn.Add (new State ("Burn", "BRN", NumberConverter.ConvertToInt (cond.Substring (cond.IndexOf (':') + 1)), 0.0, -1, 1.0, false, ""));
		} else if (cond.Contains ("INVISIBLE")) {
			Invisible.Add (new State ("Invisible", "IVI", NumberConverter.ConvertToInt (cond.Substring (cond.IndexOf (':') + 1)), 0.0, -1, 1.0, false, ""));
		} else if (cond.Contains ("LEARN")) {
			Learn.Add (new State ("Learn", "LRN", NumberConverter.ConvertToInt (cond.Substring (cond.IndexOf (':') + 1)), 0.0, -1, 1.0, false, ""));
		} else if (cond.StartsWith ("SCALE")) {
			spriteScale = (float)NumberConverter.ConvertToDouble (cond.Substring (cond.IndexOf (':') + 1));
		} else if (cond.StartsWith ("LVL")) {
			Level = NumberConverter.ConvertToInt ("" + cond [3]);
		} else if (cond.StartsWith ("COMPLETE")) {
			Completed = true;
		}
 	}

	public string SelectInfo
	{
		get {
			string output = "";
			output += string.Format (Health.CurrentState + '\n');
			output += string.Format (strength.ToString () + '\n');
			output += string.Format (grit.ToString () + '\n');
			output += string.Format (magick.ToString () + '\n');
			output += string.Format (resistance.ToString () + '\n');
			output += string.Format (speed.ToString () + '\n');
			output += string.Format (dexterity.ToPercent () + '\n');
			output += string.Format (proration.ToPercent () + '\n');
			output += string.Format (movement.ToString () + '\n');
			output += string.Format (teamwork.ToString () + '\n');
			output += string.Format (luck.ToString () + '\n');
			output += string.Format ("Trait: " + SActionSkill.Name + '\n');
			//output += string.Format ("" + '\t' + SActionSkill.Description);
			return output;
		}
	}

	public float SpriteScale
	{
		get {
			return (float)spriteScale;
		}
		set {
			spriteScale = value;
		}
	}

	public string StatesToString
	{
		get {
			string output = "";
			for (int i = 1; i < States.Length; i++) {
				if (StateActive (States [i])) {
					output += string.Format ("{0} {1}, {2} turns" + '\n', States [i].Name, Potency (States [i]), States [i].NumTurns);
				}
			}
			return output;
		}
	}

	public int Fatigue
	{
		get { return fatigue;}

		set {
			fatigue = value;
			if (fatigue < 0) {
				fatigue = 0;
			}
		}
	}

	public int ResidualFatigue
	{
		get { return residualFatigue;}
		set {

			residualFatigue = value;
			if (residualFatigue < 0) {
				residualFatigue = 0;
			}
		}
	}

	public Boolean SActionEnabled
	{
		get { return SActionSkill != null && sActionEnabled && !PlayStyleActual.Equals ("B");}
		set { sActionEnabled = value;}
	}

	public void AddFatigue (int amount)
	{
		if (Fatigue + amount > ResidualFatigue) {
			Fatigue += amount;
			ResidualFatigue = Fatigue;
		} else {
			Fatigue += amount;
		}
		if (Fatigue == 0) {
			ResidualFatigue = 0;
		}
	}

	public void SetFatigue (int amount)
	{
		Fatigue = amount;
		ResidualFatigue = amount;
	}


	public Boolean NoMotion
	{
		get {
			return noMotion;
		}
		set {
			noMotion = value;
		}
	}

	public string PlayStyle
	{
		get { return playStyle;}
		set {
			playStyle = value;
			if (value.Equals ("A")) {
				for (int i = 0; i < Conditions.Length; i++)
				{
					if (Conditions [i].Contains ("NOEQP:A")) {
						for (int j = 0; j < Inventory.Count; j++) { ((Item)Inventory[j]).NumUses = 0; }
						break;
					}
				}
				DataReader reader = new DataReader ();
				if (!NoMotion) {
					learnAtBeginning (reader.loadSkill (this, "Dash", null), false);
				}
			} if (value.Equals ("B") && !CriticalActive) {
				for (int i = 0; i<Conditions.Length; i++)
				{
					if (Conditions[i].Contains("NOEQP:B")) {
						for (int j = 0; j<Inventory.Count; j++) { ((Item)Inventory[j]).NumUses = 0; }
						break;
					}
				}
				DataReader reader = new DataReader ();
				learnAtBeginning (reader.loadSkill (this, SearchName, "LastStand", "Player", null), false);
				if (!NoMotion) {
					learnAtBeginning (reader.loadSkill (this, "Run", null), false);
				}
			} if (value.Equals ("C")) {
				for (int i = 0; i<Conditions.Length; i++)
				{
					if (Conditions[i].Contains("NOEQP:C")) {
						for (int j = 0; j<Inventory.Count; j++) { ((Item)Inventory[j]).NumUses = 0; }
						break;
					}
				}
				DataReader reader = new DataReader ();
				learnAtBeginning (reader.loadSkill (this, "AutoGuard", null), false);
				sActionEnabled = false;
				adjustListForStrongBasics (AllSkills);
				adjustListForStrongBasics (NormalSkills);
				adjustListForStrongBasics (SpecialSkills);
				adjustListForStrongBasics (VitalitySkills);
				adjustListForStrongBasics (BurstSkills);
				adjustListForStrongBasics (ItemSkills);
				adjustListForStrongBasics (JumpSkills);
				Proration = 1; 
				if (PlayerClass.Equals ("Balanced")) {
					Strength += 50 / 2;
					Grit += 50 / 2;
					Magick += 50 / 2;
					Resistance += 50 / 2;
				} else if (PlayerClass.Equals ("Learn")) {
					Strength += 50 / 2;
					Grit += 50 / 2;
					Magick += 50 / 2;
					Resistance += 50 / 2;
				} else if (PlayerClass.Equals ("Strength")) {
					Strength += 200 / 2;
				} else if (PlayerClass.Equals ("Grit")) {
					Grit += 200 / 2;
				} else if (PlayerClass.Equals ("Physicality")) {
					Strength += 100 / 2;
					Grit += 100 / 2;
				} else if (PlayerClass.Equals ("Power")) {
					Magick += 200 / 2;
				} else if (PlayerClass.Equals ("Resistance")) {
					Resistance += 200 / 2;
				} else if (PlayerClass.Contains ("Magicka")) {
                    if (PlayerClass.Contains("Defense"))
                    {
                        Magick += 75 / 2;
                        Resistance += 125 / 2;
                    }
                    else if (PlayerClass.Contains("Offense"))
                    {
                        Magick += 125 / 2;
                        Resistance += 75 / 2;
                    }
                    else
                    {
                        Magick += 100 / 2;
                        Resistance += 100 / 2;
                    }
				} else if (PlayerClass.Equals ("Offense")) {
					Strength += 100 / 2;
					Magick += 100 / 2;
				} else if (PlayerClass.Equals ("Defense")) {
					Grit += 100 / 2;
					Resistance += 100 / 2;
				} else if (PlayerClass.Equals ("Agility")) {
					Speed += .5 / 2;
					Dexterity += .5 / 2;
				} else if (PlayerClass.Equals ("Speed")) {
					Speed += 1.0 / 2;
				} else if (PlayerClass.Equals ("Dexterity")) {
					Dexterity += 1.0 / 2;
				} else if (PlayerClass.Equals ("Tank")) {
					Strength += 80 / 2;
					Grit += 80 / 2;
                    Inventory.Add (new DataReader ().loadItem (SearchName, NoAmmo, @"Armor"));

					//Invulnerable.Add (new State ("Invulnerable", "INV", 1, 0.0, -1, 1.0, false, ""));
				} else if (PlayerClass.Equals ("Boss")) {
					Strength += 100 / 4;
					Grit += 100 / 4;
					Magick += 100 / 4;
					Resistance += 100 / 4;
				} else if (PlayerClass.Equals ("Action")) {
					ActionPoints += 10;
				}
			}
		}
	}

	public string PlayStyleDisplay
	{
		get {
			if (PlayStyle.Equals ("A")) {
				//return "Well-Balanced";
				return "A-ISM";
			} if (PlayStyle.Equals ("B")) {
				//return "Critical Thinking";
				return "C-ISM";
			}// return "Strong Basics";
			return "B-ISM";
		}
	}

	public string PlayStyleActual
	{
		get {
			if (PlayStyle.Equals ("B")) {
				return "C";
			} if (PlayStyle.Equals ("C")) {
				return "B";
			}
			return "A";
		}
	}

	public string LevelUp
	{
		get {
			string output = "";

			Level++;
			output += string.Format ("{0} reaches Lv. {1}!" + '\n', FirstName, Level);
			//output += string.Format ("{0} XP to Lv. {1}." + '\n', ExperienceCheckpoints [Level + 1], Level + 1);

			if (firstName != "") {
				Health.MeterShift += 10 * (Ratio - 1);
			}

			for (int i = 0; i < States.Length; i++)
			{
				if (States [i].Malicious)
				{
					States [i].Probability += .01;
				}
			}


			if (PlayerClass.Equals ("Balanced") || PlayerClass.Equals ("Learn")) {
				//STATS
				strength.LevelStat += 10 + (Ratio - 1);
				grit.LevelStat += 10 + (Ratio - 1);
				magick.LevelStat += 10 + (Ratio - 1);
				resistance.LevelStat += 10 + (Ratio - 1);
				speed.LevelStat += .01 + ((((double)Ratio - 1) / 100));
				dexterity.LevelStat += .01 + ((((double)Ratio - 1) / 100));;
				//BARS
				Health.MeterMax += 200 * Ratio;
				Health.MeterLevel += 200 * Ratio;
				Rush.MeterMax += 10 * Ratio;
				Guard.MeterMax += 10 * Ratio;
				Stun.MeterMax += 2 * Ratio;
				//SHIFT
				Rush.MeterShift += 1 * Ratio;
				Guard.MeterShift += 1 * Ratio;

				//BONUS
				if (Level % 3 == 0) {
				//	output += Poison.IncrementVariants;
				//	output += Confuse.IncrementVariants;
				//


					output += Daze.IncrementVariants;
				}


			} else if (PlayerClass.Equals ("Strength")) {
				//STATS
				strength.LevelStat += 22 + (Ratio - 1);
				grit.LevelStat += 10 + (Ratio - 1);
				magick.LevelStat += 4 + (Ratio - 1);
				resistance.LevelStat += 4 + (Ratio - 1);
				speed.LevelStat += .01 + ((((double)Ratio - 1) / 100));
				dexterity.LevelStat += .01 + ((((double)Ratio - 1) / 100));
				//BARS
				Health.MeterMax += 150 * Ratio;
				Health.MeterLevel += 150 * Ratio;
				Rush.MeterMax += 15 * Ratio;
				Guard.MeterMax += 10 * Ratio;
				Stun.MeterMax += 2 * Ratio;
				//SHIFT
				Rush.MeterShift += 2 * Ratio;
				Guard.MeterShift += 0 * Ratio;
			} else if (PlayerClass.Equals ("Grit")) {
				//STATS
				strength.LevelStat += 10 + (Ratio - 1);
				grit.LevelStat += 22 + (Ratio - 1);
				magick.LevelStat += 4 + (Ratio - 1);
				resistance.LevelStat += 4 + (Ratio - 1);
				speed.LevelStat += .01 + ((((double)Ratio - 1) / 100));
				dexterity.LevelStat += .01 + ((((double)Ratio - 1) / 100));
				//BARS
				Health.MeterMax += 150 * Ratio;
				Health.MeterLevel += 150 * Ratio;
				Rush.MeterMax += 5 * Ratio;
				Guard.MeterMax += 20 * Ratio;
				Stun.MeterMax += 2 * Ratio;
				//SHIFT
				Rush.MeterShift += 0 * Ratio;
				Guard.MeterShift += 2 * Ratio;
			} else if (PlayerClass.Equals ("Physicality")) {
				//STATS
				strength.LevelStat += 16 + (Ratio - 1);
				grit.LevelStat += 16 + (Ratio - 1);
				magick.LevelStat += 4 + (Ratio - 1);
				resistance.LevelStat += 4 + (Ratio - 1);
				speed.LevelStat += .01 + ((((double)Ratio - 1) / 100));
				dexterity.LevelStat += .01 + ((((double)Ratio - 1) / 100));
				//BARS
				Health.MeterMax += 200 * Ratio;
				Health.MeterLevel += 200 * Ratio;
				Rush.MeterMax += 10 * Ratio;
				Guard.MeterMax += 10 * Ratio;
				Stun.MeterMax += 2 * Ratio;
				//SHIFT
				Rush.MeterShift += 1 * Ratio;
				Guard.MeterShift += 1 * Ratio;
			} else if (PlayerClass.Equals ("Power")) {
				//STATS
				strength.LevelStat += 4 + (Ratio - 1);
				grit.LevelStat += 4 + (Ratio - 1);
				magick.LevelStat += 22 + (Ratio - 1);
				resistance.LevelStat += 10 + (Ratio - 1);
				speed.LevelStat += .01 + ((((double)Ratio - 1) / 100));
				dexterity.LevelStat += .01 + ((((double)Ratio - 1) / 100));
				//BARS
				Health.MeterMax += 100 * Ratio;
				Health.MeterLevel += 100 * Ratio;
				Rush.MeterMax += 15 * Ratio;
				Guard.MeterMax += 15 * Ratio;
				Stun.MeterMax += 2 * Ratio;
				//SHIFT
				Rush.MeterShift += 2 * Ratio;
				Guard.MeterShift += 0 * Ratio;
			} else if (PlayerClass.Equals ("Resistance")) {
				//STATS
				strength.LevelStat += 4 + (Ratio - 1);
				grit.LevelStat += 4 + (Ratio - 1);
				magick.LevelStat += 10 + (Ratio - 1);
				resistance.LevelStat += 22 + (Ratio - 1);
				speed.LevelStat += .01 + ((((double)Ratio - 1) / 100));
				dexterity.LevelStat += .01 + ((((double)Ratio - 1) / 100));
				//BARS
				Health.MeterMax += 180 * Ratio;
				Health.MeterLevel += 180 * Ratio;
				Rush.MeterMax += 0 * Ratio;
				Guard.MeterMax += 22 * Ratio;
				Stun.MeterMax += 2 * Ratio;
				//SHIFT
				Rush.MeterShift += 0 * Ratio;
				Guard.MeterShift += 2 * Ratio;
			} else if (PlayerClass.Contains ("Magicka")) {
                if (PlayerClass.Contains("Defense"))
                {
                    //STATS
                    strength.LevelStat += 2 + (Ratio - 1);
                    grit.LevelStat += 6 + (Ratio - 1);
                    magick.LevelStat += 16 + (Ratio - 1);
                    resistance.LevelStat += 16 + (Ratio - 1);
                    speed.LevelStat += .01 + ((((double)Ratio - 1) / 100));
                    dexterity.LevelStat += .01 + ((((double)Ratio - 1) / 100));
                    //BARS
                    Health.MeterMax += 100 * Ratio;
                    Health.MeterLevel += 100 * Ratio;
                    Rush.MeterMax += 10 * Ratio;
                    Guard.MeterMax += 20 * Ratio;
                    Stun.MeterMax += 2 * Ratio;
                    //SHIFT
                    Rush.MeterShift += 1 * Ratio;
                    Guard.MeterShift += 1 * Ratio;
                }
                else if (PlayerClass.Contains("Offense"))
                {
                    //STATS
                    strength.LevelStat += 6 + (Ratio - 1);
                    grit.LevelStat += 2 + (Ratio - 1);
                    magick.LevelStat += 16 + (Ratio - 1);
                    resistance.LevelStat += 16 + (Ratio - 1);
                    speed.LevelStat += .01 + ((((double)Ratio - 1) / 100));
                    dexterity.LevelStat += .01 + ((((double)Ratio - 1) / 100));
                    //BARS
                    Health.MeterMax += 100 * Ratio;
                    Health.MeterLevel += 100 * Ratio;
                    Rush.MeterMax += 20 * Ratio;
                    Guard.MeterMax += 10 * Ratio;
                    Stun.MeterMax += 2 * Ratio;
                    //SHIFT
                    Rush.MeterShift += 1 * Ratio;
                    Guard.MeterShift += 1 * Ratio;
                }
                else
                {
                    //STATS
                    strength.LevelStat += 4 + (Ratio - 1);
                    grit.LevelStat += 4 + (Ratio - 1);
                    magick.LevelStat += 16 + (Ratio - 1);
                    resistance.LevelStat += 16 + (Ratio - 1);
                    speed.LevelStat += .01 + ((((double)Ratio - 1) / 100));
                    dexterity.LevelStat += .01 + ((((double)Ratio - 1) / 100));
                    //BARS
                    Health.MeterMax += 100 * Ratio;
                    Health.MeterLevel += 100 * Ratio;
                    Rush.MeterMax += 15 * Ratio;
                    Guard.MeterMax += 15 * Ratio;
                    Stun.MeterMax += 2 * Ratio;
                    //SHIFT
                    Rush.MeterShift += 1 * Ratio;
                    Guard.MeterShift += 1 * Ratio;
                }
			} else if (PlayerClass.Equals ("Offense")) {
				//STATS
				strength.LevelStat += 16 + (Ratio - 1);
				grit.LevelStat += 4 + (Ratio - 1);
				magick.LevelStat += 16 + (Ratio - 1);
				resistance.LevelStat += 4 + (Ratio - 1);
				speed.LevelStat += .01 + ((((double)Ratio - 1) / 100));
				dexterity.LevelStat += .01 + ((((double)Ratio - 1) / 100));
				//BARS
				Health.MeterMax += 200 * Ratio;
				Health.MeterLevel += 200 * Ratio;
				Rush.MeterMax += 20 * Ratio;
				Guard.MeterMax += 0 * Ratio;
				Stun.MeterMax += 2 * Ratio;
				//SHIFT
				Rush.MeterShift += 2 * Ratio;
				Guard.MeterShift += 0 * Ratio;
			} else if (PlayerClass.Equals ("Defense")) {
				//STATS
				strength.LevelStat += 4 + (Ratio - 1);
				grit.LevelStat += 16 + (Ratio - 1);
				magick.LevelStat += 4 + (Ratio - 1);
				resistance.LevelStat += 16 + (Ratio - 1);
				speed.LevelStat += .01 + ((((double)Ratio - 1) / 100));
				dexterity.LevelStat += .01 + ((((double)Ratio - 1) / 100));
				//BARS
				Health.MeterMax += 200 * Ratio;
				Health.MeterLevel += 200 * Ratio;
				Rush.MeterMax += 0 * Ratio;
				Guard.MeterMax += 20 * Ratio;
				Stun.MeterMax += 2 * Ratio;
				//SHIFT
				Rush.MeterShift += 0 * Ratio;
				Guard.MeterShift += 2 * Ratio;
			} else if (PlayerClass.Equals ("Agility")) {
				//STATS
				strength.LevelStat += 4 + (Ratio - 1);
				grit.LevelStat += 4 + (Ratio - 1);
				magick.LevelStat += 4 + (Ratio - 1);
				resistance.LevelStat += 4 + (Ratio - 1);
				speed.LevelStat += .05 + ((((double)Ratio - 1) / 100));
				dexterity.LevelStat += .05 + ((((double)Ratio - 1) / 100));
				//BARS
				Health.MeterMax += 200 * Ratio;
				Health.MeterLevel += 200 * Ratio;
				Rush.MeterMax += 10 * Ratio;
				Guard.MeterMax += 10 * Ratio;
				Stun.MeterMax += 2 * Ratio;
				//SHIFT
				Rush.MeterShift += 1 * Ratio;
				Guard.MeterShift += 1 * Ratio;
			} else if (PlayerClass.Equals ("Speed")) {
				//STATS
				strength.LevelStat += 4 + (Ratio - 1);
				grit.LevelStat += 4 + (Ratio - 1);
				magick.LevelStat += 4 + (Ratio - 1);
				resistance.LevelStat += 4 + (Ratio - 1);
				speed.LevelStat += .095 + ((((double)Ratio - 1) / 100));
				dexterity.LevelStat += .005 + ((((double)Ratio - 1) / 100));
				//BARS
				Health.MeterMax += 100 * Ratio;
				Health.MeterLevel += 100 * Ratio;
				Rush.MeterMax += 5 * Ratio;
				Guard.MeterMax += 25 * Ratio;
				Stun.MeterMax += 2 * Ratio;
				//SHIFT
				Rush.MeterShift += 1 * Ratio;
				Guard.MeterShift += 1 * Ratio;
			} else if (PlayerClass.Equals ("Dexterity")) {
				//STATS
				strength.LevelStat += 4 + (Ratio - 1);
				grit.LevelStat += 4 + (Ratio - 1);
				magick.LevelStat += 14 + (Ratio - 1);
				resistance.LevelStat += 4 + (Ratio - 1);
				speed.LevelStat += .005 + ((((double)Ratio - 1) / 100));
				dexterity.LevelStat += .095 + ((((double)Ratio - 1) / 100));
				//BARS
				Health.MeterMax += 100 * Ratio;
				Health.MeterLevel += 100 * Ratio;
				Rush.MeterMax += 5 * Ratio;
				Guard.MeterMax += 5 * Ratio;
				Stun.MeterMax += 10 * Ratio;
				//SHIFT
				Rush.MeterShift += 1 * Ratio;
				Guard.MeterShift += 1 * Ratio;
			} else if (PlayerClass.Equals ("Tank")) {
				//STATS
				strength.LevelStat += 30 + (Ratio - 1);
				grit.LevelStat += 30 + (Ratio - 1);
				//BARS
				Health.MeterMax += 200 * Ratio;
				Health.MeterLevel += 200 * Ratio;
				Rush.MeterMax += 50 * Ratio;
				Guard.MeterMax += 0 * Ratio;
				Stun.MeterMax += 0 * Ratio;
				//SHIFT
				Rush.MeterShift += 2 * Ratio;
				Guard.MeterShift += 0 * Ratio;
			} else if (PlayerClass.Equals ("Boss")) {
				//STATS
				strength.LevelStat += 10 + (Ratio - 1);
				grit.LevelStat += 10 + (Ratio - 1);
				magick.LevelStat += 10 + (Ratio - 1);
				resistance.LevelStat += 10 + (Ratio - 1);
				speed.LevelStat += .01 + ((((double)Ratio - 1) / 100));
				dexterity.LevelStat += .01 + ((((double)Ratio - 1) / 100));
				//BARS
				Health.MeterMax += 200 * Ratio;
				Health.MeterLevel += 200 * Ratio;
				Rush.MeterMax += 10 * Ratio;
				Guard.MeterMax += 10 * Ratio;
				Stun.MeterMax += 2 * Ratio;
				//SHIFT
				Rush.MeterShift += 1 * Ratio;
				Guard.MeterShift += 1 * Ratio;
			} else if (PlayerClass.Equals ("Meter")) {
				//STATS
				//BARS
				Health.MeterMax += 200 * Ratio;
				Health.MeterLevel += 200 * Ratio;
				Rush.MeterMax += 10 * Ratio;
				Guard.MeterMax += 10 * Ratio;
				Stun.MeterMax += 2 * Ratio;
				//SHIFT
				Rush.MeterShift += 1 * Ratio;
				Guard.MeterShift += 1 * Ratio;
			}
			return output;
		}
	}

	public Player LockedOnTarget
	{
		get { return lockedOnTarget;}
		set { lockedOnTarget = value;}
	}

	public Map CurrentMap
	{
		get {
			return playerMap;
		}
		set {
			playerMap = value;
		}
	}

	public void setCurrentMap (Map mp)
	{
		playerMap = mp;
	}

	public Location CurrentLocation
	{
		get {
			return location;
		} 
		set {
			location = value;
		}
	}

	public Map currentMap ()
	{
		return playerMap;
	}

	public Boolean Staggered
	{
		get {
			return staggered;
		}
		set {
			staggered = value;
		}
	}

	public string StaggerDirection
	{
		get {
			return staggerDirection;
		}
		set {
			staggerDirection = value;
		}
	}

	public int StaggerTime
	{
		get {
			return staggerTime;
		}
		set {
			staggerTime = value;
		}
	}

	public Boolean IsKnockedDown
	{
		get { return StateActive (Grounded)
			&& !IsCrouching
			&& !IsGuarding;}
	}

	public Boolean IsCrouching
	{
		get { return !StateActive (Airborne) && (Staggered || isCrouching);}
		set { isCrouching = value;}
	}

	public Boolean IsGuarding
	{
		get { return isGuarding;}
		set { isGuarding = value;}
	}

	public Boolean AutoGuard
	{
		get { return autoGuard;}
		set { autoGuard = value;}
	}

	public Boolean IsResting
	{
		get { return isResting;}
		set { isResting = value;}
	}

	public Boolean IsTaunting
	{
		get { return isTaunting;}
		set { isTaunting = value;}
	}

	public int TauntDecay
	{
		get { return tauntDecay;}
		set { tauntDecay = value;}
	}

	public Boolean IsDefending
	{
		get { return isGuarding || StateActive (Counter) || StateActive (Parry) || Dexterity > 4.5;}
	}

	public Boolean IsStanding
	{
		get {
			return !StateActive (Grounded)
				&& !StateActive (Airborne)
				&& !IsCrouching;
		}
	}

	public string Name
	{
		get {
			if (reverseName) {
				return Surname + " " + FirstName;
			}
			return FirstName + " " + Surname;
		}
	}

    public Boolean ReverseName
    {
        get {
            return reverseName;
        }
        set {
            reverseName = value;
        }
    }

	public Boolean IsDizzied
	{
		get { return isDizzied;}
		set { isDizzied = value;}
	}

	public Boolean WasDizzied
	{
		get { return wasDizzied;}
		set { wasDizzied = value;}
	}

	public ArrayList AllSkills
	{
		get { return allSkills;}
		set { allSkills = value;}
	}

	public ArrayList NormalSkills
	{
		get { return normalSkills;}
		set { normalSkills = value;}
	}

	public ArrayList SpecialSkills
	{
		get { return specialSkills;}
		set { specialSkills = value;}
	}

	public ArrayList VitalitySkills
	{
		get { return vitalitySkills;}
		set { vitalitySkills = value;}
	}

	public ArrayList BurstSkills
	{
		get { return burstSkills;}
		set { burstSkills = value;}
	}

	public ArrayList JumpSkills
	{
		get { return jumpSkills;}
		set { jumpSkills = value;}
	}

	public ArrayList ItemSkills
	{
		get { return itemSkills;}
		set { itemSkills = value;}
	}

	public Skill SActionSkill
	{
		get { return sActionSkill;}
		set { sActionSkill = value;}
	}

	public Skill CounterSkill
	{
		get { return counterSkill;}
		set { counterSkill = value;}
	}

    public Skill ContactSkill
    {
        get { return contactSkill; }
        set { contactSkill = value; }
    }

	public Skill CriticalSkill
	{
		get { return criticalSkill;}
		set { criticalSkill = value;}
	}

	public Skill TauntSkill
	{
		get { return tauntSkill;}
		set { tauntSkill = value;}
	}

	public Skill KOSkill
	{
		get { return koSkill;}
		set { koSkill = value;}
	}

	public State StunHit
	{
		get { return states [0];}
		set { states [0] = value;}
	}

	public double StunHitResistance
	{
		get { return StunHit.Probability + StateResistances [0].BaseValue;}
	}

	public State Grounded
	{
		get { return states [1];}
		set { states [1] = value;}
	}

	public double GroundedResistance
	{
		get { return Grounded.Probability + StateResistances [1].BaseValue;}
	}

	public State Airborne
	{
		get { return states [2];}
		set { states [2] = value;}
	}

	public double AirborneResistance
	{
		get { return Airborne.Probability + StateResistances [2].BaseValue;}
	}

	public State Counter
	{
		get { return states [3];}
		set { states [3] = value;}
	}

	public double CounterResistance
	{
		get { return Counter.Probability + StateResistances [3].BaseValue;}
	}

	public State Parry
	{
		get { return states [4];}
		set { states [4] = value;}
	}

	public double ParryResistance
	{
		get { return Parry.Probability + StateResistances [4].BaseValue;}
	}

	public State Poison
	{
		get { return states [5];}
		set { states [5] = value;}
	}

	public double PoisonResistance
	{
		get { return Poison.Probability + StateResistances [5].BaseValue;}
	}

	public State Regen
	{
		get { return states [6];}
		set { states [6] = value;}
	}

	public double RegenResistance
	{
		get { return Regen.Probability + StateResistances [6].BaseValue;}
	}

	public State Daze
	{
		get { return states [7];}
		set { states [7] = value;}
	}

	public double DazeResistance
	{
		get { return Daze.Probability + StateResistances [7].BaseValue;}
	}

	public State Confuse
	{
		get { return states [8];}
		set { states [8] = value;}
	}

	public double ConfuseResistance
	{
		get { return Confuse.Probability + StateResistances [8].BaseValue;}
	}

	public State Sadness
	{
		get { return states [9];}
		set { states [9] = value;}
	}

	public Boolean StateActive (State st)
	{
		if (st.IsActive) {
			return true;
		}
		for (int i = 0; i < Inventory.Count; i ++) {
			Item it = (Item)Inventory [i];
			for (int j = 0; it.NumUses > 0 && j < it.StateAugments.Length; j ++) {
				if (st.Index == j
				   && it.StateAugments [j] != 0) {
					return true;
				}
			}
		}
		return false;
	}

	public int Potency (State st)
	{
		int amt = st.Potency;

		if (st.Equals (Airborne)) {
			if (StateActive (Grounded)) {
				return 0;
			}
		}

		for (int i = 0; i < Inventory.Count; i ++) {
			Item it = (Item)Inventory [i];
			for (int j = 0; it.NumUses > 0 && j < it.StateAugments.Length; j ++) {
				if (st.Index == j) {
					amt += it.StateAugments [j];
				}
			}
		}
		return amt;
	}

	public double SadnessResistance
	{
		get { return Sadness.Probability + StateResistances [9].BaseValue;}
	}

	public State Fury
	{
		get { return states [10];}
		set { states [10] = value;}

	}

	public double FuryResistance
	{
		get { return Fury.Probability + StateResistances [10].BaseValue;}
	}

	public State Sleep
	{
		get { return states [11];}
		set { states [11] = value;}
	}

	public double SleepResistance
	{
		get { return Sleep.Probability + StateResistances [11].BaseValue;}
	}

	public State Adle
	{
		get { return states [12];}
		set { states [12] = value;}
	}

	public double AdleResistance
	{
		get { return Adle.Probability + StateResistances [12].BaseValue;}
	}

	public State Freeze
	{
		get { return states [13];}
		set { states [13] = value;}
	}

	public double FreezeResistance
	{
		get { return Freeze.Probability + StateResistances [13].BaseValue;}
	}

	public State Immune
	{
		get { return states [14];}
		set { states [14] = value;}
	}

	public double ImmuneResistance
	{
		get { return Immune.Probability + StateResistances [14].BaseValue;}
	}

	public State Invulnerable
	{
		get { return states [15];}
		set { states [15] = value;}
	}

	public double InvulnerableResistance
	{
		get { return Invulnerable.Probability + StateResistances [15].BaseValue;}
	}

	public State Burn
	{
		get { return states [16];}
		set { states [16] = value;}
	}

	public double BurnResistance
	{
		get { return Burn.Probability + StateResistances [16].BaseValue;}
	}

	public State Learn
	{
		get { return states [17];}
		set { states [17] = value;}
	}

	public double LearnResistance
	{
		get { return Learn.Probability + StateResistances [17].BaseValue;}
	}

	public State Invisible
	{
		get { return states [18];}
		set { states [18] = value;}
	}

	public double InvisibleResistance
	{
		get { return Invisible.Probability + StateResistances [18].BaseValue;}
	}

	public State Blind
	{
		get { return states [19];}
		set { states [19] = value;}
	}

	public double BlindResistance
	{
		get { return Blind.Probability + StateResistances [19].BaseValue;}
	}

	public double LeechResistance
	{
		get { return StateResistances [20].BaseValue;}
	}

	public ArrayList NoAmmo
	{
		get { return noAmmo;}
		set { noAmmo = value;}
	}

	public State [] States
	{
		get { return states;}
		set { states = value;}
	}

	public Boolean IsVulnerable
	{
		get {
			return ResidualFatigue > 1 || Fatigue > 1 || StateActive (Daze) || StateActive (Confuse) || IsResting || IsTaunting;
		}
	}

	public Meter Health
	{
		get { return health;}
	}

	public Meter Rush
	{
		get { return rush;}
	}

	public Meter Guard
	{
		get { return guard;}
	}

	public Meter Vitality
	{
		get { return vitality;}
	}

	public Meter Stun
	{
		get { return stun;}
	}


	public int Index
	{
		get { return index;}
		set { index = value;}
	}

	public string FirstName
	{
		get { return firstName;}
		set { firstName = value;}
	}

	public string Surname
	{
		get { return surname;}
		set { surname = value;}
	}

	public string SearchName
	{
		get { return searchName;}
		set { searchName = value;}
	}

	public string Sex
	{
		get { return sex;}
		set { sex = value;}
	}

	public string SexualPreference
	{
		get { return sexualPreference;}
		set { sexualPreference = value;}
	}

	public string Species
	{
		get { return species;}
		set { species = value;}
	}

	public string Nationality
	{
		get { return nationality;}
		set { nationality = value;}
	}

	public int Age
	{
		get { return age;}
		set { age = value;}
	}

	public double Height
	{
		get
		{
			if (!IsCrouching) {
				return height + Potency (Airborne);
			} else {
				return (height / 2) + Potency (Airborne);
			}
		}
		set { height = value;}
	}

	public double Weight
	{
		get { return weight;}
		set { weight = value;}
	}

	public string AssimilatedClass
	{
		get { return assimilatedClass;}
		set { assimilatedClass = value;}
	}

	public int AssimilatedTime
	{
		get { return assimilatedTime; }
		set { assimilatedTime = value; }
	}


	public string PlayerClass
	{
		get { if (AssimilatedClass != "") return AssimilatedClass; return playerClass;}
		set { playerClass = value;}
	}

	public string Style
	{
		get { return style;}
		set { style = value;}
	}

	public int Ratio
	{
		get { return ratio;}
		set { ratio = value;}
	}

	public string HandDexterity
	{
		get { return handDexterity;}
		set { handDexterity = value;}
	}

	public string statAndVariation (DynamicInt stat, int weaponVars)
	{
		string output = "";
		output += string.Format ("{0}: {1}", stat.Abbreviation, stat.RealValue + BonusStatInt);
		if ((stat.RealValue + weaponVars) > stat.BaseValue) {
			output += string.Format (" +{0}", stat.RealChange + weaponVars);
		} else if ((stat.RealValue + weaponVars) < stat.BaseValue) {
			output += string.Format (" {0}", stat.RealChange + weaponVars);
		}
		return output;
	}

	public string statAndVariation (DynamicDouble stat, double weaponVars)
	{
		string output = "";
		output += string.Format ("{0}: {1}", stat.Abbreviation, (int)((stat.RealValue + BonusStatDouble) * 100));
		if ((stat.RealValue + weaponVars) > stat.BaseValue) {
			output += string.Format (" +{0}", (int)((weaponVars) * 100));
		} else if ((stat.RealValue + weaponVars) < stat.BaseValue) {
			output += string.Format (" {0}", (int)((weaponVars) * 100));
		}
		return output;
	}

	public int BonusStatInt
	{
		get {
			if (Vitality != null && Vitality.MeterLevel != null && Vitality.MeterMax != null) {
				if (Vitality.MeterLevel == Vitality.MeterMax) {
					if (PlayStyle.Equals ("C")) {
						return 20;
					} else {
						return 10;
					}
				}
			}
			return 0;
		}
	}

	public double BonusStatDouble
	{
		get {
			if (Vitality != null && Vitality.MeterLevel != null && Vitality.MeterMax != null) {
				if (Vitality.MeterLevel == Vitality.MeterMax) {
					if (PlayStyle.Equals ("C")) {
						return .2;
					} else {
						return .1;
					}
				}
			}
			return 0.0;
		}
	}

	public int Strength
	{
		get { return strength.RealValue + ItemStrength + BonusStatInt;}
		set { strength.BaseValue = value;}
	}

	public int ItemStrength
	{
		get {
			int output = 0;
			for (int i = 0; i < Inventory.Count; i++) {
				if (((Item)Inventory [i]).NumUses > 0) {
					output += (int)((Item)Inventory [i]).StatAugments [0];
				}
			}
			return output;
		}
	}

	public int Grit
	{
		get { return grit.RealValue + ItemGrit + BonusStatInt;}
		set { grit.BaseValue = value;}
	}

	public int ItemGrit
	{
		get {
			int output = 0;
			for (int i = 0; i < Inventory.Count; i++) {
				if (((Item)Inventory [i]).NumUses > 0) {
					output += (int)((Item)Inventory [i]).StatAugments [1];
				}
			}
			return output;

		}
	}

	public int Magick
	{
		get { return magick.RealValue + ItemMagick + BonusStatInt;}
		set { magick.BaseValue = value;}
	}

	public int ItemMagick
	{
		get {
			int output = 0;
			for (int i = 0; i < Inventory.Count; i++) {
				if (((Item)Inventory [i]).NumUses > 0) {
					output += (int)((Item)Inventory [i]).StatAugments [2];
				}
			}
			return output;
		}
	}

	public int Resistance
	{
		get { return resistance.RealValue + ItemResistance + BonusStatInt;}
		set { resistance.BaseValue = value;}
	}

	public int ItemResistance
	{
		get {
			int output = 0;
			for (int i = 0; i < Inventory.Count; i++) {
				if (((Item)Inventory [i]).NumUses > 0) {
					output += (int)((Item)Inventory [i]).StatAugments [3];
				}
			}
			return output;
		}
	}

	public double Dexterity
	{
		get { 
			if (StateActive (Blind)) {
				return -1 + ItemDexterity + BonusStatDouble;
			}

			return dexterity.RealValue + ItemDexterity + BonusStatDouble + PositionDexterity;}
		set { dexterity.BaseValue = value;}
	}

	public double ItemDexterity
	{
		get {
			int output = 0;
			for (int i = 0; i < Inventory.Count; i++) {
				if (((Item)Inventory [i]).NumUses > 0) {
					output += (int)((Item)Inventory [i]).StatAugments [4];
				}
			}
			return output;
		}
	}

	public double PositionDexterity
	{
		get {
			if (IsCrouching && !StateActive (Confuse)) {
				return 0.1;
			}
			return 0;
		}
	}

	public int PositionMovement
	{
		get {
			if (isCrouching) {
				return -1;
			}
			return 0;
		}
	}

	public double Speed
	{
		get { return speed.RealValue + ItemSpeed + BonusStatDouble;}
		set { speed.BaseValue = value;}
	}

	public double ItemSpeed
	{
		get {
			double output = 0;
			for (int i = 0; i < Inventory.Count; i++) {
				if (((Item)Inventory [i]).NumUses > 0) {
					output += ((Item)Inventory [i]).StatAugments [5];
				}
			}
			return output;
		}
	}

	public float ActiveSpeed {
		get {
			float tm = (float)Speed;

			if (CanAct) {
				tm += 0.1f;
			}
			if (MovesRemaining != 0) {
				tm += (0.01f * MovesRemaining);
			}
			if (StateActive (Confuse)) {
				tm -= 0.01f;
			}
			if (StateActive (Freeze)) {
				tm -= 0.01f;
			}
			if (IsGuarding) {
				tm -= 0.02f;
			}
			if (IsCrouching) {
				tm -= 0.02f;
			}
			tm -= (Potency (Airborne) * 0.01f) + (Potency (Grounded) * 0.01f);
			return tm;
		}
	}

	public void setBonusTime ()
	{
		if (!BonusRoundActivated) {
			bonusTime = ActiveSpeed;
		}
	}

	public double Proration
	{
		get { return proration.RealValue + ItemProration;}
		set { proration.BaseValue = value;}
	}

	public double ItemProration
	{
		get {
			double output = 0;
			for (int i = 0; i < Inventory.Count; i++) {
				if (((Item)Inventory [i]).NumUses > 0) {
					output += ((Item)Inventory [i]).StatAugments [6];
				}
			}
			return output;
		}
	}

	public void printAllSkills ()
	{
		for (int i = 0; i < AllSkills.Count; i++) {
			((Skill)AllSkills [i]).printSkill (0);
		}
	}

	public char charIndex (int i) {
		return ((char)('a' + i));
	}

	public string indexOutput (char c)
	{
		return c + ") ";
	}

	public void printAllSkillsOnOnePage ()
	{
		StreamWriter writer = new StreamWriter (@"Assets/Resources/Players/" + SearchName + "/Info/" + PlayStyleDisplay + "Overview.txt");

		writer.WriteLine (FirstName.ToUpper () + " " + PlayStyleDisplay);
		writer.WriteLine (Health.ToString ());
		writer.WriteLine (Rush.ToString ());
		writer.WriteLine (Guard.ToString ());
		writer.WriteLine (Stun.ToString ());
		writer.WriteLine (" ");
		writer.WriteLine ("Strength: " + Strength);
		writer.WriteLine ("Grit: " + Grit);
		writer.WriteLine ("Magick: " + Magick);
		writer.WriteLine ("Resistance: " + Resistance);
		writer.WriteLine ("Dexterity: " + Dexterity);
		writer.WriteLine ("Speed: " + Speed);
		writer.WriteLine ("Proration: " + Proration);
		writer.WriteLine ("Movement: " + Movement);
		writer.WriteLine ("Teamwork: " + Teamwork);
		writer.WriteLine ("Luck: " + Luck);
		writer.WriteLine ("Jump: " + JumpHeight);
		writer.WriteLine (" ");


		writer.WriteLine ("1) Trait");
		writer.Write ("" + '\t' + SActionSkill.GeneralDescription (0));
		writer.WriteLine ("");

		writer.WriteLine ("2) Jump");
		for (int i = 0; i < JumpSkills.Count; i++) {
			writer.Write ("" + '\t' + indexOutput (charIndex (i)) + ((Skill)JumpSkills [i]).GeneralDescription (0));
		}
		writer.WriteLine ("");

		writer.WriteLine ("3) Normal");
		for (int i = 0; i < NormalSkills.Count; i++) {
			writer.Write ("" + '\t' + indexOutput (charIndex (i)) + ((Skill)NormalSkills [i]).GeneralDescription (0));
		}
		writer.WriteLine ("");

		writer.WriteLine ("4) Special");
		for (int i = 0; i < SpecialSkills.Count; i++) {
			writer.Write ("" + '\t' + indexOutput (charIndex (i)) + ((Skill)SpecialSkills [i]).GeneralDescription (0));
		}
		writer.WriteLine ("");

		writer.WriteLine ("5) Vitality");
		for (int i = 0; i < VitalitySkills.Count; i++) {
			writer.Write ("" + '\t' + indexOutput (charIndex (i)) + ((Skill)VitalitySkills [i]).GeneralDescription (0));
		}
		writer.WriteLine ("");

		writer.WriteLine ("6) Burst");
		for (int i = 0; i < BurstSkills.Count; i++) {
			writer.Write ("" + '\t' + indexOutput (charIndex (i)) + ((Skill)BurstSkills [i]).GeneralDescription (0));
		}
		writer.WriteLine ("");

		writer.WriteLine ("7) Item");
		for (int i = 0; i < ItemSkills.Count; i++) {
			writer.Write ("" + '\t' + indexOutput (charIndex (i)) + ((Skill)ItemSkills [i]).GeneralDescription (0));
		}
		writer.WriteLine ("");

		if (CriticalSkill != null) {
			writer.WriteLine ("8) Limit Break");
			writer.Write ("" + '\t' + CriticalSkill.GeneralDescription (0));
		}

		writer.Close ();

		StreamWriter moveWriter = new StreamWriter (@"Assets/Resources/Players/" + SearchName + "/Info/" + PlayStyleDisplay + "Movelist.txt");

		moveWriter.WriteLine (Name + " SKILLS [" + AllSkills.Count + "]");

		for (int i = 0; i < AllSkills.Count; i++) {
			moveWriter.WriteLine ((i + 1) + ") " + ((Skill)AllSkills [i]).output (0));
			moveWriter.WriteLine (" " + '\n' + "---" + '\n');
		}
		moveWriter.Close ();
	}

	public double CurrentProration
	{
		get { return currentProration;}
		set { currentProration = value;}
	}

	public double CurrentHitRate
	{
		get { return currentHitRate;}
		set { currentHitRate = value;}
	}

	public int Movement
	{
		get { return movement.RealValue + ItemMovement + PositionMovement;}
		set { movement.BaseValue = value;}
	}


	public int ItemMovement
	{
		get {
			int output = 0;
			for (int i = 0; i < Inventory.Count; i++) {
				if (((Item)Inventory [i]).NumUses > 0) {
					output += (int)((Item)Inventory [i]).StatAugments [7];
				}
			}
			return output;
		}
	}

	public int Teamwork
	{
		get { return teamwork.RealValue + ItemTeamwork + BonusStatInt;}
		set { teamwork.BaseValue = value;}
	}

	public int ItemTeamwork
	{
		get {
			int output = 0;
			for (int i = 0; i < Inventory.Count; i++) {
				if (((Item)Inventory [i]).NumUses > 0) {
					output += (int)((Item)Inventory [i]).StatAugments [8];
				}
			}
			return output;
		}
	}

	public int Luck
	{
		get { return luck.RealValue + ItemLuck + BonusStatInt;}
		set { luck.BaseValue = value;}
	}

	public int ItemLuck
	{
		get {
			int output = 0;
			for (int i = 0; i < Inventory.Count; i++) {
				if (((Item)Inventory [i]).NumUses > 0) {
					output += (int)((Item)Inventory [i]).StatAugments [9];
				}
			}
			return output;
		}
	}

	public int ActionPoints
	{
		get { return actionPoints.RealValue;}
		set { actionPoints.BaseValue = value;}
	}

	public int MovesRemaining
	{
		get { return movesRemaining;}
		set
		{
			movesRemaining = value;
		}
	}

	public int [] ElementalOffense
	{
		get { return elementalOffense;}
	}

	public int [] ElementalDefense
	{
		get { return elementalDefense;}
	}

	public string [] Conditions
	{
		get { return conditions;}
		set { conditions = value;}
	}

	public Voice Vocals
	{
		get { return voice;}
		set { voice = value;}
	}

	public string speakLine (string line)
	{
		if (line.Length > 0) {
			return firstName + ": " + line + '\n';
		}
		return "";
	}

	public Location currentLocation ()
	{
		return location;
	}

    public int MapSize
    {
        get {
            return mapSize;
        }
        set {
            mapSize = value;
        }
    }

	public void changeLocation (Location newLocation)
	{
		location = newLocation;
		if (mapSize > 1) {
			pointSpace = new ArrayList ();
			Location lc;
			int r = currentLocation ().Row - (mapSize - 1), c = currentLocation ().Column - (mapSize - 1);
			for (int i = 0; i < (2 * mapSize) - 1; i++) {
				for (int j = 0; j < (2 * mapSize) - 1; j++) {
					lc = new Location (r, c);
					if (currentMap ().isValid (lc)) {
						pointSpace.Add (lc);
					}
					c++;
				}
				r++;
			}
		}
		//if (currentMap () != null) {
		//setLocations ();
		//}
	}

	public int currentHeight ()
	{
		return currentMap ().heightOf (currentLocation ());
	}

	public bool sentient ()
	{	return true;}

	public int CompareTo (object obj)
	{
		Player rhs = (Player)obj;



		if (ActiveSpeed.CompareTo (rhs.ActiveSpeed) != 0) {
			return rhs.ActiveSpeed.CompareTo (ActiveSpeed);
		}

		if (Speed.CompareTo (rhs.Speed) != 0) {
			return rhs.Speed.CompareTo (Speed);
		}

		if (PowerLevel.CompareTo (rhs.PowerLevel) != 0) {
			return rhs.PowerLevel.CompareTo (PowerLevel);
		}

		if (Dexterity.CompareTo (rhs.Dexterity) != 0) {
			return rhs.Dexterity.CompareTo (Dexterity);
		}
		if (Movement.CompareTo (rhs.Movement) != 0) {
			return rhs.Movement.CompareTo (Movement);
		}
		if (Strength.CompareTo (rhs.Strength) != 0) {
			return rhs.Strength.CompareTo (Strength);
		}
		if (Grit.CompareTo (rhs.Grit) != 0) {
			return rhs.Grit.CompareTo (Grit);
		}
		if (Magick.CompareTo (rhs.Resistance) != 0) {
			return rhs.Magick.CompareTo (Strength);
		}
		if (Luck.CompareTo (rhs.Luck) != 0) {
			return rhs.Luck.CompareTo (Luck);
		}
		if (rhs.Surname.CompareTo (Surname) != 0) {
			return rhs.Surname.CompareTo (Surname);
		}
		if (r.Next (2) == 1) {
			return -1;
		}
		return 1;
	}

	public string PlayerUIInfo
	{
		get {
			string output = "";
			output += string.Format ("{0} (Lv. {1}, {2})",
				Name, Level, CurrentLocation.ToString ());
			return output;
		}
	}

	public string StyleUIInfo
	{
		get {
			return string.Format ("{0} LV. {1}", PlayStyleDisplay, Level).ToUpper ();
		}
	}

	public string addInfoLine (string line, int limiter)
	{
		string output = "";
		lengthCheck++;

		output += line;

		if (lengthCheck > limiter) {
			output += "" + '\n';
			lengthCheck = 0;
		} else {
			output += "   ";
		}
		return output;
	}

	public string InfoText
	{
		get {
			string info = "";

			info += CharacterStats (3);
			int lineLimit = 2;

			lengthCheck = 0;


			string checkedInfo = "---(Status)" + '\n';
			int statusCount = 0;
			lineLimit = 2;

			if (StunHitResistance != 0) {
				checkedInfo += addInfoLine (string.Format ("{0} {1}%", StunHit.Abbreviation, ((int)(100 * (StunHitResistance)))), lineLimit);
				statusCount++;
			}
			if (StateActive (Counter) || CounterResistance != 0) {
				checkedInfo += addInfoLine ( string.Format ("{0} {1} {2} ({3}%)", Counter.Abbreviation, Potency (Counter), Counter.NumTurns, ((int)(100 * (CounterResistance)))), lineLimit);
				statusCount++;
			}
			if (StateActive (Parry) || ParryResistance != 0) {
				checkedInfo += addInfoLine (string.Format ("{0} {1} {2} ({3}%)", Parry.Abbreviation, Potency (Parry), Parry.NumTurns, ((int)(100 * (ParryResistance)))), lineLimit);
				statusCount++;
			}
			if (StateActive (Poison) || PoisonResistance != 0) {
				checkedInfo += addInfoLine (string.Format ("{0} {1} {2} ({3}%)", Poison.Abbreviation, Potency (Poison), Poison.NumTurns, ((int)(100 * (PoisonResistance)))), lineLimit);
				statusCount++;
			}
			if (StateActive (Regen) || RegenResistance != 0) {
				checkedInfo += addInfoLine (string.Format ("{0} {1} {2} ({3}%)", Regen.Abbreviation, Potency (Regen), Regen.NumTurns, ((int)(100 * (RegenResistance)))), lineLimit);
				statusCount++;
			}
			if (StateActive (Daze) || DazeResistance != 0) {
				checkedInfo += addInfoLine (string.Format ("{0} {1} {2} ({3}%)", Daze.Abbreviation, Potency (Daze), Daze.NumTurns, ((int)(100 * (DazeResistance)))), lineLimit);
				statusCount++;
			}
			if (StateActive (Confuse) || ConfuseResistance != 0) {
				checkedInfo += addInfoLine (string.Format ("{0} {1} {2} ({3}%)", Confuse.Abbreviation, Potency (Confuse), Confuse.NumTurns, ((int)(100 * (ConfuseResistance)))), lineLimit);
				statusCount++;
			}
			if (StateActive (Sadness) || SadnessResistance != 0) {
				checkedInfo += addInfoLine (string.Format ("{0} {1} {2} ({3}%)", Sadness.Abbreviation, Potency (Sadness), Sadness.NumTurns, ((int)(100 * (SadnessResistance)))), lineLimit);
				statusCount++;
			}
			if (StateActive (Sleep) || SleepResistance != 0) {
				checkedInfo += addInfoLine (string.Format ("{0} {1} {2} ({3}%)", Sleep.Abbreviation, Potency (Sleep), Sleep.NumTurns, ((int)(100 * (SleepResistance)))), lineLimit);
				statusCount++;
			}
			if (StateActive (Adle) || AdleResistance != 0) {
				checkedInfo += addInfoLine (string.Format ("{0} {1} {2} ({3}%)", Adle.Abbreviation, Potency (Adle), Adle.NumTurns, ((int)(100 * (AdleResistance)))), lineLimit);
				statusCount++;
			}
			if (StateActive (Freeze) || FreezeResistance != 0) {
				checkedInfo += addInfoLine (string.Format ("{0} {1} {2} ({3}%)", Freeze.Abbreviation, Potency (Freeze), Freeze.NumTurns, ((int)(100 * (FreezeResistance)))), lineLimit);
				statusCount++;
			}
			if (StateActive (Immune) || ImmuneResistance != 0) {
				checkedInfo += addInfoLine (string.Format ("{0} {1} {2} ({3}%)", Immune.Abbreviation, Potency (Immune), Immune.NumTurns, ((int)(100 * (ImmuneResistance)))), lineLimit);
				statusCount++;
			}
			if (StateActive (Invulnerable) || InvulnerableResistance != 0) {
				checkedInfo += addInfoLine (string.Format ("{0} {1} {2} ({3}%)", Invulnerable.Abbreviation, Potency (Invulnerable), Invulnerable.NumTurns, ((int)(100 * (InvulnerableResistance)))), lineLimit);
				statusCount++;
			}
			if (StateActive (Burn) || BurnResistance != 0) {
				checkedInfo += addInfoLine (string.Format ("{0} {1} {2} ({3}%)", Burn.Abbreviation, Potency (Burn), Burn.NumTurns, ((int)(100 * (BurnResistance)))), lineLimit);
				statusCount++;
			}
			if (StateActive (Learn) || LearnResistance != 0) {
				checkedInfo += addInfoLine (string.Format ("{0} {1} {2} ({3}%)", Learn.Abbreviation, Potency (Learn), Learn.NumTurns, ((int)(100 * (LearnResistance)))), lineLimit);
				statusCount++;
			}
			if (LeechResistance != 0) {
				checkedInfo += addInfoLine (string.Format ("{0}: ({1}%)", "Leech", LeechResistance), lineLimit);
				statusCount++;
			}
			if (statusCount != 0) {
				info += string.Format (checkedInfo + " " + '\n' + "");
			}



			checkedInfo = "";
			lengthCheck = 0;

			checkedInfo += "---(Status Resistance)" + '\n';
			int resistanceCount = 0;
			for (int i = 0; i < StateResistances.Length; i++) {
				if (StateResistances [i].Holder > 0) {
					resistanceCount++;
					checkedInfo += addInfoLine (string.Format ("{0} {1} {2}",
						StateResistances [i].Abbreviation, (int)(100 * StateResistances [i].BaseValue), StateResistances [i].Holder), lineLimit);
				}
			}
			if (resistanceCount != 0) {
				info += string.Format (checkedInfo + " " + '\n');
			}
			checkedInfo = "";
			lengthCheck = 0;


			string elementInfo = "";
			elementInfo += "---(Elements)" + '\n';
			int elemCount = 0;
			if (ElementalOffense [0] != 0 || ElementalDefense [0] != 0) {
				elementInfo += addInfoLine (string.Format ("FIR: {0}, {1}", ElementalOffense [0], ElementalDefense [0]), lineLimit);
				elemCount++;
			}
			if (ElementalOffense [1] != 0 || ElementalDefense [1] != 0) {
				elementInfo += addInfoLine (string.Format ("ICE: {0}, {1}", ElementalOffense [1], ElementalDefense [1]), lineLimit);
				elemCount++;
			}
			if (ElementalOffense [2] != 0 || ElementalDefense [2] != 0) {
				elementInfo += addInfoLine (string.Format ("ELE: {0}, {1}", ElementalOffense [2], ElementalDefense [2]), lineLimit);
				elemCount++;
			}
			if (ElementalOffense [3] != 0 || ElementalDefense [3] != 0) {
				elementInfo += addInfoLine (string.Format ("WND: {0}, {1}", ElementalOffense [3], ElementalDefense [3]), lineLimit);
				elemCount++;
			}
			if (ElementalOffense [4] != 0 || ElementalDefense [4] != 0) {
				elementInfo += addInfoLine (string.Format ("WTR: {0}, {1}", ElementalOffense [4], ElementalDefense [4]), lineLimit);
				elemCount++;
			}
			if (ElementalOffense [5] != 0 || ElementalDefense [5] != 0) {
				elementInfo += addInfoLine (string.Format ("EAR: {0}, {1}", ElementalOffense [5], ElementalDefense [5]), lineLimit);
				elemCount++;
			}
			if (ElementalOffense [6] != 0 || ElementalDefense [6] != 0) {
				elementInfo += addInfoLine (string.Format ("MET: {0}, {1}", ElementalOffense [6], ElementalDefense [6]), lineLimit);
				elemCount++;
			}
			if (ElementalOffense [7] != 0 || ElementalDefense [7] != 0) {
				elementInfo += addInfoLine (string.Format ("DRK: {0}, {1}", ElementalOffense [7], ElementalDefense [7]), lineLimit);
				elemCount++;
			}
			if (ElementalOffense [8] != 0 || ElementalDefense [8] != 0) {
				elementInfo += addInfoLine (string.Format ("LGT: {0}, {1}", ElementalOffense [8], ElementalDefense [8]), lineLimit);
				elemCount++;
			}
			if (ElementalOffense [9] != 0 || ElementalDefense [9] != 0) {
				elementInfo += addInfoLine (string.Format ("BRK: {0}, {1}", ElementalOffense [9], ElementalDefense [9]), lineLimit);
				elemCount++;
			}
			if (elemCount != 0) {
				info += string.Format (elementInfo + " " + '\n');
			}

			//info += "---(Items)" + '\n';
			//info += ItemInfo;

			return info;
		}
	}

	public string InventoryDisplay
	{
		get {
			string info = "---(Items)" + '\n';
			info += ItemInfo;

			return info;
		}
	}

	public Boolean Leech
	{
		get {
			return leech;
		}
		set {
			leech = value;
		}
	}

	public Boolean CounterState
	{
		get {
			return counterState;
		}
		set {
			counterState = value;
		}
	}

	public StreamWriter TurnOutput
	{
		get {
			return turnOutput;
		}
		set {
			turnOutput = value;
		}
	}

    public Boolean SkillExecuted
    {
        get {
            return skillExecuted;
        }
        set {
            skillExecuted = value;
        }
    }

	public int TauntBonus (Team otherTeam)
	{
		return currentLocation ().span ( closestLiveUnit (otherTeam).currentLocation ());
	}

	public string PlayerStats
	{
		get {
			string output = "";
			int numMods = 0;
			if (strength.BaseValue != strength.RealValue) {
				output += string.Format ("STR ({0})   ", strength.RealChangeDisplay);
				numMods++;
			}

			if (grit.BaseValue != grit.RealValue) {

				if (numMods >= 2) {
					output += string.Format ("" + '\n' + '\t');
					numMods = 0;
				}

				output += string.Format ("GRT ({0})   ", grit.RealChangeDisplay);
				numMods++;
			}

			if (magick.BaseValue != magick.RealValue) {

				if (numMods >= 2) {
					output += string.Format ("" + '\n' + '\t');
					numMods = 0;
				}

				output += string.Format ("MAG ({0})   ", magick.RealChangeDisplay);
				numMods++;
			}

			if (resistance.BaseValue != resistance.RealValue) {
			
				if (numMods >= 2) {
					output += string.Format ("" + '\n' + '\t');
					numMods = 0;
				}

				output += string.Format ("RES ({0})   ", resistance.RealChangeDisplay);
				numMods++;
			}

			if (speed.BaseValue != speed.RealValue) {
			
				if (numMods >= 2) {
					output += string.Format ("" + '\n' + '\t');
					numMods = 0;
				}

				output += string.Format ("SPD ({0})   ", speed.RealChangeDisplay);
				numMods++;
			}

			if (dexterity.BaseValue != dexterity.RealValue) {
			
				if (numMods >= 2) {
					output += string.Format ("" + '\n' + '\t');
					numMods = 0;
				}

				output += string.Format ("DEX ({0})   ", dexterity.RealChangeDisplay);
				numMods++;
			}

			if (proration.BaseValue != proration.RealValue) {
			
				if (numMods >= 2) {
					output += string.Format ("" + '\n' + '\t');
					numMods = 0;
				}

				output += string.Format ("PRO ({0})   ", proration.RealChangeDisplay);
				numMods++;
			}

			if (movement.BaseValue != movement.RealValue) {
			
				if (numMods >= 2) {
					output += string.Format ("" + '\n' + '\t');
					numMods = 0;
				}

				output += string.Format ("MOV ({0})   ", movement.RealChangeDisplay);
				numMods++;
			}

			if (teamwork.BaseValue != teamwork.RealValue) {
			
				if (numMods >= 2) {
					output += string.Format ("" + '\n' + '\t');
					numMods = 0;
				}

				output += string.Format ("TWK ({0})   ", teamwork.RealChangeDisplay);
				numMods++;
			}

			if (luck.BaseValue != luck.RealValue) {
			
				if (numMods >= 2) {
					output += string.Format ("" + '\n' + '\t');
					numMods = 0;
				}

				output += string.Format ("LCK ({0})   ", luck.RealChangeDisplay);
				numMods++;
			}

			if (actionPoints.BaseValue != actionPoints.RealValue) {

				if (numMods >= 2) {
					output += string.Format ("" + '\n' + '\t');
					numMods = 0;
				}

				numMods++;
			}

			return output;
		}
	}

	public string PlayerStates
	{
		get {
			string output = "";

			int numMods = 0;

			if (TimeRemaining > 0) {
				output += string.Format ("ACTIVE {0}  ", TimeRemaining);
				numMods++;
			}
			if (Cooldown > 0) {
				output += string.Format ("COOLDOWN ({0})   ", Cooldown);
				numMods++;
			}
			if (CurrentProration != 1) {
				output += string.Format ("DECAY ({0}%)  ", (int)(CurrentProration * 100));
				numMods++;
			}
			if (GuardStun > 0) {
				output += string.Format ("GSTUN ({0})  ", GuardStun);
				numMods++;
			} if (Fatigue > 0) {
				output += string.Format ("FATIGUE ({0})  ", Fatigue);
				numMods++;
			}

			for (int i = 2; i < states.Length; i++) {

				if (StateActive (states [i])) {
					if (numMods >= 2) {
						output += string.Format ("" + '\n' + '\t');
						numMods = 0;
					}
					if (states [i].NumTurns > 0) {
							output += string.Format ("{0} [{1}, {2}]  ",
						                             states [i].Abbreviation, Potency (states [i]), states [i].NumTurns);
					} else {
						if (!states [i].Name.Equals ("Airborne") && !states [i].Name.Equals ("Grounded")) {
						
							output += string.Format ("{0}  ",
							                         states [i].Abbreviation, Potency (states [i]), states [i].NumTurns);
						} else {
							output += string.Format ("{0} [{1}]  ",
							                         states [i].Abbreviation, Potency (states [i]), Potency (states [i]),
							                         states [i].NumTurns);
						}
					}
					numMods++;
				}
			}

			State st;

			for (int i = 0; i < TimedConditions.Count; i++) {
				st = (State)TimedConditions [i];
				output += string.Format ("{0} ({1}, {2})   ", st.Name, Potency (st), st.NumTurns);
				numMods++;
					
				if (i + 1 < TimedConditions.Count && numMods >= 2) {
					output += string.Format ("" + '\n' + '\t');
					numMods = 0;
				}
			}
			return output;
		}
	}

	public int PowerLevel
	{
		get {
			double pow = (double)(Strength + Grit + Magick + Resistance + (Speed * 100) + (Dexterity * 100) + (Proration * 100) + Movement + Luck + Teamwork) + (Level * 100);
			pow += (((double)Health.MeterLevel) / 100);
			pow += (((double)Rush.MeterMax) / 100) + (((double)Guard.MeterMax) / 100) - ((double)Stun.MeterLevel);

			if (setCostsAndConditionsMet (VitalitySkills) || setCostsAndConditionsMet (BurstSkills)) {
				pow += ((double)Vitality.MeterLevel) / 10;
			}
			return Experience + (Level * 100) + ((int)((pow * 10)) * Ratio);
		}
	}

	public override bool Equals (object obj)
	{
		if (obj != null && obj.GetType ().Name.Equals ("Player")) {
			Player rhs = (Player)obj;
			return SearchName.Equals (rhs.SearchName) && Index == rhs.Index;
		}
		return false;
	}

	public override int GetHashCode ()
	{
		return base.GetHashCode ();
	}

	public ArrayList Emotions
	{
		get {
			return emotions;
		}
		set {
			emotions = value;
		}
	}

	public string CharacterStats (int i)
	{
		string info = "";
		int ind = 0;

		lengthCheck = 0;

		info += addInfoLine (string.Format (statAndVariation (strength, ItemStrength)), i);

		info += addInfoLine (string.Format (statAndVariation (grit, ItemGrit)), i);

		info += addInfoLine (string.Format (statAndVariation (magick, ItemMagick)), i);

		info += addInfoLine (string.Format (statAndVariation (resistance, ItemResistance)), i);

		info += addInfoLine (string.Format (statAndVariation (speed, ItemSpeed)), i);

		info += addInfoLine (string.Format (statAndVariation (dexterity, ItemDexterity)), i);

		info += addInfoLine (string.Format (statAndVariation (proration, ItemProration)), i);

		info += addInfoLine (string.Format (statAndVariation (movement, ItemMovement)), i);

		info += addInfoLine (string.Format (statAndVariation (teamwork, ItemTeamwork)), i);

		info += addInfoLine (string.Format (statAndVariation (luck, ItemLuck)), i);

		info += string.Format ("Power Level: {0}", PowerLevel);

		if (PlayStyle.Equals ("B") && !criticalActive) {
			info += string.Format ('\n' + "Limit Break at: {0} HP" + '\n', CriticalLimit);
		} else {
			info += string.Format (" " + '\n');
		}


		return info;
	}

/**
private ArrayList colors;

public ArrayList normalAddedSkills;
public ArrayList specialAddedSkills;
public ArrayList vitalityAddedSkills;
public ArrayList burstAddedSkills;
public ArrayList sactionAddedSkills;
public ArrayList jumpAddedSkills;
public ArrayList itemAddedSkills;
public ArrayList singleAddedSkills;
private Player counterPlayer;
private ArrayList trackSkills;
private ArrayList longestCombo;
private string longestComboText;

private ArrayList allSkills;
private ArrayList normalSkills;
private ArrayList specialSkills;
private ArrayList vitalitySkills;
private ArrayList burstSkills;
private ArrayList jumpSkills;
private ArrayList itemSkills;
private ArrayList inventory;

private Skill sActionSkill;
private Skill counterSkill;
private Skill criticalSkill;
private Skill contactSkill;
private Skill koSkill;
private Skill tauntSkill;

private ArrayList timeActivatedSkills;

private ArrayList chainSkills;
private ArrayList burstFollowUpSkills;
private Player lockedOnTarget;

private Player storedPlayer;
private Player absorbedPlayer;

private string element;

Reel myReel;

*/



	public override string ToString ()
	{
		string info = BasicInfo;
		info += StatInfo;
		info += ItemAndSkillInfo;
		return info;
	}

    public string BasicInfo
    {
        get
        {
            string info = string.Format("{0}" + '\n', Name, Level);
            info += string.Format("Age: {0}" + '\n', Age);
            info += string.Format("Sex: {0}" + '\n', Sex);
            info += string.Format("Nationality: {0}" + '\n', Nationality);
            info += string.Format("Species: {0}" + '\n', Species);
            info += string.Format("Weight: {0} kg." + '\n', Weight);
            info += string.Format("Height: {0} ft." + '\n', Height);
            info += string.Format("Handedness: {0}" + '\n', HandDexterity);
            info += string.Format("Style: {0}" + '\n', Style);
            info += string.Format("Ratio: {0}" + '\n', Ratio);
            info += string.Format("Power Level: {0}" + '\n', PowerLevel);
            info += string.Format("Class: {0}" + '\n', PlayerClass);
            info += string.Format("Play Style: " + PlayStyleDisplay + '\n');
            info += string.Format("Team: " + MyTeam.Name + '\n');
            info += string.Format("Time Remaining: {0}" + '\n', TimeRemaining);
            return info;
        }
    }

    public string StatInfo
    {
        get
        {
            string info = string.Format("Level:{0}" + '\n', Level);
            info += string.Format("Experience:{0}" + '\n', experience);
            info += Health.StringRep () + '\n';
            info += Rush.StringRep () + '\n';
            info += Guard.StringRep () + '\n';
            info += Vitality.StringRep () + '\n';
            info += Stun.StringRep () + '\n';

            info += strength.StringRep() + '\n';
            info += grit.StringRep() + '\n';
            info += magick.StringRep() + '\n';
            info += resistance.StringRep() + '\n';
            info += speed.StringRep() + '\n';
            info += dexterity.StringRep() + '\n';
            info += proration.StringRep() + '\n';
            info += movement.StringRep() + '\n';
            info += teamwork.StringRep() + '\n';
            info += luck.StringRep() + '\n';
            info += actionPoints.StringRep() + '\n';

            info += string.Format("MovesRemaining:" + movesRemaining + '\n');

            for (int i = 0; i < States.Length; i++)
            {
                info += States[i].StringRep() + '\n';
            }

            for (int i = 0; i < StateResistances.Length; i++)
            {
                info += StateResistances[i].StringRep() + '\n';
            }

            for (int i = 0; i < elementalOffense.Length; i++)
            {

                if (i == 0)
                {
                    info += string.Format("Fire ");
                }
                if (i == 1)
                {
                    info += string.Format("Ice ");
                }
                if (i == 2)
                {
                    info += string.Format("Electricity ");
                }
                if (i == 3)
                {
                    info += string.Format("Wind ");
                }
                if (i == 4)
                {
                    info += string.Format("Water ");
                }
                if (i == 5)
                {
                    info += string.Format("Earth ");
                }
                if (i == 6)
                {
                    info += string.Format("Metal ");
                }
                if (i == 7)
                {
                    info += string.Format("Dark ");
                }
                if (i == 8)
                {
                    info += string.Format("Light ");
                }
                if (i == 9)
                {
                    info += string.Format("Crush ");
                }

                info += string.Format("" + elementalOffense[i] + "," + elementalDefense[i] + '\n');
            }

            info += string.Format("Conditions:" + conditions.Length + '\n');
            for (int i = 0; i < conditions.Length; i++)
            {
                info += string.Format(conditions[i] + '\n');
            }

            info += string.Format(TrackSkills.Count + @":TRACKSKILLS" + '\n');
            for (int i = 0; i < TrackSkills.Count; i++)
            {
                string.Format(((Skill)TrackSkills[i]).SearchName + @"" + '\n');
            }

            info += string.Format(TimeActivatedSkills.Count + @":TIMEACTIVATEDSKILLS" + '\n');
            for (int i = 0; i < TimeActivatedSkills.Count; i++)
            {
                string.Format(((Skill)TimeActivatedSkills[i]).SearchName + @"" + '\n');
            }

            info += string.Format("Direction:" + direction + '\n');

            info += string.Format("ObjectIndex:" + objectIndex + '\n');
            info += string.Format("TurnEnded:" + turnEnded + '\n');
            info += string.Format("Cooldown:" + cooldown + '\n');
            info += string.Format("SpecialState:" + specialState + '\n');
            info += string.Format("AirOff:" + airOff + '\n');
            info += string.Format("HitStunAdjustment:" + hitStunAdjustment + '\n');
            info += string.Format("NumRests:" + numRests + '\n');
            info += string.Format("SkillExecuted:" + skillExecuted + '\n');
            info += string.Format("HighestCombo:" + highestCombo + '\n');
            info += string.Format("Combo:" + combo + '\n');
            info += string.Format("ComboDamagePerRound:" + comboDamagePerRound + '\n');
            info += string.Format("Fatigue:" + fatigue + '\n');
            info += string.Format("ResidualFatigue:" + residualFatigue + '\n');
            info += string.Format("CurrentProration:" + currentProration + '\n');
            info += string.Format("CurrentHitRate:" + currentHitRate + '\n');
            info += string.Format("SActionEnabled:" + sActionEnabled + '\n');
            info += string.Format("ReverseName:" + reverseName + '\n');
            info += string.Format("MapSize:" + mapSize + '\n');
            info += string.Format("FirstTurn:" + firstTurn + '\n');
            info += string.Format("NoMotion:" + noMotion + '\n');
            info += string.Format("HurtStun:" + hurtStun + '\n');
            info += string.Format("HasChangedLocations:" + hasChangedLocations + '\n');
            info += string.Format("HasChangedHeights:" + hasChangedHeights + '\n');
            info += string.Format("CriticalLimit:" + criticalLimit + '\n');
            info += string.Format("IsGuarding:" + isGuarding + '\n');
            info += string.Format("AutoGuard:" + autoGuard + '\n');
            info += string.Format("IsDizzied:" + isDizzied + '\n');
            info += string.Format("CanAct:" + canAct + '\n');
            info += string.Format("HasActed:" + hasActed + '\n');
            info += string.Format("HasJumped:" + hasJumped + '\n');
            info += string.Format("WasDizzied:" + wasDizzied + '\n');
            info += string.Format("WasCritical:" + wasCritical + '\n');
            info += string.Format("IsResting:" + isResting + '\n');
            info += string.Format("IsTaunting:" + isTaunting + '\n');
            info += string.Format("TauntDecay:" + tauntDecay + '\n');
            info += string.Format("WasHit:" + wasHit + '\n');
            info += string.Format("InRecovery:" + inRecovery + '\n');
            info += string.Format("HasFallen:" + hasFallen + '\n');
            info += string.Format("CanRest:" + canRest + '\n');
            info += string.Format("CanBeRevived:" + canBeRevived + '\n');
            info += string.Format("CriticalActive:" + criticalActive + '\n');
            info += string.Format("BonusRoundActivated:" + bonusRoundActivated + '\n');
            info += string.Format("HitMaliciously:" + hitMaliciously + '\n');
            info += string.Format("Undead:" + undead + '\n');
            info += string.Format("HalfCost:" + halfCost + '\n');
            info += string.Format("NoCost:" + NoCost + '\n');
            info += string.Format("Leech:" + leech + '\n');
            info += string.Format("CounterState:" + counterState + '\n');
            info += string.Format("PreviousTileHeight:" + previousTileHeight + '\n');
            info += string.Format("DamagePerRound:" + damagePerRound + '\n');
            info += string.Format("DamageTotal:" + damageTotal + '\n');
            info += string.Format("ConnectedPerRound:" + connectsPerRound + '\n');
            info += string.Format("AttacksPerRound:" + attacksPerRound + '\n');
            info += string.Format("ConnectsTotal:" + connectsTotal + '\n');
            info += string.Format("AttacksTotal:" + attacksTotal + '\n');
            info += string.Format("HealingPerRound:" + healingPerRound + '\n');
            info += string.Format("HealingTotal:" + healingTotal + '\n');
            info += string.Format("DamageTakenPerRound:" + damageTakenPerRound + '\n');
            info += string.Format("DamageTakenTotal:" + damageTakenTotal + '\n');
            info += string.Format("StatusAfflicationsTotal:" + statusAfflictionsTotal + '\n');
            info += string.Format("StatusAfflictionsPerRound:" + statusAfflictionsPerRound + '\n');
            info += string.Format("StatusAidsTotal:" + statusAidsTotal + '\n');
            info += string.Format("StatusAidsPerRound:" + statusAidsPerRound + '\n');
            info += string.Format("StunPerRound:" + stunPerRound + '\n');
            info += string.Format("StunTotal:" + stunTotal + '\n');

            info += string.Format("BonusTime:" + bonusTime + '\n');
            info += string.Format("FallingHeight:" + fallingHeight + '\n');
            info += string.Format("LandingHeight:" + LandingHeight + '\n');
            info += string.Format("GuardStun:" + guardStun + '\n');


            info += string.Format("InPlay:" + inPlay + '\n');
            info += string.Format("OnField:" + onField + '\n');
            info += string.Format("LastStand:" + lastStand + '\n');
            info += string.Format("WeakFrame:" + weakFrame + '\n');
            info += string.Format("VMGainedBlocked:" + vmGainBlocked + '\n');
            info += string.Format("BloodPrice:" + bloodPrice + '\n');
            info += string.Format("Flight:" + flight + '\n');
            info += string.Format("Berserk:" + berserk + '\n');
            info += string.Format("TimeStopped:" + timeStopped + '\n');
            info += string.Format("NoGuard:" + noGuard + '\n');
            info += string.Format("NoConditions:" + noConditions + '\n');
            info += string.Format("CanFly:" + canFly + '\n');
            info += string.Format("ActivePlayer:" + activePlayer + '\n');
            info += string.Format("HasActedInRound:" + hasActedInRound + '\n');
            info += string.Format("IsCrouching:" + isCrouching + '\n');
            info += string.Format("Champion:" + champion + '\n');

            info += string.Format("Staggered:" + staggered + '\n');
            if (staggered)
            {
                info += string.Format("StaggerDirection:" + staggerDirection + '\n');
                info += string.Format("staggerTime:" + staggerTime + '\n');
            }

            info += string.Format("AssimilatedClass:" + assimilatedClass + '\n');
            if (assimilatedClass != "")
            {
                info += string.Format("AssimilatedTime:" + assimilatedTime + '\n');
            }

            if (TimedConditions.Count > 0)
            {
                info += string.Format("TimedConditions:" + TimedConditions.Count + '\n');

                for (int i = 0; i < TimedConditions.Count; i++)
                {
                    info += string.Format(((State)TimedConditions[i]).ToString() + '\n');
                }
            }

            if (Owner != null)
            {
                info += string.Format("Master:" + Owner.row() + "," + Owner.column() + " " +
                                                Owner.SearchName + '\n');
                info += string.Format("MeterGainForMaster:" + noGainForMaster + '\n');
            } else {
                info += string.Format("Master: " + '\n'
                                      + "MeterGainForMaster: " + false);
            }
            if (storedPlayer != null)
            {
                info += string.Format("StoredPlayer:" + storedPlayer.SearchName + '\n');
            } else {
                info += string.Format ("StoredPlayer: " + '\n');
            }
            return info;
        }
    }


    public string ItemAndSkillInfo
    {
        get {
            string info = "";
            info = string.Format ("Inventory:" + Inventory.Count + '\n');
            for (int i = 0; i < Inventory.Count; i ++) {
                info += ((Item)Inventory[i]).SearchName + "-" + ((Item)Inventory[i]).NumUses + '\n';
            }

            info += string.Format ('\n' + "Skills:" + AllSkills.Count + '\n');
            for (int i = 0; i < AllSkills.Count; i ++) {
                info += ((Skill)AllSkills [i]).SearchName + '\n';
            }

            info += timeActivatedSkills.Count + ":TimeActivatedSkills" + '\n';
            for (int i = 0; i < TimeActivatedSkills.Count; i++)
            {
                info += ((Skill)TimeActivatedSkills[i]).SearchName + '\n';
            }

            info += trackSkills.Count + ":TrackSkills" + '\n';
            for (int i = 0; i < TrackSkills.Count; i++)
            {
                info += ((Skill)TrackSkills[i]).SearchName + '\n';
            }


            return info;
        }
    }

	private string skillList (string nm, ArrayList list)
	{
		string skillInfo = "";
		if (list.Count > 0) {
			skillInfo += nm + " (" + list.Count + ")" + '\n';

			for (int i = 0; i < list.Count; i++) {
				skillInfo += ((Skill)list [i]).ToString () + '\n';
			}
			skillInfo += "" + '\n' + '\n';
		}
		return skillInfo;
	}

	private string skillRep (string nm, Skill skl)
	{
		if (skl != null) {
			return nm + '\n' + skl.ToString () + '\n' + '\n';
		}
		return "";
	}

	public string SkillInfo
	{
		get {
			return "" + '\n' + skillList ("NORMAL SKILLS", NormalSkills)
				+ skillList ("JUMP SKILLS", JumpSkills)
				+ skillList ("SPECIAL SKILLS", SpecialSkills)
				+ skillList ("VITALITY SKILLS", VitalitySkills)
				+ skillList ("BURST SKILLS", BurstSkills)
				+ skillList ("CONSUMABLES", ItemSkills)
				+ skillRep ("S-ACTION", SActionSkill)
				+ skillRep ("COUNTER", counterSkill)
                + skillRep ("CONTACT", contactSkill)
				+ skillRep ("CRITICAL", criticalSkill)
				+ skillRep ("KNOCKOUT", koSkill)
				+ skillRep ("TAUNT", tauntSkill);
		}
	}

	public string ItemInfo
	{
		get {
			if (Inventory.Count > 0) {
				string output = "";
				int itemCount = 0;
				for (int i = 0; i < Inventory.Count; i++) {
					output += string.Format ("{0}    ", ((Item)Inventory [i]).ToString ());
					if (i % 2 == 1) {
						output += " " + '\n';
					}
				}
				output += InventoryInfo;
				output += " " + '\n';
				return output;
			} else {
				return " " + '\t' + "NO EQUIPMENT" + '\n';
			}
		}
	}

	public string InventoryInfo
	{
		get {
			if (MyTeam.Inventory.Count > 0) {
				string output = "";
				for (int i = 0; i < MyTeam.Inventory.Count; i++) {
					output += string.Format ("{0}    ", ((Skill)MyTeam.Inventory [i]).Name + " (" + ((Skill)MyTeam.Inventory [i]).NumUses + ")");
					if (i % 2 == 1) {
						output += " " + '\n';
					}
				}
				output += " " + '\n';
				return output;
			}
			return "";
		}
	}

	public Player closestLiveUnit (Team t)
	{
		int dSpan = 60;
		Player p = null;
		for (int i = 0; i < t.Roster.Count; i ++) {
			if (!t [i].Equals (this) && !t [i].KOd && span (t [i]) < dSpan) {
				dSpan = span (t [i]);
				p = t [i];
			}
		}
		if (p == null) {
			return this;
		}
		return p;
	}

	public Player closestLiveUnit ()
	{
		int dSpan = 60;
		Player p = null;
		for (int i = 0; i < currentMap ().Roster.Count; i ++) {
			if (!((Player)currentMap ().Roster [i]).Equals (this)
				&& !((Player)currentMap ().Roster [i]).KOd 
				&& span (((Player)currentMap ().Roster [i])) < dSpan) {
				dSpan = span (((Player)currentMap ().Roster [i]));
				p = ((Player)currentMap ().Roster [i]);
			}
		}
		return p;
	}

	public Player closestLiveAlly
	{
		get {
			int dSpan = 60;
			Player p = null;
			for (int i = 0; i < MyTeam.Roster.Count; i++) {
				if (!MyTeam [i].Equals (this) && !MyTeam [i].KOd && span (MyTeam [i]) < dSpan) {
					dSpan = span (MyTeam [i]);
					p = MyTeam [i];
				}
			}
			if (p == null) {
				return this;
			}
			return p;
		}
	}

	public string mapName ()
	{
		return SearchName;
	}

	public Player PlayerStandard
	{
		get { return playerStandard;}
		set { playerStandard = value;}
	}

	public string MasteryUpForSkill (Skill s, Skill sampleSkill)
	{
		string output = "";
		if (sampleSkill != null) {
			if (s.Matches (sampleSkill)) {
				output += sampleSkill.MasteryUp;
			}
			for (int i = 0; i < sampleSkill.LinkSkills.Count; i++) {
				output += MasteryUpForSkill (s, (Skill)sampleSkill.LinkSkills [i]);
			}
			for (int i = 0; i < sampleSkill.LearnSkills.Count; i++) {
				output += MasteryUpForSkill (s, (Skill)sampleSkill.LearnSkills [i]);
			}
			if (s.CounterSkill != null) {
				output += MasteryUpForSkill (s, (Skill)sampleSkill.CounterSkill);
			}
			if (s.TrapSkill != null) {
				output += MasteryUpForSkill (s, (Skill)sampleSkill.TrapSkill);
			}
		}
		return output;
	}

	public string MasteryUp (Skill s)
	{
		string output = "";
		Skill sk;

		output += s.MasteryUp;

		for (int i = 0; i < AllSkills.Count; i++) {
			sk = (Skill)AllSkills [i];
			if (sk != null) {
				output += MasteryUpForSkill (s, sk);
			}
		}
		return output;
	}

	public string linkAllInstancesOfSkill (Skill s, Skill addedSkill, int iteration)
	{
		string output = "";
		Skill sk;
		for (int i = 0; i < s.LinkSkills.Count; i++) {
			if (!s.sameSkillSet (addedSkill)) {
				output += linkAllInstancesOfSkill ((Skill)s.LinkSkills [i], addedSkill, iteration + 1);
			}
		}

		//output += string.Format ("Checking if can link "  + addedSkill.SearchName +  " to " + s.SearchName + '\n');

		if ((addedSkill.CanBeLearnedSpecial
			|| (s.JumpIn && !addedSkill.JumpIn && addedSkill.Properties.Contains ("NORMAL "))
			//|| (s.Properties.Contains ("NORMAL ") && addedSkill.Properties.Contains ("CMD "))
		) &&
		    s.AddLink && addedSkill.Speed <= s.HitStun && !s.LinkSkills.Contains (addedSkill) && !s.sameSkillSet (addedSkill)) {

			//output += string.Format ("YES " + '\n');
			s.LinkSkill (addedSkill);
		} else {
			//output += string.Format ("NO " + '\n');
		}

		return output;
	}


	public Skill getSkill (ArrayList list, string sName)
	{
		Skill s;
		for (int i = 0; i < list.Count; i++) {
			s = (Skill)list [i];
			if (getSkill (s, sName) != null) {
				return getSkill (s, sName);
			}
			//	return (Skill)list [i];
			//}
		}

		return null;
	}


	public Skill getSkill (Skill s, string sName) {
		if (s.SearchName.Equals (sName)) {
			return s;
		}
		for (int i = 0; i < s.LinkSkills.Count; i++) {
			if (getSkill ((Skill)s.LinkSkills [i], sName) != null) {
				return getSkill ((Skill)s.LinkSkills [i], sName);
			}
		}
		return null;
	}

	public Skill getSkill (string sName)
	{
		for (int i = 0; i < AllSkills.Count; i++) {
			if (getSkill ((Skill)AllSkills [i], sName) != null) {
				return (Skill)AllSkills [i];
			}
		}

		return null;
		///return getSkill (AllSkills, sName);
	}

	public Boolean listContains (ArrayList l, string s)
	{
		for (int i = 0; i < l.Count; i ++) {
			if (((Skill)l [i]).SearchName.Equals (s)) {
				return true;
			}
		}
		return false;
	}

	public Skill learnAppendedSkill (Skill clonedSkill)
	{
		ArrayList followList = new ArrayList ();
		for (int i = 0; i < clonedSkill.LinkSkills.Count; i++) {
			if (!listContains (followList, ((Skill) clonedSkill.LinkSkills [i]).SearchName)) {
				followList.Add (clonedSkill.LinkSkills [i]);
			}
		}

		Skill nextSkill = null;
		clonedSkill.LinkSkills.Clear ();

        if (!tacticalMode)
        {
            /**
             *                 nm = reader.ReadLine();
                frames = NumberConverter.ConvertToInt (reader.ReadLine());
                rate = NumberConverter.ConvertToFloat (reader.ReadLine());
                looping = reader.ReadLine().Equals("true");
                active = reader.ReadLine().Equals("true");

                if (i == 0)
                {
                    neutralSprite = new Reel(nm, @"Players/" + searchName + @"/Animations/" + nm, frames, rate, false,
                                              looping, active, 0f, frames > 1, false, true, new Vector3(0, 0, 0));
                }

             */ 
            //string nm = 

            //clonedSkill.Animation = new Reel (@"", );
        }

		if (clonedSkill.OriginalOwner == null) {
			clonedSkill.OriginalOwner = this;
		}

		for (int i = 0; i < followList.Count; i++) {
			nextSkill = ((Skill)followList [i]).CloneSkill;

			if (!listContains (clonedSkill.LinkSkills, nextSkill.SearchName)
				&& (clonedSkill.Properties.Contains ("AUTOLINK ") || nextSkill.Properties.Contains ("CHILD "))) {

				if (nextSkill.Standalone
				    || nextSkill.IsMasterSkill
				    || (nextSkill.CanBeLearnedBase && !nextSkill.Properties.Contains ("CHILD")))
				{
					nextSkill.LinkSkills.Clear ();
					nextSkill.LinkSkills = new ArrayList ();
					clonedSkill.LinkSkill (nextSkill);
				} else {
					clonedSkill.LinkSkill (learnAppendedSkill (nextSkill));
				}
				//clonedSkill.LinkSkill (nextSkill);
			} else {
				//throw new NullReferenceException (nextSkill.Name + " is in the list");
			}
		}
		if (clonedSkill.OriginalOwner != this) {
			if (!clonedSkill.Properties.Contains (" ALWAYS ")) {
				clonedSkill.NoConditions = true;
			}
			for (int i = 0; i < clonedSkill.Cost.Length; i++) {
				if (clonedSkill.Cost [i] < 0 && i != 3) {
					clonedSkill.Cost [i] -= 5;
				}
				if (clonedSkill.Change [i] < 0) {
					clonedSkill.Change [i] += 5;
				}
				if (clonedSkill.Change [i] > 0) {
					clonedSkill.Change [i] -= 5;
				}

			}
		}
		clonedSkill.Owner = this;
		return clonedSkill;
	}

	public string learn (Skill s, Boolean learningInField, int numTurns)
	{
		string output = "";
		if (!knows (s)) {
			output = string.Format ("{0} learns {1}!" + '\n', FirstName, s.Name);
			Skill lrnSkl = s;
            lrnSkl.TurnsRemaining = lrnSkl.MaxTurnsRemaining;
			if (learningInField) {

				lrnSkl.LearnedInField = true;

				StreamWriter writer = new StreamWriter (@"Assets/Resources/Players/" + SearchName + "/Info/LearnLog.txt", true);
				writer.WriteLine (string.Format ("{0} learns {1}!", FirstName, s.Name));

				writer.WriteLine (string.Format ("{0} follow-up old length: {1}!", lrnSkl.Name, lrnSkl.LinkSkills.Count));
				lrnSkl = learnAppendedSkill (s.CloneSkill);
				writer.WriteLine (string.Format ("{0} follow-up new length: {1}!", lrnSkl.Name, lrnSkl.LinkSkills.Count));

				if (s.CanBeLearnedSpecial || s.Properties.Contains ("NORMAL ")) {
					for (int j = 0; j < AllSkills.Count; j++) {
						writer.WriteLine (linkAllInstancesOfSkill ((Skill)allSkills [j], lrnSkl, 0));
					}
				} else {
					writer.WriteLine (string.Format ("{0} ends here...", s.Name));
				}
				writer.Close ();

				lrnSkl.TurnsRemaining = numTurns;

			} if (lrnSkl.OriginalOwner == null) {
				lrnSkl.OriginalOwner = this;
			}

			lrnSkl.Owner = this;
			lrnSkl.Location = currentLocation ();
			allSkills.Add (lrnSkl);

			if (learningInField && currentMap () != null && currentLocation () != null) {
				lrnSkl.setLocations ();
			} else {
				if (lrnSkl.Properties.Contains ("COUNTER") || playerMap != null) {
					lrnSkl.setLocations ();
				}
			}
	
			if (lrnSkl.Properties.Contains ("JUMP")) {
				JumpSkills.Add (lrnSkl);
			} else if (lrnSkl.Properties.Contains ("SACTION") && !lrnSkl.Properties.Contains ("SPECIAL")) {
				SActionSkill = lrnSkl;
			} else if (lrnSkl.Properties.Contains ("TAUNT")) {
				TauntSkill = lrnSkl;
			} else if (lrnSkl.Properties.Contains ("COUNTER")) {
				CounterSkill = lrnSkl;
            } else if (lrnSkl.Properties.Contains("CONTACT")) {
                ContactSkill = lrnSkl;
            } else if (lrnSkl.Properties.Contains ("CRITICAL")) {
				CriticalSkill = lrnSkl;
			} else if (lrnSkl.Properties.Contains ("KNOCKOUT")) {
				KOSkill = lrnSkl;
			} else if (lrnSkl.Properties.Contains ("NORMAL")) {
				NormalSkills.Add (lrnSkl);
			} else if (lrnSkl.Properties.Contains ("SPECIAL")) {
				SpecialSkills.Add (lrnSkl);
			} else if (lrnSkl.Properties.Contains ("VITALITY")) {
				VitalitySkills.Add (lrnSkl);
			} else if (lrnSkl.Properties.Contains ("BURST")) {
				BurstSkills.Add (lrnSkl);
			} else if (lrnSkl.Properties.Contains ("ITEM")) {
				ItemSkills.Add (lrnSkl);
			}

			if (inventoryGet (lrnSkl.ExpendableAmmo) != null && lrnSkl.OriginalOwner != this) {
				Inventory.Add (new DataReader ().loadItem (lrnSkl.OriginalOwner.SearchName, new ArrayList (), lrnSkl.OriginalOwner.inventoryGet (lrnSkl.ExpendableAmmo).SearchName));
			}

			if (learningInField) {
				adjustListForStrongBasics (AllSkills);
				adjustListForStrongBasics (NormalSkills);
				adjustListForStrongBasics (SpecialSkills);
				adjustListForStrongBasics (VitalitySkills);
				adjustListForStrongBasics (BurstSkills);
				adjustListForStrongBasics (ItemSkills);
				adjustListForStrongBasics (JumpSkills);
			}

		} else {
			output += string.Format ("{0} already knows {1}." + '\n', FirstName, s.Name);
		}
		return output;
	}


	public string learnAtBeginning (Skill s, Boolean learningInField)
	{
		string output = "";
		if (!knows (s)) {
			output = string.Format ("{0} learns {1}!" + '\n', FirstName, s.Name);
			Skill lrnSkl = s;
			if (learningInField) {
				output += string.Format ("{0} follow-up old length: {1}!" + '\n', lrnSkl.Name, lrnSkl.LinkSkills.Count);
				lrnSkl = learnAppendedSkill (s.CloneSkill);
				output += string.Format ("{0} follow-up new length: {1}!" + '\n', lrnSkl.Name, lrnSkl.LinkSkills.Count);

				if (s.CanBeLearnedSpecial) {
					for (int j = 0; j < AllSkills.Count; j ++) {
						output += linkAllInstancesOfSkill ((Skill)allSkills [j], lrnSkl, 0);
					}
				}
			}

			lrnSkl.Owner = this;
			lrnSkl.Location = currentLocation ();
			allSkills.Add (lrnSkl);

			if (learningInField && currentMap () != null && currentLocation () != null) {
				lrnSkl.setLocations ();
			} else {
				if (lrnSkl.Properties.Contains ("COUNTER") || playerMap != null) {
					lrnSkl.setLocations ();
				}
			}

			if (lrnSkl.Properties.Contains ("JUMP")) {
				JumpSkills.Add (lrnSkl);
			} else if (lrnSkl.Properties.Contains ("SACTION") && !lrnSkl.Properties.Contains ("SPECIAL")) {
				SActionSkill = lrnSkl;
			} else if (lrnSkl.Properties.Contains ("COUNTER")) {
				CounterSkill = lrnSkl;
            } else if (lrnSkl.Properties.Contains("CONTACT")) {
                ContactSkill = lrnSkl;
            } else if (lrnSkl.Properties.Contains ("CRITICAL")) {
				CriticalSkill = lrnSkl;
			} else if (lrnSkl.Properties.Contains ("KNOCKOUT")) {
				KOSkill = lrnSkl;
			} else if (lrnSkl.Properties.Contains ("NORMAL")) {
				NormalSkills.Insert (0, lrnSkl);
			} else if (lrnSkl.Properties.Contains ("SPECIAL")) {
				SpecialSkills.Add (lrnSkl);
			} else if (lrnSkl.Properties.Contains ("VITALITY")) {
				VitalitySkills.Add (lrnSkl);
			} else if (lrnSkl.Properties.Contains ("BURST")) {
				BurstSkills.Add (lrnSkl);
			} else if (lrnSkl.Properties.Contains ("ITEM")) {
				ItemSkills.Add (lrnSkl);
			}
		} else {
			output += string.Format ("{0} already knows {1}." + '\n', FirstName, s.Name);
		}
		return output;
	}

    public Boolean TacticalMode
    {
        get {
            return tacticalMode;
        }
        set {
            tacticalMode = value;
        }
    }

	public int Combo
	{
		get {
			return combo;
		}
		set {
			combo = value;
		}
	}

	public void resetSkillUsage (ArrayList list)
	{
		for (int i = 0; i < list.Count; i++) {
			((Skill)list [i]).Used = false;
			((Skill)list [i]).HasDropped = false;
			((Skill)list [i]).Combo = false;
			resetSkillUsage (((Skill)list [i]).LinkSkills);
		}
	}

	public void adjustListForStrongBasics (ArrayList list)
	{
		Skill s;
		for (int i = 0; i < list.Count; i++) {
			s = (Skill)list [i];
			if ((s.Cost [3] < 0 && (s.Cost [3] > -300)) || s.Properties.Contains ("VITALITY") || s.Properties.Contains ("SACTION ")) {
				list.Remove (s);
				i--;
			}
			adjustListForStrongBasics (s.LinkSkills);
		}
	}

	public Boolean forget (string name)
	{
		Skill sk;
		for (int i = 0; i < AllSkills.Count; i++) {
			sk = (Skill)AllSkills [i];
			if (sk.SearchName.Equals (name)) {
				return forget (sk);
			}
		}
		return false;
	}

	public Boolean forget (Skill sk, string nm)
	{
		Boolean forgotten = false;
		for (int i = 0; i < sk.LinkSkills.Count; i ++) {
			if (((Skill)sk.LinkSkills [i]).SearchName.Equals (nm)) {

				sk.LinkSkills.Remove (sk.LinkSkills [i]);
				i --;
				forgotten = true;

			} else {
				forgotten = forget ((Skill)sk.LinkSkills [i], nm);
			}
		}
		return forgotten;
	}

	public Boolean forget (ArrayList lst, string nm)
	{
		Boolean forgotten = false;
		Skill sk;
		for (int i = 0; i < lst.Count; i ++) {

			sk = (Skill) lst [i];

			if (forget (sk, nm)) {
				forgotten = true;
			}

			if (sk.SearchName.Equals (nm)) {
				lst.Remove (sk);
				i --;
				forgotten = true;
			}
		}
		return forgotten;
	}

	public Boolean forget (Skill s)
	{

		if (allSkills.Contains (s)) {

			if (!forget (JumpSkills, s.SearchName)
			    && !forget (NormalSkills, s.SearchName)
			    && !forget (SpecialSkills, s.SearchName)
			    && !forget (VitalitySkills, s.SearchName)
			    && !forget (BurstSkills, s.SearchName)
			    && !forget (ItemSkills, s.SearchName)) {

				if (SActionSkill == s) {
					SActionSkill = null;
					return true;
				} if (TauntSkill == s) {
					TauntSkill = null;
					return true;
				} if (CounterSkill == s) {
					CounterSkill = null;
					return true;
                } if (ContactSkill == s) {
                    ContactSkill = null;
                    return true;
                } if (CriticalSkill == s) {
                    CriticalSkill = null;
                    return true;
                } if (KOSkill == s) {
					KOSkill = null;
					return true;
                }
            }

			s.Owner = this;
			allSkills.Remove (s);
		}

		for (int i = 0; i < TimeActivatedSkills.Count; i++) {
			if (((Skill)TimeActivatedSkills [i]).Name.Equals (s.Name)) {
				TimeActivatedSkills.Remove (TimeActivatedSkills [i]);
				return true;
			}
		}

		return false;
	}

	public ArrayList Destinations
	{
		get {
			return destinations;
		}
		set {
			destinations = value;
		}
	}

	public ArrayList OriginalPositions
	{
		get {
			return originalPositions;
		}
		set {
			originalPositions = value;
		}
	}

	public void SetHeight (int amt)
	{
		if (Airborne.Potency != amt) {
			if (Airborne.Potency != 0 || amt > 0) {
				HasChangedHeights = true;
				Airborne.Potency = amt;
				CurrentLocation = new Location (currentLocation ().Row, currentLocation ().Column, amt);
				if (amt < 0) {
					Airborne.Potency = 0;
					CurrentLocation = new Location (currentLocation ().Row, currentLocation ().Column, 0);
				}
			}
		}
	}

	public void SetGround (int amt, Boolean malice)
	{
		if (amt > 0) {
			Airborne.Potency = 0;
			Grounded.Potency += amt;
			HasChangedHeights = true;
		}
	}



	public void setAllLocations ()
	{
		for (int i = 0; i < AllSkills.Count; i ++)
		{	((Skill)AllSkills [i]).setLocations ();}

		for (int i = 0; i < timeActivatedSkills.Count; i++)
		{	((Skill)timeActivatedSkills [i]).setLocations ();}

		if (CounterSkill != null) 
		{	CounterSkill.setLocations ();}

        if (ContactSkill != null)
        { ContactSkill.setLocations(); }

        if (CriticalSkill != null) {
			CriticalSkill.setLocations ();
		}
		if (TauntSkill != null) {
			TauntSkill.setLocations ();
		}
		if (KOSkill != null) {
			KOSkill.setLocations ();
		}
	}

	public void setLocations (Skill s)
	{
		if (s != null) {
			s.setLocations ();
		}
			
		else {
			for (int i = 0; i < AllSkills.Count; i ++)
			{
				((Skill)AllSkills [i]).setLocations ();
			}
		
			for (int i = 0; i < timeActivatedSkills.Count; i++) {
				((Skill)timeActivatedSkills [i]).setLocations ();
			}
			if (CounterSkill != null) {
				CounterSkill.setLocations ();
			}
            if (ContactSkill != null)
            { ContactSkill.setLocations(); }

            if (CriticalSkill != null) {
				CriticalSkill.setLocations ();
			}
			if (TauntSkill != null) {
				TauntSkill.setLocations ();
			}
			if (KOSkill != null) {
				KOSkill.setLocations ();
			}
		}
	}

	public void resetDefeats ()
	{
		for (int i = 0; i < AllSkills.Count; i ++)
		{
			((Skill)AllSkills [i]).resetDefeat ();
		}
		for (int i = 0; i < timeActivatedSkills.Count; i++) {
			((Skill)timeActivatedSkills [i]).resetDefeat ();
		}
		if (CounterSkill != null) {
			CounterSkill.resetDefeat ();
		}
        if (ContactSkill != null)
        { ContactSkill.resetDefeat (); }

        if (CriticalSkill != null) {
			CriticalSkill.resetDefeat ();
		}
		if (KOSkill != null) {
			KOSkill.resetDefeat ();
		}

		if (TauntSkill != null) {
			TauntSkill.resetDefeat ();
		}
	}

	public void resetLocations ()
	{
		hasFallen = false;
		for (int i = 0; i < AllSkills.Count; i ++)
		{
			((Skill)AllSkills [i]).resetLocations ();
		}
		for (int i = 0; i < timeActivatedSkills.Count; i++) {
			((Skill)timeActivatedSkills [i]).resetLocations ();
		}
		if (CounterSkill != null) {
			CounterSkill.resetLocations ();
		}
        if (ContactSkill != null)
        { ContactSkill.setLocations(); }

        if (CriticalSkill != null) {
			CriticalSkill.resetLocations ();
		}
		if (KOSkill != null) {
			KOSkill.resetLocations ();
		}
		if (TauntSkill != null) {
			TauntSkill.resetLocations ();
		}
	}

	public int span (Locatable loc)
	{
		if (loc.sentient ()) {
			return currentLocation ().span (((Player)loc).currentLocation ());
		}
		return (currentLocation ().span (((MapObject)loc).currentLocation ()));
	}

	public Boolean inLine (Location loc)
	{
		return (loc.Row == currentLocation ().Row) || (loc.Column == currentLocation ().Column);
	}

	public Boolean bordersWith (string nm)
	{
		if (North != null && North.mapName ().Equals (nm)) {
			return true;
		}
		if (East != null && East.mapName ().Equals (nm)) {
			return true;
		}
		if (South != null && South.mapName ().Equals (nm)) {
			return true;
		}
		if (West != null && West.mapName ().Equals (nm)) {
			return true;
		}
		return false;
	}

	public Boolean BordersWithAlly
	{
		get {
			if (North != null && North.sentient () && ((Player)North).sameTeam (this)) {
				return true;
			} if (East != null && East.sentient () && ((Player)East).sameTeam (this)) {
				return true;
			} if (South != null && South.sentient () && ((Player)South).sameTeam (this)) {
				return true;
			} if (West != null && West.sentient () && ((Player)West).sameTeam (this)) {
				return true;
			}
			return false;
		}
	}

	public Player BorderAlly
	{
		get {
			if (North != null && North.sentient () && sameTeam ((Player)North)) {
				return (Player)North;
			} if (East != null && East.sentient () && sameTeam ((Player)East)) {
				return (Player)East;
			} if (South != null && South.sentient () && sameTeam ((Player)South)) {
				return (Player)South;
			} if (West != null && West.sentient () && sameTeam ((Player)West)) {
				return (Player)West;
			}
			return null;
		}
	}

    public Player BestBorderAlly
    {
        get {
            ArrayList sqd = new ArrayList ();
            Player pl = null, ally = null;
            int tmwk = 0;
            sqd.Add (North);
            sqd.Add (South);
            sqd.Add (East);
            sqd.Add (West);

            for (int i = 0; i < sqd.Count; i ++) {
                if (sqd[i] != null) {
                    pl = (Player)sqd[i];
                    if (sameTeam (pl) && !pl.KOd && !pl.StateActive (pl.Sleep) && pl.Teamwork > tmwk) {
                        ally = pl;
                        tmwk = pl.Teamwork;
                    }
                }
            }
            return ally;
        }
    }

	public Player BorderStandingAlly
	{
		get {
			if (North != null && North.sentient () && sameTeam ((Player)North) && ((Player)North).IsStanding) {
				return (Player)North;
			} if (East != null && East.sentient () && sameTeam ((Player)East) && ((Player)East).IsStanding) {
				return (Player)East;
			} if (South != null && South.sentient () && sameTeam ((Player)South) && ((Player)South).IsStanding) {
				return (Player)South;
			} if (West != null && West.sentient () && sameTeam ((Player)West) && ((Player)West).IsStanding) {
				return (Player)West;
			}
			return null;
		}
	}

	public Player BorderEnemy
	{
		get {
			if (North != null && North.sentient () && !sameTeam ((Player)North)) {
				return (Player)North;
			} if (East != null && East.sentient () && !sameTeam ((Player)East)) {
				return (Player)East;
			} if (South != null && South.sentient () && !sameTeam ((Player)South)) {
				return (Player)South;
			} if (West != null && West.sentient () && !sameTeam ((Player)West)) {
				return (Player)West;
			}
			return null;
		}
	}

	public Location closestLocationFrom (Location loc, Boolean diagonals) {
		Location l = loc;
		Location [] locs = null;
		if (!diagonals) {
			locs = new Location [4];
			locs [0] = loc.North;
			locs [1] = loc.East;
			locs [2] = loc.South;
			locs [3] = loc.West;
		} else {
			locs = new Location [8];
			locs [0] = loc.North;
			locs [1] = loc.East;
			locs [2] = loc.South;
			locs [3] = loc.West;
			locs [4] = loc.NorthEast;
			locs [5] = loc.SouthEast;
			locs [6] = loc.SouthWest;
			locs [7] = loc.NorthWest;

		}
		int spn = loc.span (currentLocation ());

		for (int i = 0; i < locs.Length; i++) {
			if (locs [i].span (currentLocation ()) < spn) {
				spn = locs [i].span (currentLocation ());
				l = locs [i];
			}
		}
		return l;
	}

    public Boolean FlashingHealth
    {
        get
        {
            return LastStand || ((HealthPresent <= criticalLimit && !KOd) || Critical);//Critical;
        }
    }

    public Boolean FlashingVitality
    {
        get {
            if (PlayStyleActual == @"A") {
                return Vitality.MeterLevel >= 100 && !StateActive (Adle);
            }
            if (PlayStyleActual == @"B") {
                return Vitality.MeterLevel >= 300 && !StateActive (Adle);
            }
            if (PlayStyleActual == @"C") {
                return Vitality.MeterLevel >= 100 && !StateActive (Adle) && CriticalActive;
            }
            return false;
        }
    }

	public Boolean Critical
	{
		get { return Health.MeterLevel <= criticalLimit && !KOd;}
	}

    public Boolean CriticalAppearance
    {
        get { return Health.MeterLevelAppearance <= criticalLimit && !KOd; }
    }

	public int CriticalLimit
	{
		get { return criticalLimit;}
		set { criticalLimit = value;}
	}

	public Boolean KOd
	{

		get { 
			return Health.MeterLevel == 0;
		}
	}

    public Boolean HasFallen
    {
        get {
            return hasFallen;
        }
        set {
            hasFallen = value;
        }
    }

	public Locatable North
	{
		get { return (Locatable)currentMap ().objectAt (currentLocation ().North);}
	}

	public Locatable NorthEast
	{
		get { return (Locatable)currentMap ().objectAt (currentLocation ().NorthEast);}
	}

	public Locatable East
	{
		get { return (Locatable)currentMap ().objectAt (currentLocation ().East);}
	}

	public Locatable SouthEast
	{
		get { return (Locatable)currentMap ().objectAt (currentLocation ().SouthEast);}
	}

	public Locatable South
	{
		get { return (Locatable)currentMap ().objectAt (currentLocation ().South);}
	}

	public Locatable SouthWest
	{
		get { return (Locatable)currentMap ().objectAt (currentLocation ().SouthWest);}
	}

	public Locatable West
	{
		get { return (Locatable)currentMap ().objectAt (currentLocation ().West);}
	}

	public Locatable NorthWest
	{
		get { return (Locatable)currentMap ().objectAt (currentLocation ().NorthWest);}
	}

	public Team MyTeam
	{
		get { return myTeam;}
		set { myTeam = value;}
	}

	public Boolean sameTeam (Player player)
	{
		return MyTeam.Equals (player.MyTeam);
	}

	public string Rest
	{
		get {
            if (!StateActive(Fury))
            {
                isResting = true;
                CanAct = false;
                MovesRemaining = 0;
                //Cooldown = 2;
                return string.Format("{0} rests." + '\n', FirstName);
            }
            return string.Format ("{0} cannot rest!" + '\n', FirstName);
		}
	}

	public string Taunt
	{
		get {
			isTaunting = true;
			CanAct = false;
			TurnEnded = true;
			MovesRemaining = 0;
			//Cooldown = 2;
			return string.Format ("{0} taunts!" + '\n', FirstName);
		}
	}

	public double RestRatio
	{
		get {
			int finalRatio = 0;
			if (IsCrouching) {
				finalRatio = 1;
			}
			if (isResting) {
				if (PlayStyle.Equals ("A")) {
					finalRatio += 4;

				} if (PlayStyle.Equals ("B")) {
					if (LastStand) {
						finalRatio += 6;
					} else {
						finalRatio += 3;
					}
				} else if (PlayStyle.Equals ("C")) {
					finalRatio += 5;
				}
				return finalRatio + NumRests;
			}
			if (PlayStyle.Equals ("A")) {
				if (IsCrouching) {
					return 1.5;
				}
				return 1;
			} if (PlayStyle.Equals ("B")) {
				if (LastStand) {
					if (IsCrouching) {
						return 2.5;
					}
					return 2;
				}
				if (IsCrouching) {
					return 1.3;
				}
				return 0.8;
			} else if (PlayStyle.Equals ("C")) {
				if (IsCrouching) {
					return 1.4;
				}
				return 0.9;
			}
			if (IsCrouching) {
				return 1.5;
			}
			return 1;
		}
	}

	public void adjustList ()
	{
		for (int i = 0; i < AllSkills.Count; i++) {

		}
	}

	public Boolean CriticalActive
	{
		get {
			return criticalActive;
		} set {
			criticalActive = value;
		}
	}

	public Boolean WasCritical
	{
		get {
			return wasCritical;
		} set {
			wasCritical = value;
		}
	}

	public Boolean getsDizzy (Skill s)
	{
		if (StateActive (Confuse)) {
			double randomChance = r.NextDouble ();
			if (s != null) {
				randomChance += Math.Abs ((((double)s.Cost [0]) / 1000)
				                          + (((double)s.Cost [1]) / 100)
				                          + (((double)s.Cost [2]) / 100)
				                          + (((double)s.Cost [3]) / 300));//(((double)s.TotalCost) / 300);
			}
			if (randomChance >= Dexterity - .5) {
				return true;
			}
		}
		return false;
	}

    public int PreviousTileHeight
    {
        get {
            return previousTileHeight;
        }
        set {
            previousTileHeight = value;
        }
    }

	public int ConnectsPerRound
	{
		get {
			return connectsPerRound;
		}
		set {
			connectsPerRound = value;
		}
	}

	public int ConnectsTotal
	{
		get {
			return connectsTotal;
		}
		set {
			connectsTotal = value;
		}
	}

	public int AttacksPerRound
	{
		get {
			return attacksPerRound;
		}
		set {
			attacksPerRound = value;
		}
	}

	public int AttacksTotal
	{
		get {
			return attacksTotal;
		}
		set {
			attacksTotal = value;
		}
	}

	public int Victories
	{
		get {
			return victories;
		}
		set {
			victories = value;
		}
	}

	public int StatusAfflictionsPerRound
	{
		get {
			return statusAfflictionsPerRound;
		}
		set {
			statusAfflictionsPerRound = value;
		}
	}

	public int StatusAfflictionsTotal
	{
		get {
			return statusAfflictionsTotal;
		}
		set {
			statusAfflictionsTotal = value;
		}
	}

	public int StatusAidsPerRound
	{
		get {
			return statusAidsPerRound;
		}
		set {
			statusAidsPerRound = value;
		}
	}

	public int StatusAidsTotal
	{
		get {
			return statusAidsTotal;
		}
		set {
			statusAidsTotal = value;
		}
	}

	public int DamagePerRound
	{
		get {
			return damagePerRound;
		}
		set {
			damagePerRound = value;
		}
	}

	public int ComboDamagePerRound
	{
		get {
			return comboDamagePerRound;
		}
		set {
			comboDamagePerRound = value;
		}
	}


	public int DamageTakenPerRound
	{
		get {
			return damageTakenPerRound;
		}
		set {
			damageTakenPerRound = value;
		}
	}

	public int DamageTakenTotal
	{
		get {
			return damageTakenTotal;
		}
		set {
			damageTakenTotal = value;
		}
	}

	public int DamageTotal
	{
		get {
			return damageTotal;
		}
		set {
			damageTotal = value;
		}
	}

	public int HealingPerRound
	{
		get {
			return healingPerRound;
		}
		set {
			healingPerRound = value;
		}
	}

	public int HealingTotal
	{
		get {
			return healingTotal;
		}
		set {
			healingTotal = value;
		}
	}

	public Player CounterPlayer
	{
		get {
			return counterPlayer;
		}
		set {
			counterPlayer = value;
		}
	}


	public string shiftRush (int amount, Boolean print)
	{
		string output = "";
		if (amount != 0) {
			Rush.MeterLevel += amount;
			if (Rush.MeterLevel < 0) {
				Rush.MeterLevel = 0;
			} else if (Rush.MeterLevel > Rush.MeterMax) {
				Rush.MeterLevel = Rush.MeterMax;
			}

			//PRINT
			if (print) {
				if (amount <= 0) {
					output += string.Format ("{0} {1} RM" + '\n', FirstName, amount);
				} else {
					output += string.Format ("{0} +{1} RM" + '\n', FirstName, amount);
				}
				output += string.Format ("{0} {1}/{2} RM" + '\n', FirstName, Rush.MeterLevel, Rush.MeterMax);
			}
			if (Rush.MeterLevel < Rush.MeterMax / 20) {
				output += GetFatigued;
			}
		}
		return output;
	}

	public string shiftGuard (int amount, Boolean print)
	{
		string output = "";
		Guard.MeterLevel += amount;
		if (Guard.MeterLevel < 0) {
			Guard.MeterLevel = 0;
		} else if (Guard.MeterLevel > Guard.MeterMax) {
			Guard.MeterLevel = Guard.MeterMax;
		}

		//PRINT
		if (print) {
			if (amount <= 0) {
				output += string.Format ("{0} {1} GM" + '\n', FirstName, amount);
			} else {
				output += string.Format ("{0} +{1} GM" + '\n', FirstName, amount);
			}
			output += string.Format ("{0} {1}/{2} GM" + '\n', FirstName, Guard.MeterLevel, Guard.MeterMax);
		}
		if (Guard.MeterLevel == 0) {
			output += GuardBust;
		}
		return output;
	}

	public string shiftVitality (int amount, Boolean print)
	{
		string output = "";
		Boolean maxedOut = Vitality.MeterLevel == Vitality.MeterMax;
		Vitality.MeterLevel += amount;
		if (Vitality.MeterLevel < 0) {
			Vitality.MeterLevel = 0;
		} else if (Vitality.MeterLevel > Vitality.MeterMax) {
			Vitality.MeterLevel = Vitality.MeterMax;
		}

		if (print) {
			if (amount <= 0) {
				output += string.Format ("{0} {1} VM" + '\n', FirstName, amount);
			} else {
				output += string.Format ("{0} +{1} VM" + '\n', FirstName, amount);
			}
			output += string.Format ("{0} {1}/{2} VM" + '\n', FirstName, Vitality.MeterLevel, Vitality.MeterMax);
		}
		if (!maxedOut && Vitality.MeterLevel == Vitality.MeterMax) {
			output += string.Format ("{0} VM Max Bonus!" + '\n' + '\n', FirstName);
		}

		return output;
	}

	public int StunPerRound
	{
		get { return stunPerRound;}
		set { stunPerRound = value;}
	}

	public int StunTotal
	{
		get { return stunTotal;}
		set { stunTotal = value;}
	}

	public int ObjectIndex
	{
		get {
			return objectIndex;
		}
		set {
			objectIndex = value;
		}
	}

	private int difference (int x, int y)
	{
		return Math.Abs (x - y);
	}

	public Boolean isGenNorth (Location loc)
	{
		return isNorth (loc)
			|| isNorthNorthEast (loc)
			|| isNorthNorthWest (loc)
			|| isEastNorthEast (loc)
			|| isWestNorthWest (loc);
	}

	public Boolean isGenEast (Location loc)
	{
		return isEast (loc)
			|| isEastNorthEast (loc)
			|| isEastSouthEast (loc)
			|| isNorthNorthEast (loc)
			|| isSouthSouthEast (loc);
	}

	public Boolean isGenSouth (Location loc)
	{
		return isSouth (loc)
			|| isSouthSouthEast (loc)
			|| isSouthSouthWest (loc)
			|| isEastSouthEast (loc)
			|| isWestSouthWest (loc);
	}

	public Boolean isGenWest (Location loc)
	{
		return isWest (loc)
			|| isWestNorthWest (loc)
			|| isWestSouthWest (loc)
			|| isNorthNorthWest (loc)
			|| isSouthSouthWest (loc);
	}

	public Boolean isMostlyNorth (Location loc)
	{
		return isNorth (loc)
			|| isNorthNorthEast (loc)
			|| isNorthNorthWest (loc);
	}

	public Boolean isMostlyEast (Location loc)
	{
		return isEast (loc)
			|| isEastNorthEast (loc)
			|| isEastSouthEast (loc);
	}

	public Boolean isMostlySouth (Location loc)
	{
		return isSouth (loc)
			|| isSouthSouthEast (loc)
			|| isSouthSouthWest (loc);
	}

	public Boolean isMostlyWest (Location loc)
	{
		return isWest (loc)
			|| isWestNorthWest (loc)
			|| isWestSouthWest (loc);
	}

	public Boolean isNorth (Location loc)
	{
		return (loc.Row < currentLocation ().Row)
			&& (loc.Column == currentLocation ().Column);
	}

	public Boolean isNorthNorthEast (Location loc)
	{
		return (loc.Row < currentLocation ().Row)
			&& (loc.Column > currentLocation ().Column)
			&& (difference (loc.Row, currentLocation ().Row)
				> difference (loc.Column, currentLocation ().Column));
	}

	public Boolean isNorthEast (Location loc)
	{
		return (loc.Row < currentLocation ().Row)
			&& (loc.Column > currentLocation ().Column);
	}

	public Boolean isEastNorthEast (Location loc)
	{
		return (loc.Row < currentLocation ().Row)
			&& (loc.Column > currentLocation ().Column)
			&& (difference (loc.Row, currentLocation ().Row)
				< difference (loc.Column, currentLocation ().Column));
	}

	public Boolean isEast (Location loc)
	{
		return (loc.Row == currentLocation ().Row)
			&& (loc.Column > currentLocation ().Column);
	}

	public Boolean isEastSouthEast (Location loc)
	{
		return (loc.Row > currentLocation ().Row)
			&& (loc.Column > currentLocation ().Column)
			&& (difference (loc.Row, currentLocation ().Row)
				< difference (loc.Column, currentLocation ().Column));
	}

	public Boolean isSouthEast (Location loc)
	{
		return (loc.Row > currentLocation ().Row)
			&& (loc.Column > currentLocation ().Column);
	}

	public Boolean isSouthSouthEast (Location loc)
	{
		return (loc.Row > currentLocation ().Row)
			&& (loc.Column > currentLocation ().Column)
			&& (difference (loc.Row, currentLocation ().Row)
				> difference (loc.Column, currentLocation ().Column));
	}

	public Boolean isSouth (Location loc)
	{
		return (loc.Row > currentLocation ().Row)
			&& (loc.Column == currentLocation ().Column);
	}

	public Boolean isSouthSouthWest (Location loc)
	{
		return (loc.Row > currentLocation ().Row)
			&& (loc.Column < currentLocation ().Column)
			&& (difference (loc.Row, currentLocation ().Row)
				> difference (loc.Column, currentLocation ().Column));
	}

	public Boolean isSouthWest (Location loc)
	{
		return (loc.Row > currentLocation ().Row)
			&& (loc.Column < currentLocation ().Column);
	}

	public Boolean isWestSouthWest (Location loc)
	{
		return (loc.Row > currentLocation ().Row)
			&& (loc.Column < currentLocation ().Column)
			&& (difference (loc.Row, currentLocation ().Row)
				< difference (loc.Column, currentLocation ().Column));
	}

	public Boolean isWest (Location loc)
	{
		return (loc.Row == currentLocation ().Row)
			&& (loc.Column < currentLocation ().Column);
	}

	public Boolean isWestNorthWest (Location loc)
	{
		return (loc.Row < currentLocation ().Row)
			&& (loc.Column < currentLocation ().Column)
			&& (difference (loc.Row, currentLocation ().Row)
				< difference (loc.Column, currentLocation ().Column));
	}

	public Boolean isNorthWest (Location loc)
	{
		return (loc.Row < currentLocation ().Row)
			&& (loc.Column < currentLocation ().Column);
	}

	public Boolean isNorthNorthWest (Location loc)
	{
		return (loc.Row < currentLocation ().Row)
			&& (loc.Column < currentLocation ().Column)
			&& (difference (loc.Row, currentLocation ().Row)
				> difference (loc.Column, currentLocation ().Column));
	}

	public Boolean CanMove
	{
		get {
			return Movement > 0 && movesRemaining > 0 && !StateActive (Freeze) && !StateActive (Sleep);
		}
	}

	public Boolean targetWithinRange (Skill s, Location loc) {
		if (s.SelfSkill) {
			return true;
		}
		return s.Locations.Contains (loc);
	}

	public ArrayList getClusterFromDirection (Location loc, string dir, int width)
	{
		int size = (2 * width) - 1;
		Location firstLoc;
		Location newLoc;
		ArrayList locCluster = new ArrayList ();

		/**
			 * 
			 * NORTH/SOUTH
			 * 
			 */

		if (dir.Equals ("N") || dir.Equals ("NNE") || dir.Equals ("NNW")
			|| dir.Equals ("S") || dir.Equals ("SSE") || dir.Equals ("SSW")) {

			if ((HandDexterity.Equals ("Left")
				&& (dir.Equals ("N") || dir.Equals ("NNE") || dir.Equals ("NNW")))
				|| (HandDexterity.Equals ("Right")
					&& (dir.Equals ("S") || dir.Equals ("SSE") || dir.Equals ("SSW")))) {

				firstLoc = new Location (loc.Row, loc.Column - Math.Abs (width));

				for (int i = 0; i < size; i++) {
					newLoc = new Location (firstLoc.Row, firstLoc.Column + (i + 1));
					if (currentMap ().isValid (newLoc)) {
						locCluster.Add (newLoc);
					}
				}
			} else {
				firstLoc = new Location (loc.Row, loc.Column + Math.Abs (width));

				for (int i = 0; i < size; i++) {
					newLoc = new Location (firstLoc.Row, firstLoc.Column - (i + 1));
					if (currentMap ().isValid (newLoc)) {
						locCluster.Add (newLoc);
					}
				}
			}
		}

		/**
			 * 
			 * EAST/WEST
			 * 
			 */

		else if (dir.Equals ("E") || dir.Equals ("ENE") || dir.Equals ("ESE")
			|| dir.Equals ("W") || dir.Equals ("WNW") || dir.Equals ("WSW")) {


			if ((HandDexterity.Equals ("Left")
				&& (dir.Equals ("W") || dir.Equals ("WNW") || dir.Equals ("WSW")))
				|| (HandDexterity.Equals ("Right")
					&& (dir.Equals ("S") || dir.Equals ("SSE") || dir.Equals ("SSW")))) {

				firstLoc = new Location (loc.Row - Math.Abs (width), loc.Column);
				for (int i = 0; i < size; i++) {
					newLoc = new Location (firstLoc.Row + (i + 1), firstLoc.Column);
					if (currentMap ().isValid (newLoc)) {
						locCluster.Add (newLoc);
					}
				}
			} else {
				firstLoc = new Location (loc.Row + Math.Abs (width), loc.Column);

				for (int i = 0; i < size; i++) {
					newLoc = new Location (firstLoc.Row - (i + 1), firstLoc.Column);
					if (currentMap ().isValid (newLoc)) {
						locCluster.Add (newLoc);
					}
				}
			}
		}
		return locCluster;
	}

	public int HurtStun
	{
		get {
			return hurtStun;
		}
		set {
			hurtStun = value;
		}
	}

	public double GuardRatio
	{
		get {
			if (isGuarding) {
				if (AutoGuard) {
					return 10.0;
					//return 7.5;
				}
				return 3.0;
			}
			return 1.0;

		}
	}

	public double guardRatio (Boolean grapple)
	{
		if (isGuarding && !grapple) {
			if (AutoGuard) {
				return 10.0;//7.5;
			}
			return 3.0;
		}
		return 1.0;
	}

	public string GuardStance
	{
		get {
			string output = "";
			if (!NoGuard) {
				if (!StateActive (Fury)) {
					output += string.Format ("{0} guards." + '\n', FirstName);
					IsGuarding = true;
					CanAct = false;
					TurnEnded = true;
					MovesRemaining = 0;
					//output += shiftGuard (-10, false);
				} else {
					output += string.Format ("{0} is too hyped to guard!" + '\n', FirstName);
				}
			} else {
				output += "Cannot guard!" + '\n';
			}
			return output;
		}
	}

	public string AutoGuardStance
	{
		get {
			string output = "";
			if (!NoGuard) {
				if (!StateActive (Fury)) {
					output += string.Format ("{0} auto guards." + '\n', FirstName);
					IsGuarding = true;
					AutoGuard = true;
					CanAct = false;
					TurnEnded = true;
					MovesRemaining = 0;
					//output += shiftGuard (-10, false);
				} else {
					output += string.Format ("{0} is too hyped to guard!" + '\n', FirstName);
				}
			} else {
				output += "Cannot guard" + '\n';
			}
			return output;
		}
	}

	public string AutoGuardStanceSpecial
	{
		get {
			string output = "";
			if (!StateActive (Fury)) {
				output += string.Format ("{0} variable guards." + '\n', FirstName);
				IsGuarding = true;
				AutoGuard = true;
				CanAct = false;
				TurnEnded = true;
				MovesRemaining = 0;
				//output += shiftGuard (-10, false);
			} else {
				output += string.Format ("{0} is too hyped to guard!" + '\n', FirstName);
			}
			return output;
		}
	}

	public Color Color
	{
		get {
			return color;
		}
		set {
			color = value;
		}
	}

	public string Crouch
	{
		get {
			string output = "";
			if (!IsCrouching) {
				output += string.Format ("{0} crouches." + '\n', FirstName);
				output += shiftGuard (-2, false);
				IsCrouching = true;
			} else if (!StateActive (Confuse)) {
				output += string.Format ("{0} stands up." + '\n', FirstName);
				output += shiftGuard (-2, false);
				IsCrouching = false;
			}
			return output;
		}
	}

	public string GuardBust
	{
		get {
			string output = "";
			output += string.Format ("{0} Guard Bust!" + '\n', FirstName);
			States [8].Add (new State ("Confuse", "CON", 2, 0.0, 2, 1.0, false, ""));
			isGuarding = false;
			isCrouching = false;
			IsDizzied = true;
			return output;
		}
	}

	public string GetFatigued
	{
		get {
			string output = "";
			output += string.Format ("{0} is fatigued!" + '\n', FirstName);
			Daze.Add (new State ("Daze", "DAZ", 1, 0, 1, 1.0, false, ""));
			return output;
		}
	}

	public Boolean skillHeightCanChange (Skill s, Location loc)
	{
		if (s.Properties.Contains (" MAP ")
		    && (s.Properties.Contains ("ALONGGROUND") || s.Properties.Contains ("GRADIUALHEIGHT"))) {
			return true;
		}

		if (s.Projectile && !s.Properties.Contains ("GRADUALHEIGHT")) {
			return false;
		}

		return s.Properties.Contains ("TELEPORT")
			|| (s.Properties.Contains ("EVERYLEVEL ")
				|| (s.MotionRange != 0 || s.Properties.Contains ("MOVE "))
				|| ((s.Properties.Contains ("GRADUALHEIGHT") 
					|| s.Properties.Contains ("LEVELDIFF ")
				) && (s.CurrentHeight - currentMap ().heightOf (loc) <= 1)))
			|| (s.Properties.Contains ("LEAP") && (s.CurrentHeight - currentMap ().heightOf (loc) <= 2));
	}

	public Boolean skillHeightDifferenceTooLarge (Skill s, Skill sk)
	{
		int heightDiff = s.CurrentHeight - sk.CurrentHeight;

		if (s.Properties.Contains ("TRACK") || s.Properties.Contains ("ALLHEIGHT")) {
			return false;
		}

		//ATTACKER IS LOWER THAN TARGET, AND LOCATION IS LOWER THAN TARGET LOCATION
		if (heightDiff <= 0) {
			return (Math.Abs (heightDiff) >= 2)
				|| (s.Properties.Contains ("BIG ") && Math.Abs (heightDiff) >= 3)
				|| (s.Properties.Contains ("HUGE ") && Math.Abs (heightDiff) >= 4)
				|| (s.Properties.Contains ("MASSIVE ") && Math.Abs (heightDiff) >= 5);

		}
		//ATTACKER IS HIGHER THAN OR LEVEL TO TARGET, AND LOCATION IS LOWER THAN TARGET LOCATION

		if (heightDiff > 0) {
			return (heightDiff >= 2)
				|| (s.Properties.Contains ("BIG ") && heightDiff >= 3)
				|| (s.Properties.Contains ("HUGE ") && heightDiff >= 4)
				|| (s.Properties.Contains ("MASSIVE ") && heightDiff >= 5);
		}
		return true;
	}

	public Boolean mapHeightDifferenceTooLarge (Skill s, Location targetLoc)
	{
		int heightDiff = s.CurrentHeight - currentMap ().heightOf (targetLoc);

		if (s.Properties.Contains ("TRACK") || s.Properties.Contains ("ALLHEIGHT")) {
			return false;
		}

		//ATTACKER IS LOWER THAN TARGET, AND LOCATION IS LOWER THAN TARGET LOCATION
		if (heightDiff <= 0) {
			return (Math.Abs (heightDiff) >= 2)
				|| (s.Properties.Contains ("BIG ") && Math.Abs (heightDiff) >= 3)
				|| (s.Properties.Contains ("HUGE ") && Math.Abs (heightDiff) >= 4)
					|| (s.Properties.Contains ("MASSIVE ") && Math.Abs (heightDiff) >= 5);

			}
			//ATTACKER IS HIGHER THAN OR LEVEL TO TARGET, AND LOCATION IS LOWER THAN TARGET LOCATION
			
		if (heightDiff > 0) {
			return (heightDiff >= 2)
				|| (s.Properties.Contains ("BIG ") && heightDiff >= 3)
				|| (s.Properties.Contains ("HUGE ") && heightDiff >= 4)
				|| (s.Properties.Contains ("MASSIVE ") && heightDiff >= 5);
		}
		return true;
	}

	public Boolean lineHeightDifferenceTooLarge (Skill s, int heightDiff, Location targetLoc, Location loc)
	{
		//ATTACKER IS LOWER THAN TARGET, AND LOCATION IS LOWER THAN TARGET LOCATION
		return (heightDiff <= 0
			&& (currentMap ().heightDifference (loc, targetLoc) <= -1)
			|| (s.Properties.Contains ("BIG ") && currentMap ().heightDifference (loc, targetLoc) <= -2)
			|| (s.Properties.Contains ("HUGE ") && currentMap ().heightDifference (loc, targetLoc) <= -3)
			|| (s.Properties.Contains ("MASSIVE ") && currentMap ().heightDifference (loc, targetLoc) <= -4))

			//ATTACKER IS HIGHER THAN OR LEVEL TO TARGET, AND LOCATION IS LOWER THAN TARGET LOCATION
			|| (heightDiff > 0
				&& (currentMap ().heightDifference (loc, targetLoc) <= -2)
				|| (s.Properties.Contains ("BIG ") && currentMap ().heightDifference (loc, targetLoc) <= -3)
				|| (s.Properties.Contains ("HUGE ") && currentMap ().heightDifference (loc, targetLoc) <= -4)
				|| (s.Properties.Contains ("MASSIVE ") && currentMap ().heightDifference (loc, targetLoc) <= -5));
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

	public Boolean setCostsAndConditionsMet (int [] costSet, ArrayList skillSet) {
		Skill s;
		for (int i = 0; i < skillSet.Count; i++) {
			s = (Skill)skillSet [i];
			if (s.costsAndConditionsMet (costSet)) {
				return true;
			}
		}
		return false;
	}

	public string Unguard
	{
		get {
			string output = "";
			IsGuarding = false;
			if (IsCrouching && !StateActive (Confuse)) {
				IsCrouching = false;
			}
			GuardStun = 0;
			return output;
		}
	}

	public Boolean canCounter (Player target, Skill s)
	{
		return (target.CounterSkill != null
		        && s.Malicious
				&& !target.KOd
		        && target.StateActive (target.Counter)
				&& target.CounterSkill.CostsAndConditionsMet
				&& target.CounterSkill.canCounter (s)
				&& (target.CounterSkill.SelfSkill
				|| target.CounterSkill.Locations.Contains (currentLocation ())
				|| target.CounterSkill.Targetless));			
	}

    public Boolean hasMadeContactWith (Player target, Skill s)
    {
        if (ContactSkill != null)
        {
            ContactSkill.setLocations();

            if (s != null)
                return ((s.Physical || s.Grapple)
                    && ContactSkill != null
                    && ContactSkill.CostsAndConditionsMet);
            return ContactSkill != null && ContactSkill.CostsAndConditionsMet;
        }
        return false;
    }

	public Boolean canWalk (Location loc)
	{
		return currentMap ().isValid (loc) && ((currentMap ().isEmpty (loc) || (currentMap ().playerAt (loc) != null && currentMap ().playerAt (loc).sameTeam (this) && currentMap ().playerAt (loc).IsActive))
			&& (Flight || currentMap ().heightOf (currentLocation ()) - currentMap ().heightOf (loc) >= -1));
	}


	public Boolean IsActive
	{
		get {
			return !KOd
				&& !IsDizzied
				&& !StateActive (Confuse)
				&& !StateActive (Sleep)
				&& !StateActive (Grounded)
				&& !StateActive (Airborne)
				&& !IsGuarding
				&& !IsCrouching
				&& HurtStun == 0
				&& Fatigue < 2;
		}
	}

	public Boolean IsAble
	{
		get {
			return !KOd
			&& !StateActive (Sleep);
		}
	}

	public Boolean isVisible ()
	{
		return !StateActive (Invisible);
	}

	public ArrayList TrackSkills
	{
		get { return trackSkills;}
		set { trackSkills = value;}
	}

	//COUNTER JUGGLE STATE
	public Boolean counterJuggle (Skill s, Player target)
	{
		return s.Malicious && s.StateAlterations [2].Probability == 1 && s.Properties.Contains ("CTRAIR") && (target.IsVulnerable);
	}

	public int UnitHeight
	{
		get {
			return unitHeight;
		}
		set {
			unitHeight = value;
		}

	}

	public Boolean InPlay
	{
		get { return inPlay;}
		set { inPlay = value;}
	}

	public Boolean OnField
	{
		get { return onField;}
		set { onField = value;}
	}

	public Boolean knows (string txt)
	{
		for (int i = 0; i < AllSkills.Count; i++) {
			if (((Skill)AllSkills [i]).SearchName.Equals (txt)) {
				return true;
			}
		}
		return false;
	}

	public Boolean knows (Skill s)
	{
		for (int i = 0; i < AllSkills.Count; i++) {
			if (((Skill)AllSkills [i]).SearchName.Equals (s.SearchName)) {
				return true;
			}
		}
		return false;
	}
	
	public Location openInteractableOf (Location loc, Boolean benign)
	{
		Location [] allLocs = new Location [9];
		allLocs [0] = loc;

		//WHEN OBJECT IS NORTH
		if (isMostlyNorth (loc)) {
			allLocs [1] = loc.South;
			if (isGenEast (loc)) {
				allLocs [2] = loc.West;
				allLocs [3] = loc.East;
				allLocs [4] = loc.North;
				allLocs [5] = loc.SouthWest;
				allLocs [6] = loc.NorthWest;
				allLocs [7] = loc.SouthEast;
				allLocs [8] = loc.NorthEast;
			} else {
				allLocs [2] = loc.East;
				allLocs [3] = loc.West;
				allLocs [4] = loc.North;
				allLocs [5] = loc.SouthEast;
				allLocs [6] = loc.NorthEast;
				allLocs [7] = loc.SouthWest;
				allLocs [8] = loc.NorthWest;
			}
		}

		//WHEN OBJECT IS SOUTH
		else if (isMostlySouth (loc)) {
			allLocs [1] = loc.North;
			if (isGenEast (loc)) {
				allLocs [2] = loc.West;
				allLocs [3] = loc.East;
				allLocs [4] = loc.South;
				allLocs [5] = loc.NorthWest;
				allLocs [6] = loc.SouthWest;
				allLocs [7] = loc.NorthEast;
				allLocs [8] = loc.SouthEast;
			} else {
				allLocs [2] = loc.East;
				allLocs [3] = loc.West;
				allLocs [4] = loc.South;
				allLocs [5] = loc.NorthEast;
				allLocs [6] = loc.SouthEast;
				allLocs [7] = loc.NorthWest;
				allLocs [8] = loc.SouthWest;
			}
		}

		//WHEN OBJECT IS EAST
		else if (isMostlyEast (loc)) {
			allLocs [1] = loc.West;
			if (isGenNorth (loc)) {
				allLocs [2] = loc.South;
				allLocs [3] = loc.North;
				allLocs [4] = loc.East;
				allLocs [5] = loc.SouthWest;
				allLocs [6] = loc.NorthWest;
				allLocs [7] = loc.SouthEast;
				allLocs [8] = loc.NorthEast;
			} else {
				allLocs [2] = loc.North;
				allLocs [3] = loc.South;
				allLocs [4] = loc.East;
				allLocs [5] = loc.NorthWest;
				allLocs [6] = loc.SouthWest;
				allLocs [7] = loc.NorthEast;
				allLocs [8] = loc.SouthEast;
			}
		}

		//WHEN OBJECT IS WEST
		else if (isMostlyWest (loc)) {
			allLocs [1] = loc.East;
			if (isGenNorth (loc)) {
				allLocs [2] = loc.South;
				allLocs [3] = loc.North;
				allLocs [4] = loc.West;
				allLocs [5] = loc.SouthEast;
				allLocs [6] = loc.NorthEast;
				allLocs [7] = loc.SouthWest;
				allLocs [8] = loc.NorthWest;
			} else {
				allLocs [2] = loc.North;
				allLocs [3] = loc.South;
				allLocs [4] = loc.West;
				allLocs [5] = loc.NorthEast;
				allLocs [6] = loc.SouthEast;
				allLocs [7] = loc.NorthWest;
				allLocs [8] = loc.SouthWest;
			}
		}

		//WHEN OBJECT IS NORTHEAST
		else if (isNorthEast (loc)) {
			allLocs [2] = loc.South;
			allLocs [3] = loc.West;
			allLocs [1] = loc.SouthWest;
			allLocs [4] = loc.SouthEast;
			allLocs [5] = loc.NorthWest;
			allLocs [6] = loc.North;
			allLocs [7] = loc.East;
			allLocs [8] = loc.NorthEast;
		}

		//WHEN IS NORTHWEST
		else if (isNorthWest (loc)) {
			allLocs [1] = loc.SouthEast;
			allLocs [2] = loc.South;
			allLocs [3] = loc.East;
			allLocs [4] = loc.SouthWest;
			allLocs [5] = loc.NorthEast;
			allLocs [6] = loc.North;
			allLocs [7] = loc.West;
			allLocs [8] = loc.NorthWest;
		}

		//WHEN IS SOUTHWEST
		else if (isSouthWest (loc)) {
			allLocs [1] = loc.NorthEast;
			allLocs [2] = loc.North;
			allLocs [3] = loc.East;
			allLocs [4] = loc.NorthWest;
			allLocs [5] = loc.SouthEast;
			allLocs [6] = loc.South;
			allLocs [7] = loc.West;
			allLocs [8] = loc.SouthWest;
		}

		//WHEN IS SOUTHEAST
		else if (isSouthEast (loc)) {
			allLocs [1] = loc.NorthWest;
			allLocs [2] = loc.North;
			allLocs [3] = loc.West;
			allLocs [4] = loc.NorthEast;
			allLocs [5] = loc.SouthWest;
			allLocs [6] = loc.South;
			allLocs [7] = loc.East;
			allLocs [8] = loc.SouthEast;
		}

		for (int i = 0; i < allLocs.Length; i++) {
			if (currentMap ().isValid (allLocs [i])
				&& ((currentMap ().isEmpty (allLocs [i]) || (benign && currentMap ().objectAt (allLocs [i]).sentient ()))
					&& currentMap ().isEmptyInteractable (allLocs [i]))) {
				return allLocs [i];

			}
		}

		for (int i = 0; i < allLocs.Length; i++) {
			if (currentMap ().isValid (allLocs [i])
			) {
				if (openInteractableOf (allLocs [i], benign) != null && !allLocs [i].Equals (loc)) {
					return openInteractableOf (allLocs [i], benign);
				}
			}
		}
		return null;
	}

	public ArrayList randomInteractablesOf (Location loc)
	{
		Location [] allLocs = new Location [9];
		ArrayList randomLocs = new ArrayList ();
		allLocs [0] = loc;

		//WHEN OBJECT IS NORTH
		if (isMostlyNorth (loc)) {
			allLocs [1] = loc.South;
			if (isGenEast (loc)) {
				allLocs [2] = loc.West;
				allLocs [3] = loc.East;
				allLocs [4] = loc.North;
				allLocs [5] = loc.SouthWest;
				allLocs [6] = loc.NorthWest;
				allLocs [7] = loc.SouthEast;
				allLocs [8] = loc.NorthEast;
			} else {
				allLocs [2] = loc.East;
				allLocs [3] = loc.West;
				allLocs [4] = loc.North;
				allLocs [5] = loc.SouthEast;
				allLocs [6] = loc.NorthEast;
				allLocs [7] = loc.SouthWest;
				allLocs [8] = loc.NorthWest;
			}
		}

		//WHEN OBJECT IS SOUTH
		else if (isMostlySouth (loc)) {
			allLocs [1] = loc.North;
			if (isGenEast (loc)) {
				allLocs [2] = loc.West;
				allLocs [3] = loc.East;
				allLocs [4] = loc.South;
				allLocs [5] = loc.NorthWest;
				allLocs [6] = loc.SouthWest;
				allLocs [7] = loc.NorthEast;
				allLocs [8] = loc.SouthEast;
			} else {
				allLocs [2] = loc.East;
				allLocs [3] = loc.West;
				allLocs [4] = loc.South;
				allLocs [5] = loc.NorthEast;
				allLocs [6] = loc.SouthEast;
				allLocs [7] = loc.NorthWest;
				allLocs [8] = loc.SouthWest;
			}
		}

		//WHEN OBJECT IS EAST
		else if (isMostlyEast (loc)) {
			allLocs [1] = loc.West;
			if (isGenNorth (loc)) {
				allLocs [2] = loc.South;
				allLocs [3] = loc.North;
				allLocs [4] = loc.East;
				allLocs [5] = loc.SouthWest;
				allLocs [6] = loc.NorthWest;
				allLocs [7] = loc.SouthEast;
				allLocs [8] = loc.NorthEast;
			} else {
				allLocs [2] = loc.North;
				allLocs [3] = loc.South;
				allLocs [4] = loc.East;
				allLocs [5] = loc.NorthWest;
				allLocs [6] = loc.SouthWest;
				allLocs [7] = loc.NorthEast;
				allLocs [8] = loc.SouthEast;
			}
		}

		//WHEN OBJECT IS WEST
		else if (isMostlyWest (loc)) {
			allLocs [1] = loc.East;
			if (isGenNorth (loc)) {
				allLocs [2] = loc.South;
				allLocs [3] = loc.North;
				allLocs [4] = loc.West;
				allLocs [5] = loc.SouthEast;
				allLocs [6] = loc.NorthEast;
				allLocs [7] = loc.SouthWest;
				allLocs [8] = loc.NorthWest;
			} else {
				allLocs [2] = loc.North;
				allLocs [3] = loc.South;
				allLocs [4] = loc.West;
				allLocs [5] = loc.NorthEast;
				allLocs [6] = loc.SouthEast;
				allLocs [7] = loc.NorthWest;
				allLocs [8] = loc.SouthWest;
			}
		}

		//WHEN OBJECT IS NORTHEAST
		else if (isNorthEast (loc)) {
			allLocs [2] = loc.South;
			allLocs [3] = loc.West;
			allLocs [1] = loc.SouthWest;
			allLocs [4] = loc.SouthEast;
			allLocs [5] = loc.NorthWest;
			allLocs [6] = loc.North;
			allLocs [7] = loc.East;
			allLocs [8] = loc.NorthEast;
		}

		//WHEN IS NORTHWEST
		else if (isNorthWest (loc)) {
			allLocs [1] = loc.SouthEast;
			allLocs [2] = loc.South;
			allLocs [3] = loc.East;
			allLocs [4] = loc.SouthWest;
			allLocs [5] = loc.NorthEast;
			allLocs [6] = loc.North;
			allLocs [7] = loc.West;
			allLocs [8] = loc.NorthWest;
		}

		//WHEN IS SOUTHWEST
		else if (isSouthWest (loc)) {
			allLocs [1] = loc.NorthEast;
			allLocs [2] = loc.North;
			allLocs [3] = loc.East;
			allLocs [4] = loc.NorthWest;
			allLocs [5] = loc.SouthEast;
			allLocs [6] = loc.South;
			allLocs [7] = loc.West;
			allLocs [8] = loc.SouthWest;
		}

		//WHEN IS SOUTHEAST
		else if (isSouthEast (loc)) {
			allLocs [1] = loc.NorthWest;
			allLocs [2] = loc.North;
			allLocs [3] = loc.West;
			allLocs [4] = loc.NorthEast;
			allLocs [5] = loc.SouthWest;
			allLocs [6] = loc.South;
			allLocs [7] = loc.East;
			allLocs [8] = loc.SouthEast;
		}

		for (int i = 0; i < allLocs.Length; i++) {
			if (currentMap ().isValid (allLocs [i])
				&& ((currentMap ().isEmpty (allLocs [i]))
					&& currentMap ().isEmptyInteractable (allLocs [i]))) {
				randomLocs.Add (allLocs [i]);

			}
		}

		if (randomLocs.Count > 0) {
			return randomLocs;
		}

		for (int i = 0; i < allLocs.Length; i++) {
			if (currentMap ().isValid (allLocs [i])  && !allLocs [i].Equals (loc) && randomInteractablesOf (allLocs [i]) != null) {
				if (randomInteractablesOf (allLocs [i]).Count > 0) {
					return randomInteractablesOf (allLocs [i]);
				}
			}
		}
		return null;
	}

	public Location openBorderOf (Location loc, Boolean forSameChar, Skill s)
	{

		if (currentMap ().isValid (loc)
			&& (currentMap ().isEmpty (loc) || (forSameChar && currentMap ().objectAt (loc).Equals (this)))
			&& (s == null || currentMap ().canMakeMove (this, loc, s))) {
			return loc;
		}

		Location [] allLocs = new Location [8];

		//WHEN OBJECT IS NORTH
		if (isMostlyNorth (loc)) {
			allLocs [0] = loc.South;
			if (isGenEast (loc)) {
				allLocs [1] = loc.West;
				allLocs [2] = loc.East;
				allLocs [3] = loc.North;
				allLocs [4] = loc.SouthWest;
				allLocs [5] = loc.NorthWest;
				allLocs [6] = loc.SouthEast;
				allLocs [7] = loc.NorthEast;
			} else {
				allLocs [1] = loc.East;
				allLocs [2] = loc.West;
				allLocs [3] = loc.North;
				allLocs [4] = loc.SouthEast;
				allLocs [5] = loc.NorthEast;
				allLocs [6] = loc.SouthWest;
				allLocs [7] = loc.NorthWest;
			}
		}

		//WHEN OBJECT IS SOUTH
		else if (isMostlySouth (loc)) {
			allLocs [0] = loc.North;
			if (isGenEast (loc)) {
				allLocs [1] = loc.West;
				allLocs [2] = loc.East;
				allLocs [3] = loc.South;
				allLocs [4] = loc.NorthWest;
				allLocs [5] = loc.SouthWest;
				allLocs [6] = loc.NorthEast;
				allLocs [7] = loc.SouthEast;
			} else {
				allLocs [1] = loc.East;
				allLocs [2] = loc.West;
				allLocs [3] = loc.South;
				allLocs [4] = loc.NorthEast;
				allLocs [5] = loc.SouthEast;
				allLocs [6] = loc.NorthWest;
				allLocs [7] = loc.SouthWest;
			}
		}

		//WHEN OBJECT IS EAST
		else if (isMostlyEast (loc)) {
			allLocs [0] = loc.West;
			if (isGenNorth (loc)) {
				allLocs [1] = loc.South;
				allLocs [2] = loc.North;
				allLocs [3] = loc.East;
				allLocs [4] = loc.SouthWest;
				allLocs [5] = loc.NorthWest;
				allLocs [6] = loc.SouthEast;
				allLocs [7] = loc.NorthEast;
			} else {
				allLocs [1] = loc.North;
				allLocs [2] = loc.South;
				allLocs [3] = loc.East;
				allLocs [4] = loc.NorthWest;
				allLocs [5] = loc.SouthWest;
				allLocs [6] = loc.NorthEast;
				allLocs [7] = loc.SouthEast;
			}
		}

		//WHEN OBJECT IS WEST
		else if (isMostlyWest (loc)) {
			allLocs [0] = loc.East;
			if (isGenNorth (loc)) {
				allLocs [1] = loc.South;
				allLocs [2] = loc.North;
				allLocs [3] = loc.West;
				allLocs [4] = loc.SouthEast;
				allLocs [5] = loc.NorthEast;
				allLocs [6] = loc.SouthWest;
				allLocs [7] = loc.NorthWest;
			} else {
				allLocs [1] = loc.North;
				allLocs [2] = loc.South;
				allLocs [3] = loc.West;
				allLocs [4] = loc.NorthEast;
				allLocs [5] = loc.SouthEast;
				allLocs [6] = loc.NorthWest;
				allLocs [7] = loc.SouthWest;
			}
		}

		//WHEN OBJECT IS NORTHEAST
		else if (isNorthEast (loc)) {
			allLocs [1] = loc.South;
			allLocs [2] = loc.West;
			allLocs [0] = loc.SouthWest;
			allLocs [3] = loc.SouthEast;
			allLocs [4] = loc.NorthWest;
			allLocs [5] = loc.North;
			allLocs [6] = loc.East;
			allLocs [7] = loc.NorthEast;

		}

		//WHEN IS NORTHWEST
		else if (isNorthWest (loc)) {
			allLocs [0] = loc.SouthEast;
			allLocs [1] = loc.South;
			allLocs [2] = loc.East;
			allLocs [3] = loc.SouthWest;
			allLocs [4] = loc.NorthEast;
			allLocs [5] = loc.North;
			allLocs [6] = loc.West;
			allLocs [7] = loc.NorthWest;
		}

		//WHEN IS SOUTHWEST
		else if (isSouthWest (loc)) {
			allLocs [0] = loc.NorthEast;
			allLocs [1] = loc.North;
			allLocs [2] = loc.East;
			allLocs [3] = loc.NorthWest;
			allLocs [4] = loc.SouthEast;
			allLocs [5] = loc.South;
			allLocs [6] = loc.West;
			allLocs [7] = loc.SouthWest;
		}

		//WHEN IS SOUTHEAST
		else if (isSouthEast (loc)) {
			allLocs [0] = loc.NorthWest;
			allLocs [1] = loc.North;
			allLocs [2] = loc.West;
			allLocs [3] = loc.NorthEast;
			allLocs [4] = loc.SouthWest;
			allLocs [5] = loc.South;
			allLocs [6] = loc.East;
			allLocs [7] = loc.SouthEast;
		} else {

			ArrayList locs = new ArrayList { loc.North, loc.NorthEast,
				loc.East, loc.SouthEast, loc.South,
				loc.SouthWest, loc.West, loc.NorthWest};
			Location rand = null;
			for (int i = 0; i < 8; i++) {
				rand = (Location)locs [R.Next (locs.Count)];
				allLocs [i] = rand;
				locs.Remove (rand);
			}
		}

		for (int i = 0; i < allLocs.Length; i++) {
			if (currentMap ().isValid (allLocs [i])
				&& (currentMap ().isEmpty (allLocs [i]) || (forSameChar && currentMap ().objectAt (allLocs [i]).Equals (this)))
				&& (s == null || currentMap ().canMakeMove (this, allLocs [i], s))) {
				return allLocs [i];

			}
		}

		for (int i = 0; i < allLocs.Length; i++) {
			if (currentMap ().isValid (allLocs [i]) && !allLocs [i].Equals (loc)
			) {
				if (openBorderOf (allLocs [i], true, s) != null) {
					return openBorderOf (allLocs [i], true, s);
				}
			}
		}
		return null;
	}

	public Boolean HasActed
	{
		get {
			return hasActed;
		}
		set {
			hasActed = value;
		}
	}

	public Boolean CanGuard
	{
		get {
			return !NoGuard && (!StateActive (Fury) && !IsDizzied && !wasDizzied);
		}
	}

	public int FallingHeight
	{
		get {
			return fallingHeight;
		}
		set {
			fallingHeight = value;
		}
	}

	public int LandingHeight
	{
		get {
			return landingHeight;
		}
		set {
			landingHeight = value;
		}
	}

	public string ClearStates
	{
		get {
			string output = "";
			for (int i = 0; i < States.Length; i++) {
				if (!States [i].Name.Contains ("Airbor") && !States [i].Name.Contains ("Ground")) {
					output += States [i].ClearStates;
				}
			}
			return output;
		}
	}

	public string ClearStats
	{
		get {
			string output = "";
			if (strength.IsActive) {
				output += strength.ClearVariants;
			} if (grit.IsActive) {
				output += grit.ClearVariants;
			} if (magick.IsActive) {
				output += magick.ClearVariants;
			} if (resistance.IsActive) {
				output += resistance.ClearVariants;
			} if (speed.IsActive) {
				output += speed.ClearVariants;
			} if (dexterity.IsActive) {
				output += dexterity.ClearVariants;
			} if (proration.IsActive) {
				output += proration.ClearVariants;
			} if (movement.IsActive) {
				output += movement.ClearVariants;
			} if (luck.IsActive) {
				output += luck.ClearVariants;
			} if (teamwork.IsActive) {
				output += teamwork.ClearVariants;
			}
			return output;

		}
	}

    public string NameAndTeam
    {
        get
        {
            return Name + @" " + MyTeam.Abbreviation;
        }
    }
}


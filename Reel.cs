using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Reel
{
    private Rigidbody2D body;
    private Locatable owner;
    private Skill guidance;
	private string name;
	private Texture2D frame;
	private ArrayList roll;
    private Boolean hasStarted;
	private Boolean isPlaying;
	private Boolean isLooping;
	private Vector3 position;
	private Boolean isCutscene;
    private Reel last;
    private Reel next;
	private float framerate;
    private float frameTime;
	private int index;
	private Boolean isActive;
	private float fadeTime;
	private string direction;
	private Boolean isAnimated;
    private Boolean inputDependent;
    private string path;
    private string motionType;
    private string endCondition;
    private Vector3 motion;
    private Boolean[] statements;
    private string[] statementNames;
    private Team opposingTeam;
    Player player;
    private BoxCollider2D[][] currentHurtBoxes;
    private BoxCollider2D[][] currentHitBoxes;
    private Vector3[][] currentHitBoxPositions;
    private Vector3[][] currentHurtBoxPositions;
    private string[] hitBoxAnchorTypes;
    private string followUpName;
    private string hitboxInfo;
    private string hurtboxInfo;
    private string soundInfo;
    private string soundTimestamp;
    private BoxCollider2D[] currentHitBox;
    private BoxCollider2D[] currentHurtBox;
    private Vector3[] currentHitBoxPosition;
    private Vector3[] currentHurtBoxPosition;
    private float horizontalDistanceTraveled;
    private float verticalDistanceTraveled;
    private string[] conditions;
    float value1;
    float value2;
    float value3;
    float value4;
    float speed;

    ArrayList sounds;

	public Reel ()
	{
        horizontalDistanceTraveled = 0;
        verticalDistanceTraveled = 0;
        value1 = 0;
        value2 = 0;
        value3 = 0;
        value4 = 0;

		name = "";
		next = null;
        last = null;
		roll = new ArrayList ();
        hasStarted = false;
		isPlaying = false;
		isLooping = false;
		fadeTime = -1.0f;
		position = new Vector3 (0, 0, 0);
		isCutscene = false;
		framerate = -1.0f;
        frameTime = -1.0f;
		index = 0;
		isActive = true;
		string direction;
		isAnimated = false;
        inputDependent = true;
        path = @"";
        motionType = @"";
        opposingTeam = new Team (@"", @"", 1);
        followUpName = @"";
        hitboxInfo = @"";
        hurtboxInfo = @"";
        currentHitBox = null;
        currentHurtBox = null;
        sounds = new ArrayList();
        soundInfo = "";
        soundTimestamp = "";
        endCondition = @"";
        speed = 0;
        //statements = new ArrayList();
	}
    public float Speed
    {
        get {
            return speed;
        }
        set {
            speed = value;
        }
    }

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

    public Boolean collidesWith (Collider2D col)
    {
        for (int i = 0; i < CurrentHitBox.Length; i ++) {
            if (CurrentHitBox [i].IsTouching (col)) {
                return true;
            }
        }
        return false;
    }

    public Boolean isType (string st)
    {
        for (int i = 0; i < conditions.Length; i++) {
            if (conditions[i].Contains (st)) {
                return true;
            }
        }
        return false;
    }

    public float Value1
    {
        get {
            return value1;
        }
        set {
            value1 = value;
        }
    }

    public float Value2
    {
        get
        {
            return value2;
        }
        set
        {
            value2 = value;
        }
    }

    public float Value3
    {
        get
        {
            return value3;
        }
        set
        {
            value3 = value;
        }
    }

    public float Value4
    {
        get
        {
            return value4;
        }
        set
        {
            value4 = value;
        }
    }

    /**statements: 
        CanInput
        IsStanding
        IsAirborne
        IsVulnerable
        InMotion
        CanBlock
        
    */

    public string[] Conditions
    {
        get {
            return conditions;
        }
        set {
            conditions = value;
        }
    }

    public float  VerticalDistanceTraveled
    {
        get {
            return verticalDistanceTraveled;
        }
        set {
            verticalDistanceTraveled = value;
        }
    }

    public float HorizontalDistanceTraveled
    {
        get
        {
            return horizontalDistanceTraveled;
        }
        set
        {
            horizontalDistanceTraveled = value;
        }
    }

    public string EndCondition
    {
        get {
            return endCondition;
        }
        set {
            endCondition = value;
        }
    }

    public string HitboxInfo
    {
        get {
            return hitboxInfo;
        }
        set {
            hitboxInfo = value;
        }
    }

    public string HurtboxInfo
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

    public string MotionType
    {
        get {
            return motionType;
        }
        set {
            motionType = value;
        }
    }

    public BoxCollider2D[][] CurrentHitBoxes
    {
        get {
            return currentHitBoxes;
        }
        set {
            currentHitBoxes = value;
        }
    }

    public BoxCollider2D[][] CurrentHurtBoxes
    {
        get
        {
            return currentHurtBoxes;
        }
        set
        {
            currentHurtBoxes = value;
        }
    }

    public Boolean Object
    {
        get
        {
            return Statements[0];
        }
        set
        {
            Statements[0] = value;
        }
    }


    public Boolean InMotion
    {
        get
        {
            return Statements [7];
        }
        set
        {
            Statements [7] = value;
        }
    }

    public Boolean Vulnerable
    {
        get {
            return Statements [5];
        }
        set {
            Statements [5] = value;
        }
    }

    public Boolean Airborne
    {
        get {
            return Statements [4];
        }
        set {
            Statements [4] = value;
        }
    }

    public Boolean Crouching
    {
        get {
            return Statements [3];
        }
        set {
            Statements [3] = value;
        }
    }
    public Boolean CanAdjustSide
    {
        get {
            return Statements [6];
        }
        set {
            Statements [6] = value;
        }
    }

    public Boolean CanInput
    {
        get {
            return Statements [1];
        }
        set {
            Statements [1] = value;
        }
    }

    public Boolean Standing
    {
        get {
            return Statements [2];
        }
        set {
            Statements [2] = value;
        }
    }

    public Boolean CanChangeDirection
    {
        get {
            return Statements [8];
        }
        set {
            Statements [8] = value;
        }
    }

    public Player MyPlayer
    {
        get {
            return player;
        }   
        set {
            player = value;
        }
    }

    public Boolean InputDependent
    {
        get {
            return inputDependent;
        }
        set {
            inputDependent = value;
        }
    }

	public float Framerate
	{
		get {
			return framerate;
		}
		set {
			framerate = value;
		}
	}

    public float FrameTime
    {
        get
        {
            return frameTime;
        }
        set
        {
            frameTime = value;
        }
    }

	public Boolean IsAnimated
	{
		get {
			return isAnimated;
		}
		set {
			isAnimated = value;
		}
	}

    public Boolean[] Statements
    {
        get {
            return statements;
        }
        set {
            statements = value;
        }
    }

    public string[] StatementNames
    {
        get
        {
            return statementNames;
        }
        set
        {
            statementNames = value;
        }
    }

	public Reel (string nm, string pth, int frames, float rte, Boolean play, Boolean loop, Boolean act,
		float fade, Boolean ani, Boolean cut, Boolean inp, Vector3 pos, Player master)
	{
        hasStarted = false;
		isAnimated = ani;
		index = -1;
		name = nm;
		isActive = act;
		isPlaying = play;
		isLooping = loop;
		isActive = act;
		fadeTime = fade;
		isCutscene = cut;
		position = pos;
        last = null;
		next = null;
        inputDependent = inp;
		roll = new ArrayList ();
        framerate = rte;
        frameTime = framerate;
        path = pth;
        player = master;
        statements = new Boolean[] { false, true, true, false, false, true, true, false, true};
        sounds = new ArrayList();

        Texture2D txt;
		for (int i = 0; i < frames; i++) {
            txt = Resources.Load<Texture2D>(path + "" + (i + 1));
            if (txt == null) {
                throw new NullReferenceException (@"NOICE " + path + (i + 1) );
            }
            roll.Add (txt);
		}
        motionType = @"";
        opposingTeam = new Team(@"", @"", 1);
        followUpName = "";
        currentHitBox = null;
        currentHurtBox = null;
        value1 = 0;
        value2 = 0;
        value3 = 0;
        value4 = 0;
        speed = 0;

	}

    public Boolean HasStarted
    {
        get {
            return hasStarted;
        }
        set {
            hasStarted = value;
        }
    }

    public ArrayList Sounds
    {
        get {
            return sounds;
        }
        set {
            sounds = value;
        }
    }

    public string FollowUpName
    {
        get {
            return followUpName;
        }
        set {
            followUpName = value;
        }
    }

    public Team OpposingTeam
    {
        get {
            return opposingTeam;
        }
        set {
            opposingTeam = value;
        }
    }

    public string Name
    {
        get {
            return name;
        }
        set {
            name = value;
        }
    }

    public string Path
    {
        get
        {
            return path;
        }
        set
        {
            path = value;
        }
    }

	public ArrayList Roll
	{
		get {
			return roll;
		}
		set {
			roll = value;
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

	public int Length
	{
		get {
			return roll.Count;
		}
	}

	public void StepIndex ()
	{
		index ++;
        if (index >= Roll.Count && isLooping) {
            //UnityEngine.Debug.Log (Name + @"SPRITE IS LOOPING");
			index = 0;
		}
	}

    public Boolean IsLooping
    {
        get {
            return isLooping;
        }
        set {
            isLooping = value;
        }
    }

	public void LoadFrame ()
	{
		frame = (Texture2D)roll [index];
	}

	public Texture2D Frame
	{
		get {
			return frame;
		}
		set {
			frame = value;
		}
	}

	public Reel Next
	{
		get {
			return next;
		}
		set {
			if (next != null) {
				isActive = true;
			}
			next = value;
		}
	}

    public Reel Last
    {
        get {
            return last;
        }
        set {
            last = value;
        }
    }

	public static float REELFRAMERATE
	{
		get {
			return 0.2f;
		}
	}

	public void Clear (Boolean act)
	{
		isActive = act;
		roll = new ArrayList ();
        frame = null;
		isLooping = false;
		isPlaying = false;
		isCutscene = false;
		fadeTime = -1.0f;
		Next = null;
	}

	public Boolean IsActive
	{
		get {
			return isActive;
		}
		set {
			isActive = value;
		}
	}

	public Boolean IsCutscene
	{
		get {
			return isCutscene;
		}
		set {
			isCutscene = value;
		}
	}

	public Boolean IsPlaying
	{
		get {
			return isPlaying;
		}
		set {
			isPlaying = value;
		}
	}

	public Boolean CanPlay
	{
		get {

			return Index < Length && IsPlaying;
		}
	}

	public void AddReel (Reel rl)
	{
		if (Next != null) {
			Next.AddReel (rl);
		}
        Next = rl;
		Frame = this.Frame;

	}

	public Boolean NoRemainingAnimations
	{
		get {
			return !IsActive || !IsCutscene;
		}
	}

    public BoxCollider2D[] CurrentHitBox
    {
        get {
            return CurrentHitBox;
        }
        set {
            currentHitBox = value;
        }
    }

    public BoxCollider2D[] CurrentHurtBox
    {
        get {
            return currentHurtBox;
        }
        set {
            currentHurtBox = value;
        }
    }

    public Vector3[][] CurrentHurtBoxPositions
    {
        get
        {
            return currentHurtBoxPositions;
        }
        set
        {
            currentHurtBoxPositions = value;
        }
    }

    public Vector3[][] CurrentHitBoxPositions
    {
        get
        {
            return currentHitBoxPositions;
        }
        set
        {
            currentHitBoxPositions = value;
        }
    }


    public Vector3[] CurrentHurtBoxPosition
    {
        get {
            return currentHurtBoxPosition;
        }
        set {
            currentHurtBoxPosition = value;
        }
    }

    public Vector3[] CurrentHitBoxPosition
    {
        get
        {
            return currentHitBoxPosition;
        }
        set
        {
            currentHitBoxPosition = value;
        }
    }


    public void CheckSpriteStatements ()
    {
        player.IsCrouching = Crouching;
        player.CanInput = CanInput;
        player.CanMoveSprite = CanChangeDirection;
    }

    public void fixDirection ()
    {
        Player plx, character = null;
        float distance = 300000;//Math.Abs(player.MapSprite.transform.position.x + player.MapSprite.transform.position.y) * 5;
        float difference = 0;
        //UnityEngine.Debug.Log (@"Fixing " + player.FirstName + @" - " + player.MyTeam.Name + @" to " + distance + " for size of "
//                              + player.CurrentMap.Roster.Count);

        for (int i = 0; i < player.OpposingTeam.Roster.Count; i++)
        {
            plx = (Player)player.OpposingTeam.Roster[i];

            if (!plx.KOd)
            {
                difference = Math.Abs (//Math.Abs
                                 (plx.MapSprite.transform.position.x + plx.MapSprite.transform.position.y)
                                 - //Math.Abs
                                 (player.MapSprite.transform.position.x + player.MapSprite.transform.position.y)
                                      )
                                 ;
                //UnityEngine.Debug.Log (@"IS " + difference + @" LESS THAN " + distance + @"?");
                if (difference < distance)
                {
                    character = plx;
                    //UnityEngine.Debug.Log(plx.FirstName + @" " + plx.MyTeam.Name + @" YES");
                    distance = Math.Abs(plx.MapSprite.transform.position.x + plx.MapSprite.transform.position.y)
                                   - Math.Abs(player.MapSprite.transform.position.x + player.MapSprite.transform.position.y);
                }
                else
                {
                    //UnityEngine.Debug.Log(plx.FirstName + @" " + plx.MyTeam.Name + @" NO, IT IS NOT");
                }
            }
            else
            {
                //UnityEngine.Debug.Log(plx.FirstName + @" " + plx.MyTeam.Name + @" IS ON THE SAME TEAM!");
            }
        }

        if (character != null)
        {
            string oldDirection = player.Direction;
            if (player.MapSprite.transform.position.x > character.MapSprite.transform.position.x)
            {
                player.Direction = "W";
                player.MapSprite.GetComponent<RectTransform>().rotation = new Quaternion(0, 180, 0, 0);
                //player.HurtBox.GetComponent<RectTransform>().rotation = new Quaternion(0, 180, 0, 0);
                //player.HitBox.GetComponent<RectTransform>().rotation = new Quaternion(0, 180, 0, 0);
                //UnityEngine.Debug.Log ("FLIP, FLIP, FLIP! " + player.MyTeam.Name); 
                // .y = 180;
            }
            else
            {
                player.Direction = "E";
                player.MapSprite.GetComponent<RectTransform>().rotation = new Quaternion(0, 0, 0, 0);// .y = 180;
                //player.HurtBox.GetComponent<RectTransform>().rotation = new Quaternion(0, 0, 0, 0);
                //player.HitBox.GetComponent<RectTransform>().rotation = new Quaternion(0, 0, 0, 0);
                //UnityEngine.Debug.Log ("CHANGE, CHANGE, CHANGE! " + player.MyTeam.Name); 
            }
        }
    }

    public void adjustPlayerSpriteBasedOnScale (Player p, RawImage sprite)
    {
        if (sprite == null) {
            throw new NullReferenceException (@"NULL!");
        }

        if (sprite.texture == null) {
            throw new NullReferenceException(@"NULL!");
        }
        sprite.GetComponent<RectTransform>().sizeDelta = new Vector2(sprite.texture.width / p.SSR, sprite.texture.height / p.SSR);
        //adjustShadow(p);

    }


}



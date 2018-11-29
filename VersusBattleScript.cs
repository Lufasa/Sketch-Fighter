using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.IO;
using UnityEngine.Networking;

public class VersusBattleScript : MonoBehaviour {

    public RawImage screenLeftBoundPosition;
    public RawImage screenRightBoundPosition;
    public RawImage screenBottomBoundPosition;
    public RawImage screenTopBoundPosition;
    public RawImage stageLeftBoundPosition;
    public RawImage stageRightBoundPosition;
    public RawImage stageBottomBoundPosition;
    public RawImage stageTopBoundPosition;
    public RawImage playerGroundBoundPosition;

    private BoxCollider2D screenLeftBound;
    private BoxCollider2D screenRightBound;
    private BoxCollider2D screenBottomBound;
    private BoxCollider2D screenTopBound;
    private BoxCollider2D stageLeftBound;
    private BoxCollider2D stageRightBound;
    private BoxCollider2D stageBottomBound;
    private BoxCollider2D stageTopBound;
    private BoxCollider2D playerGroundBound;

    private ArrayList allObjects;
    private ArrayList allHurtBoxes;
    private ArrayList allHitBoxes;

    public Text player1DebugInfo;
    public Text player2DebugInfo;
    public Text player3DebugInfo;
    public Text player4DebugInfo;


    public Player player;

    public int numRounds;
    public int currentRound;


    public Canvas gameCanvas;
    public Color healthColorMax;
    public Color healthColor;
    public Color healthColorLow;
    public Color healthColorCriticalA;
    public Color healthColorCriticalB;
    public Color healthColorLastStandA; 
    public Color healthColorLastStandB; 

    public Color healthColorMaxPoison;
    public Color healthColorPoison;
    public Color healthColorLowPoison;
    public Color healthColorCriticalPoisonA;
    public Color healthColorCriticalPoisonB;
    public Color healthColorLastStandPoisonA;
    public Color healthColorLastStandPoisonB;

    public Color vitalityBlockColor1A;
    public Color vitalityBlockColor1B;
    public Color vitalityBlockColor1C;
    public Color vitalityBlockColor1D;
    public Color vitalityBlockColor2A;
    public Color vitalityBlockColor2B;
    public Color vitalityBlockColor2C;
    public Color vitalityBlockColor2D;
    public Color vitalityBlockColor3A;
    public Color vitalityBlockColor3B;
    public Color vitalityBlockColor3C;
    public Color vitalityBlockColor3D;
    public Color vitalityBlockColorBackgroundA;
    public Color vitalityBlockColorBackgroundB;

    public Camera mainCamera;
    public AudioSource announcer;
    public AudioSource music;
    public AudioSource ambience;
    public Boolean paused;

    public RawImage vitalityBlockBig1;
    public RawImage vitalityBlockBig2;
    public RawImage vitalityBlockSmall1;
    public RawImage vitalityBlockSmall2;

    //PAUSE MENU
    public RawImage pauseBox;
    public RawImage pauseBoxArrows;
    public Text continueText;
    public Text moveListText;
    public Text characterSelectText;
    public Text mainMenuText;

    public RawImage confirmBox;
    public RawImage confirmBoxArrows;
    public Text yesText;
    public Text noText;

    //SCREEN EFFECTS
    public RawImage stageWeather;
    public RawImage stageCondition;
    public RawImage stageForeground1;
    public RawImage stageBackground1;
    public RawImage stageBackground2;
    public RawImage stageBackground3;
    public RawImage stageBackground4;

    private Map battleMap;
    public Team team1;
    public Team team2;

    public RawImage team1Wins1;
    public RawImage team1Wins2;
    public RawImage team1Wins3;
    public RawImage team2Wins1;
    public RawImage team2Wins2;
    public RawImage team2Wins3;

    public ArrayList players;

    //PLAYER 1
    public Player player1;
    public RawImage player1Sprite;
    public Vector3 player1StartingPosition;
    public Text player1Name;
    public RawImage player1Portrait;

    public RawImage player1HealthBarBackground;
    public RawImage player1HealthBar;
    public RawImage player1HealthBarRecover;

    public RawImage player1Icon1;
    public RawImage player1Icon2;
    public RawImage player1Icon3;
    public ArrayList player1Icons;
    public ArrayList player1IconTimes;

    public Text player1Icon1Text;
    public Text player1Icon2Text;
    public Text player1Icon3Text;
    public ArrayList player1IconTexts;

    public RawImage player1VitalityBarBackground;
    public RawImage player1VitalityBarBlock1;
    public RawImage player1VitalityBarBlock2;
    public RawImage player1VitalityBarBlock3;

    public RawImage player1GuardBarBackground;
    public RawImage player1GuardBar;
    public RawImage player1StunBarBackground;
    public RawImage player1StunBar;

    public Text player1Stock;

    public float player1VitalityBarTimer;

    private float player1HealthBarTimer;

    private ArrayList player1Inputs;

    //PLAYER 2
    public Player player2;
    public RawImage player2Sprite;
    public Vector3 player2StartingPosition;
    public Text player2Name;
    public RawImage player2Portrait;

    public RawImage player2HealthBarBackground;
    public RawImage player2HealthBar;
    public RawImage player2HealthBarRecover;

    public RawImage player2Icon1;
    public RawImage player2Icon2;
    public RawImage player2Icon3;
    public ArrayList player2Icons;
    public ArrayList player2IconTimes;

    public Text player2Icon1Text;
    public Text player2Icon2Text;
    public Text player2Icon3Text;
    public ArrayList player2IconTexts; 

    public RawImage player2VitalityBarBackground;
    public RawImage player2VitalityBarBlock1;
    public RawImage player2VitalityBarBlock2;
    public RawImage player2VitalityBarBlock3;

    public RawImage player2GuardBarBackground;
    public RawImage player2GuardBar;
    public RawImage player2StunBarBackground;
    public RawImage player2StunBar;

    public Text player2Stock;

    public float player2VitalityBarTimer;

    private float player2HealthBarTimer;

    private ArrayList player2Inputs;

    //PLAYER 3
    public Player player3;
    public RawImage player3Sprite;
    public Vector3 player3StartingPosition;
    public Text player3Name;
    public RawImage player3Portrait;

    public RawImage player3HealthBarBackground;
    public RawImage player3HealthBar;
    public RawImage player3HealthBarRecover;

    public RawImage player3Icon1;
    public RawImage player3Icon2;
    public RawImage player3Icon3;
    public ArrayList player3Icons;
    public ArrayList player3IconTimes;

    public Text player3Icon1Text;
    public Text player3Icon2Text;
    public Text player3Icon3Text;
    public ArrayList player3IconTexts;

    public RawImage player3VitalityBarBackground;
    public RawImage player3VitalityBarBlock1;
    public RawImage player3VitalityBarBlock2;
    public RawImage player3VitalityBarBlock3;

    public RawImage player3GuardBarBackground;
    public RawImage player3GuardBar;
    public RawImage player3StunBarBackground;
    public RawImage player3StunBar;

    public Text player3Stock;

    public float player3VitalityBarTimer;

    private float player3HealthBarTimer;

    private ArrayList player3Inputs;

    //PLAYER 4
    public Player player4;
    public RawImage player4Sprite;
    public Vector3 player4StartingPosition;
    public Text player4Name;
    public RawImage player4Portrait;

    public RawImage player4HealthBarBackground;
    public RawImage player4HealthBar;
    public RawImage player4HealthBarRecover;

    public RawImage player4Icon1;
    public RawImage player4Icon2;
    public RawImage player4Icon3;
    public ArrayList player4Icons;
    public ArrayList player4IconTimes;

    public Text player4Icon1Text;
    public Text player4Icon2Text;
    public Text player4Icon3Text;
    public ArrayList player4IconTexts;

    public RawImage player4VitalityBarBackground;
    public RawImage player4VitalityBarBlock1;
    public RawImage player4VitalityBarBlock2;
    public RawImage player4VitalityBarBlock3;

    public RawImage player4GuardBarBackground;
    public RawImage player4GuardBar;
    public RawImage player4StunBarBackground;
    public RawImage player4StunBar;

    public Text player4Stock;

    public float player4VitalityBarTimer;

    private float player4HealthBarTimer;

    private ArrayList player4Inputs;

    public float TEAMNOTIFICATIONTIME;
    public float notificationTime;

    //TEAM 1
    public Text team1Notification; 
    public Text team1ComboNotification;
    public ArrayList team1Notifications;
    public ArrayList team1NotificationTimes;
    public RawImage[] t1NumWins;

    //TEAM 2
    public Text team2Notification;
    public Text team2ComboNotification;
    public ArrayList team2Notifications;
    public ArrayList team2NotificationTimes;
    public RawImage[] t2NumWins;

    //ALL SCREEN
    public Text roundNotification;
    public RawImage roundNotificationBackground;
    public float roundNotificationTimer;
    public Text matchTime;
    public float matchTimeRemaining;
    public string lastInput;

    public float decisionTime;
    public Boolean matchOver;
    public Boolean matchStarted;

    public float matchSpeed;

    public float SPRITESCALERATIO;

    public Boolean inDecision;

    //CONTROLS
    public KeyCode player1Up,
    player1Down,
    player1Left,
    player1Right,
    player1A,
    player1B,
    player1C,
    player1D,
    player1Y,
    player1Z,
    player1Pause,
    player1Confirm,
    player1Cancel;

    public KeyCode player2Up,
    player2Down,
    player2Left,
    player2Right,
    player2A,
    player2B,
    player2C,
    player2D,
    player2Y,
    player2Z,
    player2Pause,
    player2Confirm,
    player2Cancel;

    public KeyCode player3Up,
    player3Down,
    player3Left,
    player3Right,
    player3A,
    player3B,
    player3C,
    player3D,
    player3Y,
    player3Z,
    player3Pause,
    player3Confirm,
    player3Cancel;

    public KeyCode player4Up,
    player4Down,
    player4Left,
    player4Right,
    player4A,
    player4B,
    player4C,
    player4D,
    player4Y,
    player4Z,
    player4Pause,
    player4Confirm,
    player4Cancel;

    // Use this for initialization
    void Start () {

        team1Notification.text = @"";
        team2Notification.text = @"";

        player1Icon1.enabled = false;
        player1Icon2.enabled = false;
        player1Icon3.enabled = false;

        player1Icon1Text.enabled = false;
        player1Icon2Text.enabled = false;
        player1Icon3Text.enabled = false;

        player2Icon1.enabled = false;
        player2Icon2.enabled = false;
        player2Icon3.enabled = false;

        player2Icon1Text.enabled = false;
        player2Icon2Text.enabled = false;
        player2Icon3Text.enabled = false;

        player3Icon1.enabled = false;
        player3Icon2.enabled = false;
        player3Icon3.enabled = false;

        player3Icon1Text.enabled = false;
        player3Icon2Text.enabled = false;
        player3Icon3Text.enabled = false;

        player4Icon1.enabled = false;
        player4Icon2.enabled = false;
        player4Icon3.enabled = false;

        player4Icon1Text.enabled = false;
        player4Icon2Text.enabled = false;
        player4Icon3Text.enabled = false;

        team1ComboNotification.enabled = false;
        team2ComboNotification.enabled = false;

        inDecision = false;
        numRounds = 1;
        currentRound = 0;

        Text[] playerDebugText = new Text[4];
        playerDebugText[0] = player1DebugInfo;
        playerDebugText[1] = player2DebugInfo;
        playerDebugText[2] = player3DebugInfo;
        playerDebugText[3] = player4DebugInfo;

        decisionTime = -1.0f;
        matchOver = false;
        matchSpeed = 1.0f;
        notificationTime = 0f;
        TEAMNOTIFICATIONTIME = 2.5f;
        SPRITESCALERATIO = 2.5f;

        players = new ArrayList ();

        player1StartingPosition = player1Sprite.transform.position;
        player2StartingPosition = player2Sprite.transform.position;
        player3StartingPosition = player3Sprite.transform.position;
        player4StartingPosition = player4Sprite.transform.position;

        player1 = null;
        player2 = null;
        player3 = null;
        player4 = null;

        //player1Sprite = null;
        //player2Sprite = null;
        //player3Sprite = null;
        //player4Sprite = null;

        player1HealthBarTimer = 0.0f;
        player2HealthBarTimer = 0.0f;
        player3HealthBarTimer = 0.0f;
        player4HealthBarTimer = 0.0f;

        player1VitalityBarTimer = 0.0f;
        player2VitalityBarTimer = 0.0f;
        player3VitalityBarTimer = 0.0f;
        player4VitalityBarTimer = 0.0f;

        battleMap = new DataReader().loadMapForBattle ();

        StreamReader reader = new StreamReader (@"Assets/Resources/Data/Current/Team1Versus.txt");
        Player pl; 

        team1 = new Team (@"Team 1", "1", 2);
        team2 = new Team (@"Team 2", "2", 2);

        t1NumWins = new RawImage [3];
        t2NumWins = new RawImage [3];

        t1NumWins [0] = team1Wins1;
        t1NumWins [1] = team1Wins2;
        t1NumWins [2] = team1Wins3;

        t2NumWins [0] = team2Wins1;
        t2NumWins [1] = team2Wins2;
        t2NumWins [2] = team2Wins3;

        string p1 = reader.ReadLine ();
        /**
        player1 = new DataReader ().loadTeamMember (team1,
                                                    p1.Substring (0, p1.IndexOf (':')),
                                                    p1.Substring (p1.IndexOf (':') + 1, 1),
                                                    NumberConverter.ConvertToInt (p1.Substring (p1.IndexOf (':') + 2, 1)));
                                                    */
        player1 = new DataReader().loadPlayer (p1, @"Player", true, true, false);
                                   //loadSavedPlayer (p1, @"Assets/Resources/Data/Current/Player1.txt");
        player1.Index = 1;
        setPlayerGraphicsAndAudio (player1, player1Sprite, player1HealthBar, team1, 1);
        players.Add (player1);
        team1.addPlayer (player1);
        player1Sprite.enabled = true;
        player1.changeLocation (new Location (0, 0));
        battleMap.addPlayer (player1);
        player1.CurrentMap = battleMap;

        p1 = reader.ReadLine ();
        if (!p1.Equals ("none")) {
            /**
            player3 = new DataReader ().loadTeamMember (team1,
                                                      p1.Substring (0, p1.IndexOf (':')),
                                                      p1.Substring (p1.IndexOf (':') + 1, 1),
                                                      NumberConverter.ConvertToInt (p1.Substring (p1.IndexOf (':') + 2, 1)));
                                                      */
            player3 = new DataReader().loadPlayer (p1, @"Player", true, true, false);
                                      //loadSavedPlayer (p1, @"Assets/Resources/Data/Current/Player3.txt");
            player3.Index = 3;
            setPlayerGraphicsAndAudio (player3, player3Sprite, player3HealthBar, team1, 3);
            team1.addPlayer (player3);
            player3Sprite.enabled = true;
            player3.changeLocation (new Location (1, 1));
            battleMap.addPlayer(player3);
            player3.CurrentMap = battleMap;
        } else {
            player3Sprite.enabled = false;
        }
        reader.Close ();

        reader = new StreamReader(@"Assets/Resources/Data/Current/Team2Versus.txt");
        p1 = reader.ReadLine ();
        /**
        player2 = new DataReader ().loadTeamMember (team2,
                                                   p1.Substring (0, p1.IndexOf (':')),
                                                   p1.Substring (p1.IndexOf (':') + 1, 1),
                                                   NumberConverter.ConvertToInt (p1.Substring (p1.IndexOf (':') + 2, 1)));
                                                   */
        player2 = new DataReader().loadPlayer (p1, @"Player", true, true, false);
                                  //loadSavedPlayer (p1, @"Assets/Resources/Data/Current/Player2.txt");
        player2.Index = 2;
        setPlayerGraphicsAndAudio (player2, player2Sprite, player2HealthBar, team2, 2);
        players.Add (player2);
        team2.addPlayer (player2);
        player2Sprite.enabled = true;
        player2.changeLocation (new Location(2, 2));
        battleMap.addPlayer (player2);
        player2.CurrentMap = battleMap;

        if (player3 != null) {
            players.Add(player3);
        }

        p1 = reader.ReadLine ();
        if (!p1.Equals ("none")) {
        /**
            player4 = new DataReader ().loadTeamMember (team2,
                                                       p1.Substring (0, p1.IndexOf (':')),
                                                       p1.Substring (p1.IndexOf (':') + 1, 1),
                                                       NumberConverter.ConvertToInt (p1.Substring (p1.IndexOf(':') + 2, 1)));
                                                       */
            player4 = new DataReader ().loadPlayer (p1, @"Player", true, true, false);
                                      //loadSavedPlayer (p1, @"Assets/Resources/Data/Current/Player4.txt");
            player4.Index = 4;
            setPlayerGraphicsAndAudio (player4, player4Sprite, player4HealthBar, team2, 4);
            players.Add (player4);
            team2.addPlayer (player4);
            player4Sprite.enabled = true;
            player4.changeLocation (new Location (3, 3));
            battleMap.addPlayer (player4);
            player4.CurrentMap = battleMap;
        } else {
            player4Sprite.enabled = false;
        }
        reader.Close ();

        ArrayList p1Inputs = new ArrayList () { player1Up, player1Down, player1Left, player1Right,
            player1A, player1B, player1C, player1D, player1Y, player1Z,
            player1Pause, player1Confirm, player1Cancel},
        p2Inputs = new ArrayList () { player2Up, player2Down, player2Left, player2Right,
            player2A, player2B, player2C, player2D, player2Y, player2Z,
            player2Pause, player2Confirm, player2Cancel},
        p3Inputs = new ArrayList () { player3Up, player3Down, player3Left, player3Right,
            player3A, player3B, player3C, player3D, player3Y, player3Z,
            player3Pause, player3Confirm, player3Cancel},
        p4Inputs = new ArrayList () { player4Up, player4Down, player4Left, player4Right,
            player4A, player4B, player4C, player4D, player4Y, player4Z,
            player4Pause, player4Confirm, player4Cancel};
        ArrayList pInputs = new ArrayList () {p1Inputs, p2Inputs, p3Inputs, p4Inputs};

        battleMap.Team1 = team1;
        battleMap.Team2 = team2;

        print(player1.CurrentMap.Name + " - " + player1.MyTeam.Name);
        print(player2.CurrentMap.Name + " - " + player2.MyTeam.Name);

        for (int i = 0; i < battleMap.Team1.Roster.Count; i ++) {
            battleMap.Team1 [i].OpposingTeam = battleMap.Team2;
        }

        for (int i = 0; i < battleMap.Team2.Roster.Count; i++)
        {
            battleMap.Team2 [i].OpposingTeam = battleMap.Team1;

        }
        /**
        public BoxCollider2D screenLeftBound;
        public BoxCollider2D screenRightBound;
        public BoxCollider2D stageLeftBound;
        public BoxCollider2D stageRightBound;
        public BoxCollider2D screenBottomBound;
        public BoxCollider2D stageBottomBound;
        public BoxCollider2D stageTopBound;
        public BoxCollider2D screenTopBound;
        public BoxCollider2D playerGroundBound;
        */
        screenLeftBound = screenLeftBoundPosition.GetComponent <BoxCollider2D> ();

        screenRightBound = screenRightBoundPosition.GetComponent<BoxCollider2D> ();

        screenBottomBound = screenBottomBoundPosition.GetComponent<BoxCollider2D> ();

        screenTopBound = screenTopBoundPosition.GetComponent<BoxCollider2D>();

        stageLeftBound = stageLeftBoundPosition.GetComponent<BoxCollider2D>();

        stageRightBound = stageRightBoundPosition.GetComponent<BoxCollider2D>();

        stageBottomBound = stageBottomBoundPosition.GetComponent<BoxCollider2D>();

        stageTopBound = stageTopBoundPosition.GetComponent<BoxCollider2D>();

        playerGroundBound = playerGroundBoundPosition.GetComponent<BoxCollider2D>();

        allObjects = new ArrayList ();

        for (int i = 0; i < players.Count; i++) {
            pl = ((Player)players[i]);
            pl.CurrentMap = battleMap;
            pl.Vitality.MeterLevel = 0;
            pl.setInputs ((ArrayList)pInputs [i]);
            pl.SetReel (pl.NeutralSprite);
            pl.SpriteInfo = playerDebugText[i];
           // pl.MapSprite.transform.position = new Vector3(
             //   pl.MapSprite.transform.position.x,
             //   playerGroundBound.transform.position.y,
             //   pl.MapSprite.transform.position.x);
            pl.GroundBound = playerGroundBound.transform.position.y;
            allObjects.Add (pl);
            InputReader.setSkillInputs (pl);
        }

        mainCamera = mainCamera.GetComponent<Camera> ();

        paused = false;

        lastInput = @"";

        beginRound ();
    }

    // Update is called once per frame
    void Update()
    {

        if (paused)
        {

        }
        else
        {

            //TEST
            if (matchTimeRemaining > 100)
            {
                player1.Health.MeterLevel--;
                player2.Health.MeterLevel--;

                player1.Vitality.MeterLevel++;
                player2.Vitality.MeterLevel++;
            }


            //UPDATE TEAM NOTIFICATIONS

            //UPDATE ROUND NOTIFICATION
            if (roundNotificationTimer > 0)
            {
                roundNotificationTimer -= Time.deltaTime;
                if (roundNotificationTimer < 0 && !matchStarted)
                {
                    roundNotification.text = "FIGHT!";
                    matchStarted = true;
                }
            }

            //ROUND NOTIFICATION ALPHA
            if (roundNotificationTimer <= 0 && !matchOver && roundNotification.color.a > 0)
            {
                roundNotification.color = new Color(roundNotification.color.r, roundNotification.color.g, roundNotification.color.b,
                                                     roundNotification.color.a - 0.05f);
                roundNotificationBackground.color = new Color(roundNotification.color.r, roundNotification.color.g, roundNotification.color.b,
                                                     roundNotificationBackground.color.a - 0.05f);
            }

            //UPDATE DECISION TIME
            if (!AnimationsActive && inDecision)
            {
                decisionTime -= Time.deltaTime;
                ///print(@"Decision time: " + decisionTime);
                if (decisionTime <= 0.0f)
                {
                    decideMatch();
                }
            }

            for (int i = 0; i < players.Count; i ++) {

                player = (Player)players[i];
                player.updateReel ();

            }

            Skill sk = null;

            adjustScreen ();

            //INCREMENT MATCH TIME
            if (matchStarted) {
                matchTimeRemaining -= Time.deltaTime;

                Boolean hasPressedInput = false;
                ArrayList inpt;
                //PLAYER
                for (int i = 0; i < players.Count; i++) {
                    player = (Player)players[i];
                    player.CurrentInput = new ArrayList ();

                    inpt = new ArrayList ();
                    int numInputs = 0;

                    UnityEngine.Debug.Log(@"Inputs this round: " + inpt.Count);

                    //A
                    if (Input.GetKey (player.InputA)) {
                        print ("A Entered");
                        numInputs++;
                        inpt.Add (player.InputA);
                    }

                    //B
                    if (Input.GetKey (player.InputB))
                    {
                        print ("B Entered");
                        numInputs++;
                        inpt.Add (player.InputB);
                    }

                    //C
                    if (Input.GetKey (player.InputC))
                    {
                        print ("C Entered");
                        numInputs++;
                        inpt.Add (player.InputC);
                    }

                    //D
                    if (Input.GetKey (player.InputD))
                    {
                        print ("D Entered");
                        numInputs++;
                        inpt.Add (player.InputD);
                    }

                    //Y
                    if (Input.GetKey (player.InputY))
                    {
                        print ("Y Entered");
                        numInputs++;
                        inpt.Add (player.InputY);
                    }

                    //Z
                    if (Input.GetKey (player.InputZ))
                    {
                        print ("Z Entered");
                        numInputs++;
                        inpt.Add (player.InputZ);
                    }

                    //Up
                    if (Input.GetKey (player.InputUp))
                    {
                        print ("Up Entered");
                        numInputs++;
                        inpt.Add (player.InputUp);
                    }

                    //Right
                    if (Input.GetKey (player.InputRight))
                    {
                        print ("Right Entered");
                        numInputs++;
                        inpt.Add (player.InputRight);
                    }

                    //Down
                    if (Input.GetKey (player.InputDown))
                    {
                        print ("Down Entered");
                        numInputs++;
                        inpt.Add (player.InputDown);
                    }

                    //Left
                    if (Input.GetKey (player.InputLeft))
                    {
                        print ("Left Entered");
                        numInputs++;
                        inpt.Add (player.InputLeft);
                    }

                    //PAUSE
                    if (Input.GetKey (player.InputPause)) {
                        print ("Pause Entered");
                        numInputs++;
                        inpt.Add (player.InputPause);
                    }

                    //CONFIRM
                    if (Input.GetKey (player.InputConfirm)) {
                        print ("Confirm Entered");
                        numInputs++;
                        inpt.Add (player.InputConfirm);
                    }

                    //CANCEL
                    if (Input.GetKey (player.InputCancel)) {
                        print("Cancel Entered");
                        numInputs++;
                        inpt.Add (player.InputCancel);
                    }


                    UnityEngine.Debug.Log(@"Inpt count = " + inpt.Count);

                    player.ReadInput (inpt);
                    player.Inputs.Add (player.CheckedInput);

                    UnityEngine.Debug.Log (@"Input this round for " + player.Index + ": " + player.CheckedInputString + " " + player.CurrentInput.Count);

                    if (!matchOver && decisionTime > 0) {
                        if (matchStarted && !matchOver) {

                            if (player.ActiveSkill == null) {
                                player.ActiveSkill = InputReader.readInput(player);
                            }
                            if (player.ActiveSkill != null) {
                                print (player.ActiveSkill.Name + @" IS PRESENT ACTIVE SKILL");
                            }

                            //JUMP
                            if (player.Input(@"Up"))
                            {
                                if (player.MyReel.CanChangeDirection)
                                {
                                    player.SetReel(player.JumpStartSprite);
                                    if (player.Input (@"Forward")) {
                                        player.JumpStartSprite.Next = player.JumpForwardSprite;
                                    } else if (player.Input (@"Back")) {
                                        player.JumpStartSprite.Next = player.JumpBackSprite;
                                    } else {
                                        player.JumpStartSprite.Next = player.JumpNeutralSprite;
                                    }
                                }
                            }

                            //WALKING FORWARD
                            else if (player.Input ("Forward"))
                            {
                                if (!player.MyReel.Equals(player.WalkForwardSprite) && player.MyReel.CanChangeDirection) {
                                    player.SetReel (player.WalkForwardSprite);
                                }
                                if (player.MyReel.CanChangeDirection) {
                                    if (!player.MyReel.Equals (player.WalkForwardSprite)) {
                                        player.SetReel(player.WalkForwardSprite);
                                    }
                                }
                            }
                            //WALKING BACK
                            else if (player.Input ("Back")) {
                                if (player.MyReel.CanChangeDirection) {
                                    if (!player.MyReel.Equals(player.WalkBackSprite)) {
                                        player.SetReel(player.WalkBackSprite);
                                    }
                                }
                            }
                            else if (player.Input ("Down")) {
                                if (player.IsStanding && player.MyReel.CanChangeDirection
                                    && !player.MyReel.Name.Contains ("Crouch")) {
                                    player.SetReel (player.CrouchingSprite);
                                }
                                else if (player.MyReel.Equals (player.StandingSprite)) {
                                    player.SetReel (player.CrouchingSprite, (player.CrouchingSprite.Roll.Count - player.MyReel.Index) - 1);
                                }
                            }
                            //NEUTRAL
                            if (player.EmptyInput) {
                                if (player.MyReel.Equals (player.CrouchedSprite)) {
                                    player.SetReel (player.StandingSprite);
                                }
                                else if (player.MyReel.Equals (player.CrouchingSprite)) {
                                    player.SetReel(player.StandingSprite, (player.StandingSprite.Roll.Count - player.MyReel.Index) - 1);
                                }
                                else if (!player.MyReel.Equals (player.NeutralSprite)
                                         && !player.MyReel.Equals (player.StandingSprite)
                                         && player.MyReel.CanChangeDirection) {
                                    player.SetReel (player.NeutralSprite); 
                                }
                            }
                        }
                    }

                    player.CheckMovement ();

                    /**
                    player.checkCollisions (playerGroundBound.transform.position.y,
                       screenLeftBound.transform.position.x,
                       screenRightBound.transform.position.x, allObjects);
                    */
                }
            }



            //UPDATE PLAYER HP AND VM
            updatePlayer(player1, player1Name, player1Portrait, player1HealthBarBackground, player1HealthBar, player1HealthBarRecover,
                          player1Stock, player1VitalityBarBackground, vitalityBlockBig1, player1VitalityBarBlock1, player1VitalityBarBlock2, player1VitalityBarBlock3,
                          player1GuardBarBackground, player1GuardBar, player1StunBar, player1StunBarBackground,
                          ref player1HealthBarTimer, ref player1VitalityBarTimer, 0.6f);

            updatePlayer(player3, player3Name, player3Portrait, player3HealthBarBackground, player3HealthBar, player3HealthBarRecover,
                          player3Stock, player3VitalityBarBackground, vitalityBlockSmall1, player3VitalityBarBlock1, player3VitalityBarBlock2, player3VitalityBarBlock3,
                          player3GuardBarBackground, player3GuardBar, player3StunBar, player3StunBarBackground,
                          ref player3HealthBarTimer, ref player2VitalityBarTimer, 0.6f);

            updatePlayer(player2, player2Name, player2Portrait, player2HealthBarBackground, player2HealthBar, player2HealthBarRecover,
                          player2Stock, player2VitalityBarBackground, vitalityBlockBig2, player2VitalityBarBlock1, player2VitalityBarBlock2, player2VitalityBarBlock3,
                          player2GuardBarBackground, player2GuardBar, player2StunBar, player2StunBarBackground,
                          ref player2HealthBarTimer, ref player3VitalityBarTimer, 0.6f);

            updatePlayer(player4, player4Name, player4Portrait, player4HealthBarBackground, player4HealthBar, player4HealthBarRecover,
                          player4Stock, player4VitalityBarBackground, vitalityBlockSmall2, player4VitalityBarBlock1, player4VitalityBarBlock2, player4VitalityBarBlock3,
                          player4GuardBarBackground, player4GuardBar, player4StunBar, player4StunBarBackground,
                          ref player4HealthBarTimer, ref player4VitalityBarTimer, 0.6f);

            if (matchTimeRemaining <= 0.0f && !inDecision && !matchOver)
            {
                inDecision = true;
                roundNotification.enabled = true;
                roundNotificationBackground.enabled = true;
                roundNotification.color = Color.white;
                roundNotificationBackground.color = Color.clear;
                roundNotification.text = "TIME OVER";
                decisionTime = 3.0f;
            }

            if (matchTimeRemaining > 0)
            {
                matchTime.text = @"" + (int)matchTimeRemaining;
            }

            /**
            if (!matchOver || !(decisionTime >= 0.0f))
            {
                //UPDATE PLAYER SPRITES
                updateSprite (player1, player1Sprite);
                updateSprite (player2, player2Sprite);
                updateSprite (player3, player3Sprite);
                updateSprite (player4, player4Sprite);
            }
            */
        }

        //PLAYER INPUT SECTION

    }

    public void beginRound ()
    {
        //roundNotification.enabled = true;
        //roundNotificationBackground.enabled = true;
        inDecision = false;
        currentRound ++;
        setRoundNotification (@"ROUND " + currentRound, 3.0f, roundNotification.color);
        //player1Sprite.transform.position = player1StartingPosition;
        //player2Sprite.transform.position = player2StartingPosition;
        if (player3 != null) {
          //  player3Sprite.transform.position = player3StartingPosition;
        } if (player4 != null) {
          //  player4Sprite.transform.position = player4StartingPosition;
        }
        matchStarted = false;

        setWins (battleMap.Team1, t1NumWins);
        setWins (battleMap.Team2, t2NumWins); 
    }

    public void setWins (Team team, RawImage[] winIcons)
    {
        int i = 0;
        while (i < team.RoundsWon) {
            winIcons [i].enabled = true;
            i ++;
        }

        while (i < winIcons.Length) {
            winIcons [i].enabled = false;
            i ++;
        }
    }

    public void adjustScreen ()
    {
        //mainCamera.transform.position = new Vector3 (mainCamera.transform.position.x + 1, mainCamera.transform.position.y,
          //                                           mainCamera.transform.position.z);
    }

    public void setRoundNotification (string text, float time, Color color) {
        roundNotification.color = color;
        //roundNotificationBackground.color = color;
        roundNotification.text = text;
        roundNotificationTimer = time;
        decisionTime = 3.0f;
    }

    public void updatePlayer (Player player, Text name, RawImage portrait, RawImage healthBarBackground, RawImage healthBar,
                          RawImage healthBarRecover, Text stock, RawImage vitalityBlockBackground, RawImage vitalityBlock,
                          RawImage vitalityBlock1, RawImage vitalityBlock2, RawImage vitalityBlock3,
                              RawImage guardBarBackground, RawImage guardBar, RawImage stunBarBackground, RawImage stunBar,
                          ref float healthTimer, ref float vitalityTimer, float FLASHTIMER)
    {

        if (player != null) {
            name.enabled = true;

            portrait.enabled = true;
            portrait.texture = player.Portrait;

            if (player.MyTeam.Name.Contains ("1")) {
                //name.text = player.FirstName + @" " + player.HealthPresent + @"/" + player.Health.MeterMax;
                name.text = player.FirstName + @" " + player.Health.MeterLevel + @"/" + player.Health.MeterMax;
            } else {
                //name.text = player.HealthPresent + @"/" + player.Health.MeterMax + @" " + player.FirstName;
                name.text = player.Health.MeterLevel + @"/" + player.Health.MeterMax + @" " + player.FirstName;
            }

            healthBar.enabled = true;
            healthBarBackground.enabled = true;
            healthBarRecover.enabled = true;

            stunBarBackground.enabled = true;
            stunBar.enabled = true;
            guardBarBackground.enabled = true;
            guardBar.enabled = true;

            if (player.tickHealthMeasure ()) {
                healthBar.GetComponent<RectTransform> ().sizeDelta = new Vector2 ((healthBarBackground.rectTransform.sizeDelta.x * player.HealthPresentRatio), healthBarBackground.rectTransform.sizeDelta.y);
                healthBarRecover.GetComponent<RectTransform> ().sizeDelta = new Vector2 ((healthBarBackground.rectTransform.sizeDelta.x * player.Health.MeterRatio), healthBarBackground.rectTransform.sizeDelta.y);

                if (!player.FlashingHealth) {
                    if (player.HealthPresentRatio == 1) {
                        if (player.StateActive (player.Poison)) {
                            healthBar.color = healthColorMaxPoison;
                        }
                        else {
                            healthBar.color = healthColorMax;
                        }
                    }

                    else if (player.HealthPresentRatio >= 0.5f) {
                        if (player.StateActive (player.Poison)) {
                            healthBar.color = healthColorPoison;
                        }
                        else {
                            healthBar.color = healthColor;
                        }
                    }

                    else {
                        if (player.StateActive (player.Poison)) {
                            healthBar.color = healthColorLowPoison;
                        }
                        else {
                            healthBar.color = healthColorLow;
                        }
                    }
                }
            }

            if (player.FlashingHealth) {
                healthTimer -= Time.deltaTime;
                if (healthTimer < 0.0f) {
                    healthTimer = FLASHTIMER;
                    if (player.LastStand) {
                        if (healthBar.color.Equals (healthColorLastStandA)) {
                            healthBar.color = healthColorLastStandB;
                        } else {
                            healthBar.color = healthColorLastStandA;
                        }
                    } else {
                        if (healthBar.color.Equals (healthColorCriticalA)) {
                            healthBar.color = healthColorCriticalB;
                        }
                        else {
                            healthBar.color = healthColorCriticalA;
                        }
                    }
                }
            }

            vitalityBlock1.enabled = true;
            vitalityBlock2.enabled = true;
            vitalityBlock3.enabled = true;
            vitalityBlockBackground.enabled = true;

            if (player.tickVitalityMeasure ()) {
                int vm = player.VitalityPresent, measure = vm;
                ///print(player.FirstName + @" VM " + vm);

                if (player.VitalityPresent == 600) {
                    vitalityBlock1.GetComponent<RectTransform> ().sizeDelta = vitalityBlock.GetComponent<RectTransform> ().sizeDelta;
                    vitalityBlock2.GetComponent<RectTransform> ().sizeDelta = vitalityBlock.GetComponent<RectTransform> ().sizeDelta;
                    vitalityBlock3.GetComponent<RectTransform> ().sizeDelta = vitalityBlock.GetComponent<RectTransform> ().sizeDelta;
                } else if (player.VitalityPresent < 600 && player.VitalityPresent >= 500) {
                    vitalityBlock1.GetComponent<RectTransform>().sizeDelta = vitalityBlock.GetComponent<RectTransform>().sizeDelta;
                    vitalityBlock2.GetComponent<RectTransform>().sizeDelta = vitalityBlock.GetComponent<RectTransform>().sizeDelta;
                    vitalityBlock3.GetComponent<RectTransform>().sizeDelta = new Vector2((vitalityBlock.rectTransform.sizeDelta.x * ((player.VitalityPresent - 500) / (float)100.0f)), vitalityBlock.rectTransform.sizeDelta.y);
                } else if (player.VitalityPresent < 500 && player.VitalityPresent >= 400) {
                    vitalityBlock1.GetComponent<RectTransform>().sizeDelta = vitalityBlock.GetComponent<RectTransform>().sizeDelta;
                    vitalityBlock2.GetComponent<RectTransform>().sizeDelta = new Vector2((vitalityBlock.rectTransform.sizeDelta.x * ((player.VitalityPresent - 400) / (float)100.0f)), vitalityBlock.rectTransform.sizeDelta.y);
                    vitalityBlock3.GetComponent<RectTransform>().sizeDelta = vitalityBlock.GetComponent<RectTransform>().sizeDelta;
                } else if (player.VitalityPresent < 400 && player.VitalityPresent > 300) {
                    vitalityBlock1.GetComponent<RectTransform>().sizeDelta = new Vector2((vitalityBlock.rectTransform.sizeDelta.x * ((player.VitalityPresent - 300) / (float)100.0f)), vitalityBlock.rectTransform.sizeDelta.y);
                    vitalityBlock2.GetComponent<RectTransform>().sizeDelta = vitalityBlock.GetComponent<RectTransform>().sizeDelta;
                    vitalityBlock3.GetComponent<RectTransform>().sizeDelta = vitalityBlock.GetComponent<RectTransform>().sizeDelta;
                } else if (player.VitalityPresent == 300) {
                    vitalityBlock1.GetComponent<RectTransform>().sizeDelta = vitalityBlock.GetComponent<RectTransform>().sizeDelta;
                    vitalityBlock2.GetComponent<RectTransform>().sizeDelta = vitalityBlock.GetComponent<RectTransform>().sizeDelta;
                    vitalityBlock3.GetComponent<RectTransform>().sizeDelta = vitalityBlock.GetComponent<RectTransform>().sizeDelta;
                } else if (player.VitalityPresent < 300 && player.VitalityPresent >= 200) {
                    vitalityBlock1.GetComponent<RectTransform>().sizeDelta = vitalityBlock.GetComponent<RectTransform>().sizeDelta;
                    vitalityBlock2.GetComponent<RectTransform>().sizeDelta = vitalityBlock.GetComponent<RectTransform>().sizeDelta;
                    vitalityBlock3.GetComponent<RectTransform>().sizeDelta = new Vector2((vitalityBlock.rectTransform.sizeDelta.x * ((player.VitalityPresent - 200) / (float)100.0f)), vitalityBlock.rectTransform.sizeDelta.y);
                } else if (player.VitalityPresent < 200 && player.VitalityPresent >= 100) {
                    vitalityBlock1.GetComponent<RectTransform>().sizeDelta = vitalityBlock.GetComponent<RectTransform>().sizeDelta;
                    vitalityBlock2.GetComponent<RectTransform>().sizeDelta = new Vector2((vitalityBlock.rectTransform.sizeDelta.x * ((player.VitalityPresent - 100) / (float)100.0f)), vitalityBlock.rectTransform.sizeDelta.y);
                    vitalityBlock3.GetComponent<RectTransform>().sizeDelta = new Vector2 (0, 0);
                } else if (player.VitalityPresent > 0) {
                    vitalityBlock1.GetComponent<RectTransform>().sizeDelta = new Vector2((vitalityBlock.rectTransform.sizeDelta.x * ((player.VitalityPresent) / (float)100.0f)), vitalityBlock.rectTransform.sizeDelta.y);
                    vitalityBlock2.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
                    vitalityBlock3.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
                } else {
                    vitalityBlock1.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
                    vitalityBlock2.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
                    vitalityBlock3.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
                }

                /**
                if (vm > 300) {
                    vm -= 300;
                }

                measure = vm;
                if (measure < 0) {
                    measure = 0;
                }

                vitalityBlock1.GetComponent<RectTransform> ().sizeDelta = new Vector2 ((vitalityBlock.rectTransform.sizeDelta.x * (((float)measure) / (float)300.0f)), vitalityBlock.rectTransform.sizeDelta.y);

                measure = vm - 100;
                if (measure < 0) {
                    measure = 0;
                } if (measure > 200) {
                    measure = 200;
                }
                vitalityBlock2.GetComponent<RectTransform> ().sizeDelta = new Vector2 ((vitalityBlock.rectTransform.sizeDelta.x * (((float)measure) / (float)200.0f)), vitalityBlock.rectTransform.sizeDelta.y);

                measure = vm - 200;
                if (measure < 0)
                {
                    measure = 0;
                } if (measure > 100) {
                    measure = 100;
                }
                vitalityBlock3.GetComponent<RectTransform> ().sizeDelta = new Vector2 ((vitalityBlock.rectTransform.sizeDelta.x * (((float)measure) / (float)300.0f)), vitalityBlock.rectTransform.sizeDelta.y);
                */

                if (!player.FlashingVitality) {
                    if (player.StateActive(player.Sadness)) {
                        vitalityBlock1.color = player.Sadness.StateColor;
                        vitalityBlock2.color = player.Sadness.StateColor;
                        vitalityBlock3.color = player.Sadness.StateColor;
                        vitalityBlockBackground.color = player.Sadness.StateColor;
                    }
                    else if (player.Potency(player.Adle) >= 4) {
                        vitalityBlock1.color = player.Adle.StateColor;
                        vitalityBlock2.color = player.Adle.StateColor;
                        vitalityBlock3.color = player.Adle.StateColor;
                        vitalityBlockBackground.color = player.Adle.StateColor;
                    }
                    else {
                        if (player.VitalityPresent == 600) {
                            vitalityBlock1.color = vitalityBlockColor1D;
                            vitalityBlock2.color = vitalityBlockColor2D;
                            vitalityBlock3.color = vitalityBlockColor3D;
                        } else if (player.VitalityPresent < 600 && player.VitalityPresent >= 500) {
                            vitalityBlock1.color = vitalityBlockColor1D;
                            vitalityBlock2.color = vitalityBlockColor2D;
                            vitalityBlock3.color = vitalityBlockColor3C;
                            vitalityBlockBackground.color = vitalityBlockColor3B;
                        } else if (player.VitalityPresent < 500 && player.VitalityPresent >= 400) {
                            vitalityBlock1.color = vitalityBlockColor1D;
                            vitalityBlock2.color = vitalityBlockColor2C;
                            vitalityBlock3.color = vitalityBlockColor3C;
                            vitalityBlockBackground.color = vitalityBlockColor2B;
                        } else if (player.VitalityPresent < 400 && player.VitalityPresent > 300) {
                            vitalityBlock1.color = vitalityBlockColor1C;
                            vitalityBlock2.color = vitalityBlockColor2C;
                            vitalityBlock3.color = vitalityBlockColor3C;
                            vitalityBlockBackground.color = vitalityBlockColor1B;
                        } else if (player.VitalityPresent == 300) {
                            vitalityBlock1.color = vitalityBlockColor1B;
                            vitalityBlock2.color = vitalityBlockColor2B;
                            vitalityBlock3.color = vitalityBlockColor3B;
                        } else if (player.VitalityPresent < 300 && player.VitalityPresent >= 200) {
                            vitalityBlock1.color = vitalityBlockColor1B;
                            vitalityBlock2.color = vitalityBlockColor2B;
                            vitalityBlock3.color = vitalityBlockColor3A;
                            vitalityBlockBackground.color = vitalityBlockColorBackgroundA;
                        } else if (player.VitalityPresent < 200 && player.VitalityPresent >= 100) {
                            vitalityBlock1.color = vitalityBlockColor1B;
                            vitalityBlock2.color = vitalityBlockColor2A;
                            vitalityBlockBackground.color = vitalityBlockColorBackgroundA;
                        } else if (player.VitalityPresent > 0) {
                            vitalityBlock1.color = vitalityBlockColor1A;
                            vitalityBlockBackground.color = vitalityBlockColorBackgroundA;
                        } else {
                            vitalityBlockBackground.color = vitalityBlockColorBackgroundA;
                        }
                    }
                }

            }

            /**
            if (player.FlashingVitality) {
                vitalityTimer -= Time.deltaTime;
                if (vitalityTimer < 0.0f)
                {
                    vitalityTimer = FLASHTIMER;
                    vitalityBlock1.color = vitalityBlockColor1A;
                    vitalityBlock2.color = vitalityBlockColor2A;
                    vitalityBlock3.color = vitalityBlockColor3A;

                    if (player.Vitality.MeterLevel == 600) {
                        if (vitalityBlock1.color.Equals (vitalityBlockColor1C)) {
                            vitalityBlock1.color = vitalityBlockColor1D;
                            vitalityBlock2.color = vitalityBlockColor2D;
                            vitalityBlock3.color = vitalityBlockColor3D;
                        } else {
                            vitalityBlock1.color = vitalityBlockColor1C;
                            vitalityBlock2.color = vitalityBlockColor2C;
                            vitalityBlock3.color = vitalityBlockColor3C;
                        }
                    }

                    else if (player.Vitality.MeterLevel < 600 && player.Vitality.MeterLevel >= 500)
                    {
                        vitalityBlock3.color = Color.green;
                        vitalityBlockBackground.color = vitalityBlockColor1A;
                        if (vitalityBlock1.color.Equals(vitalityBlockColor1C))
                        {
                            vitalityBlock1.color = vitalityBlockColor1D;
                            vitalityBlock2.color = vitalityBlockColor2D;
                        }
                        else
                        {
                            vitalityBlock1.color = vitalityBlockColor1C;
                            vitalityBlock2.color = vitalityBlockColor2C;
                        }
                    }

                    else if (player.Vitality.MeterLevel < 500 && player.Vitality.MeterLevel >= 400)
                    {
                        vitalityBlock2.color = Color.green;
                        vitalityBlockBackground.color = vitalityBlockColor1A;
                        if (vitalityBlock1.color.Equals(vitalityBlockColor1C))
                        {
                            vitalityBlock1.color = vitalityBlockColor1D;
                        }
                        else
                        {
                            vitalityBlock1.color = vitalityBlockColor1C;
                        }
                    }

                    else if (player.Vitality.MeterLevel < 400 && player.Vitality.MeterLevel >= 300)
                    {
                        vitalityBlock1.color = Color.green;
                        vitalityBlockBackground.color = vitalityBlockColor1A;
                        if (vitalityBlock1.color.Equals(vitalityBlockColor1C))
                        {
                            vitalityBlock1.color = vitalityBlockColor1D;
                        }
                        else
                        {
                            vitalityBlock1.color = vitalityBlockColor1C;
                        }
                    }

                    else if (player.Vitality.MeterLevel == 300) {
                        if (vitalityBlock1.color.Equals(vitalityBlockColor1A))
                        {
                            vitalityBlock1.color = vitalityBlockColor1B;
                            vitalityBlock2.color = vitalityBlockColor2B;
                            vitalityBlock3.color = vitalityBlockColor3B;
                        }
                        else
                        {
                            vitalityBlock1.color = vitalityBlockColor1A;
                            vitalityBlock2.color = vitalityBlockColor2A;
                            vitalityBlock3.color = vitalityBlockColor3A;
                        }
                    }

                    else if (player.Vitality.MeterLevel < 300 && player.Vitality.MeterLevel >= 200)
                    {
                        vitalityBlock2.color = Color.green;
                        vitalityBlockBackground.color = vitalityBlockColorBackgroundA;
                        if (vitalityBlock1.color.Equals(vitalityBlockColor1A))
                        {
                            vitalityBlock1.color = vitalityBlockColor1B;
                            vitalityBlock1.color = vitalityBlockColor1B;
                        }
                        else
                        {
                            vitalityBlock1.color = vitalityBlockColor1C;
                        }
                    }

                    else if (player.Vitality.MeterLevel < 200 && player.Vitality.MeterLevel >= 100)
                    {
                        vitalityBlock2.color = Color.green;
                        vitalityBlockBackground.color = vitalityBlockColorBackgroundA;
                        if (vitalityBlock1.color.Equals(vitalityBlockColor1A))
                        {
                            vitalityBlock1.color = vitalityBlockColor1B;
                        }
                        else
                        {
                            vitalityBlock1.color = vitalityBlockColor1A;
                        }
                    }
                }
            }
            */
            stock.enabled = true;
            //stock.text = player.VitalityPresent + @"/" + player.Vitality.MeterMax;
            stock.text = player.Vitality.MeterLevel + @"/" + player.Vitality.MeterMax;

            guardBar.GetComponent<RectTransform> ().sizeDelta = new Vector2 ((guardBarBackground.rectTransform.sizeDelta.x * player.Guard.MeterRatio), guardBarBackground.rectTransform.sizeDelta.y);
            stunBar.GetComponent<RectTransform> ().sizeDelta = new Vector2 ((stunBarBackground.rectTransform.sizeDelta.x * player.Stun.MeterRatio), stunBarBackground.rectTransform.sizeDelta.y);

        }
        else {
            name.enabled = false;
            portrait.enabled = false;
            healthBar.enabled = false;
            healthBarBackground.enabled = false;
            healthBarRecover.enabled = false;
            stock.enabled = false;
            vitalityBlock1.enabled = false;
            vitalityBlock2.enabled = false;
            vitalityBlock3.enabled = false;
            vitalityBlockBackground.enabled = false;
            stunBarBackground.enabled = false;
            stunBar.enabled = false;
            guardBarBackground.enabled = false;
            guardBar.enabled = false;
        }

    }

    public void addAnimation (Player player, Reel reel)
    {
        
    }

    public void setAnimation (Player player, Reel reel)
    {
        
    }

    public void setAnimation (Player player, Texture2D texture)
    {
        player.MapSprite.texture = texture;
        //player.MyReel = ;
    }

    public void setReel (Player player, Reel r)
    {
        //player.MyReel.SetReel (r);
        player.MyReel = r;
        player.MyReel.Index = -1;
        player.MyReel.FrameTime = 0.01f;
    }

    public void decideMatch ()
    {
        //decisionTime = 3.0f;
        roundNotification.text = @"";
        matchOver = true;
        inDecision = false;
        float team1Health = (float)player1.Health.MeterRatio,
        team2Health = (float)player2.Health.MeterRatio;

        if (player3 != null) {
            team1Health += (float)player3.Health.MeterRatio;
            team1Health /= 2;
        }
        if (player4 !=  null) {
            team2Health += (float)player4.Health.MeterRatio;
            team2Health /= 2;
        }

        if (team1Health > team2Health) {
            roundNotification.color = Color.red;
            battleMap.Team1.RoundsWon++;
            player2.SetReel (player2.LoseSprites [0]);
            player1.SetReel(player1.WinSprites [0]);

            if (player4 != null) {
                player4.SetReel (player4.CrouchingSprite);
            }
            if (player3 != null) {
                player3.SetReel (player3.WinSprites [0]);
            }
            if (!player1.KOd && player3 != null && !player3.KOd) {
                roundNotification.text += player1.FirstName.ToUpper() + @" & " + player3.FirstName.ToUpper() + '\n'
                    + @"WIN";
            } else if (player1.KOd && player3 != null && player3.KOd) {
                roundNotification.text += player3.FirstName.ToUpper() + '\n' + @"WINS";
            } else if (!player1.KOd && (player3 == null || player3.KOd)) {
                roundNotification.text += player1.FirstName.ToUpper() + '\n' + @"WINS";
            }
            print (@"TEAM 1 WINS: " + team1Health + @" >" + team2Health);
        } else if (team2Health > team1Health) {
            roundNotification.color = Color.blue;
            battleMap.Team2.RoundsWon++;
            player1.SetReel(player1.LoseSprites[0]);
            player2.SetReel(player2.WinSprites[0]);

            if (player3 != null)
            {
                player3.SetReel(player3.LoseSprites[0]);
            }
            if (player4 != null)
            {
                player4.SetReel(player4.WinSprites[0]);
            }
            if (!player2.KOd && player2 != null && !player4.KOd)
            {
                roundNotification.text += player2.FirstName.ToUpper() + @" & " + player4.FirstName.ToUpper() + '\n'
                    + @"WIN";
            }
            else if (player2.KOd && player4 != null && player4.KOd)
            {
                roundNotification.text += player4.FirstName.ToUpper() + '\n' + @"WINS";
            }
            else if (!player2.KOd && (player4 == null || player4.KOd))
            {
                roundNotification.text += player2.FirstName.ToUpper() + '\n' + @"WINS";
            }
            print(@"TEAM 2 WINS: " + team2Health + @" >" + team1Health);
        } else {
            roundNotification.color = Color.grey;
            roundNotification.text = @"DRAW GAME...";

            print (@"DRAW GAME " + team1Health);
            for (int i = 0; i < team1.Roster.Count; i ++) {
                team1[i].SetReel (team1[i].LoseSprites[0]);
            }
            for (int i = 0; i < team2.Roster.Count; i++)
            {
                team2[i].SetReel(team2[i].LoseSprites[0]);
            }

            player1.SetReel(player1.LoseSprites [0]);
            player2.SetReel(player2.LoseSprites [0]);

            if (player3 != null)
            {
                player3.SetReel(player3.LoseSprites [0]);
            }
            if (player4 != null)
            {
                player4.SetReel(player4.LoseSprites [0]);
            }


            battleMap.Team1.RoundsWon++;
            battleMap.Team2.RoundsWon++;

        }

        setWins (battleMap.Team1, t1NumWins);
        setWins (battleMap.Team2, t2NumWins); 

        //roundNotificationBackground.color = Color.white;

        print (@"-->" + player1.MapSprite.name);
        print (@"-->" + player2.MapSprite.name);
        print (player1.MapSprite.texture.name);
        print (player2.MapSprite.texture.name);

        /**
        if (!battleMap.Team1.IsDefeated && !battleMap.Team2.IsDefeated) {
            decisionTime = 3.0f;

        } else {
            decisionTime = 5.0f;
        }
        */
    }

    //SETS UP ALL PLAYER GRAPHICS
    public void setPlayerGraphicsAndAudio (Player p, RawImage sprite, RawImage healthBar, Team team, int index)
    {
        p.InPlay = true;
        p.OnField = true;

        p.MyTeam = team;
        print (@"SETTING GRAPHICS AND AUDIO FOR " + p.FirstName.ToUpper ());

        p.StepTime = 0;
        p.StepTimeModulus = 0;


        p.MapSprite = sprite;
        p.Body = p.MapSprite.GetComponent<Rigidbody2D> ();
        p.MapSprite.texture = Resources.Load<Texture2D> (@"Players/" + p.SearchName + @"/Animations/NeutralSprite1");
        if (p.MapSprite.texture == null) {
            throw new NullReferenceException (@"D'OH!");
        }
        //p.adjustPlayerSpriteBasedOnScale (p, p.MapSprite);

        //p.ShadowSprite = cloneRawImage (@"Player " + p.FirstName + @" " + p.MyTeam.Abbreviation + @" ShadowSprite",
         //   p.MapSprite, gameCanvas.transform);
        //p.ShadowSprite.enabled = false;
        //HIT SFX
        p.PlayerOutputIndexes = new ArrayList ();

        //p.Index = index;
        p.Vocals.Speech = new GameObject ().AddComponent<AudioSource>();
        p.Vocals.ContactSound = new GameObject ().AddComponent<AudioSource>();

        p.Vocals.Speech.gameObject.name = p.FirstName + @" Speech Channel";
        p.Vocals.ContactSound.gameObject.name = p.FirstName + @" Contact Sound Channel";

        p.Vocals.ContactSound.volume = 2.0f;
        p.Vocals.Speech.volume = 0.7f;
        p.Experience = 0;

        p.SSR = SPRITESCALERATIO;

        //p.MapSprite.texture = p.VersusAppearance;
        //adjustPlayerSpriteBasedOnScale (p, p.MapSprite);

        //DIALOGUE
        p.Vocals.Dialogue = new ArrayList ();

        //HEALTH DISPLAY

        p.HealthBar = healthBar;

        p.HealthBar.transform.SetParent(gameCanvas.transform);

        p.HealthBar.gameObject.name = p.FirstName + p.MyTeam.Abbreviation + @" Health";

        p.HealthBar.texture = Resources.Load<Texture2D>(@"Textures/White");

        p.ParentCanvas = gameCanvas.transform;
        /**
        //p.HitBox = cloneRawImage (p.Name + p.Index + @" HIT BOX ", sampleHitBox, gameCanvas.transform);

        //p.HurtBox = cloneRawImage (p.Name + p.Index + @" HURT BOX ", sampleHurtBox, gameCanvas.transform);

        //p.HitBox.transform.SetAsLastSibling();

        //p.HurtBox.transform.SetAsLastSibling();

        p.HitBox.texture = Resources.Load<Texture2D>(@"Textures/Blank");

        p.HurtBox.texture = Resources.Load<Texture2D>(@"Textures/Blank");

        p.HitBox.enabled = true;

        p.HurtBox.enabled = true;
        */
        //p.SpriteInfo = cloneText (p.Name + p.Index + @" SPRITE INFO", sampleInfo, gameCanvas.transform);

        p.setAllHurtAndHitBoxes ();

        //p.HealthBar.enabled = true;

       //p.HealthBar.GetComponent<RectTransform>().sizeDelta = new Vector2(sampleHealthBar.rectTransform.sizeDelta.x * p.Health.MeterRatio, sampleHealthBar.rectTransform.sizeDelta.y + MapAdjustedHeight(p.currentLocation().Row, p.currentLocation().Column));


        //float variant = targetHealthBarBackground.transform.localScale.y / 7;

        ///p.StatusIcon = new GameObject().AddComponent<RawImage>();

        ///p.StatusIcon.gameObject.name = p.FirstName + @"Status";

        ///p.StatusIcon.transform.SetParent(gameCanvas.transform);

        ///p.StatusIcon.GetComponent<RectTransform>().sizeDelta = new Vector2(15, 15);

        ///p.StatusIcon.enabled = true;

        p.SpriteScale = SPRITESCALERATIO;

        //p.printAllSkills ();
        //p.printAllSkillsOnOnePage ();

        /**
        if (battleMap.isValid(p.currentLocation ()))
        {
            displayMapPlayerGraphics(p, p.Row, p.Column);
        }
        */

        p.Colors = new ArrayList () { Color.red, Color.green };

        //TEST
        //p.Vitality.MeterLevel = 0;

    }

    public RawImage cloneRawImage (string name, RawImage original, Transform parent)
    {
        RawImage clone = new GameObject ().AddComponent<RawImage> ();
        clone.gameObject.name = name;
        clone.transform.SetParent (parent);
        clone.GetComponent<RectTransform> ().sizeDelta = new Vector2(original.rectTransform.sizeDelta.x, original.rectTransform.sizeDelta.y);
        clone.rectTransform.anchorMin = original.rectTransform.anchorMin;
        clone.rectTransform.anchorMax = original.rectTransform.anchorMax;
        clone.rectTransform.pivot = original.rectTransform.pivot;
        clone.transform.position = original.transform.position;
        clone.transform.SetSiblingIndex(original.transform.GetSiblingIndex ());
        clone.texture = original.texture;
        return clone;
    }

    public Text cloneText (string name, Text original, Transform parent)
    {
        Text clone = new GameObject ().AddComponent<Text>();
        clone.gameObject.name = name;
        clone.transform.SetParent (gameCanvas.transform);
        clone.GetComponent<RectTransform> ().sizeDelta = new Vector2 (original.preferredWidth, original.preferredHeight);
        clone.alignment = original.alignment;
        clone.font = original.font;
        clone.fontStyle = original.fontStyle;
        clone.fontSize = original.fontSize;
        clone.horizontalOverflow = original.horizontalOverflow;
        clone.verticalOverflow = original.verticalOverflow;
        clone.alignByGeometry = original.alignByGeometry;
        clone.transform.position = original.transform.position;
        clone.text = original.text;
        clone.color = original.color;
        //clone.transform.SetAsLastSibling();
        return clone;
    }

    public Boolean AnimationsActive
    {
        get {
            for (int i = 0; i < battleMap.Roster.Count; i++) {
                if (((Player)battleMap.Roster [i]).MyReel.IsPlaying) {
                    return true;
                }
            }
            return false;
        }
    }

    public Texture2D ProperOrientation (Player p)
    {
        return p.SpriteAppearance;
    }
}

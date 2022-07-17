using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GameManager : Singleton<GameManager>
{
    [Header("References")]
    public GameObject floor;
    public GameObject poolTable;
    public PoolStateManager poolGameManager;
    public GameObject poolBallTriangle;
    public GameObject cueBall;
    public GameObject inGameUI;




    private State<GameManager> currentState;

    public bool currNarrationFinished;

    #region State Definitions
    private Act1State act1;
    private Act2State act2;
    private Act3State act3;
    #endregion

    private void Awake()
    {
        InitializeSingleton();

        act1 = new Act1State(this);
        act2 = new Act2State(this);
        act3 = new Act3State(this);
    }

    private void Start()
    {
        SwitchState(act1);
    }

    private void Update()
    {
        currentState?.UpdateState();
    }

    private void SwitchState(State<GameManager> a_state)
    {
        if (this.currentState != null)
        {
            this.currentState.ExitState();
        }

        this.currentState = a_state;
        this.currentState.EnterState();
    }

    public void DoNarrationAndSetFlag(string path, params CallbackWithDelay[] afterDelay)
    {
        currNarrationFinished = false;
        NarrationManager.PlayVoiceClip(path, () => {
            currNarrationFinished = true;
        });
    }

    #region State Classes

    #region Act1
    public class Act1State : State<GameManager>
    {
        private bool playerPutBallInPocket;
        private bool turnCompleted;
        public Act1State(GameManager ctx) : base(ctx)
        {
        }

        public override void EnterState()
        {

            context.StartCoroutine(DoAct1());
        }

        public IEnumerator DoAct1()
        {
            context.floor.SetActive(false);
            context.poolTable.SetActive(false);
            context.poolBallTriangle.SetActive(false);
            context.cueBall.SetActive(false);
            context.poolGameManager.gameObject.SetActive(false);
            context.inGameUI.SetActive(false);

            //Oppy … and I’ve already got someone playtesting it right now!
            //context.DoNarrationAndSetFlag("Act1/Oppy/001Playtest");
            //yield return new WaitUntil(() => { return context.currNarrationFinished; });

            ////Cynic already?
            //context.DoNarrationAndSetFlag("Pessimist/HelloWelcomeTo");
            //yield return new WaitUntil(() => { return context.currNarrationFinished; });

            ////Oppy Crazy, right? Okay, so picture this…*elevator ding sound*
            //context.DoNarrationAndSetFlag("Act1/Oppy/003PictureThis");
            //yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Turn on lights
            context.floor.SetActive(true);  

            //Oppy A pool table
            //context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            //yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Turn on pool table
            context.poolTable.SetActive(true);

            //Oppy and, and it’s like your standard pool game you know
            //context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            //yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Enable the pool game
            context.poolBallTriangle.SetActive(true);
            context.cueBall.SetActive(true);

            //Oppy …except, they’re not actually balls, they’re dice!
            //context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            //yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Change all balls to dice
            PoolStateManager.ChangeAllToShape(PoolBall.Shape.Dice);
            PoolStateManager.ResetGame();
            context.poolGameManager.gameObject.SetActive(true);            //Enable the game and the controls.
            context.inGameUI.SetActive(true);

            //playerPutBallInPocket = false;
            //PoolBall.ballInPocketEvent += PutBallInPocket;  //Start keeping track of if the player pocketed the die.

            //Cynic: Dice?
            //context.DoNarrationAndSetFlag("Pessimist/HelloWelcomeTo");
            //yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Oppy: Yeah, so I got to thinking about making a pool game with a twist ...
            //context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            //yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Wait until ball in pocket.
            //yield return new WaitUntil(() => playerPutBallInPocket);
            //PoolBall.ballInPocketEvent -= PutBallInPocket;

            //Cynic: That’s stupid.
            //context.DoNarrationAndSetFlag("Pessimist/HelloWelcomeTo");
            //yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Oppy: *pause for a moment* what?
            //context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            //yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Cynic: I said that’s stupid. How does changing them from balls to cubes make the game more about skill?
            //context.DoNarrationAndSetFlag("Pessimist/HelloWelcomeTo");
            //yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Oppy: Well…
            //context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            //yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Cynic: Weren’t pool balls made to be balls for a reason?
            //context.DoNarrationAndSetFlag("Pessimist/HelloWelcomeTo");
            //yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Oppy: Yeah, but this is new!
            //context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            //yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Cynic: Just because something is new doesn’t make it better. You can’t just mash two ideas together and expect it to be fun.
            //context.DoNarrationAndSetFlag("Pessimist/HelloWelcomeTo");
            //yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Oppy: But I haven’t even told you about the numbers!
            //context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            //yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Cynic: You mean the numbers on the dice?
            //context.DoNarrationAndSetFlag("Pessimist/HelloWelcomeTo");
            //yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Oppy: There are so many possibilities! We could make the numbers add more balls to the table, or maybe only odd numbers give you points, or something else.
            //context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            //yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Cynic: Okay I really need to stop you there. Having “possibilities” is not the same as having “ideas”. *upbeat * Be decisive and pick one!Can you settle on one for christ’s sake ?
            //context.DoNarrationAndSetFlag("Pessimist/HelloWelcomeTo");
            //yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Oppy : Ok, ok.When you pocket something, you earn however many points are on the top face of the die!
            //context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            //yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Enable Score
            PoolStateManager._instance.ScoreEnabled = true;
            PoolStateManager.scoreMode = PoolStateManager.ScoreMode.ONLY_ONES;

            //playerPutBallInPocket = false;
            //PoolBall.ballInPocketEvent += PutBallInPocket;  //Start keeping track of if the player pocketed the die.

            ////Cynic: So its like a random chance to get 1 - 6 points…
            //context.DoNarrationAndSetFlag("Pessimist/HelloWelcomeTo");
            //yield return new WaitUntil(() => { return context.currNarrationFinished; });

            ////Oppy: No! Watch, the playtester will get a 6 in soon!
            //context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            //yield return new WaitUntil(() => { return context.currNarrationFinished; });

            ////*Wait until ball in pocket.
            //yield return new WaitUntil(() => playerPutBallInPocket);
            //PoolBall.ballInPocketEvent -= PutBallInPocket;

            //Oppy:
            //context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            //yield return new WaitUntil(() => { return context.currNarrationFinished; });

            ////Cynic: Okay, I still don’t think there’s enough control here. You’re just hitting the cube and praying for a 6.
            //context.DoNarrationAndSetFlag("Pessimist/HelloWelcomeTo");
            //yield return new WaitUntil(() => { return context.currNarrationFinished; });

            ////Oppy: Hmm… I have an idea
            //context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            //yield return new WaitUntil(() => { return context.currNarrationFinished; });

            ////Cynic: Oh boy Oh good, I love it when you get those
            //context.DoNarrationAndSetFlag("Pessimist/HelloWelcomeTo");
            //yield return new WaitUntil(() => { return context.currNarrationFinished; });

            ////Oppy: I was thinking about this time I played pool with my friends ...
            //context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            //yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Cynic: So… tilt controls ?
            context.DoNarrationAndSetFlag("Pessimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Enable Tilting
            TiltingTable table = context.poolTable.GetComponent<TiltingTable>();
            table.TiltingEnabled = true;

            //Oppy: Exactly! Now the player has control over where the balls… *erm * dice go
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Cynic: Hmmm… so the player is tilting the table? And they can use this strategically as another way to get the dice in?
            context.DoNarrationAndSetFlag("Pessimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //[Cut off if player tilts a bunch of dice in, else skip]
            yield return new WaitUntil(() => { return PoolStateManager._instance.numBallsSunk > 5; });

            //Cynic: No no no! You see how many they just got in? That’s not skill
            context.DoNarrationAndSetFlag("Pessimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Oppy: Look! How exciting!Their score shot way up!
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Cynic: Sigh, That’s not exciting! You completely removed any semblance of strategy your “game” once had. Now it’s just a QTE of a bunch of cubes falling into holes.
            context.DoNarrationAndSetFlag("Pessimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Oppy: Oh. Huh. Well, nothing that a little juice and polish can’t fix! Here, it just needs a little more … uh… 
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Cynic: I’m sure that’ll do wonders for the gameplay a little more ?
            context.DoNarrationAndSetFlag("Pessimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Oppy: Oh.Huh.Well, nothing that a little juice and polish can’t fix!Here, it just needs a little more … uh… 
            context.DoNarrationAndSetFlag("Pessimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Cynic: I’m sure that’ll do wonders for the gameplay a little more ?
            context.DoNarrationAndSetFlag("Pessimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Oppy : Juice! Yeah, Ok hold on, I saw some tutorials about this online.Behold, SCREENSHAKE!
            context.DoNarrationAndSetFlag("Pessimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Screenshake is added (ADD THIS!!!!!!!!!!!!!!)

            //Oppy: and now, the cherry on top: , and just a little bitta particle effects as the cherry on top!
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Particle effects added (ADD THIS!!!!!!!!!!!!!!)

            //Oppy: mwa! Magnifique!
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Cynic: Well uh, aesthetics are… important.Look kid, I’m gonna go grab a coffee.If you’ve got more to say, I guess stick around and I’ll be back.But I gotta take a break.
            context.DoNarrationAndSetFlag("Pessimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Oppy: Oh, uh okay. Don’t worry, I’ll be right here!
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Cynic: I’m sure you will… *footstep sounds *
            context.DoNarrationAndSetFlag("Pessimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });
        }

        private void SetTurnCompleted(object sender, System.EventArgs e)
        {
            turnCompleted = true;
        }

        private void PutBallInPocket(object sender, PoolBall.BallEventArgs e)
        {
            playerPutBallInPocket = true;
        }
    }
    #endregion

    #region Act2 
    public class Act2State : State<GameManager>
    {
        public Act2State(GameManager ctx) : base(ctx)
        {
        }

        public override void EnterState()
        {
            context.StartCoroutine(DoAct2());
        }

        public IEnumerator DoAct2()
        {
            yield return null;
        }
    }
    #endregion

    #region Act 3
    public class Act3State : State<GameManager>
    {
        public Act3State(GameManager ctx) : base(ctx)
        {
        }

        public override void EnterState()
        {
            context.StartCoroutine(DoAct3());
        }

        public IEnumerator DoAct3()
        {
            yield return null;   
        }
    }
    #endregion

    #endregion
}

//For naming convention 
public enum GameState
{
    Default
}


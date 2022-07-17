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
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            ////Cynic already?
            //context.DoNarrationAndSetFlag("Pessimist/HelloWelcomeTo");
            //yield return new WaitUntil(() => { return context.currNarrationFinished; });

            ////Oppy Crazy, right? Okay, so picture this…*elevator ding sound*
            //context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            //yield return new WaitUntil(() => { return context.currNarrationFinished; });

            context.floor.SetActive(true);  //Turn on lights

            //Oppy A pool table
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            context.poolTable.SetActive(true);

            //Oppy and, and it’s like your standard pool game you know
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Enable the pool game
            context.poolBallTriangle.SetActive(true);
            context.cueBall.SetActive(true);
            context.poolGameManager.gameObject.SetActive(true);
            context.inGameUI.SetActive(true);

            //Wait until player made a turn.
            turnCompleted = false;
            PoolStateManager.TurnEnded += SetTurnCompleted;   //Scuffy me Luffy 
            yield return new WaitUntil(() => { return turnCompleted; });
            PoolStateManager.TurnEnded -= SetTurnCompleted;

            //Oppy …except, they’re not actually balls, they’re dice!
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            PoolStateManager.ChangeAllToShape(PoolBall.Shape.Dice);


            playerPutBallInPocket = false;
            yield return new WaitUntil(() => playerPutBallInPocket);


            //Cynic

            //Oppy




        }

        private void SetTurnCompleted(object sender, System.EventArgs e)
        {
            turnCompleted = true;
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


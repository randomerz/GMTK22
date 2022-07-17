using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GameManager : Singleton<GameManager>
{
    [Header("References")]
    public GameObject poolTable;
    public PoolStateManager poolGameManager;
    public GameObject poolBallTriangle;
    public GameObject cueBall;




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

    public void DoNarrationAndSetFlag(string path)
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
        public Act1State(GameManager ctx) : base(ctx)
        {
        }

        public override void EnterState()
        {
            context.StartCoroutine(DoAct1());
        }

        public IEnumerator DoAct1()
        {
            NarrationManager.PlayVoiceClip("Optimist/HelloWelcomeTo.asset");
            yield return new WaitUntil(() =>
            {
                return context.currNarrationFinished;
            });
            Debug.Log("Narration Finished!");
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


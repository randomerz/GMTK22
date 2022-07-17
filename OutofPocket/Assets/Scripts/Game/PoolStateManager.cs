using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Controls all of the logic for running pool games.
//Call StartGame() to start the pool game (make sure everything is set in inspector)
public class PoolStateManager : Singleton<PoolStateManager>
{

    public bool hasThisStartedYet = false;
    public bool triggerTilting = false;

    public bool startGameImmediately;

    [Header("References")]
    public CueBall cueBall;
    public Collider gameArenaCollider;
    public TiltingTable tiltingTable;

    [Header("Cue Ball Shot Power")]
    public float screenDeltaToPower;
    public float minShotPower;
    public float maxShotPower;
    public float popUpForce;

    [Header("Ending Turn")]
    public float minDelayBetweenShots;
    public float maxDelayBetweenShots;
    public float ballVelStopThreshold;

    [Header("READ ONLY")]
    public int numBallsSunk;


    private PoolBall[] _poolBalls;
    private PoolStateIdle _emptyState;
    private PoolStatePlayerTurn _playerTurnState;
    private PoolStateWaitingForEndOfTurn _waitingForEndOfTurnState;

    [Header("Tutorial Annotations")]
    public TextMeshProUGUI holdClickAnnotation;
    public TextMeshProUGUI tiltingAnnotation;

    public PoolStateIdle EmptyState => _emptyState;
    public PoolStatePlayerTurn PlayerTurnState => _playerTurnState;
    public PoolStateWaitingForEndOfTurn WaitingForEndOfTurnState => _waitingForEndOfTurnState;

    public static event System.EventHandler TurnEnded;

    public PoolBall[] PoolBalls => _poolBalls;
    public bool CueBallSunk { get; set; }

    public bool ScoreEnabled
    {
        get
        {
            return _scoreEnabled;
        }

        set
        {
            _scoreEnabled = value;
            if (value)
            {
                ScoreManager._instance.ShowScore();
                ScoreManager.Score = 0;
            }
            else
            {
                ScoreManager._instance.HideScore();
                ScoreManager.Score = 0;
            }
        }
    }
    private bool _scoreEnabled;

    private State<PoolStateManager> currState;

    private void Awake()
    {
        InitializeSingleton();

        _poolBalls = GameObject.FindObjectsOfType<PoolBall>();

        _emptyState = new PoolStateIdle(this);
        _playerTurnState = new PoolStatePlayerTurn(this);
        _waitingForEndOfTurnState = new PoolStateWaitingForEndOfTurn(this);
    }

    private void OnEnable()
    {
        PoolBall.ballInPocketEvent += BallInPocketHandler;
    }

    private void OnDisable()
    {
        PoolBall.ballInPocketEvent -= BallInPocketHandler;
    }

    private void Start()
    {
        if (startGameImmediately)
        {
            StartGame();
        }
        else
        {
            SwitchState(_emptyState);
        }
        ScoreEnabled = true;    //For Testing
        tiltingAnnotation.enabled = false;
    }

    private void Update()
    {
        currState?.UpdateState();
        if (OOPInput.vertical != 0 || OOPInput.horizontal != 0)
        {
            tiltingAnnotation.gameObject.SetActive(false);
            triggerTilting = false;
        }
    }

    private void FixedUpdate()
    {
        currState?.FixedUpdateState();
    }

    private void OnTriggerEnter(Collider other)
    {
        currState?.OnTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        currState?.OnTriggerExit(other);
    }

    public void SwitchState(State<PoolStateManager> nextState)
    {
        if (currState != null)
        {
            currState.ExitState();

            if (currState == WaitingForEndOfTurnState)
            {
                TurnEnded?.Invoke(this, null);
            }
        }
        currState = nextState;
        nextState.EnterState();
    }

    public static void StartGame()
    {
        //foreach (PoolBall pb in _poolBalls)
        //{
        //    pb.initialPos = pb.transform.position;
        //}
        ResetGame();
    }

    public static void ResetGame()
    {
        _instance.numBallsSunk = 0;
        foreach (PoolBall pb in _instance._poolBalls)
        {
            pb.ResetBall();
        }
        _instance.SwitchState(_instance._playerTurnState);
    }

    public void ResetCueBall()
    {
        CueBallSunk = false;
        PoolBall pb = cueBall.GetComponent<PoolBall>();
        cueBall.transform.position = pb.initialPos;
    }

    public static void ChangeAllToShape(PoolBall.Shape shape)
    {
        foreach (var ball in _instance._poolBalls)
        {
            ball.ChangeShape(shape);
        }
    }

    private void BallInPocketHandler(object sender, PoolBall.BallEventArgs e)
    {
        //Reset the cue ball if it goes out of bounds
        CueBall cb = e.ball.GetComponent<CueBall>();
        if (cb != null)
        {
            CueBallSunk = true;
        }
        else
        {
            //Normal pool ball
            numBallsSunk++;
            if (ScoreEnabled)
            {
                ScoreManager.AddRandomAmountOfScore();
            }

            if (numBallsSunk >= _poolBalls.Length - 1)
            {
                ResetGame();
            }
        }
    }
}
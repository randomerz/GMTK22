using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controls all of the logic for running pool games.
//Call StartGame() to start the pool game (make sure everything is set in inspector)
public class PoolStateManager : Singleton<PoolStateManager>
{
    public CueBall cueBall;

    [Header("Cue Ball Shot Power")]
    public float screenDeltaToPower;
    public float minShotPower;
    public float maxShotPower;

    [Header("Ending Turn")]
    public float minDelayBetweenShots;
    public float maxDelayBetweenShots;
    public float ballVelStopThreshold;

    private PoolBall[] _poolBalls;
    private PoolStateIdle _emptyState;
    private PoolStatePlayerTurn _playerTurnState;
    private PoolStateWaitingForEndOfTurn _waitingForEndOfTurnState;

    public PoolStateIdle EmptyState => _emptyState;
    public PoolStatePlayerTurn PlayerTurnState => _playerTurnState;
    public PoolStateWaitingForEndOfTurn WaitingForEndOfTurnState => _waitingForEndOfTurnState;

    public PoolBall[] PoolBalls => _poolBalls;

    private State<PoolStateManager> currState;

    private void Awake()
    {
        InitializeSingleton();

        _poolBalls = GameObject.FindObjectsOfType<PoolBall>();

        _emptyState = new PoolStateIdle(this);
        _playerTurnState = new PoolStatePlayerTurn(this);
        _waitingForEndOfTurnState = new PoolStateWaitingForEndOfTurn(this);
        SwitchState(_emptyState);
    }

    private void Update()
    {
        currState?.UpdateState();
    }

    private void FixedUpdate()
    {
        currState?.FixedUpdateState();
    }

    public void SwitchState(State<PoolStateManager> nextState)
    {
        if (currState != null)
        {
            currState.ExitState();
        }
        currState = nextState;
        nextState.EnterState();
    }

    public void StartGame()
    {
        Debug.Log("Game Started");
        SwitchState(_playerTurnState);
    }
}

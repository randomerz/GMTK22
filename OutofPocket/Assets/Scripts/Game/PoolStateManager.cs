using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Call StartGame() to start the pool game (make sure everything is set in inspector)
public class PoolStateManager : Singleton<PoolStateManager>
{
    public CueBall cueBall;

    public float screenDeltaToPower;
    public float minShotPower;
    public float maxShotPower;

    public float maxDelayBetweenShots;

    public PoolStateIdle emptyState;
    public PoolStatePlayerTurn playerTurnState;
    public PoolStateWaitingForEndOfTurn didShotState;

    private State<PoolStateManager> currState;

    private void Awake()
    {
        InitializeSingleton();

        emptyState = new PoolStateIdle(this);
        playerTurnState = new PoolStatePlayerTurn(this);
        didShotState = new PoolStateWaitingForEndOfTurn(this);
        SwitchState(emptyState);
    }

    private void Update()
    {
        currState?.UpdateState();
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
        SwitchState(playerTurnState);
    }
}

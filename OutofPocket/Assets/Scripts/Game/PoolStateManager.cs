using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolStateManager : Singleton<PoolStateManager>
{
    public CueBall cueBall;

    public float screenDeltaToPower;
    public float minShotPower;
    public float maxShotPower;

    private State<PoolStateManager> currState;

    private void Awake()
    {
        InitializeSingleton();

        SwitchState(new PoolStatePreparingShot(this));
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


}

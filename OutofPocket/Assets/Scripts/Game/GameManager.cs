using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GameManager : Singleton<GameManager>
{
    private State<GameManager> currentState;

    #region State Definitions
    private DefaultState defaultState;
    #endregion

    private void Awake()
    {
        InitializeSingleton();
    }

    private void Start()
    {
        defaultState = new DefaultState(this);
    }

    private void Update()
    {
        this.currentState?.UpdateState();
    }

    public void SwitchState(GameState a_gameState)
    {
        switch (a_gameState) 
        {
            case GameState.Default:
                SwitchState(defaultState);
                break;
        }
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

    #region State Classes
    public class DefaultState : State<GameManager>
    {
        public DefaultState(GameManager ctx) : base(ctx)
        {
        }      
    }
    #endregion
}

//For naming convention 
public enum GameState
{
    Default
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{
    private State<GameManager> currentState;

    #region Singleton
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }

            return instance;
        }
    }
    #endregion
    #region States
    private DefaultState defaultState;
    #endregion

    private void Start()
    {
        defaultState = new DefaultState(this);
    }

    private void Update()
    {
        this.currentState.UpdateState();
    }

    public void TransitionState(GameState a_gameState)
    {
        switch (a_gameState) 
        {
            case GameState.Default:
                TransitionState(defaultState);
                break;
        }
    }

    private void TransitionState(State<GameManager> a_state)
    {
        if (this.currentState != null)
        {
            this.currentState.ExitState();
        }

        this.currentState = a_state;
        this.currentState.EnterState();
    }

    #region States
    public class DefaultState : State<GameManager>
    {
        public DefaultState(GameManager ctx) : base(ctx)
        {
        }      
    }
    #endregion
}

public enum GameState
{
    Default
}


using UnityEngine;

public class PoolStateWaitingForEndOfTurn : State<PoolStateManager>
{
    private float elapsedTimeSinceEnter;

    public PoolStateWaitingForEndOfTurn(PoolStateManager ctx) : base(ctx)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Waiting for turn to end. (no player input allowed)");
        elapsedTimeSinceEnter = 0;
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
        elapsedTimeSinceEnter += Time.deltaTime;
    }

    public override void FixedUpdateState()
    {
        //Determine when the turn should end. (physics simulation will take care of the rest)
        if (elapsedTimeSinceEnter > context.minDelayBetweenShots && (BallsStoppedMovingOrSunk())) //|| elapsedTimeSinceEnter > context.maxDelayBetweenShots))
        {
            StopAllBalls();
            context.SwitchState(context.PlayerTurnState);
        }
    }

    private bool BallsStoppedMovingOrSunk()
    {
        foreach (PoolBall ball in context.PoolBalls)
        {
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            if (rb != null && rb.velocity.magnitude > context.ballVelStopThreshold && !ball.sunk)
            { 
                return false;
            }
        }

        Debug.Log("Balls Stopped Moving");
        return true;
    }

    private void StopAllBalls()
    {
        foreach (PoolBall ball in context.PoolBalls)
        {
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            if (rb != null)
            {
                //Disable physics
                //Debug.Log($"Setting rb velocity to zero for {rb.gameObject.name}");
                rb.isKinematic = true;
                rb.velocity = Vector3.zero;
            }
        }
    }
}


using UnityEngine;

public class PoolStatePlayerTurn : State<PoolStateManager>
{
    public PoolStatePlayerTurn(PoolStateManager ctx) : base(ctx)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Player's Turn");

        if (!context.hasThisStartedYet)
        {
            // Enable tutorial stuff here
            context.holdClickAnnotation.enabled = true;
        }

        if (context.CueBallSunk)
        {
            context.ResetCueBall();
        }
        OOPInput.mouseDragOngoing += HandleMouseDrag;
        OOPInput.mouseDragEnd += HandleMouseDragEnd;
    }

    public override void ExitState()
    {
        OOPInput.mouseDragOngoing -= HandleMouseDrag;
        OOPInput.mouseDragEnd -= HandleMouseDragEnd;
    }

    //private void HandleMouseDragStart()
    //{
        /**
         * If we're in the tutorial this should go to i guess the next stage of it
         */

    //}

    private void HandleMouseDrag(object sender, OOPInput.MouseDragEventArgs e)
    {
        //Set UI Indicators
        if (!context.hasThisStartedYet)
        {
            // This is where we would remove the click-and-drag prompt and
            // begin showing a release-mouse prompt
            context.holdClickAnnotation.text = "Drag to aim and release to fire";

        }

    }

    private void HandleMouseDragEnd(object sender, OOPInput.MouseDragEventArgs e)
    {
        //Disable UI

        // Clear the Cue Ball drag tutorial if needed and start the next part of
        // the tutorial
        if (!context.hasThisStartedYet)
        {
            context.holdClickAnnotation.enabled = false;
            context.hasThisStartedYet = true;
        }


        //Debug.Log($"Mouse Drag Ended! startPos: {e.startPos} endPos: {e.endPos}");
        EnableBallPhysics();

        //Calculate shot trajectory from mouse position and shoot ball.
        Vector2 currMouseDelta = e.startPos - e.endPos;
        float power = Mathf.Clamp(currMouseDelta.magnitude * context.screenDeltaToPower, context.minShotPower, context.maxShotPower);
        Vector2 direction = currMouseDelta.normalized;
        context.cueBall.ShootBall(power, context.popUpForce, direction);
        context.SwitchState(context.WaitingForEndOfTurnState);
    }

    private void EnableBallPhysics()
    {
        foreach (PoolBall ball in context.PoolBalls)
        {
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            if (rb != null)
            {
                //Enable Physics
                rb.isKinematic = false;
            }
        }
    }
}


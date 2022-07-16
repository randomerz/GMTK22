using UnityEngine;

public class PoolStatePlayerTurn : State<PoolStateManager>
{
    public PoolStatePlayerTurn(PoolStateManager ctx) : base(ctx)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Player's Turn");
        OOPInput.mouseDragEnd += HandleMouseDragEnd;
    }

    public override void ExitState()
    {
        OOPInput.mouseDragEnd -= HandleMouseDragEnd;
    }

    private void HandleMouseDragEnd(object sender, OOPInput.MouseDragEventArgs e)
    {
        //Debug.Log($"Mouse Drag Ended! startPos: {e.startPos} endPos: {e.endPos}");
        EnableBallPhysics();

        Vector2 currMouseDelta = e.startPos - e.endPos;
        float power = Mathf.Clamp(currMouseDelta.magnitude * context.screenDeltaToPower, context.minShotPower, context.maxShotPower);
        Vector2 direction = currMouseDelta.normalized;
        context.cueBall.ShootBall(power, direction);
        context.SwitchState(context.WaitingForEndOfTurnState);
    }

    private void EnableBallPhysics()
    {
        foreach (GameObject ball in context.PoolBalls)
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


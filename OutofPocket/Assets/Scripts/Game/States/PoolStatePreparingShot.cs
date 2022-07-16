using UnityEngine;

public class PoolStatePreparingShot : State<PoolStateManager>
{
    public PoolStatePreparingShot(PoolStateManager ctx) : base(ctx)
    {
    }

    public override void EnterState()
    {
        OOPInput.mouseDragEnd += HandleMouseDragEnd;
    }

    public override void ExitState()
    {
        OOPInput.mouseDragEnd -= HandleMouseDragEnd;
    }

    private void HandleMouseDragEnd(object sender, OOPInput.MouseDragEventArgs e)
    {
        Debug.Log($"Mouse Drag Ended! startPos: {e.startPos} endPos: {e.endPos}");
        Vector2 currMouseDelta = e.endPos - e.startPos;
        float power = Mathf.Clamp(currMouseDelta.magnitude * context.screenDeltaToPower, context.minShotPower, context.maxShotPower);
        Vector2 direction = currMouseDelta.normalized;
        context.cueBall.ShootBall(power, direction);
    }
}


using UnityEngine;

public class PoolStateIdle : State<PoolStateManager>
{

    public PoolStateIdle(PoolStateManager ctx) : base(ctx)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Press d or right to start the game (lol).");
    }

    public override void UpdateState()
    {
        //This is only for testing purposes (delete later)
        if (OOPInput.horizontal > 0)
        {
            context.StartGame();
        }
    }
}
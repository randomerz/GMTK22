public class PoolStateIdle : State<PoolStateManager>
{
    public PoolStateIdle(PoolStateManager ctx) : base(ctx)
    {
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
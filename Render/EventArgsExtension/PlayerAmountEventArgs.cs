namespace MonopolyGame.Render.EventArgsExtension;

public class PlayerAmountEventArgs : EventArgs
{
    public int Amount { get; set; }

    public PlayerAmountEventArgs(int n)
    {
        Amount = n;
    }
}

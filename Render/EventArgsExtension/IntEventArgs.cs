namespace MonopolyGame.Render.EventArgsExtension;

public class IntEventArgs : EventArgs
{
    public int Amount { get; set; }

    public IntEventArgs(int n)
    {
        Amount = n;
    }
}

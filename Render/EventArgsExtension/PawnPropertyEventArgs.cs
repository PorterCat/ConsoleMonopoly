using MonopolyGame.GameObjects;

namespace MonopolyGame.Render.EventArgsExtension;

public class PawnPropertyEventArgs : EventArgs
{
    public Property Property { get; set; }

    public PawnPropertyEventArgs(Property property)
    {
        Property = property;
    }
}

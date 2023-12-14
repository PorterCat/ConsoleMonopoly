using MonopolyGame.GameObjects;

namespace MonopolyGame.Render.EventArgsExtension;

public class PropertyEventArgs : EventArgs
{
    public Property Property { get; set; }

    public PropertyEventArgs(Property property)
    {
        Property = property;
    }
}

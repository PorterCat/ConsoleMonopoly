using MonopolyGame.GameObjects;
using MonopolyGame.Render.EventArgsExtension;

namespace MonopolyGame.Render.InerfaceElements;

public class Button : IRenderable
{
    public string? Name { get; set; }
    public (int x, int y) Position { get; set; }
    public event EventHandler<PawnPropertyEventArgs>? Click;
    public bool Selected = false;

    public IRenderable SetPosition(int x, int y)
    {
        Position = (x, y);
        return this;
    }

    public void Render()
    {
        Console.SetCursorPosition(Position.x, Position.y);
        if (Selected)
        {
            Console.BackgroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(Name);
            Console.ResetColor();
        }
        else
        {
            Console.Write(Name);
        }
    }

    public void OnClick()
    {
        Click?.Invoke(this, null);
    }

    public void OnClick(Property property)
    {
        Click?.Invoke(this, new PawnPropertyEventArgs(property));
    }
}

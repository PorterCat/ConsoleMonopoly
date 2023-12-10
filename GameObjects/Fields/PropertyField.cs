using System.Reflection;

namespace MonopolyGame.GameObjects.Fields;

public class PropertyField : BoardField
{
    public Property Property;

    public PropertyField(Property property)
    {
        Property = property;
        Name = property.Name;
        Index = property.Index;
    }

    public override void Render((int x, int y) Position)
    {
        base.Render(Position);
        Console.SetCursorPosition(Position.x + 1, Position.y + 2);
        Console.Write($"$: {Property.Price}");

        if( Property.Owner != null ) 
        {
            Console.SetCursorPosition(Position.x + 8, Position.y + 2);
            Console.WriteLine($"({Property.Owner.Avatar})");
        }
    }
}

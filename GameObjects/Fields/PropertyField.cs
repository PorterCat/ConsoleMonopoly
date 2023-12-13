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


        Console.SetCursorPosition(Position.x + 1, Position.y + 1);

        if(Property.Owner != null)
        {
            Console.ForegroundColor = Property.Owner.Color;
        }
        Console.Write(Name);
        Console.ResetColor();

        Console.SetCursorPosition(Position.x + 1, Position.y + 2);

        if( Property.Owner != null ) 
        {
            if (Property.Owner.pawnedProperty.ContainsKey(Property.Index))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(Position.x + 1, Position.y + 2);
                Console.Write($"Срок: {Property.Owner.pawnedProperty[Property.Index].Item2}");
                Console.ResetColor();
            }
            else
            {
                Console.SetCursorPosition(Position.x + 1, Position.y + 2);
                Console.Write($"Рента: {Property.Rent}$");
            }
            Console.ForegroundColor = Property.Owner.Color;
            Console.SetCursorPosition(Position.x + 1, Position.y + 3);
            Console.WriteLine($"({Property.Owner.Avatar})");
            Console.ResetColor();
        }
        else
        {
            Console.SetCursorPosition(Position.x + 1, Position.y + 2);
            Console.Write($"$: {Property.Price}");
        }
    }
}

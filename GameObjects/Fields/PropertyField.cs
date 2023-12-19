using MonopolyGame.Render.Windows;

namespace MonopolyGame.GameObjects.Fields;

public class PropertyField : BoardField
{
    public Property Property;

    public PropertyField(Property property)
    {
        Property = property;
        Name = property.Name;
        Index = property.Index;
        IsBuyable = true;
    }

    public override bool HandlePlayerOnField(Player player)
    {
        if(Property.Owner != null)
        {
            IsBuyable = false;
        }
        else
        {
            IsBuyable = true;
            return base.HandlePlayerOnField(player);
        }

        if (Property.Owner != player)
        {
            if (!Property.IsPawned)
            {
                player.PayRent(Property.Rent, Property.Owner);
                EventLoggerWindow.Record($"Игрок {player.Name} попал на поле игрока {Property.Owner.Name}. " +
                    $"Плата: {Property.Rent}$");
            }
            else
            {
                EventLoggerWindow.Record($"Игрок {player.Name} попал на поле игрока {Property.Owner.Name}. Но оно заложено");
            }
        }
        else if (Property.Owner == player)
        {
            EventLoggerWindow.Record($"Игрок {player.Name} попал на своё поле.");
        }
        
        return base.HandlePlayerOnField(player);
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

        Console.SetCursorPosition(Position.x + 1, Position.y + 3);
        Console.WriteLine($"({Property.Group + 1})");
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
            Console.WriteLine($"({Property.Group + 1})");
            Console.ResetColor();
        }
        else
        {
            Console.SetCursorPosition(Position.x + 1, Position.y + 2);
            Console.Write($"$: {Property.Price}");
        }
    }
}

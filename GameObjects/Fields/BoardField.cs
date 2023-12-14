namespace MonopolyGame.GameObjects.Fields;

public abstract class BoardField
{
    public bool IsBuyable = false; 

    public int Index { get; set; }
    public string Name { get; set; }
    public List<Player>? PlayersOnTheField = new List<Player>();

    public virtual void Render((int x, int y) Position)
    {
        (int x, int y) pointer = Position;
        Console.SetCursorPosition(pointer.x, pointer.y);
        Console.Write('#');

        Console.SetCursorPosition(pointer.x, pointer.y + 4);
        Console.Write('#');
        Console.SetCursorPosition(pointer.x + 12, pointer.y);
        Console.Write('#');
        Console.SetCursorPosition(pointer.x + 12, pointer.y + 4);
        Console.Write('#');

        pointer = Position;
        pointer.x++;

        for (int i = 0; i < 11; i++)
        {
            Console.SetCursorPosition(pointer.x, pointer.y);
            Console.Write('=');
            Console.SetCursorPosition(pointer.x, pointer.y + 4);
            Console.Write('=');
            pointer.x++;
        }

        pointer = Position;
        pointer.y++;

        for (int i = 0; i < 3; i++)
        {
            Console.SetCursorPosition(pointer.x, pointer.y);
            Console.Write('|');
            Console.SetCursorPosition(pointer.x + 12, pointer.y);
            Console.Write('|');
            pointer.y++;
        }

        pointer = Position;
        Console.SetCursorPosition(pointer.x + 1, pointer.y + 1);
        Console.Write(Name);
        if(PlayersOnTheField != null)
        {
            Console.SetCursorPosition(pointer.x + 8, pointer.y + 3);

            foreach (var player in PlayersOnTheField)
            {
                Console.ForegroundColor = player.Color;
                Console.Write(player.Avatar);
            }
            Console.ResetColor();
        }
    }

    public virtual bool HandlePlayerOnField(Player player)
    {
        return IsBuyable;
    }
}

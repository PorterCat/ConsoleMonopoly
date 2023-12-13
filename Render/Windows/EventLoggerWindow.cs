namespace MonopolyGame.Render.Windows;

public static class EventLoggerWindow
{
    public const int N = 33;
    public static Queue<string> Events = new Queue<string>(N);
    
    public static void Render()
    {
        while(Events.Count > 33)
        { 
            Events.Dequeue();
        }

        (int x, int y) pointer = (75, 6);
        Console.SetCursorPosition(pointer.x, pointer.y);
        Console.Write(">");
        pointer.x += 3;
        var stack = new Stack<string>(Events);

        while(stack.Count > 0) 
        {
            Console.SetCursorPosition(pointer.x, pointer.y);
            Console.Write(stack.Pop());
            pointer.y++;
        }
    }
}

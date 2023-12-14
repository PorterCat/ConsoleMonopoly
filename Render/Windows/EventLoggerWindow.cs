namespace MonopolyGame.Render.Windows;

public static class EventLoggerWindow
{
    public const int N = 33;
    private static Queue<string> _events = new Queue<string>(N);
    
    public static void Record(string line)
    {
        _events.Enqueue(line);
    }

    public static void Render()
    {
        while(_events.Count > 33)
        { 
            _events.Dequeue();
        }

        (int x, int y) pointer = (75, 6);
        Console.SetCursorPosition(pointer.x, pointer.y);
        Console.Write(">");
        pointer.x += 3;
        var stack = new Stack<string>(_events);

        while(stack.Count > 0) 
        {
            Console.SetCursorPosition(pointer.x, pointer.y);
            Console.Write(stack.Pop());
            pointer.y++;
        }
    }
}

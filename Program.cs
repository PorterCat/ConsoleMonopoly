using MonopolyGame.Controller;
using MonopolyGame.Factory;
using MonopolyGame.GameObjects;
using MonopolyGame.Render.Windows;

internal class Program
{
    private static void Main(string[] args)
    {
        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
        {
            Console.SetBufferSize(512, 512);
            Console.SetWindowSize(200, 50);
        }

        Console.ForegroundColor = ConsoleColor.White;

        Console.CursorVisible = false;

        GameController.StartGame();

        Console.ReadLine();
    }


}
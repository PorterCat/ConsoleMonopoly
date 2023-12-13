using MonopolyGame.GameObjects;
using MonopolyGame.Render.Windows;
using System.Xml.Linq;

namespace MonopolyGame.Controller;

public static class GameController
{
    public static List<Player> Players;

    private static bool _isPlay = true;

    public static void StartGame()
    {

        Players = new List<Player>
        {
            new Player() { Name = "Андрей", Avatar = "А", Color = ConsoleColor.Red },
            new Player() { Name = "Никита", Avatar = "Н", Color = ConsoleColor.Green }
        };

        /*var menuWindow = new MenuWindow();
        menuWindow.Render();*/

        Board.SetPlayers(Players);

        EventLoggerWindow.Events.Enqueue("Начало игры");
        while ( _isPlay )
        {
            foreach ( var player in Players)
            {
                player.MakeStep();
                CheckGameStatus();
            }

        }
        Console.WriteLine("Игра окончена");
    }

    public static void FinishGame()
    {
        _isPlay = false;
    }

    private static void CheckGameStatus()
    {
        if(Players.Count == 1) 
        {
            _isPlay = false;
            EventLoggerWindow.Events.Enqueue($"Игрок {Players[0].Name} одержал победу!");
        }
    }
}

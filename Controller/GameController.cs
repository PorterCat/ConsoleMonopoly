using MonopolyGame.GameObjects;
using MonopolyGame.GameObjects.Fields;
using MonopolyGame.Render.Windows;

namespace MonopolyGame.Controller;

public static class GameController
{
    public static List<Player> Players = new List<Player>();

    private static bool _isPlay = true;

    public static void StartGame()
    {
        var menuWindow = new MenuWindow();
        menuWindow.Render();

        Board.SetPlayers(Players);

        EventLoggerWindow.Record("Начало игры");
        while ( _isPlay )
        {
            foreach ( var player in Players)
            {
                player.MakeStep();
                CheckGameStatus();
                if(!_isPlay)
                {
                    break;
                }
            }

        }
        Console.Clear();
        BoardWindow boardWindow = new BoardWindow();
        boardWindow.Render();
        EventLoggerWindow.Render();
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
            EventLoggerWindow.Record($"Игрок {Players[0].Name} одержал победу!");
            EventLoggerWindow.Record($"Конец игры");
        }
    }
}

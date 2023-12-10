using MonopolyGame.GameObjects;
using MonopolyGame.Render.Windows;

namespace MonopolyGame.Controller;

public class GameController
{
    private List<Player> _players;

    private bool _isPlay = true;

    public GameController()
    {
        _players = new List<Player>();


        var menuWindow = new MenuWindow(_players);
        menuWindow.Render();

        _players[0].Color = ConsoleColor.Red;
        _players[1].Color = ConsoleColor.Blue;

        Board.SetPlayers(_players);

        if(!menuWindow.ExitQ)
        {
            EventLoggerWindow.Events.Enqueue("Начало игры");          
        }
    }

    public void StartGame()
    {
        while( _isPlay )
        {
            foreach( var player in _players)
            {
                MakeStep(player);
            }

        }
    }

    private void MakeStep(Player player)
    {
        var GamePlayerMenu = new GameWindow(player);
        GamePlayerMenu.Render();
    }

    private void CheckGameStatus()
    {
        
    }
}

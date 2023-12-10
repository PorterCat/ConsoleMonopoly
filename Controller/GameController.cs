using MonopolyGame.Factory;
using MonopolyGame.GameObjects;
using MonopolyGame.Render.Windows;

namespace MonopolyGame.Controller;

public class GameController
{
    private Board _board;
    private Dice _dice;
    private List<Player> _players;
    private BoardWindow _boardWindow;

    private bool _isPlay = true;

    public GameController()
    {
        _players = new List<Player>();

        var menuWindow = new MenuWindow(_players);
        menuWindow.Render();

        if(!menuWindow.ExitQ)
        {
            _board = new Board(_players);
            _dice = new Dice(12);

            _boardWindow = new BoardWindow(_board);
            _boardWindow.Render();
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

    public void MakeStep(Player player)
    {
        var GamePlayerMenu = new GamePlayerMenu(player, _board, _dice, _boardWindow);
        GamePlayerMenu.Render();
    }

    public void CheckGameStatus()
    {

    }
}

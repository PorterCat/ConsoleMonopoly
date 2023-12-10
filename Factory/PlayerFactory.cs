using MonopolyGame.GameObjects;
using MonopolyGame.Render.Windows;

namespace MonopolyGame.Factory;

public class PlayerFactory : IFactory
{
    public Board CreateBoard(List<Player> players)
    {
        throw new NotImplementedException();
    }

    public Dice CreateDice()
    {
        throw new NotImplementedException();
    }

    public Player CreatePlayer()
    {
        var createPlayerWindow = new CreatePlayerWindow();
        createPlayerWindow.Render();
        return createPlayerWindow.GetPlayer();
    }
}

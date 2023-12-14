using MonopolyGame.GameObjects;
using MonopolyGame.Render.Windows;

namespace MonopolyGame.Factory;

public class PlayerFactory : IFactory
{
    public Player CreatePlayer()
    {
        var createPlayerWindow = new CreatePlayerWindow();
        createPlayerWindow.Render();
        return createPlayerWindow.GetPlayer();
    }
}

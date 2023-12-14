using MonopolyGame.GameObjects;

namespace MonopolyGame.Factory;

public interface IFactory
{
    Player CreatePlayer();
}

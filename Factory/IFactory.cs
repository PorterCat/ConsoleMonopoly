using MonopolyGame.GameObjects;

namespace MonopolyGame.Factory;

public interface IFactory
{
    Board CreateBoard(List<Player> players);
    Player CreatePlayer();
    Dice CreateDice();
}

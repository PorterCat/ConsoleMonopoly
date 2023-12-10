namespace MonopolyGame.GameObjects.Fields;

public class StartField : BoardField
{
    public void GivePrize(Player player)
    {
        player.Balance += 200;
    }
}

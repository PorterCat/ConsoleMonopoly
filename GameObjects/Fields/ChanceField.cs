namespace MonopolyGame.GameObjects.Fields;

public class ChanceField : BoardField
{
    public override bool HandlePlayerOnField(Player player)
    {
        player.TakeCardFromChanceDeck();
        return base.HandlePlayerOnField(player);
    }
}

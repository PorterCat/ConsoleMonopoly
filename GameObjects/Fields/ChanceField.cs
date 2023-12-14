namespace MonopolyGame.GameObjects.Fields;

public class ChanceField : BoardField
{
    public override bool HandlePlayerOnField(Player player)
    {
        ChanceDeck.SetPlayer(player);
        player.TakeCardFromChanceDeck();
        return base.HandlePlayerOnField(player);
    }
}

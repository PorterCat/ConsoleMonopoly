namespace MonopolyGame.GameObjects.Fields;

public class TaxField : BoardField
{
    public void PayTax(Player player)
    {
        player.Balance -= 150;
    }
}

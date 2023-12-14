using MonopolyGame.Render.Windows;

namespace MonopolyGame.GameObjects.Fields;

public class TaxField : BoardField
{
    private int _taxAmount;

    public TaxField(int taxAmout)
    {
        _taxAmount = taxAmout;
    }

    public override bool HandlePlayerOnField(Player player)
    {
        EventLoggerWindow.Record($"Игрок {player.Name} платит налог {_taxAmount}$");
        player.Pay(_taxAmount);
        return base.HandlePlayerOnField(player);
    }
}

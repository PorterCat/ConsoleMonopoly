namespace MonopolyGame.GameObjects.Fields;

public class TaxField : BoardField
{
    private int _taxAmount;

    public TaxField(int taxAmout)
    {
        _taxAmount = taxAmout;
    }

    public void PayTax(Player player)
    {
        player.Balance -= _taxAmount;
    }

    public override void Render((int x, int y) Position)
    {
        base.Render(Position);
        Console.SetCursorPosition(Position.x + 1, Position.y + 2);
        Console.Write($"{_taxAmount}$");
    }
}

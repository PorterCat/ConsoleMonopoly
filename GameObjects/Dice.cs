namespace MonopolyGame.GameObjects;

public class Dice
{
    private Random _random { get; set; }
    private uint _sides { get; set; }

    public Dice(uint sides)
    {
        _random = new Random();
        _sides = sides;
    }

    public int Roll()
    {
        return _random.Next(2, (int)_sides+1);
    }
}

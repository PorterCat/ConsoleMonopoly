namespace MonopolyGame.GameObjects;

public static class Dice
{
    private static Random _random = new Random();

    public static int Roll()
    {
        return _random.Next(1, 7);
    }
}

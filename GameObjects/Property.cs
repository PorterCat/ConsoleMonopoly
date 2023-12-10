using MonopolyGame.GameObjects.Fields;
using System.Drawing;

namespace MonopolyGame.GameObjects;

public class Property
{
    public string Name { get; set; }
    public int Index { get; set; }
    public int Price { get; set; }
    public int Rent { get; set; }
    public int Level { get; set; }
    public bool IsPawned = false;
    public Player? Owner { get; set; }

    public Property(string name, int price)
    {
        Name = name;
        Price = price;
        Level = 1;
        Owner = null;
    }

    public void Upgrade()
    {

    }
}

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
    public bool IsPawned { get; set; }
    public Player? Owner { get; set; }

    public Property(string name, int price)
    {
        IsPawned = false; 
        Name = name;
        Price = price;
        Level = 1;
        Owner = null;
    }

    public void Upgrade()
    {

    }
}

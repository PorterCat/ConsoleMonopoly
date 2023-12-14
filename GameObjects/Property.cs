namespace MonopolyGame.GameObjects;

public class Property
{
    private int _level = 1;

    public bool IsPossibleToUpgrade { get; set; }
    public string Name { get; set; }
    public int Index { get; set; }
    public int Price { get; set; }
    public int Group { get; set; }
    public int UpgradeCost { get; set; }
    public int Rent { get; set; }
    public int Level 
    {
        get
        { 
            return _level; 
        }
        set
        {
            if(value < 1)
            {
                _level = 1;
            }
            else if(value > 5) 
            {
                _level = 5;
            }
            else
            {
                _level = value;
            }
        }
    }
    public bool IsPawned { get; set; }
    public Player? Owner { get; set; }

    public Property(string name, int price)
    {
        IsPossibleToUpgrade = false;
        IsPawned = false; 
        Name = name;
        Price = price;
        Level = 1;
        Owner = null;
    }

    public void Upgrade()
    {
        if(Level < 5)
        {
            Level++;
            Price += UpgradeCost;
            Owner.Pay(UpgradeCost);
            Rent = 2 * Rent;
        }       
    }

    public void Degrade()
    {
        Level--;
        Price -= UpgradeCost;
        Rent = Rent / 2;
    }
}

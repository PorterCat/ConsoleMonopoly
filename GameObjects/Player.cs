using MonopolyGame.Render.Windows;

namespace MonopolyGame.GameObjects;

public class Player
{
    private int _poistion = 0;
    private int _balance = 500;

    public int Index { get; set; }
    public string Name { get; set; }
    public string Avatar { get; set; }
    public int Balance
    {
        get { return _balance;  }
        set
        {
            _balance = value;
            if ( _balance < 0)
            {
                PawnProperty(Properties);
            }
        }
    }
    public int Position
    {
        get { return _poistion; }
        set 
        { 
            if(value >= 40)
            {
                _poistion = value - 40;
                Balance += 200;
            }     
            else
            {
                _poistion = value;
            }    
        }
    }
    public List<Property>? Properties { get; set; }
    public bool IsPrisioned { get; set; }

    public Player()
    {
        Balance = 500;
        Properties = new List<Property>();
        IsPrisioned = false;
    }

    public int Dice(Dice dice)
    {
        return dice.Roll();
    }

    public void Move(int steps)
    {
        Position += steps;
    }

    public void Buy(Property property)
    {
        
        Balance -= property.Price;
        property.Owner = this;
        
    }

    public void TakeChanceCard()
    {

    }

    public void PayRent(int amount, Player player)
    {      
        Balance -= amount;
        player.Balance += amount;
    }

    private void PawnProperty(List<Property> properties)
    {
        var pawnProperty = new PawnPropertyWindow(properties);
        pawnProperty.Render();
    }
}

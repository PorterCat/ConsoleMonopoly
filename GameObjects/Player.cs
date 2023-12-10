using MonopolyGame.GameObjects.Fields;
using MonopolyGame.Render.Windows;

namespace MonopolyGame.GameObjects;

public class Player
{
    private int _poistion = 0;
    private int _balance = 500;

    public ConsoleColor Color = ConsoleColor.White;

    public Dictionary<int, (Property, int)> pawnedProperty = new(40);


    public int Steps { get; set; }
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
                PawnBuyProperty();
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
                EventLoggerWindow.Events.Enqueue($"{Name} прошёл поле и получает 200$");
            }     
            else
            {
                _poistion = value;
            }    
        }
    }
    public List<Property>? Properties { get; set; }
    public int PrisonTerm { get; set; }

    public Player()
    {
        PrisonTerm = 0;
        Balance = 500;
        Properties = new List<Property>();
    }

    public void ReducingTerm()
    {
        if(pawnedProperty.Count > 0)
        {
            foreach(var key in pawnedProperty.Keys.ToList())
            {
                var value = pawnedProperty[key];
                pawnedProperty[key] = (value.Item1, value.Item2 - 1);
                if (value.Item2 == 0)
                {
                    EventLoggerWindow.Events.Enqueue($"Игрок {Name} не успел выкупить {value.Item1.Name}. Теперь можно ее купить");
                    value.Item1.IsPawned = false;
                    value.Item1.Owner = null;
                    pawnedProperty.Remove(key);
                }
            }
        }
    }

    public void Move(int steps)
    {
        Board.BoardFields[Position].PlayersOnTheField.Remove(this);
        Position += steps;
        Board.BoardFields[Position].PlayersOnTheField.Add(this);
    }

    public void SendToJail()
    {
        Board.BoardFields[_poistion].PlayersOnTheField.Remove(this);
        _poistion = 10;
        PrisonTerm = 3;
        Board.BoardFields[_poistion].PlayersOnTheField.Add(this);
    }

    public void Pay(int loss)
    {
        Balance -= loss;
    }

    public void Buy(Property property)
    {    
        Balance -= property.Price;
        property.Owner = this;
        Properties.Add(property);
    }

    public void PayRent(int amount, Player player)
    {      
        Balance -= amount;
        player.Balance += amount;
    }

    public void PawnBuyProperty()
    {
        var pawnProperty = new PawnPropertyWindow(this, Properties);
        pawnProperty.Render();
    }
}

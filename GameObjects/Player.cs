using MonopolyGame.Controller;
using MonopolyGame.Render.Windows;

namespace MonopolyGame.GameObjects;

public class Player
{
    private int _poistion = 0;
    private int _balance = 500;

    public ConsoleColor Color { get; set; }

    public Dictionary<int, (Property, int)> pawnedProperty = new(40);

    public int Steps { get; set; }
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

                foreach (var group in Properties)
                {
                    if (group.Count > 0 && group.Any(c => c.IsPawned == false))
                    {
                        PawnBuyProperty();
                        break;
                    }
                }               

                if (_balance < 0)
                {
                    GameController.Players.Remove(this);
                }         
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
                EventLoggerWindow.Record($"{Name} прошёл поле и получает 200$");
            }     
            else
            {
                _poistion = value;
            }    
        }
    }
    public List<List<Property>> Properties { get; set; }
    public int PrisonTerm { get; set; }

    public Player()
    {
        PrisonTerm = 0;
        Balance = 500;
        Properties = new List<List<Property>>(8)
        { 
            new List<Property>(2),
            new List<Property>(3),
            new List<Property>(3),
            new List<Property>(3),
            new List<Property>(3),
            new List<Property>(3),
            new List<Property>(3),
            new List<Property>(2),
        };
    }

    public void MakeStep()
    {
        var GamePlayerMenu = new GameWindow(this);
        GamePlayerMenu.Render();
    }

    public void TakeCardFromChanceDeck()
    {
        var textEvent = ChanceDeck.GetCard();
        EventLoggerWindow.Record($"{Name}: {textEvent}");
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
                    EventLoggerWindow.Record($"Игрок {Name} не успел выкупить {value.Item1.Name}. Теперь можно ее купить");
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
        Board.BoardFields[Position].PlayersOnTheField.Remove(this);
        Position = 10;
        PrisonTerm = 3;
        Steps = 0;
        Board.BoardFields[Position].PlayersOnTheField.Add(this);
    }

    public void Pay(int loss)
    {
        Balance -= loss;
    }

    public void Get(int profit)
    {
        Balance += profit;
    }

    public void Buy(Property property)
    {    
        Balance -= property.Price;
        property.Owner = this;
        Properties[property.Group].Add(property);
    }

    public void IsPropertyRelatedToMonopolyGroup()
    {
        for(int i = 0; i < Properties.Count; i++)
        {
            if (Properties[i].Count == Properties[i].Capacity)
            {
                foreach (var prop in Properties[i])
                {
                    prop.IsPossibleToUpgrade = true;                
                }
            }
        }
    }

    public void PayRent(int amount, Player toPlayer)
    {      
        Pay(amount);
        toPlayer.Balance += amount;
    }

    public void PawnBuyProperty()
    {
        var pawnProperty = new PawnPropertyWindow(this, Properties);
        pawnProperty.Render();
    }
}

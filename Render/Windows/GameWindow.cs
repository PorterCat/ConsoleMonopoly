    using MonopolyGame.Controller;
using MonopolyGame.GameObjects;
using MonopolyGame.GameObjects.Fields;
using MonopolyGame.Render.InerfaceElements;

namespace MonopolyGame.Render.Windows;

public class GameWindow : IRenderable
{
    private List<Button> _menuActions;
    private BoardWindow _boardWindow;

    private Button buttonDie;
    private Button buttonBuy;
    private Button listOfProperty;
    private Button finishButton;

    private bool _isInitilized = true;
    private Player _player;

    public GameWindow(Player player)
    {
        _boardWindow = new BoardWindow();
        _player = player;
        _player.Steps = 1;
    }

    public void Render()
    {
        for (int i = 0; i < _player.Steps; i++)
        {
            if (_player.PrisonTerm > 0)
            {
                _player.PrisonTerm--;
            }
            _player.IsPropertyRelatedToMonopolyGroup();

            buttonDie = new Button()
            {
                Name = "Кинуть кубик",
            };

            buttonBuy = new Button()
            {
                Name = "Купить",
            };

            listOfProperty = new()
            {
                Name = "Посмотреть имущество",
            };

            finishButton = new()
            {
                Name = "Закончить ход",
            };

            listOfProperty.Click += OpenListOfProperty;
            buttonDie.Click += Die;
            buttonBuy.Click += Buy;
            finishButton.Click += Exit;

            _menuActions = new List<Button>();
            _boardWindow.Render();
            EventLoggerWindow.Render();


            
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"Ходит игрок: {_player.Name}({_player.Avatar}) Баланс: {_player.Balance}");
            

            if (_player.PrisonTerm > 0)
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine($"Ходит игрок {_player.Name}. Вы в тюрьме ещё {_player.PrisonTerm + 1} хода");
                var jailMenu = new List<Button>();
                var escapeButton = new Button()
                {
                    Name = "Попробовать сбежать",
                };
                escapeButton.Click += Escape;

                _menuActions.Add(escapeButton);

                var payButton = new Button()
                {
                    Name = "Заплатить 50$",
                };
                payButton.Click += Pay;

                _menuActions.Add(payButton);
                _menuActions.Add(finishButton);
            }
            else
            {
                _menuActions.Add(buttonDie);
            }

            _menuActions.RenderWithDots((4, 2), 2);

            Console.Clear();
            while (_isInitilized)
            {
                _boardWindow.Render();
                EventLoggerWindow.Render();

                if (_player.Balance < 0)
                {
                    Console.SetCursorPosition(0, 0);

                    foreach (var group in _player.Properties)
                    {
                        if(group.Count > 0)
                        {
                            foreach(var prop in group)
                            {
                                prop.Owner = null;
                                prop.IsPawned = false;
                                prop.IsPossibleToUpgrade = false;
                                while(prop.Level > 1)
                                {
                                    prop.Degrade();
                                }
                            }
                        }
                    }

                    Console.WriteLine($"Игрок: {_player.Name}({_player.Avatar}) вы банкрот.");
                    EventLoggerWindow.Record($"Игрок {_player.Name} обанкротился и выбывает из игры");
                    Board.BoardFields[_player.Position].PlayersOnTheField.Remove(_player);
                    _menuActions.Remove(listOfProperty);
                    _menuActions.RenderWithDots((4, 2), 2);
                    return;
                }

                Console.SetCursorPosition(0, 0);
                Console.WriteLine($"Ходит игрок: {_player.Name}({_player.Avatar}) Баланс: {_player.Balance}");
                _menuActions.RenderWithDots((4, 2), 2);
                Console.Clear();
            }
            _menuActions.Clear();
            _isInitilized = true;
            _player.ReducingTerm();
        }
    }

    void Die(object sender, EventArgs e)
    {
        var result1 = Dice.Roll();
        var result2 = Dice.Roll();

        _player.Move(result1 + result2);
        
        string line = $"Игрок {_player.Name} бросает кубики. Выпало: {result1} и {result2}";
        if (result1 == result2)
        {
            line += $". Дубль: игрок делает ход ещё раз";
            _player.Steps++;
        }

        if (_player.Steps > 3)
        {
            _player.SendToJail();
            EventLoggerWindow.Record(line);
            EventLoggerWindow.Record($"Игрок {_player.Name} арестован за махинации над кубиками");
            _isInitilized = false;
            _player.Steps = 0;
            return;
        }

        EventLoggerWindow.Record(line);
        
        _menuActions.Clear();

        if(Board.BoardFields[_player.Position].HandlePlayerOnField(_player))
        {
            _menuActions.Add(buttonBuy);
        }

        _menuActions.Add(listOfProperty);
        _menuActions.Add(finishButton);

    }

    void Pay(object sender, EventArgs e)
    {
        _menuActions.Clear();
        _menuActions.Add(buttonDie);
        _player.Pay(50);
        _player.PrisonTerm = 0;
        EventLoggerWindow.Record($"Игрок {_player.Name} выкупил свою свободу.");
    }

    void Escape(object sender, EventArgs e)
    {
        var result1 = Dice.Roll();
        var result2 = Dice.Roll();
                
        if (result1 == result2)
        {
            _player.PrisonTerm = 0;
            EventLoggerWindow.Record($"Игрок {_player.Name} сбегает! Выпало: {result1} и {result2}");
            _player.Move(result1+result2);
        }
        else
        {
            EventLoggerWindow.Record($"Игрок {_player.Name} предпринимает безуспешную попытку сбежать");
            _player.Steps = 0;
            _isInitilized = false;
            return;
        }

        _menuActions.Clear();      
    }

    void Buy(object sender, EventArgs e)
    {
        var propertyField = (PropertyField)Board.BoardFields[_player.Position];
        if (_player.Balance >= propertyField.Property.Price)
        {
            _player.Buy(propertyField.Property);
            EventLoggerWindow.Record($"Игрок {_player.Name} приобрёл {propertyField.Name}");
            _menuActions.RemoveAt(0);
        }
        else
        {
            EventLoggerWindow.Record($"Недостаточно средств");
        }
    }

    void OpenListOfProperty(object sender, EventArgs e)
    {
        Console.Clear();
        EventLoggerWindow.Render();
        var listOfProperty = new ListOfPropertyWindow(_player);
        listOfProperty.Render();
    }

    void Exit(object sender, EventArgs e)
    {
        _isInitilized = false;
    }
}

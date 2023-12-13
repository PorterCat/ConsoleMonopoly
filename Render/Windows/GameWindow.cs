using MonopolyGame.GameObjects;
using MonopolyGame.GameObjects.Fields;
using MonopolyGame.Render.InerfaceElements;
using System.Numerics;

namespace MonopolyGame.Render.Windows;

public class GameWindow : IRenderable
{
    private List<Button> _menuActions;
    private BoardWindow _boardWindow;

    private Button buttonDie;
    private Button buttonBuy;
    private Button pawnBuyButton;
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
            buttonDie = new Button()
            {
                Name = "Кинуть кубик",
            };

            buttonBuy = new Button()
            {
                Name = "Купить",
            };

            pawnBuyButton = new()
            {
                Name = "Заложить/выкупить имущство",
            };

            finishButton = new()
            {
                Name = "Закончить ход",
            };

            pawnBuyButton.Click += PawnBuy;
            buttonDie.Click += Die;
            buttonBuy.Click += Buy;
            finishButton.Click += Exit;

            _menuActions = new List<Button>();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"Ходит игрок: {_player.Name}({_player.Avatar}) Баланс: {_player.Balance}");
            _boardWindow.Render();
            EventLoggerWindow.Render();

            if (_player.PrisonTerm > 0)
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine($"Ходит игрок {_player.Name}. Вы в тюрьме ещё {_player.PrisonTerm} ходов");
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
                Console.SetCursorPosition(0, 0);
                Console.WriteLine($"Ходит игрок: {_player.Name}({_player.Avatar}) Баланс: {_player.Balance}");
                _boardWindow.Render();
                EventLoggerWindow.Render();
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
        
        string line = $"Игрок {_player.Name} бросает кубики. Выпало: {result1} и {result2}";
        if (result1 == result2)
        {
            line += $". Дубль: игрок делает ход ещё раз";
            _player.Steps++;
        }

        _player.Move(result1 + result2);

        if (_player.Steps > 3)
        {
            _player.SendToJail();
            EventLoggerWindow.Events.Enqueue(line);
            EventLoggerWindow.Events.Enqueue($"Игрок {_player.Name} арестован за махинации над кубиками");
            _isInitilized = false;
            _player.Steps = 0;
            return;
        }

        EventLoggerWindow.Events.Enqueue(line);
        

        _menuActions.Clear();

        if (Board.BoardFields[_player.Position] is ChanceField)
        {
            ChanceDeck.SetPlayer(_player);
            EventLoggerWindow.Events.Enqueue($"{_player.Name}: {ChanceDeck.GetCard()}");
        }
        else if (Board.BoardFields[_player.Position] is TaxField)
        {
            _player.Pay(150);
            EventLoggerWindow.Events.Enqueue($"Ужас! Игрок {_player.Name} должен заплатить налог в размере 150$");
        }
        else if (Board.BoardFields[_player.Position] is FreeParkingField)
        {
            EventLoggerWindow.Events.Enqueue($"Игрок {_player.Name} попал на бесплатную парковку. Отдохните :)");
        }
        else if (Board.BoardFields[_player.Position] is GoToJailField)
        {
            _player.SendToJail();
            _isInitilized = false;
            EventLoggerWindow.Events.Enqueue($"Игрок {_player.Name} отправляется в тюрьму");
            _player.Steps = 1;
        }
        else if (Board.BoardFields[_player.Position] is JailField)
        {
            EventLoggerWindow.Events.Enqueue($"Игрок {_player.Name} посещает тюрьму как посетитель");
        }
        else if (Board.BoardFields[_player.Position] is PropertyField)
        {
            var propertyField = Board.BoardFields[_player.Position] as PropertyField;

            if (propertyField.Property.Owner == null)
            {
                _menuActions.Add(buttonBuy);
            }
            else
            {
                if (propertyField.Property.Owner != _player)
                {
                    if (propertyField.Property.Owner.pawnedProperty.Count == 0)
                    {
                        _player.PayRent(propertyField.Property.Rent, propertyField.Property.Owner);
                        EventLoggerWindow.Events.Enqueue($"Игрок {_player.Name} попал на поле игрока {propertyField.Property.Owner.Name}. " +
                            $"Плата: {propertyField.Property.Rent}$");
                    }
                    else
                    {
                        EventLoggerWindow.Events.Enqueue($"Игрок {_player.Name} попал на поле игрока {propertyField.Property.Owner.Name}. Но оно заложено");
                    }
                }
                else
                {
                    EventLoggerWindow.Events.Enqueue($"Игрок {_player.Name} попал на своё поле.");
                }
            }
        }

        _menuActions.Add(pawnBuyButton);
        _menuActions.Add(finishButton);

    }

    void Pay(object sender, EventArgs e)
    {
        _menuActions.Clear();
        _menuActions.Add(buttonDie);
        _player.Pay(50);
        _player.PrisonTerm = 0;
        EventLoggerWindow.Events.Enqueue($"Игрок {_player.Name} выкупил свою свободу.");
    }

    void Escape(object sender, EventArgs e)
    {
        var result1 = Dice.Roll();
        var result2 = Dice.Roll();
                
        if (result1 == result2)
        {
            _player.PrisonTerm = 0;
            EventLoggerWindow.Events.Enqueue($"Игрок {_player.Name} сбегает! Выпало: {result1} и {result2}");
            _player.Move(result1+result2);
        }
        else
        {
            EventLoggerWindow.Events.Enqueue($"Игрок {_player.Name} предпринимает безуспешную попытку сбежать");
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
            EventLoggerWindow.Events.Enqueue($"Игрок {_player.Name} приобрёл {propertyField.Name}");
            _menuActions.RemoveAt(0);
        }
        else
        {
            EventLoggerWindow.Events.Enqueue($"Недостаточно средств");
        }
    }

    void PawnBuy(object sender, EventArgs e)
    {
        Console.Clear();
        EventLoggerWindow.Render();
        _player.PawnBuyProperty();
    }

    void Exit(object sender, EventArgs e)
    {
        _isInitilized = false;
    }
}

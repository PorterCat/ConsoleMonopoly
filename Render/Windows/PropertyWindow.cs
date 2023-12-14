using MonopolyGame.GameObjects;
using MonopolyGame.Render.InerfaceElements;

namespace MonopolyGame.Render.Windows;

public class PropertyWindow : IRenderable
{
    private Property _property;
    private Player _player;
    private List<Button> _buttons = new List<Button>();
    private bool _isInitilized = true;
    private Button finishButton;
    private Button upgradeButton;
    private Button pawnPropertyButton;

    public PropertyWindow(Property property, Player player)
    {
        finishButton = new Button() { Name = "Назад" };
        finishButton.Click += Exit;

        _property = property;
        pawnPropertyButton = new Button();
        if (property.IsPawned == true)
        {
            pawnPropertyButton.Name = $"Выкупить за {property.Price}";
        }
        else
        {
            pawnPropertyButton.Name = $"Заложить за {(int)(property.Price * 0.65)}";
        }

        pawnPropertyButton.Click += BuyBackPawn;

        _buttons.Add(pawnPropertyButton);

        if(property.IsPossibleToUpgrade && !(property.IsPawned) && property.Level < 6)
        {
            upgradeButton = new Button()
            {
                Name = $"Улучшить за {_property.UpgradeCost}$"
            };
            upgradeButton.Click += Upgrade;
            _buttons.Add(upgradeButton);
        }

        _buttons.Add(finishButton);
        _player = player;
    }

    public void Render()
    {
        Console.Clear();
        var boardWindow = new BoardWindow();

        while(_isInitilized)
        {
            boardWindow.Render();
            EventLoggerWindow.Render();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"Ходит игрок: {_player.Name}({_player.Avatar}) Баланс: {_player.Balance}");
            Console.WriteLine($"\n{_property.Name} | Цена: {_property.Price}");

            string line = string.Empty;

            for(int i = 0; i < 5; i++)
            {
                if(i >= _property.Level)
                {
                    line += "[*]";
                }
                else
                {
                    line += $"[{i+1}]";
                }    
            }

            Console.WriteLine($"\nУровень: {line}");
            _buttons.RenderWithDots((4, 6), 2);
            Console.Clear();
        }
    }

    private void BuyBackPawn(object sender, EventArgs e)
    {
        var button = sender as Button;

        if (_property.IsPawned)
        {
            button.Name = $"Заложить за {_property.Price * 0.7}";
            if (_property.Owner.Balance - _property.Price > 0)
            {
                _property.Owner.pawnedProperty.Remove(_property.Index);
                _property.IsPawned = false;
                _property.Owner.Balance -= _property.Price;

                EventLoggerWindow.Record($"Игрок {_property.Owner.Name} выкупил {_property.Name} обратно");
            }
            else
            {
                EventLoggerWindow.Record($"Недостаточно средств");
            }
        }
        else
        {
            if(_buttons.Contains(pawnPropertyButton))
            {
                _buttons.Remove(pawnPropertyButton);
            }

            if (_buttons.Contains(upgradeButton))
            {
                _buttons.Remove(upgradeButton);
            }

            button.Name = $"Выкупить за {_property.Price}";
            _property.IsPawned = true;
            _property.Owner.pawnedProperty[_property.Index] = (_property, 10);
            _property.Owner.Balance += (int)(_property.Price * 0.7);
            EventLoggerWindow.Record($"Игрок {_property.Owner.Name} заложил {_property.Name}");
        }
    }

    private void Upgrade(object sender, EventArgs e)
    {
        EventLoggerWindow.Record($"Игрок {_property.Owner.Name} улучшает {_property.Name}. Рента повысилась до {_property.Rent * 2}");
        _property.Upgrade();
        if (_buttons.Contains(upgradeButton))
        {
            _buttons.Remove(upgradeButton);
        }
        _property.IsPossibleToUpgrade = false;
    }

    private void Exit(object sender, EventArgs e)
    {
        _isInitilized = false;
    }
}

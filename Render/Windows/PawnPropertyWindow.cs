using MonopolyGame.GameObjects;
using MonopolyGame.Render.EventArgsExtension;
using MonopolyGame.Render.InerfaceElements;

namespace MonopolyGame.Render.Windows;

public class PawnPropertyWindow : IRenderable
{
    private bool _isInitilized = true;

    private List<Property>? _properties;
    private List<Button> _buttons = new List<Button>();
    private Player _player;

    public PawnPropertyWindow(Player player, List<Property> properties)
    {
        _player = player;
        if (properties != null)
        {
            _properties = properties;
        }
    }

    public void Render()
    {
        var boardWindow = new BoardWindow();

        while (_isInitilized)
        {
            Console.Clear();
            boardWindow.Render();
            EventLoggerWindow.Render();

            if (_buttons.Count > 0)
            {
                _buttons.Clear();
            }
            Console.SetCursorPosition(4, 2);
            if (_properties != null)
            {
                foreach (Property property in _properties)
                {
                    var line = property.Name;

                    if (property.IsPawned)
                    {
                        line += $" || Заложено. Выкуп: {property.Price}$";
                    }
                    else
                    {
                        line += $" || Заложить за {(int)(property.Price * 0.7)} $";
                    }

                    var button = new Button()
                    {
                        Name = line,
                    };

                    button.Click += (sender, e) =>
                    {
                        var n = new PawnPropertyEventArgs(property);
                        PawnBuyProperty(sender, n);
                    };
                    _buttons.Add(button);
                }
            }

            var buttonExit = new Button()
            {
                Name = "Готово",
            };
            buttonExit.Click += Exit;
            _buttons.Add(buttonExit);
            _buttons[0].Selected = true;

            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"Ходит игрок: {_player.Name}({_player.Avatar}) Баланс: {_player.Balance}");
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("  ==== Заложите/выкупите собственность: ====");

            _buttons.RenderWithDots((4, 4), 2);
        }
        Console.Clear();
    }

    private void PawnBuyProperty(object sender, PawnPropertyEventArgs property)
    {
        if(property.Property.IsPawned)
        {
            if(property.Property.Owner.Balance - property.Property.Price > 0)
            {
                property.Property.Owner.pawnedProperty.Remove(property.Property.Index);
                property.Property.IsPawned = false;
                property.Property.Owner.Balance -= property.Property.Price;
                
                EventLoggerWindow.Events.Enqueue($"Игрок {property.Property.Owner.Name} выкупил {property.Property.Name} обратно");
            }
            else
            {
                EventLoggerWindow.Events.Enqueue($"Недостаточно средств");
            }
        }
        else
        {
            property.Property.IsPawned = true;
            property.Property.Owner.pawnedProperty[property.Property.Index] = (property.Property, 10);
            property.Property.Owner.Balance += (int)(property.Property.Price * 0.7);
            EventLoggerWindow.Events.Enqueue($"Игрок {property.Property.Owner.Name} заложил {property.Property.Name}");
        }

    }

    private void Exit(object sender, EventArgs e)
    {
        _isInitilized = false;
    }
}

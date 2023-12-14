using MonopolyGame.GameObjects;
using MonopolyGame.Render.EventArgsExtension;
using MonopolyGame.Render.InerfaceElements;

namespace MonopolyGame.Render.Windows;

public class ListOfPropertyWindow : IRenderable
{
    private bool _isInitilized = true;
    private List<Button> _buttons = new List<Button>();
    private Player _player;

    public ListOfPropertyWindow(Player player)
    {
        _player = player;
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
            if (_player.Properties != null)
            {
                foreach(var groupProperty in _player.Properties)
                {
                    foreach(var property in groupProperty)
                    {
                        var button = new Button()
                        {
                            Name = property.Name,
                        };

                        button.Click += (sender, e) =>
                        {
                            var n = new PawnPropertyEventArgs(property);
                            ShowProperty(sender, n);
                        };
                        _buttons.Add(button);
                    }
                }
            }

            var buttonExit = new Button()
            {
                Name = "Готово",
            };
            buttonExit.Click += Exit;
            _buttons.Add(buttonExit);

            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"Ходит игрок: {_player.Name}({_player.Avatar}) Баланс: {_player.Balance}");

            _buttons.RenderWithDots((4, 2), 2);
        }
        Console.Clear();
    }

    public void ShowProperty(object sender, PawnPropertyEventArgs property)
    {
        var propertWindow = new PropertyWindow(property.Property, _player);
        propertWindow.Render();
    }

    public void Exit(object sender, EventArgs e)
    {
        _isInitilized = false;
    }
}

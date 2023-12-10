using MonopolyGame.GameObjects;
using MonopolyGame.Render.InerfaceElements;

namespace MonopolyGame.Render.Windows;

public class PawnPropertyWindow : IRenderable
{
    private bool _isInitilized = true;

    private List<Property>? _properties;
    private List<Button> _buttons;

    public PawnPropertyWindow(List<Property> properties)
    {
        _buttons = new List<Button>();
        if(properties == null)
        {
            _properties = properties;
            foreach (Property property in _properties)
            {
                var button = new Button()
                {
                    Name = property.Name,
                };
                button.Click += PawnProperty;
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
    }

    public void Render()
    {
        while(_isInitilized)
        {
            Console.WriteLine("==== Заложите собственность: ====");
            _buttons.RenderWithDots((4, 2), 2);
        }
        Console.Clear();
    }

    private void PawnProperty(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void Exit(object sender, EventArgs e)
    {
        _isInitilized = false;
    }
}

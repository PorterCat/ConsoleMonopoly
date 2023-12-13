using MonopolyGame.GameObjects;
using MonopolyGame.Render.InerfaceElements;

namespace MonopolyGame.Render.Windows;

public class CreatePlayerWindow : IRenderable
{
    private bool _isInitilized = true;
    private List<Button> _menuButtons;
    private Player _player = new Player(); 

    public CreatePlayerWindow()
    {
        _menuButtons = new List<Button>();

        var button1 = new Button()
        {
            Name = "Введите имя",
        };
        button1.Click += SetName;

        var button2 = new Button()
        {
            Name = "Введите аватар",
        };
        button2.Click += SetAvatar;

        var button3 = new Button()
        {
            Name = "Готово",
        };
        button3.Click += Exit;

        _menuButtons.Add(button1);
        _menuButtons.Add(button2);
        _menuButtons.Add(button3);
    }

    public void Render()
    {
        while( _isInitilized )
        {
            Console.WriteLine("Меню создания игрока");
            if(!string.IsNullOrEmpty(_player.Name))
            {
                Console.SetCursorPosition(20, 2);
                Console.WriteLine("[V]");
            }
            if (!string.IsNullOrEmpty(_player.Avatar))
            {
                Console.SetCursorPosition(20, 4);
                Console.WriteLine("[V]");
            }
            _menuButtons.RenderWithDots((4, 2), 2);
        }
        Console.Clear();
    }

    public Player GetPlayer()
    {
        return _player;
    }

    void SetName(object sender, EventArgs e)
    {
        var line = string.Empty;
        while(String.IsNullOrEmpty(line))
        {
            Console.Clear();
            Console.WriteLine("Введите имя: ");
            line = Console.ReadLine();
        }
        _player.Name = line;
        Console.Clear();
    }

    void SetAvatar(object sender, EventArgs e)
    {
        var line = string.Empty;
        while (String.IsNullOrEmpty(line) || line.Length > 1)
        {
            Console.Clear();
            Console.WriteLine("Введите символ для вашего автара: ");
            line = Console.ReadLine();
        }
        _player.Avatar = line;
        Console.Clear();
    }

    void Exit(object sender, EventArgs e)
    {
        if(!string.IsNullOrEmpty(_player.Name) && !string.IsNullOrEmpty(_player.Avatar))
        {
            _isInitilized = false;
        }
        else
        {
            Console.SetCursorPosition(0, 8);
            Console.WriteLine($"[!] Прежде чем нажимать готово, дозаполните информацию о вашем персонаже");
        }
    }

}

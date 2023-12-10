using MonopolyGame.Factory;
using MonopolyGame.GameObjects;
using MonopolyGame.Render.InerfaceElements;

namespace MonopolyGame.Render.Windows;

public class MenuWindow : IRenderable
{
    public bool ExitQ = false;

    private List<Button> _menuButtons;
    private List<Player> _players;
    private bool _isInitilized = true;

    public MenuWindow(List<Player> players)
    {
        _menuButtons = new List<Button>(2);
        var button1 = new Button();
        var button2 = new Button();

        button1.Name = "Начать игру на 2";
        button2.Name = "Выход";

        button1.Click += InitCreatePlayerWindow;
        button2.Click += Exit;

        _menuButtons.Add(button1);
        _menuButtons.Add(button2);
        _players = players;

    }

    public void Render()
    {
        while(_isInitilized)
        {
            Console.WriteLine("==== Монополия v0.1 ====");
            _menuButtons.RenderWithDots((4, 2), 2);
        }
        Console.Clear();
    }

    public List<Player> GetPlayers()
    {
        return _players;
    }

    void InitCreatePlayerWindow(object sender, EventArgs e)
    {
        Console.Clear();
        IFactory playerFactory = new PlayerFactory();
        for (int i = 0; i < 2; i++)
        {
            Console.SetCursorPosition(0, 10);
            Console.WriteLine($"Создайте {i+1}-ого игрока");
            Console.SetCursorPosition(0, 0);
            _players.Add(playerFactory.CreatePlayer());
        }
        _isInitilized = false;
    }

    void Exit(object sender, EventArgs e)
    {
        ExitQ = true;
        Console.Clear();
        _isInitilized = false;
    }
}

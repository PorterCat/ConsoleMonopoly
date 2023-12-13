using MonopolyGame.Controller;
using MonopolyGame.Factory;
using MonopolyGame.GameObjects;
using MonopolyGame.Render.InerfaceElements;

namespace MonopolyGame.Render.Windows;

public class MenuWindow : IRenderable
{
    private List<Button> _menuButtons;
    private bool _isInitilized = true;

    public MenuWindow()
    {
        _menuButtons = new List<Button>(2);
        var button1 = new Button();
        var button2 = new Button();
        var button3 = new Button();
        var button4 = new Button();

        button1.Name = "Начать игру на 2";
        button2.Name = "Начать игру на 3";
        button3.Name = "Начать игру на 4";
        button4.Name = "Выход";

        button1.Click += InitCreatePlayerWindow;
        button2.Click += InitCreatePlayerWindow;
        button3.Click += InitCreatePlayerWindow;
        button4.Click += Exit;

        _menuButtons.Add(button1);
        _menuButtons.Add(button2);
        _menuButtons.Add(button3);
        _menuButtons.Add(button4);
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

    void InitCreatePlayerWindow(object sender, EventArgs e)
    {
        Console.Clear();
        IFactory playerFactory = new PlayerFactory();

        var playerColors = new List<ConsoleColor>()
        {
            ConsoleColor.Red,
            ConsoleColor.Blue,
            ConsoleColor.Green, 
            ConsoleColor.Yellow,
        };

        for (int i = 0; i < 2; i++)
        {
            Console.SetCursorPosition(0, 10);
            Console.WriteLine($"Создайте {i+1}-ого игрока");
            Console.SetCursorPosition(0, 0);
            var player = playerFactory.CreatePlayer();
            player.Color = playerColors[i];
            GameController.Players.Add(player);
        }
        _isInitilized = false;
    }

    void Exit(object sender, EventArgs e)
    {
        GameController.FinishGame();
        Console.Clear();
        _isInitilized = false;
    }
}

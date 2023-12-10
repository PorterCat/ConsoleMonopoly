using MonopolyGame.GameObjects;
using MonopolyGame.GameObjects.Fields;
using MonopolyGame.Render.InerfaceElements;

namespace MonopolyGame.Render.Windows;

public class GamePlayerMenu : IRenderable
{
    private List<Button> _menuActions;
    private List<Button> _diceMenu;
    private BoardWindow _boardWindow;

    private bool _isInitilized = true;
    private Player _player;
    private Board _board;
    private Dice _dice;

    public GamePlayerMenu(Player player, Board board, Dice dice, BoardWindow boardWindow)
    {
        _menuActions = new List<Button>();

        _player = player;
        _board = board;
        _dice = dice;
        _boardWindow = boardWindow;

        _diceMenu = new List<Button>();
        var diceButton = new Button()
        {
            Name = "Кинуть кубик",
            Selected = true,
        };
        diceButton.Click += Die;
        _diceMenu.Add(diceButton);
    }

    public void Render()
    {
        Console.SetCursorPosition(0, 0);
        Console.WriteLine($"Ходит игрок: {_player.Name}({_player.Avatar}) Баланс: {_player.Balance}");
        _boardWindow.Render();
        _diceMenu.RenderWithDots((4, 2), 2);


        var button2 = new Button()
        {
            Name = "Заложить имущство",
            Selected = false,
        };

        button2.Click += Exit;
        _menuActions.Add(button2);

        var button3 = new Button()
        {
            Name = "Закончить ход",
            Selected = false,
        };

        button3.Click += Exit;
        _menuActions.Add(button3);

        Console.Clear();
        while (_isInitilized)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"Ходит игрок: {_player.Name}({_player.Avatar}) Баланс: {_player.Balance}");
            _boardWindow.Render();
            _menuActions.RenderWithDots((4, 2), 2);
            Console.Clear();
        }
    }

    void Die(object sender, EventArgs e)
    {
        var result = _player.Dice(_dice);
        var previousIndex = _player.Position;
        _player.Move(result);
        _board.BoardFields[previousIndex].PlayersOnTheField.Remove(_player);
        _board.BoardFields[_player.Position].PlayersOnTheField.Add(_player);

        if (_board.BoardFields[_player.Position] is ChanceField) //Игрок попал на поле шанс
        {
            _player.TakeChanceCard();
            return;
        }
        else if (_board.BoardFields[_player.Position] is FreeParkingField)
        {

        }
        else if (_board.BoardFields[_player.Position] is GoToJailField)
        {

        }
        else if(_board.BoardFields[_player.Position] is JailField)
        {

        }
        else if (_board.BoardFields[_player.Position] is PropertyField) //Игрок попал на поле собственности
        {
            var propertyField = _board.BoardFields[_player.Position] as PropertyField;
            if (propertyField.Property.Owner != null) //Собственность уже чья-то
            {
                if(!propertyField.Property.IsPawned) //Собственность заложена
                {
                    _player.PayRent(propertyField.Property.Rent, propertyField.Property.Owner);
                }
            }
            else
            {
                var button1 = new Button()
                {
                    Name = "Купить",
                    Selected = true,
                };
                button1.Click += Buy;
                _menuActions.Add(button1);
            }
            
            
        }
        
    }

    void Buy(object sender, EventArgs e)
    {
        var propertyField = (PropertyField)_board.BoardFields[_player.Position];
        if(_player.Balance >= propertyField.Property.Price)
        {
            _player.Buy(propertyField.Property);
            _menuActions.RemoveAt(0);
        }    
    }

    void Pawn(object sender, EventArgs e)
    {
        var pawnPropertyMenu = new PawnPropertyWindow(_player.Properties);

    }

    void Exit(object sender, EventArgs e)
    {
        _isInitilized = false;
    }
}

using MonopolyGame.GameObjects;
using MonopolyGame.GameObjects.Fields;

namespace MonopolyGame.Render.Windows;

public class BoardWindow : IRenderable
{
    private List<BoardField> _fields;

    public BoardWindow(Board board)
    {
        _fields = board.BoardFields;
    }

    public void Render()
    {
        (int x, int y) pointer = (60, 0);
        for (int i = 0; i < 11; i++)
        {
            _fields[i].Render(pointer);
            pointer.x += 12;
        }
        pointer.x -= 12;
        pointer.y += 4;
        for (int i = 11; i < 21; i++)
        {
            _fields[i].Render(pointer);
            pointer.y += 4;
        }
        pointer.y -= 4;
        pointer.x -= 12;
        for (int i = 21; i < 31; i++)
        {
            _fields[i].Render(pointer);
            pointer.x -= 12;
        }
        pointer.x += 12;
        pointer.y -= 4;
        for (int i = 31; i < 40; i++)
        {
            _fields[i].Render(pointer);
            pointer.y -= 4;
        }
    }
}

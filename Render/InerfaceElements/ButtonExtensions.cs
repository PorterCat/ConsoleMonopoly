namespace MonopolyGame.Render.InerfaceElements;

public static class ButtonExtensions
{
    public static void RenderWithDots(this List<Button> buttons, (int x, int y) Position, int interval)
    {
        (int x, int y) pointer = Position;
        pointer.x -= 4;
        for(int i = 0; i < buttons.Count; i++)
        {
            Console.SetCursorPosition(pointer.x, pointer.y);
            Console.Write("[+]");
            pointer.y += interval;
        }
        Render(buttons, Position, interval);
    }

    public static void Render(this List<Button> buttons, (int x, int y) Position, int interval)
    {
        int selectedIndex = 0;

        for(int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i].Selected == true)
            {
                selectedIndex = i;
                break;
            }
        }

        while (true)
        {
            (int x, int y) pointer = Position;
            buttons[selectedIndex].Selected = true;
            foreach (var button in buttons)
            {
                if (button != null)
                {
                    button.SetPosition(pointer.x, pointer.y);
                    pointer.y += interval;
                    button.Render();
                }
                else
                {
                    break;
                }
            }

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.UpArrow:
                    buttons[selectedIndex].Selected = false;
                    selectedIndex--;
                    if (selectedIndex < 0)
                    {
                        selectedIndex = buttons.Count - 1;
                    }
                    break;

                case ConsoleKey.DownArrow:
                    buttons[selectedIndex].Selected = false;
                    selectedIndex++;
                    if (selectedIndex > buttons.Count - 1)
                    {
                        selectedIndex = 0;
                    }
                    break;

                case ConsoleKey.Enter:
                    buttons[selectedIndex].OnClick();
                    return;

                case ConsoleKey.Escape:
                    return;
            }

        }
    }
}

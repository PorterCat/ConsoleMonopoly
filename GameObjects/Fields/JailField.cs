using MonopolyGame.Render.Windows;

namespace MonopolyGame.GameObjects.Fields;

public class JailField : BoardField
{
    public override bool HandlePlayerOnField(Player player)
    {
        EventLoggerWindow.Record($"Игрок {player.Name} посещает тюрьму как посетитель");
        return base.HandlePlayerOnField(player);
    }
}

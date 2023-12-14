using MonopolyGame.Render.Windows;

namespace MonopolyGame.GameObjects.Fields;

public class GoToJailField : BoardField
{
    public override bool HandlePlayerOnField(Player player)
    {
        player.SendToJail();
        EventLoggerWindow.Record($"Игрок {player.Name} отправляется в тюрьму");
        return base.HandlePlayerOnField(player);
    }
}

using MonopolyGame.Render.Windows;

namespace MonopolyGame.GameObjects.Fields;

public class FreeParkingField : BoardField
{
    public override bool HandlePlayerOnField(Player player)
    {
        EventLoggerWindow.Record($"Игрок {player.Name} попал на бесплатную парковку. Отдохните :)");
        return base.HandlePlayerOnField(player);
    }
}

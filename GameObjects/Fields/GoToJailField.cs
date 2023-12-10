namespace MonopolyGame.GameObjects.Fields;

public class GoToJailField : BoardField
{
    public void SendToJail(Player player)
    {
        player.Position = 10;
        player.IsPrisioned = true;
    }
}

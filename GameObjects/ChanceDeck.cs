using MonopolyGame.Controller;

namespace MonopolyGame.GameObjects;

public static class ChanceDeck
{
    private static Player _player = GameController.Players.First();

    private static List<ChanceCard> _chanceCards = new List<ChanceCard>()
    {
            new ChanceCard("Наследство от деда! Получите 100$", () => _player.Get(100)),
            new ChanceCard("У вас день рождение! Получите 10$", () => _player.Get(10)),
            new ChanceCard("Продлите страховку! Заплатите 50$", () => _player.Pay(50)),
            new ChanceCard("Заплатите разработчику ПО зарплату уже наконец! С вас 50$", () => _player.Pay(50)),
            new ChanceCard("Уклонение от налогов! Отправляйтесь в тюрьму", _player.SendToJail),
            new ChanceCard("Вы победили в лотерею, но забыли выключить утюг дома :("),
    };

    public static void SetPlayer(Player player)
    {
        _player = player;
    }

    public static string GetCard()
    {
        var random = new Random();
        var card = _chanceCards[random.Next(0, _chanceCards.Count)];
        card.Event?.Invoke();
        return card.Text;
    }

    public class ChanceCard
    {
        public string Text { get; set; }
        public Action? Event;

        public ChanceCard(string text, Action eventAction) 
        { 
            Text = text;
            Event = eventAction;
        }

        public ChanceCard(string text)
        {
            Text = text;
            Event = null;
        }
    }
}

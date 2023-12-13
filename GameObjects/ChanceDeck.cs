namespace MonopolyGame.GameObjects;

public static class ChanceDeck
{
    private static List<ChanceCard> _chanceCards = new List<ChanceCard>()
    {
            /*new ChanceCard("Наследство от деда! Получите 100$", () => AddToBalance(100)),
            new ChanceCard("У вас день рождение! Получите 10$", () => AddToBalance(10)),
            new ChanceCard("Продлите страховку! Заплатите 50$", () => TakeFromBalance(50)),
            new ChanceCard("Заплатите разработчику ПО зарплату уже наконец! С вас 50$", () => TakeFromBalance(50)),*/
            new ChanceCard("Вы победили в лотерею, но забыли выключить утюг дома :("),
    };

    private static Player _player;

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

    private static void AddToBalance(int sum)
    {
        _player.Balance += sum;
    }

    private static void TakeFromBalance(int loss)
    {
        _player.Balance -= loss;
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

using MonopolyGame.GameObjects.Fields;

namespace MonopolyGame.GameObjects;

public class Board
{
    public List<Player> Players { get; set; }
    public List<BoardField> BoardFields { get; set; }

    public Board(List<Player> players)
    {
        List<Player> firstPlayers = new List<Player>();
        foreach (Player p in players)
        {
            firstPlayers.Add(p);
        }

        BoardFields = new List<BoardField>
        {
            new StartField() {Name = "Начало", Index = 0, PlayersOnTheField = firstPlayers},
            new PropertyField(new Property("Искитимская", 60)),
            new ChanceField() {Name = "? Шанс ?" },
            new PropertyField(new Property("Единорожья", 60)),
            new TaxField() {Name = "$ Налог $"},
            new PropertyField(new Property("Станция1", 200)),
            new PropertyField(new Property("test6", 100)),
            new ChanceField() {Name = "? Шанс ?" },
            new PropertyField(new Property("test7", 100)),
            new PropertyField(new Property("test8", 120)),
            new JailField() {Name = "#Тюрьма#"},
            new PropertyField(new Property("test9", 140)),
            new PropertyField(new Property("Энергия", 150)),
            new PropertyField(new Property("test11", 140)),
            new PropertyField(new Property("test12", 160)),
            new PropertyField(new Property("Станция2", 200)),
            new PropertyField(new Property("test14", 180)),
            new ChanceField(){Name = "? Шанс ?" },
            new PropertyField(new Property("test15", 180)),
            new PropertyField(new Property("test16", 200)),
            new FreeParkingField() {Name = "Парковка"},
            new PropertyField(new Property("test17", 220)),
            new ChanceField() {Name = "? Шанс ?" },
            new PropertyField(new Property("test18", 220)),
            new PropertyField(new Property("test19", 240)),
            new PropertyField(new Property("Станция3", 200)),
            new PropertyField(new Property("test21", 260)),
            new PropertyField(new Property("test22", 260)),
            new PropertyField(new Property("Вода", 150)),
            new PropertyField(new Property("test24", 280)),
            new GoToJailField() {Name = "В тюрьму!" },
            new PropertyField(new Property("test25", 300)),
            new PropertyField(new Property("test26", 300)),
            new ChanceField(){Name = "? Шанс ?"},
            new PropertyField(new Property("test27", 320)),
            new PropertyField(new Property("Станция4", 200)),
            new ChanceField(){Name = "? Шанс ?"},
            new PropertyField(new Property("test29", 350)),
            new TaxField(){Name = "$ Налог $"},
            new PropertyField(new Property("test30", 400))
        };      
    }
}

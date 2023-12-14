using MonopolyGame.GameObjects.Fields;

namespace MonopolyGame.GameObjects;

public static class Board
{
    public static List<BoardField> BoardFields = new List<BoardField>
    {
        new StartField() { Name = "Начало", Index = 0},
        new PropertyField(new Property("Искитимская", 60) { Rent = 2, Index = 1, UpgradeCost = 50, Group = 0}),
        new ChanceField() { Name = " ? Шанс ?", Index = 2},
        new PropertyField(new Property("Горская", 60) { Rent = 4, Index = 3, UpgradeCost = 50, Group = 0}),
        new TaxField(200) { Name = " $ Налог $", Index = 4},
        new PropertyField(new Property("(М)Маркса", 200) { Rent = 4, Index = 5, UpgradeCost = 100}),

        new PropertyField(new Property("СибГУТИ", 100) { Rent = 6 , Index = 6, UpgradeCost = 50, Group = 1}),
        new ChanceField() { Name = " ? Шанс ?" , Index = 7},
        new PropertyField(new Property("СибУПК", 100) { Rent = 6, Index = 8, UpgradeCost = 50, Group = 1}),
        new PropertyField(new Property("НГТУ", 120) { Rent = 8, Index = 9, UpgradeCost = 50, Group = 1}),
        new JailField() { Name = "#Тюрьма#", Index = 10},

        new PropertyField(new Property("ДНС", 140) { Rent = 10 , Index = 11, UpgradeCost = 100, Group = 2}),
        new PropertyField(new Property("Энергия", 150) { Rent = 30, Index = 12, UpgradeCost = 100}),
        new PropertyField(new Property("МВидео", 140) { Rent = 10 , Index = 13, UpgradeCost = 100, Group = 2}),
        new PropertyField(new Property("Xaomi", 160) { Rent = 12, Index = 14, UpgradeCost = 100, Group = 2}),
        new PropertyField(new Property("(М)Студяга", 200) { Rent = 20, Index = 15, UpgradeCost = 100}),

        new PropertyField(new Property("KFC", 180) { Rent = 14 , Index = 16, UpgradeCost = 100, Group = 3}),
        new ChanceField() { Name = " ? Шанс ?" , Index = 17},
        new PropertyField(new Property("Burger King", 180) { Rent = 14, Index = 18, UpgradeCost = 100, Group = 3}),
        new PropertyField(new Property("ВилкаЛожка", 200) { Rent = 16, Index = 19, Group = 3}),
        new FreeParkingField() { Name = "Парковка", Index = 20},

        new PropertyField(new Property("Сан-Сити", 220) { Rent = 18, Index = 21, UpgradeCost = 150, Group = 4}),
        new ChanceField() { Name = " ? Шанс ?" , Index = 22},
        new PropertyField(new Property("Аура", 220) { Rent = 18, Index = 23, UpgradeCost = 150, Group = 4}),
        new PropertyField(new Property("МЕГА", 240) { Rent = 20 , Index = 24, UpgradeCost = 150, Group = 4}),
        new PropertyField(new Property("(М)Речник", 200) { Rent = 20 , Index = 25, UpgradeCost = 100}),

        new PropertyField(new Property("Бердск", 260) { Rent = 22 , Index = 26, UpgradeCost = 150, Group = 5}),
        new PropertyField(new Property("Академ", 260) { Rent = 22 , Index = 27, UpgradeCost = 150, Group = 5}),
        new PropertyField(new Property("Вода", 150) { Rent = 30 , Index = 28, UpgradeCost = 100}),
        new PropertyField(new Property("п. Обь", 280) { Rent = 20 , Index = 29, UpgradeCost = 150, Group = 5}),
        new GoToJailField() { Name = "В тюрьму!" , Index = 30},

        new PropertyField(new Property("Пятёрочка", 300) { Rent = 24, Index = 31, UpgradeCost = 200, Group = 6}),
        new PropertyField(new Property("Ярче", 300) { Rent = 24, Index = 32, UpgradeCost = 200, Group = 6}),
        new ChanceField() { Name = " ? Шанс ?", Index = 33},
        new PropertyField(new Property("SPAR", 320) { Rent = 26, Index = 34, UpgradeCost = 200, Group = 6}),
        new PropertyField(new Property("(М)Ленина", 200) { Rent = 20 , Index = 35, UpgradeCost = 100}),

        new ChanceField() { Name = " ? Шанс ?", Index = 36},
        new PropertyField(new Property("Пегасья", 350) { Rent = 45, Index = 37, UpgradeCost = 200, Group = 7}),
        new TaxField(150) { Name = " $ Налог $", Index = 38},
        new PropertyField(new Property("Единорожья", 400) { Rent = 50, Index = 39, UpgradeCost = 200, Group = 7})
    };

    public static void SetPlayers(List<Player> players)
    {
        List<Player> list = new List<Player>();
        foreach (Player p in players)
        {
            list.Add(p);
        }

        BoardFields[0]= new StartField() { Name = "Начало", Index = 0, PlayersOnTheField = list};
    }
}

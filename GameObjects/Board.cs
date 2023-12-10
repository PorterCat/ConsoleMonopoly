using MonopolyGame.GameObjects.Fields;
using System.Xml.Linq;

namespace MonopolyGame.GameObjects;

public static class Board
{
    public static List<BoardField> BoardFields = new List<BoardField>
    {
        new StartField() { Name = "Начало", Index = 0},
        new PropertyField(new Property("Искитимская", 60) { Rent = 20, Index = 0}),
        new ChanceField() { Name = "? Шанс ?", Index = 1},
        new PropertyField(new Property("Единорожья", 60) { Rent = 20, Index = 2}),
        new TaxField() { Name = "$ Налог $", Index = 3},
        new PropertyField(new Property("Станция1", 200) { Rent = 20, Index = 4}),
        new PropertyField(new Property("test6", 100) { Rent = 20 , Index = 5}),
        new ChanceField() { Name = "? Шанс ?" , Index = 6},
        new PropertyField(new Property("test7", 100) { Rent = 20, Index = 7}),
        new PropertyField(new Property("test8", 120) { Rent = 20, Index = 8}),
        new JailField() { Name = "#Тюрьма#", Index = 9},
        new PropertyField(new Property("test9", 140) { Rent = 20 , Index = 10}),
        new PropertyField(new Property("Энергия", 150) { Rent = 20, Index = 11}),
        new PropertyField(new Property("test11", 140) { Rent = 20 , Index = 12}),
        new PropertyField(new Property("test12", 160) { Rent = 20, Index = 13}),
        new PropertyField(new Property("Станция2", 200) { Rent = 20, Index = 14}),
        new PropertyField(new Property("test14", 180) { Rent = 20 , Index = 15}),
        new ChanceField() { Name = "? Шанс ?" , Index = 16},
        new PropertyField(new Property("test15", 180) { Rent = 20, Index = 17}),
        new PropertyField(new Property("test16", 200) { Rent = 20, Index = 18}),
        new FreeParkingField() { Name = "Парковка", Index = 19},
        new PropertyField(new Property("test17", 220) { Rent = 20, Index = 20}),
        new ChanceField() { Name = "? Шанс ?" , Index = 21},
        new PropertyField(new Property("test18", 220) { Rent = 20, Index = 22}),
        new PropertyField(new Property("test19", 240) { Rent = 20 , Index = 23}),
        new PropertyField(new Property("Станция3", 200) { Rent = 20 , Index = 24}),
        new PropertyField(new Property("test21", 260) { Rent = 20 , Index = 25}),
        new PropertyField(new Property("test22", 260) { Rent = 20 , Index = 26}),
        new PropertyField(new Property("Вода", 150) { Rent = 20 , Index = 27}),
        new PropertyField(new Property("test24", 280) { Rent = 20 , Index = 28}),
        new GoToJailField() { Name = "В тюрьму!" , Index = 29},
        new PropertyField(new Property("test25", 300) { Rent = 20 , Index = 30}),
        new PropertyField(new Property("test26", 300) { Rent = 20 , Index = 31}),
        new ChanceField() { Name = "? Шанс ?", Index = 32},
        new PropertyField(new Property("test27", 320) { Rent = 20, Index = 33}),
        new PropertyField(new Property("Станция4", 200) { Rent = 20 , Index = 34}),
        new ChanceField() { Name = "? Шанс ?", Index = 35},
        new PropertyField(new Property("test29", 350) { Rent = 20, Index = 36}),
        new TaxField() { Name = "$ Налог $", Index = 37},
        new PropertyField(new Property("test30", 400) { Rent = 20, Index = 38})
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

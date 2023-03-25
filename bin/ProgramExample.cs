// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using dice_roller;

Dice dice = new Dice();
ChanceResult[] resultTable = { new ChanceResult(2, "Cat"), new ChanceResult(3, "Dog"), new ChanceResult(1, "Fish"), new ChanceResult(1, "Snake") };
ChanceTable table = ChanceTable.FromJson(FileWorker.ReadFile("../../../../example/example.json"));

Console.WriteLine("Basic 1d6 roll with Dice.Roll(6): " + dice.Roll(6));
Console.WriteLine("Basic 2d6 roll with Dice.RollMany(2, 6): " + dice.RollMany(2, 6));
Console.WriteLine("Basic percentile roll with Dice.RollPercent(): " + dice.RollPercent());
Console.WriteLine("Parsing a string '2d6+1' into a roll with Dice.ParseString('2d6+1'): " + dice.ParseString("2d6+1"));
Console.WriteLine("Rolling on a chance table with Dice.RollOnTable(table): " + dice.RollOnTable(table));
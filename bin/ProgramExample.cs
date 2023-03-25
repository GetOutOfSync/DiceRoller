// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using dice_roller;

Dice dice = new Dice();
ChanceResult[] resultTable = { new ChanceResult(2, "Cat"), new ChanceResult(3, "Dog"), new ChanceResult(1, "Fish"), new ChanceResult(1, "Snake") };
ChanceTable table = new ChanceTable("example", resultTable);

Console.Write(JsonConvert.SerializeObject(table, Formatting.Indented));
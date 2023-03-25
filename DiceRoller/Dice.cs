namespace dice_roller; 
public class Dice {

    private int _seed;
    private Random _rand;

    public Dice() : this(new Random().Next()) { }

    public Dice(int seed) {
        _seed = seed;
        _rand = new Random(seed);
    }

    public int Seed {
        get { return _seed; }
        set { _rand = new Random(GenSeed()); }
    }

    /// <returns>A randomly generated seed.</returns>
    public int GenSeed() {
        return _rand.Next();
    }

    /// <summary>
    /// Simulates a dice roll on an (s) sided dice. Results are 1-(s)
    /// </summary>
    /// <param name="sides">The number of sides the dice has.</param>
    /// <returns>An integer between 1 and (s)</returns>
    public int Roll(int sides) {
        return _rand.Next(sides) + 1;
    }

    /// <summary>
    /// Simulates (n) dice rolls on an (s) sided dice. Similar to RPG dice notation, like 2d6. Results range from (s) to (n)*(s)
    /// </summary>
    /// <param name="number">Number of dice rolls</param>
    /// <param name="sides">Number of sides the dice has</param>
    /// <returns>An integer between (s) and (n)*(s)</returns>
    public int Roll(int number, int sides)
    {
        int result = 0;
        for (int i = 0; i < number; i++)
        {
            result += this.Roll(sides);
        }
        return result;
    }

    /// <summary>
    /// Simulates (n) dice rolls on an (s) sided dice. Similar to RPG dice notation, like 2d6. Results range from (s) to (n)*(s)
    /// </summary>
    /// <param name="number">Number of dice rolls</param>
    /// <param name="sides">Number of sides the dice has</param>
    /// <returns>An integer between (s) and (n)*(s)</returns>
    public int RollMany(int number, int sides) {
        return Roll(number, sides);
    }

    /// <summary>
    /// Simulates a roll of a 100 sided dice, or a percentile dice.
    /// </summary>
    /// <returns>An integer between 1 and 100</returns>
    public int RollPercent() {
        return Roll(100);
    }

    /// <summary>
    /// Simulates a dice roll on a given ChanceTable object with a number range of 1 to ChanceTable.TotalChance. Retrieves the result from that index
    /// </summary>
    /// <param name="table"></param>
    /// <returns>The string associated with the roll</returns>
    public string RollOnTable(ChanceTable table) {
        return table.GetResult(Roll(table.TotalChance));
    }

    /// <summary>
    /// Render a string into a set of commands and generate a number from those commands. 
    /// For example: ParseString("2d6+1") will run RollMany(2,6) and add one to the result.
    /// </summary>
    /// <param name="s">The string to render into commands. </param>
    /// <returns>An valid integer value which is the result of the passed command.</returns>
    public int ParseString(string s) {
        string[] NumArray = s.Split(new char[] { 'd', '+', '/', '-', '*' });
        if (NumArray.Length == 1) return Int32.Parse(NumArray[0]);
        string operators = ExtensionMethods.StripNumbers(s);
        int holder = 0;
        for (int i = 0; i < operators.Length; i++) {
            switch (operators[i]) {
                case 'd':
                    holder += Roll(Int32.Parse(NumArray[i]), Int32.Parse(NumArray[i + 1]));
                    break;
                case '+':
                    holder += Int32.Parse(NumArray[i + 1]);
                    break;
                case '-':
                    holder -= Int32.Parse(NumArray[i + 1]);
                    break;
                case '*':
                    holder *= Int32.Parse(NumArray[i + 1]);
                    break;
                case '/':
                    holder /= Int32.Parse(NumArray[i + 1]);
                    break;
            }
        }
        return holder;
    }
}
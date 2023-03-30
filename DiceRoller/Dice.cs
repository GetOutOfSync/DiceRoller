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
        /// Processes a string so it can be used by the Dice class to generate a random number. Examples include:
        /// "1": Will return 1;
        /// "1d6": Will return the result of rolling a single six sided dice.
        /// "1d6/2": Will get the result of rolling a single six sided dice and divide it by two. Other operations can be performed and are read left to right.
        /// </summary>
        /// <param name="s">The string to process.</param>
        /// <returns>The randomly generated value from the commands passed through the string.</returns>
        public int ParseString(string s) {
            string[] NumArray = s.Split(new char[] { 'd', '+', '/', '-', '*' });
            if (NumArray.Length == 1) return Int32.Parse(NumArray[0]);
            string operators = ExtensionMethods.StripNumbers(s);
            int holder = 0;
            for (int i = 0; i < operators.Length; i++) {
                switch (operators[i]) {
                    case 'd':
                        holder += RollMany(Int32.Parse(NumArray[i]), Int32.Parse(NumArray[i + 1]));
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

        /// <summary>
        /// Gets the minimum possible result from a passed string
        /// </summary>
        /// <param name="s">String to parse</param>
        /// <returns>Minimum possible result from rolling on a passed string</returns>
        public static int ParseStringMin(string s)
        {
            string[] NumArray = s.Split(new char[] { 'd', '+', '/', '-', '*' });
            if (NumArray.Length == 1) return Int32.Parse(NumArray[0]);
            string operators = ExtensionMethods.StripNumbers(s);
            int holder = 0;
            for (int i = 0; i < operators.Length; i++)
            {
                switch (operators[i])
                {
                    case 'd':
                        holder += Int32.Parse(NumArray[i]);
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

        /// <summary>
        /// Gets the maximum possible result from a passed string
        /// </summary>
        /// <param name="s">String to parse</param>
        /// <returns>Maximum possible result from rolling on a passed string</returns>
        public static int ParseStringMax(string s)
        {
            string[] NumArray = s.Split(new char[] { 'd', '+', '/', '-', '*' });
            if (NumArray.Length == 1) return Int32.Parse(NumArray[0]);
            string operators = ExtensionMethods.StripNumbers(s);
            int holder = 0;
            for (int i = 0; i < operators.Length; i++)
            {
                switch (operators[i])
                {
                    case 'd':
                        holder += (Int32.Parse(NumArray[i]) * Int32.Parse(NumArray[i + 1]));
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
}
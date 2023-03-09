using System.Collections.Generic;

namespace dice_roller {
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
            set { _rand = new Random(value); }
        }

        public int GenSeed() {
            return _rand.Next();
        }

        /// <summary>
        /// Generates a list of non-repeating random integers from a given range
        /// </summary>
        /// <param name="startRange">The start of the range from which random integers are selected</param>
        /// /// <param name="endRange">The end of the range from which random integers are selected. This integer will be included in the range.</param>
        /// <param name="number">The number of random integers to select</param>
        /// <returns>A list of randomly selected integers</returns>
        public List<int> GenNonRepeatList(int startRange, int endRange, int number)
        {
            List<int> list = new List<int>();
            for (int i = startRange; i <= endRange; i++) { list.Add(i); }
            return GenNotRepeatList(list, number);
        }

        /// <summary>
        /// Generates a list of non-repeating random elemets from a given list
        /// </summary>
        /// <param name="list">The input list from which random elements are selected</param>
        /// <param name="number">The number of random elements to select</param>
        /// <returns>A list of randomly selected elements</returns>
        public List<T> GenNotRepeatList<T>(List<T> list, int number)
        {
            List<T> holder = new List<T>();
            for (int i = 0; i < number; i++)
            {
                holder.Add(RollOnList(list));
                list.Remove(holder[i]);
            }
            return holder;
        }

        /// <summary>
        /// Reorders the list in a random way.
        /// </summary>
        /// <param name="list">List to reorder</param>
        /// <returns>List with elements randomly moved</returns>
        public List<T> GenNotRepeatList<T>(List<T> list)
        {
            return GenNotRepeatList(list, list.Count);
        }

        /// <summary>
        /// Generates a random number between 1 and a given integer, much like a dice with a given number of sides.
        /// </summary>
        /// <param name="sides">The maximum value which can be rolled.</param>
        /// <returns>A random number between1 and sides.</returns>
        public int Roll(int sides) {
            return _rand.Next(sides) + 1;
        }

        /// <summary>
        /// Rolls a random number between the lesser and greater.
        /// </summary>
        /// <param name="lesser">The bottom range where a value could be</param>
        /// <param name="greater">The top range where a value could be</param>
        /// <returns>A random double number between the lesser and greater</returns>
        public double RollBetween(double lesser, double greater)
        {
            return Math.Round((_rand.NextDouble() * (greater-lesser)) + lesser, 2);
        }

        /// <summary>
        /// Returns a random element from a given list
        /// </summary>
        /// <param name="list">The list to pull a random element from</param>
        /// <returns>A random element from the provided list</returns>
        public T RollOnList<T> (List<T> list)
        {
            return list[Roll(list.Count) - 1];
        }

        /// <summary>
        /// Returns a random element from a given array
        /// </summary>
        /// <param name="array">The array to pull a random element from</param>
        /// <returns>A random element from the provided array</returns>
        public T RollOnArray<T>(T[] array)
        {
            return array[Roll(array.Length) - 1];
        }

        /// <summary>
        /// Rolls multiple "dice" at a time. 
        /// </summary>
        /// <param name="number">The number of times to roll a dice</param>
        /// <param name="sides">The number of sides the dice has</param>
        /// <returns>A random number which is generated from rolling said dice.</returns>
        public int RollMany(int number, int sides) {
            int result = 0;
            for (int i = 0; i < number; i++) {
                result += Roll(sides);
            }
            return result;
        }

        /// <summary>
        /// Rolls a number between 1 and 100.
        /// </summary>
        /// <returns>A randomly generated number between 1 and 100.</returns>
        public int RollPercent() {
            return Roll(100);
        }

        /// <summary>
        /// Selects an element from a ChanceTable object randomly.
        /// </summary>
        /// <param name="table">The ChanceTable to roll on.</param>
        /// <returns>The randomly selected element from the table.</returns>
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
    }
}
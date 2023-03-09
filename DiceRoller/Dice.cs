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
        /// Generates a list of non-repeating random integers from a given list
        /// </summary>
        /// <param name="list">The input list from which random integers are selected</param>
        /// <param name="number">The number of random integers to select</param>
        /// <returns>A list of randomly selected integers</returns>
        public List<int> GenNotRepeatList(List<int> list, int number)
        {
            List<int> holder = new List<int>();
            for (int i = 0; i < number; i++)
            {
                holder.Add(RollOnList(list));
                list.Remove(holder[i]);
            }
            return holder;
        }

        public int Roll(int sides) {
            return _rand.Next(sides) + 1;
        }

        public double RollBetween(double lesser, double greater)
        {
            return Math.Round((_rand.NextDouble() * (greater-lesser)) + lesser, 2);
        }

        public T RollOnList<T> (List<T> list)
        {
            return list[Roll(list.Count - 1)];
        }

        public T RollOnArray<T>(T[] array)
        {
            return array[Roll(array.Length - 1)];
        }

        public int RollMany(int number, int sides) {
            int result = 0;
            for (int i = 0; i < number; i++) {
                result += this.Roll(sides);
            }
            return result;
        }

        public int RollPercent() {
            return Roll(100);
        }

        public string RollOnTable(ChanceTable table) {
            return table.GetResult(Roll(table.TotalChance));
        }

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
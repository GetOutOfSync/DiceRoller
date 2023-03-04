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
            set { _rand = new Random(GenSeed()); }
        }

        public int GenSeed() {
            return _rand.Next();
        }

        public int Roll(int sides) {
            return _rand.Next(sides) + 1;
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
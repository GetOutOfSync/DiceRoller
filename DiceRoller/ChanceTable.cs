using Newtonsoft.Json;

namespace dice_roller
{

    public class ChanceTable
    {
        private ChanceResult[] _table;
        private string _name;
        private int _totalChance;

        public ChanceTable(string name, ChanceResult[] table)
        {
            _table = table;
            _name = name;
            _totalChance = 0;

            foreach (ChanceResult result in _table)
            {
                _totalChance += result.Chance;
            }
        }

        /// <summary>
        /// Will return the value of the selected ChanceResult.
        /// </summary>
        /// <param name="roll"></param>
        /// <returns>The string of the associated rolled element</returns>
        public string GetResult(int roll)
        {
            int comp = 0;
            foreach (ChanceResult result in _table)
            {
                comp += result.Chance;
                if (roll <= comp) return result.Result;
            }
            return "null";
        }

        /// <summary>
        /// The total range of weights which appear on the table. Used to randomly select elements.
        /// </summary>
        [JsonIgnoreAttribute]
        public int TotalChance
        {
            get { return _totalChance; }
        }

        /// <summary>
        /// Name of the table. Used when searching for certain tables.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// The table of possible results and their weights.
        /// </summary>
        public ChanceResult[] Table
        {
            get { return _table; }
            set { _table = value; }
        }
    }
}
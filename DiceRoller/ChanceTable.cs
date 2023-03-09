using Newtonsoft.Json;

namespace dice_roller
{

    public class ChanceTable
    {
        private ChanceResult[] _table;
        private string _name;
        private int _totalChance;

        public ChanceTable(String name, ChanceResult[] table)
        {
            _table = table;
            _name = name;
            _totalChance = 0;

            foreach (ChanceResult result in _table)
            {
                _totalChance += result.Chance;
            }
        }

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

        [JsonIgnoreAttribute]
        public int TotalChance
        {
            get { return _totalChance; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public ChanceResult[] Table
        {
            get { return _table; }
            set { _table = value; }
        }
    }
}

namespace dice_roller
{


    public class ChanceResult
    {
        private int _chance;
        private string _result;

        public ChanceResult(int chance, string result)
        {
            _chance = chance;
            _result = result;
        }

        public int Chance
        {
            get { return _chance; }
            set { _chance = value; }
        }

        public string Result
        {
            get { return _result; }
            set { _result = value; }
        }
    }
}
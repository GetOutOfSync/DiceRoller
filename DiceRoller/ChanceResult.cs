
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

        /// <summary>
        /// The weight this result has when rolling.
        /// </summary>
        public int Chance
        {
            get { return _chance; }
            set { _chance = value; }
        }

        /// <summary>
        /// The string which will be passed on if this element is selected.
        /// </summary>
        public string Result
        {
            get { return _result; }
            set { _result = value; }
        }
    }
}
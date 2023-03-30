namespace dice_roller;

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
    /// The weight of the associated Result string, used in the context of the greater ChanceTable
    /// </summary>
    public int Chance
    {
        get { return _chance; }
        set { _chance = value; }
    }

    /// <summary>
    /// A string which is returned when this object is selected from a greater ChanceTable object.
    /// </summary>
    public string Result
    {
        get { return _result; }
        set { _result = value; }
    }
}
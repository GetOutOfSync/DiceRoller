using Newtonsoft.Json;

namespace dice_roller;

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
    /// Converts a json string into a ChanceTable object
    /// </summary>
    /// <param name="json">The json to convert into a ChanceTable object</param>
    /// <returns>A ChanceTable object</returns>
    /// <exception cref="InvalidOperationException">Throws an exception if the json is corrupt</exception>
    public static ChanceTable FromJson(string json)
    {
        return JsonConvert.DeserializeObject<ChanceTable>(json) ?? throw new InvalidOperationException();
    }

    /// <summary>
    /// When given the result of a random number generator, find the result of the roll.
    /// </summary>
    /// <example>
    /// 
    /// [1-3] = "Cat"
    /// [4-6] = "Dog"
    /// 
    /// GetResult(2) returns "Cat"
    /// GetResult(5) returns "Dog"
    /// 
    /// </example>
    /// <param name="roll">A number from a random number generator, which should be based on the total table size.</param>
    /// <returns>The string of the associated ChanceResult object which was selected.</returns>
    public string GetResult(int roll)
    {
        if (_totalChance == 1) return _table[0].Result;
        int comp = 0;
        foreach (ChanceResult result in _table)
        {
            comp += result.Chance;
            if (roll <= comp) return result.Result;
        }
        return "null";
    }

    /// <summary>
    /// The sum of all of the weights in the ChanceTable. Used as the max number to feed to a random number generator. 
    /// </summary>
    [JsonIgnoreAttribute]
    public int TotalChance
    {
        get { return _totalChance; }
    }

    /// <summary>
    /// The name of the table. Used to find the table if multiple tables are stored in an array or other form of list.
    /// </summary>
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    /// <summary>
    /// The reference array where the weights and results are stored.
    /// </summary>
    public ChanceResult[] Table
    {
        get { return _table; }
        set { _table = value; }
    }
}
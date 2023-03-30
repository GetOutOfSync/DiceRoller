namespace dice_roller;

public class TableStore
{
    public List<ChanceTable> _chanceTables;
    
    public TableStore() 
    { 
        _chanceTables = new List<ChanceTable>();
    }

    /// <summary>
    /// Finds and returns the stored ChanceTable object based on the provided name, case insensitive.
    /// </summary>
    /// <param name="name">The name of the table to search for</param>
    /// <returns>The ChanceTable object for follow-on processing</returns>
    public ChanceTable GetTable(string name)
    {
        foreach (ChanceTable table in _chanceTables)
        {
            if (table.Name.ToUpper() == name.ToUpper()) { return table; }
        }
        return null;
    }

    /// <returns>A list of all stored table names.</returns>
    public List<string> GetTableNames()
    {
        List<string> names = new List<string>();
        foreach (ChanceTable table in _chanceTables)
        {
            names.Add(table.Name);
        }
        return names;
    }

    /*public void ImportProfile(string root)
    {
        foreach(string json in FileWorker.RecursiveImport(root, "json"))
        {
            _chanceTables.Add(ConvertTable(json));
        }
    }*/

    /// <summary>
    /// Creates and adds a ChanceTable based on json retrieved from a given file.
    /// </summary>
    /// <param name="file">File which stores json</param>
    public void ImportTable(string file)
    {
        _chanceTables.Add(ChanceTable.FromJson(FileWorker.ReadFile(file)));
    }
}

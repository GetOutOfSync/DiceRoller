using Newtonsoft.Json;
using System.Collections;

namespace dice_roller
{
    public class TableStore
    {
        public List<ChanceTable> _chanceTables;
        
        public TableStore() 
        { 
            _chanceTables = new List<ChanceTable>();
        }

        /// <summary>
        /// Returns stored ChanceTable based on the table's name
        /// </summary>
        /// <param name="name">The name of the table to search for</param>
        /// <returns>The ChanceTable object which matches the name. Returns nothing if nothing is found.</returns>
        public ChanceTable GetTable(string name)
        {
            foreach (ChanceTable table in _chanceTables)
            {
                if (table.Name == name) { return table; }
            }
            return null;
        }

        /// <summary>
        /// Returns a List with all stored ChanceTable names.
        /// </summary>
        /// <returns>A List with all stored ChanceTable names.</returns>
        public List<string> GetTableNames()
        {
            List<string> names = new List<string>();
            foreach (ChanceTable table in _chanceTables)
            {
                names.Add(table.Name);
            }
            return names;
        }

        /// <summary>
        /// Imports all files with a .json extension and attempts to convert them into ChanceTables, placing the results into Storage
        /// </summary>
        /// <param name="root">The start point for the import</param>
        public void ImportProfile(string root)
        {
            foreach(string json in FileWorker.RecursiveImport(root, "json"))
            {
                _chanceTables.Add(ConvertTable(json));
            }
        }

        /// <summary>
        /// Imports a ChanceTable to the list from a file with json contained.
        /// </summary>
        /// <param name="file">The file to import the ChanceTable from, with properly formated json.</param>
        public void ImportTable(String file)
        {
            _chanceTables.Add(ConvertTable(FileWorker.ReadFile(file)));
        }

        /// <summary>
        /// Converts a table from a string to json, and seralizes from there.
        /// </summary>
        /// <param name="json">A string with the json to parse</param>
        /// <returns>A ChanceTable object serialized from the json</returns>
        /// <exception cref="InvalidOperationException">Throws an error if the json does not render properly.</exception>
        private ChanceTable ConvertTable(string json)
        {
            return JsonConvert.DeserializeObject<ChanceTable>(json) ?? throw new InvalidOperationException();
        }
    }
}

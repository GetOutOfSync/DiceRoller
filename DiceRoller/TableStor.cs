using Newtonsoft.Json;
using System.Collections;

namespace dice_roller
{
    public class TableStore
    {
        private static string _profileStart = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\spectrum-sector-creator";
        public List<ChanceTable> _chanceTables;
        
        public TableStore() 
        { 
            _chanceTables = new List<ChanceTable>();
        }

        public ChanceTable GetTable(String name)
        {
            foreach (ChanceTable table in _chanceTables)
            {
                if (table.Name == name) { return table; }
            }
            return null;
        }

        public void ImportProfile(String root)
        {
            foreach(string json in FileWorker.RecursiveImport(root))
            {
                _chanceTables.Add(JsonConvert.DeserializeObject<ChanceTable>(json) ?? throw new InvalidOperationException());
            }
        }
    }
}

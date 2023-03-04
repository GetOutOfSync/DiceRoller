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

        public ChanceTable GetTable(String name)
        {
            foreach (ChanceTable table in _chanceTables)
            {
                if (table.Name == name) { return table; }
            }
            return null;
        }

        public List<string> GetTableNames()
        {
            List<string> names = new List<string>();
            foreach (ChanceTable table in _chanceTables)
            {
                names.Add(table.Name);
            }
            return names;
        }

        public void ImportProfile(String root)
        {
            foreach(string json in FileWorker.RecursiveImport(root, "json"))
            {
                _chanceTables.Add(ConvertTable(json));
            }
        }

        public void ImportTable(String file)
        {
            _chanceTables.Add(ConvertTable(FileWorker.ReadFile(file)));
        }

        private ChanceTable ConvertTable(string json)
        {
            return JsonConvert.DeserializeObject<ChanceTable>(json) ?? throw new InvalidOperationException();
        }
    }
}

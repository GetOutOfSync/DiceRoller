using System.Text.Json.Nodes;

namespace dice_roller
{
    public static class FileWorker
    {
        public static string ReadFile(string file)
        {
            return System.IO.File.ReadAllText(file);
        }

        public static List<string> RecursiveImport(String root)
        {
            List<string> holder = new List<string>();
            foreach (string file in Directory.EnumerateFiles(root, "*.*", SearchOption.AllDirectories))
            {
                holder.Add(ReadFile(file));
            }
            return holder;
        }
    }
}
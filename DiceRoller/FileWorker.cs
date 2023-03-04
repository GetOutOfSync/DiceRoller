namespace dice_roller
{
    public static class FileWorker
    {
        public static string ReadFile(string file)
        {
            return System.IO.File.ReadAllText(file);
        }
    }
}
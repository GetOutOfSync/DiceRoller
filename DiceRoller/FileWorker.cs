namespace dice_roller;

public static class FileWorker
{
    /// <summary>
    /// Returns the contents of a given file.
    /// </summary>
    /// <param name="file">The filename which needs to be read</param>
    /// <returns>The contents of a given file.</returns>
    public static string ReadFile(string file)
    {
        return System.IO.File.ReadAllText(file);
    }

    /// <summary>
    /// Returns the contents all files below a certain location
    /// </summary>
    /// <param name="root">The folder to start importing from.</param>
    /// <returns>A list with the contents of all files found.</returns>
    public static List<string> RecursiveImport(string root)
    {
        return RecursiveImport(root, "*");
    }

    /// <summary>
    /// Returns the contents of all files below a certain location which has a certain defined extension.
    /// </summary>
    /// <param name="root">The folder to start importing from</param>
    /// <param name="filter">The filter applied to the search</param>
    /// <returns>A list with the contents of all files found</returns>
    public static List<string> RecursiveImport(string root, string filter)
    {
        List<string> holder = new List<string>();
        foreach (string file in Directory.EnumerateFiles(root, filter, SearchOption.AllDirectories))
        {
            Console.WriteLine(file);
            holder.Add(ReadFile(file));
        }
        return holder;
    }
}


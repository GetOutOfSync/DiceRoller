namespace dice_roller;

public static class ExtensionMethods
{
    /// <summary>
    /// This function takes a string and returns a copy of the string without and digits (0-9)
    /// </summary>
    /// <param name="input">A string which can possible include digits.</param>
    /// <returns>The string without any digits</returns>
    public static string StripNumbers(string input)
    {
        return new string(input.Where(c => !char.IsDigit(c)).ToArray());
    }
}
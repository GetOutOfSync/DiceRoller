namespace dice_roller
{
    public static class ExtensionMethods
    {
        public static string Replace(this string s, char[] seperators, string newVal)
        {
            string[] temp;

            temp = s.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
            return String.Join(newVal, temp);
        }

        /// <summary>
        /// This removes all numbers from a given string.
        /// </summary>
        /// <param name="input">A string which potentially has numbers</param>
        /// <returns>A string without numbers</returns>
        public static string StripNumbers(string input)
        {
            return new string(input.Where(c => !char.IsDigit(c)).ToArray());
        }
    }
}
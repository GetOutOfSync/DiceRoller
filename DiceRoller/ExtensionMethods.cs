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

        public static string StripNumbers(string input)
        {
            return new string(input.Where(c => !char.IsDigit(c)).ToArray());
        }
    }
}
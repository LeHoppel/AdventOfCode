namespace AdventOfCode.Utility;

public static class StringReplacement
{
    public static string ReplaceAt(this string input, int index, char newChar)
    {
        char[] chars = input.ToCharArray();
        chars[index] = newChar;
        return new string(chars);
    }
}
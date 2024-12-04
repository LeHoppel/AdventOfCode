namespace AdventOfCode._2024;

public abstract class Day
{
    public abstract int CalculatePart01(string kindOfInput, string pathPrefix);

    public abstract int CalculatePart02(string kindOfInput, string pathPrefix);

    protected virtual List<string> ReadInput(string path)
    {
        StreamReader streamReader = new StreamReader(path);
        string? currentLine = streamReader.ReadLine(); 
        
        List<string> input = new List<string>();
        
        while (currentLine != null)
        {
            input.Add(currentLine);
            currentLine = streamReader.ReadLine(); 
        }

        return input;
    }
}
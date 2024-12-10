namespace AdventOfCode._2024;

public class Day10 : Day
{
    public override bool PrintTime { get => false; set { } }
    
    public override long CalculatePart01(string kindOfInput, string pathPrefix)
    {
        List<string> input = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt");
        input.Reverse();

        int answerValue = 0;
        
        for (int y = 0; y < input.Count; y++)
        {
            for (int x = 0; x < input[0].Length; x++)
            {
                if (input[y][x] != '0') continue;

                List<(int, int)> visitedNines = new();
                answerValue += RecursiveHikeSearch(input, x, y, -1, visitedNines);
            }
        }

        return answerValue;
    }

    private int RecursiveHikeSearch(List<string> input, int x, int y, int entryHeight, List<(int, int)> visitedNines)
    {
        Console.WriteLine($"x: {x}, y: {y}, entryHeight: {entryHeight}, ");
        if (x < 0 || x >= input[0].Length || y < 0 || y >= input.Count) return 0;
        
        int fieldHeight = int.Parse(input[y][x].ToString());
        if (fieldHeight - entryHeight != 1) return 0;

        if (fieldHeight == 9 && !visitedNines.Contains((x, y)))
        {
            visitedNines.Add((x, y));
            return 1;
        }

        return RecursiveHikeSearch(input, x + 1, y, fieldHeight, visitedNines)
               + RecursiveHikeSearch(input, x, y + 1, fieldHeight, visitedNines)
               + RecursiveHikeSearch(input, x - 1, y, fieldHeight, visitedNines)
               + RecursiveHikeSearch(input, x, y - 1, fieldHeight, visitedNines);
    }

    public override long CalculatePart02(string kindOfInput, string pathPrefix)
    {
        if (kindOfInput == "input") return -1;
        List<string> input = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt");
        int answerValue = 0;
        
        return answerValue;
    }
}
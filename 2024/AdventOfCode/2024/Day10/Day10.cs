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
                answerValue += RecursiveHikeScore(input, x, y, -1, visitedNines);
            }
        }

        return answerValue;
    }

    private int RecursiveHikeScore(List<string> input, int x, int y, int entryHeight, List<(int, int)> visitedNines)
    {
        if (x < 0 || x >= input[0].Length || y < 0 || y >= input.Count) return 0;
        
        int fieldHeight = int.Parse(input[y][x].ToString());
        if (fieldHeight - entryHeight != 1) return 0;

        if (fieldHeight == 9 && !visitedNines.Contains((x, y)))
        {
            visitedNines.Add((x, y));
            return 1;
        }

        return RecursiveHikeScore(input, x + 1, y, fieldHeight, visitedNines)
               + RecursiveHikeScore(input, x, y + 1, fieldHeight, visitedNines)
               + RecursiveHikeScore(input, x - 1, y, fieldHeight, visitedNines)
               + RecursiveHikeScore(input, x, y - 1, fieldHeight, visitedNines);
    }

    public override long CalculatePart02(string kindOfInput, string pathPrefix)
    {
        List<string> input = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt");
        input.Reverse();

        int answerValue = 0;
        
        for (int y = 0; y < input.Count; y++)
        {
            for (int x = 0; x < input[0].Length; x++)
            {
                if (input[y][x] != '0') continue;
                
                answerValue += RecursiveHikeRating(input, x, y, -1);
            }
        }

        return answerValue;
    }
    
    private int RecursiveHikeRating(List<string> input, int x, int y, int entryHeight)
    {
        if (x < 0 || x >= input[0].Length || y < 0 || y >= input.Count) return 0;
        
        int fieldHeight = int.Parse(input[y][x].ToString());
        if (fieldHeight - entryHeight != 1) return 0;

        if (fieldHeight == 9) return 1;

        return RecursiveHikeRating(input, x + 1, y, fieldHeight)
               + RecursiveHikeRating(input, x, y + 1, fieldHeight)
               + RecursiveHikeRating(input, x - 1, y, fieldHeight)
               + RecursiveHikeRating(input, x, y - 1, fieldHeight);
    }
}
namespace AdventOfCode._2024;

public class Day12 : Day
{
    public override bool PrintTime { get => false; set { } }

    private List<(int, int)> _visitedRegions = new();
    
    public override long CalculatePart01(string kindOfInput, string pathPrefix)
    {
        List<string> input = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt");
        input.Reverse();

        int answerValue = 0;
        
        for (int x = 0; x < input[0].Length; x++)
        {
            for (int y = 0; y < input.Count; y++)
            {
                if (_visitedRegions.Contains((x, y))) continue;

                (int area, int perimeter) = RecursiveCost(input, x, y, input[y][x], new List<(int, int)>());
                
                answerValue += area * perimeter;
            }
        }

        return answerValue;
    }

    private (int, int) RecursiveCost(List<string> input, int x, int y, char regionSymbol, List<(int, int)> visitedFields)
    {
        if (x < 0 || x >= input[0].Length || y < 0 || y >= input.Count || input[y][x] != regionSymbol)
            return (0, 1);
        
        if (visitedFields.Contains((x, y))) return (0, 0);
        
        visitedFields.Add((x, y));
        _visitedRegions.Add((x, y));

        (int, int) north = RecursiveCost(input, x, y+1, regionSymbol, visitedFields);
        (int, int) east = RecursiveCost(input, x+1, y, regionSymbol, visitedFields);
        (int, int) south = RecursiveCost(input, x, y-1, regionSymbol, visitedFields);
        (int, int) west = RecursiveCost(input, x-1, y, regionSymbol, visitedFields);
        
        return (1 + north.Item1 + east.Item1 + south.Item1 + west.Item1,
                north.Item2 + east.Item2 + south.Item2 + west.Item2);
    }

    public override long CalculatePart02(string kindOfInput, string pathPrefix)
    {
        List<string> input = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt");
        int answerValue = 0;
        
        return answerValue;
    }
}
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
        
        _visitedRegions.Clear();
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
        if (kindOfInput == "input") return -1;
        
        List<string> input = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt");
        input.Reverse();

        for (int i = 0; i < input.Count; i++)
        {
            input[i] = input[i].Insert(0, "_");
            input[i] = input[i].Insert(input[i].Length, "_");
        }
        
        input.Insert(0, string.Join("", input[0].ToList().ConvertAll(x => '_')));
        input.Add(string.Join("", input[0].ToList().ConvertAll(x => '_')));
        
        foreach (string str in input)
            Console.WriteLine(str);
        
        int answerValue = 0;
        
        int[,] isPointEdge = new int[input[0].Length + 2, input.Count + 2];
        
        for (int x = 1; x < input[0].Length; x++)
        {
            for (int y = 1; y < input.Count; y++)
            {
                char regionSymbol = 'E';

                bool[] neighbourhood = [false, false, false, false];

                neighbourhood[0] = input[y - 1][x - 1] == regionSymbol;
                neighbourhood[1] = input[y][x - 1] == regionSymbol;
                neighbourhood[2] = input[y - 1][x] == regionSymbol;
                neighbourhood[3] = input[y][x] == regionSymbol;
                
                int adjacentSum = neighbourhood.Count(b => b);

                if (adjacentSum % 2 == 1) isPointEdge[x, y] = 1;
                else if (adjacentSum == 2)
                {
                    if (neighbourhood[0] && !neighbourhood[1] && !neighbourhood[2] && neighbourhood[3])
                        isPointEdge[x, y] = 2;
                    if (!neighbourhood[0] && neighbourhood[1] && neighbourhood[2] && !neighbourhood[3])
                        isPointEdge[x, y] = 2;
                }
            }
        }
        
        for (int i = 0; i < input[0].Length + 1; i++)
        {
            for (int j = 0; j < input.Count + 1; j++)
            {
                Console.WriteLine($"({i},{j}): {isPointEdge[i, j]}");
                answerValue += isPointEdge[i, j];
            }
        }
        
        return answerValue;
    }

    private int Ceil(float x) => (int)float.Ceiling(x);
    private int Floor(float x) => (int)float.Floor(x);
}
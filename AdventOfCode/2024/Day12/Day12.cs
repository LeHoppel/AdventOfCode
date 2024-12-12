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
        int answerValue = 0;

        int[,][] edges = CalculateEdges(input);

        //Console.WriteLine($"Field 3,0 ({input[0][3]}): "+ string.Join(",", edges[3,0]));
        //Console.WriteLine($"Field 4,1 ({input[1][4]}): "+ string.Join(",", edges[4,1]));
        //Console.WriteLine($"Field 5,1 ({input[1][5]}): "+ string.Join(",", edges[5,1]));

        _visitedRegions.Clear();
        for (int x = 0; x < input[0].Length; x++)
        {
            for (int y = 0; y < input.Count; y++)
            {
                if (_visitedRegions.Contains((x, y))) continue;
                if (edges[x, y].Sum() == 0) continue;
                
                TraverseBorder(input, x, y, edges);
                //(int area, int perimeter) = RecursiveCost(input, x, y, input[y][x], new List<(int, int)>());

                //answerValue += area * perimeter;
            }
        }
        
        return answerValue;
    }

    private void TraverseBorder(List<string> input, int x, int y, int[,][] edges)
    {
        Console.WriteLine($"Field {x},{y} ({input[y][x]}): "+ string.Join(",", edges[x,y]));
        
        switch (edges[x,y])
        {
            case [0,0,0,1]:
                break;
            case [0,0,1,0]:
                break;
            case [0,0,1,1]:
                break;
            case [0,1,0,0]:
                break;
            case [0,1,0,1]:
                break;
            case [0,1,1,0]:
                break;
            case [0,1,1,1]:
                break;
            case [1,0,0,1]:
                break;
            case [1,0,1,0]:
                break;
            case [1,0,1,1]:
                break;
            case [1,1,0,0]:
                break;
            case [1,1,0,1]:
                break;
            case [1,1,1,0]:
                break;
            case [1,1,1,1]:
                break;
        }
    }

    private static int[,][] CalculateEdges(List<string> input)
    {
        int[,][] edges = new int[input[0].Length, input.Count][];
        for (int x = 0; x < input[0].Length; x++)
        {
            for (int y = 0; y < input.Count; y++)
            {
                char regionSymbol = input[y][x];

                edges[x, y] = new int[4];

                if (y == input.Count - 1 || input[y + 1][x] != regionSymbol) edges[x, y][0] = 1;
                if (x == input[0].Length - 1 || input[y][x + 1] != regionSymbol) edges[x, y][1] = 1;
                if (y == 0 || input[y - 1][x] != regionSymbol) edges[x, y][2] = 1;
                if (x == 0 || input[y][x - 1] != regionSymbol) edges[x, y][3] = 1;
            }
        }

        return edges;
    }
}
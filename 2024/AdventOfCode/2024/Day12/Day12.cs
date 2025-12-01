namespace AdventOfCode._2024;

public class Day12 : Day
{
    public override bool PrintTime { get => true; set { } }

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
        //if (kindOfInput == "input") return -1;
        
        List<string> input = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt");
        input.Reverse();

        PadInput(input);

        _visitedRegions = new();
        List<List<string>> sanitizedInput = EmptySanitizedInput(input);

        int idCounter = 0;
        for (int x = 0; x < input[0].Length; x++)
        {
            for (int y = 0; y < input.Count; y++)
            {
                if (_visitedRegions.Contains((x, y))) continue;
                
                UpdateRegionIds(sanitizedInput, input, x, y, input[y][x], idCounter, new());
                idCounter++;
            }
        }

        //foreach (var str in sanitizedInput)
            // Console.WriteLine(string.Join(" ", str));
        
        int answerValue = 0;
        
        for (int iD = 1; iD <= idCounter; iD++)
        {
            int[,] isPointEdge = new int[input[0].Length + 2, input.Count + 2];
            
            _visitedRegions.Clear();
            for (int x = 1; x < input[0].Length; x++)
            {
                for (int y = 1; y < input.Count; y++)
                {
                    if (_visitedRegions.Contains((x,y))) continue;
                    
                    _visitedRegions.Add((x, y));
                    string regionSymbol = iD.ToString();

                    bool[] neighbourhood = [false, false, false, false];

                    neighbourhood[0] = sanitizedInput[y - 1][x - 1] == regionSymbol;
                    neighbourhood[1] = sanitizedInput[y][x - 1] == regionSymbol;
                    neighbourhood[2] = sanitizedInput[y - 1][x] == regionSymbol;
                    neighbourhood[3] = sanitizedInput[y][x] == regionSymbol;
                
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

            int edgeCountForThisId = 0;
            for (int i = 0; i < input[0].Length + 1; i++)
            {
                for (int j = 0; j < input.Count + 1; j++)
                {
                    // Console.WriteLine($"({i},{j}): {isPointEdge[i, j]}");
                    edgeCountForThisId += isPointEdge[i, j];
                }
            }
            
            answerValue += edgeCountForThisId * CalcArea(sanitizedInput, iD.ToString());
            Console.WriteLine($"Num. ids: {idCounter}, answer value: {answerValue}, iter: {iD}");
        }
        
        
        
        return answerValue;
    }

    private int CalcArea(List<List<string>> sanitizedInput, string iD)
    {
        int area = 0;
        
        for (int x = 0; x < sanitizedInput[0].Count; x++)
            for (int y = 0; y < sanitizedInput.Count; y++)
                area += sanitizedInput[y][x] == iD ? 1 : 0;
        
        return area;
    }

    private static List<List<string>> EmptySanitizedInput(List<string> input)
    {
        List<List<string>> sanitizedInput = new();
        for (int x = 0; x < input[0].Length; x++)
        {
            sanitizedInput.Add(new List<string>());
            for (int y = 0; y < input.Count; y++)
            {
                sanitizedInput[x].Add("");
            }
        }

        return sanitizedInput;
    }

    private void UpdateRegionIds(List<List<string>> sanitizedInput, List<string> input, int x, int y, char regionSymbol, int idCounter, List<(int, int)> visitedFields)
    {
        if (x < 0 || x >= input[0].Length || y < 0 || y >= input.Count || input[y][x] != regionSymbol)
            return;

        if (visitedFields.Contains((x, y))) return;
        
        visitedFields.Add((x, y));
        _visitedRegions.Add((x, y));
        sanitizedInput[y][x] = idCounter.ToString();
        
        UpdateRegionIds(sanitizedInput, input, x+1, y, regionSymbol, idCounter, visitedFields);
        UpdateRegionIds(sanitizedInput, input, x, y+1, regionSymbol, idCounter, visitedFields);
        UpdateRegionIds(sanitizedInput, input, x-1, y, regionSymbol, idCounter, visitedFields);
        UpdateRegionIds(sanitizedInput, input, x, y-1, regionSymbol, idCounter, visitedFields);
    }

    private static void PadInput(List<string> input)
    {
        for (int i = 0; i < input.Count; i++)
        {
            input[i] = input[i].Insert(0, "_");
            input[i] = input[i].Insert(input[i].Length, "_");
        }
        
        input.Insert(0, string.Join("", input[0].ToList().ConvertAll(x => '_')));
        input.Add(string.Join("", input[0].ToList().ConvertAll(x => '_')));
    }
}
using AdventOfCode.Utility;

namespace AdventOfCode._2024;

public class Day18 : Day
{
    public override bool PrintTime { get => true; set { } }
    
    public override long CalculatePart01(string kindOfInput, string pathPrefix)
    {
        List<string> input = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt");

        int dimensions = kindOfInput == "input" ? 70 : 6;
        Dictionary<VecInt2, bool> corruptedFields = new();

        for (int x = 0; x <= dimensions; x++)
            for (int y = 0; y <= dimensions; y++)
                corruptedFields.Add(new VecInt2(x, y), false);

        int maxCorruption = kindOfInput == "input" ? 1024 : 12;
        for (int i = 0; i < maxCorruption; i++)
            corruptedFields[new VecInt2(int.Parse(input[i].Split(',')[0]), int.Parse(input[i].Split(',')[1]))] = true;

        // PrintMap(dimensions, corruptedFields);

        int answerValue = FindShortestPath(dimensions, corruptedFields);;

        return answerValue;
    }

    private int FindShortestPath(int dimensions, Dictionary<VecInt2, bool> corruptedFields)
    {
        Dictionary<VecInt2, int> distanceFromStart = new();
        for (int x = 0; x <= dimensions; x++)
            for (int y = 0; y <= dimensions; y++)
                distanceFromStart.Add(new VecInt2(x, y), int.MaxValue);
        
        VecInt2 start = new VecInt2(0, 0);
        VecInt2 exit = new VecInt2(dimensions, dimensions);
        
        PriorityQueue<VecInt2, int> queue = new();
        queue.Enqueue(start, 0);

        while (queue.Count > 0)
        {
            queue.TryDequeue(out VecInt2 currentField, out int entryDistance);
            //if (queue.Count % 5000 == 0) Console.WriteLine($"Heartbeat: {currentField}, Count: {queue.Count}, Distance: {entryDistance}");
            
            if (corruptedFields[currentField]) continue;
            if (currentField == exit) continue;
            
            if (distanceFromStart[currentField] < entryDistance) continue;
            distanceFromStart[currentField] = entryDistance;
            
            foreach (VecInt2 dir in (VecInt2[])[VecInt2.Up, VecInt2.Right, VecInt2.Down, VecInt2.Left])
            {
                VecInt2 nextField = currentField + dir;
                if (!distanceFromStart.ContainsKey(nextField) || !corruptedFields.ContainsKey(nextField) || corruptedFields[nextField]) continue;
                if (distanceFromStart[nextField] <= distanceFromStart[currentField] + 1) continue;

                distanceFromStart[nextField] = distanceFromStart[currentField] + 1;
                queue.Enqueue(nextField, distanceFromStart[nextField]);
            }
        }
        
        return distanceFromStart[exit];
    }

    private void PrintMap(int dimensions, Dictionary<VecInt2, bool> corruptedFields)
    {
        for (int y = 0; y <= dimensions; y++)
        {
            Console.Write("\n");
            for (int x = 0; x <= dimensions; x++)
            {
                if (corruptedFields[new VecInt2(x, y)]) Console.Write("#");
                else Console.Write(".");
            }
        }
    }

    public override long CalculatePart02(string kindOfInput, string pathPrefix)
    {
        List<string> input = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt");

        int dimensions = kindOfInput == "input" ? 70 : 6;
        Dictionary<VecInt2, bool> corruptedFields = new();

        for (int x = 0; x <= dimensions; x++)
        for (int y = 0; y <= dimensions; y++)
            corruptedFields.Add(new VecInt2(x, y), false);

        VecInt2 latestCorruption = new VecInt2(-1, -1);
        foreach (string line in input)
        {
            latestCorruption = new VecInt2(int.Parse(line.Split(',')[0]), int.Parse(line.Split(',')[1]));
            corruptedFields[latestCorruption] = true;
            
            int pathLength = FindShortestPath(dimensions, corruptedFields);
            if (pathLength == int.MaxValue) break;
        }
        
        Console.WriteLine($"Day 18 second part for {kindOfInput}: {latestCorruption}");
        return -1;
    }
}
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
        Dictionary<VecInt2, int> distanceFromStart = new();
        for (int x = 0; x <= dimensions; x++)
        {
            for (int y = 0; y <= dimensions; y++)
            {
                corruptedFields.Add(new VecInt2(x, y), false);
                distanceFromStart.Add(new VecInt2(x, y), int.MaxValue);
            }
        }

        int maxCorruption = kindOfInput == "input" ? 1024 : 12;
        for (int i = 0; i < maxCorruption; i++)
            corruptedFields[new VecInt2(int.Parse(input[i].Split(',')[0]), int.Parse(input[i].Split(',')[1]))] = true;

        // PrintMap(dimensions, corruptedFields);

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
        
        
        
        int answerValue = distanceFromStart[exit];

        return answerValue;
    }

    private static void PrintMap(int dimensions, Dictionary<VecInt2, bool> corruptedFields)
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
        if (kindOfInput == "input") return -1;
        List<string> input = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt");
        int answerValue = 0;
        
        return answerValue;
    }
}
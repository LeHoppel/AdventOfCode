using System.Data;
using AdventOfCode.Utility;

namespace AdventOfCode._2024;

public class Day16 : Day
{
    public override bool PrintTime { get => true; set { } }
    
    public override long CalculatePart01(string kindOfInput, string pathPrefix)
    {
        List<string> input = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt");

        VecInt2 exit = new VecInt2(-1, -1);
        VecInt2 start = new VecInt2(-1, -1);
        
        Dictionary<VecInt2, bool> isWall = new();
        Dictionary<VecInt2, int> costToFields = new();
        
        for (int y = 0; y < input.Count; y++)
        {
            for (int x = 0; x < input[0].Length; x++)
            {
                costToFields[new VecInt2(x,y)] = int.MaxValue;
                
                switch (input[y][x])
                {
                    case '.':
                        isWall[new VecInt2(x, y)] = false;
                        break;
                    case 'S':
                        start = new VecInt2(x, y);
                        isWall[start] = false;
                        break;
                    case 'E':
                        exit = new VecInt2(x, y);
                        isWall[exit] = false;
                        break;
                    case '#':
                        isWall[new VecInt2(x, y)] = true;
                        break;
                }
                    
            }
        }
        
        PriorityQueue<(VecInt2, VecInt2, int), int> queue = new();
        
        queue.Enqueue((start, VecInt2.Right, 0), 0);

        while (queue.Count > 0)
        {
            queue.TryDequeue(out (VecInt2, VecInt2, int) tuple, out int entryDistance);
            
            VecInt2 currentField = tuple.Item1;
            VecInt2 entryOrientation = tuple.Item2;
            int entryCost = tuple.Item3;

            if (isWall[currentField]) continue;
            if (currentField == exit) continue;
            
            if (entryCost + 1 < costToFields[currentField + entryOrientation])
            {
                costToFields[currentField + entryOrientation] = entryCost + 1;
                queue.Enqueue((currentField + entryOrientation, entryOrientation, entryCost + 1), entryDistance + 1);
            }
            if (entryCost + 1001 < costToFields[currentField + entryOrientation.RotateClockwise()])
            {
                costToFields[currentField + entryOrientation.RotateClockwise()] = entryCost + 1001;
                queue.Enqueue((currentField + entryOrientation.RotateClockwise(), entryOrientation.RotateClockwise(), entryCost + 1001), entryDistance + 1);
            }
            if (entryCost + 1001 < costToFields[currentField + entryOrientation.RotateCounterClockwise()])
            {
                costToFields[currentField + entryOrientation.RotateCounterClockwise()] = entryCost + 1001;
                queue.Enqueue((currentField + entryOrientation.RotateCounterClockwise(), entryOrientation.RotateCounterClockwise(), entryCost + 1001), entryDistance + 1);
            }
        }
        
        int answerValue = costToFields[exit];
        return answerValue;
    }
    
    public override long CalculatePart02(string kindOfInput, string pathPrefix)
    {
        //if (kindOfInput == "input") return -1;
        List<string> input = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt");

        VecInt2 exit = new VecInt2(-1, -1);
        VecInt2 start = new VecInt2(-1, -1);
        
        Dictionary<VecInt2, bool> isWall = new();
        Dictionary<(VecInt2, VecInt2), int> costFromStart = new();
        
        for (int y = 0; y < input.Count; y++)
        {
            for (int x = 0; x < input[0].Length; x++)
            {
                VecInt2 currentField = new VecInt2(x, y);
                costFromStart[(currentField, VecInt2.Up)] = int.MaxValue;
                costFromStart[(currentField, VecInt2.Right)] = int.MaxValue;
                costFromStart[(currentField, VecInt2.Down)] = int.MaxValue;
                costFromStart[(currentField, VecInt2.Left)] = int.MaxValue;
                
                switch (input[y][x])
                {
                    case '.':
                        isWall[currentField] = false;
                        break;
                    case 'S':
                        start = currentField;
                        isWall[start] = false;
                        break;
                    case 'E':
                        exit = currentField;
                        isWall[exit] = false;
                        break;
                    case '#':
                        isWall[currentField] = true;
                        break;
                }
                    
            }
        }

        costFromStart[(start, VecInt2.Right)] = 0;
        PriorityQueue<(VecInt2, VecInt2), int> queue = new();
        queue.Enqueue((start, VecInt2.Right), 0);

        while (queue.Count > 0)
        {
            (VecInt2, VecInt2) currentWithDir = queue.Dequeue();
            
            if (currentWithDir.Item1 == exit) continue;

            var straight = (currentWithDir.Item1 + currentWithDir.Item2, currentWithDir.Item2);
            var cw = (currentWithDir.Item1, currentWithDir.Item2.RotateClockwise());
            var ccw = (currentWithDir.Item1, currentWithDir.Item2.RotateCounterClockwise());
            
            if (!isWall[straight.Item1] && costFromStart[straight] > costFromStart[currentWithDir] + 1)
            {
                costFromStart[straight] = costFromStart[currentWithDir] + 1;
                queue.Enqueue(straight, costFromStart[straight]);
            }
            if (!isWall[cw.Item1] && costFromStart[cw] > costFromStart[currentWithDir] + 1000)
            {
                costFromStart[cw] = costFromStart[currentWithDir] + 1000;
                queue.Enqueue(cw, costFromStart[cw]);
            }
            if (!isWall[ccw.Item1] && costFromStart[ccw] > costFromStart[currentWithDir] + 1000)
            {
                costFromStart[ccw] = costFromStart[currentWithDir] + 1000;
                queue.Enqueue(ccw, costFromStart[ccw]);
            }
        }

        List<int> exitCosts = [costFromStart[(exit, VecInt2.Up)], costFromStart[(exit, VecInt2.Right)], costFromStart[(exit, VecInt2.Down)], costFromStart[(exit, VecInt2.Left)]];
        int answerValue = exitCosts.Min();
        return answerValue;
    }
}




















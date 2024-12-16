using AdventOfCode.Utility;

namespace AdventOfCode._2024;

public class Day16 : Day
{
    public override bool PrintTime { get => false; set { } }
    
    public override long CalculatePart01(string kindOfInput, string pathPrefix)
    {
        //if (kindOfInput == "input") return -1;
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
        
        // field, orientation, cost, distance to start
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

    private void LookAtNeighbouringFields(VecInt2 entryOrientation, int entryCost, Dictionary<VecInt2, int> costToFields, VecInt2 currentField, PriorityQueue<(VecInt2, VecInt2, int), int> queue, int entryDistance)
    {
        
    }

    private Dictionary<(VecInt2, VecInt2), int> _visitedFields = new();
    private int RecursiveAttempt(Dictionary<VecInt2, bool> fieldBlocked, VecInt2 currentField, VecInt2 exit, VecInt2 orientation, int entryCost = 0)
    {
        if (fieldBlocked[currentField])
            return 999999999;
    
        if (entryCost > 80000)
            return 999999999;
        
        if (currentField == exit)
        {
            _visitedFields.TryAdd((currentField, orientation), entryCost);
            if (_visitedFields[(currentField, orientation)] < entryCost)
                return 999999999;
            else 
                _visitedFields[(currentField, orientation)] = Math.Min(_visitedFields[(currentField, orientation)], entryCost);
            
            return entryCost;
        }
        
        if (_visitedFields.Any(kvp => kvp.Key.Item1 == exit))
        {
            int? minCostToExit = _visitedFields.Where(kvp => kvp.Key.Item1 == exit).Select(kvp => kvp.Value)?.Min();
            if (minCostToExit != null && minCostToExit < entryCost) return 999999999;
        }
        
        _visitedFields.TryAdd((currentField, orientation), entryCost);
        if (_visitedFields[(currentField, orientation)] < entryCost)
            return 999999999;
        else 
            _visitedFields[(currentField, orientation)] = Math.Min(_visitedFields[(currentField, orientation)], entryCost);
        
    
        
        int rotateCCw = RecursiveAttempt(fieldBlocked, currentField, exit, orientation.RotateCounterClockwise(), entryCost + 1000);
        int rotateCw = RecursiveAttempt(fieldBlocked, currentField, exit, orientation.RotateClockwise(), entryCost + 1000);
        int goAhead = RecursiveAttempt(fieldBlocked, currentField + orientation, exit, orientation, entryCost + 1);
    
        return ((List<int>) [rotateCw, rotateCCw, goAhead]).Min();
    }

    public override long CalculatePart02(string kindOfInput, string pathPrefix)
    {
        List<string> input = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt");
        int answerValue = 0;
        
        return answerValue;
    }
}
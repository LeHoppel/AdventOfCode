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
        for (int y = 0; y < input.Count; y++)
        {
            for (int x = 0; x < input[0].Length; x++)
            {
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

        Dictionary< (VecInt2, VecInt2), int> costMap = new();
        Queue<(VecInt2, VecInt2)> toDoQueue = new();
        toDoQueue.Append((start, VecInt2.Up));

        List<VecInt2> directions = [VecInt2.Up, VecInt2.Right, VecInt2.Down, VecInt2.Left];
        
        while (toDoQueue.Count > 0)
        {
            (VecInt2 field, VecInt2 orientation) = toDoQueue.Dequeue();

            foreach (VecInt2 dir in directions)
            {
                if (dir == orientation) continue;
                toDoQueue.Append((field + orientation, orientation));
            }

            //if (!costMap.ContainsKey())

        }
        
        
        //int answerValue = CostToReachField(fieldBlocked, start, exit, VecInt2.Right);
        int answerValue = 0;
        Console.WriteLine($"{kindOfInput} = {answerValue}");
        return answerValue;
    }
    
    // private Dictionary<(VecInt2, VecInt2), int> _visitedFields = new();
    // private int CostToReachField(Dictionary<VecInt2, bool> fieldBlocked, VecInt2 currentField, VecInt2 exit, VecInt2 orientation, int entryCost = 0)
    // {
    //     if (fieldBlocked[currentField])
    //         return 999999999;
    //
    //     if (entryCost > 80000)
    //         return 999999999;
    //     
    //     if (currentField == exit)
    //     {
    //         _visitedFields.TryAdd((currentField, orientation), entryCost);
    //         if (_visitedFields[(currentField, orientation)] < entryCost)
    //             return 999999999;
    //         else 
    //             _visitedFields[(currentField, orientation)] = Math.Min(_visitedFields[(currentField, orientation)], entryCost);
    //         
    //         return entryCost;
    //     }
    //     
    //     if (_visitedFields.Any(kvp => kvp.Key.Item1 == exit))
    //     {
    //         int? minCostToExit = _visitedFields.Where(kvp => kvp.Key.Item1 == exit).Select(kvp => kvp.Value)?.Min();
    //         if (minCostToExit != null && minCostToExit < entryCost) return 999999999;
    //     }
    //     
    //     _visitedFields.TryAdd((currentField, orientation), entryCost);
    //     if (_visitedFields[(currentField, orientation)] < entryCost)
    //         return 999999999;
    //     else 
    //         _visitedFields[(currentField, orientation)] = Math.Min(_visitedFields[(currentField, orientation)], entryCost);
    //     
    //
    //     
    //     int rotateCCw = CostToReachField(fieldBlocked, currentField, exit, orientation.RotateCounterClockwise(), entryCost + 1000);
    //     int rotateCw = CostToReachField(fieldBlocked, currentField, exit, orientation.RotateClockwise(), entryCost + 1000);
    //     int goAhead = CostToReachField(fieldBlocked, currentField + orientation, exit, orientation, entryCost + 1);
    //
    //     return ((List<int>) [rotateCw, rotateCCw, goAhead]).Min();
    // }

    public override long CalculatePart02(string kindOfInput, string pathPrefix)
    {
        List<string> input = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt");
        int answerValue = 0;
        
        return answerValue;
    }
}
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
        if (kindOfInput == "input") return -1;
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

        int maxSteps = kindOfInput == "input" ? 428 : 48;
        int maxTurns = kindOfInput == "input" ? 72 : 11;
        TraverseBestPath(start, VecInt2.Right, exit, maxSteps, maxTurns, isWall);
        
        Console.WriteLine(string.Join(", ", _fieldsOnBestPath));
        
        int answerValue = _fieldsOnBestPath.Count;
        return answerValue;
    }

    private Dictionary<(VecInt2, VecInt2, int, int), bool> _visitedFields = new();
    private List<(VecInt2, VecInt2)> _fieldsOnBestPath = new();
    private bool TraverseBestPath(VecInt2 currentField, VecInt2 entryOrient, VecInt2 exit, int stepsLeft, int turnsLeft, Dictionary<VecInt2, bool> isWall)
    {
        if (currentField == exit || _fieldsOnBestPath.Contains((currentField, entryOrient)))
        {
            Console.WriteLine("Found!");
            return true;
        }

        if (_visitedFields.ContainsKey((currentField, entryOrient, stepsLeft, turnsLeft)))
        {
            return _visitedFields[(currentField, entryOrient, stepsLeft, turnsLeft)];
        }
        
        if (isWall[currentField] || stepsLeft == 0 || turnsLeft == 0) 
        {
            //Console.WriteLine($"isWall? {isWall[currentField]}, stepsLeft: {stepsLeft}, turnsLeft: {turnsLeft}");
            return false;
        }
        
        Console.Write(currentField + " -> ");

        bool straight = TraverseBestPath(currentField + entryOrient, entryOrient, exit, stepsLeft - 1, turnsLeft, isWall);
        bool cw = TraverseBestPath(currentField, entryOrient.RotateClockwise(), exit, stepsLeft, turnsLeft - 1, isWall);
        bool ccw = TraverseBestPath(currentField, entryOrient.RotateCounterClockwise(), exit, stepsLeft, turnsLeft - 1, isWall);

        if (straight || cw || ccw)
        {
            _fieldsOnBestPath.Add((currentField, entryOrient));
            if (!_visitedFields.TryAdd((currentField, entryOrient, stepsLeft, turnsLeft), true))
                _visitedFields[(currentField, entryOrient, stepsLeft, turnsLeft)] = true;
            return true;
        }

        if (!_visitedFields.TryAdd((currentField, entryOrient, stepsLeft, turnsLeft), false))
            _visitedFields[(currentField, entryOrient, stepsLeft, turnsLeft)] = false;
        return false;
    }

    // public override long CalculatePart02(string kindOfInput, string pathPrefix)
    // {
    //     if (kindOfInput == "input") return -1;
    //     List<string> input = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt");
    //
    //     VecInt2 exit = new VecInt2(-1, -1);
    //     VecInt2 start = new VecInt2(-1, -1);
    //     
    //     Dictionary<VecInt2, bool> isWall = new();
    //     Dictionary<VecInt2, (int, List<VecInt2>)> costToFieldsWithEntryDirs = new();
    //     
    //     for (int y = 0; y < input.Count; y++)
    //     {
    //         for (int x = 0; x < input[0].Length; x++)
    //         {
    //             costToFieldsWithEntryDirs[new VecInt2(x,y)] = (int.MaxValue, new());
    //             
    //             switch (input[y][x])
    //             {
    //                 case '.':
    //                     isWall[new VecInt2(x, y)] = false;
    //                     break;
    //                 case 'S':
    //                     start = new VecInt2(x, y);
    //                     isWall[start] = false;
    //                     break;
    //                 case 'E':
    //                     exit = new VecInt2(x, y);
    //                     isWall[exit] = false;
    //                     break;
    //                 case '#':
    //                     isWall[new VecInt2(x, y)] = true;
    //                     break;
    //             }
    //                 
    //         }
    //     }
    //     
    //     PriorityQueue<(VecInt2, VecInt2, int), int> queue = new();
    //     
    //     queue.Enqueue((start, VecInt2.Right, 0), 0);
    //
    //     while (queue.Count > 0)
    //     {
    //         queue.TryDequeue(out (VecInt2, VecInt2, int) tuple, out int entryDistance);
    //         
    //         VecInt2 currentField = tuple.Item1;
    //         VecInt2 entryOrientation = tuple.Item2;
    //         int entryCost = tuple.Item3;
    //
    //         if (isWall[currentField]) continue;
    //         if (currentField == exit) continue;
    //         
    //         if (entryCost + 1 < costToFieldsWithEntryDirs[currentField + entryOrientation].Item1)
    //         {
    //             List<VecInt2> nextEntryDirs = costToFieldsWithEntryDirs[currentField + entryOrientation].Item2;
    //             nextEntryDirs = nextEntryDirs.Append(entryOrientation).ToList();
    //             
    //             costToFieldsWithEntryDirs[currentField + entryOrientation] = (entryCost + 1, nextEntryDirs);
    //             queue.Enqueue((currentField + entryOrientation, entryOrientation, entryCost + 1), entryDistance + 1);
    //         }
    //         if (entryCost + 1001 < costToFieldsWithEntryDirs[currentField + entryOrientation.RotateClockwise()].Item1)
    //         {
    //             List<VecInt2> nextEntryDirs = costToFieldsWithEntryDirs[currentField + entryOrientation.RotateClockwise()].Item2;
    //             nextEntryDirs = nextEntryDirs.Append(entryOrientation.RotateClockwise()).ToList();
    //             
    //             costToFieldsWithEntryDirs[currentField + entryOrientation.RotateClockwise()] = (entryCost + 1001, nextEntryDirs);
    //             queue.Enqueue((currentField + entryOrientation.RotateClockwise(), entryOrientation.RotateClockwise(), entryCost + 1001), entryDistance + 1);
    //         }
    //         if (entryCost + 1001 < costToFieldsWithEntryDirs[currentField + entryOrientation.RotateCounterClockwise()].Item1)
    //         {
    //             List<VecInt2> nextEntryDirs = costToFieldsWithEntryDirs[currentField + entryOrientation.RotateCounterClockwise()].Item2;
    //             nextEntryDirs = nextEntryDirs.Append(entryOrientation.RotateCounterClockwise()).ToList();
    //             
    //             costToFieldsWithEntryDirs[currentField + entryOrientation.RotateCounterClockwise()] = (entryCost + 1001, nextEntryDirs);
    //             queue.Enqueue((currentField + entryOrientation.RotateCounterClockwise(), entryOrientation.RotateCounterClockwise(), entryCost + 1001), entryDistance + 1);
    //         }
    //     }
    //
    //     // VecInt2 orientationOnReachingExit;
    //     // if (costToFieldsWithEntryDirs[exit - VecInt2.Right] == costToFieldsWithEntryDirs[exit] - 1) orientationOnReachingExit = VecInt2.Right;
    //     // else orientationOnReachingExit = VecInt2.Up;
    //
    //     Console.WriteLine($"Bla: {costToFieldsWithEntryDirs[exit]}");
    //     ExitToStartBestPathsTraversal(exit, VecInt2.Up, start, costToFieldsWithEntryDirs, isWall);
    //     
    //     //_fieldsOnBestPath.Add(start);
    //     //_fieldsOnBestPath.Add(exit);
    //     
    //     int answerValue = _fieldsOnBestPath.Count;
    //     
    //     Console.WriteLine("\n" + string.Join(",", _fieldsOnBestPath));
    //     Console.WriteLine($"Entries for {new VecInt2(15,1)}: {string.Join(", ", costToFieldsWithEntryDirs[new VecInt2(15,1)].Item2)}");
    //     return answerValue;
    // }
    //
    // private List<VecInt2> _fieldsOnBestPath = new();
    // private void ExitToStartBestPathsTraversal(VecInt2 currentField, VecInt2 exitOrientation, VecInt2 start, Dictionary<VecInt2, (int, List<VecInt2>)> costToFieldsWithEntryDirs, Dictionary<VecInt2, bool> isWall)
    // {
    //     Console.WriteLine($"Current: {currentField}, exitOrientation: {exitOrientation}");
    //     
    //     if (currentField == start) return;
    //     if (costToFieldsWithEntryDirs[currentField].Item1 == int.MaxValue) return;
    //
    //     if (_fieldsOnBestPath.Contains(currentField)) return;
    //     _fieldsOnBestPath.Add(currentField);
    //
    //     Console.WriteLine($"{currentField - exitOrientation}, {currentField - exitOrientation - exitOrientation.RotateClockwise()}, {currentField - exitOrientation - exitOrientation.RotateCounterClockwise()}");
    //
    //     foreach (VecInt2 entryOrientation in costToFieldsWithEntryDirs[currentField].Item2)
    //     {
    //         ExitToStartBestPathsTraversal(currentField - entryOrientation, entryOrientation, start, costToFieldsWithEntryDirs, isWall);
    //     }
    // }
}
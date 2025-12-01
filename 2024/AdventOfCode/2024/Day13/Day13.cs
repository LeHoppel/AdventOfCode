using System.Text.RegularExpressions;

namespace AdventOfCode._2024;

public class Day13 : Day
{
    public override bool PrintTime { get => true; set { } }
    
    private List<(long, long)> _visitedFields = new();
    
    public override long CalculatePart01(string kindOfInput, string pathPrefix)
    {
        return -1;
        List<string> input = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt");
        
        List<long[]> automatons = new List<long[]>();
        for (int i = 0; i < input.Count; i += 4)
        {
            MatchCollection matches = Regex.Matches(input[i]+input[i+1]+input[i+2], "(\\+|-|=)([0-9]*)");

            long[] automaton = 
            {
                long.Parse(matches[0].Value.Remove(0, 1)), long.Parse(matches[1].Value.Remove(0, 1)),
                long.Parse(matches[2].Value.Remove(0, 1)), long.Parse(matches[3].Value.Remove(0, 1)),
                long.Parse(matches[4].Value.Remove(0, 1)), long.Parse(matches[5].Value.Remove(0, 1))
            };
            automatons.Add(automaton);
        }

        long answerValue = 0;
        
        foreach (long[] automaton in automatons)
        {
            _visitedFields = new();
            long result = RecursiveTokenCalc(automaton);
            answerValue += result == long.MaxValue-1 ? 0 : result;
        }

        return answerValue;
    }
    
    private long RecursiveTokenCalc(long[] automaton, long x = 0, long y = 0, long aButton = 0, long bButton = 0)
    {
        if (_visitedFields.Contains((x, y))) 
            return long.MaxValue-1;
        
        _visitedFields.Add((x, y));
        if (x > automaton[4] || y > automaton[5] || aButton >= 100 || bButton >= 100) 
            return long.MaxValue-1;

        if (x == automaton[4] && y == automaton[5])
            return 0;

        long resultA = RecursiveTokenCalc(automaton, x + automaton[0], y + automaton[1], aButton + 1, bButton);
        long resultB = RecursiveTokenCalc(automaton, x + automaton[2], y + automaton[3], aButton, bButton + 1);
        
        if (resultA < resultB)
            return resultA == long.MaxValue-1 ? long.MaxValue-1 : 3 + resultA;
        return resultB == long.MaxValue-1 ? long.MaxValue-1 : 1 + resultB;
    }

    public override long CalculatePart02(string kindOfInput, string pathPrefix)
    {
        List<string> input = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt");
        
        List<long[]> automatons = new List<long[]>();
        for (int i = 0; i < input.Count; i += 4)
        {
            MatchCollection matches = Regex.Matches(input[i]+input[i+1]+input[i+2], "(\\+|-|=)([0-9]*)");

            long[] automaton = 
            {
                long.Parse(matches[0].Value.Remove(0, 1)), long.Parse(matches[1].Value.Remove(0, 1)),
                long.Parse(matches[2].Value.Remove(0, 1)), long.Parse(matches[3].Value.Remove(0, 1)),
                long.Parse(matches[4].Value.Remove(0, 1)), long.Parse(matches[5].Value.Remove(0, 1))
            };
            automatons.Add(automaton);
        }

        long answerValue = 0;
        
        foreach (long[] automaton in automatons)
        {
            (long, long) result = SolveAutomaton(automaton, 10000000000000);
            
            if (result != (-1, -1)) answerValue += 3 * result.Item1 + result.Item2;
        }
        
        return answerValue;
    }

    private (long, long) SolveAutomaton(long[] automaton, long prizeOffset = 0)
    {
        long ax = automaton[0];
        long ay = automaton[1];
        long bx = automaton[2];
        long by = automaton[3];

        long px = automaton[4] + prizeOffset;
        long py = automaton[5] + prizeOffset;

        long a = (px * by - py * bx) / (ax * by - ay * bx);
        long b = (px - a * ax) / bx;
        
        if (a < 0 || a * ax + b * bx != px || b < 0 || a * ay + b * by != py) 
            return (-1, -1);
        
        return (a, b);
    }
}
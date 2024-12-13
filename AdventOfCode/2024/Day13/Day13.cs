using System.Text.RegularExpressions;

namespace AdventOfCode._2024;

public class Day13 : Day
{
    public override bool PrintTime { get => true; set { } }
    
    private List<(long, long)> _visitedFields = new();
    
    public override long CalculatePart01(string kindOfInput, string pathPrefix)
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
        long answerValue = 0;
        
        return answerValue;
    }
}
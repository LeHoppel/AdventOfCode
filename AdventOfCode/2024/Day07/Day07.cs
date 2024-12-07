namespace AdventOfCode._2024;

public class Day07 : Day
{
    public override int CalculatePart01(string kindOfInput, string pathPrefix)
    {
        List<string> input = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt");
        long answerValue = 0;

        foreach (string line in input)
        {
            List<string> splitLine = line.Split([':',' ']).ToList();
            long testValue = long.Parse(splitLine[0]);
            splitLine.RemoveRange(0, 2);

            answerValue += ReduceLineValue(splitLine, testValue) ? testValue : 0;
        }
        
        Console.WriteLine($"Day 7 part 1 {kindOfInput}: {answerValue}");
        return -1;
    }

    private bool ReduceLineValue(List<string> splitLine, long testValue)
    {
        if (testValue < 0) return false;
        
        if (splitLine.Count == 1)
            return testValue == long.Parse(splitLine[0]);
        
        long last = long.Parse(splitLine.Last());

        if (testValue % last != 0) return ReduceLineValue(splitLine.GetRange(0, splitLine.Count - 1), testValue - last);
        
        return ReduceLineValue(splitLine.GetRange(0, splitLine.Count - 1), testValue / last)
            || ReduceLineValue(splitLine.GetRange(0, splitLine.Count - 1), testValue - last);
    }

    public override int CalculatePart02(string kindOfInput, string pathPrefix)
    {
        List<string> input = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt");
        long answerValue = 0;

        foreach (string line in input)
        {
            List<string> splitLine = line.Split([':',' ']).ToList();
            long testValue = long.Parse(splitLine[0]);
            splitLine.RemoveRange(0, 2);

            answerValue += ReduceLineValueWithConcat(splitLine, testValue) ? testValue : 0;
        }
        
        Console.WriteLine($"Day 7 part 2 {kindOfInput}: {answerValue}");
        return -1;
    }

    private bool ReduceLineValueWithConcat(List<string> splitLine, long testValue, long runningValue = 0)
    {
        if (runningValue > testValue) return false;
        
        long first = long.Parse(splitLine.First());
        
        if (splitLine.Count == 1)
            return testValue == runningValue + first || testValue == runningValue * first || testValue == long.Parse(runningValue.ToString() + first.ToString());
            
        return ReduceLineValueWithConcat(splitLine.GetRange(1, splitLine.Count - 1), testValue, runningValue + first)
            || ReduceLineValueWithConcat(splitLine.GetRange(1, splitLine.Count - 1), testValue, runningValue != 0 ? runningValue * first : first)
            || ReduceLineValueWithConcat(splitLine.GetRange(1, splitLine.Count - 1), testValue, long.Parse(runningValue.ToString() + first.ToString()));
    }
}
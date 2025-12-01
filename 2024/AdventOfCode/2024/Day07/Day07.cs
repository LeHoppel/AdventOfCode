namespace AdventOfCode._2024;

public class Day07 : Day
{
    public override bool PrintTime { get => false; set { } }
    
    public override long CalculatePart01(string kindOfInput, string pathPrefix)
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
        
        return answerValue;
    }

    private bool ReduceLineValue(List<string> splitLine, long testValue, long runningValue = 0)
    {
        if (runningValue > testValue) return false;
        
        long first = long.Parse(splitLine.First());
        
        if (splitLine.Count == 1)
            return testValue == runningValue + first || testValue == runningValue * first;

        return ReduceLineValue(splitLine.GetRange(1, splitLine.Count - 1), testValue, runningValue + first)
               || ReduceLineValue(splitLine.GetRange(1, splitLine.Count - 1), testValue, runningValue != 0 ? runningValue * first : first);
    }

    public override long CalculatePart02(string kindOfInput, string pathPrefix)
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
        
        return answerValue;
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
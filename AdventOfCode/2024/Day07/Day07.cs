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
        int answerValue = 0;
        
        return answerValue;
    }
}

// private bool ReduceLineValue(List<string> splitLine, long testValue)
// {
//     Console.WriteLine($"TestValue: {testValue}");
//     if (testValue < 0) return false;
//         
//     if (splitLine.Count == 1)
//         return testValue == long.Parse(splitLine[0]);
//         
//     long last = long.Parse(splitLine.Last());
//     long beforeLast = long.Parse(splitLine[^2]);
//         
//     Console.WriteLine($"Last: {last}, Before: {beforeLast}, Value: {testValue}");
//
//     if (splitLine.Count == 2)
//         return testValue == last * beforeLast || testValue == last + beforeLast;
//         
//     return ReduceLineValue(splitLine.GetRange(0, splitLine.Count - 2), testValue - (last * beforeLast))
//            || ReduceLineValue(splitLine.GetRange(0, splitLine.Count - 2), testValue - (last + beforeLast));
// }
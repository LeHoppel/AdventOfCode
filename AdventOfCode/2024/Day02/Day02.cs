namespace AdventOfCode._2024.Day02;

public static class Day02
{
    public static int CalculateAnswer()
    {
        StreamReader streamReader = new StreamReader("D:\\Lennart\\Git\\AdventOfCode\\AdventOfCode\\2024\\Day02\\input.txt");
        string? currentLine = streamReader.ReadLine(); 
        int answerValue = 0;

        while (currentLine != null)
        {
            List<int> lineNumbers = new List<int>();
            
            foreach (string linePart in currentLine.Split(" "))
                lineNumbers.Add(int.Parse(linePart));

            bool lineIsSafe = false;
            
            lineIsSafe = lineNumbers[0] < lineNumbers[1] ? CheckSafetyAscending(lineNumbers) : CheckSafetyDescending(lineNumbers);

            if (lineIsSafe) answerValue++;
            
            currentLine = streamReader.ReadLine(); 
        }

        return answerValue;
    }

    private static bool CheckSafetyAscending(List<int> lineNumbers)
    {
        for (int i = 0; i < lineNumbers.Count - 1; i++)
        {
            if (lineNumbers[i] > lineNumbers[i + 1]) return false;
            int difference = int.Abs(lineNumbers[i] - lineNumbers[i + 1]);
            if (difference < 1 || difference > 3) return false;
        }

        return true;
    }
    
    private static bool CheckSafetyDescending(List<int> lineNumbers)
    {
        for (int i = 0; i < lineNumbers.Count - 1; i++)
        {
            if (lineNumbers[i] < lineNumbers[i + 1]) return false;
            int difference = int.Abs(lineNumbers[i] - lineNumbers[i + 1]);
            if (difference < 1 || difference > 3) return false;
        }

        return true;
    }
}
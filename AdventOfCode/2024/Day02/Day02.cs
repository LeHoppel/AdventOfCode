namespace AdventOfCode._2024.Day02;

public static class Day02
{
    public static int CalculatePart01()
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
            
            lineIsSafe = CheckSafety(lineNumbers);

            if (lineIsSafe) answerValue++;
            
            currentLine = streamReader.ReadLine(); 
        }

        return answerValue;
    }

    public static int CalculatePart02()
    {
        StreamReader streamReader = new StreamReader("D:\\Lennart\\Git\\AdventOfCode\\AdventOfCode\\2024\\Day02\\input.txt");
        string? currentLine = streamReader.ReadLine(); 
        int answerValue = 0;

        while (currentLine != null)
        {
            List<int> lineNumbers = new List<int>();
            
            foreach (string linePart in currentLine.Split(" "))
                lineNumbers.Add(int.Parse(linePart));
            
            if (CheckSafety(lineNumbers))
            {
                answerValue++;
                currentLine = streamReader.ReadLine(); 
                continue;
            }

            for (int i = 0; i < lineNumbers.Count; i++)
            {
                List<int> subList = new List<int>(lineNumbers);
                subList.RemoveAt(i);
                
                if (CheckSafety(subList))
                {
                    answerValue++;
                    break;
                }
            }
            currentLine = streamReader.ReadLine(); 
        }

        return answerValue;
    }

    private static bool CheckSafety(List<int> lineNumbers)
    {
        Func<int, int, bool> comparison;

        if (lineNumbers[0] < lineNumbers[1])
            comparison = (x, y) => x < y;
        else
            comparison = (x, y) => x > y;
        
        for (int i = 0; i < lineNumbers.Count - 1; i++)
        {
            if (!comparison(lineNumbers[i],  lineNumbers[i + 1])) return false;
            int difference = int.Abs(lineNumbers[i] - lineNumbers[i + 1]);
            if (difference < 1 || difference > 3) return false;
        }

        return true;
    }
}
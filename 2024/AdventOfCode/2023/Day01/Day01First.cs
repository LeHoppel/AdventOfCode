namespace AdventOfCode._2023.Day01;

public static class Day01First
{
    public static int CalculateAnswer()
    {
        StreamReader streamReader = new StreamReader("C:\\Users\\Lennart\\RiderProjects\\AdventOfCode\\AdventOfCode\\2023\\Day01\\input.txt");
        string? currentLine = streamReader.ReadLine(); 
        int answerValue = 0;

        while (currentLine != null)
        {
            List<char> filteredLine = currentLine.Where(c => char.IsDigit(c)).ToList();
            
            
            if (filteredLine.Count == 1)
                answerValue += int.Parse($"{filteredLine.First()}{filteredLine.First()}");
            if (filteredLine.Count >= 2)
                answerValue += int.Parse($"{filteredLine.First()}{filteredLine.Last()}");

            currentLine = streamReader.ReadLine(); 
        }

        return answerValue;
    }
}
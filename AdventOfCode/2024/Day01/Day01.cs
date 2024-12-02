namespace AdventOfCode._2024.Day01;

public static class Day01
{
    public static int CalculatePart01()
    {
        StreamReader streamReader = new StreamReader("D:\\Lennart\\Git\\AdventOfCode\\AdventOfCode\\2024\\Day01\\input.txt");
        string? currentLine = streamReader.ReadLine(); 
        
        List<int> leftList = new List<int>();
        List<int> rightList = new List<int>();

        while (currentLine != null)
        {
            string[] splitLine = currentLine.Split("   ");
            
            leftList.Add(int.Parse(splitLine[0]));
            rightList.Add(int.Parse(splitLine[1]));
            
            currentLine = streamReader.ReadLine(); 
        }
        
        leftList.Sort();
        rightList.Sort();
        
        int answerValue = 0;
        for (int i = 0; i < leftList.Count; i++)
            answerValue += Int32.Abs(leftList[i] - rightList[i]);
        
        return answerValue;
    }

    public static int CalculatePart02()
    {
        StreamReader streamReader = new StreamReader("D:\\Lennart\\Git\\AdventOfCode\\AdventOfCode\\2024\\Day02\\input.txt");
        string? currentLine = streamReader.ReadLine(); 
        int answerValue = 0;

        while (currentLine != null)
        {
            currentLine = streamReader.ReadLine(); 
        }

        return answerValue;
    }
}
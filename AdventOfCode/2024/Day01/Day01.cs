namespace AdventOfCode._2024;

public class Day01 : Day
{
    public override int CalculatePart01(string kindOfInput, string pathPrefix)
    {
        StreamReader streamReader = new StreamReader(pathPrefix + "\\" + kindOfInput + ".txt");
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

    public override int CalculatePart02(string kindOfInput, string pathPrefix)
    {
        StreamReader streamReader = new StreamReader(pathPrefix + "\\" + kindOfInput + ".txt");
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

        int answerValue = 0;
        foreach (int left in leftList)
            answerValue += left * rightList.Count(x => x == left);

        return answerValue;
    }
}
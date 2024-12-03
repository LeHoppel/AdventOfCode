using System.Text.RegularExpressions;

namespace AdventOfCode._2024.Day03;

public static class Day03
{
    public static int CalculatePart01()
    {
        StreamReader streamReader = new StreamReader("C:\\Users\\Lennart\\RiderProjects\\AdventOfCode\\AdventOfCode\\2024\\Day03\\input.txt");
        string? currentLine = streamReader.ReadLine(); 

        int answerValue = 0;
        
        while (currentLine != null)
        {
            MatchCollection match = Regex.Matches(currentLine, "(mul\\()[0-9]+(,)[0-9]+(\\))");
            foreach (Capture capture in match)
            {
                string[] splitEquation = capture.Value.Split(["mul(", ",", ")"], StringSplitOptions.RemoveEmptyEntries);
                int a = int.Parse(splitEquation[0]);
                int b = int.Parse(splitEquation[1]);
                answerValue += a * b;
            }
            
            currentLine = streamReader.ReadLine();
        }
        
        return answerValue;
    }

    public static int CalculatePart02()
    {
        StreamReader streamReader = new StreamReader("C:\\Users\\Lennart\\RiderProjects\\AdventOfCode\\AdventOfCode\\2024\\Day03\\example.txt");
        string? currentLine = streamReader.ReadLine(); 

        while (currentLine != null)
        {
            
            currentLine = streamReader.ReadLine(); 
        }
       
        int answerValue = 0;
        
        return answerValue;
    }
}
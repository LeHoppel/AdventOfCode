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
            answerValue += EvaluateMuls(currentLine);
            
            currentLine = streamReader.ReadLine();
        }
        
        return answerValue;
    }

    private static int EvaluateMuls(string input)
    {
        int value = 0;
        
        MatchCollection match = Regex.Matches(input, "(mul\\()[0-9]+(,)[0-9]+(\\))");
        foreach (Capture capture in match)
        {
            string[] splitEquation = capture.Value.Split(["mul(", ",", ")"], StringSplitOptions.RemoveEmptyEntries);
            int a = int.Parse(splitEquation[0]);
            int b = int.Parse(splitEquation[1]);
            value += a * b;
        }

        return value;
    }

    public static int CalculatePart02()
    {
        StreamReader streamReader = new StreamReader("C:\\Users\\Lennart\\RiderProjects\\AdventOfCode\\AdventOfCode\\2024\\Day03\\input.txt");
        string? currentLine = streamReader.ReadLine(); 

        int answerValue = 0;
        
        while (currentLine != null)
        {
            string[] splitLine = Regex.Split(currentLine, @"(?=do\(\)|don't\(\))");
            
            foreach (string s in splitLine)
            {
                if (!s.StartsWith("don't()"))
                    answerValue += EvaluateMuls(s);
            }

            currentLine = streamReader.ReadLine(); 
        }
        
        return answerValue;
    }
}
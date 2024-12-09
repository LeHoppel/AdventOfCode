using System.Text.RegularExpressions;

namespace AdventOfCode._2024;

public class Day03 : Day
{
    public override bool PrintTime { get => false; set { } }
    
    public override int CalculatePart01(string kindOfInput, string pathPrefix)
    {
        StreamReader streamReader = new StreamReader(pathPrefix + "\\" + kindOfInput + ".txt");
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

    public override int CalculatePart02(string kindOfInput, string pathPrefix)
    {
        StreamReader streamReader = new StreamReader(pathPrefix + "\\" + kindOfInput + ".txt");
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
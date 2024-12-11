namespace AdventOfCode._2024;

public class Day11 : Day
{
    public override bool PrintTime { get => false; set { } }
    
    public override long CalculatePart01(string kindOfInput, string pathPrefix)
    {
        List<string> input = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt")[0].Split(" ").ToList();;
        int answerValue = 0;
        Console.WriteLine(string.Join(",", input));
        
        for (int i = 0; i < 25; i++)
            input = UpdateStones(input);

        answerValue = input.Count;

        return answerValue;
    }

    private List<string> UpdateStones(List<string> input) 
    {
        for (int i = 0; i < input.Count; i++)
        {  
            if (input[i] == "0")
                input[i] = "1";
            else if (input[i].Length % 2 == 0) 
            {
                string firstHalf = input[i].Substring(0, input[i].Length/2);
                string secondHalf = input[i].Substring(input[i].Length/2, input[i].Length - firstHalf.Length);
   
                input[i] = firstHalf;
                input.Insert(i+1, int.Parse(secondHalf).ToString());
                i++;
            }
            else
                input[i] = (long.Parse(input[i]) * 2024).ToString();
        }
 
        return input;
    }
    
    public override long CalculatePart02(string kindOfInput, string pathPrefix)
    {
        List<string> input = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt");
        int answerValue = 0;
        
        return answerValue;
    }
}
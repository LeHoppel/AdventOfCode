namespace AdventOfCode._2024;

public class Day11 : Day
{
    public override bool PrintTime { get => true; set { } }
    
    public override long CalculatePart01(string kindOfInput, string pathPrefix)
    {
        List<string> input = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt")[0].Split(" ").ToList();
        
        for (int i = 0; i < 25; i++)
            input = UpdateStones(input);

        int answerValue = input.Count;

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
                input.Insert(i, int.Parse(secondHalf).ToString());
                i++;
            }
            else
                input[i] = (long.Parse(input[i]) * 2024).ToString();
        }
 
        return input;
    }
    
    private Dictionary<string, long> UpdateStoneTuples(Dictionary<string, long> input) 
    {
        Dictionary<string, long> temp = new();
        
        foreach (KeyValuePair<string,long> kvp in input)
        {
            if (kvp.Key == "0")
            {
                if (!temp.TryAdd("1", input[kvp.Key]))
                    temp["1"] += input[kvp.Key];
            }
            else if (kvp.Key.Length % 2 == 0) 
            {
                string firstHalf = kvp.Key.Substring(0, kvp.Key.Length/2);
                string secondHalf = kvp.Key.Substring(kvp.Key.Length/2, kvp.Key.Length - firstHalf.Length);
                secondHalf = long.Parse(secondHalf).ToString();
   
                if (!temp.TryAdd(firstHalf, input[kvp.Key]))
                    temp[firstHalf] += input[kvp.Key];
                if (!temp.TryAdd(secondHalf, input[kvp.Key]))
                    temp[secondHalf] += input[kvp.Key];
            }
            else
            {
                string parsed = (long.Parse(kvp.Key) * 2024).ToString();
                if (!temp.TryAdd(parsed, input[kvp.Key]))
                    temp[parsed] += input[kvp.Key];
            }
        }
 
        return temp;
    }
    
    public override long CalculatePart02(string kindOfInput, string pathPrefix)
    {
        List<string> inputStrings = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt")[0].Split(" ").ToList();
        Dictionary<string, long> input = new();

        foreach (string inputString in inputStrings)
            input.Add(inputString, 1);
        
        for (int i = 0; i < 75; i++)
            input = UpdateStoneTuples(input);

        return input.Values.Sum();
    }
}
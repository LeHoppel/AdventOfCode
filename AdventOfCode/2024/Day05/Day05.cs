namespace AdventOfCode._2024;

public class Day05 : Day
{
    public override bool PrintTime { get => false; set { } }
    
    public override long CalculatePart01(string kindOfInput, string pathPrefix)
    {
        List<string> input = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt");
        int answerValue = 0;

        Dictionary<int,List<int>> pageOrderingRules = new Dictionary<int,List<int>>();
        int numberOfRules = 0;
        
        foreach (string line in input)
        {
            if (!line.Contains('|')) break;
            
            string[] lineParts = line.Split("|");
            int left  = int.Parse(lineParts[0]);
            int right = int.Parse(lineParts[1]);


            if (!pageOrderingRules.ContainsKey(left))
            {
                var rules = new List<int>();
                rules.Add(right);
                pageOrderingRules.Add(left, rules);
            }
            else
                pageOrderingRules[left].Add(right);

            numberOfRules++;
        }
        
        input.RemoveRange(0, numberOfRules+1);

        foreach (string line in input)
        {
            string[] lineParts = line.Split(",");
            bool ruleBroken = false;

            for (int i = 0; i < lineParts.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    int left = int.Parse(lineParts[i]);
                    int right = int.Parse(lineParts[j]);
                    
                    if (pageOrderingRules.ContainsKey(left) && pageOrderingRules[left].Contains(right))
                    {
                        ruleBroken = true;
                        break;
                    }
                }

                if (ruleBroken)
                    break;
            }

            if (!ruleBroken)
                answerValue += int.Parse(lineParts[lineParts.Length/2]);
        }
        
        return answerValue;
    }

    public override long CalculatePart02(string kindOfInput, string pathPrefix)
    {
        List<string> input = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt");
        int answerValue = 0;

        Dictionary<int,List<int>> pageOrderingRules = new Dictionary<int,List<int>>();
        int numberOfRules = 0;
        
        foreach (string line in input)
        {
            if (!line.Contains('|')) break;
            
            string[] lineParts = line.Split("|");
            int left  = int.Parse(lineParts[0]);
            int right = int.Parse(lineParts[1]);


            if (!pageOrderingRules.ContainsKey(left))
            {
                var rules = new List<int>();
                rules.Add(right);
                pageOrderingRules.Add(left, rules);
            }
            else
                pageOrderingRules[left].Add(right);

            numberOfRules++;
        }
        
        input.RemoveRange(0, numberOfRules+1);

        foreach (string line in input)
        {
            string[] lineParts = line.Split(",");
            bool ruleBroken = false;

            for (int i = 0; i < lineParts.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    int left = int.Parse(lineParts[i]);
                    int right = int.Parse(lineParts[j]);
                    
                    if (pageOrderingRules.ContainsKey(left) && pageOrderingRules[left].Contains(right))
                    {
                        ruleBroken = true;
                        break;
                    }
                }

                if (ruleBroken)
                {
                    List<string> linePartsList = lineParts.ToList();
                    linePartsList.Sort( (x,y) => pageOrderingRules.ContainsKey(int.Parse(x)) && pageOrderingRules[int.Parse(x)].Contains(int.Parse(y)) ? -1 : 1);
                    
                    answerValue += int.Parse(linePartsList[linePartsList.Count/2]);
                    break;
                }
            }
        }
        
        return answerValue;
    }
}
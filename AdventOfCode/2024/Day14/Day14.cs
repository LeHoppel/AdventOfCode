namespace AdventOfCode._2024;

public class Day14 : Day
{
    public override bool PrintTime { get => false; set { } }
    
    public override long CalculatePart01(string kindOfInput, string pathPrefix)
    {
        List<string> input = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt");

        List<int[]> robots = new List<int[]>();
        
        foreach (string line in input)
        {
            string[] parts = line.Split([',', ' ', '=']);
            robots.Add([int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[4]), int.Parse(parts[5])]);
        }

        int width = 0;
        int height = 0;

        if (kindOfInput == "input")
        {
            width = 101;
            height = 103;
        }
        else
        {
            width = 11;
            height = 7;
        }
        
        int steps = 100;

        foreach (int[] robot in robots)
        {
            robot[0] += robot[2] * steps;
            robot[0] %= width;
            if (robot[0] < 0) robot[0] += width;
            
            robot[1] += robot[3] * steps;
            robot[1] %= height;
            if (robot[1] < 0) robot[1] += height;
        }

        int[] quadrants = new int[4];
        foreach (int[] robot in robots)
        {
            if (robot[0] == width / 2 || robot[1] == height / 2) continue;
            
            if (robot[0] < width / 2)
            {
                if (robot[1] < height / 2)
                    quadrants[0]++;
                else
                    quadrants[1]++;
            }
            else
            {
                if (robot[1] < height / 2)
                    quadrants[2]++;
                else
                    quadrants[3]++;
            }
        }
        
        int answerValue = quadrants[0] * quadrants[1] * quadrants[2] * quadrants[3];

        return answerValue;
    }

    public override long CalculatePart02(string kindOfInput, string pathPrefix)
    {
        if (kindOfInput == "input") return -1;
        List<string> input = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt");
        int answerValue = 0;
        
        return answerValue;
    }
}
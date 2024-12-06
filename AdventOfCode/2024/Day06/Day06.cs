namespace AdventOfCode._2024;

public class Day06 : Day
{
    public override int CalculatePart01(string kindOfInput, string pathPrefix)
    {
        List<string> input = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt");
        input.Reverse();

        (int, int) guardPos = (-1, -1);

        for (int y = 0; y < input.Count; y++)
        {
            if (input[y].Contains('^'))
            {
                guardPos = (input[y].IndexOf('^'), y);
                break;
            }
        }

        int answerValue = 1;
        int currentRotation = 0; // 0 = north, 1 = east, 2 = south, 3 = west

        for (int y = guardPos.Item2; y >= 0 && y < input.Count; )
        {
            for (int x = guardPos.Item1; x >= 0 && x < input[0].Length; )
            {
                if (input[y][x] == '.')
                {
                    answerValue++;
                    input[y] = input[y].Remove(x, 1);
                    input[y] = input[y].Insert(x, "X");
                }
                
                switch (currentRotation)
                {
                    case 0:
                        if (y == input.Count-1) return answerValue;
                        if (input[y+1][x].Equals('#'))
                        {
                            currentRotation++;
                        }
                        else
                            y++;
                        break;
                    case 1:
                        if (x == input[0].Length-1) return answerValue;
                        if (input[y][x+1] == '#')
                        {
                            currentRotation++;
                        }
                        else
                            x++;
                        break;
                    case 2:
                        if (y == 0) return answerValue;
                        if (input[y-1][x] == '#')
                        {
                            currentRotation++;
                        }
                        else
                            y--;
                        break;
                    case 3:
                        if (x == 0) return answerValue;
                        if (input[y][x-1] == '#')
                        {
                            currentRotation = 0;
                        }
                        else
                            x--;
                        break;
                    default:
                        break;
                }
            }
        }
        
        return answerValue;
    }

    public override int CalculatePart02(string kindOfInput, string pathPrefix)
    {
        List<string> input = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt");
        int answerValue = 0;
        
        return answerValue;
    }
}
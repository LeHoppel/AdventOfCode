namespace AdventOfCode._2024;

public class Day06 : Day
{
    public override bool PrintTime { get => true; set { } }
    
    public override int CalculatePart01(string kindOfInput, string pathPrefix)
    {
        List<string> input = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt");
        input.Reverse();

        (int,int) guardStartingPos = CalculateGuardStartingPos(input);

        return CalculateNumberOfSteps(guardStartingPos, input);
    }

    private static (int, int) CalculateGuardStartingPos(List<string> input)
    {
        (int, int) guardStartingPos = (-1, -1);

        for (int y = 0; y < input.Count; y++)
        {
            if (input[y].Contains('^'))
            {
                guardStartingPos = (input[y].IndexOf('^'), y);
                break;
            }
        }

        return guardStartingPos;
    }

    private static int CalculateNumberOfSteps((int, int) guardStartingPos, List<string> freshInput)
    {
        List<string> input = freshInput.ToList();
        int answerValue = 1;
        int currentRotation = 0; // 0 = north, 1 = east, 2 = south, 3 = west
        
        bool[,,] visitedCoords = new bool[input.Count,input[0].Length,4];

        for (int y = guardStartingPos.Item2; y >= 0 && y < input.Count; )
        {
            for (int x = guardStartingPos.Item1; x >= 0 && x < input[0].Length; )
            {
                if (visitedCoords[x,y,currentRotation]) return -1;
                
                if (input[y][x] == '.')
                {
                    answerValue++;
                    input[y] = input[y].Remove(x, 1);
                    input[y] = input[y].Insert(x, "X");
                    visitedCoords[x,y,currentRotation] = true;
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
        //if (kindOfInput == "input") return -1;
        List<string> input = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt");
        input.Reverse();

        (int,int) guardStartingPos = CalculateGuardStartingPos(input);
        
        int answerValue = 0;
        
        for (int y = 0; y < input.Count; y++)
        {
            for (int x = 0; x < input[0].Length; x++)
            {
                if (input[y][x] != '.')
                    continue;
                
                input[y] = input[y].Remove(x, 1);
                input[y] = input[y].Insert(x, "#");

                if (CalculateNumberOfSteps(guardStartingPos, input) == -1) 
                    answerValue++;
                
                input[y] = input[y].Remove(x, 1);
                input[y] = input[y].Insert(x, ".");
            }
        }
        
        return answerValue;
    }
}
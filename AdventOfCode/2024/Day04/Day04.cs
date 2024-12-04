namespace AdventOfCode._2024.Day04;

public static class Day04
{
    public static int CalculatePart01(string kindOfInput)
    {
        StreamReader streamReader = new StreamReader("D:\\Lennart\\Git\\AdventOfCode\\AdventOfCode\\2024\\Day04\\" + kindOfInput + ".txt");
        string? currentLine = streamReader.ReadLine(); 

        int answerValue = 0;
        List<string> input = new List<string>();
        
        while (currentLine != null)
        {
            input.Add(currentLine);
            currentLine = streamReader.ReadLine(); 
        }

        for (int i = 0; i < input[0].Length; i++)
        {
            for (int j = 0; j < input.Count; j++)
            {
                if (input[i][j] != 'X') continue;
                
                // north
                if (j+3 < input.Count)
                    if (input[i][j+1] == 'M' && input[i][j+2] == 'A' && input[i][j+3] == 'S') answerValue += 1;
                
                // north-east
                if (j+3 < input.Count && i+3 < input[0].Length)
                    if (input[i+1][j+1] == 'M' && input[i+2][j+2] == 'A' && input[i+3][j+3] == 'S') answerValue += 1;
                
                // east
                if (i+3 < input[0].Length)
                    if (input[i+1][j] == 'M' && input[i+2][j] == 'A' && input[i+3][j] == 'S') answerValue += 1;
                
                // south-east
                if (j-3 >= 0 && i+3 < input[0].Length)
                    if (input[i+1][j-1] == 'M' && input[i+2][j-2] == 'A' && input[i+3][j-3] == 'S') answerValue += 1;
                
                // south
                if (j-3 >= 0)
                    if (input[i][j-1] == 'M' && input[i][j-2] == 'A' && input[i][j-3] == 'S') answerValue += 1;
                
                // south-west
                if (j-3 >= 0 && i-3 >= 0)
                    if (input[i-1][j-1] == 'M' && input[i-2][j-2] == 'A' && input[i-3][j-3] == 'S') answerValue += 1;
                
                // west
                if (i-3 >= 0)
                    if (input[i-1][j] == 'M' && input[i-2][j] == 'A' && input[i-3][j] == 'S') answerValue += 1;
                
                // north-west
                if (j+3 < input.Count && i-3 >= 0)
                    if (input[i-1][j+1] == 'M' && input[i-2][j+2] == 'A' && input[i-3][j+3] == 'S') answerValue += 1;
                
            }
        }

        return answerValue;
    }

    public static int CalculatePart02(string kindOfInput)
    {
        StreamReader streamReader = new StreamReader("D:\\Lennart\\Git\\AdventOfCode\\AdventOfCode\\2024\\Day04\\" + kindOfInput + ".txt");
        string? currentLine = streamReader.ReadLine(); 

        int answerValue = 0;
        List<string> input = new List<string>();
        
        while (currentLine != null)
        {
            input.Add(currentLine);
            currentLine = streamReader.ReadLine(); 
        }

        for (int i = 0; i < input[0].Length; i++)
        {
            for (int j = 0; j < input.Count; j++)
            {
                if (input[i][j] != 'A') continue;

                bool northEast = false;
                
                // north-east
                if (j-1 >= 0 && j+1 < input.Count && i-1 >= 0 && i+1 < input[0].Length)
                {
                    if (input[i - 1][j - 1] == 'M' && input[i + 1][j + 1] == 'S') northEast = true;
                    if (input[i - 1][j - 1] == 'S' && input[i + 1][j + 1] == 'M') northEast = true;
                }
                
                // south-east
                if (northEast && j-1 >= 0 && j+1 < input.Count && i-1 >= 0 && i+1 < input[0].Length)
                {
                    if (input[i + 1][j - 1] == 'M' && input[i - 1][j + 1] == 'S') answerValue += 1;
                    if (input[i + 1][j - 1] == 'S' && input[i - 1][j + 1] == 'M') answerValue += 1;
                }
            }
        }

        return answerValue;
    }
}
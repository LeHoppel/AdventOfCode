using AdventOfCode.Utility;

namespace AdventOfCode._2024;

public class Day15 : Day
{
    public override bool PrintTime { get => false; set { } }
    
    public override long CalculatePart01(string kindOfInput, string pathPrefix)
    {
        List<string> fullInput = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt");

        int mapSize = kindOfInput == "input" ? 50 : 10;
        List<string> map = fullInput.GetRange(0, mapSize);
        string movement = string.Join("", fullInput.GetRange(mapSize+1, fullInput.Count - mapSize - 1));

        int roboX = -1;
        int roboY = -1;
        for (int x = 0; x < mapSize; x++) 
            for (int y = 0; y < mapSize; y++)
            {
                if (map[y][x] != '@') continue;
                roboX = x;
                roboY = y;
                x = mapSize;
                y = mapSize;
            }
        
        while (movement != string.Empty)
        {
            int[] direction = [movement[0] == '^' ? 1 : 0, movement[0] == '>' ? 1 : 0, movement[0] == 'v' ? 1 : 0, movement[0] == '<' ? 1 : 0];
            
            if (RecursiveMoveIfPossible(map, roboX, roboY, direction))
            {
                roboX += direction[1] - direction[3];
                roboY += direction[2] - direction[0];
            }

            //PrintMap(map, movement[0]);
            movement = movement.Substring(1);
        }
        
        int answerValue = 0;
        
        for (int x = 0; x < mapSize; x++)
            for (int y = 0; y < mapSize; y++)
                answerValue += map[y][x] == 'O' ? x + y * 100 : 0;

        return answerValue;
    }

    private void PrintMap(List<string> map, char direction)
    {
        Console.WriteLine($"Movement dir: {direction} \n {string.Join("\n", map)} \n\n\n");
    }

    private bool RecursiveMoveIfPossible(List<string> map, int x, int y, int[] dir)
    {
        if (map[y][x] == '.') return true;
        if (map[y][x] == '#') return false;

        int nextX = x + dir[1] - dir[3];
        int nextY = y + dir[2] - dir[0];
        
        bool movePossible = RecursiveMoveIfPossible(map, nextX, nextY, dir);
        if (!movePossible)
            return false;
        
        map[nextY] = map[nextY].ReplaceAt(nextX, map[y][x]);
        map[y] = map[y].ReplaceAt(x, '.');

        return true;
    }
    
    public override long CalculatePart02(string kindOfInput, string pathPrefix)
    {
        List<string> input = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt");
        int answerValue = 0;
        
        return answerValue;
    }
}
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
        Console.WriteLine($"Movement dir: {direction} \n{string.Join("\n", map)} \n");
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
        List<string> fullInput = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt");

        int mapSize = kindOfInput == "input" ? 50 : 10;
        List<string> map = fullInput.GetRange(0, mapSize);
        string movement = string.Join("", fullInput.GetRange(mapSize+1, fullInput.Count - mapSize - 1));
        
        ExpandInput(map);
        
        int roboX = -1;
        int roboY = -1;
        for (int x = 0; x < map[0].Length; x++) 
            for (int y = 0; y < map.Count; y++)
            {
                if (map[y][x] != '@') continue;
                roboX = x;
                roboY = y;
                x = map[0].Length;
                y = map.Count;
            }
        
        while (movement != string.Empty)
        {
            int[] direction = [movement[0] == '^' ? 1 : 0, movement[0] == '>' ? 1 : 0, movement[0] == 'v' ? 1 : 0, movement[0] == '<' ? 1 : 0];
            
            if (RecursiveCheckMove(map, roboX, roboY, direction))
            {
                RecursivePerformMove(map, roboX, roboY, direction);
                roboX += direction[1] - direction[3];
                roboY += direction[2] - direction[0];
            }

            //PrintMap(map, movement[0]);
            movement = movement.Substring(1);
        }
        
        int answerValue = 0;
        
        for (int x = 0; x < map[0].Length; x++)
        for (int y = 0; y < map.Count; y++)
            answerValue += map[y][x] == '[' ? x + y * 100 : 0;
        
        return answerValue;
    }
    
    private bool RecursiveCheckMove(List<string> map, int x, int y, int[] dir, bool secondPartOfCrate = false)
    {
        if (map[y][x] == '.') return true;
        if (map[y][x] == '#') return false;

        if (dir[1] + dir[3] > 0)
            return RecursiveCheckMove(map, x + dir[1] - dir[3], y, dir);
        
        int nextY = y + dir[2] - dir[0];

        if (map[y][x] == '[' && !secondPartOfCrate)
            return RecursiveCheckMove(map, x, nextY, dir) && RecursiveCheckMove(map, x + 1, y, dir, true);
        if (map[y][x] == ']' && !secondPartOfCrate)
            return RecursiveCheckMove(map, x, nextY, dir) && RecursiveCheckMove(map, x - 1, y, dir, true);
        
        return RecursiveCheckMove(map, x, nextY, dir);
    }

    private void RecursivePerformMove(List<string> map, int x, int y, int[] dir, bool secondPartOfCrate = false)
    {
        if (map[y][x] == '.' || map[y][x] == '#') return;

        int nextX = x + dir[1] - dir[3];
        int nextY = y + dir[2] - dir[0];
        
        if (dir[1] + dir[3] > 0)
        {
            RecursivePerformMove(map, x + dir[1] - dir[3], y, dir);
            map[nextY] = map[nextY].ReplaceAt(nextX, map[y][x]);
            map[y] = map[y].ReplaceAt(x, '.');
            return;
        }

        if (map[y][x] == '[' && !secondPartOfCrate)
        {
            RecursivePerformMove(map, x, y, dir, true);
            RecursivePerformMove(map, x + 1, y, dir, true);
            return;
        }
        
        if (map[y][x] == ']' && !secondPartOfCrate)
        {
            RecursivePerformMove(map, x, y, dir, true);
            RecursivePerformMove(map, x - 1, y, dir, true);
            return;
        }
        
        RecursivePerformMove(map, nextX, nextY, dir);
        
        map[nextY] = map[nextY].ReplaceAt(nextX, map[y][x]);
        map[y] = map[y].ReplaceAt(x, '.');
    }

    private void ExpandInput(List<string> map)
    {
        for (int y = 0; y < map.Count; y++)
        {
            for (int x = 0; x < map[y].Length; x += 2)
            {
                if (map[y][x] == '@') 
                    map[y] = map[y].Insert(x + 1, ".");
                else if (map[y][x] == 'O')
                {
                    map[y] = map[y].ReplaceAt(x, '[');
                    map[y] = map[y].Insert(x + 1, "]");
                }
                else
                {
                    map[y] = map[y].Insert(x + 1, map[y][x].ToString());
                }
            }
        }
    }
}
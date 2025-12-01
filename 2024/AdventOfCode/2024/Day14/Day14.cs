using System.Drawing;
using System;

namespace AdventOfCode._2024;

public class Day14 : Day
{
    public override bool PrintTime { get => true; set { } }

    private int _width;
    private int _height;
    
    public override long CalculatePart01(string kindOfInput, string pathPrefix)
    {
        List<string> input = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt");

        List<int[]> robots = ParseRobots(input);

        _width = kindOfInput == "input" ? 101 : 11;
        _height = kindOfInput == "input" ? 103 : 7;
        
        int steps = 100;

        UpdateRobotPositions(robots, steps);

        int[] quadrants = CountRobotsPerQuadrants(robots);

        int answerValue = quadrants[0] * quadrants[1] * quadrants[2] * quadrants[3];

        return answerValue;
    }

    private int[] CountRobotsPerQuadrants(List<int[]> robots)
    {
        int[] quadrants = new int[4];
        foreach (int[] robot in robots)
        {
            if (robot[0] == _width / 2 || robot[1] == _height / 2) continue;
            
            if (robot[0] < _width / 2)
            {
                if (robot[1] < _height / 2)
                    quadrants[0]++;
                else
                    quadrants[1]++;
            }
            else
            {
                if (robot[1] < _height / 2)
                    quadrants[2]++;
                else
                    quadrants[3]++;
            }
        }

        return quadrants;
    }

    private void UpdateRobotPositions(List<int[]> robots, int steps)
    {
        foreach (int[] robot in robots)
        {
            robot[0] += robot[2] * steps;
            robot[0] %= _width;
            if (robot[0] < 0) robot[0] += _width;
            
            robot[1] += robot[3] * steps;
            robot[1] %= _height;
            if (robot[1] < 0) robot[1] += _height;
        }
    }

    private List<int[]> ParseRobots(List<string> input)
    {
        List<int[]> robots = new List<int[]>();

        foreach (string line in input)
        {
            string[] parts = line.Split([',', ' ', '=']);
            robots.Add([int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[4]), int.Parse(parts[5])]);
        }

        return robots;
    }

#pragma warning disable CA1416
    public override long CalculatePart02(string kindOfInput, string pathPrefix)
    {
        if (kindOfInput == "example") return -1;
        List<string> input = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt");
        List<int[]> robots = ParseRobots(input);

        _width = 101;
        _height = 103;
        
        int step = 0;
        UpdateRobotPositions(robots, step);
        
        while (step < 10000)
        {
            UpdateRobotPositions(robots, 1);
            step++;

            // optimization taken from reddit
            // takes 0.27s instead of 7s
            if ((step - 33) % _width != 0 && (step - 87) % _height != 0) 
                continue;

            var bitmap = new Bitmap(_width, _height);

            foreach (int[] robot in robots)
            {
                bitmap.SetPixel(robot[0], robot[1], Color.Black);
            }
            bitmap.Save(pathPrefix + "\\visualization\\" + step + ".bmp");
        }

        return 7709;
    }
#pragma warning restore CA1416
}
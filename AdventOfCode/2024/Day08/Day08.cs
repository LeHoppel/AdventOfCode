namespace AdventOfCode._2024;

public class Day08 : Day
{
    public override bool PrintTime { get => false; set { } }
    
    public override int CalculatePart01(string kindOfInput, string pathPrefix)
    {
        List<string> input = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt");
        input.Reverse();

        Dictionary<char, List<(int, int)>> antennaPos = SampleAntennaPositions(input);

        foreach (var kvp in antennaPos)
        {
            List<(int, int)> antennaPositions = kvp.Value;

            for (int i = 0; i < antennaPositions.Count; i++)
            {
                for (int j = 0; j < antennaPositions.Count; j++)
                {
                    if (j == i) continue;
                    (int, int) distance = SubTupels(antennaPositions[j], antennaPositions[i]);
                    (int, int) antinode = AddTupels(antennaPositions[i], MulTupel(distance, 2));

                    if (antinode.Item1 < 0 || antinode.Item1 >= input[0].Length ||
                        antinode.Item2 < 0 || antinode.Item2 >= input.Count) continue;

                    ReplaceInputField(input, "#", antinode);
                }
            }
        }

        int answerValue = 0;

        foreach (string line in input)
            answerValue += line.Count(x => x == '#');

        return answerValue;
    }

    public override int CalculatePart02(string kindOfInput, string pathPrefix)
    {
        List<string> input = ReadInput(pathPrefix + "\\" + kindOfInput + ".txt");
        input.Reverse();

        Dictionary<char, List<(int, int)>> antennaPos = SampleAntennaPositions(input);

        foreach (var kvp in antennaPos)
        {
            List<(int, int)> antennaPositions = kvp.Value;

            for (int i = 0; i < antennaPositions.Count; i++)
            {
                for (int j = 0; j < antennaPositions.Count; j++)
                {
                    if (j == i) continue;

                    (int, int) distance = SubTupels(antennaPositions[j], antennaPositions[i]);

                    int factor = 1;
                    (int, int) antinode = AddTupels(antennaPositions[i], MulTupel(distance, factor));
                    while (antinode.Item1 >= 0 && antinode.Item1 < input[0].Length &&
                           antinode.Item2 >= 0 && antinode.Item2 < input.Count)
                    {
                        ReplaceInputField(input, "#", antinode);

                        antinode = AddTupels(antennaPositions[i], MulTupel(distance, factor));

                        factor++;
                    }
                }
            }
        }

        int answerValue = 0;

        foreach (string line in input)
            answerValue += line.Count(x => x == '#');

        return answerValue;
    }

    private static Dictionary<char, List<(int, int)>> SampleAntennaPositions(List<string> input)
    {
        Dictionary<char, List<(int, int)>> antennaPos = new();

        for (int y = 0; y < input.Count; y++)
        {
            for (int x = 0; x < input[0].Length; x++)
            {
                char currentTile = input[y][x];

                if (currentTile == '.') continue;

                if (antennaPos.ContainsKey(currentTile))
                    antennaPos[currentTile].Add((x, y));
                else
                {
                    List<(int, int)> positions = [(x, y)];
                    antennaPos.Add(currentTile, positions);
                }
            }
        }

        return antennaPos;
    }

    private (int, int) SubTupels((int, int) a, (int, int) b) => (a.Item1 - b.Item1, a.Item2 - b.Item2);
    private (int, int) AddTupels((int, int) a, (int, int) b) => (a.Item1 + b.Item1, a.Item2 + b.Item2);

    private (int, int) MulTupel((int, int) a, int b) => (a.Item1 * b, a.Item2 * b);

    private void ReplaceInputField(List<string> input, string character, (int, int) position)
    {
        input[position.Item2] = input[position.Item2].Remove(position.Item1, 1);
        input[position.Item2] = input[position.Item2].Insert(position.Item1, character);
    }
}
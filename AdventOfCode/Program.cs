using AdventOfCode._2024;

List<Day> days =
[
    new Day01(),
    new Day02(),
    new Day03(),
    new Day04(),
    new Day05(),
    new Day06(),
    new Day07(),
    new Day08()
];

int startDay = 6;
int endDay = 6;

for (int i = startDay-1; i < endDay; i++)
{
    // Work:
    //string pathPrefix = "D:\\Lennart\\Git\\AdventOfCode\\AdventOfCode\\2024\\Day" + (i+1 < 10 ? "0" + (i+1) : i+1);
    // Home:
    string pathPrefix = "C:\\Users\\Lennart\\RiderProjects\\AdventOfCode\\AdventOfCode\\2024\\Day" + (i+1 < 10 ? "0" + (i+1) : i+1);
    
    Console.WriteLine($"Day {i+1} of 2024 Part01: example={days[i].CalculatePart01("example", pathPrefix)}, input={days[i].CalculatePart01("input", pathPrefix)}; " +
                      $"Part02: example={days[i].CalculatePart02("example", pathPrefix)}, input={days[i].CalculatePart02("input", pathPrefix)}");
}
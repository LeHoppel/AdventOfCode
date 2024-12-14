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
    new Day08(),
    new Day09(),
    new Day10(),
    new Day11(),
    new Day12(),
    new Day13(),
    new Day14(),
    new Day15()
];

int startDay = 14;
int endDay = 14;

for (int i = startDay-1; i < endDay; i++)
{
    // Work: "D:\\Lennart\\Git\\AdventOfCode\\AdventOfCode\\2024\\Day" + (i+1 < 10 ? "0" + (i+1) : i+1);
    // Home: "C:\\Users\\Lennart\\RiderProjects\\AdventOfCode\\AdventOfCode\\2024\\Day" + (i+1 < 10 ? "0" + (i+1) : i+1);
    string pathPrefix = "C:\\Users\\Lennart\\RiderProjects\\AdventOfCode\\AdventOfCode\\2024\\Day" + (i+1 < 10 ? "0" + (i+1) : i+1);
    
    TimeSpan startTime = DateTime.Now.TimeOfDay;

    Console.WriteLine($"Day {i+1} of 2024 Part01: example={days[i].CalculatePart01("example", pathPrefix)}, input={days[i].CalculatePart01("input", pathPrefix)}; " +
                      $"Part02: example={days[i].CalculatePart02("example", pathPrefix)}, input={days[i].CalculatePart02("input", pathPrefix)}");
    
    if(days[i].PrintTime) Console.WriteLine($"Day {i+1} elapsed time: {double.Round((DateTime.Now.TimeOfDay - startTime).TotalSeconds, 2)}s");
}
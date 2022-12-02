namespace AdventOfCode.Solutions.Year2022.Day02;

class Solution : SolutionBase
{
    private static readonly Dictionary<string, int> pointsPartOne = new()
        {
            {"A X", 4 },
            {"A Y", 8 },
            {"A Z", 3 },
            {"B X", 1 },
            {"B Y", 5 },
            {"B Z", 9 },
            {"C X", 7 },
            {"C Y", 2 },
            {"C Z", 6 },
        };

    private static readonly Dictionary<string, int> pointsPartTwo = new()
        {
            {"A X", 3 },
            {"A Y", 4 },
            {"A Z", 8 },
            {"B X", 1 },
            {"B Y", 5 },
            {"B Z", 9 },
            {"C X", 2 },
            {"C Y", 6 },
            {"C Z", 7 },
        };

    public Solution() : base(02, 2022, "Rock Paper Scissors") { }

    protected override string SolvePartOne()
    {
        return Input.SplitByNewline().Aggregate(0, (current, game) => current + pointsPartOne[game]).ToString();
    }

    protected override string SolvePartTwo()
    {
        return Input.SplitByNewline().Aggregate(0, (current, game) => current + pointsPartTwo[game]).ToString();
    }
}

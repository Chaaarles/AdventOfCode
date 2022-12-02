namespace AdventOfCode.Solutions.Year2022.Day01;

class Solution : SolutionBase
{
    private readonly List<int> elves;

    public Solution() : base(01, 2022, "Calorie Counting")
    {
        elves = Input.SplitByParagraph().Select(elf => elf.SplitByNewline().Sum(calories => int.Parse(calories))).OrderDescending().ToList();
    }

    protected override string SolvePartOne()
    {
        return elves[0].ToString();
    }

    protected override string SolvePartTwo()
    {
        return (elves[0] + elves[1] + elves[2]).ToString();
    }
}

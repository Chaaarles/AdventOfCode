namespace AdventOfCode.Solutions.Year2022.Day04;

class Solution : SolutionBase
{
    public Solution() : base(04, 2022, "") { }

    protected override string SolvePartOne()
    {
        int containCount = 0;
        foreach (string pair in Input.SplitByNewline())
        {
            var assignments = ParseAssignments(pair);
            if ((assignments.FirstStart <= assignments.SecondStart
                && assignments.FirstEnd >= assignments.SecondEnd)
                || (assignments.SecondStart <= assignments.FirstStart
                && assignments.SecondEnd >= assignments.FirstEnd))
            {
                containCount++;
            }
        }

        return containCount.ToString();
    }

    protected override string SolvePartTwo()
    {
        int overlapCount = 0;
        foreach (string pair in Input.SplitByNewline())
        {
            var assignments = ParseAssignments(pair);
            if ((assignments.FirstStart <= assignments.SecondStart
                && assignments.FirstEnd >= assignments.SecondStart)
                || (assignments.SecondStart <= assignments.FirstStart
                && assignments.SecondEnd >= assignments.FirstStart))
            {
                overlapCount++;
            }
        }

        return overlapCount.ToString();
    }

    private (int FirstStart, int FirstEnd, int SecondStart, int SecondEnd) ParseAssignments(string input)
    {
        string[] splitPair = input.Split(',');
        int[] firstAssignment = splitPair[0].ToIntArray("-");
        int[] secondAssignment = splitPair[1].ToIntArray("-");
        return (firstAssignment[0], firstAssignment[1], secondAssignment[0], secondAssignment[1]);
    }
}

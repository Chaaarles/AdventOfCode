namespace AdventOfCode.Solutions.Year2022.Day04;

class Solution : SolutionBase
{
    public Solution() : base(04, 2022, "Camp Cleanup") { }

    protected override string SolvePartOne()
    {
        int containCount = 0;
        foreach (string pair in Input.SplitByNewline())
        {
            (int FirstStart, int FirstEnd, int SecondStart, int SecondEnd) = ParseAssignments(pair);
            if ((FirstStart <= SecondStart
                && FirstEnd >= SecondEnd)
                || (SecondStart <= FirstStart
                && SecondEnd >= FirstEnd))
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
            (int FirstStart, int FirstEnd, int SecondStart, int SecondEnd) = ParseAssignments(pair);
            if ((FirstStart <= SecondStart
                && FirstEnd >= SecondStart)
                || (SecondStart <= FirstStart
                && SecondEnd >= FirstStart))
            {
                overlapCount++;
            }
        }

        return overlapCount.ToString();
    }

    private static (int FirstStart, int FirstEnd, int SecondStart, int SecondEnd) ParseAssignments(string input)
    {
        string[] splitPair = input.Split(',');
        int[] firstAssignment = splitPair[0].ToIntArray("-");
        int[] secondAssignment = splitPair[1].ToIntArray("-");
        return (firstAssignment[0], firstAssignment[1], secondAssignment[0], secondAssignment[1]);
    }
}

namespace AdventOfCode.Solutions.Year2022.Day06;

class Solution : SolutionBase
{
    public Solution() : base(06, 2022, "") { }

    protected override string SolvePartOne()
    {
        return FindMarkerEnd(4);
    }

    protected override string SolvePartTwo()
    {
        return FindMarkerEnd(14);
    }

    private string FindMarkerEnd(int length)
    {
        int start = 0;
        int end = 0;
        var buffer = new HashSet<char>();

        while (buffer.Count < length)
        {
            if (!buffer.Contains(Input[end]))
            {
                buffer.Add(Input[end++]);
            }
            else
            {
                buffer.Remove(Input[start++]);
            }
        }

        return end.ToString();
    }
}

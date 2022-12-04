namespace AdventOfCode.Solutions.Year2022.Day03;

class Solution : SolutionBase
{
    public Solution() : base(03, 2022, "") { }

    protected override string SolvePartOne()
    {
        int prioritySum = 0;
        foreach (string rucksack in Input.SplitByNewline())
        {
            var compartment1 = new HashSet<char>();
            for (int i = 0; i < rucksack.Length / 2; i++)
            {
                compartment1.Add(rucksack[i]);
            }

            for (int i = rucksack.Length / 2; i < rucksack.Length; i++)
            {
                if (compartment1.Contains(rucksack[i]))
                {
                    prioritySum += GetPriority(rucksack[i]);
                    break;
                }
            }
        }
        return prioritySum.ToString();
    }

    protected override string SolvePartTwo()
    {
        int prioritySum = 0;
        string[] rucksacks = Input.SplitByNewline();
        for (int i = 0; i < rucksacks.Length; i += 3)
        {
            var rucksack1 = new HashSet<char>(rucksacks[i]);
            var rucksack2 = new HashSet<char>(rucksacks[i + 1]);
            var rucksack3 = new HashSet<char>(rucksacks[i + 2]);

            char item = rucksack1.Intersect(rucksack2).Intersect(rucksack3).First();
            prioritySum += GetPriority(item);
        }
        return prioritySum.ToString();
    }

    private int GetPriority(char item)
    {
        if (item <= 90)
        {
            return item - 64 + 26;
        }
        else
        {
            return item - 96;
        }
    }
}

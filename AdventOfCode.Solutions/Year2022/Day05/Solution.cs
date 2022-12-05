namespace AdventOfCode.Solutions.Year2022.Day05;

class Solution : SolutionBase
{
    private readonly string[] InitialState;
    private readonly string[] Instructions;

    public Solution() : base(05, 2022, "Supply Stacks")
    {
        string[] input = Input.SplitByParagraph();
        InitialState = input[0].SplitByNewline();
        Instructions = input[1].SplitByNewline();
    }

    protected override string SolvePartOne()
    {
        List<List<char>> crateStacks = BuildCrateStacks();
        foreach (string instruction in Instructions)
        {
            (int Move, int From, int To) = ReadInstruction(instruction);
            for (int i = 0; i < Move; i++)
            {
                crateStacks[To].Add(crateStacks[From].Last());
                crateStacks[From].RemoveAt(crateStacks[From].Count - 1);
            }
        }

        return CreateOutput(crateStacks);
    }

    protected override string SolvePartTwo()
    {
        List<List<char>> crateStacks = BuildCrateStacks();
        foreach (string instruction in Instructions)
        {
            (int Move, int From, int To) = ReadInstruction(instruction);
            for (int i = Move; i > 0; i--)
            {
                crateStacks[To].Add(crateStacks[From][^i]);
            }

            crateStacks[From].RemoveRange(crateStacks[From].Count - Move, Move);
        }

        return CreateOutput(crateStacks);
    }

    private List<List<char>> BuildCrateStacks()
    {
        var crateStacks = new List<List<char>>();

        for (int stack = 0; 1 + 4 * stack < InitialState[0].Length; stack++)
        {
            crateStacks.Add(new List<char>());
            for (int i = InitialState.Length - 2; i >= 0; i--)
            {
                char crate = InitialState[i][1 + 4 * stack];
                if (crate == ' ')
                {
                    break;
                }
                crateStacks[stack].Add(crate);
            }
        }

        return crateStacks;
    }

    private static (int Move, int From, int To) ReadInstruction(string instruction)
    {
        string[] splitInstruction = instruction.Split(' ');
        return (int.Parse(splitInstruction[1]), int.Parse(splitInstruction[3]) - 1, int.Parse(splitInstruction[5]) - 1);
    }

    private static string CreateOutput(List<List<char>> crateStacks)
    {
        StringBuilder sb = new StringBuilder();
        foreach (List<char> stack in crateStacks)
        {
            sb.Append(stack.Last());
        }

        return sb.ToString();
    }
}

namespace AdventOfCode.Solutions.Year2022.Day10;

using System.Reflection.Emit;

class Solution : SolutionBase
{
    public Solution() : base(10, 2022, "Cathode-Ray Tube") { }

    protected override string SolvePartOne()
    {
        string[] ops = Input.SplitByNewline();
        int signalStrength = 0;
        int pointer = 0;
        int regX = 1;
        bool adding = false;
        string[] op; ;
        for (int cycle = 1; cycle <= 220; cycle++)
        {
            if ((cycle % 40) == 20)
            {
                signalStrength += cycle * regX;
            }
            op = ops[pointer].Split(" ");
            if (op[0] == "noop")
            {
                adding = false;
                pointer++;
            }
            else if (op[0] == "addx")
            {
                if (!adding)
                {
                    adding = true;
                    continue;
                }
                else
                {
                    adding = false;
                    regX += int.Parse(op[1]);
                    pointer++;
                }
            }
        }

        return signalStrength.ToString();
    }
   
    protected override string SolvePartTwo()
    {
        string[] ops = Input.SplitByNewline();
        var render = new StringBuilder();
        int pointer = 0;
        int regX = 1;
        bool adding = false;
        string[] op;

        for (int cycle = 0; cycle < 240; cycle++)
        {
            // Draw
            int pos = cycle % 40;
            if (pos == 0)
            {
                render.AppendLine();
            }
            if (Math.Abs(pos - regX) <= 1)
            {
                render.Append('#');
            }
            else
            {
                render.Append(' ');
            }

            // Compute
            op = ops[pointer].Split(" ");
            if (op[0] == "noop")
            {
                adding = false;
                pointer++;
            }
            else if (op[0] == "addx")
            {
                if (!adding)
                {
                    adding = true;
                    continue;
                }
                else
                {
                    adding = false;
                    regX += int.Parse(op[1]);
                    pointer++;
                }
            }
        }

        return render.ToString();
    }
}

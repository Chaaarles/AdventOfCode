namespace AdventOfCode.Solutions.Year2022.Day09;

using System.Numerics;

class Solution : SolutionBase
{
    private readonly (Vector2 Direction, int Distance)[] Moves;
    public Solution() : base(09, 2022, "Rope Bridge")
    {
        string[] splitInput = Input.SplitByNewline();
        Moves = new (Vector2, int)[splitInput.Length];
        for (int i = 0; i < splitInput.Length; i++)
        {
            string moveDirection = splitInput[i].Split(' ')[0];
            int distance = int.Parse(splitInput[i].Split(' ')[1]);

            Vector2 direction = Vector2.Zero;
            switch (moveDirection)
            {
                case "U":
                    direction.Y = 1;
                    break;
                case "D":
                    direction.Y = -1;
                    break;
                case "R":
                    direction.X = 1;
                    break;
                case "L":
                    direction.X = -1;
                    break;
                default:
                    throw new ArgumentException("Unexpected move direction.");
            }

            Moves[i] = (direction, distance);
        }
    }

    protected override string SolvePartOne()
    {
        return SimulateRope(2).ToString();
    }

    protected override string SolvePartTwo()
    {
        return SimulateRope(10).ToString();
    }

    private int SimulateRope(int length)
    {
        var rope = new Vector2[length];
        for (int i = 0; i < rope.Length; i++)
        {
            rope[i] = Vector2.Zero;
        }

        var visitedByTail = new HashSet<(float, float)>();
        foreach ((Vector2 Direction, int Distance) in Moves)
        {
            for (int i = 0; i < Distance; i++)
            {
                rope[0] += Direction;

                for (int j = 1; j < rope.Length; j++)
                {
                    float distX = rope[j - 1].X - rope[j].X;
                    float distY = rope[j - 1].Y - rope[j].Y;

                    if (distX == 2 || distX == -2)
                    {
                        rope[j].X += distX / Math.Abs(distX);
                        if (distY != 0)
                        {
                            rope[j].Y += distY / Math.Abs(distY);
                        }
                    }
                    else if (distY == 2 || distY == -2)
                    {
                        rope[j].Y += distY / Math.Abs(distY);
                        if (distX != 0)
                        {
                            rope[j].X += distX / Math.Abs(distX);
                        }
                    }
                }

                visitedByTail.Add((rope[^1].X, rope[^1].Y));
            }
        }

        return visitedByTail.Count;
    }
}

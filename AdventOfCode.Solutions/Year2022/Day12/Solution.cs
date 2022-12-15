namespace AdventOfCode.Solutions.Year2022.Day12;

using System.Numerics;
using System.Runtime.InteropServices;

class Solution : SolutionBase
{
    private static readonly Vector2[] Directions = new Vector2[]
    {
        new Vector2(1,0),
        new Vector2(-1,0),
        new Vector2(0,1),
        new Vector2(0,-1),
    };

    public Solution() : base(12, 2022, "") { }

    protected override string SolvePartOne()
    {
        return Solver(false);

    }

    protected override string SolvePartTwo()
    {
        return Solver(true);
    }

    private string Solver(bool isPart2)
    {
        string[] inputLines = Input.SplitByNewline();
        char[,] map = new char[inputLines.Length, inputLines[0].Length];

        var queue = new Queue<(Vector2 Pos, int Steps)>();
        var visited = new HashSet<string>();

        for (int i = 0; i < inputLines.Length; i++)
        {
            for (int j = 0; j < inputLines[0].Length; j++)
            {
                map[i, j] = inputLines[i][j];
                if (map[i, j] == 'S')
                {
                    map[i, j] = 'a';
                    queue.Enqueue((new Vector2(i, j), 0));
                    visited.Add(Stringify(new Vector2(i, j)));
                };
                if (map[i, j] == 'a' && isPart2)
                {
                    queue.Enqueue((new Vector2(i, j), 0));
                    visited.Add(Stringify(new Vector2(i, j)));
                }
            }
        }

        while (queue.Count > 0)
        {
            (Vector2 Pos, int Steps) = queue.Dequeue();
            char currentHeight = map[(int)Pos.X, (int)Pos.Y];

            foreach (Vector2 direction in Directions)
            {
                Vector2 candidate = Pos + direction;
                if (visited.Contains(Stringify(candidate)))
                {
                    continue;
                }

                char candidateHeight;
                try
                {
                    candidateHeight = map[(int)candidate.X, (int)candidate.Y];
                }
                catch
                {
                    continue;
                }
                if (candidateHeight == 'E')
                {
                    return (Steps + 1).ToString();
                }
                if (candidateHeight - currentHeight < 2)
                {
                    visited.Add(Stringify(candidate));
                    queue.Enqueue((candidate, Steps + 1));
                }
            }
        }
        return "FAILED";
    }

    private static string Stringify(Vector2 vector)
    {
        return $"x{vector.X}y{vector.Y}";
    }
}

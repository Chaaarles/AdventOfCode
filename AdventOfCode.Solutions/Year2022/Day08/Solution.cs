namespace AdventOfCode.Solutions.Year2022.Day08;

class Solution : SolutionBase
{
    public Solution() : base(08, 2022, "Treetop Tree House")
    {

    }

    protected override string SolvePartOne()
    {
        string[] inputRows = Input.SplitByNewline();
        int rows = inputRows.Length;
        int cols = inputRows[0].Length;

        int visibleCount = 0;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                int height = int.Parse(inputRows[i][j].ToString());
                bool end = false;
                // look up
                for (int k = i - 1; k >= -1; k--)
                {
                    if (k == -1)
                    {
                        visibleCount++;
                        end = true;
                        break;
                    }
                    if (int.Parse(inputRows[k][j].ToString()) >= height)
                    {
                        break;
                    }
                }
                if (end)
                {
                    continue;
                }

                // look down
                for (int k = i + 1; k <= rows; k++)
                {
                    if (k == rows)
                    {
                        visibleCount++;
                        end = true;
                        break;
                    }
                    if (int.Parse(inputRows[k][j].ToString()) >= height)
                    {
                        break;
                    }
                }
                if (end)
                {
                    continue;
                }

                // look left
                for (int k = j - 1; k >= -1; k--)
                {
                    if (k == -1)
                    {
                        visibleCount++;
                        end = true;
                        break;
                    }
                    if (int.Parse(inputRows[i][k].ToString()) >= height)
                    {
                        break;
                    }
                }
                if (end)
                {
                    continue;
                }

                // look right
                for (int k = j + 1; k <= cols; k++)
                {
                    if (k == cols)
                    {
                        visibleCount++;
                        break;
                    }
                    if (int.Parse(inputRows[i][k].ToString()) >= height)
                    {
                        break;
                    }
                }
            }
        }

        return visibleCount.ToString();
    }

    protected override string SolvePartTwo()
    {
        string[] inputRows = Input.SplitByNewline();
        int rows = inputRows.Length;
        int cols = inputRows[0].Length;

        int bestView = 0;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                int view = 1;
                int height = int.Parse(inputRows[i][j].ToString());
                // look up
                for (int k = i - 1; k >= -1; k--)
                {
                    if (k == -1 || int.Parse(inputRows[k][j].ToString()) >= height)
                    {
                        if (k == -1)
                        {
                            k++;
                        }
                        view *= i - k;
                        break;
                    }
                }

                // look down
                for (int k = i + 1; k <= rows; k++)
                {
                    if (k == rows || int.Parse(inputRows[k][j].ToString()) >= height)
                    {
                        if (k == rows)
                        {
                            k--;
                        }
                        view *= k - i;
                        break;
                    }
                }

                // look left
                for (int k = j - 1; k >= -1; k--)
                {
                    if (k == -1 || int.Parse(inputRows[i][k].ToString()) >= height)
                    {
                        if (k == -1)
                        {
                            k++;
                        }
                        view *= j - k;
                        break;
                    }
                }

                // look right
                for (int k = j + 1; k <= cols; k++)
                {
                    if (k == cols || int.Parse(inputRows[i][k].ToString()) >= height)
                    {
                        if (k == cols)
                        {
                            k--;
                        }
                        view *= k - j;
                        break;
                    }
                }

                bestView = Math.Max(bestView, view);
            }
        }

        return bestView.ToString();
    }
}

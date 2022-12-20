namespace AdventOfCode.Solutions.Year2022.Day13;
using System.Text.Json;
using System.Text.Json.Nodes;

class Solution : SolutionBase
{
    public Solution() : base(13, 2022, "Distress Signal") { }

    protected override string SolvePartOne()
    {
        int i = 0;
        int sum = 0;
        foreach (string pair in Input.SplitByParagraph())
        {
            i++;
            string[] splitPair = pair.SplitByNewline();
            JsonArray left = JsonSerializer.Deserialize<JsonArray>(splitPair[0]) ?? throw new Exception("Array was null");
            JsonArray right = JsonSerializer.Deserialize<JsonArray>(splitPair[1]) ?? throw new Exception("Array was null");

            if (CompareArray(left, right) == true)
            {
                sum += i;
            }
        }
        return sum.ToString();
    }

    protected override string SolvePartTwo()
    {
        List<string> lines = Input.SplitByParagraph().SelectMany(pair => pair.SplitByNewline()).ToList();
        lines.Add("[[2]]");
        lines.Add("[[6]]");
        lines.Sort((left, right) => CompareArray(
            JsonSerializer.Deserialize<JsonArray>(left) ?? throw new Exception("Array was null"),
            JsonSerializer.Deserialize<JsonArray>(right) ?? throw new Exception("Array was null")) == true ? -1 : 1);

        int firstDivider = lines.FindIndex(s => s == "[[2]]") + 1;
        int secondDivider = lines.FindIndex(s => s == "[[6]]") + 1;
        return (firstDivider * secondDivider).ToString();
    }

    private bool? CompareArray(JsonArray left, JsonArray right)
    {
        for (int i = 0; i < left.Count; i++)
        {
            if (i == right.Count)
            {
                return false;
            }

            if (left[i] is JsonValue leftValue && right[i] is JsonValue rightValue)
            {
                int leftInt = leftValue.GetValue<int>();
                int rightInt = rightValue.GetValue<int>();
                if (leftInt < rightInt)
                {
                    return true;
                }
                else if (rightInt < leftInt)
                {
                    return false;
                }
                continue;
            }

            JsonArray leftArray;
            JsonArray rightArray;

            if (left[i] is JsonArray)
            {
                leftArray = left[i]?.AsArray() ?? throw new Exception("Array was null");
            }
            else
            {
                leftArray = new JsonArray(left[i]?.AsValue().GetValue<int>());
            }

            if (right[i] is JsonArray)
            {
                rightArray = right[i]?.AsArray() ?? throw new Exception("Array was null");
            }
            else
            {
                rightArray = new JsonArray(right[i]?.AsValue().GetValue<int>());
            }


            // Right is array
            bool? comp = CompareArray(leftArray, rightArray);
            /*Console.WriteLine(comp);*/
            if (comp is not null)
            {
                return comp;
            }
            continue;
        }

        if (left.Count < right.Count)
        {
            return true;
        }
        return null;
    }
}

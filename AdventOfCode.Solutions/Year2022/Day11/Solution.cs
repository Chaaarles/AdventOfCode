namespace AdventOfCode.Solutions.Year2022.Day11;

class Solution : SolutionBase
{
    public Solution() : base(11, 2022, "Monkey in the Middle") { }

    protected override string SolvePartOne()
    {
        List<Monkey> monkeys = GetMonkeys();

        for (int i = 0; i < 20; i++)
        {
            foreach (Monkey monkey in monkeys) { monkey.TakeTurn(3, 0); };
        }

        return monkeys.OrderByDescending(monkey => monkey.ItemsInspected).Take(2).Aggregate((ulong)1, (acc, monkey) => acc * monkey.ItemsInspected).ToString();
    }

    protected override string SolvePartTwo()
    {
        List<Monkey> monkeys = GetMonkeys();

        int mod = monkeys.Select(m => m.Divisor).Aggregate((a, m) => a * m);

        for (int i = 0; i < 10000; i++)
        {
            foreach (Monkey monkey in monkeys) { monkey.TakeTurn(1, mod); };
        }

        return monkeys.OrderByDescending(monkey => monkey.ItemsInspected).Take(2).Aggregate((ulong)1, (acc, monkey) => acc * monkey.ItemsInspected).ToString();
    }

    List<Monkey> GetMonkeys()
    {
        string[] inputMonkeys = Input.SplitByParagraph();
        var monkeys = new List<Monkey>();

        for (int i = 0; i < inputMonkeys.Length; i++)
        {
            monkeys.Add(new Monkey());
        }

        foreach (string input in inputMonkeys)
        {
            string[] splitInput = input.SplitByNewline();
            Monkey currentMonkey = monkeys[ParseMonkeyId(splitInput[0])];
            List<ulong> startingItems = ParseItems(splitInput[1]);
            Func<ulong, ulong> operation = ParseOperation(splitInput[2]);
            int divisor = ParseLastInt(splitInput[3]);
            Monkey trueMonkey = monkeys[ParseLastInt(splitInput[4])];
            Monkey falseMonkey = monkeys[ParseLastInt(splitInput[5])];


            currentMonkey.Items = startingItems;
            currentMonkey.Operation = operation;
            currentMonkey.Divisor = divisor;
            currentMonkey.TrueMonkey = trueMonkey;
            currentMonkey.FalseMonkey = falseMonkey;
        }

        return monkeys;
    }

    static int ParseMonkeyId(string monkeyString) => int.Parse(monkeyString.Split(' ')[1].Trim(':'));

    static List<ulong> ParseItems(string itemString) => itemString.Split(':')[1].Trim().Split(',').Select(x => ulong.Parse(x.Trim())).ToList();

    static Func<ulong, ulong> ParseOperation(string operationString)
    {
        string[] splitOperation = operationString.Trim().Split(' ');
        if (splitOperation[4] == "+")
        {
            if (splitOperation[5] == "old")
            {
                return old => old + old;
            }
            else
            {
                return old => old + ulong.Parse(splitOperation[5]);
            }
        }
        else if (splitOperation[4] == "*")
        {
            if (splitOperation[5] == "old")
            {
                return old => old * old;
            }
            else
            {
                return old => old * ulong.Parse(splitOperation[5]);
            }
        }
        throw new Exception();
    }

    static int ParseLastInt(string inputLine) => int.Parse(inputLine.Split(' ')[^1]);
}

class Monkey
{
    public List<ulong> Items { get; set; } = null!;
    public Func<ulong, ulong> Operation { get; set; } = null!;
    public int Divisor { get; set; } = 0;
    public Monkey TrueMonkey { get; set; } = null!;
    public Monkey FalseMonkey { get; set; } = null!;
    public ulong ItemsInspected { get; private set; } = 0;

    public void TakeTurn(int div, int mod)
    {
        foreach (ulong item in Items)
        {
            ItemsInspected++;

            ulong newWorry = Operation(item) / (ulong)div;
            if (mod is not 0)
            {
                newWorry %= (ulong)mod;
            }

            if (newWorry % (ulong)Divisor == 0)
            {
                TrueMonkey.Items.Add(newWorry);
            }
            else
            {
                FalseMonkey.Items.Add(newWorry);
            }
        }

        Items.Clear();
    }
}

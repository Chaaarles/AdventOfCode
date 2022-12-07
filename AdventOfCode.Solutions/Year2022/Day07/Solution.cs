namespace AdventOfCode.Solutions.Year2022.Day07;
class Solution : SolutionBase
{
    private readonly Directory root;

    public Solution() : base(07, 2022, "")
    {
        root = new Directory(null);
        Directory currentDirectory = root;

        foreach (string line in Input.SplitByNewline())
        {
            (CommandType Command, string Name, int FileSize) = ParseLine(line);
            switch (Command)
            {
                case CommandType.cd:
                    if (Name == "..")
                    {
                        currentDirectory = currentDirectory.Parent!;
                    }
                    else if (Name == "/")
                    {
                        currentDirectory = root;
                    }
                    else
                    {
                        currentDirectory = currentDirectory.SubDirectories[Name];
                    }
                    break;
                case CommandType.ls:
                    break;
                case CommandType.dir:
                    var newDirectory = new Directory(currentDirectory);
                    currentDirectory.SubDirectories[Name] = newDirectory;
                    currentDirectory.Items.Add(newDirectory);
                    break;
                case CommandType.file:
                    currentDirectory.Items.Add(new File(FileSize));
                    break;
            }
        }
    }

    protected override string SolvePartOne()
    {
        return Calculate(root).ToString();
    }

    protected override string SolvePartTwo()
    {
        int fileSystem = 70000000;
        int target = 30000000;
        int currentUnused = fileSystem - root.Size;
        int toDelete = target - currentUnused;
        int candidate = root.Size;

        return FindDeleteCandidate(root, candidate, toDelete).ToString();
    }

    private static int FindDeleteCandidate(Directory root, int currentCandidate, int toDelete)
    {
        if (root.Size < toDelete)
        {
            return currentCandidate;
        }

        if (root.Size >= toDelete && root.Size < currentCandidate)
        {
            currentCandidate = root.Size;
        }

        foreach (Directory directory in root.SubDirectories.Values)
        {
            currentCandidate = FindDeleteCandidate(directory, currentCandidate, toDelete);
        }

        return currentCandidate;
    }

    private static int Calculate(Directory root)
    {
        int sum = 0;
        if (root.Size < 100000)
        {
            sum += root.Size;
        }

        foreach (Directory directory in root.SubDirectories.Values)
        {
            sum += Calculate(directory);
        }

        return sum;
    }

    private enum CommandType
    {
        cd,
        ls,
        dir,
        file,
    }

    private (CommandType Command, string Name, int FileSize) ParseLine(string line)
    {
        string[] splitLine = line.Split(' ');
        if (splitLine[0] == "dir")
        {
            return (CommandType.dir, splitLine[1], 0);
        }

        if (int.TryParse(splitLine[0], out int fileSize))
        {
            return (CommandType.file, splitLine[1], fileSize);
        }
        
        if (splitLine[1] == "ls")
        {
            return (CommandType.ls, "", 0);
        }
        
        return (CommandType.cd, splitLine[2], 0);
    }
}

abstract class Item
{
    public abstract int Size { get; }
}

class Directory : Item
{
    public Dictionary<string, Directory> SubDirectories { get; } = new();
    public List<Item> Items { get; } = new();
    public Directory? Parent { get; }

    public Directory(Directory? parent)
    {
        Parent = parent;
    }

    public override int Size => Items.Aggregate(0, (sum, item) => sum + item.Size);
}

class File : Item
{
    private readonly int size;

    public File(int size)
    {
        this.size = size;
    }

    public override int Size => size;
}
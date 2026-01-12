using Xunit.Abstractions;

namespace AOC_2025.Day_10;

public class FactorySolution(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void Demo_Input_Should()
    {
        var input = FileHelper.ReadFileAsList(10, "puzzle.txt");
        var factories = Factory.FromList(input);

        int total = 0;
        foreach (var factory in factories)
        {
            total += factory.Solve(testOutputHelper.WriteLine);
        }
        
        testOutputHelper.WriteLine(total.ToString());
    }
}

public class Factory
{
    public Indicator Indicator { get; set; }
    public List<Switch> Switches { get; set; } = new();
    public Joltage Joltage { get; set; }

    public int Solve(Action<string> output)
    {
        int target = Indicator.Target;
        List<int> seen = new() {0};
        List<int> next = new();
        int depth = 1;
        while (depth < Switches.Count)
        {
            foreach (var switcher in Switches)
            {
                foreach (var seenValue in seen)
                {
                    var mod = seenValue ^ switcher.Modify;
                    if (mod == target) return depth;
                    
                    next.Add(mod);
                }
            }
            seen = next;
            next = new();
            depth++;
        }
        return depth;

    }
    
    public Factory(string input)
    {
        var parts = input.Split(' ');
        foreach (var part in parts)
        {
            // if starts with '[' is Indicator
            // if starts with '(' is Switch
            // if starts with '{' is Joltage
            if (part.StartsWith('[')) Indicator = new Indicator(part);
            if (part.StartsWith('(')) Switches.Add(new Switch(part));
            if (part.StartsWith('{')) Joltage = new Joltage(part);
        }
    }
    
    public static List<Factory> FromList(List<string> input)
    {
        return input.Select(i => new Factory(i)).ToList();
    }
    
}

public class Switch
{
    public int Modify { get; }
    
    public Switch(string value)
    {
        var substr = value.Substring(1, value.Length - 2);
        foreach (var c in substr.Split(','))
        {
            int num = int.Parse(c);
            Modify += (1 << num);
        }
    }
    
    public override string ToString() => $"Modify: {Modify}";
}

public class Indicator
{
    public int Target { get; init; }
    
    public Indicator(string part)
    {
        int sum = 0;
        var substr = part.Substring(1, part.Length - 2).Reverse();
        foreach (var c in substr)
        {
            sum *= 2;
            if (c == '#')
            {
                sum++;
            }
        }
        Target = sum;
    }

    public override string ToString() 
        => $"Target: {Target}";
}

public class Joltage
{
    public Joltage(string part)
    {
        
    }
    
}
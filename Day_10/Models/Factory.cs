namespace AOC_2025.Day_10.Models;

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

    public int Solve2(Action<string> testOutputHelper)
    {
        ReduceSwitches();
        var unique = FindUniqueSwitches(Joltage.Targets.Count);
        
        Status start = new(Joltage.Targets);
        foreach (var (index, @switch) in unique)
        {
            int scale = start.Positions[index];
            start.Apply(@switch, scale);
        }
        
        Output(Switches, testOutputHelper);
        while(start.Distance() > 0)
        {
            testOutputHelper.Invoke(start.ToString());
            var priorities = start.Priorities();
            
            var next = FindNextSwitch(priorities);
            testOutputHelper.Invoke(next.ToString());
            
            int diff = priorities.Count == 1 ? start.Positions[priorities[0]]
                : Math.Max(1, start.Positions[priorities[0]] - start.Positions[priorities[1]]);
            
            start.Apply(next, diff);
            testOutputHelper.Invoke($"For: {diff}");
            testOutputHelper.Invoke($"Clicks: {start.Depth.ToString()}");
        }

        return start.Depth;
    }

    private void Output(List<Switch> values, Action<string> output)
    {
        foreach (var value in values) output(value.ToString());
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
    
    private void ReduceSwitches()
    {
        var sortedSwitches = Switches.OrderByDescending(s => s.Operators.Count).ToList();
        List<Switch> reducedSwitches = new();
        foreach (var current in sortedSwitches)
        {
            if (!sortedSwitches.Any(s => s.Contains(current)))
            {
                reducedSwitches.Add(current);
            }
        }
        
        Switches = reducedSwitches;
    }

    private List<(int, Switch)> FindUniqueSwitches(int maxCount = 2)
    {
        List< (int, Switch)> uniqueSwitches = new();
        for (int i = 0; i < maxCount; i++)
        {
            var selected = Switches.Where(s => s.Operators.Contains(i)).Take(2).ToList();
            if(selected.Count == 1) 
                uniqueSwitches.Add((i, selected[0]));
        }
        
        return uniqueSwitches;
    }

    private Switch FindNextSwitch(List<int> priorities)
    {
        List<Switch> filtered = Switches.Where(s => s.Operators.Contains(priorities[0])).ToList();

        for (int i = 1; i < priorities.Count; i++)
        {
            var nextFiltered = filtered.Where(s => s.Operators.Contains(priorities[i])).ToList();
            if (nextFiltered.Count > 0)
            {
                filtered = nextFiltered;
            }
        }
        
        return filtered.First();
    }
}
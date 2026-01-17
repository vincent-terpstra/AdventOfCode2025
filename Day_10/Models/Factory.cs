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
        List<Fact> facts = Joltage.Targets.ConvertAll(t => new Fact(t));
        
        foreach (var switcher in Switches)
        {
            foreach (var oper in switcher.Operators)
            {
                facts[oper].Operators.Add(switcher);
            }
        }

        bool addedFact = false;
        do
        {


            facts = SortFacts(facts);
            addedFact = FindAdditionalFacts(facts, s => { });
            
            // var solve = facts.FirstOrDefault(f => f.Operators.Count == Switches.Count);
            //
            // if (solve != null)
            // {
            //     return solve.Target;
            // }
            
        } while (addedFact);

        testOutputHelper.Invoke(Joltage.ToString());

        var start = new Status(Joltage.Targets);
        foreach(var switcher in Switches.Where(s => s.Value != null))
        {
            testOutputHelper.Invoke($"{start} - ({switcher.OperatorString()}) - {switcher.Value}");
            start = start.Next(switcher, switcher.Value ?? 0);
        }
        testOutputHelper.Invoke($"{start} - {start.Depth}");
        
        facts = FilterPartialFacts(facts);
        facts = PrioritizeFacts(facts);
        // foreach (var f in facts)
        // {
        //     testOutputHelper.Invoke(f.ToString());
        // }
        //
        
        return 0;
    }
    
    private List<Fact> FilterPartialFacts(List<Fact> facts)
        => facts.Where(f => f.Operators.All(opr => opr.Value == null)).ToList();
    
    private List<Fact> SortFacts(List<Fact> facts)
        => facts.OrderByDescending(f => f.Operators.Count).ToList();
    
    private List<Fact> PrioritizeFacts(List<Fact> facts)
        => facts.OrderBy(f => f.Operators.Count).ThenBy(f => f.Target).ToList();

    private bool FindAdditionalFacts(List<Fact> facts, Action<string> testOutputHelper)
    {
        bool found = false;
        for (int i = 0; i < facts.Count; i++)
        {
            Fact current = facts[i];
            for (int j = i + 1; j < facts.Count; j++)
            {
                Fact next = facts[j];
                if (current.Operators.Count != next.Operators.Count)
                {
                    if (next.Operators.All(opr => current.Operators.Contains(opr)))
                    {
                        
                        var newFact = new Fact(current.Target - next.Target);
                        newFact.Operators.AddRange(current.Operators.Where(opr => !next.Operators.Contains(opr)));
                        if(Process(facts, newFact))
                        {
                            found = true;
                        }
                    }

                    if (!next.Operators.Any(opr => current.Operators.Contains(opr)))
                    {
                        var  newFact = new Fact(current.Target + next.Target);
                        newFact.Operators.AddRange(current.Operators);
                        newFact.Operators.AddRange(next.Operators);
                        if(Process(facts, newFact))
                        {
                            found = true;
                        }
                    }
                }
                
            }
        }

        return found;
    }

    private bool Process(List<Fact> facts, Fact newFact)
    {
        if (newFact.Operators.Count == 1)
        {
            newFact.Operators.First().Value = newFact.Target;
        }

        if (!CheckContains(facts, newFact))
        {
            facts.Add(newFact);
            newFact.Operators = newFact.Operators.OrderBy(f => f.Id).ToList();
            return true;
        }

        return false;

    }

    private bool CheckContains(List<Fact> facts, Fact fact)
    {
        return facts.Any(f =>  f.Operators.Count == fact.Operators.Count &&  
                                    f.Operators.All(opr => fact.Operators.Contains(opr)));
    }

    public Factory(string input)
    {
        var parts = input.Split(' ');
        int idx = 0;
        foreach (var part in parts)
        {
            // if starts with '[' is Indicator
            // if starts with '(' is Switch
            // if starts with '{' is Joltage
            if (part.StartsWith('[')) Indicator = new Indicator(part);
            if (part.StartsWith('('))
            {
                Switches.Add(new Switch(part, (char)('A' + idx++)));
            }
            if (part.StartsWith('{')) Joltage = new Joltage(part);
        }
    }
    
    public static List<Factory> FromList(List<string> input)
    {
        return input.Select(i => new Factory(i)).ToList();
    }
}

class Fact(int target)
{
    public int Target { get; set; } = target;
    public List<Switch> Operators { get; set; } = new();
    
    public override string ToString()
    {
        return $"{string.Join(" + ", Operators)} = {Target}";
    }
}
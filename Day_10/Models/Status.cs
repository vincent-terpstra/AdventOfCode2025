namespace AOC_2025.Day_10.Models;

public class Status
{
    public List<int> Positions { get; } = new();
    public int Depth { get; private set; } = 0;
    
    public Status(List<int> positions)
    {
        foreach (var position in positions) 
            Positions.Add(position);
    }

    public Status Apply(Switch @switch, int scale = 1)
    {
        foreach (var mod in @switch.Operators)
        {
            int current = Positions[mod];
            Positions[mod] = Math.Max(current - scale, 0);
        }
        
        Depth += scale;
        return this;
    }

    public List<int> Priorities()
    {
        return Positions.Select((p, idx) => (p, idx))
            .Where(p => p.p > 0)
            .OrderByDescending(p => p.p)
            .Select(p => p.idx)
            .ToList();
    }

    public int Distance()
    {
        return Positions.Sum();
    }
    
    public override string ToString() => $"Positions: {string.Join(',', Positions)}";
}
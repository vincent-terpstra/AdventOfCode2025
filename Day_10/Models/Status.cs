namespace AOC_2025.Day_10.Models;

public class Status
{
    public List<int> Positions { get; } = new();
    public int Depth { get; private set; } = 0;
    
    public Status(List<int> positions)
    {
        Positions = positions;
    }

    public Status Next(Switch modify, int depth)
    {
        List<int> newPositions = [..Positions];
        foreach (var m in modify.Operators)
        {
            newPositions[m] -= depth;
        }
        
        return new(newPositions)
        {
            Depth = this.Depth + depth,
        };
    }
    
    public IEnumerable<Status> Apply(Switch @switch)
    {
        yield return this;
        var next = this.Next(@switch);
        while (next.IsValid())
        {
            yield return next;
            next = next.Next(@switch);
        }
    }

    public Status Next(Switch @switch)
    {
        List<int> positions = [..Positions];
        foreach (var op in @switch.Operators)
        {
            positions[op]--;
        }
        return new(positions)
        {
            Depth = Depth + 1
        };
    }

    public bool IsValid()
        => Positions.All(p => p >= 0);

    public bool IsZero() 
        => Positions.All(p => p == 0);
    
    public int Distance()
    {
        return Positions.Sum();
    }
    
    public override string ToString() => $"Positions: {string.Join(',', Positions)}";
}
namespace AOC_2025.Day_8.Models;

public class Circuit
{
    public List<Junction> Junctions { get; set; } = new();

    public void Combine(Circuit other)
    {
        Junctions.AddRange(other.Junctions);
        foreach (var junction in other.Junctions) junction.Circuit = this;
    }
}
namespace AOC_2025.Day_10.Models;

public class Joltage
{
    public List<int> Targets { get; } = new();
    public Joltage(string part)
    {
        var substr = part.Substring(1, part.Length - 2);
        foreach (var c in substr.Split(','))
        {
            Targets.Add(int.Parse(c));
        }
    }
    
    public override string ToString() => $"Targets: {string.Join(',', Targets)}";
}
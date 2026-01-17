namespace AOC_2025.Day_10.Models;

public class Switch
{
    public int Modify { get; }
    public List<int> Operators { get; } = new();
    public Switch(string value)
    {
        var substr = value.Substring(1, value.Length - 2);
        foreach (var c in substr.Split(','))
        {
            int num = int.Parse(c);
            Operators.Add(num);
            Modify += (1 << num);
        }
    }

    public bool Contains(Switch other) => this != other && other.Operators.All(Operators.Contains);
    
    public override string ToString() => $"Modify: {string.Join(',', Operators)}";
}
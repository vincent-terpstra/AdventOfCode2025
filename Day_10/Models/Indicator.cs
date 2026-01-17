namespace AOC_2025.Day_10.Models;

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
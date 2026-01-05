namespace AOC_2025.Day_7.Models;

public class Map
{
    public List<List<char>> Values { get; set; }
    
    public Map(List<string> input)
    {
        Values = input.Select(l => l.ToList()).ToList();
    }
}
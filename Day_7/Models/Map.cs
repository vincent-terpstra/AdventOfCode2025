namespace AOC_2025.Day_7.Models;

public class Map
{
    public List<List<char>> Values { get; set; }
    
    public Map(List<string> input)
    {
        Values = input.Select(l => l.ToList()).ToList();
    }

    public Map(int width, int height, char initial = '.')
    {
        Values = new List<List<char>>(height);
        for (int i = 0; i < height; i++)
        {
            Values.Add(new List<char>(width));
            for (int j = 0; j < width; j++) 
                Values[i].Add(initial);
        }
    }

    public void Display(Action<string> display)
    {
        foreach (var line in Values) 
            display(string.Join("", line));
    }
}
namespace AOC_2025.Day_9;

public class Coordinate
{
    public long X { get; set; }
    public long Y { get; set; }

    public Coordinate(string input)
    {
        var parts = input.Split(',');
        X = long.Parse(parts[0]);
        Y = long.Parse(parts[1]);
    }
    
    public override string ToString() => $"{X},{Y}";
    
    public long Area(Coordinate other) => 
        (Math.Abs(X - other.X) + 1) * (Math.Abs(Y - other.Y) + 1);
    
    public static List<Coordinate> FromFile(List<string> fileInput)
        => fileInput.Select(c => new Coordinate(c)).ToList();
    
}
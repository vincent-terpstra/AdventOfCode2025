namespace AOC_2025.Day_8.Models;

public class Junction
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }
    
    public Circuit? Circuit { get; set; }

    public Junction(string input)
    {
        var parts = input.Split(',');
        X = int.Parse(parts[0]);
        Y = int.Parse(parts[1]);
        Z = int.Parse(parts[2]);
    }

    public long Distance(Junction other)
    {
        long dx = Math.Abs(X - other.X);
        long dy = Math.Abs(Y - other.Y);
        long dz = Math.Abs(Z - other.Z);
        
        return dx * dx + dy * dy + dz * dz;
    }
    
    public override string ToString() => $"{X},{Y},{Z}";
    
    public static List<Junction> FromLines(List<string> lines) 
        => lines.Select(l => new Junction(l)).ToList();
}
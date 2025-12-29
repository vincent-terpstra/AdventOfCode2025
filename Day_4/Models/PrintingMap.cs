namespace AOC_2025.Day_4;

public class PrintingMap
{
    public char[][] Map { get; set; }
    public int Width => Map[0].Length;
    public int Height => Map.Length;

    public PrintingMap(string input)
    {
        Map = input.Split(Environment.NewLine)
            .Select(l => l.ToCharArray()).ToArray();
    }
    
    public char At((int x, int y) pox)
    {
        if (pox.x < 0 || pox.x >= Width || pox.y < 0 || pox.y >= Height) 
            return Legend.Wall;
        return Map[pox.y][pox.x];
    }
    
    public void Remove((int x, int y) pox) 
        => Map[pox.y][pox.x] = Legend.Empty;

    public IEnumerable<(int x, int y)> Squares()
        => Map.SelectMany((row, y) => row.Select((c, x) => (x, y)));
    
    public IEnumerable<(int x, int y)> Adjacent((int x, int y) pox) 
        => Adjacent(pox.x, pox.y);

    // all 8 Adjacent safe on boarders
    public IEnumerable<(int x, int y)> Adjacent(int x, int y)
    {
        yield return (x - 1, y);
        yield return (x + 1, y);
        yield return (x, y - 1);
        yield return (x, y + 1);
        yield return (x - 1, y - 1);
        yield return (x + 1, y - 1);
        yield return (x - 1, y + 1);
        yield return (x + 1, y + 1);
    }
}
using AOC_2025.Day_7.Models;
using Xunit.Abstractions;

namespace AOC_2025.Day_9;

public class MovieTheaterSolution(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void Demo_Input_Should()
    {
        var input = FileHelper.ReadFileAsList(9, "demo.txt");
        var coordinates = Coordinate.FromFile(input);

        long maxArea = 0;
        
        for(int i = 0; i< coordinates.Count; i++)
        {
            Coordinate current = coordinates[i];
            for (int j = i + 1; j < coordinates.Count; j++)
            {
                var area = current.Area(coordinates[j]);

                if (area > maxArea)
                {
                    // _testOutputHelper.WriteLine($"{current} {coordinates[j]} {area}");
                    maxArea = area;
                }
            }
        }
        
        testOutputHelper.WriteLine(maxArea.ToString());
        Assert.Equal(50, maxArea);
        // Assert.Equal(expected, actual)
    }

    [Fact]
    public void Demo_Input_Red_WithFill()
    {
        var input = FileHelper.ReadFileAsList(9, "puzzle.txt");
        var coordinates = Coordinate.FromFile(input);
        
       // var map = new CoordinateMap(12, 12);

        // for (int i = 0; i < coordinates.Count; i++)
        // {
        //     var current = coordinates[i];
        //     var next = coordinates[(i + 1) % coordinates.Count];
        //     map.DrawLine(current, next);
        // }
        //
        // // map.Fill('X');
        //
        // map.Display(testOutputHelper.WriteLine);
        
        var orderedX = coordinates.Select(c => c.X).Order().Distinct().ToList();
        var orderedY = coordinates.Select(c => c.Y).Order().Distinct().ToList();
        
        var xDict = orderedX.Select((x, i) => (x, i)).ToDictionary(x => x.x, x => x.i + 1);
        var yDict = orderedY.Select((x, i) => (x, i)).ToDictionary(x => x.x, x => x.i + 1);

        var scaleMap = new CoordinateMap(orderedX.Count + 2, orderedY.Count + 2);

        for (int i = 0; i < coordinates.Count; i++)
        {
            var current = coordinates[i];
            var next = coordinates[(i + 1) % coordinates.Count];
            
            var x1 = xDict[current.X];
            var y1 = yDict[current.Y];
            var x2 = xDict[next.X];
            var y2 = yDict[next.Y];
            
            scaleMap.DrawLine(x1, y1, x2, y2);
        }
        
        scaleMap.Fill(' ');
        
        scaleMap.Display(testOutputHelper.WriteLine);
    }
}

public class CoordinateMap(int width, int height) : Map(width, height)
{
    public void SetValue(int x, int y, char value)
    {
        base.Values[x][y] = value;
    }

    public void DrawLine(int x1, int y1, int x2, int y2)
    {
        int dx = (int)Math.Sign(x2 - x1);
        int dy = (int)Math.Sign(y2 - y1);
        
        int x = x1;
        int y = y1;
        
        SetValue(x, y, '#');
        while (x != x2 || y != y2)
        {
            x += dx;
            y += dy;
            SetValue(x, y, '#');
        }
    }

    public void DrawLine(Coordinate a, Coordinate b)
    {
        DrawLine((int)a.X, (int)a.Y,(int)b.X, (int)b.Y);
    }

    public void Fill(char set)
    {
        List<(int x, int y)> queue = new();
        int width = Values.Count;
        int height = Values[0].Count;
        queue.Add((0, 0));

        while (queue.Count > 0)
        {
            var (x, y) = queue.First();
            queue.RemoveAt(0);
            
            // check boundary
            if (x >= 0 && y >= 0 && x < width && y < height && Values[x][y] == '.')
            {
                SetValue(x, y, set);
                queue.Add((x - 1, y));
                queue.Add((x + 1, y));
                queue.Add((x, y - 1));
                queue.Add((x, y + 1));
            }
        }
    }
}

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
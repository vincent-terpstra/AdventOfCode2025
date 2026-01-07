using Xunit.Abstractions;

namespace AOC_2025.Day_9;

public class MovieTheaterSolution
{
    private readonly ITestOutputHelper _testOutputHelper;

    public MovieTheaterSolution(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

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
        
        _testOutputHelper.WriteLine(maxArea.ToString());
        Assert.Equal(50, maxArea);
        // Assert.Equal(expected, actual)
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
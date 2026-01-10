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

        long maxArea = 0;
        for (int i = 0; i < coordinates.Count; i++)
        {
            for (int j = i + 1; j < coordinates.Count; j++)
            {
                var current = coordinates[i];
                var next = coordinates[j];
                
                var x1 = xDict[current.X];
                var y1 = yDict[current.Y];
                
                var x2 = xDict[next.X];
                var y2 = yDict[next.Y];
                
                var area = current.Area(next);
                if (area > maxArea && scaleMap.DoesNotContain(x1, y1, x2, y2))
                {
                    maxArea = area;
                }
            }
        }
        
        testOutputHelper.WriteLine(maxArea.ToString());
    }
}
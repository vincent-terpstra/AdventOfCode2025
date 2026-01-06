using AOC_2025.Day_7.Models;
using Xunit.Abstractions;

namespace AOC_2025.Day_7;

public class TachyonSolution(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void Demo_Input_Count_Split()
    {
        var input = FileHelper.ReadFileAsList(7, "demo.txt");

        TachyonMap map = new(input);
        
        var splits = map.Process();
        
        testOutputHelper.WriteLine(splits.ToString());
        Assert.Equal(21, splits);
    }

    [Fact]
    public void Demo_Input_Count_TimeSplits()
    {
        var input = FileHelper.ReadFileAsList(7, "demo.txt");
        TachyonMap map = new (input);

        var splits = map.ProcessTimeSplits(testOutputHelper.WriteLine);
        
        testOutputHelper.WriteLine(splits.ToString());
    }
}
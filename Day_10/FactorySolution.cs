using AOC_2025.Day_10.Models;
using Xunit.Abstractions;

namespace AOC_2025.Day_10;

public class FactorySolution(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void Demo_Input_Should()
    {
        var input = FileHelper.ReadFileAsList(10, "puzzle.txt");
        var factories = Factory.FromList(input);

        int total = 0;
        foreach (var factory in factories)
        {
            total += factory.Solve(testOutputHelper.WriteLine);
        }

        testOutputHelper.WriteLine(total.ToString());
    }

    [Fact]
    public void Demo_Input_Should_2()
    {
        var input = FileHelper.ReadFileAsList(10, "puzzle.txt");
        var factories = Factory.FromList(input);
        
        var sum = factories
            .Select(f => f.Solve2(testOutputHelper.WriteLine))
            .Output(testOutputHelper)
            .Sum();
        testOutputHelper.WriteLine(sum.ToString());
    }
}
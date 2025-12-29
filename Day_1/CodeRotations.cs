using AOC_2025.Day_1.Models;
using Xunit.Abstractions;

namespace AOC_2025.Day_1;

public class CodeRotations
{
    private readonly ITestOutputHelper _output;

    public CodeRotations(ITestOutputHelper output)
    {
        _output = output;
    }
    
    [Fact]
    public void Password_Is_TotalRotations()
    {
        var input = FileHelper.ReadFileAsList(1, "firstInput.txt")
            .Select(c => new SafeAction(c))
            .ToList();

        int start = 50;
        int totalZeros = 0;
        
        foreach (var direction in input)
        {
            start = direction.Apply(start, ref totalZeros);
        }
        
        _output.WriteLine(totalZeros.ToString());
    }

        
    [Theory]
    [MemberData(nameof(GetExamples))]
    public void Password_Is_TotalRotations_2(List<string> input, int expected)
    {
        var actions = input.Select(c => new SafeAction(c)).ToList();
        int start = 50;
        int totalZeros = 0;
        
        foreach (var direction in actions)
        {
            _output.WriteLine(direction.ToString());
            start = direction.Apply(start, ref totalZeros);
            _output.WriteLine(start.ToString());
            _output.WriteLine(totalZeros.ToString());
        }
        
        Assert.Equal(expected, totalZeros);
        
    }

    public static IEnumerable<object[]> GetExamples()
        => new[]
        {
            new object[] {new List<string>(){"L50", "R50"}, 1},
            new object[] {new List<string>(){"L50", "L50"}, 1},
            new object[] {new List<string>(){"R50", "L50"}, 1},
            new object[] {new List<string>(){"R50", "R50"}, 1},
            new object[] {new List<string>(){"L150", "L50"}, 2},
            new object[] {new List<string>(){"L150", "R50"}, 2},
            new object[] {new List<string>(){"R150", "L50"}, 2},
            new object[] {new List<string>(){"R150", "R50"}, 2},
        };
}
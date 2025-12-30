using Xunit.Abstractions;

namespace AOC_2025.Day_5;

public class CafeteriaSolution
{
    private readonly ITestOutputHelper _testOutputHelper;

    public CafeteriaSolution(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Demo_Input_Should()
    {
        var text = FileHelper.ReadFileAsList(5, "puzzle.txt");

        var (receipes, ingredients) = ParseInput(text);

        var fresh = receipes.Select(r => new Range(r))
            .ToList();

        var available = ingredients.Select(long.Parse)
            .ToList();

        int spoiled = 0;
        foreach (var value in available)
        {
            if (IsSpoiled(fresh, value))
            {
                spoiled++;
            }
        }

        _testOutputHelper.WriteLine(spoiled);
    }

    private bool IsSpoiled(List<Range> ranges, long value)
    {
        foreach (var range in ranges)
        {
            if (value >= range.Min && value <= range.Max) return true;
        }

        return false;
    }

    private (List<string>, List<string>) ParseInput(List<string> input)
    {
        List<string> receipes = new();
        List<string> ingredients = new();

        bool emptyLine = false;


        foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                emptyLine = true;
                continue;
            }

            if (emptyLine)
            {
                ingredients.Add(line);
            }
            else
            {
                receipes.Add(line);
            }
        }

        return (receipes, ingredients);
    }
}

public class Range
{
    public long Min;
    public long Max;

    public Range(string input)
    {
        // split on -
        var parts = input.Split('-');
        Min = long.Parse(parts[0]);
        Max = long.Parse(parts[1]);
    }
}
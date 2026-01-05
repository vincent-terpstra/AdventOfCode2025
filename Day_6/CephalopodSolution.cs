using Xunit.Abstractions;

namespace AOC_2025.Day_6;

public class CephalopodSolution(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void Demo_Input_Should()
    {
        var lines = FileHelper.ReadFileAsList(6, "puzzle.txt");
        
        // parse the first row into a list of MathRow
        var rows = ParseMathRowsFromLines(lines);
        long result = rows.Select(r => r.Result()).Sum();
        
        testOutputHelper.WriteLine(result.ToString());
    }

    [Fact]
    public void Demo_Input_Part2_Should()
    {
        var lines = FileHelper.ReadFileAsList(6, "puzzle.txt");
        
        int length = lines.First().Count();
        Assert.All(lines.Skip(1), r => Assert.Equal(length, r.Count()));

        List<string> input = new();
        for (int i = 0; i < length; i++)
        {
            var result = string.Join(string.Empty, lines.Select(l => l[i]));
            input.Add(result);
        }

        long total = 0;
        long subtotal = 0;
        char Operand = MathOperator.Add;
        
        
        foreach (string value in input)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                total += subtotal;
                subtotal = 0;
            } else if (value.EndsWith(MathOperator.Add.ToString()))
            {
                Operand = MathOperator.Add;
                subtotal = long.Parse(value.Substring(0, value.Length - 1));
            }
            else if (value.EndsWith(MathOperator.Multiply.ToString()))
            {
                Operand = MathOperator.Multiply;
                subtotal = long.Parse(value.Substring(0, value.Length - 1));
            }
            else
            {
                long number = long.Parse(value);
                if (Operand == MathOperator.Add)
                {
                    subtotal += number;
                } else if (Operand == MathOperator.Multiply)
                {
                    subtotal *= number;
                }
            }
            
        }
        total += subtotal;
        
        testOutputHelper.WriteLine(total.ToString());
    }
    
    private static List<MathRow> ParseMathRowsFromLines(List<string> lines)
    {
        List<MathRow> rows = new();
        foreach (var line in lines)
        {
            // split line on whitespace including multiple characters
            var values = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (rows.Count == 0)
            {
                foreach (var value in values)
                {
                    MathRow row = new();
                    row.Apply(value);
                    rows.Add(row);
                }
            }
            else
            {
                rows.Zip(values, (r, v) =>
                {
                    r.Apply(v);
                    return r;
                }).ToList();
            }
        }
        return rows;
    }
}

public static class MathOperator
{
    public const char Add = '+';
    public const char Multiply = '*';
}


public class MathRow
{
    public List<long> Values { get; set; } = new();
    
    public char Operator { get; set; }

    public long Result()
    {
        return Operator switch
        {
            MathOperator.Add => Values.Sum(),
            MathOperator.Multiply => Values.Aggregate(1L, (acc, v) => acc * v),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    public void Apply(string value)
    {
        if (value.Contains(MathOperator.Add))
        {
            Operator = MathOperator.Add;
        } else if (value.Contains(MathOperator.Multiply))
        {
            Operator = MathOperator.Multiply;
        } else if (long.TryParse(value, out var number))
        {
            Values.Add(number);
        }
    }
}
using AOC_2025.Day_2.Models;
using Xunit.Abstractions;

namespace AOC_2025.Day_2;

public class GiftShopSolution
{
    private readonly ITestOutputHelper _output;

    public GiftShopSolution(ITestOutputHelper output)
    {
        _output = output;
    }
    
    [Fact]
    public void GiftShopSolution_DemoInput()
    {
        var input = FileHelper.ReadFile(2, "puzzleInput.txt");
        var ranges = GiftShopRange.FromFile(input);

        long sum = 0;
        foreach (var range in ranges)
        {
            // _output.WriteLine(range.ToString());
            long valid = range.Range()
                .Where(GiftShopValidator.IsValid2)
            //    .Select(_output.Output<long>())
                .Sum();
            sum += valid;
            // _output.WriteLine($"Valid: {valid}");
        }
        
        _output.WriteLine($"Sum: {sum}");
    }

    [Theory]
    [InlineData(990099)]
    [InlineData(1000001)]
    [InlineData(1010101)]
    [InlineData(1000100)]
    public void Example_IsNotValid(long number)
    {
        Assert.False(GiftShopValidator.IsValid(number));
    }
    
    [Theory]
    [InlineData(111)]
    public void Example_IsValid2(long number)
    {
        Assert.True(GiftShopValidator.IsValid2(number));
    }
    
    [Theory]
    [InlineData(101)]
    [InlineData(1001)]
    public void Example_IsNotValid2(long number)
    {
        Assert.True(GiftShopValidator.IsValid2(number));
    }
}
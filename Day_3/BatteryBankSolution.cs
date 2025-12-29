using AOC_2025.Day_3.Models;
using Xunit.Abstractions;

namespace AOC_2025.Day_3;

public class BatteryBankSolution
{
    private readonly ITestOutputHelper _output;

    public BatteryBankSolution(ITestOutputHelper output)
    {
        _output = output;
    }
    
    [Fact]
    private void Demo_Input_Should()
    {
        List<string> text = FileHelper.ReadFileAsList(3, "puzzle.txt");
        var banks = BatteryBank.SelectBatteryBanks(text)
            .Select(b => b.Power())
            .Sum();
        
        _output.WriteLine(banks.ToString());
    }

    [Fact]
    public void Demo_Input_Should_2()
    {
        List<string> text = FileHelper.ReadFileAsList(3, "puzzle.txt");
        var first = BatteryBank.SelectBatteryBanks(text)
        //    .Output(_output)
            .Select(b => b.Power2())
        //    .Output(_output)
            .Sum();
        
        _output.WriteLine(first.ToString());
    }
}
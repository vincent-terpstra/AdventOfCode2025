using System.Drawing;
using Xunit.Abstractions;

namespace AOC_2025.Day_4;

public class PrintingSolution
{
    private readonly ITestOutputHelper _testOutputHelper;

    public PrintingSolution(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Demo_Text_ShouldCount_Adjacent()
    {
        var map = new PrintingMap(FileHelper.ReadFile(4, "puzzle.txt"));

        var squares = map
            .Squares()
            .Where(p => map.At(p) == Legend.Paper &&
                        map.Adjacent(p).Count(a => map.At(a) == Legend.Paper) < 4);

        _testOutputHelper.WriteLine(squares.Count());
    }
    
    [Fact]
    public void PaperMap_WithRemoveEntities()
    {
        var map = new PrintingMap(FileHelper.ReadFile(4, "puzzle.txt"));

        int total = 0;
        int removed;
        
        do
        {
            var canRemove = map.Squares()
                .Where(p => map.At(p) == Legend.Paper &&
                            map.Adjacent(p).Count(a => map.At(a) == Legend.Paper) < 4)
                .ToList();
            
            removed = canRemove.Count();
            total += removed;
            
            canRemove.ForEach(map.Remove);
            
        } while (removed > 0);
        
        _testOutputHelper.WriteLine(total);
    }
}
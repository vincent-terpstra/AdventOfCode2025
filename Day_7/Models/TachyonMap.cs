namespace AOC_2025.Day_7.Models;

public class TachyonMap(List<string> input) : Map(input)
{
    public int Process()
    {
        int totalSplits = 0;
        
        var start = Values.First().IndexOf(TachyonMapLegend.Start);
        Values.First()[start] = TachyonMapLegend.Beam;

        for (int i = 0; i < Values.Count - 1; i++)
        {
            var current = Values[i];
            var next = Values[(i + 1)];

            for (int j = 0; j < current.Count; j++)
            {
                if (current[j] == TachyonMapLegend.Beam)
                {
                    var nextValue = next[j];
                    
                    if (nextValue == TachyonMapLegend.Empty)
                    {
                        next[j] = TachyonMapLegend.Beam;
                    } 
                    else if (nextValue == TachyonMapLegend.Split)
                    {
                        totalSplits++;
                        next[j-1] = TachyonMapLegend.Beam;
                        next[j+1] = TachyonMapLegend.Beam;
                    }
                }
            }
        }
        
        return totalSplits;
    }

    public long ProcessTimeSplits()
    { 
        var col = Values.First().IndexOf(TachyonMapLegend.Start);
        int row = 0;
        return CacheSplits(row, col);
    }

    private long CacheSplits(int row, int col, Dictionary<(int, int), long>? counts = null)
    {
        counts ??= new();
        if(counts.TryGetValue((row, col), out var count)) 
            return count;
        
        var total = CountSplits(row, col);
        counts[(row, col)] = total;
        return total;
    }
    
    public long CountSplits(int row, int col)
    {
        if (row >= Values.Count - 1)
            return 0;
        
        var next = Values[row + 1][col];
        
        if (next == TachyonMapLegend.Split)
        {
            var left = CountSplits(row + 1, col - 1);
            var right = CountSplits(row + 1, col + 1);
            long total = left + right + 1;
            return total;
            
        } else if (next == TachyonMapLegend.Empty)
        {
            return CountSplits(row + 1, col);
        }

        return 0;
    }
}
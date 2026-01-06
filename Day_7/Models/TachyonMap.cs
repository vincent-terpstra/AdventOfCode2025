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

    public int ProcessTimeSplits(Action<string> log = null)
    { 
        var col = Values.First().IndexOf(TachyonMapLegend.Start);
        int row = 0;
        Dictionary<(int, int), int > Counts = new();
        
        var count =  CountSplits(row, col, Counts);
        foreach (var key in Counts)
        {
            log?.Invoke($"{key.Key.Item1}, {key.Key.Item2}: {key.Value}");
            
        }

        return count;
    }

    public int CountSplits(int row, int col, Dictionary<(int, int), int> counts)
    {
        if(counts.TryGetValue((row, col), out var count)) 
            return count;

        if (row >= Values.Count - 1)
            return 0;
        
        var next = Values[row + 1][col];
        
        if (next == TachyonMapLegend.Split)
        {
            var left = CountSplits(row + 1, col - 1, counts);
            var right = CountSplits(row + 1, col + 1, counts);
            int total = left + right + 1;
            counts[(row, col)] = total;
            return total;
            
        } else if (next == TachyonMapLegend.Empty)
        {
            return CountSplits(row + 1, col, counts);
        }

        return 0;
    }
}
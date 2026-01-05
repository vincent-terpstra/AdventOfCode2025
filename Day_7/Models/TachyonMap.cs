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

    public int ProcessTimeSplits()
    { int totalSplits = 0;
        
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
}
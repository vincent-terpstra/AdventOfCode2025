using System.Reflection.Metadata.Ecma335;

namespace AOC_2025.Day_3.Models;

public class BatteryBank
{
    public string Value { get; set; }
    
    public BatteryBank(string value)
    {
        Value = value;
    }

    public long Power()
    {
        int length = Value.Length;
        
        int lower = Value[length - 1] - '0';
        int upper = Value[length - 2] - '0';

        
        int idx = length - 3;
        while (idx >= 0)
        {
            int at = Value[idx] - '0';
            if (at >= upper)
            {
                if (upper > lower)
                {
                    lower = upper;
                }
                upper = at;
            }
            idx--;
        }
        
        return upper * 10 + lower;
    }

    public long Power2()
    {
        int length = Value.Length;
        long total = 0;
        int minIndex = -1;
        for (int current = 12; current > 0; current --)
        {
            int idx = length - current;
            int lowest = Value[idx] - '0';
            int lowestIndex = idx;
            while (idx > minIndex)
            {
                int at = Value[idx] - '0';
                if (at >= lowest)
                {
                    lowest = at;
                    lowestIndex = idx;
                }
                idx--;
            }
            minIndex = lowestIndex;
            total = total * 10 + lowest;
        }
        
        return total;
    }

    public override string ToString() 
        => Value.ToString();

    public static IEnumerable<BatteryBank> SelectBatteryBanks(List<string> values)
        => values.Select(v => new BatteryBank(v));
}
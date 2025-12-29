namespace AOC_2025.Day_2.Models;

public class GiftShopRange
{
    public long Min;
    public long Max;
    
    public GiftShopRange(string input)
    {
        var parts = input.Split('-');
        Min = long.Parse(parts[0]);
        Max = long.Parse(parts[1]);
    }

    public IEnumerable<long> Range()
    {
        for (long i = Min; i <= Max; i++) yield return i;
    }
        
    
    public override string ToString() => $"Range: {Min} - {Max}";
    
    public static IEnumerable<GiftShopRange> FromFile(string fileInput)
        => fileInput.Split(',').Select(r => new GiftShopRange(r));
}
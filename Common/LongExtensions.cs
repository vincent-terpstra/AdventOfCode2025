namespace AOC_2025.Common;

public static class LongExtensions
{
    public static long At(this long value, long pow) => ( value / pow ) % 10;
}
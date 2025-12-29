using Xunit.Abstractions;

namespace AOC_2025.Common;

public static class TestOutputExtensionMethods
{
    public static Func<T, T> Output<T>(this ITestOutputHelper output) =>
        (T input) =>
        {
            output.WriteLine(input?.ToString());
            return input;
        };

    public static IEnumerable<T> Output<T>(this IEnumerable<T> input, ITestOutputHelper output)
    {
        foreach (var item in input)
        {
            output.WriteLine(item?.ToString());
            yield return item;
        }
    }
    
    public static void WriteLine(this ITestOutputHelper output, object message) 
        => output.WriteLine(message.ToString());
}
namespace AOC_2025.Common;

public static class FileHelper
{
    private static string FilePath(int day, string filename) =>
        Path.Combine(Directory.GetCurrentDirectory(), $"Day_{day}/Data/{filename}");
    
    public static string ReadFile(int day, string filename)
    {
        var directory = Directory.GetCurrentDirectory();
        return File.ReadAllText(FilePath(day, filename));
    }
    
    public static List<string> ReadFileAsList(int day, string filename)
    {
        return File.ReadAllLines(FilePath(day, filename)).ToList();
    }
}
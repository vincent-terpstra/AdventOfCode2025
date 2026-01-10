using AOC_2025.Day_7.Models;

namespace AOC_2025.Day_9;

public class CoordinateMap(int width, int height) : Map(width, height)
{
    public void SetValue(int x, int y, char value)
    {
        base.Values[x][y] = value;
    }

    public void DrawLine(int x1, int y1, int x2, int y2)
    {
        int dx = (int)Math.Sign(x2 - x1);
        int dy = (int)Math.Sign(y2 - y1);
        
        int x = x1;
        int y = y1;
        
        SetValue(x, y, '#');
        while (x != x2 || y != y2)
        {
            x += dx;
            y += dy;
            SetValue(x, y, '#');
        }
    }

    public void DrawLine(Coordinate a, Coordinate b)
    {
        DrawLine((int)a.X, (int)a.Y,(int)b.X, (int)b.Y);
    }

    public bool DoesNotContain(int x, int y, int x2, int y2, char empty = ' ')
    {
        if(x > x2) (x, x2) = (x2, x);
        if(y > y2) (y, y2) = (y2, y);
        
        for (int i = x; i <= x2; i++)
        {
            for (int j = y; j <= y2; j++)
            {
                if (Values[i][j] == empty) return false;
            }
        }

        return true;
    }

    public void Fill(char set)
    {
        List<(int x, int y)> queue = new();
        int width = Values.Count;
        int height = Values[0].Count;
        queue.Add((0, 0));

        while (queue.Count > 0)
        {
            var (x, y) = queue.First();
            queue.RemoveAt(0);
            
            // check boundary
            if (x >= 0 && y >= 0 && x < width && y < height && Values[x][y] == '.')
            {
                SetValue(x, y, set);
                queue.Add((x - 1, y));
                queue.Add((x + 1, y));
                queue.Add((x, y - 1));
                queue.Add((x, y + 1));
            }
        }
    }
}
namespace AOC_2025.Day_1.Models;

public class SafeAction
{
    public const int ROTATION = 100;
    public Direction Direction { get; init; }
    public int Distance { get; init; }

    public SafeAction(string row)
    {
        var direction = row[0];
        var number = int.Parse(row.Substring(1));
        Direction = direction == 'R' ? Direction.Right : Direction.Left;
        Distance = number;
    }

    public int Apply(int position, ref int totalZeros)
    {
        totalZeros += Distance / ROTATION;
        int remainder = Distance % ROTATION;

        if (remainder == 0) return position;

        if (Direction == Direction.Right)
        {
            position += remainder;
            if (position >= ROTATION)
            {
                totalZeros++;
                position -= ROTATION;
            }
        }
        // Direction.Left
        else if (position == 0)
        {
            position = ROTATION - remainder;
        }
        else
        {
            position -= remainder;
            if (position < 0)
            {
                totalZeros++;
                position += ROTATION;
            }
            else if (position == 0)
            {
                totalZeros++;
            }
        }

        return position;
    }

    public override string ToString()
    {
        return $"{Direction} {Distance}";
    }
}

public enum Direction
{
    Right,
    Left
};
namespace AOC_2025.Day_8.Models;

public static class JunctionExtensions
{
    public static (Junction first, Junction second) Closest(this List<Junction> junctions)
    {
        (Junction first, Junction second) closest = default!;
        int closestDistance = int.MaxValue;

        for (int i = 0; i < junctions.Count; i++)
        {
            for (int j = i + 1; j < junctions.Count; j++)
            {
                var distance = junctions[i].Distance(junctions[j]);
                if (distance < closestDistance) closest = (junctions[i], junctions[j]);
            }
        }
        
        return closest;
    }

    public static void AddClosest(this PriorityQueue<(Junction, Junction), long> queue, Junction current,
        List<Junction> junctions)
    {
        foreach (var junction in junctions)
        {
            var distance = current.Distance(junction);
            queue.Enqueue((current, junction), distance);
        }
    }
    
    public static Junction FindClosest(this List<Junction> junctions, Junction current)
    {
        return junctions.MinBy(current.Distance)!;
    }
}
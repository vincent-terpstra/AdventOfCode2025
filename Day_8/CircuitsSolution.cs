using AOC_2025.Day_8.Models;
using Xunit.Abstractions;

namespace AOC_2025.Day_8;

public class CircuitsSolution(ITestOutputHelper testOutputHelper)
{
    
    private  PriorityQueue<(Junction, Junction), long> CreateQueue(int queueSize) 
    => new(queueSize + 1
        //Comparer<long>.Create((x, y) => x.CompareTo(y))
    );
    
    [Fact]
    public void Demo_Circuit_Should()
    {
        int queueSize = 10;
        var input = FileHelper.ReadFileAsList(8, "demo.txt");
        
        var junctions = Junction.FromLines(input);
        
        PriorityQueue<(Junction, Junction), long> queue = CreateQueue(queueSize);
        
        for(int i = 0; i < junctions.Count; i++)
        {
            for (int j = i + 1; j < junctions.Count; j++)
            {
                var distance = junctions[i].Distance(junctions[j]);
                queue.Enqueue((junctions[i], junctions[j]), distance);
                if(queue.Count > queueSize)
                    queue.Dequeue();
            }
        }

        List<Circuit> connections = new();
        while (queue.Count > 0)
        {
            var (a, b) = queue.Dequeue();
            connections.Combine(a, b);
        }

        var multi = connections.Select(c => c.Junctions.Count)
            .OrderByDescending(c => c)
            .Take(3)
            .Aggregate(1, (a, b) => a * b);
        
        testOutputHelper.WriteLine(multi.ToString());
    }

    [Fact]
    public void Demo_Circuit_Should_Part_Two()
    {
        var input = FileHelper.ReadFileAsList(8, "puzzle.txt");
        List<Junction> junctions = Junction.FromLines(input);
        List<Junction> connected = new();
        
        // find the two closest junctions
        var (first, second) = junctions.Closest();
        connected.Add(first);
        connected.Add(second);
        junctions.Remove(first);
        junctions.Remove(second);
        
        var queue = CreateQueue(100);
        // keep going until one junction is left
        queue.AddClosest(first, junctions);
        queue.AddClosest(second, junctions);
        
        // find the closest junction to that circuit
        while (junctions.Count > 1)
        {
            var (current, next) = queue.Dequeue();
            if (!connected.Contains(next))
            {
                connected.Add(next);
                junctions.Remove(next);
                queue.AddClosest(next, junctions);
            }
        }
        var remainder = junctions.First();
        var closest = connected.FindClosest(remainder);
        
        long multiX = (long)remainder.X * (long)closest.X;
        testOutputHelper.WriteLine(multiX.ToString());
    }
}
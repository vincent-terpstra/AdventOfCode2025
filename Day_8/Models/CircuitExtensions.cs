namespace AOC_2025.Day_8.Models;

public static class CircuitExtensions
{
    public static void Combine(this List<Circuit> connections, Junction a, Junction b)
    {
        if (a.Circuit is null && b.Circuit is null)
        {
            var circuit = new Circuit {Junctions = {a, b}};
            a.Circuit = circuit;
            b.Circuit = circuit;
            connections.Add(circuit);
        }
        else if (a.Circuit == b.Circuit)
        {
                
        }
        else if(a.Circuit is not null && b.Circuit is not null)
        {
            connections.Remove(b.Circuit);
            a.Circuit.Combine(b.Circuit);  
        } else if (a.Circuit is not null)
        {
            b.Circuit = a.Circuit;
            a.Circuit.Junctions.Add(b);
                
        } else if (b.Circuit is not null)
        {
            a.Circuit = b.Circuit;
            b.Circuit.Junctions.Add(a);
        }
    }
}
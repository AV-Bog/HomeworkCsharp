using System.Collections;

namespace HW4.Routers;

public class Graph : IEnumerable<Edge>
{
    private List<Edge> _graph = new List<Edge>();

    public Graph() { }

    public Graph(Edge edge)
    {
        _graph.Add(edge);
    }

    public IEnumerator<Edge> GetEnumerator()
    {
        return _graph.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(Graph graph)
    {
        foreach (Edge edge in graph)
        {
            _graph.Add(edge);
        }
    }

    public void Add(Edge edge)
    {
        _graph.Add(edge);
    }

    public int GetWeight()
    {
        int weight = 0;
        foreach (var edge in _graph)
        {
            weight += edge.EdgeWeight;
        }
        return weight;
    }

    public override string ToString()
    {
        string result = string.Empty;
        foreach (var edge in _graph)
        {
            result += $"{edge.VertexA} {edge.VertexB} {edge.EdgeWeight}\n";
        }
        
        return result;
    }

    public void Sort()
    {
        _graph.Sort();
    }

    public static bool IsGraphConnected(Graph graph)
    {
        if (graph.Count() == 0) return false;

        HashSet<int> visited = new HashSet<int>();
        Queue<int> queue = new Queue<int>();
        var enumerator = graph.GetEnumerator();
        enumerator.MoveNext();
        int firstVertex = enumerator.Current.VertexA;

        queue.Enqueue(firstVertex);
        visited.Add(firstVertex);

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();
            foreach (Edge edge in graph)
            {
                if (edge.VertexA == current && !visited.Contains(edge.VertexB))
                {
                    visited.Add(edge.VertexB);
                    queue.Enqueue(edge.VertexB);
                }
                else if (edge.VertexB == current && !visited.Contains(edge.VertexA))
                {
                    visited.Add(edge.VertexA);
                    queue.Enqueue(edge.VertexA);
                }
            }
        }

        HashSet<int> allVertices = new HashSet<int>();
        foreach (Edge edge in graph)
        {
            allVertices.Add(edge.VertexA);
            allVertices.Add(edge.VertexB);
        }

        return visited.Count == allVertices.Count;
    }
}
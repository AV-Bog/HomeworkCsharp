using System.Collections;

namespace HW4.Routers;

public class Graph : IEnumerable<Edge>
{
    private List<Edge> _graph;

    public Graph()
    {
        _graph = new List<Edge>();
    }
    
    public Graph(Edge val)
    {
        Edge[] value = new Edge[] {val};
        _graph = new List<Edge>();
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
}
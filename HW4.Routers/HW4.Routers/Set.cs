namespace HW4.Routers;

public class Set
{
    public Graph SetGraph;
    public List<int> Vertices;

    public Set(Edge edge)
    {
        SetGraph = new Graph(edge);
        Vertices = new List<int>() { edge.VertexA, edge.VertexB };
    }

    public bool Contains(int vertex)
    {
        return Vertices.Contains(vertex);
    }

    public void Union(Set otherSet, Edge connectingEdge)
    {
        Vertices.AddRange(otherSet.Vertices);
        SetGraph.Add(otherSet.SetGraph);
        SetGraph.Add(connectingEdge);
    }
}












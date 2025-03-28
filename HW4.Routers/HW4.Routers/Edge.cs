namespace HW4.Routers;

public class Edge : IComparable<Edge>
{
    public int EdgeWeight { get; set; }
    public int VertexA { get; set; }
    public int VertexB { get; set; }

    public Edge(int edgeWeight, int vertexA, int vertexB)
    {
        EdgeWeight = edgeWeight;
        VertexA = vertexA;
        VertexB = vertexB;
    }

    public int CompareTo(Edge? other)
    {
        if (other == null)
        {
            return 1;
        }
        return EdgeWeight.CompareTo(other.EdgeWeight);
    }
}












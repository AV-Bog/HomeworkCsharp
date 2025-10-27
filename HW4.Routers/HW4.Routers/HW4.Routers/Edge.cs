// <copyright file="Edge.cs" author="AV-Bog">
// under MIT License
// </copyright>

namespace HW4.Routers;

/// <summary>
/// Represents an edge in a graph with weight and connected vertices.
/// </summary>
public class Edge(int edgeWeight, int vertexA, int vertexB) : IComparable<Edge>
{
    /// <summary>
    /// Gets or sets the weight of the edge.
    /// </summary>
    public int EdgeWeight { get; set; } = edgeWeight;

    /// <summary>
    /// Gets or sets the first vertex connected by the edge.
    /// </summary>
    public int VertexA { get; set; } = vertexA;

    /// <summary>
    /// Gets or sets the second vertex connected by the edge.
    /// </summary>
    public int VertexB { get; set; } = vertexB;

    /// <summary>
    /// Compares this edge with another edge by weight.
    /// </summary>
    /// <param name="other">The edge to compare with.</param>
    /// <returns>
    /// 1 if the other edge is null, otherwise a comparison of edge weights.
    /// </returns>
    public int CompareTo(Edge? other)
    {
        return other == null ? 1 : this.EdgeWeight.CompareTo(other.EdgeWeight);
    }
}
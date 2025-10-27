// <copyright file="Set.cs" author="AV-Bog">
// under MIT License
// </copyright>

namespace HW4.Routers;

/// <summary>
/// Represents a set of vertices and edges used in Kruskal's algorithm.
/// </summary>
public class Set
{
    /// <summary>
    /// The graph associated with this set.
    /// </summary>
    public Graph SetGraph;

    /// <summary>
    /// The vertices contained in this set.
    /// </summary>
    public List<int> Vertices;

    /// <summary>
    /// Initializes a new instance of the <see cref="Set"/> class.
    /// Initializes a new set containing the vertices of the given edge.
    /// </summary>
    /// <param name="edge">The initial edge for the set.</param>
    public Set(Edge edge)
    {
        this.SetGraph = new Graph(edge);
        this.Vertices = new List<int>() { edge.VertexA, edge.VertexB };
    }

    /// <summary>
    /// Determines whether the set contains a specific vertex.
    /// </summary>
    /// <param name="vertex">The vertex to locate.</param>
    /// <returns>True if the set contains the vertex, false otherwise.</returns>
    public bool Contains(int vertex)
    {
        return this.Vertices.Contains(vertex);
    }

    /// <summary>
    /// Merges another set into this set, adding a connecting edge.
    /// </summary>
    /// <param name="otherSet">The set to merge with.</param>
    /// <param name="connectingEdge">The edge connecting the sets.</param>
    public void Union(Set otherSet, Edge connectingEdge)
    {
        this.Vertices.AddRange(otherSet.Vertices);
        this.SetGraph.Add(otherSet.SetGraph);
        this.SetGraph.Add(connectingEdge);
    }
}
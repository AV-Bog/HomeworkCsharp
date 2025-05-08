// <copyright file="Graph.cs" author="AV-Bog">
// under MIT License
// </copyright>

namespace HW4.Routers;

using System.Collections;

/// <summary>
/// Represents a graph consisting of edges between vertices.
/// </summary>
public class Graph : IEnumerable<Edge>
{
    private List<Edge> _graph = new List<Edge>();

    /// <summary>
    /// Initializes a new instance of the <see cref="Graph"/> class.
    /// Initializes a new empty graph.
    /// </summary>
    public Graph()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Graph"/> class.
    /// Initializes a new graph with a single edge.
    /// </summary>
    /// <param name="edge">The initial edge to add to the graph.</param>
    public Graph(Edge edge)
    {
        this._graph.Add(edge);
    }

    /// <summary>
    /// Returns an enumerator that iterates through the edges in the graph.
    /// </summary>
    /// <returns>An enumerator for the edges.</returns>
    public IEnumerator<Edge> GetEnumerator()
    {
        return this._graph.GetEnumerator();
    }

    /// <summary>
    /// Returns a non-generic enumerator that iterates through the edges in the graph.
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through the collection.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    /// <summary>
    /// Adds all edges from another graph to this graph.
    /// </summary>
    /// <param name="graph">The graph whose edges to add.</param>
    public void Add(Graph graph)
    {
        foreach (Edge edge in graph)
        {
            this._graph.Add(edge);
        }
    }

    /// <summary>
    /// Adds a single edge to the graph.
    /// </summary>
    /// <param name="edge">The edge to add.</param>
    public void Add(Edge edge)
    {
        this._graph.Add(edge);
    }

    /// <summary>
    /// Calculates the total weight of all edges in the graph.
    /// </summary>
    /// <returns>The sum of all edge weights.</returns>
    public int GetWeight()
    {
        int weight = 0;
        foreach (var edge in this._graph)
        {
            weight += edge.EdgeWeight;
        }

        return weight;
    }

    /// <summary>
    /// Returns a string representation of the graph.
    /// </summary>
    /// <returns>A string showing all edges in the graph.</returns>
    public override string ToString()
    {
        string result = string.Empty;

        foreach (var edge in this._graph)
        {
            result += $"{edge.VertexA} {edge.VertexB} {edge.EdgeWeight}\n";
        }

        return result;
    }

    /// <summary>
    /// Sorts the edges in the graph by weight.
    /// </summary>
    public void Sort()
    {
        this._graph.Sort();
    }

    /// <summary>
    /// Determines whether the graph is connected (all vertices are reachable from any other vertex).
    /// </summary>
    /// <param name="graph">The graph to check.</param>
    /// <returns>True if the graph is connected, false otherwise.</returns>
    public static bool IsGraphConnected(Graph graph)
    {
        if (graph.Count() == 0)
        {
            return false;
        }

        HashSet<int> visited = new HashSet<int>();
        Queue<int> queue = new Queue<int>();
        var enumerator = graph.GetEnumerator();
        using var enumerator1 = enumerator as IDisposable;
        if (enumerator1 == null)
        {
            throw new ArgumentNullException(nameof(enumerator1));
        }

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
// <copyright file="Node.cs" author="AV-Bog">
// under MIT License
// </copyright>

namespace HW3.LZW;

/// <summary>
/// Represents a node in the LZW dictionary tree.
/// </summary>
public class Node()
{
    private byte Symbol { get; }

    private Dictionary<byte, Node> SubNodes { get; } = new();

    private int Code { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Node"/> class with a specific symbol.
    /// </summary>
    /// <param name="symbol">The byte symbol to store in this node.</param>
    public Node(byte symbol)
        : this()
    {
        this.Symbol = symbol;
    }

    /// <summary>
    /// Attempts to find a subnode by symbol.
    /// </summary>
    /// <param name="symbol">The symbol to search for.</param>
    /// <returns>The found node or null if not found.</returns>
    public Node? TryFind(byte symbol)
    {
        return this.SubNodes.TryGetValue(symbol, out var node) ? node : null;
    }

    /// <summary>
    /// Adds a new node to the dictionary tree.
    /// </summary>
    /// <param name="key">The byte sequence representing the path to the new node.</param>
    /// <param name="code">The code to associate with the new node.</param>
    public void AddNode(byte[] key, int code)
    {
        Node current = this;
        foreach (var b in key)
        {
            if (!current.SubNodes.ContainsKey(b))
            {
                current.SubNodes[b] = new Node(0);
            }

            current = current.SubNodes[b];
        }

        current.Code = (ushort)code;
    }

    /// <summary>
    /// Attempts to get the code associated with a byte sequence.
    /// </summary>
    /// <param name="key">The byte sequence to search for.</param>
    /// <param name="code">Output parameter for the found code.</param>
    /// <returns>True if the code was found, false otherwise.</returns>
    public bool TryGetCode(byte[] key, out int code)
    {
        Node current = this;
        foreach (byte b in key)
        {
            if (!current.SubNodes.TryGetValue(b, out current!))
            {
                code = 0;
                return false;
            }
        }

        code = current.Code;
        return code != 0;
    }
}

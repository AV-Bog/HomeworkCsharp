// <copyright file="Trie.cs" author="AV-Bog">
// under MIT License
// </copyright>

namespace HW3.LZW;

/// <summary>
/// This prefixTree.
/// </summary>
public class Trie
{
    private readonly Node _root = new Node(0);

    private int _nextCode = 0;

    /// <summary>
    /// Initializes a new instance of the <see cref="Trie"/> class.
    /// Pre-populates the Trie with all possible byte values (0-255).
    /// </summary>
    public Trie()
    {
        for (int i = 0; i < 256; i++)
        {
            this._root.AddNode(new byte[] { (byte)i }, i);
        }

        this._nextCode = 256;
    }

    /// <summary>
    /// Adds a word to the tree
    /// </summary>
    /// <param name="key">The byte sequence to add to the Trie.</param>
    public void Add(byte[] key) => _root.AddNode(key, _nextCode++);

    /// <summary>
    /// Attempts to retrieve the code associated with a byte sequence.
    /// </summary>
    /// <param name="key">The byte sequence to search for.</param>
    /// <param name="code">When this method returns, contains the code associated with the specified byte sequence,
    /// if the sequence is found; otherwise, zero.</param>
    /// <returns>true if the Trie contains the specified byte sequence; otherwise, false.</returns>
    public bool TryGetCode(byte[] key, out ushort code)
    {
        if (this._root.TryGetCode(key, out int intCode))
        {
            code = (ushort)intCode;
            return true;
        }

        code = 0;
        return false;
    }

    public int GetNextCode() => _nextCode;
}
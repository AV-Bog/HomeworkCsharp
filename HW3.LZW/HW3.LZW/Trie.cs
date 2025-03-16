namespace HW3.LZW;

/// <summary>
/// This prefixTree.
/// </summary>
/// <typeparam name="T"></typeparam>
public class Trie
{
    /// <summary>
    /// The root node of the Trie. It represents the starting point of the tree
    /// and is initialized with a default symbol ('\0').
    /// </summary>
    private readonly Node _root = new Node(0);

    private int _nextCode = 0;

    public Trie()
    {
        for (int i = 0; i < 256; i++)
        {
            _root.AddNode(new byte[] {(byte)i}, i);
        }
        _nextCode = 256;
    }
    /// <summary>
    /// Adds a word to the tree
    /// </summary>
    /// <param name="key"></param>
    public void Add(byte[] key)
    {
        _root.AddNode(key, _nextCode++);
    }

    /// <summary>
    /// Checks if there is such a word in the tree.
    /// </summary>
    /// <param name="key"></param>
    /// <returns>True if such a word is contained in the tree.</returns>
    public bool TryGetCode(byte[] key, out int code)
    {
        return _root.TryGetCode(key, out code);
    }
    
    public int GetNextCode() => _nextCode;
}

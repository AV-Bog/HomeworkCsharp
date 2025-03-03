namespace HW2.Trie;

/// <summary>
/// This prefixTree.
/// </summary>
/// <typeparam name="T"></typeparam>
public class Trie<T>(int size = 0)
{
    /// <summary>
    /// The root node of the Trie. It represents the starting point of the tree
    /// and is initialized with a default symbol ('\0').
    /// </summary>
    private readonly Node<T>? _root = new Node<T>('\0');
    
    /// <summary>
    /// Gets the number of unique words in the tree
    /// </summary>
    public int Size { get; set; } = size;

    /// <summary>
    /// Adds a word to the tree
    /// </summary>
    /// <param name="key"></param>
    /// <returns>True if there was no such word in the tree.</returns>
    public bool Add(string key)
    {
        if (!Contains(key))
        {
            _root?.AddNode(key); 
            ++Size;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Removes a word from the tree.
    /// </summary>
    /// <param name="key"></param>
    /// <returns>True if such a word existed in the tree.</returns>
    public bool Remove(string key)
    {
        bool result = Node<T>.RemoveNode(key, _root);
        if (result)
        {
            --Size;
        }
        return result;
    }

    /// <summary>
    /// Checks if there is such a word in the tree.
    /// </summary>
    /// <param name="key"></param>
    /// <returns>True if such a word is contained in the tree.</returns>
    public bool Contains(string key)
    {
        return Node<T>.SearchNode(key, _root);
    }

    /// <summary>
    /// Counts how many words in the tree begin with this prefix.
    /// </summary>
    /// <param name="prefix"></param>
    /// <returns>Returns the number of words in the tree that start with this prefix.</returns>
    public int HowManyStartsWithPrefix(String prefix)
    {
        return Node<T>.CountingWordsWithThisPrefix(prefix, _root);
    }
}

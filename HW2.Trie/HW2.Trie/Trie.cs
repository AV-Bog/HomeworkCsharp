namespace HW2.Trie;

/// <summary>
/// This prefixTree.
/// </summary>
/// <typeparam name="T"></typeparam>
public class Trie<T>
{
    private readonly Node<T>? _root;
    /// <summary>
    /// Gets the number of unique words in the tree
    /// </summary>
    public int Size { get; set; }

    /// <summary>
    /// Trie
    /// </summary>
    public Trie()
    {
        _root = new Node<T>('\0');
        Size = 0;
    }

    /// <summary>
    /// Adds a word to the tree
    /// </summary>
    /// <param name="key"></param>
    /// <returns>True if there was no such word in the tree.</returns>
    public bool Add(string key)
    {
        if (!Contains(key))
        {
            Node<T>.AddNode(key, _root); 
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
